package main

import "testing"

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
	result := Validate(testData[0:15], 5)
	if !result {
		t.Error("test failure")
	}
}
