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
	fmt.Println(len(removeFromN(intArray)))
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

func removeFrom3(data []int) []int {
	if data[2]-data[0] <= 3 {
		return []int{data[0], data[2]}
	}
	return data
}

func removeFrom4(data []int) [][]int {
	resultMap := make(map[string][]int)

	resultMap[arrayToString(data)] = data

	if data[2]-data[0] <= 3 {
		var firstThree []int
		firstThree = append(firstThree, removeFrom3(data)...)
		firstThree = append(firstThree, data[3])

		resultMap[arrayToString(firstThree)] = firstThree
	}

	if data[3]-data[1] <= 3 {
		var lastThree []int
		lastThree = append(lastThree, data[0])
		lastThree = append(lastThree, removeFrom3(data[1:])...)

		resultMap[arrayToString(lastThree)] = lastThree
	}

	if data[3]-data[0] <= 3 {

		resultMap[arrayToString([]int{data[0], data[3]})] = []int{data[0], data[3]}
	}

	r1 := mapOfIntTo2D(resultMap)
	return r1
}

func removeFromN(data []int) [][]int {
	resultMap := make(map[string][]int)

	resultMap[arrayToString(data)] = data

	for i := 0; i < len(data)-4; i++ {
		window := data[i : i+4]
		windowResults := removeFrom4(window)

		for ii, r := range windowResults {
			if ii == 0 {
				toAdd := make([]int, 0)
				toAdd = append(toAdd, data[:i]...)
				toAdd = append(toAdd, r...)
				toAdd = append(toAdd, data[i+4:]...)
				resultMap[arrayToString(toAdd)] = toAdd
			} else {
				toAdd := make([]int, 0)
				toAdd = append(toAdd, data[:i]...)
				toAdd = append(toAdd, r...)
				toAdd = append(toAdd, data[i+4:]...)

				moreResults := removeFromN(toAdd)

				for _, r1 := range moreResults {
					resultMap[arrayToString(r1)] = r1
				}
			}
		}
	}

	return mapOfIntTo2D(resultMap)
}

func mapOfIntTo2D(intMap map[string][]int) [][]int {
	r1 := make([][]int, 0)
	for _, thing1 := range intMap {
		r1 = append(r1, thing1)
	}
	return r1
}

func equal2d(a, b [][]int) bool {
	if len(a) != len(b) {
		return false
	}
	for i := range a {
		if !equal(a[i], b[i]) {
			return false
		}
	}
	return true
}

func equal(a, b []int) bool {
	if len(a) != len(b) {
		return false
	}
	for i, v := range a {
		if v != b[i] {
			return false
		}
	}
	return true
}

func arrayToString(a []int) string {
	delim := ","
	return strings.Trim(strings.Replace(fmt.Sprint(a), " ", delim, -1), "[]")
	//return strings.Trim(strings.Join(strings.Split(fmt.Sprint(a), " "), delim), "[]")
	//return strings.Trim(strings.Join(strings.Fields(fmt.Sprint(a)), delim), "[]")
}
