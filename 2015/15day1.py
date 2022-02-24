with open('15day1.in') as f:
  ans = 0
  pos = 1
  for cmd in f.read():
    if cmd == '(':
      ans +=1
    else:
      ans -=1
    if ans < 0:
      print(pos)
      break
    pos+=1