package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

type Packet struct {
	mode       string
	subPackets []*Packet
	value      int
	parent     *Packet
}

func parseLine(line string) *Packet {
	pack := new(Packet)
	pack.mode = "list"
	for i := 0; i < len(line); i++ {
		char := line[i]
		if char == '[' {
			new_pack := new(Packet)
			new_pack.mode = "list"
			new_pack.parent = pack
			pack.subPackets = append(pack.subPackets, new_pack)
			pack = new_pack
		} else if char == ']' {
			pack = pack.parent
		} else if char == ',' {
			continue
		} else {
			num, _ := strconv.Atoi(string(char)) //:puke
			if string(char) == "1" && string(line[i+1]) == "0" {
				num = 10
				i++
			}
			// fmt.Println("my num is ", num)
			num_pack := new(Packet)
			num_pack.parent = pack
			num_pack.mode = "num"
			num_pack.value = num
			pack.subPackets = append(pack.subPackets, num_pack)
		}
	}
	return pack
}

func comparePackets(p1, p2 *Packet) int {
	min_len := len(p1.subPackets)
	if len(p2.subPackets) < min_len {
		min_len = len(p2.subPackets)
	}
	for idx := 0; idx < min_len; idx++ {
		if p1.subPackets[idx].mode == "num" && p2.subPackets[idx].mode == "num" {
			// fmt.Printf("got two nums %d %d\n", p1.subPackets[idx].value, p2.subPackets[idx].value)
			if p1.subPackets[idx].value > p2.subPackets[idx].value {
				return 1
			} else if p1.subPackets[idx].value < p2.subPackets[idx].value {
				return -1
			}
		} else if p1.subPackets[idx].mode == "num" {
			// fmt.Printf("got a num and a list %d p1\n", p1.subPackets[idx].value)
			temp := new(Packet)
			temp.mode = "list"
			temp.subPackets = append(temp.subPackets, p1.subPackets[idx])
			return comparePackets(temp, p2.subPackets[idx])
		} else if p2.subPackets[idx].mode == "num" {
			// fmt.Printf("got a num and a list %d p2\n", p2.subPackets[idx].value)
			temp := new(Packet)
			temp.mode = "list"
			temp.subPackets = append(temp.subPackets, p2.subPackets[idx])
			return comparePackets(p1.subPackets[idx], temp)
		} else {
			compare := comparePackets(p1.subPackets[idx], p2.subPackets[idx])
			if compare != 0 {
				return compare
			}
		}
	}
	if len(p1.subPackets) > len(p2.subPackets) {
		return 1
	}
	if len(p1.subPackets) < len(p2.subPackets) {
		return -1
	}
	return 0
}

type IndexedArray struct {
	index int
	value *Packet
}

func main() {
	data, err := os.ReadFile("input.txt")
	// data, err := os.ReadFile("input_test.txt")
	if err != nil {
		panic(err)
	}
	// ans := 0
	data_rows := strings.Split(string(data), "\n\n")
	// part 1
	// for idx, rows := range data_rows {
	// 	first_pack := parseLine(strings.Split(rows, "\n")[0])
	// 	second_pack := parseLine(strings.Split(rows, "\n")[1])
	// 	if comparePackets(first_pack, second_pack) < 0 {
	// 		ans += idx + 1
	// 		fmt.Println(idx + 1)
	// 	}
	// }
	// 	fmt.Println(ans)

	all_packets := make([]*Packet, 0)
	for _, rows := range data_rows {
		first_pack := parseLine(strings.Split(rows, "\n")[0])
		second_pack := parseLine(strings.Split(rows, "\n")[1])
		all_packets = append(all_packets, first_pack)
		all_packets = append(all_packets, second_pack)
	}
	all_packets = append(all_packets, parseLine("[[6]]"))
	all_packets = append(all_packets, parseLine("[[2]]"))

	indexed_array := make([]IndexedArray, 0)
	for idx, pack := range all_packets {
		indexed_array = append(indexed_array, IndexedArray{idx, pack})
	}

	for i := 0; i < len(indexed_array); i++ {
		for j := i + 1; j < len(indexed_array); j++ {
			if comparePackets(indexed_array[i].value, indexed_array[j].value) > 0 {
				aux := indexed_array[i]
				indexed_array[i] = indexed_array[j]
				indexed_array[j] = aux
			}
		}
	}
	vals := make([]int, 0)
	for idx, pack := range indexed_array {
		if pack.index > len(indexed_array)-3 {
			fmt.Println(idx)
			vals = append(vals, idx+1)
		}
	}
	// print the product of vals
	fmt.Println(vals[0] * vals[1])

}

// 15548
// 15810 too low
