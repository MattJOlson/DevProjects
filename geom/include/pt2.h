// pt2.h
// Declaration for 2d point class in Geom
// Matt Olson 2015

#ifndef PT2_H
#define PT2_H

#include "tup3.h"

namespace Geom {

class Vec2;
class Mat2;

// Pt2 is a 2d point <x, y, 1>.  It obeys the following rules:
//
// * Points can be converted to vectors relative to a given anchor (by
//   default, the origin)
// * Vectors and points can be added, and vectors can be subtracted from
//   points
// * Pre- or post-multiplying a point by a matrix returns a point; so
//   for example p*M == M*p and p1*M*p2 is invalid (p1*M is a Pt2, which
//   can't be multiplied by p2).

class Pt2 : public Tup3 {
  public:
    // Let's force the user to give two params
    Pt2(float x, float y) : Tup3(x, y, 1) {}
    ~Pt2() = default;
    Pt2(const Pt2&) = default;
    Pt2& operator=(const Pt2&) = default;

    Vec2 toVec(Pt2 o = {0,0}) const;

    Pt2 operator+(const Vec2& v) const;
    Pt2 operator-(const Vec2& v) const;

    // Not convinced that these should be allowed
    Pt2& operator+=(const Vec2& v);
    Pt2& operator-=(const Vec2& v);

    Pt2 operator*(const Mat2& M) const;
// pretty convinced that p *= M is gross, but usage might show otherwise
};

// Point-to-point distance functions
float sqdist(const Pt2& u, const Pt2& v);

float dist(const Pt2& u, const Pt2& v);

// Interpolation
Pt2 lerp(const Pt2& u, const Pt2& v, float t);

} // namespace Geom

#endif
