(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

let read f = Console.ReadLine() |> f
let N = int(Console.In.ReadLine()) (* Number of elements which make up the association table. *)
let Q = int(Console.In.ReadLine()) (* Number Q of file names to be analyzed. *)
let mappingSequenceReader = seq {
    for i in 0 .. N - 1 do
        (* EXT: file extension *)
        (* MT: MIME type. *)
        let token = (Console.In.ReadLine()).Split [|' '|]
        let EXT = token.[0].ToUpper()
        let MT = token.[1]
        yield (EXT,MT)
}

let mapping = mappingSequenceReader |> Map.ofSeq

let UNKNOWN = "UNKNOWN"

(* For each of the Q filenames, display on a line the corresponding MIME type. If there is no corresponding type, then display UNKNOWN. *)
for i in 0 .. Q - 1 do
    let FNAME = Console.In.ReadLine() (* One file name per line. *)
    let EXT_WITH_POINT = System.IO.Path.GetExtension(FNAME).ToUpper()
    let EXT = if EXT_WITH_POINT.Length>1 then EXT_WITH_POINT.Substring(1) else EXT_WITH_POINT
    let MT =
        match EXT with
        | "" -> UNKNOWN
        | s -> if mapping.ContainsKey(s) then mapping.[s] else UNKNOWN
    printfn "%s" MT


(* Write an action using printfn *)
(* To debug: eprintfn "Debug message" *)



