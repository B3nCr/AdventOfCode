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
	b, _ := ioutil.ReadFile("./input.txt")

	draw, boards := ReadInput(strings.NewReader(string(b)))

	if len(boards) != 3 {
		t.Errorf("%d", len(boards))
	}

	fmt.Print(boards)

	earliestWin := 999999
	earliestWinningBoard := 0
	earliestWinningScore := 0
	latestWin := 0
	latestWinningBoard := 0
	latestWinningScore := 0
	for i, layout := range boards {
		board := Board{Layout: layout}
		board = board.CalcResult(draw)
		if board.HasWon && board.Round < earliestWin {
			earliestWin = board.Round
			earliestWinningBoard = i
			earliestWinningScore = board.Score
		}
		if board.HasWon && board.Round > latestWin {
			latestWin = board.Round
			latestWinningBoard = i
			latestWinningScore = board.Score
		}
	}

	fmt.Printf("Board %d wins on round %d with score %d\n", earliestWinningBoard, earliestWin, earliestWinningScore)
	fmt.Printf("Board %d wins on round %d with score %d\n", latestWinningBoard, latestWin, latestWinningScore)
}

func (b Board) CalcResult(draw []int) Board {
	for round, pick := range draw {
		found := false
		for row := range b.Layout { // replace with range
			for col := range b.Layout[row] {
				if b.Layout[row][col] == pick {
					b.State[row][col] = true
					found = true
					break
				}
			}
			if found {
				break
			}
		}
		b.Round = round + 1 // because of 0 index
		if b.State.won() {
			b.HasWon = true
			b.Score = b.CalcScore(pick)
			return b
		}
	}

	return b
}

func (b Board) CalcScore(pick int) int {
	score := 0
	for row := range b.Layout {
		for col := range b.Layout[row] {
			if !b.State[row][col] {
				score += b.Layout[row][col]
			}
		}
	}
	score *= pick
	return score
}

func (s BoardState) won() bool {
	result := false
	for _, row := range s {
		result = checkLine(row)
		if result {
			return result
		}
	}
	for colNum := range s {
		var column [5]bool
		for i := 0; i < 5; i++ {
			column[i] = s[i][colNum]
		}
		result = checkLine(column)
		if result {
			return result
		}
	}
	return result
}

func checkLine(line [5]bool) bool {
	return line[0] && line[1] && line[2] && line[3] && line[4]
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
