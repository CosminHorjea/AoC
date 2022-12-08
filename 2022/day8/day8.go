package main

import (
	"os"
	"strings"
)

func isVisible(heights [][]int, i, j int) bool { //Part1
	flag := true
	// check left

	for k := 0; k < j; k++ {
		if heights[i][k] >= heights[i][j] {
			flag = false
		}
	}
	if flag {
		return true
	}
	flag = true

	// check right
	for k := j + 1; k < len(heights); k++ {
		if heights[i][k] >= heights[i][j] {
			flag = false
		}
	}
	if flag {
		return true
	}
	flag = true

	// check up
	for k := 0; k < i; k++ {
		if heights[k][j] >= heights[i][j] {
			flag = false
		}
	}
	if flag {
		return true
	}
	flag = true

	// check down
	for k := i + 1; k < len(heights); k++ {
		if heights[k][j] >= heights[i][j] {
			flag = false
		}
	}
	return flag
}

func countVisible(heights [][]int, i, j int) int { //part 2
	left := 0
	// check left
	for k := j - 1; k >= 0; k-- {
		if heights[i][k] >= heights[i][j] {
			left++
			break
		}
		left++
	}
	up := 0
	// check up
	for k := i - 1; k >= 0; k-- {
		if heights[k][j] >= heights[i][j] {
			up++
			break
		}
		up++
	}

	right := 0
	// check right
	for k := j + 1; k < len(heights); k++ {
		if heights[i][k] >= heights[i][j] {
			right++
			break
		}
		right++
	}

	down := 0
	// check down
	for k := i + 1; k < len(heights); k++ {
		if heights[k][j] >= heights[i][j] {
			down++
			break
		}
		down++
	}
	return left * up * right * down
}

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	lines := strings.Split(string(data), "\n")
	// total_visible := 4*len(lines) - 4
	heights := make([][]int, len(lines))

	for i := 0; i < len(lines); i++ {
		heights[i] = make([]int, len(lines))
		for j := 0; j < len(lines); j++ {
			heights[i][j] = int(lines[i][j] - '0')
		}
	}

	// for i := 1; i < len(lines)-1; i++ { //part1
	// 	for j := 1; j < len(lines)-1; j++ {
	// 		if isVisible(heights, i, j) {
	// 			total_visible++
	// 			fmt.Printf("%d at (%d, %d) is visible\n", heights[i][j], i, j)
	// 		}
	// 	}
	// }
	maximum_trees := 0 //part2
	for i := 1; i < len(lines)-1; i++ {
		for j := 1; j < len(lines)-1; j++ {
			visible_trees := countVisible(heights, i, j)
			if visible_trees > maximum_trees {
				maximum_trees = visible_trees
			}

		}
	}

	println(maximum_trees)

}

// 726 low
// 2234 too high
