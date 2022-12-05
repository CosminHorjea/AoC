package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

// hardcode the stacks at the beginning
// var (
// 	stacks_test = [4]string{
// 		"",
// 		"ZN",
// 		"MCD",
// 		"P",
// 	}
// 	stacks = [10]string{
// 		"",
// 		"WDGBHRV",
// 		"JNGCRF",
// 		"LSFHDNJ",
// 		"JDSV",
// 		"SHDRQWNV",
// 		"PGHCH",
// 		"FJBGLZHC",
// 		"SJR",
// 		"LGSRBNVM",
// 	}
// )

func parseStacks(rawData string) []string {
	fmt.Println(rawData)
	lines := strings.Split(rawData, "\n")
	max_length := 0
	for _, line := range lines {
		if len(line) > max_length {
			max_length = len(line)
		}
	}
	transposed := make([]string, max_length)
	for _, line := range lines {
		for j, char := range line {
			transposed[j] += string(char)
		}
	}
	stacks := []string{}
	stacks = append(stacks, "")
	for _, line := range transposed {
		line = strings.ReplaceAll(line, " ", "")
		line = strings.ReplaceAll(line, "[", "")
		line = strings.ReplaceAll(line, "]", "")
		if len(line) == 0 {
			continue
		}
		stacks = append(stacks, reverseString(line[:len(line)-1]))
	}

	return stacks
}

func main() {
	// data, err := os.ReadFile("input_test.txt")
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	lines := strings.Split(string(data), "\n\n")[1]
	stacks := parseStacks(strings.Split(string(data), "\n\n")[0])
	for _, row := range strings.Split(lines, "\n") {
		// fmt.Println(row)
		commands := strings.Split(row, " ")
		qty, _ := strconv.Atoi(commands[1])
		source, _ := strconv.Atoi(commands[3])
		dest, _ := strconv.Atoi(commands[5])
		part := stacks[source][len(stacks[source])-qty:]
		// part = reverseString(part) //PART 1
		stacks[source] = stacks[source][:len(stacks[source])-qty]
		stacks[dest] += part
	}
	result := ""
	for _, stack := range stacks {
		if len(stack) > 0 {
			result += stack[len(stack)-1:]
		}
	}
	fmt.Println(result)

}

func reverseString(s string) string {
	r := []rune(s)
	for i, j := 0, len(r)-1; i < j; i, j = i+1, j-1 {
		r[i], r[j] = r[j], r[i]
	}
	return string(r)
}
