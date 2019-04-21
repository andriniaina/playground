(* Don't let the machines win. You are humanity's last hope... *)
open System
open System.Linq

let read f = Console.In.ReadLine() |> f

let width = read int (* the number of cells on the X axis *)
let height = read int (* the number of cells on the Y axis *)
let lines = [
    for y in 0 .. height - 1 do
        let sline = read string
        eprintfn "%s" sline
        let line = sline |> Seq.mapi (fun x c -> c='0') |> List.ofSeq
        yield line
]

let getNeighbor (x,y) (grid: bool list list) = 
    let currentPoint = grid.[x].[y]
    if not currentPoint then failwithf "(%i,%i) is not a node" x y
    else
        eprintfn "%i %i" x y
        let right = grid.[y] |> List.
        let right = if x+1< width && grid.[y].[x+1] then (x+1,y) else (-1,-1)
        let bottom = if y+1< height && grid.[y+1].[x] then (x,y+1) else (-1,-1)

        right, bottom

let firstPoint = lines |> List.head |> List.findIndex id
for y in 0 .. height - 1 do
    let line = lines.[y]
    for x in 0 .. width - 1 do
        let point = line.[x]
        let isNode = point
        if not isNode then ignore 0
        else
            let coords = (x,y)
            let right,bottom = getNeighbor coords lines
            printfn "%i %i %i %i %i %i" x y (fst right) (snd right) (fst bottom) (snd bottom)


