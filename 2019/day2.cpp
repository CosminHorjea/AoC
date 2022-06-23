#include <bits/stdc++.h>

using namespace std;

int main()
{
  ifstream f("day2.in");
  vector<int> cmds;
  int x;
  while (f >> x)
  {
    if (x != ',')
      cmds.push_back(x);
  }
  long long ans = 0;
  int i1 = 0, i2 = 0;
  vector<int> oldCmds = cmds;
  cmds[1] = 12;
  cmds[2] = 2;
  for (i1 = 0; i1 < 100; i1++)
  {
    for (i2 = 0; i2 < 100; i2++)
    {
      cmds[1] = i1;
      cmds[2] = i2;
      cout << cmds[1] << " " << cmds[2] << " : ";
      for (int i = 0; i < cmds.size(); i = i + 4)
      {
        int cmd = cmds[i];
        if (cmd == 99)
        {
          // cout << "HALTED " << cmds[0] << endl;
          cout << cmds[0] << endl;
          if (cmds[0] == 19690720)
            return 1;
          cmds = oldCmds;
          break;
        }

        int index1, index2, endPos;
        index1 = cmds[i + 1];
        index2 = cmds[i + 2];
        endPos = cmds[i + 3];
        if (index1 >= cmds.size() || index2 >= cmds.size() || endPos >= cmds.size())
          break;
        if (cmd == 1)
        {
          cmds[endPos] = cmds[index1] + cmds[index2];
        }
        else if (cmd == 2)
        {
          cmds[endPos] = cmds[index1] * cmds[index2];
        }
      }
    }
  }

  // for (int i : cmds)
  // {
  //   cout << i << " ";
  // }
  cout << endl;
}