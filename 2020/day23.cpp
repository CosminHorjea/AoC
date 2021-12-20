#include <bits/stdc++.h>

using namespace std;

int main()
{
  list<int> cups;
  char c;
  ifstream f("day23.in");
  while (f >> c)
  {
    cups.push_back(c - '0');
  }
  for (int i : cups)
    cout << i << ' ';
  cout << endl;
  int currentCup = *cups.begin();
  for (int i = 0; i < 100; i++)
  {
    cout << "Tura :" << i + 1 << endl;
    cout << "Selected: " << currentCup << endl;
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

    for (int aux : cups)
      cout << aux << ' ';
    cout << endl;
    for (int aux : selectedCups)
      cout << aux << ' ';
    cout << endl;
    //le sterg din lista
    for (int toBeDeleted : selectedCups)
    {
      cups.remove(toBeDeleted);
    }
    //gasesc unde trebuie sa le mut
    if (currentCup == 1)
      currentCup = 10;
    while (find(selectedCups.begin(), selectedCups.end(), --currentCup) != selectedCups.end())
    {
      cout << "Incerc cupa " << currentCup << endl;
      if (currentCup == 1)
        currentCup = 10;
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
    for (int aux : cups)
      cout << aux << " ";
    cout << endl;
    // break;
  }
  // list<int>::iterator nextCurrentPos = find(cups.begin(), cups.end(), 2);
  // if (++nextCurrentPos == cups.end())
  //   cout << "Hey";
  // currentCup = *(++nextCurrentPos);
  // cout << "|" << currentCup << "|\n";
  // cups.remove(1);
  // cups.erase(cups.begin())
  for (int i : cups)
    cout << i << ' ';
  cout << endl;
}
//871369452

//85472963 - too high
//84579263 -- too high
//92786354

//28793654