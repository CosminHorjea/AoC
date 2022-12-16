package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

type Point struct {
	x, y int
}

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}

	walls := make(map[Point]bool)
	max_y := 0
	for _, line := range strings.Split(string(data), "\n") {
		parseLine(line, walls)
		for _, p := range strings.Split(line, " -> ") {
			y, _ := strconv.Atoi(strings.Split(p, ",")[1])
			if y > max_y {
				max_y = y
			}
		}
	}

	sand := 0
	// for drop(Point{x: 500, y: 0}, walls, max_y) { //part1
	// 	sand++
	// }
	for drop2(Point{x: 500, y: 0}, walls, max_y+1) {
		sand++
	}
	fmt.Println(sand)
}

//	func drop(p Point, walls map[Point]bool, max_y int) bool {
//		if _, ok := walls[p]; ok {
//			return false
//		}
//		for p.y < max_y {
//			fmt.Println(p)
//			if !walls[Point{p.x, p.y + 1}] {
//				p = Point{p.x, p.y + 1}
//				continue
//			}
//			left := Point{p.x - 1, p.y + 1}
//			if !walls[left] {
//				p = left
//				continue
//			}
//			right := Point{p.x + 1, p.y + 1}
//			if !walls[right] {
//				p = right
//				continue
//			}
//			fmt.Println("Filling", p)
//			walls[p] = true
//			return true
//		}
//		return false
//	}
func drop2(p Point, walls map[Point]bool, max_y int) bool {
	if walls[p] {
		return false
	}
	for p.y < max_y {
		if !walls[Point{p.x, p.y + 1}] {
			p = Point{p.x, p.y + 1}
			continue
		}
		left := Point{p.x - 1, p.y + 1}
		if !walls[left] {
			p = left
			continue
		}
		right := Point{p.x + 1, p.y + 1}
		if !walls[right] {
			p = right
			continue
		}
		walls[p] = true
		return true
	}
	walls[p] = true
	return true
}

func parseLine(line string, walls map[Point]bool) {
	coords := strings.Split(line, " -> ")
	points := make([]Point, len(coords))
	for i := 0; i < len(coords); i++ {
		x, _ := strconv.Atoi(strings.Split(coords[i], ",")[0])
		y, _ := strconv.Atoi(strings.Split(coords[i], ",")[1])
		points[i] = Point{x: x, y: y}
	}
	for i := 1; i < len(points); i++ {
		draw(points[i-1], points[i], walls)
	}
}

func draw(p1, p2 Point, walls map[Point]bool) {
	if p1.x == p2.x {
		min_y := p1.y
		max_y := p2.y
		if p2.y < min_y {
			min_y = p2.y
			max_y = p1.y
		}
		for y := min_y; y <= max_y; y++ {
			walls[Point{x: p1.x, y: y}] = true
		}
	} else {
		min_x := p1.x
		max_x := p2.x
		if p2.x < min_x {
			min_x = p2.x
			max_x = p1.x
		}
		for x := min_x; x <= max_x; x++ {
			walls[Point{x: x, y: p1.y}] = true
		}
	}
}

// 24794 high
// 24478 high
