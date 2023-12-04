numbers = open("input.in", "r").readlines()


def fromSNAFU(n: str):
    digits = {"0": 0, "1": 1, "2": 2, "-": -1, "=": -2}
    power = 1
    ans = 0
    for c in n[::-1]:
        ans += digits[c] * power
        power *= 5
    return ans


def toSNAFU(n: int):
    digits = {0: "0", 1: "1", 2: "2", -1: "-", -2: "="}
    ans = ""
    while n:
        n, b = divmod(n, 5)
        if b > 2:
            n += 1
            if b == 3:
                ans += "="
            else:
                ans += "-"
        else:
            ans += digits[b]
    return ans[::-1]


ans = 0
for i in range(len(numbers)):
    c = fromSNAFU(numbers[i].strip())
    ans += c
print(toSNAFU(ans))
