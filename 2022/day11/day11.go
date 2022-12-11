package main

import (
	"fmt"
	"os"
	"sort"
	"strconv"
	"strings"
)

type Monkey struct {
	items        []int
	operation    string
	divider      int
	friends      [2]int
	num_inspects int
}

func parseMonkey(monkeyLine string) Monkey {
	monkey := Monkey{}
	props := strings.Split(monkeyLine, "\n")

	items_string := strings.Split(props[1], ": ")[1]
	for _, item := range strings.Split(items_string, ", ") {
		value, _ := strconv.Atoi(item)
		monkey.items = append(monkey.items, value)
	}

	monkey.operation = strings.Split(props[2], "new = old ")[1]
	monkey.divider, _ = strconv.Atoi(strings.Trim(strings.Split(props[3], " divisible by ")[1], "\n"))
	monkey.friends[0], _ = strconv.Atoi(strings.Split(props[4], "monkey ")[1])
	monkey.friends[1], _ = strconv.Atoi(strings.Split(props[5], "monkey ")[1])

	return monkey
}

// method for monkey
func (m *Monkey) inspect_item() int {
	takeItem := m.items[0]
	// fmt.Println("Inspecting item from the front ", takeItem)
	m.items = m.items[1:]
	if m.operation[0] == '+' {
		value, _ := strconv.Atoi(strings.Trim(m.operation[1:], " "))
		takeItem += value
	} else {
		if m.operation[1:] == " old" {
			takeItem *= takeItem
		} else {
			value, _ := strconv.Atoi(strings.Trim(m.operation[1:], " "))
			takeItem *= value
		}
	}
	m.num_inspects++
	return takeItem
}
func (m *Monkey) inspect_item_mod(modulo int) int {
	takeItem := m.items[0]
	// fmt.Println("Inspecting item from the front ", takeItem)
	m.items = m.items[1:]
	if m.operation[0] == '+' {
		value, _ := strconv.Atoi(strings.Trim(m.operation[1:], " "))
		takeItem += value
	} else {
		if m.operation[1:] == " old" {
			takeItem *= takeItem
		} else {
			value, _ := strconv.Atoi(strings.Trim(m.operation[1:], " "))
			takeItem *= (value % modulo)
		}
	}
	m.num_inspects++
	return takeItem % modulo
}

// func main() {
// 	data, err := os.ReadFile("input.txt")
// 	if err != nil {
// 		panic(err)
// 	}
// 	myMonkeys := []Monkey{}
// 	monkeyLines := strings.Split(string(data), "\n\n")
// 	for _, monkeyLine := range monkeyLines {
// 		myMonkeys = append(myMonkeys, parseMonkey(monkeyLine))
// 	}
// 	fmt.Println(myMonkeys)
// 	// num_inspects = make([]int, len(myMonkeys))
// 	num_rounds := 20
// 	for i := 0; i < num_rounds; i++ {
// 		for j := 0; j < len(myMonkeys); j++ {
// 			for len(myMonkeys[j].items) > 0 {
// 				got := myMonkeys[j].inspect_item()
// 				// fmt.Printf("Monkey %d got %d\n", j, got)
// 				got = int(got / 3)
// 				// fmt.Println("Calmed down to ", got)
// 				if got%myMonkeys[j].divider == 0 {
// 					myMonkeys[myMonkeys[j].friends[0]].items = append(myMonkeys[myMonkeys[j].friends[0]].items, got)
// 				} else {
// 					myMonkeys[myMonkeys[j].friends[1]].items = append(myMonkeys[myMonkeys[j].friends[1]].items, got)
// 				}
// 			}
// 		}
// 		// print all of monkeys items
// 		// for j := 0; j < len(myMonkeys); j++ {
// 		// 	fmt.Printf("Monkey %d: %v (%d)\n", j, myMonkeys[j].items, myMonkeys[j].num_inspects)
// 		// }
// 	}

// 	for j := 0; j < len(myMonkeys); j++ {
// 		fmt.Printf("Monkey %d: %v (%d)\n", j, myMonkeys[j].items, myMonkeys[j].num_inspects)
// 	} // used the calculator tu just multiply the greatest 2
// }

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}
	myMonkeys := []Monkey{}
	monkeyLines := strings.Split(string(data), "\n\n")
	for _, monkeyLine := range monkeyLines {
		myMonkeys = append(myMonkeys, parseMonkey(monkeyLine))
	}
	lowest_common_multiple := 1
	for _, monkey := range myMonkeys {
		lowest_common_multiple *= monkey.divider
	}
	fmt.Println(myMonkeys)
	num_rounds := 10000
	for i := 0; i < num_rounds; i++ {
		for j := 0; j < len(myMonkeys); j++ {
			for len(myMonkeys[j].items) > 0 {
				got := myMonkeys[j].inspect_item()
				got = modLikePython(got, lowest_common_multiple)
				if got%myMonkeys[j].divider == 0 {
					myMonkeys[myMonkeys[j].friends[0]].items = append(myMonkeys[myMonkeys[j].friends[0]].items, got)
				} else {
					myMonkeys[myMonkeys[j].friends[1]].items = append(myMonkeys[myMonkeys[j].friends[1]].items, got)
				}
			}
		}
	}
	print_biggest_two(myMonkeys)
}

func print_biggest_two(myMonkeys []Monkey) {
	sort.Slice(myMonkeys, func(i, j int) bool {
		return myMonkeys[i].num_inspects > myMonkeys[j].num_inspects
	})
	max1 := myMonkeys[0].num_inspects
	max2 := myMonkeys[1].num_inspects

	fmt.Println(max1, max2)
	fmt.Println(max1 * max2)
}

func modLikePython(d, m int) int { //https://stackoverflow.com/questions/43018206/modulo-of-negative-integers-in-go
	var res int = d % m
	if (res < 0 && m > 0) || (res > 0 && m < 0) {
		return res + m
	}
	return res
}

// 20709554856 wrong
// 18170818354 correct
//
// 2713310158
// 2637590098
