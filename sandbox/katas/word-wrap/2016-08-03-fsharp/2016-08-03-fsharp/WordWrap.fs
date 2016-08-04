module WordWrap

open System.Text.RegularExpressions

let words t = Regex.Split(t, "( )") |> Seq.toList

let wrap corpus len =
    let rec wrapRec (wip:string) (currsep:string) (currlen:int) (rest:string list) = match rest with
        | [] -> wip
        | [w] when currlen + w.Length + 1 <= len -> wip + currsep + w
        | [w] when wip = "" -> w
        | [w] when len < currlen + w.Length + 1 -> wip + "\n" + w
        | w :: s :: ws when currlen + w.Length + 1 < len -> wrapRec (wip + w) s (currlen + 1 + w.Length) ws
        | w :: s :: ws when currlen + w.Length + 1 = len -> wrapRec (wip + w) "\n" (currlen + 1 + w.Length) ws
        | w :: s :: ws when len < currlen + w.Length + 1 -> wrapRec wip "\n" 0 (w :: ws)
    wrapRec "" "" 0 (words corpus)