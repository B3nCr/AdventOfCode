package main

func FindWinnerOnBoard(draw []int, colMap []map[int]int, rowMap []map[int]int) int {
	rowSums := []int{0, 0, 0, 0, 0}
	colSums := []int{0, 0, 0, 0, 0}
	winner := -1

	for _, v := range draw {

		winner = FindValueInMap(rowMap, rowSums, v)

		if winner == -1 {
			winner = FindValueInMap(colMap, colSums, v)
		}

		// fmt.Printf("%d Row Sum %v\n", i, rowSums)
		// fmt.Printf("%d Row Sum %v\n", i, colSums)
		// fmt.Printf("%d %d %v\n", i, v, rowMap)
		// fmt.Printf("%d %d %v\n", i, v, colMap)

		if winner >= 0 {
			break
		}
	}
	return winner
}

func FindValueInMap(rowMap []map[int]int, rowSums []int, v int) int {

	for ir, row := range rowMap {
		_, present := row[v]
		if present {
			row[v]++
			rowSums[ir]++
			if rowSums[ir] == 5 {
				// fmt.Printf("I broke here. %d", v)
				return v
			}
		}
	}
	return -1
}

func SumUnmarkedNumbers(rows []map[int]int) int {
	sum := 0
	for _, row := range rows {
		for key, val := range row {
			if val != 1 {
				sum += key
			}
		}
	}
	return sum
}
