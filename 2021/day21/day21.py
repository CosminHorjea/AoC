total_rolls =0


def rollDice(face):
    global total_rolls
    score = face
    face = face+1 if face < 100 else 1
    score += face 
    face = face+1 if face < 100 else 1
    score += face
    face = face+1 if face < 100 else 1
    total_rolls+= 3
    return face , score

##
# p1,p2 positions
# s1,s2 scores
# 
# 
dp = {}
def part2(p1,p2,s1,s2):
    if s1 >= 21:
        return (1,0)
    if s2 >= 21:
        return (0,1)
    if((p1,p2,s1,s2) in dp):
        return dp[(p1,p2,s1,s2)]
    wins = (0,0)
    for d1 in range(1,4):
        for d2 in range(1,4):
            for d3 in range(1,4):
                new_pos = (p1+d1+d2+d3) % 10
                new_score = s1 + new_pos + 1

                ans = part2(p2,new_pos,s2,new_score)
                wins = (ans[1]+wins[0],ans[0]+wins[1])
    
    dp[(p1,p2,s1,s2)] = wins
    return wins



#  -1 beacause it's easier with 0..9 (for part 2)
# p1_pos = 4-1 # test
# p2_pos = 8-1

p1_pos = 8-1
p2_pos = 3-1

p1_score = 0    
p2_score = 0

dice_roll = 1
roll_count = 0

print(part2(p1_pos,p2_pos,0,0))
# 306621346123766 for someone else

# while(p2_score<1000):
#     dice_roll, score = rollDice(dice_roll)
#     # print(f"Current face: {dice_roll}, score {score}, p1_pos {p1_pos}")
#     p1_pos += score %10
#     p1_pos = p1_pos if p1_pos <= 10 else p1_pos % 11 + (p1_pos//10)
#     p1_score += p1_pos
#     if(p1_score >= 1000):
#         print("P1 wins")
#         print(p2_score * total_rolls)
#         break;

#     dice_roll, score = rollDice(dice_roll)
#     # print(f"Current face: {dice_roll}, score {score}, p2_pos {p2_pos}")
#     p2_pos += score %10
#     p2_pos = p2_pos if p2_pos <= 10 else p2_pos % 11 + (p2_pos//10)
#     p2_score += p2_pos
#     if(p1_score >= 1000):
#         print("P2 wins")
#         print(p1_score * total_rolls)
#         break;
#     # print(f"Scores: P1: {p1_score}, P2: {p2_score}")
