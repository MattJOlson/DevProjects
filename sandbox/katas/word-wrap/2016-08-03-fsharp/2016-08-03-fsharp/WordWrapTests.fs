module WordWrapTests

open WordWrap
open NUnit.Framework
open FsUnit

[<Test>]
let ``Wrapping a single word that's shorter than the line length returns that word``() =
    wrap "foo" 5 |> should equal "foo"

[<Test>]
let ``Wrapping a single word that's longer than the line length still returns that word``() =
    wrap "antidisestablishmentarianism" 5 |> should equal "antidisestablishmentarianism"

[<Test>]
let ``Wrapping two words that, combined, are shorter than the line length returns one line``() =
    wrap "foo bar" 8 |> should equal "foo bar"

[<Test>]
let ``Wrapping two words that exceed the line length returns two lines``() =
    wrap "foo bar" 4 |> should equal "foo\nbar"