package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	input, err := os.ReadFile("input.txt")
	if err != nil {
		fmt.Println("Error reading file:", err)
		return
	}
	intervals := strings.Split(string(input), ",")
	ans := 0
	for _, interval := range intervals {
		boundaries := strings.Split(interval, "-")
		if len(boundaries) != 2 {
			fmt.Println("Invalid interval:", interval)
			break;
		}
		left, err := strconv.Atoi(boundaries[0])
		if err != nil {
			fmt.Println("Invalid number:", boundaries[0])
			break;
		}
		right, err := strconv.Atoi(boundaries[1])
		if err != nil {
			fmt.Println("Invalid number:", boundaries[1])
			break;
		}

		for i := left; i <= right; i++ {
			if IsInvalidIdPart2(i) {
				fmt.Println("Invalid ID found:", i)
				ans+=i
			}
		}
	}
	fmt.Println("Total invalid IDs:", ans)
}

func IsInvalidIdPart1(id int) bool {
	idStr := strconv.Itoa(id)
	if(len(idStr) % 2 != 0){
		return false
	}
	firstHalf := idStr[:len(idStr)/2]
	secondHalf := idStr[len(idStr)/2:]
	return firstHalf == secondHalf
}

func IsInvalidIdPart2(id int) bool {
	idStr := strconv.Itoa(id)
	for i := 1; i <= len(idStr)/2; i++ {
		substr := idStr[:i]
		if strings.Count(idStr, substr) == len(idStr)/i && len(idStr)%i == 0 {
			return true
		}
	}
	return false
}

// 1803621227933 high