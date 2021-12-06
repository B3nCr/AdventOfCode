package main

import (
	"bufio"
	"fmt"
	"io"
	"io/ioutil"
	"strconv"
	"strings"
	"testing"
)

func TestFindAndSum(t *testing.T) {
	// 14 21 17 24  4
	// 10 16 15  9 19
	// 18  8 23 26 20
	// 22 11 13  6  5
	//  2  0 12  3  7

	draw := []int{7, 4, 9, 5, 11, 17, 23, 2, 0, 14, 21, 24, 10, 16, 13, 6, 15, 25, 12, 22, 18, 20, 8, 19, 3, 26, 1}

	rowMap := []map[int]int{{14: 0, 21: 0, 17: 0, 24: 0, 4: 0}, {10: 0, 16: 0, 15: 0, 9: 0, 19: 0}, {18: 0, 8: 0, 23: 0, 26: 0, 20: 0}, {22: 0, 11: 0, 13: 0, 6: 0, 5: 0}, {2: 0, 0: 0, 12: 0, 3: 0, 7: 0}}
	colMap := []map[int]int{{14: 0, 10: 0, 18: 0, 22: 0, 2: 0}, {21: 0, 16: 0, 8: 0, 11: 0, 0: 0}, {17: 0, 15: 0, 23: 0, 13: 0, 12: 0}, {24: 0, 9: 0, 26: 0, 6: 0, 3: 0}, {4: 0, 19: 0, 20: 0, 5: 0, 7: 0}}

	winner := FindWinnerOnBoard(draw, colMap, rowMap)
	// fmt.Println()
	// fmt.Print(rowMap)

	if winner == -1 {
		t.Errorf("There is supposed to be a winner. Got %d", winner)
	}

	sum := SumUnmarkedNumbers(rowMap)

	if sum != 188 {
		t.Errorf("Sum should equal 188. Got %d", sum)
	}
}

func TestReadInput(t *testing.T) {
	b, _ := ioutil.ReadFile("./test_input.txt")

	instructions, _ := ReadInput(strings.NewReader(string(b)))

	if len(instructions) != 27 {
		t.Errorf("Length should be 27. Got: %d", len(instructions))
	}
}

func ReadInput(r io.Reader) ([]int, error) {
	scanner := bufio.NewScanner(r)
	scanner.Split(bufio.ScanLines)

	first := true

	for scanner.Scan() {
		fmt.Print(scanner.Text())
		if first {
			draw, err := GetDraw(scanner.Text())
			fmt.Println(draw)
		} else {
			break
		}

		if first {
			first = false
		}
	}
	return nil, scanner.Err()
}

func GetDraw(drawString string) ([]int, error) {
	draw := []int{}

	for _, val := range strings.Split(drawString, ",") {
		num, err := strconv.Atoi(val)

		if err != nil {
			return draw, err
		}

		draw = append(draw, num)

		fmt.Printf("DrawArray in GetDraw %v\n", draw)
	}

	return draw, nil
}
