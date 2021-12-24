# did it by hand and verified my work with this
# subbreddit gave coll insights https://www.reddit.com/r/adventofcode/comments/rnejv5/2021_day_24_solutions/

# model_num = "79997391969649"
model_num = "16931171414113" 
model_num = list(map(int,model_num))
memory = {
	"w":0,
	"x":0,
	"y":0,
	"z":0
}

def execute(command):
	global memory,model_num
	stmts = command.split(" ")
	instruction = stmts[0]
	if(len(stmts) == 2):
		# its input
		print(memory)
		memory[stmts[1]] = model_num.pop(0)
	else:
		args = stmts[1:]
		if instruction == "add":
			if(args[1].isalpha()):
				memory[args[0]]+=memory[args[1]]
			else:
				memory[args[0]]+=int(args[1])
	
		elif instruction == "mul":
			if(args[1].isalpha()):
				memory[args[0]]*=memory[args[1]]
			else:
				memory[args[0]]*=int(args[1])
		elif instruction == "mod":
			if(args[1].isalpha()):
				memory[args[0]]%=memory[args[1]]
			else:
				memory[args[0]]%=int(args[1])
		elif instruction == "div":
			if(args[1].isalpha()):
				memory[args[0]]//=memory[args[1]]
			else:
				memory[args[0]]//=int(args[1])
		elif instruction == "eql":
			if(args[1].isalpha()):
				memory[args[0]] = 1 if memory[args[1]] == memory[args[0]] else 0
			else:
				memory[args[0]] = 1 if int(args[1]) == memory[args[0]] else 0 
		
f= open("day24.in","r")
commands = f.readlines()
f.close()
for cmd in commands:
	execute(cmd.strip())

print(memory)