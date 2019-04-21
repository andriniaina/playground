open System

let read f = f(stdin.ReadLine())
let r1 = 13L// read int64
eprintfn "r1=%i" r1

let getDigits n = [for c in n.ToString() do yield int64 c - 48L]
let river (n:int64) = 
    Seq.unfold (fun i ->
        let next = i + Seq.sum (getDigits i)
        Some(i,next) ) n

let findSources r =
    seq { r .. -1L .. 0L } |> Seq.map river |> Seq.map (Seq.take 2 >> List.ofSeq) |> Seq.choose (fun l -> if l.[0]=r || l.[1]=r then Some(l.[0]) else None) // get all possible previous river

let hasIntersection =
    findSources >> (Seq.tryItem 1) >> Option.isSome

let toYesNo b = if b then "YES" else "NO"
printfn "%s" (hasIntersection r1 |> toYesNo)
