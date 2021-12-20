#include <bits/stdc++.h>
using namespace std;

const int width = 10;
const int height = 10;
//only did part 1
//props : https://topaz.github.io/paste/#XQAAAQCIDQAAAAAAAAA4GEiZzRd1JAg3b83u8PjH6J/81KCc6h2MAcMTobmam6gjQ5kNteo392jY6vUFVhjmBrGSDyjB5EGo7008Pd9vLSA0L0FSS844SI1u7u4GRard8iOAEnG99JzwaFreUlq6QGOvPIX3sPjzbFJiQhNae+wF6nLayTk40p+Vv0Res591mIIh4pA1Aq2fflza9B8EaTx1cFcZaqsxr7DnH7CROiNw78EtG6Xg29sMsIuBj1HSiUQlWiEQtC0Jzd6WsKu8N7qz90G3WfJeXdFjfJdUNyG0fdaZf4cGW6bQgK6khB1DoM8Iigu1eg1whHy2ZHpT3oGbSEwluWWvID1mcpipD8aHgJtIKSY3LfIrLT4aywRBrtOaeGJtEXvLpZbRsiHMDBGC+OjVbyQ4fDfR5Bk3ZmCo49J4xRPWvY+wYazlUrtV2EJ67A9+m7gf6i7BM6pHgQPOLppJP1LIBBvdvVGVg7l1myUbh/pvhYELmGdhiq+tVDIajBuJgAK6RQXYs6UYosh1Asy16SrRhEvEVYZ5deC682rXBrCN1THNd+1ysYDUzjTy9yp9Dx50scWNhwdr20IhXK66jJ32Cd5iztFB0pLW9/P80PnRwnRO4ZEb+YYFYbpyDfSbH9ig+0tFs27mhk+1nqGPuE0ZW+8Ces1i9lt/NvGBJehC2rgNhHy/Ya+mkk3GkrcMbtKRvm1GS1OEOEtuoFLse4WL3tffHQvW6IAhAFSjaCfLwTlqhCcOAy/JcFXVZAjvwYoQH/Wo/SAFnSyCGhHfiCTkwGRfzGBhC/DO3mbxaJLIyX9/BU5GZPcpclO4teUhtAOZpQfM9vjjt7aFPAe3y3risiF0/0SvzBKW43Y17DdC1chZWp9ryiNH0ygIIdPqR4VdXl74GZ0NQpb0fYqNxmbi8nIt3CtNV/uSzs87YdL9wz+E8sC9uin5IpD2gZxj7cRDk+vNiqF7NcP+XvCYKnStZh5JwuA1oP+nL7AgixyALZk7FsAg2+WX68M+nJHQUaiosWwyxNzs+vEEeZmdxXn2U0ZN6NRJpbEQ6QnIYQ3gFJ8dLjyqjPdzVewFXl+QKRFtPcUc9UiSW4fbvXWxpSKy4xV2znRLBzjiof8BeS0lxkCuRnNApf55MiEs/slK5IKvGkV1aQirnygCx4W3rcOF50u5gesazO0dwxaHBoXNCfJuwpx1G53FfnRUOBHwOMQc/UdHVJXd+P5/L/s3iIYwjflpMmKazW83XwXJfIi3Qw0SYXdPTrgkccOZTSmQCBufvOl8H09YPEvsXswbGxiYjBSQJ8ZmJ9KEoQeAVP/rdd7A
//      : https://topaz.github.io/paste/#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
//      : https://github.com/zv0n/advent_of_code_2020/blob/master/20/main.cpp
int main()
{
  vector<pair<int, vector<string>>> pieces;
  string s;
  ifstream f("day20.in");
  vector<string> piece;
  int id = 0;
  while (getline(f, s))
  {
    // cout << s << '|';
    if (s[0] == 'T')
    {
      id = stoi(s.substr(s.find(' ') + 1));
    }
    else if (s.length() == 0)
    {
      pieces.push_back({id, piece});
      piece.clear();
    }
    else
    {
      piece.push_back(s);
    }
  }
  map<string, int> freq;
  for (auto p : pieces)
  {
    // cout << "Doing id: " << p.first << endl;
    vector<string> piece = p.second;
    s = "";
    for (int i = 0; i < width; i++)
      s += piece[0][i];
    freq[s]++;
    reverse(s.begin(), s.end());
    freq[s]++;
    s.clear();

    for (int i = 0; i < width; i++)
      s += piece[i][0];
    freq[s]++;
    reverse(s.begin(), s.end());
    freq[s]++;
    s.clear();

    for (int i = 0; i < height; i++)
      s += piece[9][i];
    freq[s]++;
    reverse(s.begin(), s.end());
    freq[s]++;
    s.clear();

    for (int i = 0; i < height; i++)
      s += piece[i][9];
    freq[s]++;
    reverse(s.begin(), s.end());
    freq[s]++;
    s.clear();
  }
  unsigned long ans = 1;
  for (auto p : freq)
  {
    cout << p.first << '-' << p.second << endl;
  }

  for (auto p : pieces)
  {
    int cnt = 0;
    vector<string> piece = p.second;
    s = "";
    for (int i = 0; i < width; i++)
      s += piece[0][i];
    if (freq[s] == 1)
      cnt++;
    reverse(s.begin(), s.end());
    if (freq[s] == 1)
      cnt++;
    s.clear();

    for (int i = 0; i < width; i++)
      s += piece[i][0];
    if (freq[s] == 1)
      cnt++;
    reverse(s.begin(), s.end());
    if (freq[s] == 1)
      cnt++;
    s.clear();

    for (int i = 0; i < height; i++)
      s += piece[9][i];
    if (freq[s] == 1)
      cnt++;
    reverse(s.begin(), s.end());
    if (freq[s] == 1)
      cnt++;
    s.clear();

    for (int i = 0; i < height; i++)
      s += piece[i][9];
    if (freq[s] == 1)
      cnt++;
    reverse(s.begin(), s.end());
    if (freq[s] == 1)
      cnt++;
    s.clear();
    if (cnt > 2)
    {
      cout << p.first << endl;
      ans *= p.first;
    }
  }
  cout << ans << endl;
}