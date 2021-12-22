#include <bits/stdc++.h>
#define ll unsigned long
using namespace std;

int main()
{
  ifstream f("day15.in");
  int x;
  unordered_map<int, int> seen;
  unordered_map<int, int> seenBefore;
  unordered_map<int, int> times;
  int i = 1;
  while (f >> x)
  {
    seen[x] = i++;
    times[x] = 1;
  }
  while (i <= 2020) // <=30mil for part 2
  {
    if (times[x] == 1)
    {
      seenBefore[0] = seen[0];
      seen[0] = i;
      times[0]++;
      x = 0;
    }
    else //it's kind of shitty, beacause seen[x] it's basically the turnNUmber-1, so it could be improved but i should go to class now
    {
      int spoke = seen[x] - seenBefore[x];
      if (seen[spoke])
        seenBefore[spoke] = seen[spoke];
      seen[spoke] = i;
      times[spoke]++;
      x = spoke;
    }
    i++;
  }
  // cout << x;
  for (auto p : seen)
  {
    if (p.first == 164878)
      cout << p.first << ' ' << p.second << endl;
  }
}
//
//164878 part 2