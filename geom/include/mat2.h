// mat2.h
// 3x3 matrix (2d homogeneous) for Geom
// Matt Olson 2015

#ifndef MAT3_H
#define MAT3_H

#include "tup3.h"
#include "pt2.h"
#include "vec2.h"

namespace Geom {

class Mat2 {
  public:
    // Default ctor is identity
    Mat2() : M_{1,0,0, 0,1,0, 0,0,1} {}

    // Explicit entry ctor
    Mat2(float aa, float ba, float ca,
         float ab, float bb, float cb,
         float ac, float bc, float cc) :
        // See below!  M_ is stored as a column-major matrix
        M_{aa, ab, ac, ba, bb, bc, ca, cb, cc} {}

    // Construct from columns
    Mat2(Tup3 c1, Tup3 c2, Tup3 c3) :
        // See below!  M_ is stored as a column-major matrix
        M_{c1[0], c1[1], c1[2],
           c2[0], c2[1], c2[2],
           c3[0], c3[1], c3[2]} {}

    // Row-major indexing, so A[R][C] returns the Rth row of A as a
    // Tup3, then the Cth element of the row.
    Tup3 operator[](int c) const;

    Pt2 operator*(const Pt2& p) const;
    Vec2 operator*(const Vec2& v) const; 
    Mat2 operator*(const Mat2& M) const;

    Mat2 transpose() const;

  private:
    // We're storing M_ as a column-major matrix so we can easily pass
    //  Mat2s to OpenGL.  On the other hand, everyone in the world uses
    //  row-major notation, so we do too.  This may cause some confusion
    //  and is probably a case of too much design up front.
    float M_[9];

    // convenience functions to extract row r or col c from M_.
    Tup3 row(int r) const;
    Tup3 col(int c) const;
};

Mat2 translation(float x, float y);
Mat2 translation(const Vec2& v);

Mat2 rotation(float theta);

Mat2 placeAtAzimuth(const Pt2& loc, float a);

} // namespace Geom

#endif
