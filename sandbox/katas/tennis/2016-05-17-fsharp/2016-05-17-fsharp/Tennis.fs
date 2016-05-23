module Tennis_2016_05_17_fsharp

type Player =
| Serving
| Receiving

type Score =
| Love
| Fifteen
| Thirty
| Forty

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

let ScoreSucc s =
    match s with
    | Love -> Fifteen
    | Fifteen -> Thirty
    | Thirty -> Forty
    | Forty -> failwith "Succ Forty is not a normal score!"

let StartGame = NormalScoreOf Love Love

let PossiblyDeuce n =
    if n = (NormalScoreOf Forty Forty) then DeuceScore Deuce
    else n

let DeclareWinner p =
    match p with
    | Serving -> GameOver ServingPlayerWon
    | Receiving -> GameOver ReceivingPlayerWon

let HasAdvantage p s =
    match p with
    | Serving -> s = AdvantageServing
    | Receiving -> s = AdvantageReceiving

let GrantAdvantage p =
    match p with
    | Serving -> DeuceScore AdvantageServing
    | Receiving -> DeuceScore AdvantageReceiving

let ResolveDeuce p s =
    match s with
    | Deuce -> GrantAdvantage p
    | _ when HasAdvantage p s -> DeclareWinner p
    | _ -> DeuceScore Deuce

let PointFor (p : Player) (s: TennisScore) : TennisScore =
    match s with
    | DeuceScore d -> ResolveDeuce p d
    | NormalScore n when (n.[p] = Forty) -> DeclareWinner p
    | NormalScore n -> NormalScore (n.Add(p, ScoreSucc n.[p])) |> PossiblyDeuce
    | GameOver _ -> s