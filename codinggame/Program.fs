(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System


let result1 = Enigma.ceasarReverse 4 "EFG"
printfn "%s" result1
let x = Enigma.rotor "ABCDEFGHIJKLMNOPQRSTUVWXYZ" "BDFHJLCPRTXVZNYEIWGAKMUSQO" "EFG"
let chars = Enigma.decode 9 ("BDFHJLCPRTXVZNYEIWGAKMUSQO","AJDKSIRUXBLHWTMCQGZNPYFVOE","EKMFLGDQVZNTOWYHXUSPAIBRCJ") "PQSACVVTOISXFXCIAMQEM"
let result = chars |> Array.ofSeq |> String
printfn "%s" result
Console.ReadLine()