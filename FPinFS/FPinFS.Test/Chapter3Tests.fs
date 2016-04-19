module Chapter3Tests

open FPinFS.Chapter3
open System.Linq
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

// 3.5. dropWhile
[<Test>]
let ``dropWhile from an empty list is empty``() =
    dropWhile [] (fun x -> true) |> should be Empty

[<Test>]
let ``dropWhile all elements hit is empty``() =
    dropWhile [2; 6; 9] (fun x -> true) |> should be Empty

[<Test>]
let ``dropWhile some elements hit is the tail from the first failure``() =
    dropWhile [2; 3; 6; 9] (fun x -> x % 2 = 0) |> should equal [3; 6; 9]

// 3.6. init
[<Test>]
let ``init of an empty list is empty``() =
    init [] |> should be Empty

[<TestCase(1)>]
[<TestCase(4)>]
let ``init of an n-element list should be the first n-1 elements of that list``(n) =
    let ns = Enumerable.Range(1, n) |> Seq.toList
    let expected = Enumerable.Range(1, n-1) |> Seq.toList

    init ns |> should equal expected