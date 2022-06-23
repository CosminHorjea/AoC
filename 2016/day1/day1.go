package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"math"
	"strconv"
	"strings"
)

type Point struct {
	x int
	y int
}

func main() {
	directions := [][]int{{0, 1}, {1, 0}, {0, -1}, {-1, 0}}
	current_direction := 0
	position := Point{0, 0}
	visited := make(map[Point]bool)
	body, err := ioutil.ReadFile("day1_full.txt")
	if err != nil {
		log.Fatalf("Unable to read file")
	}
	visited[position] = true
	data := strings.Split(string(body), ", ")
	for _, command := range data {
		fmt.Println(command)
		first_char := string(command[0])
		ammount, _ := strconv.ParseInt(command[1:], 10, 32)
		if first_char == "R" {
			current_direction += 1
			current_direction %= 4
		} else {
			current_direction -= 1
			if current_direction == -1 {
				current_direction = 3
			}
		}
		for i := 0; i < int(ammount); i++ {
			position.x += directions[current_direction][0]
			position.y += directions[current_direction][1]
			if val, ok := visited[position]; ok {
				fmt.Printf("%+v\n %t \n", position, val)
				fmt.Println(math.Abs(float64(position.x)) + math.Abs(float64(position.y)))
				return
			}
			visited[position] = true
		}

	}
	fmt.Println(math.Abs(float64(position.x)) + math.Abs(float64(position.y)))

}
