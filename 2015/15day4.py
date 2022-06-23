import hashlib

with open('15day4.in', 'r') as f:
    data = f.read()
    flag = 1
    ans = 0
    while(flag):
        if hashlib.md5((data+str(ans)).encode('utf-8')).hexdigest()[:6] == '000000':
            print(ans)
            flag = 0
        ans += 1
