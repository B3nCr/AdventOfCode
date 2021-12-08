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

type BoardLayout [5][5]int
type BoardState [5][5]bool
type Board struct {
	Layout BoardLayout
	State  BoardState
	Round  int
	HasWon bool
	Score  int
}

func TestReadInput(t *testing.T) {
	b, _ := ioutil.ReadFile("./test_input.txt")

	_, boards := ReadInput(strings.NewReader(string(b)))

	if len(boards) != 3 {
		t.Errorf("%d", len(boards))
	}

	fmt.Print(boards)
}

func ReadInput(r io.Reader) ([]int, []BoardLayout) {
	scanner := bufio.NewScanner(r)
	scanner.Split(bufio.ScanLines)

	scanner.Scan()
	draw, _ := GetDraw(scanner.Text())

	boards := []BoardLayout{}

	for scanner.Scan() {

		board := BoardLayout{}

		for i := 0; i < 5; i++ {

			scanner.Scan()
			strings := strings.Fields(scanner.Text())
			for si, v := range strings {
				board[i][si], _ = strconv.Atoi(v)
			}

		}
		boards = append(boards, board)
	}

	return draw, boards
}

func GetDraw(drawString string) ([]int, error) {
	draw := []int{}

	for _, val := range strings.Split(drawString, ",") {
		num, err := strconv.Atoi(val)

		if err != nil {
			return draw, err
		}

		draw = append(draw, num)
	}

	return draw, nil
}
