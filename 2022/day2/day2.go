package main

import (
	"flag"
	"fmt"
	"os"
	"strings"
)

var scores_pick = map[string]int{
	"A": 1,
	"B": 2,
	"C": 3,
}

func checkHand(hand byte) {
	if hand < 'A' || hand > 'C' {
		panic("Invalid hand")
	}
}

func checkHandBoundries(hand, lower, higher byte) {
	if hand < lower || hand > higher {
		panic("Invalid hand")
	}
}
func decrypt_hand(hand byte) byte {
	checkHandBoundries(hand, 'X', 'Z')
	return hand - 'X' + 'A'
}

func beat_hand(hand byte) byte {
	checkHand(hand)
	win_hand := hand + 1
	if win_hand > 'C' {
		win_hand = 'A'
	}
	return win_hand
}

func lose_hand(hand byte) byte {
	checkHand(hand)
	lose_hand := hand - 1
	if lose_hand < 'A' {
		lose_hand = 'C'
	}
	return lose_hand
}

func outcome_score(player1, player2 byte) int {
	checkHand(player1)
	checkHand(player2)
	if player1 == player2 {
		return 3
	}
	if player2 == player1+1 || player2 == player1-2 {
		return 6
	}
	return 0
}

func second_player_pick(player1, instruction byte) byte {
	checkHand(player1)
	option := byte(0)
	if instruction == 'X' {
		option = lose_hand(player1)
	} else if instruction == 'Y' {
		option = player1
	} else if instruction == 'Z' {
		option = beat_hand(player1)
	}
	return option
}

func part2(data string) {
	total := 0

	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		picks := strings.Split(line, " ")
		chosen_hand := second_player_pick(picks[0][0], picks[1][0])
		total += scores_pick[string(chosen_hand)] + outcome_score(picks[0][0], chosen_hand)
	}

	fmt.Printf("Part 2: %d\n", total)
}

func part1(data string) {
	total := 0

	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		picks := strings.Split(line, " ")
		decrpyted_hand := decrypt_hand(picks[1][0])
		total += scores_pick[string(decrpyted_hand)] + outcome_score(picks[0][0], decrpyted_hand)
	}

	fmt.Printf("Part 1: %d\n", total)
}

func main() {
	filename := "input.txt"
	var testFlag = flag.Bool("test", false, "Run test")
	flag.Parse()
	if *testFlag {
		filename = "input_test.txt"
	}
	data, err := os.ReadFile(filename)
	if err != nil {
		panic(err)
	}
	part1(string(data))
	part2(string(data))
}
