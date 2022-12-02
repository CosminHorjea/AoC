package main

import (
	"fmt"
	"os"
	"strings"
)

var scores_pick = map[string]int{
	"X": 1,
	"Y": 2,
	"Z": 3,
}

func outcome_score(player1, player2 byte) int {
	player2 = player2 - 'X' + 'A'
	// fmt.Printf("%c,%c\n", player1, player2)
	if player1 == player2 {
		return 3
	}
	if player2 == player1+1 || player2 == player1-2 {
		return 6
	}
	return 0
}

func second_player_pick(player1, player2 byte) byte {
	if player2 == 'X' {
		//lose
		lose_hand := player1 - 1
		if lose_hand < 'A' {
			lose_hand = 'C'
		}
		return lose_hand + 'X' - 'A'
	} else if player2 == 'Y' {
		//draw
		return player1 + 'X' - 'A'
	} else if player2 == 'Z' {
		//win
		win_hand := player1 + 1
		if win_hand > 'C' {
			win_hand = 'A'
		}
		return win_hand + 'X' - 'A'
	}
	return 0
}

func main() {
	data, err := os.ReadFile("input.txt")
	if err != nil {
		panic(err)
	}

	total := 0

	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		picks := strings.Split(line, " ")
		// fmt.Printf("Player 1 picks: %s, Player 2 picks: %s\n", picks[0], picks[1])
		// part 2
		chosen_hand := second_player_pick(picks[0][0], picks[1][0])
		// fmt.Printf("Player 1 picks: %s, Player 2 picks: %s, Player 2 chooses: %c\n", picks[0], picks[1], chosen_hand)
		total += scores_pick[string(chosen_hand)] + outcome_score(picks[0][0], chosen_hand)

	}

	fmt.Printf("Part 1: %d\n", total)
}

// func main() {
// 	data, err := os.ReadFile("input.txt")
// 	if err != nil {
// 		panic(err)
// 	}

// 	total := 0

// 	lines := strings.Split(string(data), "\n")
// 	for _, line := range lines {
// 		picks := strings.Split(line, " ")
// 		// fmt.Printf("Player 1 picks: %s, Player 2 picks: %s\n", picks[0], picks[1])
// 		total += scores_pick[picks[1]] + outcome_score(picks[0][0], picks[1][0])
// 	}

// 	fmt.Printf("Part 1: %d\n", total)
// }

// 8069 too low
