#include <bits/stdc++.h>

using namespace std;

int mod(int a, int b)
{
  int r = a % b;
  return r < 0 ? r + b : r;
}

int main()
{
  int x = 0, y = 0;
  int dir[4] = {0, 1, 2, 3}; //E,S,W,N
  int forward = 0;
  ifstream f("day12.in");
  char action;
  int value;
  // part 1
  // while (f >> action >> value)
  // {
  //   // cout << action << "-" << value << endl;
  //   switch (action)
  //   {
  //   case 'F':
  //     switch (dir[forward])
  //     {
  //     case 0:
  //       x += value;
  //       break;
  //     case 1:
  //       y -= value;
  //       break;
  //     case 2:
  //       x -= value;
  //       break;
  //     case 3:
  //       y += value;
  //       break;
  //     default:
  //       break;
  //     }
  //     break;
  //   case 'N':
  //     y += value;
  //     break;
  //   case 'E':
  //     x += value;
  //     break;
  //   case 'S':
  //     y -= value;
  //     break;
  //   case 'W':
  //     x -= value;
  //     break;
  //   case 'R':
  //     forward += (value) / 90;
  //     forward %= 4;
  //     break;
  //   case 'L':
  //     forward -= (value) / 90;
  //     forward = mod(forward, 4);
  //     break;
  //   default:
  //     break;
  //   }
  // }
  int wayPointX = 10, wayPointY = 1;
  int aux;
  while (f >> action >> value)
  {
    switch (action)
    {
    case 'F':
      for (int i = 0; i < value; i++)
      {
        x += wayPointX;
        y += wayPointY;
      }
      break;
    case 'N':
      wayPointY += value;
      break;
    case 'E':
      wayPointX += value;
      break;
    case 'S':
      wayPointY -= value;
      break;
    case 'W':
      wayPointX -= value;
      break;
    case 'R':

      switch ((value) / 90)
      {
      case 1:
        aux = wayPointX;
        wayPointX = wayPointY;
        wayPointY = -aux;
        break;
      case 2:
        aux = wayPointX;
        wayPointX = -wayPointX;
        wayPointY = -wayPointY;
        break;
      case 3:
        aux = wayPointX;
        wayPointX = -wayPointY;
        wayPointY = aux;
        break;
      }

      break;
    case 'L':

      switch ((value) / 90)
      {
      case 1:
        aux = wayPointX;
        wayPointX = -wayPointY;
        wayPointY = aux;
        break;
      case 2:
        aux = wayPointX;
        wayPointX *= -1;
        wayPointY *= -1;
        break;
      case 3:
        aux = wayPointX;
        wayPointX = wayPointY;
        wayPointY = -aux;
        break;
      }

      break;
    default:
      break;
    }
  }
  cout << abs(x) + abs(y) << endl;
}