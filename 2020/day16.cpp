#include <bits/stdc++.h>
#define ll unsigned long
using namespace std;

int verifyNumber(int n, vector<pair<int, int>> &valid)
{
  int ok = 0;
  for (auto interval : valid)
  {
    if (interval.first <= n && interval.second >= n)
      ok = 1;
  }
  return ok;
}
bool gasescNealocat(map<string, int> &categories);
bool isCorrect(int valOnTicket, int poz, vector<pair<int, int>> &validValues);
int main()
{
  ifstream f("day16.in");
  string fileLine;
  vector<pair<int, int>> validValues;
  vector<vector<int>> validTickets;
  vector<int> myTicket;
  map<string, int> categories;
  while (getline(f, fileLine))
  {
    if (fileLine.length() == 0)
      break;
    string categ;
    int low, high;
    string aux;
    categ = fileLine.substr(0, fileLine.find(':'));
    categories[categ] = -1;
    stringstream ss(fileLine.substr(fileLine.find(':')));
    ss >> categ >> low >> high;
    high = -high;
    validValues.push_back({low, high});
    ss >> aux >> low >> high;
    validValues.push_back({low, -high});
  }
  getline(f, fileLine);
  getline(f, fileLine);
  int start = 0;
  int end = fileLine.find(',');
  while (end != string::npos)
  {
    const char *nr = fileLine.substr(start, end - start).c_str();
    int number = atoi(nr);
    myTicket.push_back(number);
    start = end + 1;
    end = fileLine.find(',', start);
  }
  const char *nr = fileLine.substr(start, end - start).c_str();
  myTicket.push_back(atoi(nr));
  int scanningError = 0;
  getline(f, fileLine);
  while (getline(f, fileLine))
  {
    vector<int> validTicket;
    start = 0;
    end = fileLine.find(',');
    while (end != string::npos)
    {
      const char *nr = fileLine.substr(start, end - start).c_str();
      int number = atoi(nr);
      if (!verifyNumber(number, validValues))
      {
        scanningError += number;
      }
      else
      {
        validTicket.push_back(number);
      }
      start = end + 1;
      end = fileLine.find(',', start);
    }
    const char *nr = fileLine.substr(start, end - start).c_str();
    int number = atoi(nr);
    if (!verifyNumber(number, validValues))
    {
      scanningError += number;
    }
    else
    {
      validTicket.push_back(number);
    }
    if (!scanningError)
      validTickets.push_back(validTicket);
    scanningError = 0;
  }
  validTickets.erase(validTickets.begin());
  //valid tickets
  //validValues
  //categories
  // It's seems like a lot of code to write and i can't make sense of a solution yet, i found a solution online and submitted it,
  // but i might come back to this one sometime
  //Idee: iei toate numerele de pe o coloana din ticketele valide
  // faci un map[string] = valorileValide
  // mergi cu fiecare valoare din alea valide prin map
  // daca un numar nu se corespune valorilor alea din map
  // scot aia din map(asta inseamana sa resetez map-ul la fiecare element, is ok)
  //daca la final map-ul are un singur element, inseamna ca am gasit coloana
  // pun in map-ul de acum pozitia coloanei la care ma aflu in tichetele valide la cheia din map-ul cu valori valide
  //data viitoare cand fac map-ul sa fiu atent sa nu mai includ coloana aia
  // chestia cu while(gasescNealocat ramane la fel)
  while (gasescNealocat(categories))
  {
    int possible = 0;
    vector<int> valuesOnColumnI;
    for (int i = 0; i < 20; i++)
    {
      for (auto t : validTickets)
        valuesOnColumnI.push_back(t[i]);
      int totalCols = 0;
      for (int j = 0; j < 20; j++)
      {
        int validCols = 0;
        for (int k : valuesOnColumnI)
        {
          if (isCorrect(k, j, validValues))
          {
            validCols++;
          }
        }
        if (validCols == 20)
        {
          totalCols++;
        }
        validCols = 0;
      }
      if (totalCols == 1)
      {
      }
    }
  }
  cout << endl;
}

bool gasescNealocat(map<string, int> &categories)
{
  for (auto &p : categories)
  {
    if (p.second == -1)
      return true;
  }
  return false;
}
bool isCorrect(int valOnTicket, int poz, vector<pair<int, int>> &validValues)
{
  if (valOnTicket >= validValues[poz * 2].first && valOnTicket <= validValues[poz * 2].second)
    return true;
  if (valOnTicket >= validValues[poz * 2 + 1].first && valOnTicket <= validValues[poz * 2 + 1].second)
    return true;
  return false;
}

//514662805187