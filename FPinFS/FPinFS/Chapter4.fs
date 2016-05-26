module FPinFS.Chapter4

type Maybe<'a> =
    | Nothing
    | Just of 'a
    member this.Map f =
        match this with
        | Nothing -> Nothing
        | Just a -> Just (f a)

// This is about all we can do without GetOrElse
//    member this.FlatMap f = 
//        match this with
//        | Nothing -> Nothing
//        | Just a -> f a

    member this.GetOrElse b =
        match this with
        | Nothing -> b
        | Just a -> a

    member this.FlatMap f =
        (this.Map f).GetOrElse(Nothing) // pitfalls of dot-notation?

    member this.OrElse (b : 'b Maybe) =
        match this with
        | Nothing -> b
        | Just a -> this