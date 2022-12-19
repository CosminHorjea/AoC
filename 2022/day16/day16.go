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

// 766 low
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
	distances := floydWarshall(valves)
	// fmt.Println(distances)
	usefulValesLabel := []string{"BB", "CC", "DD", "EE", "HH"}
	// for label, v := range valves {
	// 	if v.flow > 0 {
	// 		usefulValesLabel = append(usefulValesLabel, label)
	// 	}
	// }
	fmt.Println(usefulValesLabel)
	init := make(map[string]int)
	for _, v := range usefulValesLabel {
		init[v] = 0
	}

	ans := max_pressure(0, "AA", init, distances, valves)
	fmt.Println(distances["AA"])
	fmt.Println(ans)

}

func max_pressure(time int, pos string, opened map[string]int, distances map[string]map[string]int, valves map[string]Valve) int {
	fmt.Println(opened)
	max_p := 0
	for label, isOpen := range opened {
		if isOpen == 1 {
			continue
		}
		next_time := time + distances[pos][label] + 1
		if next_time >= 30 {
			continue
		} else {
			next_pressure := (30 - next_time) * valves[label].flow
			opened[label] = 1

			aux := next_pressure + max_pressure(next_time, label, opened, distances, valves)
			if max_p < aux {
				max_p = aux
			}
		}
	}
	return max_p
}

func floydWarshall(graph map[string]Valve) map[string]map[string]int {
	dist := make(map[string]map[string]int)
	for k := range graph {
		dist[k] = make(map[string]int)
	}

	for k := range graph {
		for k2 := range graph {
			dist[k][k2] = 1000000000
		}
		dist[k][k] = 0
		for _, n := range graph[k].neighbours {
			dist[k][n] = 1
			dist[n][k] = 1
		}
	}
	for k := range graph {
		for i := range graph {
			for j := range graph {
				if dist[i][j] > dist[i][k]+dist[k][j] {
					dist[i][j] = dist[i][k] + dist[k][j]
				}
			}
		}
	}
	return dist
}

func parseLabel(s string) string {
	s = strings.Trim(s, ";")
	s = strings.Trim(s, ",")
	s = strings.Trim(s, " ")
	return s
}

func parseFlow(s string) int {
	num := strings.Split(s, "=")[1]
	num = num[:len(num)-1]
	num_conv, _ := strconv.Atoi(num)
	return num_conv
}
