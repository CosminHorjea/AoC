#include <bits/stdc++.h>

using namespace std;

int runInstructions(vector<pair<string, int>> instructions)
{
  int acc = 0;
  int visited[instructions.size() + 1] = {0};
  for (int i = 0; i < instructions.size(); i++)
  {
    if (visited[i])
    {
      return 0;
    }
    visited[i] = 1;
    if (instructions[i].first == "acc")
    {
      acc += instructions[i].second;
    }
    else if (instructions[i].first == "jmp")
    {
      i += instructions[i].second - 1;
    }
  }
  return acc;
}

void printInstructions(vector<pair<string, int>> ins)
{
  for (auto p : ins)
  {
    cout << p.first << " " << p.second << endl;
  }
}

int main()
{
  ifstream f("day8.in");
  string instruction;
  int value;
  vector<pair<string, int>> instructions, correctInstructions;
  while (f >> instruction >> value)
  {
    instructions.push_back({instruction, value});
  }
  // printInstructions(instructions);
  // cout << runInstructions(instructions) << endl;
  correctInstructions = instructions;
  for (int i = 0; i < instructions.size(); i++)
  {
    string instruction = instructions[i].first;
    int value = instructions[i].second;
    if (instruction == "jmp")
      correctInstructions[i].first = "nop";
    if (instruction == "nop")
      correctInstructions[i].first = "jmp";
    if (runInstructions(correctInstructions))
      cout << runInstructions(correctInstructions) << endl;
    correctInstructions = instructions;
  }
}