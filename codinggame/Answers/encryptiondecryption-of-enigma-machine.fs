module Enigma

open System

let read f = stdin.ReadLine() |> f
let Operation = read string
let pseudoRandomNumber = read int
let rotors = [
    for i in 0 .. 3 - 1 do
        let rotor = read string
        yield rotor
]
let message = read string
eprintfn "message=%s" message


let rotor m1 m2 s =
    let toTuple a b = a,b
    let toElput a b = b,a
    let map1 = m1 |> Seq.mapi (toElput) |> Map.ofSeq
    let map2 = m2 |> Seq.mapi (toTuple) |> Map.ofSeq
    s |> Seq.map (map1.TryFind >> Option.get >> map2.TryFind >> Option.get)

let shiftChar c x = char(((int(c)-65+x) % 26)+65)
let ceasarShift n (s:char seq) =
    s |> Seq.mapFold (fun state c -> shiftChar c state,state+1) n |> fst |> Array.ofSeq |> String
let ceasarReverse n (s:char seq) =
    s |> Seq.mapFold (fun state c -> shiftChar c (-state+26*5),state+1) n |> fst |> Array.ofSeq |> String


let ABCDEFGHIJKLMNOPQRSTUVWXYZ = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
let encode n (r1,r2,r3) =
    ceasarShift n >> rotor ABCDEFGHIJKLMNOPQRSTUVWXYZ r1 >> rotor ABCDEFGHIJKLMNOPQRSTUVWXYZ r2 >> rotor ABCDEFGHIJKLMNOPQRSTUVWXYZ r3
let decode n (r1,r2,r3) =
    rotor r3 ABCDEFGHIJKLMNOPQRSTUVWXYZ >> rotor r2 ABCDEFGHIJKLMNOPQRSTUVWXYZ >> rotor r1 ABCDEFGHIJKLMNOPQRSTUVWXYZ >> ceasarReverse n


let charSeqToString = Array.ofSeq >> String

eprintfn "n=%i, %A" pseudoRandomNumber rotors
type Action = | ENCODE | DECODE
let convertAction = function | "ENCODE" -> ENCODE | "DECODE" -> DECODE | _ -> failwith "unable to parse"
let executeAction = function
    | ENCODE -> encode pseudoRandomNumber (rotors.[0], rotors.[1], rotors.[2]) message |> charSeqToString
    | DECODE -> decode pseudoRandomNumber (rotors.[0], rotors.[1], rotors.[2]) message |> charSeqToString

let result = Operation |> convertAction |> executeAction
printfn "%s" (result)