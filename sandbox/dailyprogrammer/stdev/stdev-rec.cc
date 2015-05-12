// stdev-rec.cc
// Recursive single-pass solution
// Matt Olson 2015


#include <algorithm>
#include <cmath>
#include <functional>
#include <iterator>
#include <iostream>
#include <vector>

double varsum(std::vector<int>& ints, size_t i, double& flex)
{
    // stop at the end and calc mean
    if(ints.size() == i) { flex /= i; return 0; }

    flex += ints[i]; // recursing down, flex is sum
    auto v = varsum(ints, i+1, flex);
    return v + (ints[i]-flex)*(ints[i]-flex); // going up, flex is mean
}

int main() {
    std::vector<int> ints(std::istream_iterator<int>(std::cin),
                          std::istream_iterator<int>());

    double flex;
    std::cout << sqrt(varsum(ints, 0, flex)/ints.size()) << std::endl;

    return 0;
}
