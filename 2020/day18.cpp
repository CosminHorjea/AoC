#include <bits/stdc++.h>
#define ll unsigned long
using namespace std;

stack<ll> cifre;
stack<char> operators;

void applyOp(char c)
{
  ll n1 = cifre.top();
  cifre.pop();
  ll n2 = cifre.top();
  cifre.pop();
  switch (c)
  {
  case '+':
    cifre.push(n1 + n2);
    break;
  case '*':
    cifre.push(n1 * n2);
    break;
  }
}

//props https://www.reddit.com/r/adventofcode/comments/kfeldk/2020_day_18_solutions/gg868h3?utm_source=share&utm_medium=web2x&context=3
int main()
{
  ifstream f("day18.in");
  string s;
  ll sum = 0;
  while (getline(f, s))
  {
    for (char c : s)
    {
      if (isdigit(c))
        cifre.push(c - '0');
      else if (c == '(')
      {
        operators.push('(');
      }
      else if (c == ')')
      {
        while (operators.size() && operators.top() != '(')
        {
          applyOp(operators.top());
          operators.pop();
        }
        if (operators.top() == '(')
          operators.pop();
      }
      else if (c == '+' || c == '*')
      {
        while (operators.size() && operators.top() != '(' && (c == '*' && operators.top() == '+')) //daca am o inmultire, fac prima data toate adunarile
        {
          applyOp(operators.top());
          operators.pop();
        }
        operators.push(c);
      }
    }
    while (operators.size())
    {
      applyOp(operators.top());
      operators.pop();
    }
    sum += cifre.top();
    cifre.pop();
  }
  cout << sum;
  cout << endl;
}
//low 20074017916
// part 1 12956356593940
//part 2
//low 287643754335