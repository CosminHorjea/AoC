package main

import (
	"fmt"
	"math"
	"os"
	"strings"
)

type Point struct {
	x int
	y int
}

func main() {
	inputFile := os.Args[1]
	input, err := os.ReadFile(inputFile)
	if err != nil {
		fmt.Println("Ersror reading file:", err)
		return
	}
	lines := strings.Split(string(input), "\n")
	points := make([]Point, 0)
	for _, line := range lines {
		var x, y int
		_, err := fmt.Sscanf(line, "%d,%d", &x, &y)
		if err != nil {
			fmt.Println("Error parsing line:", err)
			continue
		}
		points = append(points, Point{x: x, y: y})
	}
	largestRectangleArea := 0
	for i := 0; i < len(points)-1; i++ {
		for j := i + 1; j < len(points); j++ {
			dx := math.Abs(float64(points[j].x-points[i].x)) + 1
			dy := math.Abs(float64(points[j].y-points[i].y)) + 1
			currentArea := int(dx * dy)
			if currentArea > largestRectangleArea {
				largestRectangleArea = currentArea
			}
		}
	}
	fmt.Println("Largest rectangle area:", largestRectangleArea)
}
