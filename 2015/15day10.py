import itertools


number = open("15day10.in").read().strip()

# for i in range(50):
#     aux = ""
#     i = 0
#     while i < len(number):
#         j = i
#         while j < len(number) and number[i] == number[j]:
#             j += 1
#         aux += str(j - i) + number[i]
#         i = j
#     number = aux

for i in range(50):
    aux = ""
    for a, b in itertools.groupby(number):
        aux += str(len(list(b))) + a
    number = aux

print("Part 1/2:", len(number))
