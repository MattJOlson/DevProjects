// matrix-tests.cc
// gtest unit tests for Mat2
// Matt Olson 2015

#include "tup3.h"
#include "pt2.h"
#include "vec2.h"
#include "mat2.h"
#include "gtest/gtest.h"

#include <cmath>
#include <cstdio>

TEST(MatrixCreation, MatrixDefaultIsIdentity) {
    auto I = Geom::Mat2 {};
    for(auto r = 0; r < 3; r++) {
        for(auto c = 0; c < 3; c++) {
            if(r == c) {
                EXPECT_FLOAT_EQ(1.0f, I[r][c]);
            } else {
                EXPECT_FLOAT_EQ(0.0f, I[r][c]);
            }
        }
    }
}

TEST(MatrixCreation, MatrixIsPacked) {
    auto I = Geom::Mat2 {};
    EXPECT_EQ(sizeof(I), 9*sizeof(float));
}

TEST(MatrixCreation, ArbitraryMatrixValues) {
    auto M = Geom::Mat2 {
        1,2,3,
        4,5,6,
        7,8,9
    };

    EXPECT_TRUE(M[0] == Geom::Tup3(1,2,3));
    EXPECT_TRUE(M[1] == Geom::Tup3(4,5,6));
    EXPECT_TRUE(M[2] == Geom::Tup3(7,8,9));
}

TEST(MatrixCreation, CreationByColumns) {
    auto M = Geom::Mat2 {
        Geom::Tup3 {1,4,7}, // column 1
        Geom::Tup3 {2,5,8}, // column 2
        Geom::Tup3 {3,6,9}  // column 3
    };

    EXPECT_TRUE(M[0] == Geom::Tup3(1,2,3));
    EXPECT_TRUE(M[1] == Geom::Tup3(4,5,6));
    EXPECT_TRUE(M[2] == Geom::Tup3(7,8,9));
}

TEST(MatrixOperations, Transposition) {
    auto M = Geom::Mat2 {
        1,2,3,
        4,5,6,
        7,8,9
    };
    auto T = M.transpose();

    for(auto r = 0; r < 3; r++) {
        for(auto c = 0; c < 3; c++) {
            EXPECT_EQ(M[r][c], T[c][r]);
        }
    }
}

TEST(MatrixOperations, MatrixMultiplication) {
    auto L = Geom::Mat2 {
        1, 2, 3,
        2, 3, 1,
        4, 1, 2
    };
    auto R = Geom::Mat2 {
        2, 3, 1,
        4, 1, 2,
        1, 2, 3
    };
    auto Truth = Geom::Mat2 {
        13, 11, 14,
        17, 11, 11,
        14, 17, 12
    };
    auto P = L*R;

    for(auto r = 0; r < 3; r++) {
        for(auto c = 0; c < 3; c++) {
            EXPECT_FLOAT_EQ(Truth[r][c], P[r][c]);
        }
    }
}

TEST(MatrixTransforms, IdentityDoesNothing) {
    auto I = Geom::Mat2 {};
    auto p = Geom::Pt2 {2, 3};
    auto v = Geom::Vec2 {2, 4};

    EXPECT_TRUE(p == I*p);
    EXPECT_TRUE(p == p*I);
    EXPECT_TRUE(v == I*v);
    EXPECT_TRUE(v == v*I);
}

TEST(MatrixTransforms, TranslationFactory) {
    auto T = Geom::Mat2 {
        1,0,2,
        0,1,3,
        0,0,1
    };
    auto T1 = Geom::translation(2,3);
    auto T2 = Geom::translation(Geom::Vec2{2,3});

    for(auto i = 0; i < 3; i++) {
        for(auto j = 0; j < 3; j++) {
            EXPECT_EQ(T[i][j], T1[i][j]);
            EXPECT_EQ(T[i][j], T2[i][j]);
        }
    }
}

TEST(MatrixTransforms, TranslateWorksOnPoints) {
    auto T = Geom::translation(2,3);
    auto p = Geom::Pt2  {1,2};
    auto v = Geom::Vec2 {1,2};

    EXPECT_TRUE(T*p == Geom::Pt2(3,5));
    EXPECT_TRUE(T*v == v);
}

TEST(MatrixTransforms, RotationFactory) {
    auto R = Geom::Mat2 {
        cos(1), -sin(1), 0,
        sin(1),  cos(1), 0,
        0,       0,      1
    };
    auto R1 = Geom::rotation(1);

    for(auto r = 0; r < 3; r++) {
        for(auto c = 0; c < 3; c++) {
            EXPECT_EQ(R[r][c], R1[r][c]);
        }
    }
}

TEST(MatrixTransforms, RotationWorksOnBoth) {
    auto R = Geom::rotation(1);
    auto v = Geom::anglevec(0.5);
    auto p = Geom::Pt2{0,0} + v;

    EXPECT_FLOAT_EQ(cos(1), Geom::dprod(v, R*v));
    EXPECT_FLOAT_EQ(cos(1), Geom::dprod(p.toVec(), (R*p).toVec()));
}

TEST(MatrixTransforms, PlaceAtAzimuth) {
    auto loc = Geom::Pt2{2, 3};

    auto R = Geom::rotation(1);
    auto T = Geom::translation(loc.toVec());

    auto p = Geom::Pt2(1,0);
    auto r = R*p; // rotate p around the origin
    auto t = T*r; // now place it at loc

    auto M = Geom::placeAtAzimuth(loc, 1);
    auto x = M*p;

    EXPECT_FLOAT_EQ(t[0], x[0]);
    EXPECT_FLOAT_EQ(t[1], x[1]);
}
