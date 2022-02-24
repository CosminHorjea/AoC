totalWrappingPaper = 0
totalRibbon = 0
with open('15day2.in') as f:
    for line in f:
        line = line.strip()
        line = line.split('x')
        line = [int(i) for i in line]
        line.sort()
        # totalWrappingPaper += 2 * line[0]*line[1] + \
        # 2*line[1]*line[2] + 2*line[0]*line[2]
        # totalWrappingPaper += min(line[0]*line[1],
        #   line[1]*line[2], line[0]*line[2])
        totalRibbon += 2*(line[0]+line[1])
        totalRibbon += line[0]*line[1]*line[2]
    print(totalRibbon)
