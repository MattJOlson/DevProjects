// pile.cc
// Solution to r/dailyprogrammer's Pile Of Paper challenge
// http://www.reddit.com/r/dailyprogrammer/comments/35s2ds/20150513_challenge_214_intermediate_pile_of_paper/
// Matt Olson 2015

#include "pile.h"

#include <algorithm>

IntervalList insertInterval(IntervalList& base, Interval i)
{

    auto first_inter =
        std::find_if(base.begin(), base.end(),
                     [&](Interval j) { return j.contains(i.start()); });

    auto result = IntervalList { base.begin(), first_inter };

    if(first_inter->start() != i.start()) {
        // Add leading fragment of first_inter
        auto len = i.start() - first_inter->start();
        result.push_back(Interval {first_inter->start(), len});
    }

    result.push_back(i);

    auto end = i.start() + i.length();

    auto last_inter =
        std::find_if(base.begin(), base.end(),
                     [&](Interval j) { return j.contains(end); });

    auto last_end = last_inter->start() + last_inter->length();
    if(end != last_end) {
        // Add trailing fragment of last_inter
        auto len = last_end - end;
        result.push_back(Interval {end, len});
    }

    ++last_inter;
    result.insert(result.end(), last_inter, base.end());

    return result;
}
