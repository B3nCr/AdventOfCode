package main

import (
	"fmt"
	"strconv"
	"strings"
	"testing"
)

func TestGetDiagnostics(t *testing.T) {

	diagnostic := "00100\n11110\n10110\n10111\n10101\n01111\n00111\n11100\n10000\n11001\n00010\n01010\n"

	result, _ := GetDiagnostics(strings.NewReader(diagnostic))

	if len(result) != 12 {
		t.Errorf("Result should have 12 rows. Got %d", len(result))
	}
}

func TestAverageColumn(t *testing.T) {
	input := [][]int{{1, 0, 1, 1, 0}, {0, 1, 1, 1, 0}, {1, 0, 1, 1, 0}}
	result := GetMostCommonValues(input)
	if len(result) != 5 {
		t.Errorf("Result should have 5 columns. Got %v", result)
	}
}

func TestGetArrayAsString(t *testing.T) {
	input := []int{1, 0, 1, 1, 0}
	result := getArrayAsString(input)

	if result != "10110" {
		t.Errorf(result)
	}
}

func TestBinaryToDecimal(t *testing.T) {
	input := "1111"
	result, _ := strconv.ParseInt(input, 2, 64)
	if result != 15 {
		t.Error("Expected 15")
	}
}

func TestInvertArray(t *testing.T) {
	input := []int{1, 0, 1}
	result := InvertArray(input)

	if !Equal(result, []int{0, 1, 0}) {
		t.Errorf("Expected inverted array. %v", result)
	}
}

func TestFiltering(t *testing.T) {
	input := [][]int{{1, 0, 1, 1, 0}, {0, 1, 1, 1, 0}, {1, 0, 1, 0, 0}}

	result := FilterOnColumn(input, 0, 1)

	if !Equal(result[0], []int{1, 0, 1, 1, 0}) {
		t.Errorf("%v", result[0])
	}

	if !Equal(result[1], []int{1, 0, 1, 0, 0}) {
		t.Errorf("%v", result[1])
	}
}

func TestGetMostCommonWhenTheresAtie(t *testing.T) {
	input := [][]int{{1, 0, 1, 1, 0}, {1, 0, 1, 1, 1}}

	result := GetMostCommonValues(input)

	if !Equal(result, []int{1, 0, 1, 1, 1}) {
		t.Errorf("%v", result)
	}
}

func TestKeepFiltering(t *testing.T) {

	input := [][]int{{1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1}, {1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1}}
	fmt.Print(GetMostCommonValues(input))
	input = FilterToOne(input)

	if len(input) != 1 {
		t.Errorf("Should return a single result. %d", len(input))
	}
}

func TestGetMostCommon(t *testing.T) {
	input := [][]int{
		{1, 1, 1, 1, 0},
		{1, 0, 1, 1, 0},
		{1, 0, 1, 1, 1},
		{1, 0, 1, 0, 1},
		{1, 1, 1, 0, 0},
		{1, 0, 0, 0, 0},
		{1, 1, 0, 0, 1}}
	result := GetMostCommonValues(input)
	if !Equal(result, []int{1, 0, 1, 0, 0}) {
		t.Errorf("%v", result)
	}
}

func TestGetMostCommon2(t *testing.T) {
	input := [][]int{{1, 0, 1, 1, 0}, {1, 0, 1, 1, 1}}
	result := GetMostCommonValues(input)
	if !Equal(result, []int{1, 0, 1, 1, 1}) {
		t.Errorf("%v", result)
	}
}
