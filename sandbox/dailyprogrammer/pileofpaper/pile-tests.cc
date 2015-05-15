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

TEST(Scanline, ScanlineInsertion) {
    // from above
    auto base = IntervalList { Interval {0, 20, 0} };
    auto truth = insertInterval(base, Interval {5, 10, 7} );

    auto line = Scanline { 20 };
    line.insert(Interval {5, 10, 7});
    auto list = line.intervals();

    EXPECT_EQ(truth.size(), list.size());

    for(size_t i = 0; i < truth.size(); i++) {
        EXPECT_EQ(truth[i].start(), list[i].start());
        EXPECT_EQ(truth[i].end(),   list[i].end());
        EXPECT_EQ(truth[i].color(), list[i].color());
    }
}

TEST(Scanline, ScanlineClipping) {
    auto line = Scanline { 20 };
    line.insert(Interval {15, 10, 1});
    line.insert(Interval {-5, 10, 2});
    auto list = line.intervals();

    EXPECT_EQ(0, list[0].start());
    EXPECT_EQ(19, list[2].end());
}

TEST(Scanline, ScanlineColorCount) {
    auto line = Scanline { 20 };
    line.insert(Interval {15, 5, 1});
    line.insert(Interval {0, 10, 2});
    line.insert(Interval {3, 2, 3});
    
    // 22233222220000011111

    EXPECT_EQ(5, line.colorCount(0));
    EXPECT_EQ(5, line.colorCount(1));
    EXPECT_EQ(8, line.colorCount(2));
    EXPECT_EQ(2, line.colorCount(3));
}

TEST(Canvas, CanvasCreation) {
    auto canvas = Canvas { 20, 10 };

    EXPECT_EQ(20, canvas.width());
    EXPECT_EQ(10, canvas.height());
    EXPECT_EQ(200, canvas.colorCount(0));
}

TEST(Canvas, CanvasInsertion) {
    auto canvas = Canvas { 20, 10 };
    canvas.insert(2, 2, 5, 5, 1);
    canvas.insert(3, 2, 10, 1, 2);

    EXPECT_EQ(169, canvas.colorCount(0));
    EXPECT_EQ(21, canvas.colorCount(1));
    EXPECT_EQ(10, canvas.colorCount(2));
}
