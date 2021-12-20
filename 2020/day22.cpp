#include <bits/stdc++.h>

using namespace std;

const int deckLen = 25;

int main()
{
  ifstream f("day22.in");
  deque<int> player1;
  deque<int> player2;
  string s;
  int x;
  getline(f, s);
  for (int i = 0; i < deckLen; i++)
  {
    f >> x;
    player1.push_back(x);
  }
  f >> s;
  f >> s;
  for (int i = 0; i < deckLen; i++)
  {
    f >> x;
    player2.push_back(x);
  }
  while (player1.size() && player2.size())
  {
    int cardPlayer1 = player1.front();
    int cardPlayer2 = player2.front();
    player1.pop_front();
    player2.pop_front();
    if (cardPlayer1 > cardPlayer2)
    {
      player1.push_back(cardPlayer1);
      player1.push_back(cardPlayer2);
    }
    else
    {
      player2.push_back(cardPlayer2);
      player2.push_back(cardPlayer1);
    }
  }
  // for (int i : player2)
  // {
  //   cout << i << ' ';
  // }
  unsigned long ans = 0;
  if (player1.size())
  {
    int i = player1.size();
    for (int card : player1)
    {
      ans += card * (i--);
    }
  }
  else
  {
    int i = player2.size();
    for (int card : player2)
    {
      ans += card * (i--);
    }
  }
  cout << ans << endl;
}
