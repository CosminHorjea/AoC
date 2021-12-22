#include <bits/stdc++.h>

using namespace std;

int main()
{
  ifstream f("day13.in");
  int x, time;
  string s;
  f >> time;
  getline(f, s);
  getline(f, s);
  // cout << s;
  char c;
  int minTime = time;
  int bestID;
  int start = 0;
  int end = s.find(',');
  // while (end != string::npos)//part 1
  // {
  //   const char *nr = s.substr(start, end - start).c_str();
  //   int busID = atoi(nr);
  //   if (busID)
  //   {
  //     minTime = min(busID - time % busID, minTime);
  //     if (minTime == busID - time % busID)
  //     {
  //       bestID = busID;
  //     }
  //   }
  //   start = end + 1;
  //   end = s.find(',', start);
  // }
  // cout << minTime * bestID << endl;
  //part 2
  // this is so cool : https://github.com/mebeim/aoc/blob/master/2020/README.md#day-13---shuttle-search
  vector<int> ids; // credits : https://github.com/neiomi1/AdventofCode2020/tree/master/Day_13
  while (end != string::npos)
  {
    const char *nr = s.substr(start, end - start).c_str();
    int busID = atoi(nr);
    if (busID)
    {
      ids.push_back(busID);
    }
    else
    {
      ids.push_back(0);
    }
    start = end + 1;
    end = s.find(',', start);
  }
  const char *nr = s.substr(start, end - start).c_str();
  int busID = atoi(nr);
  if (busID)
    ids.push_back(busID);
  else
    ids.push_back(0);

  int minute = -1;
  uint64_t mult = 0;
  map<int, int> waitTime;
  for (int busID : ids)
  {
    minute++;
    if (busID == 0)
      continue;
    if (minute == 0)
    {
      mult = (uint64_t)busID;
    }
    else
    {
      waitTime[busID] = busID - (minute % busID);
    }
  }
  uint64_t ts = 0;
  for (auto it = waitTime.begin(); it != waitTime.end(); it++)
  {
    while ((ts % (*it).first) != (*it).second)
    {
      ts += mult;
    }
    mult *= (*it).first;
  }
  cout << ts << endl;
}
