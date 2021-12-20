#include <bits/stdc++.h>
#define ll unsigned long
using namespace std;

// ll computeVal(string val, string mask) // part 1
// {
//   bitset<36> bits(stoul(val));
//   // cout << mask.length();
//   for (int i = 0; i < mask.length(); i++)
//   {
//     // cout << 25 - i << endl;
//     if (mask[i] == '0')
//       bits.set(35 - i, 0);
//     if (mask[i] == '1')
//       bits.set(35 - i, 1);
//   }
//   // cout << bits << endl;
//   return bits.to_ulong();
//   // return 1;
// }

//part 2
long bin2Dec(string num)
{
  long dec = 0;
  for (int i = 35; i >= 0; i--)
    if (num[i] == '1')
      dec += pow(2, 35 - i);
  return dec;
}
void assign2(string bits, string val, map<ll, ll> &mem)
{
  int floatingIndex = bits.find("X");
  if (floatingIndex == string::npos)
  {
    // cout << bits << endl;
    // cout << "Pun pe pozitia " << bin2Dec(bits) << " valoarea " << val << endl;
    mem[bin2Dec(bits)] = stoi(val);
    return;
  }
  bits[floatingIndex] = '0';
  assign2(bits, val, mem);
  bits[floatingIndex] = '1';
  assign2(bits, val, mem);
}
ll assign(int pos, string val, string mask, map<ll, ll> &mem) // part 1
{
  bitset<36> bits(pos);
  string bitss = bits.to_string();
  // cout << bitss;
  for (int i = 0; i < 36; i++)
  {
    if (mask[i] == '0')
      continue;
    bitss[i] = mask[i];
  }
  assign2(bitss, val, mem);
}

int main()
{
  ifstream f("day14.in");
  string s;
  map<ll, ll> mem;
  string mask;
  while (getline(f, s))
  {
    stringstream ss(s);
    string s1, s2, s3;
    ss >> s1 >> s2 >> s3;
    string::size_type sz;
    if (s1 == "mask")
      mask = s3;
    else
    {
      int pos1 = s1.find("[");
      int pos2 = s1.find("]");
      int nr = stoi(s1.substr(pos1 + 1, pos2 - pos1 - 1), nullptr, 10);
      // mem[nr] = computeVal(s3, mask); //part1
      assign(nr, s3, mask, mem);
    }
  }
  ll sum = 0;
  for (auto p : mem)
  {
    if (p.second > 0)
      sum += p.second;
  }
  cout << sum << endl;
}