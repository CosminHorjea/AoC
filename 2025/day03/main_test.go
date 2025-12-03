package main_test

import (
	main "aoc2025/day03"
	"testing"
)

func TestGetJoltage(t *testing.T) {
	tests := []struct {
		id       string
		expected int
	}{
		{"987654321111111", 98},
		{"811111111111119", 89},
		{"234234234234278", 78},
		{"818181911112111", 92},
	}	
	for _, test := range tests {
		result := main.GetJoltage(test.id)
		if result != test.expected {
			t.Errorf("GetJoltage(%s) = %v; want %v", test.id, result, test.expected)
		}
	}
}


func TestGetJoltagePart2(t *testing.T) {
	tests := []struct {
		id       string
		expected int
	}{
		{"987654321111111", 987654321111},
		{"811111111111119", 811111111119},
		{"234234234234278", 434234234278},
		{"818181911112111", 888911112111},
	}	
	maxBatteries := 12
	for _, test := range tests {
		result := main.GetJoltagePart2(test.id, maxBatteries)
		if result != test.expected {
			t.Errorf("GetJoltagePart2(%s) = %v; want %v", test.id, result, test.expected)
		}
	}
}