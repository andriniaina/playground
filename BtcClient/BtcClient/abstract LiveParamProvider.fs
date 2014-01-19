namespace andri.BtcClient
    open System
    open Newtonsoft.FsJson
    open System.Collections.Generic
    
    type Tick = {Now:DateTime; Last:float; Vwap:float; Bid:float; Ask:float; }

    type Ticker = {
        Name:string
        History:seq<Tick>
        }
    [<AbstractClass>]
    type LiveTickerProvider(name:string) =
        member x.Name = name
        abstract member Data:IObservable<IList<Tick>>
        (*
    type LiveTickerCollection(providers:IList<LiveTickerProvider>) =
        let tickerUpdated = new Event<Ticker>()
        let bubbleEvent (arg:Ticker) =
            tickerUpdated.Trigger(arg)
        member x.Add (item:LiveTickerProvider) =
            providers.Add(item)
            item.TickerUpdated |> Event.add bubbleEvent
        member x.TickerUpdated with get() = tickerUpdated.Publish
*)