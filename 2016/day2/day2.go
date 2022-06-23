package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"strings"
)

type Point struct {
	x int
	y int
}

func main() {
	// code := ""
	body, err := ioutil.ReadFile("day2_full.txt")
	if err != nil {
		log.Fatalf("Unable to open file")
	}

	disallowed := []Point{{0, 0}, {0, 1}, {0, 3}, {0, 4}, {1, 0}, {1, 4}, {3, 0}, {3, 4}, {4, 0}, {4, 1}, {4, 3}, {4, 4}}
	m := make(map[Point]string)

	m[Point{2, 0}] = "1"
	m[Point{1, 1}] = "2"
	m[Point{2, 1}] = "3"
	m[Point{3, 1}] = "4"
	m[Point{0, 2}] = "5"
	m[Point{1, 2}] = "6"
	m[Point{2, 2}] = "7"
	m[Point{3, 2}] = "8"
	m[Point{4, 2}] = "9"
	m[Point{1, 3}] = "A"
	m[Point{2, 3}] = "B"
	m[Point{3, 3}] = "C"
	m[Point{2, 4}] = "D"

	position := Point{0, 2}
	data := strings.Split(string(body), "\n")
	for _, line := range data {
		for _, crr := range string(line) {

			next_position := position

			switch string(crr) {
			case "D":
				next_position.y += 1
			case "U":
				next_position.y -= 1
			case "R":
				next_position.x += 1
			case "L":
				next_position.x -= 1
			}
			ok := 1
			for _, b := range disallowed {
				if b == next_position {
					ok = 0
					break
				}
			}

			if ok == 0 {
				continue
			}

			position = next_position

			if position.x < 0 {
				position.x = 0
			}

			if position.x > 4 {
				position.x = 4
			}

			if position.y < 0 {
				position.y = 0
			}

			if position.y > 4 {
				position.y = 4
			}
			// fmt.Println(position)
		}
		// fmt.Println(position)
		fmt.Printf("%s", string(m[position]))
	}
}
