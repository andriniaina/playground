namespace andri.Utilities
    open System.Collections.Generic
    type EnumerableQueue<'t>(maxCount:int) =
        inherit List<'t>(maxCount)
        member x.Enqueue o = x.Add(o)
        member x.Dequeue() = x.Remove(x.[0])

