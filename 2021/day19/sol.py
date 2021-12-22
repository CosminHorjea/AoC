from math import cos, floor, pi, sin

import numpy as np

def cosFloor(x):
	return floor(cos(x))
def sinFloor(x):
	return floor(sin(x))

def rotationMatrix(gamma,beta,alpha):
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
	if(len(set(result)) != len(result)):
		return ()
	for x in result:
		if x not in point and -x not in point:
			return ()
	# print(result)
	return result

idx = 1
def rotatePoint(x,y,z):
	global idx
	seen = set()
	rotations = [0,90,180,270]
	for i in rotations:
		for j in rotations:
			for k in rotations:
				r = rotationMatrix(i*pi/180,j*pi/180,k*pi/180)
				idx += 1
				seen.add(tuple(mult(r,[x,y,z])))
	for s in seen:
		print(s)		
	print(len(seen))
	

f = open("day19.test")
content = f.read().split("\n\n")
scanners = [set() for x in range(28)]
# print(content.split("\n\n"))
for (i, scanner) in enumerate(content):
	scanner = scanner.split("\n")[1:]
	for pt in scanner:
		x, y, z = map(int, pt.split(","))
		scanners[i].add((x,y,z))

rotatePoint(3,1,2)