package main

import (
	"bufio"
	"fmt"
	"io"
	"io/ioutil"
	"strconv"
	"strings"
)

func getArrayAsString(input []int) string {
	return strings.Trim(strings.Replace(fmt.Sprint(input), " ", "", -1), "[]")
}

func GetMostCommonValues(diagnostic [][]int) []int {
	totalLength := len(diagnostic)
	columns := make([]int, len(diagnostic[0]))

	for _, row := range diagnostic {
		for i, value := range row {
			columns[i] += value
		}
	}

	result := make([]int, len(diagnostic[0]))

	for i, val := range columns {
		if val*2 >= totalLength {
			result[i] = 1
		} else {
			result[i] = 0
		}
	}

	return result
}

func GetDiagnostics(r io.Reader) ([][]int, error) {
	scanner := bufio.NewScanner(r)
	scanner.Split(bufio.ScanBytes)
	var result [][]int
	var row []int
	for scanner.Scan() {

		if scanner.Text() == "\n" {
			result = append(result, row)
			row = []int{}
			continue
		}

		x, err := strconv.Atoi(scanner.Text())
		if err != nil {
			return result, err
		}

		row = append(row, x)
	}
	return result, scanner.Err()
}

func ConvertBinaryToDecimal(array []int) int64 {
	arrayAsString := getArrayAsString(array)
	res, _ := strconv.ParseInt(arrayAsString, 2, 32)
	return res
}

func InvertArray(array []int) []int {
	var result []int
	for _, val := range array {
		if val == 1 {
			result = append(result, 0)
		} else {
			result = append(result, 1)
		}
	}
	return result
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

func FilterOnColumn(data [][]int, index int, keep int) [][]int {
	if len(data) == 2 && Equal(data[0], data[1]) {
		return [][]int{data[0]}
	}

	result := make([][]int, 0)

	for i := 0; i < len(data); i++ {
		if data[i][index] == keep {
			result = append(result, data[i])
		}
	}
	return result
}

func FilterToOne(data [][]int) [][]int {
	for i := 0; i < len(data[0]); i++ {
		mask := GetMostCommonValues(data)

		if len(data) > 1 {
			data = FilterOnColumn(data, i, mask[i])
		}

	}
	return data
}

func main() {
	b, _ := ioutil.ReadFile("./input.txt")

	diagnostics, _ := GetDiagnostics(strings.NewReader(string(b)))
	o2 := diagnostics
	co2 := diagnostics

	fmt.Printf("O2 Length %d. CO2 Length %d\n", len(o2), len(co2))

	for i := 0; i < len(diagnostics[0]); i++ {
		fmt.Printf("Filtering based on column: %d. ", i)
		o2Mask := GetMostCommonValues(o2)

		if len(o2) > 1 {
			o2 = FilterOnColumn(o2, i, o2Mask[i])
		}

		fmt.Printf("O2 Length %d. CO2 Length %d\n", len(o2), len(co2))
	}

	fmt.Println()
	fmt.Printf("O2 Length %d. CO2 Length %d\n", len(o2), len(co2))

	for i := 0; i < len(diagnostics[0]); i++ {
		fmt.Printf("Filtering based on column: %d. ", i)
		co2Mask := InvertArray(GetMostCommonValues(co2))

		if len(co2) > 1 {
			co2 = FilterOnColumn(co2, i, co2Mask[i])
		}

		fmt.Printf("O2 Length %d. CO2 Length %d\n", len(o2), len(co2))
	}

	fmt.Printf("O2: %d. CO2: %d. Res: %d\n", ConvertBinaryToDecimal(o2[0]), ConvertBinaryToDecimal(co2[0]), ConvertBinaryToDecimal(o2[0])*ConvertBinaryToDecimal(co2[0]))
}
