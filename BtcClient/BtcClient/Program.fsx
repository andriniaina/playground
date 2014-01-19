#I @"..\packages\FSharp.Charting.0.90.5\lib\net40"
#I @"D:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug"
#I @"c:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug"
#I @"..\packages\MathNet.Numerics.2.6.2\lib\net40"
#I @"..\packages\MathNet.Numerics.FSharp.2.6.0\lib\net40"

#r @"PubNubMessaging.Core"
#r @"Newtonsoft.Json"
#r @"Fsharp.Charting"
#r @"System.Windows.Forms"
#r @"System.Windows.Forms.DataVisualization"
#load "../../andri.FsUtilities/Finance.fs"
#load "../../andri.FsUtilities/ServiceLocator.fs"
open andri.Utilities
#load "../../andri.FsUtilities/Web.fs"
#load "../../andri.FsUtilities/Queue.fs"
#load "../../andri.FsUtilities/Strings.fs"
#load "Log.fs"
#load "Newtonsoft.FsJson.fs"
#load "abstract LiveParamProvider.fs"
#load "_MtGox.fs"
open andri.BtcClient
#load "MtGoxStream.fs"
#load "MtGoxHttp.fs"
open andri.BtcClient


open System
open PubNubMessaging.Core
open andri.Utilities
open andri.BtcClient
open Newtonsoft.FsJson
open FSharp.Charting
open System.Collections.Generic
#I @"..\BtcClient.Data\bin\x86\Debug"
#r "BtcClient.Data"
#r "System.Data"
#r "System.Data.Linq"
open andri.BtcClient.Data

/// An event which triggers at regular intervals reporting the real world time at each trigger
let clock interval = 
    let out = new Event<_>()
    let timer = new System.Windows.Forms.Timer(Interval=interval, Enabled=true)
    timer.Tick.Add (fun args -> out.Trigger System.DateTime.Now)
    timer.Start()
    out.Publish
    
let providers = new List<LiveTickerProvider>()
let pubnub = new Pubnub("", MtGoxStream.PUBNUB_KEY)
let tickerEUR = MtGoxStream.LiveTickerFactory (new MtGoxStream.PubnubWrapper(pubnub), MtGoxStream.PUBNUB_CHANNELS.ticker_BTCEUR, "BTCEUR")
let tickerUSD = MtGoxStream.LiveTickerFactory (new MtGoxStream.PubnubWrapper(pubnub), MtGoxStream.PUBNUB_CHANNELS.ticker_BTCUSD, "BTCUSD")
providers.Add(tickerEUR)
providers.Add(tickerUSD)
let tickers = new LiveTickerCollection(providers)

let chartEUR = clock 1000 |> Event.map (fun _ -> tickerEUR.History |> Seq.map (fun t -> t.Now,t.Last) ) |> LiveChart.Line
let chartUSD = clock 1000 |> Event.map (fun _ -> tickerUSD.History |> Seq.map (fun t -> t.Now,t.Vwap)) |> LiveChart.Line
Chart.Combine([chartEUR;chartUSD]).WithYAxis(Min=600.0).ShowChart()
chartUSD.WithYAxis(Min=800.0).ShowChart()

#load "proxy.fs"
// MtGox Quote
let bid = MtGoxHttp.Quote (MtGoxHttp.QuoteType.Bid) (1.0) "BTC" "USD" |> Async.RunSynchronously
let ask = MtGoxHttp.Quote (MtGoxHttp.QuoteType.Ask) (1.0) "BTC" "USD" |> Async.RunSynchronously
let ticker = MtGoxHttp.TickerFast "BTC" "USD" |> Async.RunSynchronously
let spread = bid-ask
let spreadP = spread/bid
jsonString ticker?now
jsonInt ticker?last?value_int

MtGoxHttp.Quote (MtGoxHttp.QuoteType.Ask) (1.0) "BTC" "USD" |> Async.RunSynchronously

#load "BitCoinCharts.fs"
open andri.BtcClient
// History
let dataUSD =
    BitcoinCharts.HistorySample "mtgoxUSD" (DateTime.UtcNow.Subtract(new TimeSpan(6,0,0)))
    |> Async.RunSynchronously
    |> Seq.mapi (fun i r -> if i%15=0 then Some(r.Now,r.Price) else None) |> Seq.filter (Option.isSome) |> Seq.map (Option.get)
let dataEUR =
    BitcoinCharts.HistorySample "mtgoxEUR" (DateTime.UtcNow.Subtract(new TimeSpan(6,0,0)))
    |> Async.RunSynchronously
    |> Seq.mapi (fun i r -> if i%15=0 then Some(r.Now,r.Price*1.36) else None) |> Seq.filter (Option.isSome) |> Seq.map (Option.get)
let min = Seq.append dataUSD dataEUR |> Seq.map snd |> Seq.min
Chart.Combine([FSharp.Charting.Chart.FastLine(dataUSD, Name="BTC/USD");FSharp.Charting.Chart.FastLine(dataEUR, Name="BTC/EUR")]).WithLegend().WithYAxis(Min=min).ShowChart()

let dataUSD =
    BitcoinCharts.HistoryFull "mtgoxUSD" (DateTime.UtcNow.Subtract(new TimeSpan(1,0,0))) (DateTime.UtcNow)
    |> Seq.mapi (fun i r -> if i%15=0 then Some(r.Now,r.Price) else None) |> Seq.filter (Option.isSome) |> Seq.map (Option.get)
let dataEUR =
    BitcoinCharts.HistoryFull "mtgoxEUR" (DateTime.UtcNow.Subtract(new TimeSpan(1,0,0))) (DateTime.UtcNow)
    |> Seq.mapi (fun i r -> if i%15=0 then Some(r.Now,r.Price*1.36) else None) |> Seq.filter (Option.isSome) |> Seq.map (Option.get)
Chart.Combine([FSharp.Charting.Chart.FastLine(dataUSD, Name="USD");FSharp.Charting.Chart.FastLine(dataEUR, Name="EUR")]).WithLegend().WithYAxis(Min=min).ShowChart()

//let vol = MathFi.vol (mtgoxHistory |> Seq.map (fun e -> e.Price) |> Array.ofSeq )

// AllMarkets Tickers
open andri.BtcClient
let allmarkets = BitcoinCharts.AllMarkets.Ticker()
let highestbid_USD = BitcoinCharts.AllMarkets.Highest_bid "USD"
jsonString highestbid_USD?symbol, jsonDouble highestbid_USD?bid
let lowestAsk_USD = BitcoinCharts.AllMarkets.Lowest_ask "USD"
jsonString lowestAsk_USD?symbol, jsonDouble lowestAsk_USD?bid
BitcoinCharts.AllMarkets.TickerSimple "USD" |> List.ofSeq

let tickers_webresponse =
    BitcoinCharts.AllMarkets.async_Ticker ()
    |> Async.RunSynchronously
    |> Newtonsoft.Json.Linq.JArray.Parse
    |> Seq.map (fun e-> jsonString e?symbol, jsonDouble e?bid, jsonDouble e?ask)
    |> Seq.filter (fun e ->
                    let s,b,a = e
                    printfn "%f" b
                    b>0.0)
    |> List.ofSeq
