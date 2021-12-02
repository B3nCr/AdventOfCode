package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"strconv"
	"strings"
)

type Instruction struct {
	Instruction string
	Value       int
	Visited     bool
}

func (c Instruction) String() string {
	return fmt.Sprintf("{%s, %d}", c.Instruction, c.Value)
}

func main() {
	content, err := ioutil.ReadFile("./8/file.txt")
	if err != nil {
		log.Fatal(err)
	}

	allRows := strings.Split(string(content), "\r\n")
	masterProgram := getProgram(allRows)

	programs := getAllPrograms(masterProgram)

	for _, program := range programs {

		register, competed := runProgram(program)
		if competed {
			fmt.Printf("%d %v", register, competed)
			fmt.Println()
		}
	}
}

func getProgram(allRows []string) []Instruction {
	var program []Instruction

	for i := 0; i < len(allRows); i++ {
		val, _ := strconv.Atoi(allRows[i][4:])

		inst := Instruction{allRows[i][:3], val, false}

		program = append(program, inst)
	}

	return program
}

func getAllPrograms(program []Instruction) [][]Instruction {
	programs := make([][]Instruction, 0)

	for i := 0; i < len(program); i++ {

		if program[i].Instruction == "jmp" || program[i].Instruction == "nop" {
			newProgram := make([]Instruction, len(program))

			copy(newProgram, program)
			newProgram[i] = flipInstruction(newProgram[i])

			programs = append(programs, newProgram)
		}
	}
	return programs
}

func flipInstruction(instruction Instruction) Instruction {
	if instruction.Instruction == "jmp" {
		instruction.Instruction = "nop"
	} else if instruction.Instruction == "nop" {
		instruction.Instruction = "jmp"
	}

	return instruction
}

func runProgram(program []Instruction) (int, bool) {
	register := 0
	programCompleted := false
	var instructionPointer int

	for instructionPointer = 0; instructionPointer < len(program); {

		if program[instructionPointer].Visited == true {
			break
		}

		program[instructionPointer].Visited = true

		switch ins := program[instructionPointer].Instruction; ins {
		case "acc":
			register += program[instructionPointer].Value
			instructionPointer++
		case "jmp":
			instructionPointer += program[instructionPointer].Value
		case "nop":
			instructionPointer++
		}

		if instructionPointer >= len(program) {
			programCompleted = true
		}
	}

	return register, programCompleted
}
