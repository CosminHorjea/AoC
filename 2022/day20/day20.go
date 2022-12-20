package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

type Node struct {
	val  int
	next *Node
	prev *Node
}

// func main() {
// 	data, _ := os.ReadFile("input.txt")
// 	// data, _ := os.ReadFile("input_test.txt")
// 	nums := make([]int, 0)
// 	for _, line := range strings.Split(string(data), "\n") {
// 		num, _ := strconv.Atoi(line)
// 		nums = append(nums, num)
// 	}
// 	size := len(nums)
// 	list, lookup := makeList(nums)
// 	for i := 0; i < size; i++ {
// 		curr := lookup[i]
// 		next_pos := modLikePython(curr.val, size-1)
// 		prev := curr.prev
// 		if next_pos == 0 {
// 			continue
// 		}
// 		unlink(curr)
// 		insertNodeAtPos(prev, curr, next_pos)
// 		// fmt.Println("Moving ", curr.val, "with ", next_pos)
// 		// printList(list)
// 	}
// 	for list.val != 0 {
// 		list = list.next
// 	}

//		a := getNthNodeValue(list, 1000)
//		b := getNthNodeValue(list, 2000)
//		c := getNthNodeValue(list, 3000)
//		fmt.Println(a, b, c)
//		fmt.Println(a + b + c)
//	}
func main() {
	key := 811589153
	data, _ := os.ReadFile("input.txt")
	// data, _ := os.ReadFile("input_test.txt")
	nums := make([]int, 0)
	for _, line := range strings.Split(string(data), "\n") {
		num, _ := strconv.Atoi(line)
		nums = append(nums, num*key)
	}
	size := len(nums)
	list, lookup := makeList(nums)
	for round := 0; round < 10; round++ {
		for i := 0; i < size; i++ {
			curr := lookup[i]
			next_pos := modLikePython(curr.val, size-1)
			prev := curr.prev
			if next_pos == 0 {
				continue
			}
			unlink(curr)
			insertNodeAtPos(prev, curr, next_pos)
			// fmt.Println("Moving ", curr.val, "with ", next_pos)
			// printList(list)
		}
	}
	for list.val != 0 {
		list = list.next
	}

	a := getNthNodeValue(list, 1000)
	b := getNthNodeValue(list, 2000)
	c := getNthNodeValue(list, 3000)
	fmt.Println(a, b, c)
	fmt.Println(a + b + c)
}

// -3329 not good
// -17187 not good
// -12711 not good
// 3466 ??
func getNthNodeValue(node *Node, n int) int {
	curr := node
	for i := 0; i < n; i++ {
		curr = curr.next
	}
	return curr.val
}

func printList(node *Node) {
	curr := node
	for {
		fmt.Printf("%d ", curr.val)
		curr = curr.next
		if curr == node {
			break
		}
	}
	fmt.Println()
}

func insertNodeAtPos(list, node *Node, pos int) {
	curr := list
	for i := 0; i < pos; i++ {
		curr = curr.next
	}
	node.next = curr.next
	node.prev = curr
	curr.next.prev = node
	curr.next = node
}

func unlink(node *Node) {
	node.prev.next = node.next
	node.next.prev = node.prev
}

func makeList(nums []int) (*Node, []*Node) {
	var head *Node
	var prev *Node
	lookup := make([]*Node, len(nums))
	for id, num := range nums {
		node := &Node{val: num}
		lookup[id] = node
		if head == nil {
			head = node
		}
		if prev != nil {
			prev.next = node
			node.prev = prev
		}
		prev = node
	}
	head.prev = prev
	prev.next = head

	return head, lookup
}

func modLikePython(d, m int) int { //https://stackoverflow.com/questions/43018206/modulo-of-negative-integers-in-go
	var res int = d % m
	if (res < 0 && m > 0) || (res > 0 && m < 0) {
		return res + m
	}
	return res
}
