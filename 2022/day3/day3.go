package main

import (
	"flag"
	"fmt"
	"os"
	"strings"
)

const proprities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"

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
	// Part1
	total := 0
	for _, line := range strings.Split(string(data), "\n") {
		firstHalf := line[:len(line)/2]
		secondHalf := line[len(line)/2:]
		// fmt.Printf("%s %s\n", firstHalf, secondHalf)
		seenItems := map[string]bool{}
		for i := 0; i < len(firstHalf); i++ {
			seenItems[string(firstHalf[i])] = true
		}
		for i := 0; i < len(secondHalf); i++ {
			if seenItems[string(secondHalf[i])] {
				// fmt.Printf("Found %s in both halves\n", string(secondHalf[i]))
				total = total + strings.Index(proprities, string(secondHalf[i])) + 1
				break
			}
		}
	}
	fmt.Printf("Part 1: %d\n", total)

	total = 0
	lines := strings.Split(string(data), "\n")
	// group lines in chunks of 3
	lines_chunks := [][]string{}
	for i := 0; i < len(lines); i += 3 {
		lines_chunks = append(lines_chunks, lines[i:i+3])
	}
	for _, chunk := range lines_chunks {
		seenItems := map[string]int{}
		for i := 0; i < len(chunk[0]); i++ {
			if seenItems[string(chunk[0][i])] == 0 {
				seenItems[string(chunk[0][i])] = seenItems[string(chunk[0][i])] + 1
			}
		}
		for i := 0; i < len(chunk[1]); i++ {
			if seenItems[string(chunk[1][i])] == 1 {
				seenItems[string(chunk[1][i])] = seenItems[string(chunk[1][i])] + 1
			}
		}
		for i := 0; i < len(chunk[2]); i++ {
			if seenItems[string(chunk[2][i])] == 2 {
				total = total + strings.Index(proprities, string(chunk[2][i])) + 1
				break
			}
		}
	}
	fmt.Println("Part 2:", total)

}
