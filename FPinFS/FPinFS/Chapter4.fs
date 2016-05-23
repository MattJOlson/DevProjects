module FPinFS.Chapter4

type Maybe<'a> =
    | Nothing
    | Just of 'a
    member this.Map f =
        match this with
        | Nothing -> Nothing
        | Just a -> Just (f a)