package main

import (
	"fmt"
	"os"
	"strings"
)

type Point struct {
	X, Y int
}

func main() {
	input, err := os.ReadFile("input.txt")
	if err != nil {
		fmt.Println("Error reading file:", err)
		return
	}
	lines := strings.Split(string(input), "\n")
	grid := make([][]byte, len(lines))
	for i, line := range lines {
		grid[i] = []byte(line)
	}
	ans := 0
	deleting := true
	for deleting { // For part 2
		var deletedRolls []Point
		for i, row := range grid {
			for j, cell := range row {
				if cell == '@' && IsRollAccessible(grid, i, j) {
					ans++
					deletedRolls = append(deletedRolls, Point{X: i, Y: j})
				}
			}
		}
		for _, point := range deletedRolls {
			grid[point.X][point.Y] = '.'
		}
		if len(deletedRolls) == 0 {
			deleting = false
		}
	}
	fmt.Println(ans)
}

func IsRollAccessible(grid [][]byte, i, j int) bool {
	dirs := [][]int{{0, 1}, {0, -1}, {1, 0}, {-1, 0}, {1, 1}, {1, -1}, {-1, 1}, {-1, -1}}
	acc := 0
	for _, dir := range dirs {
		x, y := i+dir[0], j+dir[1]
		if x >= 0 && x < len(grid) && y >= 0 && y < len(grid[x]) && grid[x][y] == '@' {
			acc += 1
		}
	}
	return acc < 4
}
