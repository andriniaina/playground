(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

open Winamax


let w,h,map = 3,3,[Ball(2);Grass;Water;Water;Grass;Hole(false);Grass;Hole(false);Ball(1)]

(*

let d = findPossibleLandingSpot 0 (3,3,map) |> List.ofSeq
assert (d.Length=2)
let newMaps = shootEverywhere 0 2 (w,h,map) |> List.ofSeq

for map in newMaps do
    stdout.WriteLine (dump (w,h,map))
*)

let solutions = findSolutions (w,h,map) |> List.ofSeq
for map in solutions do
    let isAllHolesFilled = true //map |> List.filter (isHole) |> List.forall (isFilledHole)
    if isAllHolesFilled then
        stdout.WriteLine()
        dump (w,h,map)

Console.ReadLine() |> ignore