#include <bits/stdc++.h>

using namespace std;

vector<vector<char>> harta;
int di[8] = {0, 0, -1, -1, -1, 1, 1, 1};
int dj[8] = {1, -1, -1, 1, 0, -1, 0, 1};
int main()
{
  ifstream f("day11.in");
  string line;
  int i = 0;
  while (getline(f, line))
  {
    harta.resize(i + 1);
    for (char c : line)
    {
      harta[i].push_back(c);
    }
    i++;
  }
  vector<vector<char>> newMap;
  newMap = harta;
  int n = harta.size();
  int m = harta[0].size();
  int changes = 1;
  // part 1
  // while (changes)
  // {
  //   changes = 0;
  //   for (int i = 0; i < harta.size(); i++)
  //   {
  //     for (int j = 0; j < harta[i].size(); j++)
  //     {
  //       int totalChairsOccupied = 0;
  //       for (int pos = 0; pos < 8; pos++)
  //       {
  //         int ni = i + di[pos];
  //         int nj = j + dj[pos];
  //         if ((ni >= 0 && ni < n && nj >= 0 && nj < m))
  //         {
  //           if (harta[ni][nj] == '#')
  //             totalChairsOccupied++;
  //         }
  //       }
  //       if (harta[i][j] == 'L' && totalChairsOccupied == 0)
  //       {
  //         newMap[i][j] = '#';
  //         changes = 1;
  //       }
  //       if (harta[i][j] == '#' && totalChairsOccupied >= 4)
  //       {
  //         changes = 1;
  //         newMap[i][j] = 'L';
  //       }
  //     }
  //   }
  //   harta = newMap;
  // }
  while (changes)
  {
    changes = 0;
    for (int i = 0; i < harta.size(); i++)
    {
      for (int j = 0; j < harta[i].size(); j++)
      {
        // cout << "Pozitia: " << i << ' ' << j << endl;
        int totalChairsOccupied = 0;
        for (int pos = 0; pos < 8; pos++)
        {
          int ni = i + di[pos];
          int nj = j + dj[pos];

          while ((ni >= 0 && ni < n && nj >= 0 && nj < m))
          {
            if (harta[ni][nj] == 'L')
              break;
            if (harta[ni][nj] == '#')
            {
              totalChairsOccupied++;
              break;
            }
            ni += di[pos];
            nj += dj[pos];
            // cout << ni << " " << nj << endl;
            // if (ni > 100)
            //   break;
          }
        }
        if (harta[i][j] == 'L' && totalChairsOccupied == 0)
        {
          newMap[i][j] = '#';
          changes = 1;
        }
        if (harta[i][j] == '#' && totalChairsOccupied >= 5)
        {
          changes = 1;
          newMap[i][j] = 'L';
        }
      }
    }
    harta = newMap;
  }
  int total = 0;
  for (auto line : harta)
  {
    for (char c : line)
    {
      if (c == '#')
        total++;
    }
  }
  cout << total << endl;
  //   for (auto i : newMap)
  //     for (auto j : i)
  //       cout << j << " ";
}