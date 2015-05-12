// stdev.cc
// Calculate the standard deviation of a set of inputs
// http://www.reddit.com/r/dailyprogrammer/comments/35l5eo/20150511_challenge_214_easy_calculating_the/
// Matt Olson 2015

#include <algorithm>
#include <cmath>
#include <functional>
#include <iostream>
#include <vector>

double avg(std::vector<int>& ints, std::function<int(int)> f)
{
    double sum = 0;
    for(auto x : ints) { sum += f(x); }
    return sum / ints.size();
}

double mean(std::vector<int>& ints)
{
    return avg(ints, [](int n) { return n; });
}

double var(std::vector<int>& ints)
{
    auto m = mean(ints);
    return avg(ints, [m](int n) { return (n-m)*(n-m); });
}

int main() {
    std::vector<int> ints;
    int temp;

    while(std::cin >> temp) { ints.push_back(temp); }

    std::cout << sqrt(var(ints)) << std::endl;

    return 0;
}
