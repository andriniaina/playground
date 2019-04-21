(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

let read f = Console.In.ReadLine() |> f
let expression = read string

let brackets = ['(',')';'[',']';'{','}']

let matches o c =
    List.contains (o,c) brackets

let parse (s:string) =
    s
    |> List.ofSeq
    |> List.fold (fun (state:char list) input -> 
        match input with 
        | '('| '['| '{' -> input::state
        | ')'|']'|'}' -> 
            match state with
            | [] -> ['!']
            | lastOpeningBracket::tail -> if matches lastOpeningBracket input then tail else '!'::tail //failwith "bam"
        | _ -> state
        ) []

let remaining = parse expression



printfn "%b" (remaining.Length=0)