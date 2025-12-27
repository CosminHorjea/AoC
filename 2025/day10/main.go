package main

import (
	"fmt"
	"os"
	"regexp"
	"strings"
)

type Machine struct {
	display string
	buttons [][]int
	joltage []int
}

func main() {
	inputFile := os.Args[1]
	input, err := os.ReadFile(inputFile)
	if err != nil {
		fmt.Println("Error reading file:", err)
		return
	}
	lines := strings.Split(string(input), "\n")
	machines := make([]Machine, 0)
	for _, line := range lines {
		if line == "" {
			continue
		}
		machine := Machine{
			display: ParseDisplay(line),
			buttons: ParseButtons(line),
			joltage: ParseJoltageLevels(line),
		}
		machines = append(machines, machine)
	}
	ansPart1 := 0
	ansPart2 := 0

	for _, machine := range machines {
		ansPart1 += getShortestStepsPart1(machine)
		// ansPart2 += getShortestStepsPart2(machine)
	}

	fmt.Println("Answer Part1:", ansPart1)
	fmt.Println("Answer Part2:", ansPart2)
}

type State struct {
	display string
	steps   int
	jolts   []int
}

type StateKey struct {
	jolts string
}

// of coursethis hangs, but i don't know what z3 is and if it is useable in go
func getShortestStepsPart2(machine Machine) int {
	fmt.Println(machine)
	visited := make(map[StateKey]bool)
	queue := make([]State, 0)
	queue = append(queue, State{display: strings.Repeat(".", len(machine.display)), steps: 0, jolts: make([]int, len(machine.joltage))})
	fmt.Println("Starting jolts", queue[0].jolts)
	iteration := 0
	for len(queue) > 0 {
		// fmt.Println("Part2: Iteration", iteration, "Queue length", len(queue))
		iteration++
		current := queue[0]
		queue = queue[1:]
		if equalSlices(current.jolts, machine.joltage) {
			return current.steps
		}
		if visited[StateKey{jolts: fmt.Sprint(current.jolts)}] {
			// fmt.Println("Already visited ", StateKey{jolts: fmt.Sprint(current.jolts)})
			continue
		}
		visited[StateKey{jolts: fmt.Sprint(current.jolts)}] = true
		// fmt.Println("Visited new state key", StateKey{jolts: fmt.Sprint(current.jolts)})
		for _, button := range machine.buttons {
			newDisplay := []rune(current.display)
			newJolts := make([]int, len(current.jolts))
			copy(newJolts, current.jolts)
			for _, pos := range button {
				newJolts[pos]++
				if pos < 0 || pos >= len(newDisplay) {
					continue
				}
				if newDisplay[pos] == '#' {
					newDisplay[pos] = '.'
				} else {
					newDisplay[pos] = '#'
				}
			}
			if !anyHigher(newJolts, machine.joltage) {
				queue = append(queue, State{
					display: string(newDisplay),
					steps:   current.steps + 1,
					jolts:   newJolts,
				})
			}
		}
	}
	return -1
}

func anyHigher(i1 []int, i2 []int) bool {
	for idx := range i1 {
		if i1[idx] > i2[idx] {
			return true
		}
	}
	return false
}

func equalSlices(i1 []int, i2 []int) bool {
	if len(i1) != len(i2) {
		return false
	}
	for idx := range i1 {
		if i1[idx] != i2[idx] {
			return false
		}
	}
	return true
}

func ParseJoltageLevels(line string) []int {
	re := regexp.MustCompile(`\{(\d+(?:,\d+)*)\}`)
	matches := re.FindStringSubmatch(line)
	joltageArray := strings.Split(matches[1], ",")
	joltageInts := make([]int, 0)
	for _, joltage := range joltageArray {
		var joltageInt int
		fmt.Sscanf(joltage, "%d", &joltageInt)
		joltageInts = append(joltageInts, joltageInt)
	}

	return joltageInts
}

func getShortestStepsPart1(machine Machine) int {
	visited := make(map[string]bool)
	queue := make([]struct {
		display string
		steps   int
	}, 0)
	queue = append(queue, struct {
		display string
		steps   int
	}{display: strings.Repeat(".", len(machine.display)), steps: 0})
	for len(queue) > 0 {
		current := queue[0]
		queue = queue[1:]
		if current.display == machine.display {
			return current.steps
		}
		if visited[current.display] {
			continue
		}
		visited[current.display] = true
		for _, button := range machine.buttons {
			newDisplay := []rune(current.display)
			for _, pos := range button {
				if pos < 0 || pos >= len(newDisplay) {
					continue
				}
				if newDisplay[pos] == '#' {
					newDisplay[pos] = '.'
				} else {
					newDisplay[pos] = '#'
				}
			}
			queue = append(queue, struct {
				display string
				steps   int
			}{display: string(newDisplay), steps: current.steps + 1})
		}
	}
	return -1
}

func ParseButtons(line string) [][]int {
	re := regexp.MustCompile(`\((\d+(?:,\d+)*)\)`)
	buttons := make([][]int, 0)
	matches := re.FindAllStringSubmatch(line, -1)
	for _, match := range matches {
		currentButton := strings.Split(match[1], ",")
		buttonInts := make([]int, 0)
		for _, btn := range currentButton {
			var btnInt int
			fmt.Sscanf(btn, "%d", &btnInt)
			buttonInts = append(buttonInts, btnInt)
		}
		buttons = append(buttons, buttonInts)
	}
	return buttons
}

func ParseDisplay(line string) string {
	re := regexp.MustCompile(`\[(.*)]`)
	matches := re.FindStringSubmatch(line)
	if len(matches) != 2 {
		panic("Hell let loose")
	}
	return matches[1]
}
