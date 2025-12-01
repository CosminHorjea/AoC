package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

func mod(a, b int) int {
	m := a % b
	if m < 0 {
		m += b
	}
	return m
}

func main() {
	lines, err := os.ReadFile("input.txt")
	if err != nil {
		fmt.Println("Error reading file:", err)
		return
	}
	lineArray := strings.Split(string(lines), "\n")
	fmt.Println("Total lines read:", len(lineArray))
	currentIndex := 50
	ans1 := 0
	ans2 := 0
	maxIndex := 100
	for _, line := range lineArray {
		if len(line) < 2 {
			continue
		}
		direction := line[0]
		distance, _ := strconv.Atoi(line[1:])

		// Part 2: Count zero crossings
		var distToZero int
		if direction == 'L' {
			distToZero = currentIndex
			if distToZero == 0 {
				distToZero = maxIndex
			}
		} else {
			distToZero = maxIndex - currentIndex
		}

		if distance >= distToZero {
			ans2 += 1 + (distance-distToZero)/maxIndex
		}

		switch direction {
		case 'L':
			currentIndex = mod(currentIndex-distance, maxIndex)
		case 'R':
			currentIndex = mod(currentIndex+distance, maxIndex)
		}

		if currentIndex == 0 {
			ans1 += 1
		}

	}
	fmt.Println("Part 1 Answer:", ans1)
	fmt.Println("Part 2 Answer:", ans2)
}
