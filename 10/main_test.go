package main

import (
	"fmt"
	"sort"
	"strings"
	"testing"
)

func TestGetDiffs(t *testing.T) {
	testData := []int{16,
		10,
		15,
		5,
		1,
		11,
		7,
		19,
		6,
		12,
		4}

	oneDiffs, threeDiffs := getDiffs(testData)

	fmt.Printf("%d %d", oneDiffs, threeDiffs)

	if !(oneDiffs == 7 && threeDiffs == 5) {
		t.Error("test failure")
	}

}

type Test struct {
	Input    []int
	Expected [][]int
}

func TestRemoveFrom3(t *testing.T) {
	testData := make([]Test, 0)
	test1 := Test{[]int{0, 1, 2}, make([][]int, 0)}
	test1.Expected = append(test1.Expected, []int{0, 2})
	testData = append(testData, test1)

	// can't remove mid due to too large gap
	test2 := Test{[]int{0, 1, 4}, make([][]int, 0)}
	test2.Expected = append(test2.Expected, []int{0, 1, 4})
	testData = append(testData, test2)

	for _, testCase := range testData {
		actual := removeFrom3(testCase.Input)

		if !equal(testCase.Expected[0], actual) {
			t.Errorf("Expected %v, got %v", testCase.Expected, actual)
		}
	}
}

func TestRemoveFrom4(t *testing.T) {
	expected := make(map[string][]int)
	expected[arrayToString([]int{0, 1, 2, 3})] = []int{0, 1, 2, 3}
	expected[arrayToString([]int{0, 2, 3})] = []int{0, 2, 3}
	expected[arrayToString([]int{0, 1, 3})] = []int{0, 1, 3}
	expected[arrayToString([]int{0, 3})] = []int{0, 3}

	actual := removeFrom4([]int{0, 1, 2, 3})

	if !equal2d(mapOfIntTo2D(expected), actual) {
		t.Errorf("Expected %v, got %v", expected, actual)
	}

}

func TestLongerArray(t *testing.T) {
	input := []int{0, 1, 2, 3, 4, 5}
	actual := removeFromN(input)
	if len(actual) != 8 {
		t.Errorf("Incorrect number of results %v", actual)
	}
}

func TestFromSample(t *testing.T) {
	input := []int{0, 16, 10, 15, 5, 1, 11, 7, 19, 6, 12, 4, 22}
	sort.Ints(input)
	actual := removeFromN(input)
	if len(actual) != 8 {
		t.Errorf("Incorrect number of results. No %d, Results %v", len(actual), actual)
	}
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
	result := make([][]int, 0)
	resultMap := make(map[string][]int)
	result = append(result, data)

	for i := 0; i < len(data)-4; i++ {
		window := data[i : i+4]
		windowResults := removeFrom4(window)
		for _, r := range windowResults {
			toAdd := make([]int, 0)
			toAdd = append(toAdd, data[:i]...)
			toAdd = append(toAdd, r...)
			toAdd = append(toAdd, data[i+4:]...)
			result = append(result, toAdd)
			resultMap[arrayToString(toAdd)] = toAdd
		}
	}

	return result
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
