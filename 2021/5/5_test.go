package main

import (
	"fmt"
	"io/ioutil"
	"strings"
	"testing"
)

func TestImport(t *testing.T) {
	// 	testInput := `0,9 -> 5,9
	// 8,0 -> 0,8
	// 9,4 -> 3,4
	// 2,2 -> 2,1
	// 7,0 -> 7,4
	// 6,4 -> 2,0
	// 0,9 -> 2,9
	// 3,4 -> 1,4
	// 0,0 -> 8,8
	// 5,5 -> 8,2`

	b, _ := ioutil.ReadFile("./input.txt")

	vents := GetVents(strings.NewReader(string(b)))
	fmt.Println(vents)
	oceanFloor := MakeFloor(vents)

	for _, line := range vents {
		if line.IsDiagonal() {
			continue
		}
		points := line.GetAllMidpoints()

		fmt.Println(points)

		for _, point := range points {
			oceanFloor[point.y][point.x]++
		}
	}

	fmt.Println()
	points := 0
	for _, line := range oceanFloor {
		// fmt.Println(line)
		for _, cell := range line {
			if cell >= 2 {
				points++
			}
		}
	}
	fmt.Printf("Points: %d\n", points)

}

func PrintAllPoints(t *testing.T) {
	v := GetLineSegment("8,0 -> 0,8")
	isDiagonal := v.IsDiagonal()
	if !isDiagonal {
		t.Errorf("Vertex is diagonal %v", isDiagonal)
	}

	v = GetLineSegment("1,0 -> 1,9")
	isDiagonal = v.IsDiagonal()
	if isDiagonal {
		t.Errorf("Vertex is not diagonal %v", isDiagonal)
	}
}

func TestLineParse(t *testing.T) {
	line := "173,602 -> 919,602"

	segment := GetLineSegment(line)

	if segment.start.x != 173 {
		t.Errorf("Start X is wrong. Got %d", segment.start.x)
	}
}

func TestGetAllMidpoints(t *testing.T) {
	v := GetLineSegment("0,0 -> 0,4")

	points := v.GetAllMidpoints()

	// fmt.Println(points)

	if len(points) != 5 {
		t.Errorf("Should be 4 points. Got %d", len(points))
	}

	v = GetLineSegment("0,0 -> 4,0")

	points = v.GetAllMidpoints()

	// fmt.Println(points)

	if len(points) != 5 {
		t.Errorf("Should be 4 points. Got %d", len(points))
	}

	v = GetLineSegment("0,4 -> 0,0")

	points = v.GetAllMidpoints()

	// fmt.Println(points)

	if len(points) != 5 {
		t.Errorf("Should be 4 points. Got %d", len(points))
	}
}

func TestGetMidpoint(t *testing.T) {
	v := GetLineSegment("0,0 -> 0,8")
	midpoint := v.GetMidpoint()

	if midpoint.x != 0 && midpoint.y != 4 {
		t.Errorf("Incorrect Midpoint %v", midpoint)
	}

	v = GetLineSegment("0,8 -> 0,0")
	midpoint = v.GetMidpoint()

	if midpoint.x != 0 && midpoint.y != 4 {
		t.Errorf("Incorrect Midpoint %v", midpoint)
	}

	v = GetLineSegment("0,0 -> 0,0")
	midpoint = v.GetMidpoint()

	if midpoint.x != 0 && midpoint.y != 0 {
		t.Errorf("Incorrect Midpoint %v", midpoint)
	}
}
