#include <bits/stdc++.h>

using namespace std;

map<string, int> visited;

int calculateBags(map<string, vector<pair<int, string>>> bags, string bag)
{
  visited[bag] = 1;
  if (!bags.count(bag))
  {
    return 0;
  }
  int prod = 0;
  for (auto b : bags[bag])
  {
    // if (visited[b.second] == 0)
    prod = prod + b.first + b.first * calculateBags(bags, b.second);
    // else
    // prod += b.first * visited[b.second];
  }
  // visited[bag] = prod;
  return prod;
}

int main()
{
  ifstream f("day7.in");
  string s;
  string aux;
  int qty;
  string in1, in2;
  int nr = 0;
  map<string, vector<pair<int, string>>> bags;
  queue<string> bagSearch;
  map<string, int> seen;
  while (getline(f, s))
  {
    stringstream ss(s);
    string cl1, cl2;
    ss >> cl1 >> cl2;
    cl1 += cl2;
    ss >> aux >> aux;
    while (ss >> qty >> in1 >> in2)
    {
      ss >> aux;
      in1 += in2;
      if (in1 == "shinygold")
      {
        bagSearch.push(cl1);
        seen[cl1] = 1;
        nr++;
      }
      bags[cl1].push_back({qty, in1});
    }
  }

  // for (auto p : bags)
  // {
  //   cout << p.first << " ";
  //   for (auto b : p.second)
  //     cout << b.first << " " << b.second << " ";
  //   cout << endl;
  // }

  while (bagSearch.size() > 0)
  {
    string currBag = bagSearch.front();
    bagSearch.pop();
    for (auto p : bags)
    {
      for (auto b : p.second)
      {
        if (b.second == currBag && !seen[p.first])
        {
          // if (!seen[p.first])
          bagSearch.push(p.first);
          seen[p.first] = 1;
          nr++;
        }
      }
    }
  }
  // cout << nr << endl;
  int i = calculateBags(bags, "shinygold");
  // cout << calculateBags(bags, "shinygold") << endl;
  int others = 0;
  for (auto b : bags["shinygold"])
  {
    // others += b.first;
  }
  cout << others + i << endl;
}