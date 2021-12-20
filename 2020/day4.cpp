#include <bits/stdc++.h>

using namespace std;

string required[7] = {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

int validate(map<string, string> passport)
{
  // cout << "validez ceva";
  for (string prop : required)
  {
    if (passport.count(prop) == 0)
      return 0;
  }
  if (passport["byr"].length() != 4 || passport["iyr"].length() != 4)
    return 0;
  if (atoi(passport["byr"].c_str()) < 1920 || atoi(passport["byr"].c_str()) > 2002)
    return 0;
  if (atoi(passport["iyr"].c_str()) < 2010 || atoi(passport["iyr"].c_str()) > 2020)
    return 0;
  if (atoi(passport["eyr"].c_str()) < 2020 || atoi(passport["eyr"].c_str()) > 2030)
    return 0;
  if (passport["hgt"].find("cm") == string::npos && passport["hgt"].find("in") == string::npos)
    return 0;
  if (passport["hgt"].find("cm") != string::npos)
  {
    if (atoi(passport["hgt"].c_str()) < 150 || atoi(passport["hgt"].c_str()) > 193)
      return 0;
  }
  if (passport["hgt"].find("in") != string::npos)
  {
    if (atoi(passport["hgt"].c_str()) < 59 || atoi(passport["hgt"].c_str()) > 76)
      return 0;
  }
  if (passport["hcl"][0] != '#')
    return 0;
  if (passport["hcl"].length() != 7)
    return 0;
  for (int i = 1; i < 7; i++)
  {
    if (passport["hcl"][i] < 'a' || passport["hcl"][i] > 'f')
      if (passport["hcl"][i] < '0' || passport["hcl"][i] > '9')
        return 0;
  }
  set<string> colors = {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
  if (colors.find(passport["ecl"]) == colors.end())
    return 0;
  if (passport["pid"].length() != 9)
    return 0;
  for (char c : passport["pid"])
    if (c < '0' || c > '9')
      return 0;

  return 1;
}

int main()
{
  ifstream f("day4.in");
  int sep, nr = 0;
  string prop, val;
  vector<map<string, string>> passports;
  string currentInfo;
  map<string, string> pass;
  while (getline(f, currentInfo))
  {
    if (currentInfo.size() == 0)
    {
      nr += validate(pass);
      pass.clear();
    }
    int start = 0;
    int end = currentInfo.find(" ");
    while (end != string::npos)
    {
      string p = currentInfo.substr(start, end - start).c_str();
      sep = p.find(':');
      prop = p.substr(0, sep);
      val = p.substr(sep + 1, p.size() - sep);
      // cout << "adaug in pasaport " << prop << " : " << val << endl;
      pass[prop] = val;
      start = end + 1;
      end = currentInfo.find(" ", start);
    }
    string p = currentInfo.substr(start, end - start).c_str();
    sep = p.find(':');
    prop = p.substr(0, sep);
    val = p.substr(sep + 1);
    // cout << "adaug in pasaport " << prop << " : " << val << endl;
    pass[prop] = val;
  }
  nr += validate(pass);
  cout << nr << endl;
}
