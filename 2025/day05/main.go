package main

import (
	"fmt"
	"os"
	"sort"
	"strconv"
	"strings"
)

func main() {
	input, err := os.ReadFile("input.txt")
	if err != nil {
		fmt.Println("Error reading file:", err)
		return
	}
	lines := strings.Split(string(input), "\n")
	intervals := make([][2]int, len(lines))
	ans := 0
	for i, line := range lines {
		if strings.Contains(line, "-") {
			parts := strings.Split(line, "-")
			intervals[i][0], _ = strconv.Atoi(parts[0])
			intervals[i][1], _ = strconv.Atoi(parts[1])
		} else {
			if len(line) == 0 {
				continue
			}
			itemId, _ := strconv.Atoi(line)
			if isFreshItem(itemId, intervals) {
				ans++
			}
		}
	}
	fmt.Printf("Part 1: %d\n", ans)
	fmt.Printf("Part 2: %d\n", calculateAllFreshItemsIds(intervals))

}

func calculateAllFreshItemsIds(intervals [][2]int) any {
	sort.Slice(intervals, func(i, j int) bool {
		return intervals[i][0] < intervals[j][0]
	})
	intervals = removeZeroValues(intervals)
	total := intervals[0][1] - intervals[0][0] + 1
	rightmost := intervals[0][1]
	for i := 1; i < len(intervals); i++ {
		if intervals[i][1] <= rightmost {
		} else if intervals[i][0] <= rightmost && intervals[i][1] > rightmost {
			total += intervals[i][1] - rightmost
			rightmost = intervals[i][1]
		} else {
			total += intervals[i][1] - intervals[i][0] + 1
			rightmost = intervals[i][1]
		}
	}
	return total
}

func removeZeroValues(intervals [][2]int) [][2]int {
	var result [][2]int
	for _, interval := range intervals {
		if interval[0] != 0 && interval[1] != 0 {
			result = append(result, interval)
		}
	}
	return result
}

func isFreshItem(itemId int, intervals [][2]int) bool {
	for _, interval := range intervals {
		if itemId >= interval[0] && itemId <= interval[1] {
			return true
		}
	}
	return false
}

// 309610011127990 low
// 347468726696961
