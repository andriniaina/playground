(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
open System

let LON = float (Console.In.ReadLine())
let LAT = float (Console.In.ReadLine())
let N = int (Console.In.ReadLine())
let asFloat (s : string) = float (s.Replace(',', '.'))

let defibsReader =
    seq {
        for i in 0..N - 1 do
            let [| id; name; address; phonenum; slon; slat |] =
                Console.In.ReadLine().Split(';')
            yield (id, name, address, phonenum, asFloat slon, asFloat slat)
    }

let defibs = List.ofSeq (defibsReader)

let computeDistance (longitudeA : float, latitudeA : float)
    (longitudeB : float, latitudeB : float) =
    let x = (longitudeB - longitudeA) * Math.Cos((latitudeA + latitudeB) / 2.0)
    let y = latitudeB - latitudeA
    let d = Math.Sqrt(x * x + y * y) * 6371.0
    d

let computeDistance2 (longitudeA, latitudeA)
    (id, name, address, phonenum, lon, lat) =
    let d = computeDistance (longitudeA, latitudeA) (lon, lat)
    (name, d)

let distances =
    defibs |> List.map (fun defib -> computeDistance2 (LON, LAT) (defib))
let name, d = distances |> List.minBy (fun (name, d) -> d)

(* Write an action using printfn *)
(* To debug: eprintfn "Debug message" *)

printfn "%s" name
