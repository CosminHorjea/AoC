# def toggle(x,y,z,cubes,state):
# 	if x[0]<-50 or x[1]>50:
# 		return
# 	for i in range(x[0],x[1]+1):
# 		for j in range(y[0],y[1]+1):
# 			for k in range(z[0],z[1]+1):
# 				if( state == "on" ):
# 					cubes.add((i,j,k))
# 				elif((i,j,k) in cubes):
# 					cubes.remove((i,j,k))

def part2(x,y,z,cubes,state):
	new_cubes = collections.Counter()
	for ((ox,oy,oz),sgn) in cubes.items():
		#verify if the cube is in the range
		ix0 = max(ox[0],x[0]); ix1 = min(ox[1],x[1])
		iy0 = max(oy[0],y[0]); iy1 = min(oy[1],y[1])
		iz0 = max(oz[0],z[0]); iz1 = min(oz[1],z[1])
		if(ix0<=ix1 and iy0<=iy1 and iz0<=iz1):
			new_cubes[((ix0,ix1),(iy0,iy1),(iz0,iz1))] -= sgn 
	if state > 0:
		new_cubes[(x,y,z)]+= state
	cubes.update(new_cubes)

def calculate_volume(x,y,z):
	return (x[1]-x[0]+1) * (y[1]-y[0]+1) * (z[1]-z[0]+1)
import collections
f = open("day22.in","r")
cubes = collections.Counter()

for l in f.readlines():
	state = 1 if l.split(" ")[0] == "on" else -1
	l = l.split(" ")[1].split(",")
	x,y,z = tuple(map(lambda x : tuple(map(int,x[2:].split(".."))),l))
	# toggle(x,y,z,cubes,state)
	part2(x,y,z,cubes,state)

volume = 0
for i in cubes:
	volume += (cubes[i] * calculate_volume(*i))
print(volume)


# 2356413761977441 too high
# 1243330199283578 too high
# 1236645880885788 too high
# 1169600245432395 not right
# 1235164413198198 - correct
# 2758514936282235 - coorect test
# 2758447208291213
# 2758481072261743
