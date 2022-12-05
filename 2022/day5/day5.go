package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

var (
	stacks_test = [4]string{
		"",
		"ZN",
		"MCD",
		"P",
	}
	stacks = [10]string{
		"",
		"WDGBHRV",
		"JNGCRF",
		"LSFHDNJ",
		"JDSV",
		"SHDRQWNV",
		"PGHCH",
		"FJBGLZHC",
		"SJR",
		"LGSRBNVM",
	}
)

func main() {
	// data, err := os.ReadFile("input_test.txt")
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	for _, row := range strings.Split(string(data), "\n") {
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
