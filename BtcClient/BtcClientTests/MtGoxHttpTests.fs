namespace BtcClientTests

open andri.Utilities
open andri.BtcClient
open Xunit
open FsUnit.Xunit
open Newtonsoft.FsJson
open System
open Newtonsoft.Json.Linq

module MtGoxHttpTests =
    let [<Fact>] ``Quote bid BTC USD``()=
        ServiceLocator.forceSet "webrequest:/api/2/BTCUSD/money/order/quote?type=bid&amount=100000000" (async{ return """{"result":"success","data":{"amount":9670000}}"""})
        MtGoxHttp.Quote (MtGoxHttp.QuoteType.Bid) (1.0) "BTC" "USD" |> Async.RunSynchronously
            |> should equal 96.7

    let [<Fact>] ``Quote ask BTC USD``()=
        ServiceLocator.forceSet "webrequest:/api/2/BTCUSD/money/order/quote?type=ask&amount=100000000" (async{ return """{"result":"success","data":{"amount":9670000}}"""})
        MtGoxHttp.Quote (MtGoxHttp.QuoteType.Ask) (1.0) "BTC" "USD" |> Async.RunSynchronously
            |> should equal 96.7

    let [<Fact>] ``TickerFast BTC USD``()=
        ServiceLocator.forceSet "webrequest:/api/2/BTCUSD/money/ticker_fast" (async { return """
        {"result":"success","data":{
            "last_local":{"value":"938.05000","value_int":"93805000","display":"$938.05","display_short":"$938.05","currency":"USD"},
            "last":{"value":"938.05000","value_int":"93805000","display":"$938.05","display_short":"$938.05","currency":"USD"},
            "last_orig":{"value":"687.30000","value_int":"68730000","display":"687.30\u00a0\u20ac","display_short":"687.30\u00a0\u20ac","currency":"EUR"},
            "last_all":{"value":"934.86546","value_int":"93486546","display":"$934.87","display_short":"$934.87","currency":"USD"},
            "buy":{"value":"939.03000","value_int":"93903000","display":"$939.03","display_short":"$939.03","currency":"USD"},
            "sell":{"value":"939.20000","value_int":"93920000","display":"$939.20","display_short":"$939.20","currency":"USD"},
            "now":"1389129414191664"}}
        """})
        let ticker = MtGoxHttp.TickerFast "BTC" "USD" |> Async.RunSynchronously
        jsonString ticker?now |> should equal "1389129414191664"
        jsonInt ticker?last?value_int |> should equal 93805000
        jsonDouble ticker?last?value |> should equal 938.05000

