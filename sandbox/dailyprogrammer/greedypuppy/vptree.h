// vptree.h
// A viewpoint tree for the Greedy Puppy problem on /r/dailyprogrammer
// http://www.reddit.com/r/dailyprogrammer/comments/3629st/20150515_challenge_214_hard_chester_the_greedy/
// Matt Olson 2015

#ifndef VPTREE_H
#define VPTREE_H

#include <memory>
#include <pt2.h>
#include <vector>

using namespace Geom;

using PtVector = std::vector<Pt2>;

class VPTree {
  public:
    VPTree(Pt2 pivot, float radius) : 
        pivot_(pivot),
        radius_(radius),
        radsq_(radius*radius),
        inside_(nullptr),
        outside_(nullptr)
    {}

    Pt2 pivot() const { return pivot_; }
    float radius() const { return radius_; }

    bool contains(Pt2 p) const;

    bool isInside(Pt2 p) const { return sqdist(pivot_, p) <= radsq_; }
    bool isOutside(Pt2 p) const { return !isInside(p); }

    // Precondition: input is a vector of Pt2s that's been processed by
    //  partitionByDistance(), and radius_ is the median distance from
    //  pivot_ to the elements in input.
    // This really shouldn't be part of the public interface in a more
    //  thought-out implementation.
    void buildChildren(PtVector& input);
    void buildVPTree(std::unique_ptr<VPTree>& root, PtVector& points);

  private:
    Pt2 pivot_;
    float radius_;
    float radsq_;

    std::unique_ptr<VPTree> inside_;
    std::unique_ptr<VPTree> outside_;
};

// Compute the median distance of points relative to pivot, and
//  partition points into [0, len/2], (len/2, len) subsets such that the
//  second subset contains all points further than the median distance
//  from pivot.
float partitionByDistance(Pt2 pivot, PtVector& points);

#endif
