// pile.h
// Header for Pile Of Paper
// Matt Olson 2015

#ifndef PILE_H
#define PILE_H

#include <vector>

class Interval {
  public:
    Interval(int start, int length) : start_(start), length_(length) {}
    // rest can be defaults

    int start() const { return start_; }
    int end() const { return start_ + length_; }
    int length() const { return length_; }
    bool contains(int i) const { 
        return (start_ <= i) && (i <= start_ + length_);
    }
  private:
    int start_;
    int length_;
};

using IntervalList = std::vector<Interval>;

IntervalList insertInterval(IntervalList& base, Interval i);

#endif
