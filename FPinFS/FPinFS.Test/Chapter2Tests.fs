module FPinFS.Test.Chapter2

open NUnit.Framework
open FsUnit
open FPinFS.Chapter2

[<TestCase(0, 0)>]
[<TestCase(1, 1)>]
[<TestCase(2, 1)>]
[<TestCase(3, 2)>]
[<TestCase(4, 3)>]
[<TestCase(6, 8)>]
let ``fib implements fibonacci sequence``(a, expected) =
    fib a |> should equal expected

//let rec isSorted f els =
//    match els with
//    | [] -> true
//    | [_] -> true
//    | a :: b :: cs -> (f a b) && isSorted f (b :: cs)

[<Test>]
let ``isSorted returns true on an empty list``() =
    isSorted (<=) [] |> should equal true

[<Test>]
let ``isSorted detects a sorted list``() =
    isSorted (<=) [1; 2; 4; 8] |> should equal true