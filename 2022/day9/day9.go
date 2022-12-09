package main

import (
	"fmt"
	"math"
	"os"
	"strconv"
	"strings"
)

// declare a point struct
type Point struct {
	x, y int
}

func distanceBetweenPoints(p1, p2 Point) int {
	x_dist := math.Abs(float64(p1.x - p2.x))
	y_dist := math.Abs(float64(p1.y - p2.y))

	return int(math.Max(x_dist, y_dist))
}

func moveTail(tail, head Point) Point {
	if distanceBetweenPoints(tail, head) <= 1 {
		return tail
	}
	if head.x == tail.x {
		direction := head.y - tail.y
		if direction > 0 {
			tail.y += 1
		} else {
			tail.y -= 1
		}
		return tail
	}

	if head.y == tail.y {
		direction := head.x - tail.x
		if direction > 0 {
			tail.x += 1
		} else {
			tail.x -= 1
		}
		return tail
	}
	diagonals := []Point{
		{1, 1},
		{1, -1},
		{-1, 1},
		{-1, -1},
	}
	for _, diagonal := range diagonals {
		temp_tail := Point{tail.x + diagonal.x, tail.y + diagonal.y}
		if distanceBetweenPoints(temp_tail, head) <= 1 {
			return temp_tail
		}
	}
	return tail
}

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	directions := map[string]Point{
		"U": {0, 1},
		"D": {0, -1},
		"L": {-1, 0},
		"R": {1, 0},
	}
	commands := strings.Split(string(data), "\n")
	head := Point{0, 0}
	// tail := Point{0, 0} part 1
	knots := make([]Point, 9)

	seenSqares := make(map[Point]bool)
	for _, command := range commands {
		direction := strings.Split(command, " ")[0]
		steps, _ := strconv.Atoi(strings.Split(command, " ")[1])
		// fmt.Printf("direction: %s, steps: %d\n", direction, steps)
		for i := 0; i < steps; i++ {
			head.x += directions[direction].x
			head.y += directions[direction].y
			// fmt.Printf("head: %v, tail: %v\n", head, tail)
			knots[0] = moveTail(knots[0], head) // remove the array for part 1 and do this line with tail
			for i := 1; i < len(knots); i++ {
				knots[i] = moveTail(knots[i], knots[i-1])
			}
			// fmt.Printf("tail moves to %v\n", tail)
			seenSqares[knots[8]] = true
		}
	}
	// print seen sqares
	fmt.Println(len(seenSqares))

}
