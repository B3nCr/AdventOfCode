package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"strings"
)

type Bag struct {
	Colour       string
	ContainsBags []string
}

var bagsAndRules map[string]Bag

func main() {
	content, err := ioutil.ReadFile("./7/file.txt")
	if err != nil {
		log.Fatal(err)
	}

	allRows := strings.Split(string(content), "\r\n")

	bagsAndRules = make(map[string]Bag)

	for i := 0; i < len(allRows); i++ {

		bag, indexOfContains := getBag(allRows[i])

		if !containsBags(allRows[i], indexOfContains) {
			continue
		}

		rules := getRules(allRows[i], indexOfContains)
		bagsAndRules[bag] = Bag{bag, rules}
	}

	count := 0
	fmt.Println(canContainBag("shiny gold", count, bagsAndRules))

}

func canContainBag(find string, count int, bags map[string]Bag) int {

	if len(bags) == 0 {
		return count
	}

	for _, bag := range bags {
		for i := 0; i < len(bag.ContainsBags); i++ {
			if bag.ContainsBags[i] == find {
				return 1
			}
		}
	}

	return count
}

func getBag(row string) (string, int) {
	indexOfContains := strings.Index(row, "contain ")
	bag := row[:indexOfContains-6]

	// fmt.Println("Bag: " + bag)
	return bag, indexOfContains
}

func containsBags(row string, indexOfContains int) bool {
	return row[indexOfContains+8:len(row)-1] != "no other bags"
}

func getRules(row string, indexOfContains int) []string {
	bagRules := strings.Split(row[indexOfContains+8:len(row)-1], ", ")

	rules := make([]string, 1)

	for r := 0; r < len(bagRules); r++ {
		splitRule := strings.Split(bagRules[r], " ")

		// count := splitRule[0]
		colour := splitRule[1] + " " + splitRule[2]
		// fmt.Println("Colour: " + colour + " Count: " + count)
		// i, _ := strconv.Atoi(count)
		rules = append(rules, colour)
	}

	return rules
}
