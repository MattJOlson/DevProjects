// mat2.cc
// Implementation of Mat2 matrix class

#include "mat2.h"

#include <cmath>
#include <cstdio>

namespace Geom {

Tup3 Mat2::operator[](int r) const
{
    // Man, bounds-checking really doesn't have a good solution here
    //  besides throwing an exception.  For now we'll return the zero
    //  vector and hope for the best.
    if((r < 0) || (2 < r)) return Tup3{0,0,0};

    return row(r);
}

Tup3 Mat2::row(int r) const
{
    // Recall that M_ is column-major
    return Tup3{M_[r], M_[3+r], M_[6+r]};
}

Tup3 Mat2::col(int c) const
{
    return Tup3{M_[3*c], M_[3*c+1], M_[3*c+2]};
}

// Point transform
Pt2 Mat2::operator*(const Pt2& p) const
{
    auto x = Tup3 { sprod(row(0), p),
                    sprod(row(1), p),
                    sprod(row(2), p) };

    // XXX: Possible divide by zero
    return Pt2(x[0]/x[2], x[1]/x[2]);
}

Vec2 Mat2::operator*(const Vec2& v) const
{
    auto x = Tup3 { sprod(row(0), v),
                    sprod(row(1), v),
                    sprod(row(2), v) };

    // Ignore x[2] for now; this will be wrong if *this is a shear
    // transform but for the moment we're assuming rigid xforms only
    return Vec2(x[0], x[1]);
}

Mat2 Mat2::operator*(const Mat2& M) const
{
    return Mat2 {
        // Each triplet is a row
        sprod(row(0), M.col(0)),
        sprod(row(0), M.col(1)),
        sprod(row(0), M.col(2)),

        sprod(row(1), M.col(0)),
        sprod(row(1), M.col(1)),
        sprod(row(1), M.col(2)),

        sprod(row(2), M.col(0)),
        sprod(row(2), M.col(1)),
        sprod(row(2), M.col(2)),
    };
}

Mat2 Mat2::transpose() const
{
    return Mat2 { row(0), row(1), row(2) };
}

//
// Factory methods
//
Mat2 translation(float x, float y)
{
    return Mat2 {
        1, 0, x,
        0, 1, y,
        0, 0, 1
    };
}

Mat2 translation(const Vec2& v)
{
    return translation(v[0], v[1]);
}

Mat2 rotation(float theta)
{
    auto c = float(cos(theta)); // narrowing, {cos(...)} would warn
    auto s = float(sin(theta)); // we're okay with it for now

    return Mat2 {
        c, -s, 0,
        s,  c, 0,
        0,  0, 1
    };
}

Mat2 placeAtAzimuth(const Pt2& loc, float a)
{
    auto c = float(cos(a));
    auto s = float(sin(a));

    return Mat2 {
        c, -s, loc[0],
        s,  c, loc[1],
        0,  0, 1
    };
}

} // namespace Geom
