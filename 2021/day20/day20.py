def enhance(darks,enhancement,i,j,new_darks,min_i,max_i,min_j,max_j,outside):
    idx = 0

    for x in (i-1,i,i+1):
        for y in (j-1,j,j+1):
            # if((x,y) in darks):
            #     number+='1'
            # else:
            #     if outside and (x < min_i or x > max_i or y < min_j or y > max_j) :
            #         number += '1'
            #     else:
            #         number+='0'
            idx<<=1
            idx |= ((x,y) in darks)
            idx |= outside and (x < min_i or x > max_i or y < min_j or y > max_j)

    # number = int(number,2)
    out = enhancement[idx]
    if(out=='#'):
        new_darks.add((i,j))

def print_image(darks):
    offset = 10
    for x in range(-offset,offset):
        for y in range(-offset,offset):
            if((x,y) in darks):
                print("#",end="")
            else:
                print(".",end="")
        print()

# f = open("day20.test","r")
f = open("day20.in","r")
# f = open("day20_real_test.in","r") # should be 5326

image_enhancement = f.readline().strip()

input_image = f.readlines()[1:]

input_image = [line.strip() for line in input_image]

height = len(input_image)
width = len(input_image[0])

darks = set()


for i in range(height):
    for j in range(width):
        if input_image[i][j] == '#':
            darks.add((i,j))
outside = 0
rule = 1 if image_enhancement[0] =='#' else 0
for _ in range(50):
    new_darks = set()
    min_i,max_i = min(darks,key=lambda x:x[0])[0],max(darks,key=lambda x:x[0])[0]
    min_j,max_j = min(darks,key=lambda x:x[1])[1],max(darks,key=lambda x:x[1])[1]
    for i in range(min_i-1,max_i+2): 
        for j in range(min_j-1,max_j+2):
            enhance(darks,image_enhancement,i,j,new_darks,min_i,max_i,min_j,max_j, rule and _ % 2 ==1)
            
            
    darks = new_darks.copy()

print(len(darks))
# assert(5326 == len(darks))
# 7256 too high
# 5353 high
# 4678 too low
# 4863 wrong
# 5087-4 wrong 

# correct 
# 5057
# 18502 