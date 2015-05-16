// pile.cc
// Solution to r/dailyprogrammer's Pile Of Paper challenge
// http://www.reddit.com/r/dailyprogrammer/comments/35s2ds/20150513_challenge_214_intermediate_pile_of_paper/
// Matt Olson 2015

#include "pile.h"

#include <algorithm>

IntervalList insertInterval(IntervalList& base, Interval i)
{
    // Brittle; we assume that i.start() is contained in the base
    // interval list
    auto first_inter =
        std::find_if(base.begin(), base.end(),
                     [&](Interval j) { return j.contains(i.start()); });

    auto result = IntervalList { base.begin(), first_inter };

    if(first_inter->start() != i.start()) {
        // Add leading fragment of first_inter
        auto len = i.start() - first_inter->start();
        result.push_back(Interval {first_inter->start(),
                                   len,
                                   first_inter->color()});
    }

    result.push_back(i);

    auto last_inter =
        std::find_if(base.begin(), base.end(),
                     [&](Interval j) { return j.contains(i.end()); });

    if(i.end() != last_inter->end()) {
        // Add trailing fragment of last_inter
        auto len = last_inter->end() - i.end();
        result.push_back(Interval {i.end()+1,
                                   len,
                                   last_inter->color()});
    }

    ++last_inter;
    result.insert(result.end(), last_inter, base.end());

    return result;
}

#include <cstdio>

void Scanline::insert(Interval i)
{
    auto start = std::max(i.start(), 0);
    auto end = std::min(i.end(), length_-1);
    auto length = end - start + 1;

    intervals_ = insertInterval(intervals_, {start, length, i.color()});
}

long long Scanline::colorCount(int which) const
{
    auto ct = 0ll;

    for(auto i : intervals_) {
        if(i.color() == which) ct += i.length();
    }

    return ct;
}

long long Canvas::colorCount(int which) const
{
    auto ct = 0ll;

    for(auto l : scanlines_) { ct += l.colorCount(which); }

    return ct;
}

void Canvas::insert(int x, int y, int width, int height, int color)
{
    for(auto i = 0; i < height; i++) {
        auto l = y + i;
        if((l < 0) || (height_ <= l)) continue;
        scanlines_[l].insert({x, width, color});
    }
}
