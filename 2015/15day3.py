with open('15day3.in', 'r') as f:
    data = f.read()
    houses = set()
    # x = 0
    # y = 0
    # for c in data:
    #     if c == '^':
    #         y += 1
    #     elif c == 'v':
    #         y -= 1
    #     elif c == '<':
    #         x -= 1
    #     elif c == '>':
    #         x += 1
    #     houses.add((x, y))
    # print(len(houses))
    santa = (0, 0)
    robo = (0, 0)
    for i, c in enumerate(data):
        if i % 2 == 0:
            if c == '^':
                santa = (santa[0], santa[1] + 1)
            elif c == 'v':
                santa = (santa[0], santa[1] - 1)
            elif c == '<':
                santa = (santa[0] - 1, santa[1])
            elif c == '>':
                santa = (santa[0] + 1, santa[1])
            houses.add(santa)
        else:
            if c == '^':
                robo = (robo[0], robo[1] + 1)
            elif c == 'v':
                robo = (robo[0], robo[1] - 1)
            elif c == '<':
                robo = (robo[0] - 1, robo[1])
            elif c == '>':
                robo = (robo[0] + 1, robo[1])
            houses.add(robo)
    print(len(houses))
