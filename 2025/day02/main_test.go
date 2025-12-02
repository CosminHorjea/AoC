package main_test

import (
	main "aoc2025/day02"
	"testing"
)

func TestIsInvalidIdPart2(t *testing.T) {
	tests := []struct {
		id       int
		expected bool
	}{
		{1188511885, true},
		{1188511880, false},
		{565656, true},
		{123123, true},
		{123456, false},
	}	
	for _, test := range tests {
		result := main.IsInvalidIdPart2(test.id)
		if result != test.expected {
			t.Errorf("IsInvalidIdPart2(%d) = %v; want %v", test.id, result, test.expected)
		}
	}
}