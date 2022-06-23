# --- Day 6: Probably a Fire Hazard ---

def part1(input):
	lights = [[0 for x in range(1000)] for y in range(1000)]
	for line in input:
		instruction = line.split()
		if instruction[0] == 'turn':
			if instruction[1] == 'on':
				for i in range(int(instruction[2].split(',')[0]), int(instruction[4].split(',')[0]) + 1):
					for j in range(int(instruction[2].split(',')[1]), int(instruction[4].split(',')[1]) + 1):
						lights[i][j] = 1
			elif instruction[1] == 'off':
				for i in range(int(instruction[2].split(',')[0]), int(instruction[4].split(',')[0]) + 1):
					for j in range(int(instruction[2].split(',')[1]), int(instruction[4].split(',')[1]) + 1):
						if lights[i][j] > 0:
							lights[i][j] = 0
		elif instruction[0] == 'toggle':
			for i in range(int(instruction[1].split(',')[0]), int(instruction[3].split(',')[0]) + 1):
				for j in range(int(instruction[1].split(',')[1]), int(instruction[3].split(',')[1]) + 1):
					lights[i][j] = 1 - lights[i][j]
	total = 0
	for i in range(1000):
		for j in range(1000):
			total += lights[i][j]
	return total


def main():
    # Part 1
    with open('15day6.in', 'r') as f:
        lines = f.readlines()
        print(part1(lines))

    # Part 2
    # with open('input.txt', 'r') as f:
    #     lines = f.readlines()
    #     print(part2(lines))

if __name__ == '__main__':
    main()