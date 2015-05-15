// pile-main.cc
// Main loop for Pile-of-Paper problem
// Matt Olson 2015

#include "pile.h"

#include <cstdio>
#include <set>

int main()
{
    // For fixed-format integer input, scanf feels like the right tool
    // for the job
    int w, h;
    scanf("%d %d", &w, &h);

    auto canvas = Canvas { w, h };

    int c, x, y;
    std::set<int> colors({0});
    while(scanf("%d %d %d %d %d", &c, &x, &y, &w, &h) != EOF) {
        canvas.insert(x, y, w, h, c);
        colors.insert(c); // ignore retval
    }

    auto sum = 0ll;
    for(auto i = colors.begin(); i != colors.end(); i++) {
        printf("%d %lld\n", *i, canvas.colorCount(*i));
        sum += canvas.colorCount(*i);
    }

    printf("Total %lld\n", sum);

    return 0;
}
