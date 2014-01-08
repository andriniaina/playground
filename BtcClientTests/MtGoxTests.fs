namespace BtcClientTests

open andri.Utilities
open andri.BtcClient
open Xunit
open FsUnit.Xunit
open Newtonsoft.FsJson
open System
open Newtonsoft.Json.Linq
open FsMocks.Syntax
open PubNubMessaging.Core
open System.Collections.Generic

module MtGoxTests =

    type PubnubFake() =
        inherit MtGox.PubnubWrapper(null)
        override x.DetailedHistory channel count userCallback errorCallback =
            let l2 = new List<obj>() in
                l2.Add(Newtonsoft.Json.Linq.JObject.Parse("""{channel:"x", ticker:{"high":{"value":"966.99630","value_int":"96699630","display":"$967.00","display_short":"$967.00","currency":"USD"},"low":{"value":"859.00000","value_int":"85900000","display":"$859.00","display_short":"$859.00","currency":"USD"},"avg":{"value":"920.89718","value_int":"92089718","display":"$920.90","display_short":"$920.90","currency":"USD"},"vwap":{"value":"915.68177","value_int":"91568177","display":"$915.68","display_short":"$915.68","currency":"USD"},"vol":{"value":"16335.63905547","value_int":"1633563905547","display":"16,335.64\u00a0BTC","display_short":"16,335.64\u00a0BTC","currency":"BTC"},"last_local":{"value":"928.99499","value_int":"92899499","display":"$928.99","display_short":"$928.99","currency":"USD"},"last_orig":{"value":"928.99499","value_int":"92899499","display":"$928.99","display_short":"$928.99","currency":"USD"},"last_all":{"value":"928.99499","value_int":"92899499","display":"$928.99","display_short":"$928.99","currency":"USD"},"last":{"value":"928.99499","value_int":"92899499","display":"$928.99","display_short":"$928.99","currency":"USD"},"buy":{"value":"923.30100","value_int":"92330100","display":"$923.30","display_short":"$923.30","currency":"USD"},"sell":{"value":"928.99499","value_int":"92899499","display":"$928.99","display_short":"$928.99","currency":"USD"},"item":"BTC","now":"1389219160000000"}}"""))
            let l1 = new List<obj>() in
                l1.Add(l2)
            userCallback(l1)
            true
        override x.Subscribe channel userCallback connectCallback errorCallback = ()


    let [<Fact>] xxx()=
        let pubnubFake = PubnubFake()
        let tickerEUR = MtGox.LiveTickerFactory (pubnubFake, MtGox.PUBNUB_CHANNELS.ticker_BTCEUR, "BTCEUR")
        tickerEUR.History_Last |> Seq.length |> should equal 1
        tickerEUR.History_Last |> Seq.head |> should equal (new DateTime(2014,1,8,22,12,40), 928.99499)

