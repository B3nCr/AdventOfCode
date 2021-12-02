package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"math"
	"sort"
	"strings"
)

func main() {

	// Read entire file content, giving us little control but
	// making it very simple. No need to close the file.
	content, err := ioutil.ReadFile("./5/file.txt")
	if err != nil {
		log.Fatal(err)
	}

	// Convert []byte to string and print to screen
	text := string(content)

	boardingCards := strings.Split(text, "\r\n")

	var ids []int

	var highestId int = 0

	for row := 0; row < len(boardingCards); row++ {
		var minRow, maxRow int = 0, 127
		var minSeat, maxSeat int = 0, 7

		for i := 0; i < len(boardingCards[row]); i++ {
			insByte := boardingCards[row][i]

			switch {
			case insByte == 70 || insByte == 66:
				minRow, maxRow = findRow(minRow, maxRow, insByte)
				break
			case insByte == 76 || insByte == 82:
				minSeat, maxSeat = findRow(minSeat, maxSeat, insByte)
			}

		}

		var id = minRow*8 + minSeat

		if id > highestId {
			highestId = id
		}

		ids = append(ids, id)

		fmt.Print(minRow)

		fmt.Print(" ")
		fmt.Print(minSeat)
		fmt.Print(" ")
		fmt.Print(id)
		fmt.Println()
	}

	sort.Ints(ids)

	for i := 0; i < len(ids)-1; i++ {
		if ids[i+1]-ids[i] > 1 {
			fmt.Print("Bracketing seats ")
			fmt.Print(ids[i])
			fmt.Print(" ")
			fmt.Print(ids[i+1])
			fmt.Println()
		}
	}

	fmt.Print("Highest Id ")
	fmt.Print(highestId)
	fmt.Println()
}

func findRow(minRow int, maxRow int, instruction byte) (int, int) {
	var minMax = maxRow - minRow

	if instruction == 70 || instruction == 76 {

		maxRow = minRow + (minMax / 2)

	} else if instruction == 66 || instruction == 82 {

		var thing = float64(minMax) / float64(2)
		minRow = minRow + int(math.Ceil(thing))
	}

	return minRow, maxRow
}
