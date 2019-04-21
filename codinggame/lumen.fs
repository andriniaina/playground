open System

let read f = Console.In.ReadLine() |> f
let N = read int
let L = read int
let map = [
    for y in 0 .. N - 1 do
        yield System.Collections.Generic.List<int>(List.init N (fun i -> 0))
]

let lightSources = [
    for y in 0 .. N - 1 do
        let sline = read string
        let sources = sline |> Seq.mapi (fun x c -> if c='C' then Some(x,y,L) else None)
        let xx = sources |> Seq.choose (fun p -> match p with | Some(x,y,L) as oo -> oo | _ -> None)
        yield! xx |> List.ofSeq
]
(*
let apply (map:System.Collections.Generic.List<int> list) (x,y,p)  = 
    let l  = map.[x].[y]
    match l with
    | 0 -> map
    | _ when l<p -> 

let rec propagate map lightSources= 
    match lightSources with 
    | source::rest ->
        let newMap = apply map source
        propagate newMap rest
    | [] -> map

let xxx = propagate map lightSources

printfn "2"
*)
