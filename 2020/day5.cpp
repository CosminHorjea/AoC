#include <bits/stdc++.h>

using namespace std;

int main()
{
  bool boards[1000] = {0};
  ifstream f("day5.in");
  string boardingPass;
  int maxBoard = 0;
  while (f >> boardingPass)
  {
    int lowRow = 0, highRow = 127;
    int lowHalf = 0, highHalf = 7;
    for (int i = 0; i < 7; i++)
    {
      if (boardingPass[i] == 'F')
      {
        highRow = (highRow + lowRow) / 2;
      }
      else
      {
        lowRow = (lowRow + highRow) / 2 + 1;
      }
    }
    for (int i = 7; i < 10; i++)
    {
      if (boardingPass[i] == 'R')
      {
        lowHalf = (lowHalf + highHalf) / 2 + 1;
      }
      else
      {
        highHalf = (lowHalf + highHalf) / 2;
      }
    }
    int seatID = lowRow * 8 + highHalf;
    boards[seatID] = 1;
    maxBoard = max(seatID, maxBoard);
  }
  for (int i = 1; i < 999; i++)
  {
    if (!boards[i])
    {
      if (boards[i - 1] && boards[i + 1])
        cout << i << endl;
    }
  }
}