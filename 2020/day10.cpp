#include <bits/stdc++.h>
#define ll long long
using namespace std;

int main()
{
  int x;
  set<int> nums;
  vector<int> adapters;
  ifstream f("day10.in");
  int jolts = 0;
  int oneDiff = 0;
  int threeDiff = 0;
  int maxJolts = 0;
  while (f >> x)
  {
    nums.insert(x);
    adapters.push_back(x);
    maxJolts = max(maxJolts, x);
  }
  sort(adapters.begin(), adapters.end());
  // part 1
  // for (int i : nums)
  // {
  //   if (i - jolts == 1)
  //     oneDiff++;
  //   else if (i - jolts == 3)
  //     threeDiff++;
  //   jolts = i;
  // }
  // threeDiff++;
  // cout << oneDiff * threeDiff;

  //thanks : https://www.youtube.com/watch?v=5o-kdjv7FD0&ab_channel=CSDojo
  ll *joltsWays = new ll[maxJolts + 1];
  for (int i = 0; i <= maxJolts; i++)
    joltsWays[i] = 0;
  for (int n : nums)
  {
    joltsWays[n] = 1;
  }
  joltsWays[0] = 1;
  for (int i = 1; i <= maxJolts; i++)
  {
    if (joltsWays[i])
    {
      ll total = 0;
      for (int j : {1, 2, 3})
      {
        if (i - j >= 0)
          if (joltsWays[i - j])
            total += joltsWays[i - j];
      }
      joltsWays[i] = total;
    }
  }
  cout << joltsWays[maxJolts];
  cout << endl;
}