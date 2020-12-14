package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"sort"
	"strconv"
	"strings"
)

func main() {
	content, err := ioutil.ReadFile("./10/file.txt")
	if err != nil {
		log.Fatal(err)
	}

	allRows := strings.Split(string(content), "\r\n")

	var intArray = []int{}

	for _, i := range allRows {
		j, err := strconv.Atoi(i)
		if err != nil {
			panic(err)
		}
		intArray = append(intArray, j)
	}

	ones, threes := getDiffs(intArray)

	fmt.Printf("%d %d", ones, threes)
	fmt.Println()

	sort.Ints(intArray)
	fmt.Println(removeSkipable(intArray))
}

func getDiffs(adapters []int) (int, int) {
	sort.Ints(adapters)

	currentJolts := 0
	oneDiffs := 0
	threeDiffs := 0

	for i := 0; i < len(adapters); i++ {
		differnce := adapters[i] - currentJolts

		if differnce == 1 {
			oneDiffs++
		} else if differnce == 3 {
			threeDiffs++
		}

		if differnce > 3 {
			break
		}
		currentJolts = adapters[i]
	}
	threeDiffs++

	return oneDiffs, threeDiffs
}

func removeSkipable(input []int) [][]int {

	fmt.Println()

	var out [][]int

	for i := 0; i < len(input)-1; i++ {
		var inner []int
		working := make([]int, len(input))
		copy(working, input)

		fmt.Printf("i: %d input[i]: %d  input[i+2]: %d", i, working[i], working[i+2])
		fmt.Println()
		if input[i+2]-input[i] <= 3 {
			fmt.Printf("Input %v", working)
			fmt.Println()
			fmt.Printf("Front %v Back %v", working[:i+1], working[i+2:])
			fmt.Println()
			fmt.Println()

			inner = append(working[:i+1], working[i+2:]...)
			fmt.Printf("Result: %v\r\n", inner)
			out = append(out, inner)
		}
	}

	return out
}
