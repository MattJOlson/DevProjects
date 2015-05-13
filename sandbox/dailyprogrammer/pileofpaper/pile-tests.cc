// pile-tests.cc
// gtest unit tests for Pile Of Paper
// Matt Olson 2015

#include "pile.h"
#include "gtest/gtest.h"

#include <cmath>

TEST(Interval, IntervalCreation) {
    auto i = Interval {1, 20};

    EXPECT_EQ(1, i.start());
    EXPECT_EQ(21, i.end());
    EXPECT_EQ(20, i.length());
}

TEST(Interval, IntervalContainment) {
    auto i = Interval {5, 10};
    EXPECT_TRUE(i.contains(5));
    EXPECT_FALSE(i.contains(4));
    EXPECT_TRUE(i.contains(15));
    EXPECT_FALSE(i.contains(16));
}

TEST(Interval, IntervalInsertion) {
    auto b = IntervalList { Interval {0, 20} };
    auto i = Interval {5, 10};

    auto list = insertInterval(b, i);
    EXPECT_EQ(3, list.size());
}

/*
TEST(Scanline, ScanlineCreation) {
    auto l = Scanline { 20 };

}
 */
