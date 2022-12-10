package main

import (
	"fmt"
	"math"
	"os"
	"strconv"
	"strings"
)

// Part 1
// func checkIfInterestingCycle(cycle int, x_register int) int {
// 	interestingCycles := []int{20, 60, 100, 140, 180, 220}
// 	for _, interestingCycle := range interestingCycles {
// 		if cycle == interestingCycle {
// 			return x_register * interestingCycle
// 		}
// 	}
// 	return 0

// }

// func main() {
// 	data, err := os.ReadFile("input.txt")
// 	if err != nil {
// 		panic(err)
// 	}
// 	commands := strings.Split(string(data), "\n")
// 	total_cycles := 0
// 	x_register := 1
// 	answer := 0
// 	for _, command := range commands {
// 		terms := strings.Split(command, " ")
// 		if terms[0] == "noop" {
// 			total_cycles++
// 			answer += checkIfInterestingCycle(total_cycles, x_register)
// 		}
// 		if terms[0] == "addx" {
// 			total_cycles++
// 			answer += checkIfInterestingCycle(total_cycles, x_register)
// 			total_cycles++
// 			answer += checkIfInterestingCycle(total_cycles, x_register)
// 			value, _ := strconv.Atoi(terms[1])
// 			x_register = x_register + value
// 		}
// 	}
// 	println(answer)

// }

func write_on_screen(screen *[]int, cycle int, x_register int) {
	cycle_row := cycle % 40
	if math.Abs(float64(x_register-cycle_row)) > 1 {
		(*screen)[cycle] = 0
	} else {
		(*screen)[cycle] = 1
	}
}

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	commands := strings.Split(string(data), "\n")
	total_cycles := 0
	x_register := 1
	crt_screen := make([]int, 242)
	for _, command := range commands {
		terms := strings.Split(command, " ")
		if terms[0] == "noop" {
			write_on_screen(&crt_screen, total_cycles, x_register)
			total_cycles++
		}
		if terms[0] == "addx" {
			write_on_screen(&crt_screen, total_cycles, x_register)
			total_cycles++
			write_on_screen(&crt_screen, total_cycles, x_register)
			total_cycles++
			value, _ := strconv.Atoi(terms[1])
			x_register = x_register + value
		}
	}
	showScreen(crt_screen)

}

func showScreen(screen []int) {
	for idx, pixel := range screen {
		if idx%40 == 0 {
			fmt.Println()
		}
		if pixel == 1 {
			fmt.Print("█")
		} else {
			fmt.Print(" ")
		}

	}
}
