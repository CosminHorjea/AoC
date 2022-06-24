package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"sort"
	"strconv"
	"strings"
)

func part1(data []byte) {
	ans := 0
	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		nums := strings.Fields(line)

		var nums_int []int

		for _, x := range nums {
			whole, _ := strconv.Atoi(x)
			nums_int = append(nums_int, whole)
		}

		sort.Ints(nums_int)

		if len(nums_int) != 3 {
			log.Fatalln("Ce dracu?")
		}
		if nums_int[0]+nums_int[1] > nums_int[2] {
			ans += 1
		}
	}
	fmt.Println("Part 1:", ans)
}

func parseArray(nums []int) int {
	ans := 0
	for i := 0; i < len(nums); i += 3 {
		var nums_int []int
		nums_int = append(nums_int, nums[i])
		nums_int = append(nums_int, nums[i+1])
		nums_int = append(nums_int, nums[i+2])
		sort.Ints(nums_int)
		if nums_int[0]+nums_int[1] > nums_int[2] {
			ans += 1
		}
	}
	return ans
}

func part2(data []byte) {
	ans := 0
	lines := strings.Split(string(data), "\n")
	var nums1 []int
	var nums2 []int
	var nums3 []int

	for _, line := range lines {
		nums := strings.Fields(line)

		var nums_int []int

		for _, x := range nums {
			whole, _ := strconv.Atoi(x)
			nums_int = append(nums_int, whole)
		}
		nums1 = append(nums1, nums_int[0])
		nums2 = append(nums2, nums_int[1])
		nums3 = append(nums3, nums_int[2])
	}

	ans += parseArray(nums1) + parseArray(nums2) + parseArray(nums3)
	fmt.Println("Part 2:", ans)
}

func main() {
	data, err := ioutil.ReadFile("day3_full.txt")
	if err != nil {
		log.Fatalln("Couldn't read file")
	}
	part1(data)
	part2(data)
}

// low 	   	1006
// correct 	1050
// correct  1921
