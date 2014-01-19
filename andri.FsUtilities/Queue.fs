namespace andri.Utilities
    open System.Collections.Generic
    open System
    type EnumerableQueue<'t>(maxCount:int) =
        inherit List<'t>(maxCount)
        member x.Enqueue o = x.Add(o)
        member x.Dequeue() = x.Remove(x.[0])
                
    type CyclicObservableQueue<'t>(maxCount:int) =
        //inherit List<'t>(maxCount)
        let internalList = new List<'t>(maxCount)

        let observers = new List<IObserver<IList<'t>>>()
        let notifyAll () = observers |> Seq.iter (fun o -> o.OnNext(internalList))
        member x.InternalList = internalList
        member x.Enqueue o =
            if(internalList.Count>=maxCount) then x.Dequeue() |> ignore
            internalList.Add(o)
            notifyAll()
        member x.Dequeue() =
            internalList.Remove(internalList.[0])
        interface IObservable<IList<'t>> with
            member x.Subscribe(o) =
                observers.Add(o)
                { new IDisposable with
                    member x.Dispose() = observers.Remove(o) |> ignore
                }