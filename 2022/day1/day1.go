package main

import (
	"fmt"
	"log"
	"math"
	"os"
	"sort"
	"strconv"
	"strings"
)

func sumCaloriesForElf(elf string) int {
	total := 0
	for _, line := range strings.Split(elf, "\n") {
		value, err := strconv.Atoi(line)
		if err != nil {
			log.Fatal(err)
		}
		total += value
	}
	return total
}

func part1(elfs []string) int {
	max_total := 0
	for _, elf := range elfs {
		max_total = int(math.Max(float64(max_total), float64(sumCaloriesForElf(elf))))
	}
	return max_total
}

func part2(elfs []string) int {
	totals := []int{}
	for _, elf := range elfs {
		totals = append(totals, sumCaloriesForElf(elf))
	}
	sort.Ints(totals)
	top_3 := totals[len(totals)-3:]
	return top_3[0] + top_3[1] + top_3[2]
}

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		return
	}
	// elfs := regexp.MustCompile(`\n\s*\n`).Split(string(data), -1)
	elfs := strings.Split(string(data), "\n\n")
	fmt.Printf("Part 1, %d\n", part1(elfs))
	fmt.Printf("Part 2, %d\n", part2(elfs))
}
