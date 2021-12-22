#include <bits/stdc++.h>

using namespace std;

int main()
{
  ifstream f("day6.in");
  string s;
  int persons = 0;
  map<char, int> peopleWhoSaidYes;
  int peopleInGroup = 0;
  while (getline(f, s))
  {
    if (s.length() == 0)
    {
      for (pair<char, int> ans : peopleWhoSaidYes)
      {
        if (ans.second == peopleInGroup)
          persons++;
      }
      peopleWhoSaidYes.clear();
      peopleInGroup = -1;
    }
    for (char c : s)
    {
      peopleWhoSaidYes[c]++;
    }
    peopleInGroup++;
  }
  for (pair<char, int> ans : peopleWhoSaidYes)
  {
    if (ans.second == peopleInGroup)
      persons++;
  }

  cout << persons << endl;
}