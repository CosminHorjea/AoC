package main

import (
	"fmt"
	"os"
	"strings"
)

type Point struct {
	x, y int
}

func main() {
	inputFile := os.Args[1]
	input, err := os.ReadFile(inputFile)
	if err != nil {
		fmt.Println("Error reading file:", err)
		return
	}
	startingPoint := Point{0, 0}
	breams := make([]Point, 0)
	lines := strings.Split(string(input), "\n")
	H := len(lines)
	W := len(lines[0])
	for i := 0; i < H; i++ {
		for j := 0; j < W; j++ {
			switch lines[i][j] {
			case 'S':
				startingPoint = Point{j, i}
			case '^':
				breams = append(breams, Point{j, i})
			}
		}
	}

	Q := make([]Point, 0)
	visited := make(map[Point]bool)
	Q = append(Q, startingPoint)
	visited[startingPoint] = true
	directions := []Point{{0, +1}}
	ans := 0
	for len(Q) > 0 {
		p := Q[0]
		Q = Q[1:]
		for _, d := range directions {
			np := Point{p.x + d.x, p.y + d.y}
			if np.x < 0 || np.x >= W || np.y < 0 || np.y >= H {
				continue
			}
			if visited[np] {
				continue
			}
			cell := lines[np.y][np.x]
			switch cell {
			case '^':
				ans++
				Q = append(Q, Point{np.x + 1, np.y})
				Q = append(Q, Point{np.x - 1, np.y})
			case '.':
				Q = append(Q, np)
			}
			visited[np] = true
		}
	}
	fmt.Println(ans)
	dp := make(map[Point]int64)
	fmt.Println(Part2(startingPoint, lines, W, H, dp))
}

func Part2(startingPoint Point, lines []string, W, H int, dp map[Point]int64) int64 {
	if val, ok := dp[startingPoint]; ok {
		return val
	}
	if startingPoint.y == H-1 {
		return 1
	}
	if lines[startingPoint.y+1][startingPoint.x] == '^' {
		dp[startingPoint] = Part2(Point{startingPoint.x + 1, startingPoint.y}, lines, W, H, dp) +
			Part2(Point{startingPoint.x - 1, startingPoint.y}, lines, W, H, dp)
		return dp[startingPoint]
	} else {
		dp[startingPoint] = Part2(Point{startingPoint.x, startingPoint.y + 1}, lines, W, H, dp)
		return dp[startingPoint]
	}

}
