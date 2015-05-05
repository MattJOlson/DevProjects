// erdos.cc
// Compute Erdos numbers from a list of citations
// http://www.reddit.com/r/dailyprogrammer/comments/30bquq/20150326_challenge_207_bonus_erdos_number/
// Matt Olson 2015

#include <iostream>
#include <regex>
#include <string>

#include <cstdio>

enum class LineState {LastName, Initials, FinalName, Suffix};



int main(int argc, char* argv[])
{
    // I'm just sort of expecting encodings to magically work :(
    std::string test {"Thomassen, C., Erdös, P., Alavi, Y., Malde, P. J., & Schwenk, A. J. (1989). Tight bounds on the chromatic sum of a connected graph.  Journal of Graph Theory, 13(3), 353-357."};

#if 0
    // Well, shit.  Seems like g++ doesn't support regexes yet.  Fuck that shit.
    std::regex author {R"(\w+,( [[:upper:]]\.)+)"};

    // Is there a way to use auto here?
    for(std::sregex_iterator match(test.begin(), test.end(), author);
        match != std::sregex_iterator{};
        match++)
    {
        std::cout << (*match)[1] << std::endl;
    }
#endif

    return 0;
}
