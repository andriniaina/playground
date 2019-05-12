(* Auto-generated code below aims at helping you parse *)
(* the standard input according to the problem statement. *)
module Winamax
open System

type Direction = | North | South | East | West 
type Point = | Grass | Hole of bool | Water | Ball of int | Occupied of Direction
let isBall = function | Ball(_) -> true | _ -> false
let isHole = function | Hole(_) -> true | _ -> false
let isEmptyHole = function | Hole(false) -> true | _ -> false
let isFilledHole = function | Hole(true) -> true | _ -> false
let canLand = function | Grass | Hole(false) -> true | _ -> false
let canCross = function | Water -> true | _ -> false
let canCrossOrLand p = canLand p || canCross p || isEmptyHole p

type TerrainMap = int*int*Point list
let read f = stdin.ReadLine>>f
let [|w;h|] = (read string ()).Split [|' '|] |> Array.map (int)
let parsePoint = function
    | '.' -> Grass
    | 'H' -> Hole(false)
    | 'X' -> Water
    | c -> Ball(Int32.Parse(string c))
let map = [
    eprintfn "Problem:"
    for i in 0 .. h - 1 do
        let line = read string ()
        let row = line |> Seq.map (parsePoint)
        eprintfn "%s" line
        yield! row
]

let copyWith x p l = List.init (List.length l) (fun i -> if x=i then p else l.[i] )
let walkNorth start finish w = seq { start .. -w .. finish}
let walkSouth start finish w = seq {start .. w .. finish }
let walkEast start finish = seq {start .. 1 .. finish}
let walkWest start finish = seq {start .. -1 .. finish}
let getPoints (map:Point list) s = Seq.map (fun i -> (map.[i],i)) s 

let findPossibleLandingSpot start (w,h,map) = 
    let takeUntil f (s:seq<'a>) =
        seq {
            let enumerator = s.GetEnumerator()
            let mutable stop = false
            while stop || enumerator.MoveNext() do
                let p = enumerator.Current
                stop <- f p
                yield p
        }
    let northDirections =  walkNorth start 0 w |> Seq.filter((<>)start) |> getPoints map |> Seq.takeWhile (fst>>canCrossOrLand) |> Seq.choose (fun (p,i)-> if canLand p then Some(North,i,p)  else None)
    let southDirections = walkSouth start (w*h-1) w |> Seq.filter((<>)start) |> getPoints map |> Seq.takeWhile (fst>>canCrossOrLand ) |> Seq.choose (fun (p,i)-> if canLand p then Some(South,i,p)  else None)
    let eastDirections =  walkEast start (start-(start%w)+w-1) |> Seq.filter((<>)start) |> getPoints map |> Seq.takeWhile ( fst>> canCrossOrLand ) |> Seq.choose (fun (p,i)-> if canLand p then Some(East,i,p)  else None)
    let westDirections = walkWest start (start-(start%w)) |> Seq.filter((<>)start) |> getPoints map |> Seq.takeWhile (fst>> canCrossOrLand ) |> Seq.choose (fun (p,i)-> if canLand p then Some(West,i,p)  else None)

    westDirections |> Seq.append eastDirections |> Seq.append northDirections |> Seq.append southDirections
let shootEverywhere start startingScore (w,h,map) =
    let aimAndCreateNewMap direction finish map =
        let petitPoucet d initialMap = Seq.fold (fun _map i -> copyWith (i) (Occupied(d)) (_map) ) initialMap
        match direction with 
        | North -> walkNorth start finish w |> petitPoucet direction map
        | South -> walkSouth start finish w |> petitPoucet direction map
        | East -> walkEast start finish |> petitPoucet direction map
        | West -> walkWest start finish |> petitPoucet direction map
    let directions = findPossibleLandingSpot start (w,h,map)
    directions |> Seq.map (fun (d,i,p) -> aimAndCreateNewMap d i map |> copyWith (i) (if isHole(map.[i]) then Hole(true) else Ball(startingScore-1)) )


let rec findSolutions (w,h,map:Point list) : seq<Point list> = 
    let possibleShots = map |> Seq.indexed |> Seq.collect (fun (start,p) ->
        match p with
        | Ball(x) -> if x>0 then shootEverywhere start x (w,h,map) else Seq.empty<Point list>
        | _ -> Seq.empty<Point list> )
    seq {
        yield! possibleShots
        for newmap in possibleShots do
            let solutions = findSolutions (w,h,newmap)
            yield! solutions
    }

let dump (w,h,map:Point list) =
    let toString = function 
        | Grass
        | Hole(true)
        | Water -> '.'
        | Hole(false) -> 'H'
        | Ball(x) -> string x |> Seq.head
        | Occupied(North) -> '^'
        | Occupied(South) -> 'v'
        | Occupied(West) -> '<'
        | Occupied(East) -> '>'

    for j in 0 .. h - 1 do
        for i in 0 .. w - 1 do
            printf "%c" (map.[h*j+i] |> toString)
        printf "\n"


let dumpForDebug (w,h,map:Point list) =
    let toString = function 
        | Grass -> '.'
        | Hole(true) -> 'B'
        | Water -> 'X'
        | Hole(false) -> 'H'
        | Ball(x) -> string x |> Seq.head
        | Occupied(North) -> '^'
        | Occupied(South) -> 'v'
        | Occupied(West) -> '<'
        | Occupied(East) -> '>'

    eprintfn "Solution:"
    for j in 0 .. h - 1 do
        for i in 0 .. w - 1 do
            printf "%c" (map.[h*j+i] |> toString)
        printf "\n"


let isAllHolesFilled = List.filter (isHole) >> List.forall (isFilledHole)
let hasBall = List.exists (isBall)
let solution = findSolutions (w,h,map) |> Seq.find (fun map -> isAllHolesFilled(map) && not(hasBall(map)) )
dump (w,h,solution)