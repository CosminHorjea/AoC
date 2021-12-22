#include <bits/stdc++.h>

using namespace std;

set<pair<int, int>> blackTiles;
vector<pair<int, int>> deltas = {{1, 0}, {1, 1}, {0, 1}, {-1, 0}, {-1, -1}, {0, -1}};

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

int neighboursBlack(pair<int, int> pos)
{
  int numberOfBlacks = 0;
  for (auto p : deltas)
  {
    if (blackTiles.find({pos.first + p.first, pos.second + p.second}) != blackTiles.end())
    {
      numberOfBlacks++;
    }
  }
  return numberOfBlacks;
}

int main()
{
  ifstream f("day24.in");
  string s;
  while (getline(f, s))
  {
    calculateTile(s);
  }
  cout << blackTiles.size() << endl;
  for (auto p : blackTiles)
    cout << p.first << ' ' << p.second << endl;
  // return 0;
  for (int iter = 0; iter < 100; iter++)
  {
    cout << iter << ':' << blackTiles.size() << endl;
    set<pair<int, int>> auxSet;
    int neighbours;
    for (int y = -100; y < 100; y++) //probably this is broke, shoul find the min and max from black tiles;
    {

      for (int x = -100; x < 100; x++)
      {
        neighbours = neighboursBlack({x, y});
        if ((blackTiles.find({x, y}) != blackTiles.end()) && !(neighbours == 0 || neighbours > 2))
        {
          auxSet.insert({x, y});
        }
        else if (neighbours == 2 && blackTiles.find({x, y}) == blackTiles.end())
        {
          auxSet.insert({x, y});
        }
      }
    }
    blackTiles = auxSet;
  }
  cout << endl
       << blackTiles.size();
}
//4458 too high<- this is what i get

//4280 - correct
// idk what is the problem , my solution is very similar to this:
//https://raw.githubusercontent.com/mebeim/aoc/master/2020/solutions/day24.py