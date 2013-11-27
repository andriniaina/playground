#r @"D:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug\PubNubMessaging.Core.exe"
#r @"D:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug\Newtonsoft.Json.dll"

#I @"packages\FSharp.Charting.0.90.5\lib\net40"
#r @"Fsharp.Charting"
#r @"System.Windows.Forms"
#r @"System.Windows.Forms.DataVisualization"
#load "../andri.FsUtilities/Finance.fs"
#load "../andri.FsUtilities/Web.fs"
#load "../andri.FsUtilities/Queue.fs"
#load "../andri.FsUtilities/Strings.fs"
#load "Log.fs"
#load "Newtonsoft.Json.fs"
#load "abstract LiveParamProvider.fs"
#load "_MtGox.fs"
open andri.BtcClient
#load "MtGox.fs"
#load "MtGoxHttp.fs"
#load "BitCoinCharts.fs"
open andri.BtcClient


open System
open PubNubMessaging.Core
open andri.Utilities
open andri.BtcClient
open Newtonsoft.FsJson
open FSharp.Charting
open System.Collections.Generic

/// An event which triggers at regular intervals reporting the real world time at each trigger
let clock interval = 
    let out = new Event<_>()
    let timer = new System.Windows.Forms.Timer(Interval=interval, Enabled=true)
    timer.Tick.Add (fun args -> out.Trigger System.DateTime.Now)
    timer.Start()
    out.Publish
    
let providers = new List<LiveTickerProvider>()
let tickerEUR = MtGox.LiveTickerFactory (MtGox.PUBNUB_CHANNELS.ticker_BTCEUR, "BTCEUR")
let tickerUSD = MtGox.LiveTickerFactory (MtGox.PUBNUB_CHANNELS.ticker_BTCUSD, "BTCUSD")
providers.Add(tickerEUR)
providers.Add(tickerUSD)
let tickers = new LiveTickerCollection(providers)

let chartEUR = clock 1000 |> Event.map (fun _ -> tickerEUR.History_Last) |> LiveChart.Line
let chartUSD = clock 1000 |> Event.map (fun _ -> tickerUSD.History_Last) |> LiveChart.Line
Chart.Combine([chartEUR;chartUSD]).ShowChart()


MtGoxHttp.Quote (MtGoxHttp.QuoteType.Bid) (1.0) "BTC" "USD"
MtGoxHttp.Quote (MtGoxHttp.QuoteType.Ask) (1.0) "BTC" "USD"
let ticker = MtGoxHttp.TickerFast "BTC" "USD"
jsonString ticker?now
jsonInt ticker?last?value_int

MtGoxHttp.Quote (MtGoxHttp.QuoteType.Ask) (1.0) "BTC" "EUR"
let mtgoxHistory = BitcoinCharts.History "mtgoxUSD" (DateTime.UtcNow.Subtract(new TimeSpan(1,0,0))) |> Async.RunSynchronously
let chart = mtgoxHistory |> Seq.mapi (fun i r -> if i%15=0 then Some(r.Time,r.Price) else None) |> Seq.filter (Option.isSome) |> Seq.map (Option.get) |> FSharp.Charting.Chart.Line
chart.ShowChart()

let vol = Finance.vol (mtgoxHistory |> Seq.map (fun e -> e.Price) |> Array.ofSeq )


