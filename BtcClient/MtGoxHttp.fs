namespace andri.BtcClient
    open System
    open System.Net
    open _MtGox
    open andri.Utilities
    open andri.Log
    open System.Collections.Generic
    open Newtonsoft.FsJson
    open PubNubMessaging.Core

    module MtGoxHttp =
        type QuoteType =
            | Bid | Ask
            override x.ToString() = match x with | Bid -> "bid" | Ask -> "ask"

        // il n'y a plus rien à améliorer ici
        // https://data.mtgox.com/api/2/
        let Quote qType (famount:float) currency1 currency2 =
            let response =
                new Uri(BASE_URI, sprintf "%s%s/money/order/quote?type=%s&amount=%i" currency1 currency2 (qType.ToString()) (toIntValue currency1 famount))
                |> Web.getResponse
                |> Async.RunSynchronously
                |> Newtonsoft.Json.Linq.JObject.Parse
            let status = jsonString response?result
            match status with
            | "success" -> jsonInt response?data?amount |> toRealValue currency2
            | _ -> failwith "Erreur de communication"

        /// exemples: jsonString ticker?now / jsonInt ticker?last?value_int
        let TickerFast currency1 currency2 =
            let response =
                new Uri(BASE_URI, sprintf "%s%s/money/ticker_fast" currency1 currency2)
                |> Web.getResponse
                |> Async.RunSynchronously
                |> Newtonsoft.Json.Linq.JObject.Parse
            let status = jsonString response?result
            match status with
            | "success" -> response?data
            //| "success" -> jsonInt response?data?last?value_int |> toRealValue currency2
            | _ -> failwith "Erreur de communication"


