#include <bits/stdc++.h>
#define ul unsigned long
using namespace std;

int noLoops(int key, int subjectNumber = 7)
{
  ul sN = 1;
  int i = 0;
  while (sN != key)
  {
    sN = sN * subjectNumber;
    sN %= 20201227;
    i++;
  }
  return i;
}

int main()
{
  ifstream f("day25.in");

  int cardKey;
  int doorKey;
  f >> cardKey >> doorKey;
  int cardLoop = noLoops(cardKey);
  int doorLoop = noLoops(doorKey);

  cout << cardLoop << " " << doorLoop << endl;
  ul publicKey = 1;
  for (int i = 0; i < doorLoop; i++)
  {
    publicKey = publicKey * cardKey;
    publicKey %= 20201227;
  }
  cout << publicKey;
}