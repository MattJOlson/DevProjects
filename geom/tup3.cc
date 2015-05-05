// tup3.cc
// (Trivial) definitions for Tup3 class

#include "tup3.h"

namespace Geom {

const bool Tup3::operator==(const Tup3& r) const
{
    // XXX: Needs floating-point comparison instead
    return (this->x() == r.x() && 
           (this->y() == r.y() &&
            this->w() == r.w()));
}

const bool Tup3::operator!=(const Tup3& r) const
{
    return !(*this == r);
}

const float& Tup3::operator[](int i) const
{
    // % gives the remainder of division with truncation towards zero,
    // which isn't what we want if i is negative.  Let's first ensure
    // that i is positive, then index by modulus.
    if(i < 0) { i += ((i / -3) + 1) * 3; }
    return tup_[i % 3]; 
}

float sprod(const Tup3& l, const Tup3& r)
{
    return l[0]*r[0] + l[1]*r[1] + l[2]*r[2];
}

} // namespace Geom
