#include <bits/stdc++.h>

using namespace std;

set<pair<int, int>> blackTiles;

void calculateTile(string s)
{
  pair<int, int> currentTile = {0, 0};
  for (int i = 0; i < s.length(); i++)
  {
    if (s[i] == 'e')
    {
      currentTile.first++;
    }
    else if (s[i] == 'w')
    {
      currentTile.first--;
    }
    else if (s[i] == 'n' && s[i + 1] == 'w')
    {
      currentTile.first--;
      currentTile.second++;
      i++;
    }
    else if (s[i] == 'n' && s[i + 1] == 'e')
    {
      currentTile.second++;
      i++;
    }
    else if (s[i] == 's' && s[i + 1] == 'w')
    {
      currentTile.second--;
      i++;
    }
    else if (s[i] == 's' && s[i + 1] == 'e')
    {
      currentTile.second--;
      currentTile.first++;
      i++;
    }
  }

  if (blackTiles.find(currentTile) != blackTiles.end())
  {
    blackTiles.erase(currentTile);
  }
  else
  {
    blackTiles.insert(currentTile);
  }
}
int main()
{
  ifstream f("day24.in");
  string s;
  while (getline(f, s))
  {
    calculateTile(s);
  }
  cout << blackTiles.size();
}