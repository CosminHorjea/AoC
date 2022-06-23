
def isNiceString(s):
    vowels = ['a', 'e', 'i', 'o', 'u']
    hasDouble = False
    hasVowels = 0
    hasBadStr = False
    for i in range(len(s) - 1):
        if s[i] == s[i+1]:
            hasDouble = True
        if s[i] in vowels:
            hasVowels += 1
        if s[i] + s[i+1] in ['ab', 'cd', 'pq', 'xy']:
            hasBadStr = True
    return hasDouble and (hasVowels > 2) and not hasBadStr


def isNiceStringPart2(s):
    hasPair = False
    hasRepeat = False
    for i in range(len(s) - 2):
        if s[i] == s[i+2]:
            hasRepeat = True
        if s[i:i+2] in s[i+2:]:
            hasPair = True
    return hasRepeat and hasPair


with open('15day5.in', 'r') as f:
    ans = 0
    lines = f.readlines()
    for line in lines:
        if isNiceStringPart2(line):
            # print(line, 'is nice')
            ans += 1
    print(ans)
