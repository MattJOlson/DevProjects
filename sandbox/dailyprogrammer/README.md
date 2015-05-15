# dailyprogrammer

Reddit's programming challenges.  Well, some of them.

## addchains (C++)

A simple(-minded) recursive solution to the addition chains problem
described at
https://www.reddit.com/r/dailyprogrammer/comments/2y5ziw/20150306_challenge_204_hard_addition_chains/.

## erdosnumbers (C++11, scons)

A TDD-driven, functionally-flavoured, probably over-engineered solution
to the Erdös number problem described at
http://www.reddit.com/r/dailyprogrammer/comments/30bquq/20150326_challenge_207_bonus_erdos_number/.
So far incomplete.  I'd be done if gcc 4.8.2 supported C++11 regexes; on
the other hand, this way I get to implement a state machine around
first-rest list processing and a big whack of value objects.

## pileofpaper (C++11, scons)

Another TDD/over-engineered solution, this time to the
raster-manipulation problem at
http://www.reddit.com/r/dailyprogrammer/comments/35s2ds/20150513_challenge_214_intermediate_pile_of_paper/
This one's based on scanlines, which takes me back to the days before
hardware 3D when engines like Quake's managed hidden-surface removal
this way.

## stdev (C++11)

Calculating the standard deviation of a set of integers turned into a
quick exercise in range-for and lambdas.

http://www.reddit.com/r/dailyprogrammer/comments/35l5eo/20150511_challenge_214_easy_calculating_the/
