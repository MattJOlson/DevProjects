module TennisTests

open Tennis_2016_05_17_fsharp
open NUnit.Framework
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

[<Test>]
let ``When the receiving player scores from Forty-Thirty the score is Deuce`` () =
    let score = NormalScoreOf Forty Thirty |> (PointFor Receiving)
    score |> should equal (DeuceScore Deuce)

[<Test>]
let ``When the serving player scores from Deuce the score is AdvantageServing`` () =
    let score = (DeuceScore Deuce) |> (PointFor Serving)
    score |> should equal (DeuceScore AdvantageServing)

[<Test>]
let ``When the receiving player scores from AdvantageServing the score is Deuce`` () =
    let score = (DeuceScore AdvantageServing) |> (PointFor Receiving)
    score |> should equal (DeuceScore Deuce)

[<Test>]
let ``When the serving player scores from AdvantageServing the score is ServingPlayerWon`` () =
    let score = (DeuceScore AdvantageServing) |> (PointFor Serving)
    score |> should equal (GameOver ServingPlayerWon)