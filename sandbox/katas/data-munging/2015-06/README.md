### kata-04, data munging

Merged version didn't turn out as clean as I would have liked; the
if/else-if for selecting weather vs. football is unpleasant and the
parsing loop could be better done as a filter.

It might be fun to rewrite the parsers as extension methods on string[]
so I could run the entire parsing loop as a filter on File.ReadAllLines,
but that would be a bit surprising if I just came across the declaration
while reading code.  Although since the parsers never really care where
they get their list-of-string input, maybe not....
