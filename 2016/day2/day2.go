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
	position := Point{1, 1}
	data := strings.Split(string(body), "\n")
	for _, line := range data {
		for _, crr := range string(line) {
			switch string(crr) {
			case "D":
				position.y += 1
			case "U":
				position.y -= 1
			case "R":
				position.x += 1
			case "L":
				position.x -= 1
			}
			if position.x < 0 {
				position.x = 0
			}
			if position.x > 2 {
				position.x = 2
			}
			if position.y < 0 {
				position.y = 0
			}
			if position.y > 2 {
				position.y = 2
			}
		}
		fmt.Printf("%d", 3*position.y+position.x+1)
	}
}
