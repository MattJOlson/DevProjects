// vec2.cc
// Matt Olson 2015

#include "pt2.h"
#include "vec2.h"
#include "mat2.h"
#include <cmath>

namespace Geom {

float Vec2::sqmag() const
{
    return x()*x() + y()*y();
}

float Vec2::mag() const
{
    return sqrt(sqmag());
}

Pt2 Vec2::operator+(const Pt2& p) const
{
    return Pt2(x() + p.x(), y() + p.y());
}

Vec2 Vec2::operator-() const
{
    return Vec2(-x(), -y());
}

Vec2 Vec2::operator+(const Vec2& v) const
{
    return Vec2(x() + v.x(), y() + v.y());
}

Vec2 Vec2::operator-(const Vec2& v) const
{
    return Vec2(x() - v.x(), y() - v.y());
}

Vec2& Vec2::operator+=(const Vec2& v)
{
    x(x() + v.x()); y(y() + v.y());
    return *this;
}

Vec2& Vec2::operator-=(const Vec2& v)
{
    x(x() - v.x()); y(y() - v.y());
    return *this;
}

Vec2 Vec2::operator*(float n) const
{
    return Vec2(x()*n, y()*n);
}

// XXX: Not checking for division by zero could be an issue.
Vec2 Vec2::operator/(float n) const
{
    return Vec2(x()/n, y()/n);
}

Vec2& Vec2::operator*=(float n)
{
    x(x() * n); y(y() * n);
    return *this;
}

// XXX: Not checking for division by zero could be an issue.
Vec2& Vec2::operator/=(float n)
{
    x(x() / n); y(y() / n);
    return *this;
}

Vec2 Vec2::operator*(const Mat2& M) const
{
    return M*(*this);
}

// Non-member functions
Vec2 operator*(float n, Vec2& t)
{
    return Vec2(t.x() * n, t.y() * n);
}

float dprod(const Vec2& u, const Vec2& v)
{
    return u.x() * v.x() + u.y() * v.y();
}

float xprod(const Vec2& u, const Vec2& v)
{
    return u.x() * v.y() - v.x() * u.y();
}

Vec2 normalize(const Vec2& v)
{
    return v / v.mag();
}

Vec2 anglevec(float theta)
{
    return Vec2(cos(theta), sin(theta));
}

} // namespace Geom
