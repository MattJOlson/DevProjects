module Chapter4Tests

open FPinFS.Chapter4
open System.Linq
open NUnit.Framework
open FsUnit

let nil = Nothing : Maybe<int>

// 4.1. Maybe.Map
[<Test>]
let ``Nothing.Map(foo) is Nothing`` () =
    nil.Map (fun a -> a + 1) |> should equal nil

[<Test>]
let ``Just a.Map(foo) is Just (foo a)`` () =
    (Just 42).Map (fun a -> a.ToString()) |> should equal (Just "42")

// 4.1. Maybe.FlatMap
let odd m = match m with
    | m when m % 2 = 0 -> Nothing
    | m -> Just m

[<Test>]
let ``Nothing.FlatMap(foo) is still Nothing`` () =
    nil.FlatMap odd |> should equal nil

[<Test>]
let ``Just a.FlatMap(foo) is foo a`` () =
    (Just 42).FlatMap odd |> should equal nil
    (Just 43).FlatMap odd |> should equal (Just 43)

// 4.1. Maybe.GetOrElse
// Not quite the same thing as in the book as I'm not fiddling around with supertype constraints

[<Test>]
let ``Nothing.GetOrElse(foo) returns foo`` () =
    nil.GetOrElse 42 |> should equal 42

[<Test>]
let ``Just a.GetOrElse(foo) returns a`` () =
    (Just 42).GetOrElse 97 |> should equal 42

// 4.1. Maybe.OrElse
// Again kind of hampered by my momentary dilligaf about covariance in F# return types

[<Test>]
let ``Nothing.OrElse(foo) returns foo`` () =
    let nil' = Nothing : Maybe<int>
    nil.OrElse(nil') |> should equal nil'
    nil.OrElse(Just 39) |> should equal (Just 39)

[<Test>]
let ``Just a.OrElse(foo) returns Just a`` () =
    (Just 42).OrElse nil |> should equal (Just 42)