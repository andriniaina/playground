(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

let PRECISION = 9
let read f = f(Console.In.ReadLine())
let mpsToKph(v : double) = v * 3.6
let kphToMps(v : double) = v / 3.6

let trafficLightDataParser(s : string) =
    eprintfn "%s" s
    let [| distance; duration |] = s.Split(' ') |> Array.map double
    (distance, duration)

// READ INPUT
let maxSpeedAllowedInKph = read double
let nbLights = read int

let trafficLightData =
    [ for i in 0..nbLights - 1 do
          yield trafficLightDataParser(read string) ]

let maxSpeedAllowedInMps = maxSpeedAllowedInKph / 3.6

eprintfn "maxSpeedAllowedInKph=%f;maxSpeedAllowedInMps=%f" maxSpeedAllowedInKph maxSpeedAllowedInMps
eprintfn "nbLights=%i" nbLights

let computePossibleTimeIntervals t = Seq.initInfinite(fun i -> i * 2) |> Seq.map(fun i -> (double i * t, double(i + 1) * t))

let computePossibleSpeeds distance duration =
    computePossibleTimeIntervals duration
    |> Seq.map(fun (t1, t2) -> (distance / t2, distance / t1))
    // the speed must be convertible to a positive integer later...
    |> Seq.takeWhile(fun (v1, v2) -> mpsToKph v1 >= 1.0)

let speedIntervalsList =
    [ for distance, duration in trafficLightData do
          let speeds = computePossibleSpeeds distance duration |> Seq.toList
          // use Seq.cache here
          yield speeds ]

let intersectIntervals (intervalList1 : (double * double) list) (intervalList2 : (double * double) seq) =
    [ for v1, v2 in intervalList1 do
          let possibleIntersections = intervalList2 |> Seq.takeWhile(fun (w1, w2) -> w1 > v1 || w2 > v1)
          for w1, w2 in possibleIntersections do
              let borneGauche =
                  if v1 > w1 then v1
                  else w1 //Math.Max(v1,w1)

              let borneDroite =
                  if v2 < w2 then v2
                  else w2 //Math.Min(v2,w2)

              if borneGauche <= borneDroite then yield (borneGauche, borneDroite) ]

match speedIntervalsList with
| [] -> failwith "PAS BIEN"
| head :: tail ->
    let allIntersections = tail |> List.fold (fun state input -> intersectIntervals state input) head

    let allIntersectionsInKph =
        allIntersections
        |> List.map(fun (v1, v2) -> Math.Ceiling(mpsToKph v1), Math.Floor(mpsToKph v2))
        // when converted to KPH, the interval must have an integer inside it
        |> List.filter(fun (v1, v2) -> v1 <= v2)

    let maxSpeedInKph =
        allIntersectionsInKph
        |> Seq.choose(fun (v1, v2) ->
               if v1 > maxSpeedAllowedInKph then None
               else Some(Math.Min(maxSpeedAllowedInKph, v2)))
        |> Seq.head
        |> int

    printfn "%i" maxSpeedInKph
