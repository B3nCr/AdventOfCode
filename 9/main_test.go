package main

import (
	"testing"
)

func TestValidate(t *testing.T) {
	testData := []int{
		35,
		20,
		15,
		25,
		47,
		40,
		62,
		55,
		65,
		95,
		102,
		117,
		150,
		182,
		127,
		219,
		299,
		277,
		309,
		576}
	_, result := Validate(testData, 5)
	if result != 127 {
		t.Error("test failure")
	}
}

func TestFindSequence(t *testing.T) {
	testData := []int{
		35,
		20,
		15,
		25,
		47,
		40,
		62,
		55,
		65,
		95,
		102,
		117,
		150,
		182,
		127,
		219,
		299,
		277,
		309,
		576}

	low, high := FindSequence(testData, 14, 127)

	if low == 0 || high == 0 {
		t.Error("test failure")
	}
}
