package main

import (
	"fmt"
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

// func TestRemoveSkipable(t *testing.T) {
// 	testData := []int{16,
// 		10,
// 		15,
// 		5,
// 		1,
// 		11,
// 		7,
// 		19,
// 		6,
// 		12,
// 		4}
// 	removed := removeSkipable(testData)

// 	if !equal(removed[0], []int{0, 2, 3}) {
// 		t.Error(removed)
// 	}
// }

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
	testData := make([]Test, 0)
	testData = append(testData, Test{[]int{0, 1, 2, 3}, make([][]int, 0)})
	testData[0].Expected = append(testData[0].Expected, []int{0, 2, 3})
	testData[0].Expected = append(testData[0].Expected, []int{0, 1, 3})
	testData[0].Expected = append(testData[0].Expected, []int{0, 3})

	for _, test := range testData {
		actual := removeFrom4(test.Input)

		if !equal2d(test.Expected, actual) {
			t.Errorf("Expected %v, got %v", test.Expected, actual)
		}
	}
}

func removeFrom3(data []int) []int {
	// result := make([]int, 0)
	if data[2]-data[0] <= 3 {
		return []int{data[0], data[2]}
	}
	return data
}

func removeFrom4(data []int) [][]int {
	result := make([][]int, 0)

	if data[2]-data[0] <= 3 {
		var firstThree []int
		firstThree = append(firstThree, removeFrom3(data)...)
		firstThree = append(firstThree, data[3])
		result = append(result, firstThree) // append(result, append(data[:1], data[2:]...))
	}

	if data[3]-data[1] <= 3 {
		var lastThree []int
		lastThree = append(lastThree, data[0])
		lastThree = append(lastThree, removeFrom3(data[1:])...)

		result = append(result, lastThree)
	}

	if data[3]-data[0] <= 3 {
		result = append(result, []int{data[0], data[3]})
	}

	return result
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
