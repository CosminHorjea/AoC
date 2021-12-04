f = open("input.txt", "r")

larger = 0

# Part 1
# line = f.readline()
# depth = int(line)
# for line in f.readlines():
# 	if depth < int(line):
# 		larger += 1
# 	depth = int(line)
# print(larger)


depths = [int(x) for x in [f.readline(), f.readline(), f.readline()]]
for line in f.readlines():
    allDepths = sum(depths)
    depths.pop(0)
    depths += [int(line)]
    if allDepths < sum(depths):
        larger += 1

print(larger)
