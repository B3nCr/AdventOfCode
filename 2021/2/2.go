package main

import (
	"bufio"
	"fmt"
	"io"
	"io/ioutil"
	"strconv"
	"strings"
)

// const (
// 	forward = "foward"
// 	up      = "up"
// 	down    = "down"
// )

type instruction struct {
	direction string
	distance  int
}

func SumPositions(instructions []instruction) (int, int) {
	horizontal := 0
	vertical := 0
	for _, v := range instructions {
		if v.direction == "forward" {
			horizontal += v.distance
		}
		if v.direction == "up" {
			vertical -= v.distance
		}
		if v.direction == "down" {
			vertical += v.distance
		}

	}
	return horizontal, vertical
}

func AimAndMove(instructions []instruction) (int, int) {
	aim := 0
	horizontal := 0
	vertical := 0

	for _, v := range instructions {
		fmt.Printf("Direction: %s, Distance: %d\n", v.direction, v.distance)
		if v.direction == "forward" {
			horizontal += v.distance
			if aim != 0 {
				fmt.Printf("Changing vertical\n")
				vertical += aim * v.distance
			}
		}
		if v.direction == "up" {
			fmt.Printf("Aiming up\n")
			aim -= v.distance
			fmt.Printf("Aim: %d\n", aim)
		}
		if v.direction == "down" {
			fmt.Printf("Aiming down\n")
			aim += v.distance
			fmt.Printf("Aim: %d\n", aim)
		}
		fmt.Printf("%d %d\n", horizontal, vertical)
	}
	return horizontal, vertical
}

func ReadInstructions(r io.Reader) ([]instruction, error) {
	scanner := bufio.NewScanner(r)
	scanner.Split(bufio.ScanLines)
	var result []instruction

	for scanner.Scan() {
		// fmt.Println(scanner.Text())
		parts := strings.Split(scanner.Text(), " ")
		x, err := strconv.Atoi(parts[1])
		if err != nil {
			return result, err
		}

		result = append(result, instruction{direction: parts[0], distance: x})
	}
	return result, scanner.Err()
}

func main() {
	b, _ := ioutil.ReadFile("./input.txt")

	instructions, _ := ReadInstructions(strings.NewReader(string(b)))

	horiz, ver := AimAndMove(instructions)

	fmt.Printf("%d", horiz*ver)
	fmt.Println()
}
