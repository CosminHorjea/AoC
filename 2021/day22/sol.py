def toggle(x,y,z,cubes,state):
	if x[0]<-50 or x[1]>50:
		return
	for i in range(x[0],x[1]+1):
		for j in range(y[0],y[1]+1):
			for k in range(z[0],z[1]+1):
				if( state == "on" ):
					cubes.add((i,j,k))
				elif((i,j,k) in cubes):
					cubes.remove((i,j,k))

def count_num_of_overplaps(x,y,z,cubes):
	for cube in cubes:
		if( x[0]<cube[0][0]<x[1] and y[0]<cube[1][0]<y[1] and z[0]<cube[2][0]<z[1] ):
			return abs(x[0]-cube[0][0])*abs(y[0]-cube[1][0])*abs(z[0]-cube[2][0])
		if( x[0]<=cube[0][1]<=x[1] and y[0]<=cube[1][1]<=y[1] and z[0]<=cube[2][1]<=z[1] ):
			return abs(x[0]-cube[0][1])*abs(y[0]-cube[1][1])*abs(z[0]-cube[2][1])
			
f = open("day22.in","r")

cubes = set()
# for l in f.readlines():
# 	# print(len(cubes))
# 	state = l.split(" ")[0]
# 	l = l.split(" ")[1].split(",")
# 	x,y,z = tuple(map(lambda x : tuple(map(int,x[2:].split(".."))),l))
# 	toggle(x,y,z,cubes,state)

cubes.add(((0,4),(0,4),(0,4)))
print(count_num_of_overplaps((0,2), (0,2), (0,2), cubes))

print(len(cubes))