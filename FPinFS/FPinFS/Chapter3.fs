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

let rec foldr els i f = match els with
    | [] -> i
    | x :: xs -> f x (foldr xs i f)

let length els =
    foldr els 0 (fun el l -> 1+l)

let rec foldl els i f = match els with
    | [] -> i
    | x :: xs -> foldl xs (f i x) f

let rec reverse els =
    //foldr els [] (fun head reversed -> reversed @ [head]) // You could do it this way, too, though it's not tail-recursive
    foldl els [] (fun reversed head -> head :: reversed)

let foldr' els i f = foldl (reverse els) i (fun a b -> f b a)

//let append xs ys = foldr xs ys (fun a b -> a :: b)
let append xs ys = foldl (reverse xs) ys (fun a b -> b :: a)

let rec flatmap lists = match lists with
    | [] -> []
    | list :: lists -> append list (flatmap lists)