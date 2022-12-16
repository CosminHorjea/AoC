package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

type Valve struct {
	flow       int
	neighbours []string
}

// notes: I don;t think i have time to do this today, but i have ideas
// the example seems to prioritize the high flow valves
// so we might be able to decide where to go using a A* heuristic
// it can be something like time to go there - flow that it will generate, since we can calculate the flow
// it might seem like it's a ford fulekrson algorithm, but i think its deterministic, no need to go backtrack or things like that
// maybe precompute a table of distances between valves, and then use that to calculate the heuristic
// while travelling, if we pass through a lower flow valve, we can enable it, even though we are not looking for it initially
// so yeah.... maybe it'll work

func main() {
	// data, err := os.ReadFile("input.txt")
	data, err := os.ReadFile("input_test.txt")
	if err != nil {
		panic(err)
	}
	valves := make(map[string]Valve)
	for _, line := range strings.Split(string(data), "\n") {
		bits := strings.Split(line, " ")
		if len(bits) < 10 {
			panic("invalid line")
		}
		label := bits[1]
		flow := parseFlow(bits[4])
		neighbours := make([]string, 0)
		for i := 9; i < len(bits); i += 1 {
			neighbours = append(neighbours, parseLabel(bits[i]))
		}
		valves[label] = Valve{flow, neighbours}
	}
	fmt.Println(valves)
}

func parseLabel(s string) string {
	s = strings.Trim(s, ";")
	s = strings.Trim(s, " ")
	return s
}

func parseFlow(s string) int {
	num := strings.Split(s, "=")[1]
	num = num[:len(num)-1]
	num_conv, _ := strconv.Atoi(num)
	return num_conv
}
