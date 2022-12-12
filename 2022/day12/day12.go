package main

import (
	"fmt"
	"os"
	"strings"
)

func isReachable(height1, height2 byte) bool {
	if height1 == 'S' {
		height1 = 'a'
	}
	if height2 == 'E' {
		height2 = 'z'
	}
	return height2 <= height1+1
}

type Point struct {
	x, y int
}

func get_distance(start, end Point, mountain [][]byte) int {
	queue := []Point{start}
	visited := make(map[Point]bool)
	visited[start] = true
	distance := 0
	for len(queue) > 0 {
		new_queue := make([]Point, 0)
		for _, point := range queue {
			if point == end {
				return distance
			}
			for _, direction := range []Point{{0, 1}, {0, -1}, {1, 0}, {-1, 0}} {
				new_point := Point{point.x + direction.x, point.y + direction.y}
				if new_point.x < 0 || new_point.y < 0 || new_point.x >= len(mountain[0]) || new_point.y >= len(mountain) {
					continue
				}
				if visited[new_point] {
					continue
				}
				if isReachable(mountain[point.y][point.x], mountain[new_point.y][new_point.x]) {
					new_queue = append(new_queue, new_point)
					visited[new_point] = true
				}
			}
		}
		distance += 1
		queue = new_queue
	}
	return -1

}

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	mountain := make([][]byte, 0)
	for _, line := range strings.Split(string(data), "\n") {
		mountain = append(mountain, []byte(line))
	}
	start_position := Point{0, 0}
	end_position := Point{0, 0}
	min_steps := len(mountain) * len(mountain[0])
	for y, row := range mountain {
		for x, height := range row {
			if height == 'S' {
				start_position = Point{x, y}
			}
			if height == 'E' {
				end_position = Point{x, y}
			}
		}
	}
	for y, row := range mountain {
		for x, height := range row {
			if height == 'a' || height == 'S' {
				distance_from_here := get_distance(Point{x, y}, end_position, mountain)
				fmt.Printf("distance from %d,%d to %d,%d is %d\n", x, y, end_position.x, end_position.y, distance_from_here)
				if distance_from_here < min_steps && distance_from_here != -1 {
					min_steps = distance_from_here
				}
			}
		}
	}

	distance := get_distance(start_position, end_position, mountain)
	fmt.Println("part1:", distance)
	fmt.Println("part2:", min_steps)

}
