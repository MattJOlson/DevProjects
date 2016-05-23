module Chapter4Tests

open FPinFS.Chapter4
open System.Linq
open NUnit.Framework
open FsUnit

// 4.1. Maybe.Map
[<Test>]
let ``Nothing.map(foo) is Nothing`` () =
    let nil = Nothing : Maybe<int>
    nil.Map(fun a -> a + 1) |> should equal nil

[<Test>]
let ``Just a.Map(foo) is Just (foo a)`` () =
    (Just 42).Map(fun a -> a.ToString()) |> should equal (Just "42")