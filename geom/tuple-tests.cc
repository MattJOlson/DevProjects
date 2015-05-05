// tuple-tests.cc
// gtest unit tests for vector types
// Matt Olson 2015

#include "tup3.h"
#include "pt2.h"
#include "vec2.h"
#include "gtest/gtest.h"

#include <cmath>

// Object creation
TEST(ObjectCreation, Tup3ConstructionAndAccess) {
    EXPECT_FLOAT_EQ(1, Geom::Tup3(1,2,3)[0]);
    EXPECT_FLOAT_EQ(2, Geom::Tup3(1,2,3)[1]);
    EXPECT_FLOAT_EQ(3, Geom::Tup3(1,2,3)[2]);
    // Tuple indexing is modular
    EXPECT_FLOAT_EQ(1, Geom::Tup3(1,2,3)[3]);
    EXPECT_FLOAT_EQ(2, Geom::Tup3(1,2,3)[4]);
    EXPECT_FLOAT_EQ(3, Geom::Tup3(1,2,3)[-1]);
    EXPECT_FLOAT_EQ(2, Geom::Tup3(1,2,3)[-2]);
    EXPECT_FLOAT_EQ(1, Geom::Tup3(1,2,3)[-3]);
    EXPECT_FLOAT_EQ(3, Geom::Tup3(1,2,3)[-4]);
}

TEST(ObjectCreation, Pt2ConstructionAndAccess) {
    EXPECT_FLOAT_EQ(2, Geom::Pt2(2,3).x());
    EXPECT_FLOAT_EQ(3, Geom::Pt2(2,3).y());
    EXPECT_FLOAT_EQ(1, Geom::Pt2(2,3).w());
}

TEST(ObjectCreation, Vec2ConstructionAndAccess) {
    EXPECT_FLOAT_EQ(2, Geom::Vec2(2,3).x());
    EXPECT_FLOAT_EQ(3, Geom::Vec2(2,3).y());
    EXPECT_FLOAT_EQ(0, Geom::Vec2(2,3).w());
}

// Object sizes
TEST(ObjectCreation, TuplesAreAllLean) {
    EXPECT_EQ(sizeof(Geom::Tup3), 3*sizeof(float));
    EXPECT_EQ(sizeof(Geom::Pt2),  3*sizeof(float));
    EXPECT_EQ(sizeof(Geom::Vec2), 3*sizeof(float));
}

// Equality comparisons
TEST(EqualityOperators, Pt2Equality) {
    auto u = Geom::Pt2{1,1};
    auto v = u;
    auto q = Geom::Pt2{2,1};
    EXPECT_TRUE(u == u);
    EXPECT_TRUE(u == v);
    EXPECT_FALSE(v == q);
}

TEST(EqualityOperators, Pt2Inequality) {
    auto u = Geom::Pt2{1,1};
    auto v = u;
    auto q = Geom::Pt2{2,1};
    EXPECT_FALSE(u != u);
    EXPECT_FALSE(u != v);
    EXPECT_TRUE(v != q);
}

TEST(EqualityOperators, Vec2Equality) {
    auto u = Geom::Vec2{1,1};
    auto v = u;
    auto q = Geom::Vec2{2,1};
    EXPECT_TRUE(u == u);
    EXPECT_TRUE(u == v);
    EXPECT_FALSE(v == q);
}

TEST(EqualityOperators, Vec2Inequality) {
    auto u = Geom::Vec2{1,1};
    auto v = u;
    auto q = Geom::Vec2{2,1};
    EXPECT_FALSE(u != u);
    EXPECT_FALSE(u != v);
    EXPECT_TRUE(v != q);
}

// Vector,scalar operations
TEST(VectorScalarMath, MagnitudeOps) {
    auto v = Geom::Vec2{2,0};
    EXPECT_FLOAT_EQ(2, v.mag());
    EXPECT_FLOAT_EQ(4, v.sqmag());
}

TEST(VectorScalarMath, ScaleVectors) {
    auto v = Geom::Vec2{2,1};
    EXPECT_FLOAT_EQ(2, (v*2).mag() / v.mag());
    EXPECT_FLOAT_EQ(0.5, (v/2).mag() / v.mag());
    EXPECT_FLOAT_EQ(2, (2*v).mag() / v.mag());
    EXPECT_TRUE(-v == Geom::Vec2(-2,-1));
}

TEST(VectorScalarMath, ScaleInPlace) {
    auto v = Geom::Vec2{2,1};
    auto v1 = v; v1 *= 2;
    auto v2 = v; v2 /= 2;
    EXPECT_FLOAT_EQ(2, v1.mag() / v.mag());
    EXPECT_FLOAT_EQ(0.5, v2.mag() / v.mag());
}

// Point.toVec
TEST(PointToVec, PtVDefault) {
    auto p = Geom::Pt2{1,2};
    auto v = p.toVec();
    // Google Test doesn't like Vec2{1,2}
    EXPECT_TRUE(v == Geom::Vec2(1,2));
}

TEST(PointToVec, PtVNonOrigin) {
    auto p = Geom::Pt2{1,2};
    auto v = p.toVec(Geom::Pt2{3,3});
    EXPECT_TRUE(v == Geom::Vec2(-2,-1));
}

// Point-vector math
TEST(PointVectorOps, AddingZeroDoesNothing) {
    auto p = Geom::Pt2{1,2};
    auto z = Geom::Vec2{0,0};
    EXPECT_TRUE(p == p + z);
    EXPECT_TRUE(p == z + p);
    EXPECT_TRUE(p == p - z);
}

TEST(PointVectorOps, AddAssignZeroDoesNothing) {
    auto p = Geom::Pt2{1,2};
    auto q = p;
    auto z = Geom::Vec2{0,0};
    q += z;
    EXPECT_TRUE(q == p);
    q -= z;
    EXPECT_TRUE(q == p);
}

TEST(PointVectorOps, AdditionSubtraction) {
    auto p = Geom::Pt2{1,2};
    auto v = Geom::Vec2{2,3};
    EXPECT_TRUE((p + v) == Geom::Pt2(3,5));
    EXPECT_TRUE((v + p) == Geom::Pt2(3,5));
    EXPECT_TRUE((p - v) == Geom::Pt2(-1,-1));
}

TEST(PointVectorOps, AddSubAssign) {
    auto p = Geom::Pt2{1,2};
    auto v = Geom::Vec2{2,3};
    p += v;
    EXPECT_TRUE(p == Geom::Pt2(3,5));
    p -= 2*v;
    EXPECT_TRUE(p == Geom::Pt2(-1,-1));
}

// Vector-vector ops
TEST(VectorVectorOps, AddingZeroDoesNothing) {
    auto v = Geom::Vec2{1,2};
    auto z = Geom::Vec2{0,0};
    EXPECT_TRUE(v == v + z);
    EXPECT_TRUE(v == v - z);
}

TEST(VectorVectorOps, AddAssignZeroDoesNothing) {
    auto v = Geom::Vec2{1,2};
    auto u = v;
    auto z = Geom::Vec2{0,0};
    u += z;
    EXPECT_TRUE(u == v);
    u -= z;
    EXPECT_TRUE(u == v);
}

TEST(VectorVectorOps, AdditionSubtraction) {
    auto v = Geom::Vec2{1,2};
    auto u = Geom::Vec2{2,3};
    EXPECT_TRUE((v + u) == Geom::Vec2(3,5));
    EXPECT_TRUE((v - u) == Geom::Vec2(-1,-1));
}

TEST(VectorVectorOps, AddSubAssign) {
    auto p = Geom::Vec2{1,2};
    auto v = Geom::Vec2{2,3};
    p += v;
    EXPECT_TRUE(p == Geom::Vec2(3,5));
    p -= 2*v;
    EXPECT_TRUE(p == Geom::Vec2(-1,-1));
}

TEST(AssociatedOps, PointDistances) {
    auto p = Geom::Pt2{1,2};
    auto q = Geom::Pt2{3,2};
    auto r = Geom::Pt2{1,4};

    EXPECT_EQ(4, sqdist(p,q));
    EXPECT_EQ(2, dist(p,q));
    EXPECT_EQ(4, sqdist(p,r));
    EXPECT_EQ(2, dist(p,r));
}

TEST(AssociatedOps, DotProduct) {
    auto v = Geom::Vec2{1,0};
    auto u = Geom::Vec2{0,1};

    EXPECT_FLOAT_EQ(1.0f, dprod(u,u));
    EXPECT_FLOAT_EQ(0.0f, dprod(u,v));
    EXPECT_FLOAT_EQ(-1.0f, dprod(u,-u));
}

TEST(AssociatedOps, CrossProduct) {
    auto v = Geom::Vec2{1,0};
    auto u = Geom::Vec2{0,1};

    EXPECT_LT(0, xprod(v,u));
    EXPECT_GT(0, xprod(u,v));
}

TEST(AssociatedOps, Normalize) {
    auto v = Geom::Vec2{1,0};
    auto u = Geom::Vec2{2,3};

    EXPECT_FLOAT_EQ(normalize(v).mag(), v.mag());
    EXPECT_FLOAT_EQ(1.0f, normalize(u).mag());
}

TEST(AssociatedOps, AngleVecGenerator) {
    auto v = Geom::anglevec(1.0f);

    EXPECT_FLOAT_EQ(v.x(), cos(1.0f));
    EXPECT_FLOAT_EQ(v.y(), sin(1.0f));
}

TEST(AssociatedOps, TwoPointLerp) {
    auto p = Geom::Pt2{1,0};
    auto q = Geom::Pt2{2,3};

    EXPECT_TRUE(Geom::Pt2(1.5,1.5) == Geom::lerp(p,q,0.5));

}
