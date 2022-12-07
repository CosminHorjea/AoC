package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

var folders = map[string]int{}

func increaseSize(path []string, size int) {
	// for _, folder := range path {
	// 	folders[folder] += size
	// }
	for i := 0; i < len(path); i++ {
		// i dont understand why this is the correct way but watevs
		folders[strings.Join(path[:i+1], "/")] += size
	}

}

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	cwd := []string{}
	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		// fmt.Println(line)
		details := strings.Split(line, " ")
		if details[0] == "$" {
			if details[1] == "cd" {
				if details[2] == ".." {
					cwd = cwd[:len(cwd)-1]
				} else {
					cwd = append(cwd, details[2])
				}
			}
		} else if details[0] != "dir" {
			memory, err := strconv.Atoi(details[0])
			if err != nil {
				panic(err)
			}
			increaseSize(cwd, memory)
		}
	}

	total := 0
	for _, size := range folders {
		if size <= 100000 {
			total += size
		}
	}
	fmt.Println(folders)
	fmt.Printf("Part 1: %d\n", total)

	minimal_folder := 70000000
	total_fs := 70000000
	required := 30000000
	unused_fs := total_fs - folders["/"]
	for _, size := range folders {
		if size > (required-unused_fs) && size < minimal_folder {
			minimal_folder = size
		}
	}
	fmt.Printf("Part 2: %d\n", minimal_folder)
}
