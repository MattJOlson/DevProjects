namespace FPinFS.Test

open NUnit.Framework
open FsUnit
open FPinFS.Chapter2

module Chapter2Test =

    [<TestCase(0, 0)>]
    [<TestCase(1, 1)>]
    [<TestCase(2, 1)>]
    [<TestCase(3, 2)>]
    [<TestCase(4, 3)>]
    [<TestCase(6, 8)>]
    let ``fib implements fibonacci sequence``(a, expected) =
        fib a |> should equal expected