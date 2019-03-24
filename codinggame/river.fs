(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

let read f = f(Console.In.ReadLine())
let n1 = read int64
let n2 = read int64

let getDigits n = [for c in n.ToString() do yield int64 c - 48L]
let river (n:int64) = 
    Seq.unfold (fun i ->
        let next = i + Seq.sum (getDigits i)
        Some(i,next) ) n
  
let river1 = river n1
let river2 = river n2

let iter1 = river1.GetEnumerator() in iter1.MoveNext() |> ignore
let iter2 = river2.GetEnumerator() in iter2.MoveNext() |> ignore

while iter1.Current<>iter2.Current do
    let current1 = iter1.Current
    let current2 = iter2.Current
    if current1<current2 then
        (iter1.MoveNext()) |> ignore
    else
        if current2<current1 then
            iter2.MoveNext() |> ignore


printfn "%i" iter1.Current


