﻿namespace andri.BtcClient
    module _MtGox =
        open System
        open andri.Log

        let BASE_URL = "https://data.mtgox.com/api/2/"
        let BASE_URI = new Uri(BASE_URL)

        let ticksToDateTime (unixTimestamp:int64) =
            let unixYear0 = new DateTime(1970, 1, 1);
            let unixTimeStampInTicks = unixTimestamp * 10L
            new DateTime(unixYear0.Ticks + unixTimeStampInTicks)

        let connectCallback_obj (o:obj) =
            debugf "connectCallback: %s" (o.ToString())
        let connectCallback_generic (o:System.Collections.Generic.IList<obj>) =
            let status = o |> Seq.nth 1 :?> string
            debugf "connectCallback: %s" (status)
        let disconnectCallback_generic o =
            debugf "disconnectCallback: %s" (o.ToString())
        let errorCallback_generic (o:PubNubMessaging.Core.PubnubClientError) =
            errorf "errorCallback: %s" (o.ToString())
            
        let toRealValue currency (v:int) =
            match currency with
            | "BTC" -> (float v)/1e8
            | "JPY" | "SEK" -> (float v)/1e3
            | _ -> (float v)/1e5
        let toIntValue currency (v:float) =
            match currency with
            | "BTC" -> Convert.ToInt32(Math.Round(v*1e8))
            | "JPY" | "SEK" -> Convert.ToInt32(Math.Round(v*1e3))
            | _ -> Convert.ToInt32(Math.Round(v*1e5))

        type channelsDictType = {
           ticker_LTCGBP:string
           ticker_LTCCNY:string
           depth_BTCHKD:string
           depth_BTCEUR:string
           ticker_NMCAUD:string
           ticker_BTCEUR:string
           depth_BTCKRW:string
           depth_BTCCNY:string
           ticker_BTCCAD:string
           depth_BTCCHF:string
           ticker_LTCNOK:string
           ticker_LTCUSD:string
           ticker_BTCBTC:string
           ticker_LTCCAD:string
           ticker_NMCCNY:string
           depth_BTCUSD:string
           ticker_BTCCHF:string
           depth_BTCAUD:string
           ticker_BTCCZK:string
           ticker_BTCSGD:string
           ticker_NMCJPY:string
           ticker_BTCNMC:string
           depth_BTCINR:string
           depth_BTCSGD:string
           ticker_BTCLTC:string
           ticker_LTCEUR:string
           ticker_BTCINR:string
           ticker_LTCJPY:string
           depth_BTCCAD:string
           ticker_BTCNZD:string
           depth_BTCGBP:string
           depth_BTCNOK:string
           depth_BTCTHB:string
           ticker_BTCSEK:string
           ticker_BTCNOK:string
           ticker_BTCGBP:string
           trade_lag:string
           depth_BTCSEK:string
           depth_BTCDKK:string
           depth_BTCJPY:string
           ticker_NMCUSD:string
           ticker_LTCAUD:string
           ticker_BTCJPY:string
           depth_BTCCZK:string
           ticker_LTCDKK:string
           ticker_BTCPLN:string
           ticker_BTCRUB:string
           ticker_NMCGBP:string
           ticker_BTCKRW:string
           ticker_BTCCNY:string
           depth_BTCNZD:string
           ticker_BTCHKD:string
           ticker_BTCTHB:string
           ticker_BTCUSD:string
           depth_BTCRUB:string
           ticker_NMCEUR:string
           trade_BTC:string
           ticker_NMCCAD:string
           depth_BTCPLN:string
           ticker_BTCDKK:string
           ticker_BTCAUD:string
        }
