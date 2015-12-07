module FPinFS.Chapter2

let rec fib n =
    if n = 0 then 0
    else if n = 1 then 1
    else fib (n-1) + fib(n-2)

let rec isSorted f els =
    match els with
    | [] -> true
    | [_] -> true
    | a :: b :: cs -> (f a b) && isSorted f (b :: cs)