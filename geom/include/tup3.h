// tup3.h
// Three-tuple base class for points in Geom
// Matt Olson 2015

#ifndef TUP3_H
#define TUP3_H

namespace Geom {

// Tup3 is a 3-tuple <x,y,w> representing a 2d point or vector in
// homogeneous coordinates.  It's an abstract class (Pt2 and Vec2 are
// concrete) but collects all the data, copy, assignment, and that sort
// of thing.
//
// Note that by implementing Tup3 -> (Pt2, Vec2) as a class hierarchy
// with a vtbl and such we're NOT giving up the capability of packing an
// array of Tup3s as an array of floats and shipping it off to OpenGL.
// In fact, sizeof(Tup3) == sizeof(Pt2) == sizeof(Vec2) ==
// 3*sizeof(float) because we don't have any virtual functions and thus
// no vtbl.
//
// Another sensible design would implement Tup3 as a template over a
// coordinate numerical type.  CGAL goes HAM on this, for example.  We
// won't do that.
//
class Tup3 {
  public: 
    Tup3(float x = 0, float y = 0, float w = 0) : tup_ {x, y, w} {}
    ~Tup3() = default; // holds no resources, don't need virtual dtor
    Tup3(const Tup3&) = default; // standard copy ctor, op= are fine
    Tup3& operator=(const Tup3&) = default;

    // If this object is just a 3-vector in the linear algebra sense,
    //  we'll index into tup_.
    // Indexing tuples is modular!  This behaviour is inspired by
    //  determinant computation, which I haven't actually written yet.
    //  Let's see if it works.
    const float& operator[](int i) const;

    const bool operator==(const Tup3& r) const;
    const bool operator!=(const Tup3& r) const;

    // When we're accessing tup_ as a point or as a 2-vector, we like to
    //  have named accessors.
    const float& x() const { return tup_[0]; }
    const float& y() const { return tup_[1]; }
    const float& w() const { return tup_[2]; }

  protected:
    // I'm wondering if this is all necessary.  Rather than allow += and
    //  other self-modifying operations, maybe we should just insist on
    //  side-effect-free use of tuples and their kids.
    void x(float x) { tup_[0] = x; }
    void y(float y) { tup_[1] = y; }
    void w(float w) { tup_[2] = w; }

  private:
    float tup_[3];
};

// Utility function for Pt2 and Vec2 equality ops.
//
// There's some interesting design stuff going on in equality ops.
// First of all, I'm asserting that it's nonsensical to compare a Pt2 to
// a Vec2 by the operator== type declarations, but since they're both
// tuples I'm centralizing the code around the base class.
//
// Second of all, there's this general idea that points always have w=1
// and vectors always have w=0.  That is of course not true in general
// terms for 2d homogeneous coordinates, but for now it seems to hold.
// Depending on how we implement Mat3 this may change.
bool areEqual(const Tup3& l, const Tup3& r);

// Scalar product on tuples, no special treatment for w
float sprod(const Tup3& l, const Tup3& r);

} // namespace Geom

#endif
