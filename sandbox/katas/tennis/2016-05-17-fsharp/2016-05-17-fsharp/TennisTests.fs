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

[<Test>]
let ``When the serving player scores twice the score is Thirty-Love`` () =
    let score = StartGame |> (PointFor Serving) |> (PointFor Serving)
    score |> should equal (NormalScoreOf Thirty Love)

[<Test>]
let ``When the serving player scores from Forty-Love the score is ServingPlayerWon`` () =
    let score = NormalScoreOf Forty Love |> (PointFor Serving)
    score |> should equal (GameOver ServingPlayerWon)

[<Test>]
let ``When the game is over "scoring more points" doesn't count`` () =
    let score = (GameOver ServingPlayerWon)
    score |> (PointFor Receiving) |> should equal (GameOver ServingPlayerWon)