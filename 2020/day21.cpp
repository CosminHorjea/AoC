#include <bits/stdc++.h>

using namespace std;
//part 1 was soooo over engeniiered but it was so easy to solve part two this way
set<string> splitString(string s, string delimiter)
{
  set<string> words;

  size_t pos = 0;
  std::string token;
  while ((pos = s.find(delimiter)) != std::string::npos)
  {
    token = s.substr(0, pos);
    words.insert(token);
    s.erase(0, pos + delimiter.length());
  }
  words.insert(s);
  return words;
}

int main()
{
  ifstream f("day21.in");
  string s;
  vector<string> allFoods;
  set<pair<string, string>> allergenSorted;
  set<string> allergicFoods;
  map<string, int> seenIngredients;
  // vector<vector<string>> food;
  // vector<vector<string>> alergies;
  map<string, vector<set<string>>> ingredientToFoods;
  while (getline(f, s))
  {
    string foodString = s.substr(0, s.find(" (contains"));
    set<string> foods = splitString(foodString, " ");
    // cout << "foods:";
    for (auto food : foods)
    {
      allFoods.push_back(food);
    }
    if (s.find(" (contains") == string::npos)
    {
      continue;
    }
    string ingredientString = s.substr(s.find("(contains ") + 10);
    set<string> ingredients = splitString(ingredientString, ", ");
    for (auto &ingredient : ingredients)
    {
      if (ingredient[ingredient.length() - 1] == ')')
      {
        string ing = ingredient.substr(0, ingredient.size() - 1);
        ingredients.erase(ingredient);
        ingredients.insert(ing);
      }
      // cout << '|' << ingredient << '|';
    }
    for (auto p : ingredients)
    {
      ingredientToFoods[p].push_back(foods);
    }
    // cout << endl;
  }
  int amSters = 1;
  while (amSters)
  {
    amSters = 0;
    for (auto &p : ingredientToFoods)
    {
      map<string, int> countFoods;
      for (auto vecFoods : p.second)
      {
        for (auto food : vecFoods)
        {
          countFoods[food]++;
        }
      }
      int cntMax = 0;
      string foodWithIngredient;
      for (auto freq : countFoods)
      {
        if (freq.second == p.second.size())
        {
          cntMax++;
          foodWithIngredient = freq.first;
        }
      }
      if (cntMax == 1 && seenIngredients[p.first] == 0)
      {
        allergicFoods.insert(foodWithIngredient);
        allergenSorted.insert({p.first, foodWithIngredient});
        // foodWithIngredient << '\n';
        seenIngredients[p.first] = 1;
        for (auto &p2 : ingredientToFoods)
        {
          for (auto &vecFoods : p2.second)
          {
            vecFoods.erase(foodWithIngredient);
            amSters = 1;
          }
        }
      }
    }
  }

  int ans = 0;
  // for (auto p : ingredientToFoods)
  // {
  //   cout << p.first << " : ";
  //   for (auto vec : p.second)
  //   {
  //     cout << vec.size();
  //     ans += vec.size();
  //     cout << '[';
  //     for (auto food : vec)
  //     {
  //       cout << ' ' << food << ' ';
  //     }
  //     cout << ']';
  //   }
  //   cout << endl;
  // }
  for (auto food : allFoods)
  {
    // cout << food << " ";
    if (allergicFoods.find(food) == allergicFoods.end())
      ans++;
  }
  for (auto p : allergenSorted)
  {
    cout << p.second << ',';
  }
  cout << ans;
}
