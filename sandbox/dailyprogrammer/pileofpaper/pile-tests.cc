// pile-tests.cc
// gtest unit tests for Pile Of Paper
// Matt Olson 2015

#include "pile.h"
#include "gtest/gtest.h"

#include <cmath>

TEST(Interval, IntervalCreation) {
    auto i = Interval {1, 20, 0};

    EXPECT_EQ(1, i.start());
    EXPECT_EQ(20, i.end());
    EXPECT_EQ(20, i.length());
    EXPECT_EQ(0, i.color());
}

TEST(Interval, IntervalContainment) {
    auto i = Interval {5, 10};
    EXPECT_TRUE(i.contains(5));
    EXPECT_FALSE(i.contains(4));
    EXPECT_TRUE(i.contains(14));
    EXPECT_FALSE(i.contains(15));
}

TEST(Interval, IntervalInsertion) {
    auto b = IntervalList { Interval {0, 20, 3} };
    auto i = Interval {5, 10, 7};

    auto list = insertInterval(b, i);
    EXPECT_EQ(3, list.size());

    EXPECT_EQ(0, list[0].start());
    EXPECT_EQ(4, list[0].end());
    EXPECT_EQ(3, list[0].color());

    EXPECT_EQ(i.start(), list[1].start());
    EXPECT_EQ(i.end(), list[1].end());
    EXPECT_EQ(i.color(), list[1].color());

    EXPECT_EQ(15, list[2].start());
    EXPECT_EQ(19, list[2].end());
    EXPECT_EQ(3, list[2].color());
}

TEST(Scanline, ScanlineCreation) {
    auto line = Scanline { 20 };
    auto intervals = line.intervals();

    EXPECT_EQ(1, intervals.size());
    EXPECT_EQ(0, intervals[0].start());
    EXPECT_EQ(19, intervals[0].end());
    EXPECT_EQ(0, intervals[0].color());
}
