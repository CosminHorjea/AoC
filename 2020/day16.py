with open("input.txt") as f:
	data = [(x.strip("\n")) for x in f.readlines()]

def part1(debug=False):
	cur = 0
	nums = set()
	master = set()
	count = 0
	all_good = []
	my_ticket = []
	val_dict = dict()
	for i in data:
		if i == "":
			cur += 1
			continue
		if cur == 0:
			line = i.split(' ')
			name = i.split(': ')[0]
			nums = set()
			for l in line:
				if l[0].isdigit():
					mi,ma = l.split('-')
					mi = int(mi)
					ma = int(ma)
					for z in range(mi,ma+1):
						nums.add(z)
						master.add(z)
			val_dict[name] = {
			'range':nums,
			'poss':set(),
			}
		elif cur == 1:
			if i[0].isdigit() == False:
				continue
			my_ticket = [int(x) for x in i.split(",")]
			pass
		else:
			if i[0].isdigit() == False:
				continue
			tickets = [int(x) for x in i.split(",")]
			bad = False
			for tick in tickets:
				if tick not in master:
					bad = True
					break
			if bad == False:
				all_good.append(tickets)

	first = True
	for line in all_good:
		for index,tick in enumerate(line):
			for key,val in val_dict.items():
				if tick in val['range']:
					if first:
						val_dict[key]['poss'].add(index)
				else:
					val_dict[key]['poss'].discard(index)
		first = False
	change_list = dict()
	change = True
	while change:
		change = False
		for x,y in val_dict.items():
			if len(y['poss']) == 1:
				change_list[list(y['poss'])[0]] = x
		for x,y in val_dict.items():
			for h,j in change_list.items():
				if h in y['poss'] and x != j:
					val_dict[x]['poss'].discard(h)
					change = True
	total = 1
	for x,y in change_list.items():
		if y.split(" ")[0] == "departure":
			total *= my_ticket[x]
	print(total)
part1()