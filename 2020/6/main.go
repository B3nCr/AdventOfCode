package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"strings"
)

func main() {
	content, err := ioutil.ReadFile("./6/file.txt")
	if err != nil {
		log.Fatal(err)
	}

	answers := strings.Split(string(content), "\r\n\r\n")

	sum := 0

	for i := 0; i < len(answers); i++ {

		answerMap := make(map[byte]int)

		groupSize := 1

		for j := 0; j < len(answers[i]); j++ {

			answer := answers[i][j]

			// record group size but don't treat new lines as answers
			if answer == 10 {
				groupSize++
				continue
			}
			if answer == 13 {
				continue
			}

			// store count of answers in map (dictionary)
			answerMap[answer] = answerMap[answer] + 1
		}

		// increment sum for each answer in the group where answer count == group size
		for _, value := range answerMap {
			if value == groupSize {
				sum++
			}
		}
	}
	fmt.Print(sum)
}
