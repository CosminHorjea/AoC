#include <bits/stdc++.h>

using namespace std;

ifstream f("in.txt");

vector<string> split(string s, char delimiter = ' ') {
    vector<string> tokens;
    string token;
    istringstream tokenStream(s);
    while (getline(tokenStream, token, delimiter)) {
        tokens.push_back(token);
    }
    return tokens;
}

int main() {
    set<string> cities;
    vector<string> citiesC;
    string line;
    map<string, map<string, int>> distances;
    while (getline(f, line)) {
        vector<string> tokens = split(line);
        if (cities.find(tokens[0]) == cities.end()) {
            cities.insert(tokens[0]);
            citiesC.push_back(tokens[0]);
        }
        if (cities.find(tokens[2]) == cities.end()) {
            cities.insert(tokens[2]);
            citiesC.push_back(tokens[2]);
        }
        distances[tokens[0]][tokens[2]] = stoi(tokens[4]);
        distances[tokens[2]][tokens[0]] = stoi(tokens[4]);
    }
    f.close();
    sort(citiesC.begin(), citiesC.end());
    // print cities
    // for (int i = 0; i < citiesC.size(); i++) {
    //     cout << citiesC[i] << " ";
    // }
    int ans = -INT_MAX;
    int iter = 0;
    do {
        iter++;
        int sum = 0;
        for (int i = 0; i < citiesC.size() - 1; i++) {
            sum += distances[citiesC[i]][citiesC[i + 1]];
        }
        if (sum > ans) {
            ans = sum;
        }
    } while (next_permutation(citiesC.begin(), citiesC.end()));
    cout << "Ans: " << ans << endl;
    cout << iter;
}

// too high 4504
// 284 too high
