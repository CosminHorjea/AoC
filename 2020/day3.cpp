#include <bits/stdc++.h>

using namespace std;

int main()
{
  vector<vector<char>> harta;
  ifstream f("day3.in");
  string linie;
  int i = 0;
  while (getline(f, linie))
  {
    harta.resize(i + 1);
    for (char c : linie)
    {
      harta[i].push_back(c);
    }
    i++;
  }
  int nr = 0;
  pair<int, int> coords = {0, 0};
  int lines = harta.size();
  int cols = harta[0].size();
  cout << "linii " << lines << endl;
  cout << "coloane " << cols << endl;
  while (coords.second < harta.size() - 1)
  {
    //right            //down
    coords = {coords.first + 1, coords.second + 2};
    if (harta[coords.second][coords.first % cols] == '#')
    {
      nr++;
    }
  }
  cout << nr << endl;
  cout << 270 * 77 * 74 * 78 * 35;
}
// 280,77,74,78,35