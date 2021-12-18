f = open("day18.test", "r")


def solve(nums):
    solved = 0

    while not solved:
        solved = 1

        print("Step: ", nums)
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
            if num >= 10:
                nums.pop(i)
                nums.insert(i, (num // 2, depth + 1))
                nums.insert(i + 1, (num // 2 + num % 2, depth + 1))
                solved = 0
                break
    return nums


nums = []
for line in f:
    print("Init: ", nums)
    print("Line: ", line.strip())
    for i in range(len(nums)):
        nums[i] = (nums[i][0], nums[i][1] + 1)
    depth = len(nums) > 1
    for c in line:
        if c == "[":
            depth += 1
        elif c.isdigit():
            nums.append((int(c), depth))
        elif c == "]":
            depth -= 1
    print("Before: ", nums)
    nums = solve(nums)

    print("After: ", nums)
    print()


# print(nums)
