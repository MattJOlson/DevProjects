/* erdos-tests.cc
 * Unit tests for Erdos numbers programming problem
 * Matt Olson 2015
 */

#include "erdos.h"
#include "gtest/gtest.h"

TEST(SplitString, SplitStringConstruction) {
    auto empty = SplitString {};
    auto text = std::string {"Foo bar baz"};
    auto full = SplitString {text};

    EXPECT_EQ("", empty.first);
    EXPECT_EQ("", empty.rest);
    EXPECT_EQ("", full.first);
    EXPECT_EQ(text, full.rest);
}

TEST(SplitString, SplitOnDelimiter) {
    auto prefix = std::string {"J. Random std::string"};
    auto suffix = std::string {"Esq."};
    auto delim = std::string {", "};
    auto input = SplitString {prefix + delim + suffix};
    auto split = input.split(delim);

    EXPECT_EQ(prefix, split.first);
    EXPECT_EQ(suffix, split.rest);
}

TEST(SplitString, SplitPreservesInput) {
    auto prefix = std::string {"J. Random std::string"};
    auto suffix = std::string {" Esq."};
    auto delim = std::string {", "};
    auto input = SplitString {prefix + delim + suffix};
    auto split = input.split(delim);

    EXPECT_EQ("", input.first);
    EXPECT_EQ(prefix + delim + suffix, input.rest);
}

std::string kTestLine {"Bar-Baz, F., Programmer, J. R., & Fancy Last Name, B. (2015).  The rest of this line doesn't matter."};

TEST(ParseStep, ParserInitialization) {
    auto init = ParseStep { kTestLine };

    EXPECT_EQ(LineState::LastName, init.state());
    EXPECT_EQ("", init.chunk());
}

TEST(ParseStep, ParseLastName) {
    auto init = ParseStep { kTestLine };
    auto last = init.parse();

    EXPECT_EQ(LineState::Initials, last.state());
    EXPECT_EQ("Bar-Baz", last.chunk());
}

TEST(ParseStep, ParseInitials) {
    auto last = ParseStep{kTestLine}.parse();
    auto first = last.parse();

    EXPECT_EQ(LineState::LastName, first.state());
    EXPECT_EQ("F.", first.chunk());
}

TEST(ParseStep, DetectLastAuthor)  {
    auto last = ParseStep{kTestLine}.parse(); // F. Bar-Baz
    auto first = last.parse();

    last = first.parse(); // J. R. Programmer
    EXPECT_EQ(LineState::Initials, last.state());
    EXPECT_EQ("Programmer", last.chunk());

    first = last.parse();
    EXPECT_EQ(LineState::FinalName, first.state());
    EXPECT_EQ("J. R.", first.chunk());

    last = first.parse();
    EXPECT_EQ(LineState::FinalInitials, last.state());
    EXPECT_EQ("Fancy Last Name", last.chunk());

    first = last.parse();
    EXPECT_EQ(LineState::Suffix, first.state());
    EXPECT_EQ("B.", first.chunk());

    // parsing is idempotent on suffix
    last = first.parse();
    EXPECT_EQ(first.state(), last.state());
    EXPECT_EQ(first.chunk(), last.chunk());
}

TEST(AuthorClass, AuthorCreation) {
    auto auth = std::make_shared<Author>("Bar-Baz, F.");

    EXPECT_EQ(auth->name(), "Bar-Baz, F.");
    EXPECT_TRUE(auth->edges().empty());
    EXPECT_EQ(auth->count(), 0);
}

TEST(AuthorClass, AuthorLinks) {
    auto foo = std::make_shared<Author>("Bar-Baz, F.");
    auto jrp = std::make_shared<Author>("Programmer, J.R.");

    foo->link(jrp);

    EXPECT_EQ(foo->edges().size(), 1);
    EXPECT_TRUE(foo->links(jrp));
    EXPECT_TRUE(jrp->edges().empty());
    EXPECT_FALSE(jrp->links(foo));

    foo->link(jrp); // no multiedges
    EXPECT_EQ(foo->edges().size(), 1);
}
