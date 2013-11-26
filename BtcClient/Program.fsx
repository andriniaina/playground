#I @"packages\FSharp.Charting.0.90.5\lib\net40"
#I @"D:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug"
#I @"c:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug"

#r @"PubNubMessaging.Core"
#r @"Newtonsoft.Json"
#r @"Fsharp.Charting"
#r @"System.Windows.Forms"
#r @"System.Windows.Forms.DataVisualization"
#load "Web.fs"
#load "Queue.fs"
#load "Log.fs"
#load "Newtonsoft.Json.fs"
#load "abstract LiveParamProvider.fs"
#load "_MtGox.fs"
open andri.BtcClient
#load "MtGox.fs"
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


#load "proxy.fs"
let bid = MtGox.Quote (MtGox.QuoteType.Bid) (1.0) "BTC" "USD"
let ask = MtGox.Quote (MtGox.QuoteType.Ask) (1.0) "BTC" "USD"
let spread = bid-ask
let spreadP = spread/bid


MtGox.Quote (MtGox.QuoteType.Ask) (500.0) "EUR" "BTC"
