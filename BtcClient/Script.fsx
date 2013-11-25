
#I @"D:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug"
#r "Newtonsoft.Json";;
#r "PubNubMessaging.Core";;

#load "BtcClient.fs"
#load "Newtonsoft.Json.fs"


open System
open PubNubMessaging.Core
open andri.Utilities
open Newtonsoft.FsJson


let userCallback (o:obj System.Collections.Generic.List) =
    let response = o |> Seq.head :?> Newtonsoft.Json.Linq.JObject
    let ticker = response?ticker
    let high = jsonDouble ticker?high?value
    let low = jsonDouble ticker?low?value
    let avg = jsonDouble ticker?avg?value
    let vwap = jsonDouble ticker?vwap?value // weighted average 
    let vol = jsonDouble ticker?vol?value   // volume
    let last_local = jsonDouble ticker?last_local?value // include only the last trade in the selected currency
    let last_orig = jsonDouble ticker?last_orig?value   // include data of the original last trade 
    let last_all = jsonDouble ticker?last_all?value // last trade in ANY currency, converted to your currency
    let last = jsonDouble ticker?last?value // is always the same as last_all
    let buy = jsonDouble ticker?buy?value
    let sell = jsonDouble ticker?sell?value

    printfn "%s: %f" "userCallback" last

    
let connectCallback__ (o) =
    printfn "%s" "connectCallback"
let connectCallback (o:obj System.Collections.Generic.List) =
    let response = o |> Seq.nth 1 :?> string
    printfn "%s: %s" "connectCallback" (response)
let disconnectCallback o =
    printfn "%s" "errorCallback"
let errorCallback o =
    printfn "%s" "errorCallback"

let pubnub = new Pubnub("", MtGox.PUBNUB_KEY)
pubnub.Subscribe(MtGox.PUBNUB_CHANNELS.ticker_BTCEUR, userCallback, connectCallback, errorCallback)

System.Console.ReadLine() |> ignore
pubnub.Unsubscribe(MtGox.PUBNUB_CHANNELS.ticker_BTCEUR, userCallback, connectCallback, disconnectCallback, errorCallback)
pubnub.EndPendingRequests()
