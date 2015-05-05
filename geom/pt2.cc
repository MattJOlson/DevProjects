// pt2.cc
// Definitions for 2d point class in Geom
// Matt Olson 2015

#include "pt2.h"
#include "vec2.h"
#include "mat2.h"

namespace Geom {

// Pt2 implementation stuff
Vec2 Pt2::toVec(Pt2 o) const
{
    return Vec2(x() - o.x(), y() - o.y());
}

Pt2 Pt2::operator+(const Vec2& v) const
{
    return Pt2(x() + v.x(), y() + v.y());
}
Pt2 Pt2::operator-(const Vec2& v) const
{
    return Pt2(x() - v.x(), y() - v.y());
}

Pt2& Pt2::operator+=(const Vec2& v)
{
    x(x() + v.x()); y(y() + v.y());
    return *this;
}
Pt2& Pt2::operator-=(const Vec2& v)
{
    x(x() - v.x()); y(y() - v.y());
    return *this;
}
Pt2 Pt2::operator*(const Mat2& M) const
{
    return M*(*this);
}

//    Pt2 Pt2::operator*(const Mat3& M) { return M*(*this); }

float sqdist(const Pt2& u, const Pt2& v)
{
    return u.toVec(v).sqmag();
}

float dist(const Pt2& u, const Pt2& v)
{
    return u.toVec(v).mag();
}

Pt2 lerp(const Pt2& u, const Pt2& v, float t)
{
    auto x = u.x() * (1-t) + v.x() * t;
    auto y = u.y() * (1-t) + v.y() * t;

    return Pt2 {x, y};
}

} // namespace Geom
