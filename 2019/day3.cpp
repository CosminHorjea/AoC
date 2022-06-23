#include <bits/stdc++.h>

using namespace std;
int main()
{
  ifstream f("day3.in");
  std::string firstLine, secondLine;
  f >> firstLine;
  std::string delimiter = ",";
  vector<pair<char, int>> moves1;
  vector<pair<char, int>> moves2;

  size_t pos = 0;
  std::string token;
  while ((pos = firstLine.find(delimiter)) != std::string::npos)
  {
    token = firstLine.substr(0, pos);
    // std::cout << token << std::endl;
    moves1.push_back(make_pair(token[0], atoi(token.substr(1).c_str())));
    firstLine.erase(0, pos + delimiter.length());
  }
  // std::cout << firstLine << std::endl;
  moves1.push_back(make_pair(firstLine[0], atoi(firstLine.substr(1).c_str())));

  f >> secondLine;
  pos = 0;
  while ((pos = secondLine.find(delimiter)) != std::string::npos)
  {
    token = secondLine.substr(0, pos);
    moves2.push_back(make_pair(token[0], atoi(token.substr(1).c_str())));
    secondLine.erase(0, pos + delimiter.length());
  }
  moves2.push_back(make_pair(secondLine[0], atoi(secondLine.substr(1).c_str())));

  map<char, int> dirX = {{'U', 0}, {'D', 0}, {'L', -1}, {'R', 1}};
  map<char, int> dirY = {{'U', 1}, {'D', -1}, {'L', 0}, {'R', 0}};
  set<pair<int, int>> visited1;
  set<pair<int, int>> visited2;
  map<pair<int, int>, int> steps1;
  map<pair<int, int>, int> steps2;

  int posX = 0, posY = 0;
  int currSteps = 0;
  for (auto p : moves1)
  {

    char dir = p.first;
    int n = p.second;
    for (int i = 0; i < n; i++)
    {
      posX += dirX[dir];
      posY += dirY[dir];
      visited1.insert({posX, posY});
      currSteps++;
      if (steps1.find({posX, posY}) == steps1.end())
        steps1[{posX, posY}] = currSteps;
    }
  }
  // cout << visited1.size();
  posX = 0, posY = 0;
  currSteps = 0;
  for (auto p : moves2)
  {
    char dir = p.first;
    int n = p.second;
    for (int i = 0; i < n; i++)
    {
      posX += dirX[dir];
      posY += dirY[dir];
      visited2.insert({posX, posY});
      currSteps++;
      if (steps2.find({posX, posY}) == steps2.end())
      {
        steps2[{posX, posY}] = currSteps;
      }
    }
  }
  // cout << visited2.size();
  vector<pair<int, int>> intersection(min(visited1.size(), visited2.size()));
  set_intersection(visited1.begin(), visited1.end(), visited2.begin(), visited2.end(), intersection.begin());
  int minDist = abs(intersection[0].first) + abs(intersection[0].second);
  // cout << intersection.size();
  // cout << minDist; // PART 1
  // for (auto p : intersection)
  // {
  //   cout << p.first << " " << p.second << endl;
  //   int dist = abs(p.first) + abs(p.second);
  //   if (dist == 0) //cand aloc memoria pentru intersectia seturilor, mi sa face fill cu {0,0}
  //   {
  //     break;
  //   }
  //   minDist = min(dist, minDist);
  // }
  // cout << "Min dist " << minDist;
  //PART 2
  int minSteps = steps1[intersection[0]] + steps2[intersection[0]];
  for (auto p : intersection)
  {
    if (!p.first && !p.second)
      break;
    minSteps = min(minSteps, steps1[p] + steps2[p]);
  }
  cout << "Min Steps " << minSteps;
}