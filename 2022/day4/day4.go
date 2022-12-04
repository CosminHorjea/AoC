package main

import (
	"flag"
	"fmt"
	"os"
	"strconv"
	"strings"
)

type Pair struct {
	left  int
	right int
}

func main() {
	filename := "input.txt"
	var testFlag = flag.Bool("test", false, "Run test")
	flag.Parse()
	if *testFlag {
		filename = "input_test.txt"
	}
	data, err := os.ReadFile(filename)
	if err != nil {
		panic(err)
	}
	// Part 1
	total := 0
	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		sections := strings.Split(line, ",")
		first_section := strings.Split(sections[0], "-")
		first_pair := toPair(first_section)
		second_section := strings.Split(sections[1], "-")
		second_pair := toPair(second_section)
		if first_pair.Contains(second_pair) || second_pair.Contains(first_pair) {
			total++
		}
	}
	fmt.Println("Part 1", total)

	// part2
	total = 0
	lines = strings.Split(string(data), "\n")
	for _, line := range lines {
		sections := strings.Split(line, ",")
		first_section := strings.Split(sections[0], "-")
		second_section := strings.Split(sections[1], "-")

		first_pair := toPair(first_section)
		second_pair := toPair(second_section)

		if first_pair.Overlaps(second_pair) {
			total++
		}
	}
	fmt.Println("Part 2:", total)
}

func toPair(section []string) Pair {
	left, _ := strconv.Atoi(section[0])
	right, _ := strconv.Atoi(section[1])
	return Pair{left, right}
}

func (p Pair) Contains(other_pair Pair) bool {
	return p.left <= other_pair.left && p.right >= other_pair.right
}

func (p Pair) Overlaps(other_pair Pair) bool {
	if p.left > other_pair.right {
		return false
	}
	if p.right < other_pair.left {
		return false
	}
	return true
}
