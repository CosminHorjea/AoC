from math import cos, floor, pi, sin, ceil

import numpy as np
from itertools import product
from collections import Counter

# an array of matrices that have det 1
#  thank god for these
matrices = [
	[
		[1,0,0],
		[0,1,0],
		[0,0,1]
	],
	[
		[-1,0,0],
		[0,-1,0],
		[0,0,1]
	],
	[
		[-1,0,0],
		[0,1,0],
		[0,0,-1]
	],
	[
		[1,0,0],
		[0,-1,0],
		[0,0,-1]
	],
	[
		[-1,0,0],
		[0,0,1],
		[0,1,0]
	],
	[
		[1,0,0],
		[0,0,-1],
		[0,1,0]
	],
	[
		[1,0,0],
		[0,0,1],
		[0,-1,0]
	],
	[
		[-1,0,0],
		[0,0,-1],
		[0,-1,0]
	],
	[
		[0,-1,0],
		[1,0,0],
		[0,0,1]
	],
	[
		[0,1,0],
		[-1,0,0],
		[0,0,1]
	],
	[
		[0,1,0],
		[1,0,0],
		[0,0,-1]
	],
	[
		[0,-1,0],
		[-1,0,0],
		[0,0,-1]
	],
	[
		[0,1,0],
		[0,0,1],
		[1,0,0]
	],
	[
		[0,-1,0],
		[0,0,-1],
		[1,0,0],
	],
	[
		[0,-1,0],
		[0,0,1],
		[-1,0,0],
	],
	[
		[0,1,0],
		[0,0,-1],
		[-1,0,0],
	],
	[
		[0,0,1],
		[1,0,0],
		[0,1,0],
	],
	[
		[0,0,-1],
		[-1,0,0],
		[0,1,0],
	],
	[
		[0,0,-1],
		[1,0,0],
		[0,-1,0],
	],
	[
		[0,0,1],
		[-1,0,0],
		[0,-1,0],
	],
	[
		[0,0,-1],
		[0,1,0],
		[1,0,0],
	],
	[
		[0,0,1],
		[0,-1,0],
		[1,0,0],
	],
	[
		[0,0,1],
		[0,1,0],
		[-1,0,0],
	],
	[
		[0,0,-1],
		[0,-1,0],
		[-1,0,0],
	],

]

def cosFloor(x):
	return floor(cos(x))
def sinFloor(x):
	return floor(sin(x))
# this, for some reason, produces mirror images and some kings of wierd rotations that change the magnitude, maybe i shouldn't floor
def rotationMatrix(alpha,beta,gamma):
	return [
		[cosFloor(alpha)*cosFloor(beta),cosFloor(alpha)*sinFloor(alpha)*sinFloor(gamma)-sinFloor(alpha)*cosFloor(gamma),cosFloor(alpha)*sinFloor(beta)*cosFloor(gamma)+sinFloor(alpha)*sinFloor(gamma)],
		[sinFloor(alpha)*cosFloor(beta),sinFloor(alpha)*sinFloor(alpha)*sinFloor(gamma)+cosFloor(alpha)*cosFloor(gamma),sinFloor(alpha)*sinFloor(beta)*cosFloor(gamma)-cosFloor(alpha)*sinFloor(gamma)],
		[-sinFloor(beta)			   ,cosFloor(beta)*sinFloor(gamma) 												   ,cosFloor(beta)*cosFloor(gamma)]
	]

def mult(X,point):
	result = [0,0,0]
	result[0] = sum( [x*y for x,y in zip(X[0],point)])
	result[1] = sum( [x*y for x,y in zip(X[1],point)])
	result[2] = sum( [x*y for x,y in zip(X[2],point)])
	# if(len(set(result)) != len(set(point))):
	# 	return None
	# for x in result:
	# 	if x not in point and -x not in point:
	# 		return None
	return result

def rotatePointAllDirections(x,y,z):
	seen = set()
	# rotations = [0,90,180,270]
	# for i,j,k in product(rotations,repeat=3):
	# 	r = rotationMatrix(i*pi/180,j*pi/180,k*pi/180)
	# 	if(mult(r,[x,y,z])):
	# 		seen.add(tuple(mult(r,[x,y,z])))
	for mat in matrices:
		seen.add(tuple(mult(mat,[x,y,z])))
	for s in seen:
		print(s)
	print(len(seen))		
	
def rotatePoint(x,y,z,alpha,beta,gamma):
	r = rotationMatrix(alpha,beta,gamma)
	return mult(r,[x,y,z])

def countOverlaps(ref_scanner,curr_scanner):
	if(not len(ref_scanner) or not len(curr_scanner)):
		return (False,0)
	if(not len(ref_scanner[0]) or not len(curr_scanner[0])):
		return (False,0)
	# print(ref_scanner,curr_scanner)
	overlaps = Counter()
	for i in range(len(ref_scanner)):
		for j in range(len(curr_scanner)):
			x,y,z = ref_scanner[i][0]-curr_scanner[j][0], \
					ref_scanner[i][1]-curr_scanner[j][1], \
					ref_scanner[i][2]-curr_scanner[j][2]
			overlaps[(x,y,z)] += 1
	# return any(i>=12 for i in overlaps.values())
	for (k,v) in overlaps.items():
		if(v>=12):
			# print(overlaps.values())
			return (True,k)
	return (False,0)
				

# f = open("day19.test")
# scanners = [[] for x in range(5)]
f = open("day19.in")
scanners = [[] for x in range(28)]
content = f.read().split("\n\n")
# print(content.split("\n\n"))
for (i, scanner) in enumerate(content):
	scanner = scanner.split("\n")[1:]
	for pt in scanner:
		x, y, z = map(int, pt.split(","))
		scanners[i].append((x,y,z))

rotations = [0,90,180,270]
actualPosition = []
reference_scanner = 0
done_scanners = [False for x in range(len(scanners))]
done_scanners[0] = True
marked = done_scanners.copy()
# for startin_point in range(len(scanners)):
# old_scanners = scanners.copy()
visited = set()
connected = set([0])
not_connected = set(range(0,len(scanners)))
while(len(actualPosition)<28): # num of scanners
	found = 0
	for i in connected:
		for j in not_connected.difference(connected):
			if((i,j) not in visited):
				visited.add((i,j))
				reference_scanner = i
				current_scanner = j
				found = 1
				break
		if(found):
			break
	if(not found):
		print('fuck')
		break

	# print(reference_scanner,current_scanner)
	overlapping = 0
	# print(reference_scanner,current_scanner)
	for mat in matrices:
		# most probably rotations are fucked
		rotatedPoints = [mult(mat,[x,y,z]) for x,y,z in scanners[current_scanner]]
		rotatedPoints = list(filter(lambda x: x is not None , rotatedPoints))
		if(len(rotatedPoints)!= len(scanners[current_scanner])):
			continue
		# print("-----------------------------")
		# print(scanners[reference_scanner],rotatedPoints)
		overlaps = countOverlaps(scanners[reference_scanner],rotatedPoints)
		if(overlaps[0]):
			print("For scanner index:",reference_scanner,"overlaps:",current_scanner)
			# print(i,j,k)
			print(len(scanners[current_scanner]),len(rotatedPoints))
			connected.add(current_scanner)
			visited.clear()
			print(overlaps[1])
			overlapping = 1
			(ox,oy,oz) = overlaps[1]
			actualPosition.append((ox,oy,oz))
			# print(scanners[current_scanner],len(scanners[current_scanner]))
			# print(rotatedPoints,len(rotatedPoints))
			for idx in range(len(scanners[current_scanner])):
				p = rotatedPoints[idx]
				scanners[current_scanner][idx] = (p[0]+ox,p[1]+oy,p[2]+oz)
			break

# print((scanners[20]))
# tst = set()
# idx = 1
rotatePointAllDirections(-407,576,646)
rotatePointAllDirections(3,1,-2)


totalBeacons = set()
for l in scanners:
	for pos in l:
		totalBeacons.add(pos)
print("Total Beacons: ",len(totalBeacons))

def calculateDistance(i,j):
	return abs(i[0]-j[0]) + abs(i[1]-j[1]) + abs(i[2]-j[2])

max_dist = 0
for i in actualPosition:
	for j in actualPosition:
		max_dist = max(max_dist,calculateDistance(i,j))  
print("Max Dsitance: ",max_dist)
# scanners=old_scanners.copy()
# print(len(actualPosition))
# 722 too high
# 503 too high