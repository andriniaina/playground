// https://www.codingame.com/ide/puzzle/chuck-norris
(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
module ChuckNorris

open System
open System.Collections
open System.Numerics

let MESSAGE = Console.In.ReadLine()

// "ABCDE"
// 1000001 1000010 1000011 1000100 1000101
// 10000011000010100001110001001000101
// 0 0 00 00000
// "C": 1000011: 0 0 00 0000 0 00
(* Write an action using printfn *)
(* To debug: eprintfn "Debug message" *)

let bytes =
    seq {
        for c in MESSAGE do
            yield BitConverter.GetBytes(c).[0]
    }

let byteArray =
    bytes
    |> Seq.map (fun b -> new BitArray([| b |]))
    |> Seq.map (fun b ->
           b
           |> Seq.cast<bool>
           |> Seq.rev
           |> Seq.tail)
    |> Seq.map (fun b -> new BitArray(Array.ofSeq b))

let bits =
    byteArray
    |> Seq.collect (fun b -> Seq.cast<bool> b)
    |> List.ofSeq

let firstBit = bits |> Seq.head
let initialList = List.empty<bool * int>

let (incompleteList, last) =
    bits
    |> Seq.tail
    |> Seq.fold (fun (l, (previousbit, count)) next ->
           if next = previousbit then (l, (next, count + 1))
           else ((previousbit, count) :: l, (next, 1))) (initialList, (firstBit, 1))

let finalList = last :: incompleteList |> List.rev
let zeroGenerator n = String.init n (fun i -> "0")
let blockPairs =
    finalList |> List.map (fun (b, count) -> sprintf "%s %s" (if b then "0" else "00") (zeroGenerator count))

printfn "%s" (String.Join(" ", blockPairs))
Console.In.ReadLine()
