f = open("day18.in", "r")


def calculate(nums):
    depth = 4
    while depth > 0:
        sol = 0
        for i in range(len(nums)):
            if nums[i][1] == depth:
                x, y = nums[i][0], nums[i + 1][0]
                nums.pop(i)
                nums.pop(i)
                nums.insert(i, (3 * x + 2 * y, depth - 1))
                sol = 1
                break
        if not sol:
            depth -= 1

    return nums[0][0]


def solve(nums):
    solved = 0

    while not solved:
        solved = 1

        # print("Step: ", nums)
        for (i, (num, depth)) in enumerate(nums):
            if depth > 4:
                if i > 0:
                    (n, m) = nums[i - 1]
                    nums[i - 1] = ((n + num), m)
                if i < len(nums) - 2:
                    (n, m) = nums[i + 2]
                    nums[i + 2] = ((n + nums[i + 1][0]), m)
                nums.pop(i)
                nums.pop(i)
                nums.insert(i, (0, depth - 1))
                solved = 0
                break
        if not solved:
            continue
        for (i, (num, depth)) in enumerate(nums):
            if num >= 10:
                nums.pop(i)
                nums.insert(i, (num // 2, depth + 1))
                nums.insert(i + 1, (num // 2 + num % 2, depth + 1))
                solved = 0
                break
    return nums


max_calc = 0

lines = f.readlines()
for line1 in lines:
    depth = 0
    nums = []
    for c in line1:
        if c == "[":
            depth += 1
        elif c.isdigit():
            nums.append((int(c), depth))
        elif c == "]":
            depth -= 1
    for line2 in lines:
        nums_aux = nums.copy()
        # print("Init: ", nums)
        # print("Line: ", line.strip())
        for i in range(len(nums)):
            nums[i] = (nums[i][0], nums[i][1] + 1)
        depth = len(nums) > 1
        for c in line2:
            if c == "[":
                depth += 1
            elif c.isdigit():
                nums.append((int(c), depth))
            elif c == "]":
                depth -= 1
        # print("Before: ", nums)
        nums = solve(nums)

        # print("After: ", nums)
        # print()
        print(calculate(nums))

        max_calc = max(calculate(nums), max_calc)
        nums = nums_aux.copy()


print("Max: ", max_calc)
