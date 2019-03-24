(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System
open System.Linq

let read f =  Console.In.ReadLine() |> f
let R = read int
let V = read int
eprintfn "R=%i,V=%i" R V
let vaults = [
    for i in 0 .. V - 1 do
        let line = (read string).Trim()
        eprintfn "%s" line
        let cols = line.Split(' ')
        if cols.Length=2 then
            let [|nbChars;nbDigits|] = cols |> Array.map int
            yield (nbChars,nbDigits)
        else
            eprintfn "uknown format: %s" line
]

let numberOfPossibilities (nbDigitsAndVowels,nbDigits) =
    let nbVowels = nbDigitsAndVowels - nbDigits
    Math.Pow(10.0, float nbDigits)*Math.Pow(5.0, float nbVowels)

let computeTime = numberOfPossibilities

let computeTimes =
    vaults
    |> List.map computeTime
    // optimized worker mode, add this line (not asked by the exercice):
    // |> List.sortDescending

let robberWorkQueue = List.init R (fun i -> List<float>.Empty)
let rec solve (computeTimes:float list) robberWorkQueue = 
    match computeTimes with
    | [] -> robberWorkQueue
    | head::tail ->
        let freeWorker::workers = robberWorkQueue |> List.sortBy (fun q -> List.sum q)
        solve tail ((head::freeWorker)::workers)

let solvedQueues =  solve computeTimes  robberWorkQueue 
let workingTimes = solvedQueues |> List.map List.sum
let max = workingTimes |> List.max |> int
printfn "%i" max


