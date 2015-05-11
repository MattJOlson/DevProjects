// erdos.cc
// Compute Erdos numbers from a list of citations
// http://www.reddit.com/r/dailyprogrammer/comments/30bquq/20150326_challenge_207_bonus_erdos_number/
// Matt Olson 2015

#include "erdos.h"

#include <algorithm>
#include <iostream>
#include <string>

SplitString SplitString::split(std::string delim) const
{
    auto first_delim = rest.find(delim);
    auto len = delim.size();

    return SplitString { rest.substr(0, first_delim),
                         rest.substr(first_delim + len) };
}

ParseStep ParseStep::parse() const
{
    // XXX: This depends on having an ampersand before the last author's
    // name, so it'll fail on single-author papers.  The whole point of
    // the exercise is that the papers have multiple authors, though, so
    // I'm not too worried.
    //
    // Another brittle case is two-author papers with no comma between
    // the first and second author; however, the two-author papers in
    // the reddit data set both have a separating comma.
    //
    // The obvious way to work around these issues is to start by
    // looking for the publication date, which follows the author list,
    // and bound on that.  For this problem, though, the solution here
    // is nice and clean.

    return parser_(data_);

/*
    switch(state_) {
        case LineState::LastName:
            return parseLastName(data_.split(", "));
        case LineState::Initials:
            return parseInitials(data_.split(", "));
        case LineState::FinalName:
            return parseFinalName(data_.split(", "));
        case LineState::FinalInitials:
            return parseFinalInitials(data_.split(" ")); // no more commas
        case LineState::Suffix:
            return parseSuffix(data_); // suffix is idempotent
        default: // can't happen
            std::cerr << "ParseStep has invalid state, aborting";
            std::cerr << std::endl;
            exit(1);
    }
    */
}

ParseStep parseLastName(SplitString data)
{
    return ParseStep(data.split(", "), parseInitials);
}
ParseStep parseInitials(SplitString data)
{
    data = data.split(", ");
    if(data.rest.front() == '&') { // last author
        data.rest.erase(0,2);
        return ParseStep(data, parseFinalName);
    } // else
    return ParseStep(data, parseLastName);
}
ParseStep parseFinalName(SplitString data)
{
    return ParseStep(data.split(", "), parseFinalInitials);
}
ParseStep parseFinalInitials(SplitString data)
{
    return ParseStep(data.split(" "), parseSuffix); // no more commas
}
ParseStep parseSuffix(SplitString data)
{
    return ParseStep(data, parseSuffix); // suffix is idempotent
}

bool Author::links(std::weak_ptr<Author> other) const
{
    // This is decidedly less readable than I'd prefer
    return std::find_if(edges_.begin(),
                        edges_.end(),
        [&](std::weak_ptr<Author> a)
            {return a.lock()->name() == other.lock()->name(); } 
    ) != edges_.end();
}

void Author::link(std::weak_ptr<Author> other)
{
    if(!links(other))
    {
        edges_.push_back(other);
    }
}
