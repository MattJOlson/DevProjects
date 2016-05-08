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

// foldr; given, but w/e -- TDD y'all!
[<Test>]
let ``foldr of empty is empty``() =
    foldr [] 0 (+) |> should equal 0

[<TestCase(1, 1)>]  // 1 - 0 = 1
[<TestCase(4, -2)>] // 1 - (2 - (3 - (4 - 0))) = -2
let ``foldr of a list is what you'd expect``(n, expected) =
    let ns = Enumerable.Range(1, n) |> Seq.toList
    foldr ns 0 (-) |> should equal expected

// 3.8. foldr as identity
[<Test>]
let ``foldr someList nil cons is the identity function``() =
    // :: isn't an operator, so foldr list (::) [] doesn't compile
    // Not sure why foldr list (List<_>.Cons) [] doesn't work. but meh
    foldr [1;2;3] [] (fun a b -> a :: b) |> should equal [1;2;3]

// 3.9. length with foldr
[<TestCase(0)>]
[<TestCase(1)>]
[<TestCase(9)>]
let ``length can be computed with foldr``(len) =
    let list = Enumerable.Range(1, len) |> Seq.toList
    length list |> should equal len

// 3.10. foldl
[<Test>]
let ``foldl of empty is empty``() =
    foldl [] 0 (+) |> should equal 0

[<TestCase(1, -1)>]  // 0 - 1 = -1
[<TestCase(4, -10)>] // 0 - 1 - 2 - 3 - 4 = -10
let ``foldl of a list is what you'd expect``(n, expected) =
    let ns = Enumerable.Range(1, n) |> Seq.toList
    foldl ns 0 (-) |> should equal expected

// Skipping 3.11 because sum, product, and list-length are commutative

// 3.12. reverse
[<Test>]
let ``reverse of empty is empty``() =
    reverse [] |> should equal []

[<Test>]
let ``reverse of a list is what you'd expect``() =
    reverse [1;2;3] |> should equal [3;2;1]

// 3.13. foldr from foldl
[<TestCase(1, 1)>]  // 1 - 0 = 1
[<TestCase(4, -2)>] // 1 - (2 - (3 - (4 - 0))) = -2
let ``foldr' of a list is what you'd expect``(n, expected) =
    let ns = Enumerable.Range(1, n) |> Seq.toList
    foldr' ns 0 (-) |> should equal expected

// 3.14. append from fold
[<Test>]
let ``append [foo] onto an empty list gives [foo]``() =
    append [] [3] |> should equal [3]

[<Test>]
let ``append foo onto a nonempty list puts foo on the end``() =
    append [1;2;3] [4] |> should equal [1;2;3;4]

// 3.15. flatmap
[<Test>]
let ``flatmap of an empty list is an empty list``() =
    flatmap [] |> should equal []

[<Test>]
let ``flatmap of a single list extracts the inner list``() =
    flatmap [[1;2;3]] |> should equal [1;2;3]
