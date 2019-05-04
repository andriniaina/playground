(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

type IOSideEffect<'a> = IO of 'a

type IOBuilder() =
    member x.Bind(expr, func) =
        match expr with
        | IO(v) -> (func v)

    member x.Return v = IO(v)

let ConsoleRead() = IO(stdin.ReadLine())
let iobuilder = IOBuilder()
let readConsole f = iobuilder {
    let! data = ConsoleRead()
    return f data
}
let input = readConsole int
