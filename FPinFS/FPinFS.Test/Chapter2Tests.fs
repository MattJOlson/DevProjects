module FPinFS.Test.Chapter2

open FPinFS.Chapter2
open NUnit.Framework
open FsUnit

[<TestCase(0, 0)>]
[<TestCase(1, 1)>]
[<TestCase(2, 1)>]
[<TestCase(3, 2)>]
[<TestCase(4, 3)>]
[<TestCase(6, 8)>]
let ``fib implements fibonacci sequence``(a, expected) =
    fib a |> should equal expected

[<Test>]
let ``when input is empty, isSorted returns true``() =
    isSorted (<=) [] |> should equal true

[<Test>]
let ``when input is sorted, isSorted returns true``() =
    isSorted (<=) [1; 2; 4; 8] |> should equal true

[<Test>]
let ``when input isn't sorted, isSorted returns false``() =
    isSorted (<=) [1; 2; 1; 4] |> should equal false