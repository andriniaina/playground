namespace andri.BtcClient
    open System
    open Newtonsoft.FsJson
    open System.Collections.Generic
    
    type Ticker = {
        Name:string
        Last:seq<DateTime*float>
        }
    [<AbstractClass>]
    type LiveTickerProvider(name:string) =
        member val Name:string = name
        abstract TickerUpdated:IEvent<Ticker> with get
        (*
    [<AbstractClass>]
    type AbstractLiveTickerCollection() =
        abstract Add : LiveTickerProvider -> unit
        abstract TickerUpdated:IEvent<Ticker> with get
    *)
    type LiveTickerCollection(providers:IList<LiveTickerProvider>) =
        //inherit LiveTickerCollection()
        let tickerUpdated = new Event<Ticker>()
        let bubbleEvent (arg:Ticker) =
            tickerUpdated.Trigger(arg)
        member x.Add (item:LiveTickerProvider) =
            providers.Add(item)
            item.TickerUpdated |> Event.add bubbleEvent
        member x.TickerUpdated with get() = tickerUpdated.Publish
