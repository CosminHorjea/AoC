#include <bits/stdc++.h>

using namespace std;

bool verify(deque<int> q, int val)
{
  set<int> seen;
  for (int i : q)
  {
    if (seen.find(val - i) != seen.end())
      return true;
    seen.insert(i);
  }
  return false;
}
long sumOfq(deque<long> q)
{
  long sum = 0;
  for (int i : q)
  {
    sum += i;
  }
  return sum;
}
int findSmallHigh(deque<long> q)
{
  int high = 0, small = q.front();
  for (int i : q)
  {
    if (i > high)
      high = i;
    if (i < small)
      small = i;
  }
  return small + high;
}
int main()
{
  const int target = 258585477;
  ifstream f("day9.in");
  long in, preambleLength = 25;
  deque<long> q;
  // while (f >> in)
  // {
  //   if (q.size() <= preambleLength)
  //   {
  //     q.push_back(in);
  //     continue;
  //   }
  //   if (!verify(q, in))
  //   {
  //     cout << in;
  //     break;
  //   }
  //   q.pop_front();
  //   q.push_back(in);
  // }
  long sum = 0;
  while (f >> in)
  {
    if (in > target)
    {
      cout << "ies bag pl";
      break;
    }
    // cout << sum << endl;
    if (!q.size() || sumOfq(q) < target)
    {
      q.push_back(in);
      // sum += in;
    }
    else if (sumOfq(q) > target)
    {
      while (sumOfq(q) > target)
      {
        // cout << "i try----------|";
        // sum -= q.front();
        q.pop_front();
      }
    }
    if (sumOfq(q) == target)
    {
      cout << "am gasit: ";
      cout << findSmallHigh(q) << endl;
      break;
    }
    q.push_back(in);
  }
}
//258585477