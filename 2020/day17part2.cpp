#include <bits/stdc++.h>
#define ll unsigned long
using namespace std;
#define space3D 30
//sooooo slow
struct pos
{
  int x, y, z, w;
};
int mod(int a, int b)
{
  int r = a % b;
  return r < 0 ? r + b : r;
}

void change(char value, pos position, vector<vector<vector<vector<char>>>> &space, vector<vector<vector<vector<char>>>> &spaceaux)
{
  int noActive = 0;
  for (int i : {-1, 0, 1})
  {
    for (int j : {-1, 0, 1})
    {
      for (int k : {-1, 0, 1})
      {
        for (int l : {-1, 0, 1})
        {
          if (i == 0 && j == 0 && k == 0 && l == 0)
            continue;
          if (space[mod(position.x + i, space3D)][mod(position.y + j, space3D)][mod(position.z + k, space3D)][mod(position.w + l, space3D)] == '#')
          {
            noActive++;
          }
        }
      }
    }
  }
  if (value == '.' && noActive == 3)
  {
    spaceaux[position.x][position.y][position.z][position.w] = '#';
  }
  else if (value == '#' && noActive != 2 && noActive != 3)
  {
    spaceaux[position.x][position.y][position.z][position.w] = '.';
  }
}

int main()
{
  ifstream f("day17.in");
  vector<vector<vector<vector<char>>>> space(space3D, vector<vector<vector<char>>>(space3D, vector<vector<char>>(space3D, vector<char>(space3D, '.'))));
  int noActive = 0;

  for (int i = 0; i < 8; i++)
  {
    for (int j = 0; j < 8; j++)
    {
      f >> space[i][j][0][0];
    }
  }
  vector<vector<vector<vector<char>>>> spaceaux;
  spaceaux = space;
  int noCycle = 0;
  while (noCycle < 6)
  {
    for (int i = 0; i < space3D; i++)
    {
      for (int j = 0; j < space3D; j++)
      {
        for (int k = 0; k < space3D; k++)
        {
          for (int l = 0; l < space3D; l++)
          {
            change(space[i][j][k][l], {i, j, k, l}, space, spaceaux);
          }
        }
      }
    }
    noCycle++;
    space = spaceaux;
  }
  // int noActive = 0;
  for (auto a : space)
    for (auto b : a)
      for (auto c : b)
        for (auto d : c)
          if (d == '#')
            noActive++;

  cout << noActive << endl;
}