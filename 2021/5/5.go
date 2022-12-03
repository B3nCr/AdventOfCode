package main

import (
	"bufio"
	"fmt"
	"io"
	"strconv"
	"strings"
)

func GetVents(r io.Reader) []LineSegment {
	scanner := bufio.NewScanner(r)
	scanner.Split(bufio.ScanLines)

	segments := make([]LineSegment, 0)
	for scanner.Scan() {
		segments = append(segments, GetLineSegment(scanner.Text()))
	}

	return segments
}

func GetLineSegment(line string) LineSegment {
	v := LineSegment{}
	tokens := strings.Split(line, " -> ")
	v.start = GetPoint(tokens[0])
	v.end = GetPoint(tokens[1])
	return v
}

func GetPoint(p string) Point {
	point := Point{}

	tokens := strings.Split(p, ",")

	point.x, _ = strconv.Atoi(tokens[0])
	point.y, _ = strconv.Atoi(tokens[1])

	return point
}

func MakeFloor(vents []LineSegment) [][]uint16 {
	maxX := 0
	maxY := 0
	for _, line := range vents {
		if line.start.x > maxX {
			maxX = line.start.x
		}
		if line.end.x > maxX {
			maxX = line.end.x
		}

		if line.start.y > maxY {
			maxY = line.start.y
		}
		if line.end.y > maxY {
			maxY = line.end.y
		}
	}

	oceanFloor := make([][]uint16, maxY+1)

	for i := range oceanFloor {
		oceanFloor[i] = make([]uint16, maxX+1)
	}

	return oceanFloor
}

type LineSegment struct {
	start Point
	end   Point
}

type YDirection int8
type XDirection int8

const (
	Up YDirection = iota
	Down
)
const (
	Left XDirection = iota
	Right
)

type Direction struct {
	YDirection
	XDirection
}

func (l LineSegment) Direction() Direction {
	res := Direction{}
	if l.start.y < l.end.y {
		res.YDirection = Up
	} else {
		res.YDirection = Down
	}
	if l.start.x < l.end.x {
		res.XDirection = Right
	} else {
		res.XDirection = Left
	}
	return res
	// res.XDirection
	// res.YDirection
	// minY := Min(v.start.y, v.end.y)
	// maxY := Max(v.start.y, v.end.y)

	// minX := Min(v.start.x, v.end.x)
	// maxX := Max(v.start.x, v.end.x)
}

func (l LineSegment) IsDiagonal() bool {
	return !(l.start.x == l.end.x || l.start.y == l.end.y)
}

func (l LineSegment) GetMidpoint() Point {
	midpoint := Point{x: l.start.x + l.end.x/2, y: l.start.y + l.end.y/2}
	return midpoint
}

type Point struct {
	x int
	y int
}

func Min(a, b int) int {
	if a < b {
		return a
	}
	return b
}

func Max(a, b int) int {
	if a > b {
		return a
	}
	return b
}

func (v LineSegment) GetAllMidpoints() []Point {
	isDiagonal := v.IsDiagonal()

	if isDiagonal {
		fmt.Printf("Is Diagonal %v", isDiagonal)

	}

	vertical := v.start.y == v.end.y

	if !vertical {
		minY := Min(v.start.y, v.end.y)
		maxY := Max(v.start.y, v.end.y)

		points := make([]Point, maxY-minY+1)

		for x := minY; x <= maxY; x++ {
			points[x-minY] = Point{x: v.start.x, y: x}
		}
		return points
	} else {
		minX := Min(v.start.x, v.end.x)
		maxX := Max(v.start.x, v.end.x)

		points := make([]Point, maxX-minX+1)

		for x := minX; x <= maxX; x++ {
			points[x-minX] = Point{x: x, y: v.start.y}
		}
		return points
	}
}
