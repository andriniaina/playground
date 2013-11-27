namespace andri.Utilities
    module Finance =
        let private stdDevArray array =
            let avg = Array.average array
            sqrt (Array.fold (fun acc elem -> acc + (float elem - avg) ** 2.0 ) 0.0 array / float array.Length)
        
        let vol = stdDevArray
        

