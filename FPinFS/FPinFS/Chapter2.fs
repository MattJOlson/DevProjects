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

let curry (f :('a * 'b) -> 'c) =
    fun x y -> f (x,y)

let uncurry (f :'a -> 'b -> 'c) =
    fun (a, b) -> f a b

let compose f g = // idiomatically f << g
    fun x -> f (g x)