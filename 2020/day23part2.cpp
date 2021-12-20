#include <bits/stdc++.h>

using namespace std;

//ideea ar fi sa tin minte pozitiile cupelor intr-un hashmap dar nu stiu cum ar putea sa fie implementat
//https://github.com/cgreening/advent_of_code/blob/main/2020/day23/cups.ts
//https://topaz.github.io/paste/#XQAAAQC5DgAAAAAAAAARmknGRw8TogB3OxgfbBYnd5PaIesVedacC0iQ8HUdkaM0htASIes7BPdpmcRt2j7Gj/meo9XcsJ/BXY2w0PyBKYM3akKDT7EudDuL6DVSPu7G22qM4YKbp3M4p6by368bo34v9QC2xMP6FhB166QbYbgFwdSyxDJ1+hyO69Hp9+ZAm7w0Ds70YVYqsLiMEeDBJ3X1QvPxE0w6GELeu9MbwbH5Z2zsiXPlilBY7EkPDdFTGnKGH8lOwd/oPH9depbFr18I4Wr1lXuK8hyv/pd4IROkWU4fvHEDh5Q+uliiYLxAj9vthF5etN1m7iMX6WvFPPoEHpYpkIHKfaxowY/XNS9qbqPiRPggX018nAaQLwIEMw8OwHXU2ScnBRQEGnBs2tj8DSndMb+McneW6rGvDXm1BS4Zaufl/9Jq9J86bJ9735YvQ3d76dyJh/dhzJCx7gDbl7gC8+uyMcfZje6TQfkwyuDlMfIYAul/AcE9kdAzLs1g8d2BEeHGIj2lOM8HQ9qRab0xWMNoi8UGyxBRmHK9081+L4/0Yre3dVnr2N9qlns8kGNQCdwqtjkAXW0EmlAodgkJaCrfltfSZTvNMj9VfukzYWHafNCEhK9vRXzOAMc2/CI5AAvCYQGt5boTpeHUOV6ZoV44tyaeYuCrfDMradac5LA3J2jtkaasXsBYfaZdYFKCD1vm0IwqPU1VKicm5kZN7ZLiLBAIk6b2/pkhXif/FQwRg/W1fsfhuPBP/z4GDUYK6WLrB/8+IHdz0oXu36jSghFAZ23vUPPXD686GTbOLcOXpQscFZXykMlHqWlUc3lY6+4U/SPcXcv91yUr9Yj5E3rfz2nS86aJfzaxyfiAfC0c9YD/+o9e3CzRtbckqOjv1nwCoJdZpAU9c3hSdfUUI3JzukTDXUVx0+LMOXoQ0vOAoOiM636LZcrHerlNgF3DApfMrSe0tq2Bsi5VJeFin+GzANnIs+wcIZrT/agm60FRJqrPcBFFS+9PXE/NUe38lZJ/JekD+n7+J9x2SvVYuW/qVseeSHAImNHd91f5yLaLbjJNqTVOFSvUHLBhwZYIFMfQhl9Lib/Kn4WrdHnGwen20ErZbfQBgBOGpqmHc4j52jHmT8l3zV8+q1jS9oeWAhMSOUYmTbMy8KHV6WpPb8sZLLRV0exTfYphKcpkeWWTjE8/gIvtLiiRPivhqrefKiyqq+yBBPRjOnkR4GrG3TlN9TS3vSKhB4HCIvf0ow8/Kw4BNwc1HfKR2Z9hh3jtpeBWotomf5upSeu7JJFmTVh7cGPUa7pxwq82WS278OWhlb++/VBnYwGykL1hCReFouvqdiqjwlIt2Y9l4nA7hNFGGbSY2Io85xSWJVPrJlU+BCuZHQeauUTwm9ntnrdXU1y7JUXbIC7eRE6LEOagJCBGzJRaaZuLyCSW65vSibXI3ePh3lj+7puEAZ/a8lw1fbcU7UtMDQoqgtTwxKqTCl4hoswDOoojc8AG2VlPfxrFl0/y3J+GyMTMJnR/svOfeWoK4MWgbF3vcH1levkxruOj5T+mPrh7AHvK1XQIYqU8i6kOUxVMJGC1sL+r1ABqcFxDVA68kB+mwzOX1ZWxxZ1B85XeYC60Z2MhYb8u47L/8ZNX+w==
//https://pastebin.com/QmhqYTZd // same as above but shorter
//https://github.com/mebeim/aoc/blob/master/2020/solutions/day23.py si asta explica super ok
// cred ca trebuia sa implementez eu linkedlist-ul ca sa pot sa fac operatiile cum ma taie capul

int main()
{
  list<int> cups;
  char c;
  ifstream f("day23.in");
  while (f >> c)
  {
    cups.push_back(c - '0');
  }
  for (int i = 10; i <= 1000000; i++)
  {
    cups.push_back(i);
  }
  int currentCup = *cups.begin();
  for (int i = 0; i < 10000000; i++)
  {
    if (i % 1000000 == 0)
      cout << i;
    int auxCurrentCup = currentCup;
    list<int>::iterator currentPos = find(cups.begin(), cups.end(), currentCup);
    vector<int> selectedCups;
    if (++currentPos == cups.end())
      currentPos = cups.begin();
    selectedCups.push_back(*currentPos);
    if (++currentPos == cups.end())
      currentPos = cups.begin();
    selectedCups.push_back(*currentPos);
    if (++currentPos == cups.end())
      currentPos = cups.begin();
    selectedCups.push_back(*currentPos);

    //le sterg din lista
    for (int toBeDeleted : selectedCups)
    {
      cups.remove(toBeDeleted);
    }
    //gasesc unde trebuie sa le mut
    if (currentCup == 1)
      currentCup = 1000000;
    while (find(selectedCups.begin(), selectedCups.end(), --currentCup) != selectedCups.end())
    {
      if (currentCup == 1)
        currentCup = 1000000;
    }
    // cout << currentCup;
    //cupele selectate
    list<int>::iterator toBeMovesPos = find(cups.begin(), cups.end(), currentCup);
    // for (int toBeInserted : selectedCups)
    // {
    //   cups.insert(toBeMovesPos, toBeInserted++);
    // }
    toBeMovesPos++;
    cups.insert(toBeMovesPos, {selectedCups[0], selectedCups[1], selectedCups[2]});

    //gasesc next cup
    list<int>::iterator nextCurrentPos = find(cups.begin(), cups.end(), auxCurrentCup);
    if (++nextCurrentPos == cups.end())
    {
      currentCup = *cups.begin();
    }
    else
    {
      currentCup = *(nextCurrentPos);
    }
    // break;
  }
  // list<int>::iterator nextCurrentPos = find(cups.begin(), cups.end(), 2);
  // if (++nextCurrentPos == cups.end())
  //   cout << "Hey";
  // currentCup = *(++nextCurrentPos);
  // cout << "|" << currentCup << "|\n";
  // cups.remove(1);
  // cups.erase(cups.begin())
  list<int>::iterator it = find(cups.begin(), cups.end(), 1);
  cout << *(it++) << endl;
  cout << *(it++) << endl;
  cout << *(it++) << endl;
}
//871369452

//85472963 - too high
//84579263 -- too high
//92786354

//28793654