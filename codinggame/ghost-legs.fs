(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System
open System.Linq.Expressions

let STEP = 3

let token = (Console.In.ReadLine()).Split [|' '|]
let W = int(token.[0])
let H = int(token.[1])

let startLabels = Console.In.ReadLine()//.Split(' ')
let rows = [
    for i in 1 .. H - 2 do
        yield Console.In.ReadLine()
]
let endLabels = Console.In.ReadLine()//.Split(' ')

let rec getSolution startPos (rows:string list) = 
    match rows with
    | [] -> endLabels.[startPos]
    | currentRow::restOfRows ->
        match currentRow with 
        // should go left
        | row when startPos>0 && row.[startPos-1]='-' -> getSolution (startPos - STEP) ((row.Replace('-',' '))::restOfRows)
        | row when startPos<W-1 && row.[startPos+1]='-' -> getSolution (startPos + STEP) ((row.Replace('-',' '))::restOfRows)
        | _ -> getSolution (startPos) (restOfRows)

for p in 0 .. STEP .. W do
    let startLabel = startLabels.[p]
    let endLabel = getSolution p rows
    printfn "%c%c" startLabel endLabel


