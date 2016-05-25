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

let rec flatten lists = match lists with
    | [] -> []
    | car :: cdr -> append car (flatten cdr)

let rec increment xs = match xs with
    | [] -> []
    | car :: cdr -> car+1 :: increment cdr

let rec map xs f = match xs with
    | [] -> []
    | car :: cdr -> (f car) :: map cdr f

// initial 3.19 implementation:
//let rec filter xs p = match xs with
//    | [] -> []
//    | car :: cdr -> 
//        if p car 
//        then car :: filter cdr p
//        else filter cdr p

let rec flatmap xs f = match xs with
    | [] -> []
    | car :: cdr -> append (f car) (flatmap cdr f)

let filter xs p = flatmap xs (fun a -> if (p a) then [a] else [])

let rec addlists xs ys = match (xs, ys) with
    | ([],_) -> []
    | (_,[]) -> []
    | (x::xs, y::ys) -> (x + y) :: addlists xs ys

let rec zipwith f xs ys = match (xs, ys) with
    | ([],_) -> []
    | (_,[]) -> []
    | (x::xs, y::ys) -> (f x y) :: zipwith f xs ys

let subseq super sub = 
    let rec subseq' super remainder target = 
        match (super, remainder) with
        | (_, []) -> true
        | ([], _) -> false
        | (x::xs, y::ys) when x = y -> subseq' xs ys target
        | (x::xs, y::ys) -> subseq' xs target target
    subseq' super sub sub

type 'a Tree =
    | Leaf of 'a
    | Branch of 'a Tree * 'a Tree

//let rec size tree = match tree with
//    | Leaf _ -> 1
//    | Branch (a,b) -> 1 + (size a) + (size b)

//let rec maxEl tree = match tree with
//    | Leaf n -> n
//    | Branch (a,b) -> max (maxEl a) (maxEl b)

//let rec depth tree = match tree with
//    | Leaf _ -> 0
//    | Branch (a,b) -> 1 + max (depth a) (depth b)

//let rec mapTree f tree = match tree with
//    | Leaf a -> Leaf (f a)
//    | Branch (a,b) -> Branch ((mapTree f a), (mapTree f b))

let rec foldTree leaf branch tree =
    match tree with
    | Leaf a -> leaf a
    | Branch (a,b) -> branch (foldTree leaf branch a) (foldTree leaf branch b)

let size tree =
    foldTree (fun a -> 1) (fun a b -> 1 + a + b) tree

let maxEl tree =
    foldTree id max tree

let depth tree =
    foldTree (fun a -> 0) (fun a b -> 1 + max a b) tree

let rec mapTree f tree =
    foldTree (f >> Leaf) (fun a b -> Branch (a, b)) tree