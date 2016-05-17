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

type DeuceScoreData = 
| Deuce
| AdvantageServing
| AdvantageReceiving

type GameOverData = 
| ServingPlayerWon
| ReceivingPlayerWon

type TennisScore = 
| NormalScore of NormalScoreData
| DeuceScore of DeuceScoreData
| GameOver of GameOverData

let NormalScoreOf (s: Score) (r: Score) =
    NormalScore ([Serving, s; Receiving, r] |> Map.ofList)

let ScoreSucc s = match s with
    | Love -> Fifteen
    | Fifteen -> Thirty
    | Thirty -> Forty
    | Forty -> Game

let StartGame = NormalScore ([Serving, Love; Receiving, Love] |> Map.ofList)

let PointFor (p : Player) (s: TennisScore) : TennisScore =
    match s with
    | NormalScore n when (n.[p] = Forty) ->
        match p with
        | Serving -> (GameOver ServingPlayerWon)
        | Receiving -> (GameOver ReceivingPlayerWon)
    | NormalScore n -> NormalScore (n.Add(p, ScoreSucc n.[p]))
    | GameOver w -> s