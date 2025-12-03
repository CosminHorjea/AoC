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
	lines := strings.Split(string(input), "\n")

	ans := 0
	ans2 := 0

	for _, line := range lines {
		if strings.TrimSpace(line) == "" {
			continue
		}
		currentJoltage := GetJoltage(line)
		ans += currentJoltage
		ans2 += GetJoltagePart2(line,12)
	}
	fmt.Println("Total joltage:", ans)
	fmt.Println("Total joltage Part 2:", ans2)
}

func GetJoltage(r string) int {
	r = strings.TrimSpace(r)
	maxDigitUntil := make([]int, len(r))
	maxDigitUntil[0] = int(r[0] - '0')
	maxJoltage := 0
	
	for i := 1; i < len(r); i++ {
		currentDigit := int(r[i] - '0')
		if currentDigit >= maxDigitUntil[i-1] {
			maxDigitUntil[i] = currentDigit
		}else{
			maxDigitUntil[i] = maxDigitUntil[i-1]
		}
		tryCurrent := maxDigitUntil[i-1]*10 + currentDigit

		if tryCurrent > maxJoltage {
			maxJoltage = tryCurrent
		}
	}
	
	return maxJoltage
}

func GetJoltagePart2(r string, maxBatteries int) int {
	r = strings.TrimSpace(r)
	if len(r) < maxBatteries {
		return 0
	}

	canRemove := len(r) - maxBatteries
	stack := make([]byte, 0, len(r))

	for i := 0; i < len(r); i++ {
		digit := r[i]
		for len(stack) > 0 && stack[len(stack)-1] < digit && canRemove > 0 {
			stack = stack[:len(stack)-1]
			canRemove--
		}
		stack = append(stack, digit)
	}

	if len(stack) > maxBatteries {
		stack = stack[:maxBatteries]
	}

	resStr := string(stack)
	val, _ := strconv.ParseInt(resStr, 10, 64)
	return int(val)
}