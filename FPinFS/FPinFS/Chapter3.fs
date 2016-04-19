module FPinFS.Chapter3

let tail els = match els with
    | [] -> []
    | i :: is -> is

let setHead els h = match els with
    | [] -> []
    | i :: is -> h :: is

let rec drop els n = match els with
    | [] -> []
    | i :: is -> if n <= 0 then (i :: is) else drop is (n-1)

let rec dropWhile els p = match els with
    | [] -> []
    | i :: is -> if p i then dropWhile is p else (i :: is)

let rec init els = match els with
    | [] -> []
    | [_] -> []
    | i :: is -> i :: init is