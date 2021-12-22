def distance(p1,p2):
	return ((p2[0]-p1[0])**2+(p2[1]-p1[1])**2+(p2[2]-p1[2])**2)**(1/2)
	# return (abs(p1[0]-p2[0]),abs(p1[1]-p2[1]),abs(p1[2]-p2[2]))

f = open("day19.test")
content = f.read().split("\n\n")
scanners = [set() for x in range(28)]
# print(content.split("\n\n"))
for (i, scanner) in enumerate(content):
	scanner = scanner.split("\n")[1:]
	for pt in scanner:
		x, y, z = map(int, pt.split(","))
		scanners[i].add((x,y,z))

distances = set()

for sc in scanners:
	for x in sc:
		print(x)
		for y in sc:
			if(x != y):
				distances.add(distance(x, y))
sz = len(distances) * 2
for i in range(1000):
	print(i," " ,i*(i-1)," ",sz)
	if(i*(i-1) > sz):
		break 

# n(n-1) / 2 = 1419
# n^2 - n - 2838 = 0
#  d=b^2-4ac