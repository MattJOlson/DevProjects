module TennisTests

open Tennis_2016_05_17_fsharp
open NUnit.Framework
open System.Linq
open FsUnit

[<Test>]
let ``When we StartGame the score is Love-Love`` () =
    StartGame |> should equal (NormalScoreOf Love Love)

[<Test>]
let ``When the serving player scores the first point the score is Fifteen-Love`` () =
    let score = StartGame |> (PointFor Serving)
    score |> should equal (NormalScoreOf Fifteen Love)