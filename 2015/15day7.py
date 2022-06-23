f = open('15day7.txt', 'r')
mem = {}


def process_line(line):
    global mem
    # print("Current Line ", line)
    # print("Current mem ", mem)

    cmd, rez = line.split(' -> ')
    if 'AND' in cmd:
        a, b = cmd.split(' AND ')
        if((a.isalpha() and a not in mem) or (b.isalpha() and b not in mem)):
            return 0
        if(not a.isalpha()):
            mem[rez] = int(a) & mem[b]
            return 1
        mem[rez] = mem[a] & mem[b]
    elif 'OR' in cmd:
        a, b = cmd.split(' OR ')
        if(a not in mem or b not in mem):
            return 0
        mem[rez] = mem[a] | mem[b]
    elif 'LSHIFT' in cmd:
        a, b = cmd.split(' LSHIFT ')
        if(a not in mem):
            return 0
        mem[rez] = mem[a] << int(b)
    elif 'RSHIFT' in cmd:
        a, b = cmd.split(' RSHIFT ')
        if(a not in mem):
            return 0
        mem[rez] = mem[a] >> int(b)
    elif 'NOT' in cmd:
        _, a = cmd.split('NOT ')
        if(a not in mem):
            return 0
        mem[rez] = ~mem[a]
    else:
        if(not cmd.isdigit() and cmd in mem):
            mem[rez] = mem[cmd]
        elif(cmd.isdigit()):
            mem[rez] = int(cmd)
        else:
            return 0
    return 1


cmds_all = [x.strip() for x in f.readlines()]
cmds_all.sort()
cmds = cmds_all[:]
while(len(cmds) > 0):
    new_cmds = []
    for cmd in cmds:
        if(process_line(cmd) == 0):
            new_cmds.append(cmd)
    cmds = new_cmds
solution1 = mem['a']
# print(solution1)
mem.clear()
mem['b'] = solution1
# change it in input file 5Head
print("Part 2 -------------")
while(len(cmds_all) > 0):
    new_cmds = []
    for cmd in cmds_all:
        if(process_line(cmd) == 0):
            new_cmds.append(cmd)
        else:
            print("Executed ", cmd)
    cmds_all = new_cmds
solution2 = mem['a']
print(solution2)
