// vec2.h
// Declarations for a 2d vector class in Geom
// Matt Olson 2015

#ifndef GEOM2_VECTYPES_H
#define GEOM2_VECTYPES_H

#include "tup3.h"

namespace Geom {

class Pt2;
class Mat2; // 3x3 matrix

//
// Vec2 is a 2d vector <x, y, 0>.  It obeys the usual rules.
//
class Vec2 : public Tup3 {
  public:
    // Again, forcing the user to give two params
    Vec2(float x, float y) : Tup3(x, y, 0) {}
    ~Vec2() = default;
    Vec2(const Vec2&) = default;
    Vec2& operator=(const Vec2&) = default;

    // (Squared) magnitude of the vector
    float sqmag() const;
    float mag() const;

    // Adding vectors and points gives points
    // Note: v += p doesn't make sense; arguably, neither does v + p,
    // but we'll err on the side of not making addition any more
    // noncommutative than we already have by nixing +=.
    Pt2 operator+(const Pt2& p) const;

    Vec2 operator-() const;

    // Adding two vectors gives a vector
    Vec2 operator+(const Vec2& v) const;
    Vec2 operator-(const Vec2& v) const;

    Vec2& operator+=(const Vec2& v);
    Vec2& operator-=(const Vec2& v);

    // We can scale a vector by a scalar
    Vec2 operator*(float n) const;
    Vec2 operator/(float n) const;

    // Not convinced that these should be allowed
    Vec2& operator*=(float n);
    Vec2& operator/=(float n);

    Vec2 operator*(const Mat2& M) const;
};

// Don't need to declare as friend because it uses Vec2's public
// interface
Vec2 operator*(float n, Vec2& t);

// Dot product, considers only x and y.
float dprod(const Vec2& u, const Vec2& v);

// "Cross product"; signed magnitude of the orthogonal vector.  This is
// positive if u->v is counterclockwise and negative if clockwise.
float xprod(const Vec2& u, const Vec2& v);

// Produce a unit-magnitude vector with the same direction as the input
Vec2 normalize(const Vec2& v);

// Produce a unit-length vector corresponding to a given angle
Vec2 anglevec(float theta);

} // namespace Geom

#endif
