// addchains.cc
// Searches for a length-k addition chain for a positive integer n
// https://www.reddit.com/r/dailyprogrammer/comments/2y5ziw/20150306_challenge_204_hard_addition_chains/
// Matt Olson 2015

#include <stdio.h>
#include <stdlib.h>
#include <vector>

typedef std::vector<int> AddChain;

bool findChain(int length, int target, AddChain& chain)
{
    int last = chain.back();

    if(chain.size()-1 == length) { // gonna stop recursing one way or another
        if (last == target) {
            return true;
        } else {
            return false;
        }
    }

    // Okay, we're not done yet; let's see if there's an int somewhere in the
    // chain we can add onto that gets us closer.
    //
    // This is a depth-first recursive search.
    for(auto i = chain.cbegin(); i != chain.cend(); i++) {
        // Tried pruning branches that are over; this doesn't actually help!
        // if(target < last + *i) { return false; }

        AddChain candidate(chain);
        candidate.push_back(last + *i);
        if(findChain(length, target, candidate)) { // woohoo, found one!
            chain = candidate; // clobber i/o param with solution
            return true;
        } // else continue
    }

    // We exited that for loop without finding an acceptable candidate; this
    // must be a dead end.
    return false;
}

int main(int argc, char* argv[])
{
    if(argc != 3) {
        fprintf(stderr, "Usage: addchains k n\n");
        exit(1);
    }

    int length = atoi(argv[1]);
    int target = atoi(argv[2]);

    printf("Finding length-%d addition chain for %d...\n", length, target);

    AddChain chain;
    chain.reserve(2*length);
    chain.push_back(1);

    if(findChain(length, target, chain)) {
        printf("[");
        for(auto i = chain.cbegin(); i != chain.cend(); i++) {
            if(i != chain.cbegin()) { printf(", "); }
            printf("%d", *i);
        }
        printf("]\n");
    } else {
        printf("Failed to find chain!\n");
    }

    return 0;
}
