package main

import (
	"fmt"
	"log"
	"os"
	"regexp"
	"sort"
	"strconv"
	"strings"
)

// func main() {
// 	data, err := os.ReadFile("input.txt")
// 	if err != nil {
// 		return
// 	}
// 	elfs := regexp.MustCompile(`\n\s*\n`).Split(string(data), -1)
// 	max_total := 0
// 	for _, elf := range elfs {
// 		total := 0
// 		// fmt.Println(elf)
// 		for _, line := range strings.Split(elf, "\n") {
// 			value, err := strconv.Atoi(line)
// 			if err != nil {
// 				log.Fatal(err)
// 			}
// 			total += value
// 		}
// 		max_total = int(math.Max(float64(max_total), float64(total)))
// 		total = 0
// 	}
// 	fmt.Printf("Max Total, %d\n", max_total)
// }

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		return
	}
	elfs := regexp.MustCompile(`\n\s*\n`).Split(string(data), -1)
	totals := []int{}
	for _, elf := range elfs {
		total := 0
		for _, line := range strings.Split(elf, "\n") {
			value, err := strconv.Atoi(line)
			if err != nil {
				log.Fatal(err)
			}
			total += value
		}
		totals = append(totals, total)
		total = 0
	}
	sort.Ints(totals)
	top_3 := totals[len(totals)-3 : len(totals)]
	fmt.Printf("Max Total, %d\n", top_3[0]+top_3[1]+top_3[2])
	// for _, t := range top_3 {
	// 	fmt.Println(t)
	// }

}
