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

	i, value := Validate(intArray, 25)

	fmt.Printf("Result: %d T: %d", value, i)
	fmt.Println()
	x, y := FindSequence(intArray, i, value)
	sliced := intArray[x:y]
	sort.Ints(sliced)
	fmt.Println(sliced)

}

func FindSequence(data []int, index int, sum int) (int, int) {
	// fmt.Printf("Data: %v, Index: %d, Value: %d", data, index, data[index])
	// fmt.Println()

	workingSum := data[0]
	left := 0

	for i := 1; i < index; i++ {
		for workingSum > sum && left < i-1 {
			workingSum = workingSum - data[left]
			left++
		}

		if workingSum == sum {
			return left, i
		}

		if i < index {
			workingSum += data[i]
		}
	}
	return 0, 0
}

func Validate(data []int, preambleLength int) (index int, value int) {
	end := preambleLength

	for end < len(data) {
		window := data[end-preambleLength : end]
		// fmt.Printf("Start: %d End: %d Window: %v", end-preambleLength, end, window)

		// fmt.Println()

		if !validatePreamble(window, data[end]) {
			return end, data[end]
		}
		end++
	}

	return 0, 0
}

func validatePreamble(data []int, nextVal int) bool {

	for i := 0; i < len(data); i++ {
		for j := i + 1; j < len(data); j++ {
			sum := data[i] + data[j]

			// fmt.Printf("v: %d, v1: %d, v+v1: %d. nextVal: %d", data[i], data[j], sum, nextVal)
			// fmt.Println()

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
