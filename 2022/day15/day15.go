package main

import (
	"fmt"
	"math"
	"os"
	"regexp"
	"strconv"
	"strings"
)

type Point struct {
	x, y int
}

func manDist(a, b Point) int {
	return abs(a.x-b.x) + abs(a.y-b.y)
}

func abs(x int) int {
	if x < 0 {
		return -x
	}
	return x
}

type Sensor struct {
	pos, posBeacon Point
	distance       int
}

func main() {
	// data, err := os.ReadFile("input_test.txt")
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	sensors := make([]Sensor, 0)
	becons := make(map[Point]bool)
	left_x := math.MaxInt
	right_x := math.MinInt
	max_dist := 0
	for _, line := range strings.Split(string(data), "\n") {
		nums_str := regexp.MustCompile(`\d+`).FindAllString(line, -1)
		nums := make([]int, len(nums_str))
		for i, num := range nums_str {
			nums[i], _ = strconv.Atoi(num)
		}
		x1, y1, x2, y2 := nums[0], nums[1], nums[2], nums[3]
		becons[Point{x2, y2}] = true
		if x1 < left_x {
			left_x = x1
		}
		if x1 > right_x {
			right_x = x1
		}
		dist := manDist(Point{x1, y1}, Point{x2, y2})
		if dist > max_dist {
			max_dist = dist
		}
		sensors = append(sensors, Sensor{Point{x1, y1}, Point{x2, y2}, dist})
	}

	// y := 10 //part1
	// y := 2000000
	// ans := 0
	// for x := left_x - max_dist; x <= right_x+max_dist; x++ {
	// 	flag := false
	// 	for _, sensor := range sensors {
	// 		if manDist(Point{x, y}, sensor.pos) <= sensor.distance && !becons[Point{x, y}] {
	// 			flag = true
	// 			break
	// 		}
	// 	}
	// 	if flag {
	// 		ans++
	// 	}
	// }
	// fmt.Println("|", ans, "|")
	max_coords := 4000000
	for x := 0; x < max_coords; x++ {
		for y := 0; y < 4000000; y++ {
			flag := true
			for _, sensor := range sensors {
				if manDist(Point{x, y}, sensor.pos) <= sensor.distance {
					rem_dist := sensor.distance - abs(x-sensor.pos.x)
					if sensor.pos.y+rem_dist-1 > y { //wizardry
						y = sensor.pos.y + rem_dist - 1
					}
					flag = false
					break
				}
			}
			if flag {
				fmt.Println(x*max_coords + y)
			}
		}
	}
}

// another cool way of doing this is to check the margins, look here https://github.com/noah-clements/AoC2022/blob/master/day15/day15.py

// props to this https://github.com/S7012MY/Advent-of-Code-2022/blob/main/Day%2015/day15_2.pl
// 4716228 too high
// 7730280
// 4408546 low
