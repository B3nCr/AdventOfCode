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

// func testRemoveFrom4(t *testing.T) {
// 	testData := make([]Test, 0)
// 	testData = append(testData, Test{[]int{0, 1, 2, 3}, []int{0, 2}})
// 	testData = append(testData, Test{[]int{1, 2, 3}, []int{1, 3}})

// 	for _, test := range testData {
// 		actual := removeFrom3(test.Input)

// 		if !equal(test.Expected, actual) {
// 			t.Errorf("Expected %v, got %v", test.Expected, actual)
// 		}
// 	}
// }

func removeFrom3(data []int) []int {
	if data[2]-data[0] <= 3 {
		return append(data[:1], data[2:]...)
	}
	return data
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
