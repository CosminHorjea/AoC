package main

import (
	"fmt"
	"math"
	"os"
	"sort"
	"strconv"
	"strings"
)

type Point struct {
	x int
	y int
	z int
}

func Find(uf map[int]int, a int) int {
	if uf[a] != a {
		uf[a] = Find(uf, uf[a])
	}
	return uf[a]
}

func Union(uf map[int]int, a int, b int) {
	rootA := Find(uf, a)
	rootB := Find(uf, b)
	if rootA != rootB {
		uf[rootB] = rootA
	}
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
	uf := make(map[int]int, len(points))
	for i, line := range lines {
		var x, y, z int
		_, err := fmt.Sscanf(line, "%d,%d,%d", &x, &y, &z)
		if err != nil {
			fmt.Println("Error parsing line:", err)
			continue
		}
		uf[i] = i
		points = append(points, Point{x, y, z})
	}
	distances := make([][]int, 0)
	for i := 0; i < len(points)-1; i++ {
		for j := i + 1; j < len(points); j++ {
			if i == j {
				continue
			}
			dx := int(math.Abs(float64(points[i].x - points[j].x)))
			dy := int(math.Abs(float64(points[i].y - points[j].y)))
			dz := int(math.Abs(float64(points[i].z - points[j].z)))
			dist := dx*dx + dy*dy + dz*dz
			distances = append(distances, []int{i, j, dist})
		}
	}

	sort.Slice(distances, func(i, j int) bool {
		return distances[i][2] < distances[j][2]
	})
	steps, _ := strconv.Atoi(os.Args[2])
	connectionsMade := 0
	for _, d := range distances {
		if Find(uf, d[0]) != Find(uf, d[1]) {
			// fmt.Printf("Union with distance of %d between %d and %d\n", d[2], points[d[0]], points[d[1]])
			Union(uf, d[0], d[1])
			fmt.Println("Union of", d[0], " and ", d[1])
			connectionsMade++
			if connectionsMade == len(points)-1 {
				fmt.Println(points[d[0]], " and ", points[d[1]], " connected all points!")
				fmt.Println("Part 2", points[d[0]].x*points[d[1]].x)
			}
		}
		steps--
		// i thought we only count a pair when we add a pair, but it seems like not
		if steps == 1 {
			part1Ans := getLargestThreeGroupsProduct(points, uf)
			fmt.Println("Part 1", part1Ans)
		}
	}

}

func getLargestThreeGroupsProduct(points []Point, uf map[int]int) int {
	groups := make(map[int][]int)
	for i := 0; i < len(points); i++ {
		root := Find(uf, i)
		groups[root] = append(groups[root], i)
	}
	fmt.Println("Total groups found:", len(groups))
	// groups sizes
	sizes := make([]int, 0)
	for _, group := range groups {
		sizes = append(sizes, len(group))
	}
	sort.Ints(sizes)
	if len(sizes) < 3 {
		fmt.Println("Not enough groups found.")
		return 0
	}
	productOfLastThree := sizes[len(sizes)-1] * sizes[len(sizes)-2] * sizes[len(sizes)-3]
	return productOfLastThree
}
