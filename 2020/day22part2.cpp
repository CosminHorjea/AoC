#include <bits/stdc++.h>

using namespace std;

const int deckLen = 25; // 5 - test input 25 -actual input

int doSubGame(deque<int> player1, deque<int> player2)
{
  set<deque<int>> setPlayer1;
  set<deque<int>> setPlayer2;
  while (player1.size() && player2.size())
  {
    int winner;
    if (setPlayer1.find(player1) != setPlayer1.end() || setPlayer2.find(player2) != setPlayer2.end())
    {
      return 1;
    }
    else
    {
      setPlayer1.insert(player1);
      setPlayer2.insert(player2);
    }
    int cardPlayer1 = player1.front();
    int cardPlayer2 = player2.front();
    player1.pop_front();
    player2.pop_front();
    if (cardPlayer1 < player1.size() && cardPlayer2 < player2.size())
    {
      deque<int> subDeck1;
      deque<int> subDeck2;
      copy(player1.begin(), player1.begin() + cardPlayer1, inserter(subDeck1, subDeck1.begin()));
      copy(player2.begin(), player2.begin() + cardPlayer2, inserter(subDeck2, subDeck2.begin()));
      winner = doSubGame(subDeck1, subDeck2);
      if (winner == 1)
      {
        player1.push_back(cardPlayer1);
        player1.push_back(cardPlayer2);
      }
      else if (winner == 2)
      {
        player2.push_back(cardPlayer2);
        player2.push_back(cardPlayer1);
      }
    }
    else
    {

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
  }
  return player1.size() ? 1 : 2;
}

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
  int winnerGame;
  set<deque<int>> setPlayer1;
  set<deque<int>> setPlayer2;
  while (player1.size() && player2.size())
  {
    // cout << "Player 1: ";
    // for (int i : player1)
    // {
    //   cout << i << ' ';
    // }
    // cout << "\n Player 2 :";
    // for (int i : player2)
    // {
    //   cout << i << ' ';
    // }
    // cout << endl;
    if (setPlayer1.find(player1) != setPlayer1.end() || setPlayer2.find(player2) != setPlayer2.end())
    {
      winnerGame = 1;
      break;
    }
    else
    {
      setPlayer1.insert(player1);
      setPlayer2.insert(player2);
    }
    int winner;
    int cardPlayer1 = player1.front();
    int cardPlayer2 = player2.front();
    player1.pop_front();
    player2.pop_front();
    if (cardPlayer1 <= player1.size() && cardPlayer2 <= player2.size())
    {
      deque<int> subDeck1;
      deque<int> subDeck2;
      copy(player1.begin(), player1.begin() + cardPlayer1, inserter(subDeck1, subDeck1.begin()));
      copy(player2.begin(), player2.begin() + cardPlayer2, inserter(subDeck2, subDeck2.begin()));
      winner = doSubGame(subDeck1, subDeck2);
      if (winner == 1)
      {
        player1.push_back(cardPlayer1);
        player1.push_back(cardPlayer2);
      }
      else if (winner == 2)
      {
        player2.push_back(cardPlayer2);
        player2.push_back(cardPlayer1);
      }
    }
    else
    {
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
  }
  // for (int i : player2)
  // {
  //   cout << i << ' ';
  // }
  unsigned long ans = 0;
  if (player1.size() != 0 || winnerGame == 1)
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
  cout << "Ans: " << ans << endl;
  // cout << player2.size() << ":";
  // for (int i : player2)
  //   cout << i << ' ';
  // cout << endl;
  // cout << player1.size() << ":";
  // for (int i : player1)
  //   cout << i << ' ';
}
