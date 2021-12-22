#include <bits/stdc++.h>

using namespace std;

bool verify(int low, int high, char c, string s)
{
  int nr = 0;
  for (char aux : s)
  {
    if (aux == c)
      nr++;
  }
  if (nr >= low && nr <= high)
    return true;
  return false;
}
bool verify2(int firstIndex, int secondIndex, char c, string s)
{

  if (s[firstIndex - 1] == c && s[secondIndex - 1] != c)
    return true;
  if (s[firstIndex - 1] != c && s[secondIndex - 1] == c)
    return true;
  return false;
}

int main()
{
  int nr = 0;
  string s;
  ifstream f("day2.in");
  while (getline(f, s))
  {

    stringstream ss(s);
    int low, high;
    char c, aux;
    string actual;
    ss >> low >> aux >> high >> c >> aux >> actual;
    if (verify2(low, high, c, actual))
      nr++;
  }
  cout << nr;
}