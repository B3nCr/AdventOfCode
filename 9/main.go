package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"strconv"
	"strings"
)

func main() {
	content, err := ioutil.ReadFile("./9/file.txt")
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

	Validate(intArray, 25)
}

func Validate(data []int, preambleLength int) bool {
	fmt.Println(data)
	end := preambleLength

	for end < len(data) {
		window := data[end-preambleLength : end]
		fmt.Printf("Start: %d End: %d Window: %v", end-preambleLength, end, window)

		fmt.Println()

		if !validatePreamble(window, data[end]) {
			fmt.Printf("Not VALID: %d", data[end])
			return false
		}
		fmt.Print(end)

		fmt.Println()
		fmt.Println()
		end++
	}
	return true
}

func validatePreamble(data []int, nextVal int) bool {

	for i := 0; i < len(data); i++ {
		for j := i + 1; j < len(data); j++ {
			sum := data[i] + data[j]

			fmt.Printf("v: %d, v1: %d, v+v1: %d. nextVal: %d", data[i], data[j], sum, nextVal)
			fmt.Println()

			if nextVal == sum {
				return true
			}
		}
	}
	return false
}

func sum(array []int) int {
	result := 0
	for _, v := range array {
		result += v
	}
	return result
}
