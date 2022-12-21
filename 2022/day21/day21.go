package main

import (
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	// data, _ := os.ReadFile("input_test.txt")
	data, _ := os.ReadFile("input.txt")
	values := make(map[string]string)
	lines := strings.Split(string(data), "\n")
	for len(lines) > 0 {
		line := lines[0]
		lines = lines[1:]
		monke := strings.Split(line, ": ")[0]
		right_side := strings.Split(line, ": ")[1]

		terms := strings.Split(right_side, " ")
		if len(terms) == 1 {
			if monke == "humn" {
				values[monke] = "x"
			} else {
				values[monke] = terms[0]
			}
		} else {
			monke1 := terms[0]
			monke2 := terms[2]
			operation := terms[1]
			if values[monke1] == "" || values[monke2] == "" {
				lines = append(lines, line)
				continue
			}

			if monke == "root" {
				values[monke] = "(" + values[monke1] + ")" + "=" + "(" + values[monke2] + ")"
				continue
			}
			valMonke1, err1 := strconv.Atoi(values[monke1])
			valMonke2, err2 := strconv.Atoi(values[monke2])
			if err1 != nil || err2 != nil {
				switch operation {
				case "+":
					values[monke] = "(" + values[monke1] + "+" + values[monke2] + ")"
				case "*":
					values[monke] = "(" + values[monke1] + "*" + values[monke2] + ")"
				case "/":
					values[monke] = "(" + values[monke1] + "/" + values[monke2] + ")"
				case "-":
					values[monke] = "(" + values[monke1] + "-" + values[monke2] + ")"
				default:
					panic("unknown operation")
				}
			} else {
				switch operation {
				case "+":
					values[monke] = strconv.Itoa(valMonke1 + valMonke2)
				case "*":
					values[monke] = strconv.Itoa(valMonke1 * valMonke2)
				case "/":
					values[monke] = strconv.Itoa(valMonke1 / valMonke2)
				case "-":
					values[monke] = strconv.Itoa(valMonke1 - valMonke2)
				default:
					panic("unknown operation")
				}
			}
		}
	}
	fmt.Println(values["root"])
	// https://quickmath.com/#c=solve&v1=%2528%2528%2528354%2B%2528331%2B%2528%252844343996218333-%2528%2528995%2B%2528330%2B%2528%25286*%2528%2528%2528%2528%2528%2528%2528%2528%2528%2528%2528%2528%2528%2528387%2B%2528%2528473%2B%25283*%2528%2528%2528%2528%2528%252810*%2528%2528%2528%2528%2528%2528%2528%2528%25284*%2528%2528%2528%2528%2528%25282*%2528573%2B%2528%2528881%2B%2528134%2B%25281000%2B%2528909%2B%25282*%2528%25284*%2528441%2B%2528%2528%2528%2528169%2B%2528%2528%2528%2528%2528%2528%252874*%2528750%2B%2528%2528x-601%2529%2F2%2529%2529%2529%2B829%2529*2%2529-555%2529%2F5%2529-453%2529%2F2%2529%2529*2%2529-170%2529%2F2%2529%2529%2529-317%2529%2529%2529%2529%2529%2529%2F3%2529%2529%2529-102%2529%2F4%2529%2B402%2529%2F2%2529-826%2529%2529-438%2529*2%2529%2B171%2529%2F5%2529-978%2529%2B889%2529%2F2%2529%2B322%2529%2529%2B649%2529*2%2529-62%2529%2F7%2529-257%2529%2529%2529%2F2%2529%2529*2%2529-756%2529%2F4%2529-589%2529*2%2529%2B653%2529%2B173%2529%2F3%2529-343%2529*2%2529%2B561%2529%2F5%2529%2B977%2529%2529-823%2529%2529%2529%2F2%2529%2529*5%2529%2529%2529%2F5%2529%2529%253D%252821830569590923%2529&v3=x
	// i can't believe it worked
}

// func main() { //part 1
// 	// data, _ := os.ReadFile("input_test.txt")
// 	data, _ := os.ReadFile("input.txt")
// 	values := make(map[string]int)
// 	fmt.Println(values["a"])
// 	lines := strings.Split(string(data), "\n")
// 	for len(lines) > 0 {
// 		line := lines[0]
// 		lines = lines[1:]
// 		monke := strings.Split(line, ": ")[0]
// 		right_side := strings.Split(line, ": ")[1]

// 		terms := strings.Split(right_side, " ")
// 		if len(terms) == 1 {
// 			parsed, _ := strconv.Atoi(terms[0])
// 			values[monke] = parsed
// 		} else {
// 			monke1 := terms[0]
// 			monke2 := terms[2]
// 			operation := terms[1]
// 			if values[monke1] == 0 || values[monke2] == 0 {
// 				lines = append(lines, line)
// 				continue
// 			}
// 			switch operation {
// 			case "+":
// 				values[monke] = values[monke1] + values[monke2]
// 			case "*":
// 				values[monke] = values[monke1] * values[monke2]
// 			case "/":
// 				values[monke] = values[monke1] / values[monke2]
// 			case "-":
// 				values[monke] = values[monke1] - values[monke2]
// 			default:
// 				panic("unknown operation")
// 			}

// 		}
// 	}
// 	fmt.Println(values["root"])
// }
