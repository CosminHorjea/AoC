from copy import deepcopy

def do_step(situation):
	old_map = deepcopy(situation)
	new_map = deepcopy(situation)
	for i in range(len(situation)):
		for j in range(len(situation[0])):
			if(situation[i][j] == ">"):
				if(j == len(situation[0])-1):
					if(situation[i][0] == '.'):
						new_map[i][0] = '>'
						new_map[i][j] = '.'
				elif(situation[i][j+1] == "."):
					new_map[i][j+1] = ">"
					new_map[i][j] = "."
	situation = deepcopy(new_map)
	# show_map(situation)
	for i in range(len(situation)):
		for j in range(len(situation[0])):
			if(situation[i][j] == "v"):
				if(i == len(situation)-1):
					if situation[0][j] == '.':
						new_map[0][j] = 'v'
						new_map[i][j] = '.'
				elif situation[i+1][j] == '.':
					new_map[i+1][j] = 'v'
					new_map[i][j] = '.'
	situation = deepcopy(new_map)
	# show_map(situation)
	for i in range(len(situation)):
		for j in range(len(situation[0])):
			if(situation[i][j] != old_map[i][j]):
				return (True,situation)
	return (False,situation)

def show_map(situation):
	for i in situation:
		for c in i:
			print(c,end="")
		print()
	print("----------------")

f = open("day25.in","r")

situation = f.readlines()

situation = [list(x.strip()) for x in situation]
# print(situation)

idx = 1 
while True:
	if idx % 100 == 0:
		print(idx)
	(done,situation) = do_step(situation)
	if(not done):
		break
	idx += 1
print(idx)