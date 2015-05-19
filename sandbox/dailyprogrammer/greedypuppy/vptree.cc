// vptree.cc
// Implementation of VP-tree
// Matt Olson 2015

#include "vptree.h"

#include <algorithm>
#include <memory>
#include <cstdio>

bool VPTree::contains(Pt2 p) const
{
    if(p == pivot_) return true;

    if((inside_  != nullptr) && inside_->contains(p))  return true;
    if((outside_ != nullptr) && outside_->contains(p)) return true;

    return false;
}

void VPTree::buildChildren(PtVector& input)
{
    auto len = input.size() / 2;
    auto median = input.begin() + len;
    auto inner = PtVector { input.begin(), median };
    auto outer = PtVector { median, input.end() };

    buildVPTree(inside_, inner);
    buildVPTree(outside_, outer);
}

void VPTree::buildVPTree(std::unique_ptr<VPTree>& root, PtVector& points)
{
    auto pivot = points.back(); // points isn't sorted, any elem is good
    points.pop_back();

    if(points.empty()) { // leaf node
        root = std::unique_ptr<VPTree>(new VPTree { pivot, 0 });
        return;
    }

    auto median = partitionByDistance(pivot, points);

    root = std::unique_ptr<VPTree>(new VPTree { pivot, median });

    root->buildChildren(points);
}

float partitionByDistance(Pt2 pivot, PtVector& points)
{
    auto len = points.size() / 2;

    std::nth_element(points.begin(), points.begin() + len, points.end(),
         [pivot](Pt2 p, Pt2 q){ return dist(pivot, p) < dist(pivot, q); });

    auto median = dist(pivot, points[len]);
    if(len % 2 == 0) {
        median += dist(pivot, points[len-1]);
        median /= 2.0;
    }

    return median;
}
