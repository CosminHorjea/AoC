package main

import (
	"fmt"
	"math"
	"os"
	"strconv"
	"strings"
)

type Cube struct {
	x, y, z int
}
type Boundries struct {
	max_x, max_y, max_z int
	min_x, min_y, min_z int
}

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	limits := Boundries{
		max_x: math.MinInt32,
		max_y: math.MinInt32,
		max_z: math.MinInt32,
		min_x: math.MaxInt32,
		min_y: math.MaxInt32,
		min_z: math.MaxInt32,
	}

	cubes := make(map[Cube]bool)
	for _, line := range strings.Split(string(data), "\n") {
		coords := strings.Split(line, ",")
		x := toInt(coords[0])
		y := toInt(coords[1])
		z := toInt(coords[2])
		cubes[Cube{x: x, y: y, z: z}] = true
		if x > limits.max_x {
			limits.max_x = x
		}
		if y > limits.max_y {
			limits.max_y = y
		}
		if z > limits.max_z {
			limits.max_z = z
		}
		if x < limits.min_x {
			limits.min_x = x
		}
		if y < limits.min_y {
			limits.min_y = y
		}
		if z < limits.min_z {
			limits.min_z = z
		}
	}
	ans := 0
	// exposed := make(map[Cube]bool)
	for cube := range cubes {
		ans += countExposed(cube, cubes, limits)
	}

	fmt.Println(ans)
}

func isInside(cube Cube, cubes map[Cube]bool, limits Boundries) bool { // for part1 just remove this
	dirs := []Cube{
		{x: 1, y: 0, z: 0},
		{x: -1, y: 0, z: 0},
		{x: 0, y: 1, z: 0},
		{x: 0, y: -1, z: 0},
		{x: 0, y: 0, z: 1},
		{x: 0, y: 0, z: -1},
	}
	q := []Cube{cube}
	visited := make(map[Cube]bool)
	visited[cube] = true
	for len(q) > 0 {
		cube := q[0]
		q = q[1:]
		for _, dir := range dirs {
			neighbor := Cube{x: cube.x + dir.x, y: cube.y + dir.y, z: cube.z + dir.z}
			if !visited[neighbor] && !cubes[neighbor] {
				visited[neighbor] = true
				if neighbor.x < limits.min_x || neighbor.x > limits.max_x || neighbor.y < limits.min_y || neighbor.y > limits.max_y || neighbor.z < limits.min_z || neighbor.z > limits.max_z {
					return false
				}
				q = append(q, neighbor)
			}
		}
	}
	return true
}

func countExposed(cube Cube, cubes map[Cube]bool, limits Boundries) int {
	exposed := 0

	for _, neighbor := range neighbors(cube) {
		if !cubes[neighbor] && !isInside(neighbor, cubes, limits) {
			exposed += 1
		}
	}
	return exposed
}

func neighbors(cube Cube) []Cube {
	neighbors_dirs := []Cube{
		{x: 1, y: 0, z: 0},
		{x: -1, y: 0, z: 0},
		{x: 0, y: 1, z: 0},
		{x: 0, y: -1, z: 0},
		{x: 0, y: 0, z: 1},
		{x: 0, y: 0, z: -1},
	}
	neighbors := make([]Cube, 0, len(neighbors_dirs))
	for _, dir := range neighbors_dirs {
		neighbors = append(neighbors, Cube{x: cube.x + dir.x, y: cube.y + dir.y, z: cube.z + dir.z})
	}
	return neighbors
}

func toInt(s string) int {
	i, err := strconv.Atoi(s)
	if err != nil {
		panic(err)
	}
	return i
}

// 544 low
// 3128 high
// 1352 low
