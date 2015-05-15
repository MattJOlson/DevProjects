// pile.h
// Header for Pile Of Paper
// Matt Olson 2015

#ifndef PILE_H
#define PILE_H

#include <vector>

class Interval {
  public:
    Interval(int start, int length, int color = 0) :
        start_(start),
        length_(length),
        color_(color)
    {}
    // rest can be defaults

    int start() const { return start_; }
    int end() const { return start_ + length_ - 1; }
    int length() const { return length_; }
    int color() const { return color_; }

    bool contains(int i) const { 
        return (start_ <= i) && (i <= end());
    }
  private:
    int start_;
    int length_;
    int color_;
};

using IntervalList = std::vector<Interval>;

IntervalList insertInterval(IntervalList& base, Interval i);


class Scanline {
  public:
    Scanline(int length) : 
        length_(length),
        intervals_({ {0, length, basecolor_} })
    {}

    IntervalList& intervals() { return intervals_; }

    void insert(Interval i);
    long long colorCount(int which) const;
  private:
    const int basecolor_ = 0;
    int length_;
    IntervalList intervals_;
};

class Canvas {
  public:
    Canvas(int width, int height) : 
        width_(width),
        height_(height),
        scanlines_(std::vector<Scanline>(height, {width}))
    {}

    int width() const { return width_; }
    int height() const { return height_; }
    long long colorCount(int which) const;
    void insert(int x, int y, int width, int height, int color);
  private:
    int width_;
    int height_;
    std::vector<Scanline> scanlines_;
};

#endif
