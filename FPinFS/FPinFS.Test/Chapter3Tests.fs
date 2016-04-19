module Chapter3Tests

open FPinFS.Chapter3
open NUnit.Framework
open FsUnit

// 3.2. Tail
[<Test>]
let ``the tail of an empty list is the empty list``() =
    tail [] |> should be Empty 

[<Test>]
let ``the tail of a single-element list is the empty list``() =
    tail [17] |> should be Empty

[<Test>]
let ``the tail of a :: as is as``() =
    tail [2; 4; 8; 16] |> should equal [4; 8; 16]

// 3.3. setHead
[<Test>]
let ``setHead of an empty list is an empty list``() =
    setHead [] 42 |> should be Empty

[<Test>]
let ``setHead of a one-element list is the second argument``() =
    setHead [24] 42 |> should equal [42]

// 3.4. drop
[<TestCase(0)>]
[<TestCase(1)>]
[<TestCase(42)>]
let ``drop n from an empty list is empty``(n) =
    drop [] n |> should be Empty

[<TestCase(-1)>]
[<TestCase(0)>]
let ``dropping from a list only affects the list when a positive number of elements are dropped``(n) =
    drop [2; 4; 6] n |> should equal [2; 4; 6]