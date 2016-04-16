module FPinFS.Test.Chapter2

open FPinFS.Chapter2
open NUnit.Framework
open FsUnit

// 2.1. Fibonacci
[<TestCase(0, 0)>]
[<TestCase(1, 1)>]
[<TestCase(2, 1)>]
[<TestCase(3, 2)>]
[<TestCase(4, 3)>]
[<TestCase(6, 8)>]
let ``fib implements fibonacci sequence``(a, expected) =
    fib a |> should equal expected

// 2.2. IsSorted
[<Test>]
let ``when input is empty, isSorted returns true``() =
    isSorted (<=) [] |> should equal true

[<Test>]
let ``when input is sorted, isSorted returns true``() =
    isSorted (<=) [1; 2; 4; 8] |> should equal true

[<Test>]
let ``when input isn't sorted, isSorted returns false``() =
    isSorted (<=) [1; 2; 1; 4] |> should equal false

// 2.3. Currying
[<Test>]
let ``when given a function on pairs, curry returns a curried function``() =
    let curried = curry (fun (a, b) -> a + b) 42
    curried 27 |> should equal 69

[<Test>]
let ``when given a function on pairs with three types, curry still returns a curried function``() =
    let curried = curry (fun (a,b) -> a.ToString() + b.ToString()) 42
    curried true |> should equal "42True"

// 2.4. Uncurrying
[<Test>]
let ``when given a partially-applicable function, uncurry returns a function on pairs``() =
    uncurry (fun x y -> x.ToString() + y.ToString()) (42, true) |> should equal "42True"