
f = open("15day8.in", 'r')

lines = f.readlines()
res = 0
for line in lines:
    line = line.strip()
    # print(line, " ", len(line))
    res -= len(line)
    # line = line.encode('string_escape')

    # print("Processed: ", len(line))
    # res -= len(line) - 2
    # escape characters from line
    line = line.replace('\\', '\\\\')
    line = line.replace('\"', '\\"')
    res += len(line) + 2
print(res)
