// erdos.h
// Compute Erdos numbers from a list of citations
// http://www.reddit.com/r/dailyprogrammer/comments/30bquq/20150326_challenge_207_bonus_erdos_number/
// Matt Olson 2015

#ifndef ERDOS_H
#define ERDOS_H

#include <functional>
#include <memory>
#include <string>
#include <vector>

// A SplitString is a helper class for first-rest string processing,
// which should be familiar for anyone who's done functional
// programming.  So far we're just tokenizing it based on a given
// delimiter.
struct SplitString {
  public:
    SplitString() : first{}, rest{} {}
    // note: NOT explicit, we want to be able to convert std::strings to
    // SplitStrings transparently
    SplitString(std::string base) : first{}, rest{base} {}
    SplitString(std::string f, std::string r) : first{f}, rest{r} {}

    // create a new SplitString from rest, splitting on the first
    // instance of delim
    SplitString split(std::string delim) const;

    std::string first;
    std::string rest;
};

class ParseStep;
// This somehow causes cannot-convert errors in the ParseStep ctor
// error: could not convert 'parseLastName' from 'ParseStep(*)(SplitString) to 'ParseFunction {aka std::function<ParseStep(SplitString)>}'
//using ParseFunction = std::function<ParseStep(SplitString)>;
typedef ParseStep (*ParseFunction)(SplitString);

ParseStep parseLastName(SplitString next_data);
ParseStep parseInitials(SplitString next_data);
ParseStep parseFinalName(SplitString next_data);
ParseStep parseFinalInitials(SplitString next_data);
ParseStep parseSuffix(SplitString next_data);

// A ParseStep is one step of parsing a citation line.  We alternate
// parsing out last names and first initials, delimited by commas, until
// we get to the last entry (which starts with an ampersand).  Once we
// parse the last entry, we do nothing on future parse steps (it's a
// fixed point).
class ParseStep {
  public:
    ParseStep(SplitString input, ParseFunction parser=parseLastName) :
        parser_(parser),
        data_(input)
    {}
    ~ParseStep() = default;
    ParseStep(const ParseStep& from) = default;
    ParseStep& operator=(const ParseStep& rhs) = default;

    // chunk() returns the token that this ParseStep extracted.  The
    // initial chunk is an empty string -- we haven't parsed anything
    // yet.
    std::string chunk() const { return data_.first; }

    // parse() pulls a token off of the front of the unparsed tail of
    // the citation line and returns it in a new ParseState object.
    ParseStep parse() const;

  private:
    ParseFunction parser_;
    SplitString data_;
};

// An Author is a vertex in our search graph: a name, and a bunch of
// pointers (edges) to other authors.  Authors are owned by some outside
// container, so the edges are defined by weak_ptrs.
class Author {
  public:
    Author(std::string name) : name_(name), edges_(), count_(0) {}
    ~Author() = default;
    Author(const Author& from) = delete; // authors are reference objs
    Author& operator=(const Author& rhs) = delete;

    std::string name() const { return name_; }

    const std::vector< std::weak_ptr<Author> >& edges() const {
        return edges_;
    }

    void link(std::weak_ptr<Author> other);

    bool links(std::weak_ptr<Author> other) const;

    int count() const { return count_; }

    void visit() { ++count_; }
  private:
    std::string name_;
    std::vector< std::weak_ptr<Author> > edges_;
    int count_;
};

#endif // ERDOS_H
