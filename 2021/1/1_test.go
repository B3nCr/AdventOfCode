package main

import (
	"strings"
	"testing"
)

func TestAdd(t *testing.T) {
	ints, _ := ReadInts(strings.NewReader("199\n200\n208\n210\n200\n207\n240\n269\n260\n263\n"))
	got := FindIncreases(ints)
	want := 7

	if got != want {
		t.Errorf("got %d, wanted %d", got, want)
	}
}

func TestCreateWindows(t *testing.T) {
	got := CreateWindows([]int{1, 2, 3, 4, 5, 6})
	length := len(got)
	if length != 4 {
		t.Errorf("Expected 4 items. Got %d", length)
	}
	if !Equal(got[0], []int{1, 2, 3}) {
		t.Errorf("Expected first  slice to be [1,2,3]. Got %v", got[0])
	}
	if !Equal(got[1], []int{2, 3, 4}) {
		t.Errorf("Expected first  slice to be [2,3,4]. Got %v", got[1])
	}
	if !Equal(got[2], []int{3, 4, 5}) {
		t.Errorf("Expected first  slice to be [3,4,5]. Got %v", got[2])
	}
	if !Equal(got[3], []int{4, 5, 6}) {
		t.Errorf("Expected first  slice to be [4,5,6]. Got %v", got[3])
	}
}

func TestSum(t *testing.T) {
	if Sum([]int{1, 2, 3}) != 6 {
		t.Error("You can't add up")
	}
}

func TestSumOfSums(t *testing.T) {
	if !Equal(SumSlices([][]int{{1, 2, 3}, {1, 2, 3}}), []int{6, 6}) {
		t.Error("A bad thing happened")
	}
}

func Equal(a, b []int) bool {
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
