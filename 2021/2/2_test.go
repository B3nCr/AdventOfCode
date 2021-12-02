package main

import (
	"strings"
	"testing"
)

func TestSumPositions(t *testing.T) {
	instructions, _ := ReadInstructions(strings.NewReader("forward 5\ndown 5\nforward 8\nup 3\ndown 8\nforward 2"))
	horizontal, vertical := SumPositions(instructions)

	if horizontal != 15 {
		t.Error("Horizontal should be 15")
	}
	if vertical != 10 {
		t.Error("Vertical should be 10")
	}
}

func TestAimAndMove(t *testing.T) {
	instructions, _ := ReadInstructions(strings.NewReader("forward 5\ndown 5\nforward 8\nup 3\ndown 8\nforward 2"))
	horizontal, vertical := AimAndMove(instructions)

	if horizontal != 15 {
		t.Errorf("Horizontal should be 15. Got %d", horizontal)
	}
	if vertical != 60 {
		t.Errorf("Vertical should be 60. Got %d", vertical)
	}
}

func TestInput(t *testing.T) {
	got, _ := ReadInstructions(strings.NewReader("forward 5\ndown 5\nforward 8\nup 3\ndown 8\nforward 2"))
	length := len(got)
	if length != 6 {
		t.Errorf("Expected 4 items. Got %d %v", length, got)
	}
	if got[0].direction != "forward" {
		t.Errorf("Expected forward items. Got %s", got[0].direction)
	}
	if got[0].distance != 5 {
		t.Errorf("Expected forward items. Got %d", got[0].distance)
	}
}
