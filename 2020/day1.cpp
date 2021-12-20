#include <bits/stdc++.h>

using namespace std;

int main()
{
  ifstream f("day1.in");
  int x;
  map<int, bool> seen;
  int ok = 0;
  // while (!ok)
  // {
  //   f >> x;
  //   if (seen[2020 - x])
  //   {
  //     cout << "\nRaspuns:" << (2020 - x) * x << endl;

  //     ok = 1;
  //   }
  //   else
  //   {
  //     seen[x] = true;
  //   }
  // }
  vector<int> numbers;
  while (f >> x)
  {
    numbers.push_back(x);
  }
  for (int i = 0; i < numbers.size(); ++i)
  {
    for (int j = i + 1; j < numbers.size(); ++j)
    {
      for (int k = j + 1; k < numbers.size(); ++k)
      {
        if (numbers[i] + numbers[j] + numbers[k] == 2020)
        {
          cout << numbers[i] * numbers[j] * numbers[k];
          return 0;
        }
      }
    }
  }
}