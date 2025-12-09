package main

import (
	"fmt"
	"os"
	"regexp"
	"strconv"
	"strings"
)

func main() {
	inputFile := os.Args[1]
	input, err := os.ReadFile(inputFile)
	if err != nil {
		fmt.Println("Error reading file:", err)
		return
	}
	lines := strings.Split(string(input), "\n")
	if os.Args[2] == "2" {
		// MO DU LA RI TY
		part2(lines)
		return
	}
	re := regexp.MustCompile(`\d+`)
	var numbers [][]int
	for _, line := range lines {
		matches := re.FindAllString(line, -1)
		if len(matches) <= 0 {
			continue
		}
		// add to a map
		var nums []int
		for _, match := range matches {
			var num int
			fmt.Sscanf(match, "%d", &num)
			nums = append(nums, num)
		}
		numbers = append(numbers, nums)
	}
	fmt.Println(len(numbers))
	symbolLine := lines[len(lines)-2]
	symbols := regexp.MustCompile(`[+*]`).FindAllString(symbolLine, -1)
	result := evaluateExpression(numbers, symbols)
	fmt.Println("Result:", result)
}

func evaluateExpression(numbers [][]int, symbols []string) any {
	if len(numbers) == 0 {
		return nil
	}
	result := 0
	for i := 0; i < len(symbols); i++ {
		symbol := symbols[i]
		localresult := 0
		if symbol == "*" {
			localresult = 1
		}
		for j := 0; j < len(numbers); j++ {
			switch symbol {
			case "+":
				localresult += numbers[j][i]
			case "*":
				localresult *= numbers[j][i]
			}
		}
		fmt.Printf("Row %d got %d \n", i, localresult)
		result += localresult
	}
	return result

}

func part2(lines []string) {
	ans := 0
	rows := len(lines)
	cols := len(lines[0])
	for i := 1; i < rows; i++ {
		cols = max(cols, len(lines[i]))
	}
	currentBuffer := make([]int, 0)
	currentSign := lines[rows-2][0]
	for col := 0; col < cols; col++ {
		num := ""
		for row := 0; row < rows-2; row++ {
			if row >= len(lines) || col >= len(lines[row]) {
				continue
			}
			num += string(lines[row][col])
		}
		println("Composed number", num)
		if len(strings.TrimSpace(num)) == 0 {
			println("Entered")
			switch currentSign {
			case '+':
				ans += sumSlice(currentBuffer)
				fmt.Println("Sum:", ans, currentBuffer)
				currentBuffer = make([]int, 0)
			case '*':
				ans += productSlice(currentBuffer)
				fmt.Println("Product:", ans, currentBuffer)
				currentBuffer = make([]int, 0)
			default:
				fmt.Println("Unknown sign:", string(currentSign))
			}
			currentSign = lines[rows-2][col+1]
			continue
		}
		numConverted, err := strconv.Atoi(strings.TrimSpace(num))
		if err != nil {
			println("error on number", num)
			continue
		}
		currentBuffer = append(currentBuffer, numConverted)
	}
	switch currentSign {
	case '+':
		ans += sumSlice(currentBuffer)
		fmt.Println("Sum:", ans, currentBuffer)
	case '*':
		ans += productSlice(currentBuffer)
		fmt.Println("Product:", ans, currentBuffer)
	default:
		fmt.Println("Unknown sign:", string(currentSign))
	}
	fmt.Println(ans)
}

func sumSlice(currentBuffer []int) int {
	result := 0
	for _, val := range currentBuffer {
		result += val
	}
	return result
}

func productSlice(currentBuffer []int) int {
	result := 1
	for _, val := range currentBuffer {
		result *= val
	}
	return result

}

// 8843673191364 low
// 8843673199391
