// vptree-tests.cc
// Unit tests for a VP-tree implementation
// Matt Olson 2015

#include "vptree.h"
#include "gtest/gtest.h"

#include <pt2.h>
#include <vector>

const auto base = Geom::Pt2 {1, 1};
const auto out  = Geom::Pt2 {3, 2};
const auto in   = Geom::Pt2 {2, 1};

TEST(vptree, VPTreeNodeConstruction) {
    auto node = VPTree { base, 2 };

    EXPECT_EQ(base, node.pivot());
    EXPECT_EQ(2, node.radius());
}

TEST(vptree, VPTreeNodeQuery) {
    auto node = VPTree { base, 2 };

    EXPECT_TRUE(node.isOutside(out));
    EXPECT_FALSE(node.isInside(out));
    EXPECT_TRUE(node.isInside(in));
    EXPECT_FALSE(node.isOutside(in));
}

TEST(vptree, VPTreeContainsPivot) {
    auto node = VPTree { base, 2 };

    EXPECT_TRUE(node.contains(base));
    EXPECT_FALSE(node.contains(out));
}

TEST(vputil, MedianDistance) {
    auto points = PtVector { { 0.5, 1 }, out, in, { 5, 5 } };
    auto med = (Geom::dist(base, out) + Geom::dist(base, in)) / 2.0;

    EXPECT_FLOAT_EQ(med, partitionByDistance(base, points));

    points = PtVector { out, in, { 5, 5 } };
    med = Geom::dist(base, out);

    EXPECT_FLOAT_EQ(med, partitionByDistance(base, points));
}

TEST(vptree, VPTreeChildConstruction) {
    auto points = PtVector { in, out };
    auto med = partitionByDistance(base, points);
    auto node = VPTree { base, med };

    node.buildChildren(points);

    EXPECT_TRUE(node.contains(in));
    EXPECT_TRUE(node.contains(out));
}
