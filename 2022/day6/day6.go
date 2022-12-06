package main

import (
	"fmt"
	"os"
)

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}

	sequence := string(data)
	// length := 4 // Part 1
	length := 14 //Part 2
	for i := 0; i < len(sequence)-length; i += 1 {
		if areAllUnique(sequence[i : i+length]) {
			fmt.Println(sequence[i : i+length])
			fmt.Println(i + length)
			return
		}
	}

}

func areAllUnique(s string) bool {
	seen := make(map[rune]bool)
	for _, r := range s {
		if seen[r] {
			return false
		}
		seen[r] = true
	}
	return true
}
