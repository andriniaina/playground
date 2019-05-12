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

[<EntryPoint>]
let main args =
    printfn "Arguments passed to function : %A" args
    // Return 0. This indicates success.
    0