package main

import (
	"bufio"
	"fmt"
	"io"
	"io/ioutil"
	"strconv"
	"strings"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

func FindIncreases(depths []int) int {
	count := 0
	prev := depths[0]
	for i, v := range depths {
		if i == 0 {
			continue
		}

		if v > prev {
			count++
		}

		prev = v
	}
	return count
}

func ReadInts(r io.Reader) ([]int, error) {
	scanner := bufio.NewScanner(r)
	scanner.Split(bufio.ScanWords)
	var result []int
	for scanner.Scan() {
		x, err := strconv.Atoi(scanner.Text())
		if err != nil {
			return result, err
		}
		result = append(result, x)
	}
	return result, scanner.Err()
}

func CreateWindows(depths []int) [][]int {
	var slices [][]int
	for i := 0; i <= len(depths)-3; i++ {
		slices = append(slices, depths[i:i+3])
	}
	return slices
}

func Sum(array []int) int {
	result := 0
	for _, v := range array {
		result += v
	}
	return result
}

func SumSlices(arrays [][]int) []int {
	var result []int
	for _, v := range arrays {
		result = append(result, Sum(v))
	}
	return result
}

func main() {
	b, _ := ioutil.ReadFile("./input.txt")

	ints, _ := ReadInts(strings.NewReader(string(b)))

	windows := CreateWindows(ints)

	sumedSlices := SumSlices(windows)

	incs := FindIncreases(sumedSlices)
	fmt.Println(incs)
}
