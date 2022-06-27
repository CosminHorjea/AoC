package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"os"
	"sort"
	"strconv"
	"strings"
)

func check(e error) {
	if e != nil {
		panic(e)
	}
}

type Room struct {
	id       int
	label    string
	checksum string
}

const input_file = "day4_full.txt"

func rotateLabel(label string, id int) {

	decrypted := ""

	folder, err := os.OpenFile("objects.txt", os.O_APPEND|os.O_WRONLY|os.O_CREATE, 0600)
	check(err)
	defer folder.Close()

	for _, c := range label {
		if c == '-' {
			decrypted += " "
			continue
		}
		decrypted += string((int(c)-97+id)%26 + 97)
		// this is kind of hacky, but just ctrl-f your term
	}
	decrypted = decrypted + " - " + strconv.Itoa(id) + "\n"
	_, err2 := folder.WriteString(decrypted)
	check(err2)

}

func checkRoom(room Room) bool {
	m := make(map[rune]int)
	max := 0
	for _, c := range room.label {
		if c == '-' {
			continue
		}
		id, err := m[c]
		if !err {
			m[c] = 1
		} else {
			m[c] = id + 1
			if max < id {
				max = id
			}
		}
	}
	frequencies := []int{}

	for _, value := range m {
		frequencies = append(frequencies, value)
	}

	sort.Slice(frequencies, func(i, j int) bool {
		return frequencies[i] > frequencies[j]
	})

	for i := 0; i < len(room.checksum)-1; i++ {
		if frequencies[i] == frequencies[i+1] {
			if room.checksum[i] > room.checksum[i+1] {
				return false
			}
		}
		if frequencies[i] != m[rune(room.checksum[i])] {
			return false

		}

	}
	rotateLabel(room.label, room.id)
	return true
}

func main() {

	body, err := ioutil.ReadFile(input_file)
	if err != nil {
		log.Fatal("No file")
	}
	ans := 0
	// rotateLabel("qzmt-zixmtkozy-ivhz", 343)
	// return
	lines := strings.Split(string(body), "\n")
	for _, line := range lines {
		part := strings.Split(line, "[")
		id, _ := strconv.Atoi(part[0][len(part[0])-3:])
		checksum := part[1][:len(part[1])-2]
		label := part[0][:len(part[0])-4]
		if (checkRoom(Room{id, label, checksum})) {
			ans += id
			// fmt.Println(id)
		}
	}
	fmt.Println(ans)

}

// 421062 - too high
// 410301 - too high
