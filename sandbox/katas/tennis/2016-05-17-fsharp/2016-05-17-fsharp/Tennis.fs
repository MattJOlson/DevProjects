module Tennis_2016_05_17_fsharp

type Player =
| Serving
| Receiving

type Score = // Would be nice to make this ordinal, maybe?
| Love
| Fifteen
| Thirty
| Forty
| Advantage
| Game

type NormalScoreData = Map<Player, Score>

type DeuceScoreData = unit

type GameOverData = unit

type TennisScore = 
| NormalScore of NormalScoreData
| DeuceScore of DeuceScoreData
| GameOver of GameOverData

let NormalScoreOf (s: Score) (r: Score) =
    NormalScore ([Serving, s; Receiving, r] |> Map.ofList)

let StartGame = NormalScore ([Serving, Love; Receiving, Love] |> Map.ofList)

let PointFor (p : Player) (s: TennisScore) : TennisScore =
    match s with
    | NormalScore n -> NormalScore (n.Add(p, Fifteen))