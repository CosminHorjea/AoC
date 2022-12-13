package main

import (
	"fmt"
	"os"
	"strconv"
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
	for _, char := range line {
		if char == '[' {
			pack.mode = "list"
			new_pack := new(Packet)
			new_pack.parent = pack
			pack.subPackets = append(pack.subPackets, new_pack)
			pack = new_pack
		} else if char == ']' {
			pack = pack.parent
		} else if char == ',' {
			continue
		} else {
			num, _ := strconv.Atoi(string(char))
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

func main() {
	data, err := os.ReadFile("input_test.txt")
	if err != nil {
		panic(err)
	}
	// data_rows := strings.Split(string(data), "\n\n")
	_ = data
	res := parseLine("[1,2,3]")
	fmt.Println(len(res.subPackets))
	fmt.Println(res.subPackets[0].subPackets[2].value)
}
