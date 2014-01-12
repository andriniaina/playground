namespace BtcClientTests

open andri.Utilities
open andri.BtcClient
open andri.BtcClient.Data
open Xunit
open FsUnit.Xunit
open Newtonsoft.FsJson
open System
open Newtonsoft.Json.Linq

module BitcoinChartsTests =

    let [<Fact>] ``HistorySample mtgoxEUR``()=
        ServiceLocator.forceSet "webrequest:/v1/trades.csv?symbol=mtgoxEUR&start=1385856000" (async {return """1389108337,705.778560000000,0.163000000000
1389108337,700.100000000000,0.996081300000
1389108364,709.529000000000,0.199099990000
1389108695,700.040520000000,0.086214520000"""})
        let dataEUR =
            BitcoinCharts.HistorySample "mtgoxEUR" (BitcoinCharts.parseUnixTicks "1385856000")
            |> Async.RunSynchronously
            |> List.ofSeq
        let firstRow = dataEUR |> List.head
        let lastRow = List.nth dataEUR (dataEUR.Length-1)

        dataEUR.Length |> should equal 4
        firstRow |> should equal (BitcoinChartHistory(Market="mtgoxEUR", Now=DateTime(2014,1,7,15,25,37), Price=705.778560000000, Amount=0.163000000000))
        lastRow |> should equal (BitcoinChartHistory(Market="mtgoxEUR", Now=DateTime(2014,1,7,15,31,35), Price=700.040520000000, Amount=0.086214520000))
        
    let [<Fact>] ``BitcoinCharts_AllMarkets_Ticker``() =
        ServiceLocator.forceSet "webrequest:/v1/markets.json" (async {return """[
        {"high": null, "latest_trade": 1317843215, "bid": null, "volume": 0, "currency": "USD", "currency_volume": 0, "ask": null, "close": 4.750000000000, "avg": null, "symbol": "b7USD", "low": null},
        {"volume": 21186.067514900000, "latest_trade": 1389131820, "bid": 903.400000000000, "high": 1044.200000000000, "currency": "USD", "currency_volume": 20785128.341347491749, "ask": 905.000000000000, "close": 907.000000000000, "avg": 981.0753376826288888926097094, "symbol": "mtgoxUSD", "low": 900.000000000000}
        ]"""})
        let data = BitcoinCharts.AllMarkets.Ticker()
        data.Count |> should equal 2
        let row1 = data.[1]
        jsonString row1?currency |> should equal "USD"
        jsonDouble row1?bid |> should equal 903.4
        jsonDouble row1?avg |> should equal 981.0753376826288888926097094
        jsonDouble row1?close |> should equal 907.0 // correspond au latest de la période

    let [<Fact>] ``BitcoinCharts_AllMarkets_TickerSimple``() =
        ServiceLocator.forceSet "webrequest:/v1/markets.json" (async {return """[
        {"high": null, "latest_trade": 1384199283, "bid": null, "volume": 0, "currency": "CZK", "currency_volume": 0, "ask": null, "close": 6700.000000000000, "avg": null, "symbol": "bitcashCZK", "low": null},
        {"volume": 21186.067514900000, "latest_trade": 1389131820, "bid": 903.400000000000, "high": 1044.200000000000, "currency": "USD", "currency_volume": 20785128.341347491749, "ask": 905.000000000000, "close": 907.000000000000, "avg": 981.0753376826288888926097094, "symbol": "mtgoxUSD", "low": 900.000000000000},
        {"volume": 32824.441155150000, "latest_trade": 1389131798, "bid": 794.000000000000, "high": 950.000000000000, "currency": "USD", "currency_volume": 28589177.824738966940, "ask": 794.962000000000, "close": 794.998000000000, "avg": 870.9722639178415922801816170, "symbol": "btceUSD", "low": 779.800000000000}
        ]"""})
        let data = BitcoinCharts.AllMarkets.TickerSimple "USD" |> List.ofSeq
        data.Length |> should equal 2
        let symbol,bid,ask,close = data.[1]
        data.[1] |> should equal ("btceUSD", 794.0, 794.962, 794.998)
        
    let [<Fact>] ``BitcoinCharts_AllMarkets_Highest_bid USD``() =
        ServiceLocator.forceSet "webrequest:/v1/markets.json" (async {return """[
        {"high": null, "latest_trade": 1384199283, "bid": null, "volume": 0, "currency": "USD", "currency_volume": 0, "ask": null, "close": 6700.000000000000, "avg": null, "symbol": "bitcashUSD", "low": null},
        {"volume": 21186.067514900000, "latest_trade": 1389131820, "bid": 903.400000000000, "high": 1044.200000000000, "currency": "USD", "currency_volume": 20785128.341347491749, "ask": 905.000000000000, "close": 907.000000000000, "avg": 981.0753376826288888926097094, "symbol": "mtgoxUSD", "low": 900.000000000000},
        {"volume": 32824.441155150000, "latest_trade": 1389131798, "bid": 794.000000000000, "high": 950.000000000000, "currency": "USD", "currency_volume": 28589177.824738966940, "ask": 794.962000000000, "close": 794.998000000000, "avg": 870.9722639178415922801816170, "symbol": "btceUSD", "low": 779.800000000000}
        ]"""})

        let highestbid = BitcoinCharts.AllMarkets.Highest_bid "USD"
        jsonString highestbid?symbol |> should equal "mtgoxUSD"
        jsonDouble highestbid?bid |> should equal 903.4
        
    let [<Fact>] ``BitcoinCharts_AllMarkets_Lowest_ask USD``() =
        ServiceLocator.forceSet "webrequest:/v1/markets.json" (async {return """[
        {"high": null, "latest_trade": 1384199283, "bid": null, "volume": 0, "currency": "USD", "currency_volume": 0, "ask": null, "close": 6700.000000000000, "avg": null, "symbol": "bitcashUSD", "low": null},
        {"volume": 21186.067514900000, "latest_trade": 1389131820, "bid": 903.400000000000, "high": 1044.200000000000, "currency": "USD", "currency_volume": 20785128.341347491749, "ask": 905.000000000000, "close": 907.000000000000, "avg": 981.0753376826288888926097094, "symbol": "mtgoxUSD", "low": 900.000000000000},
        {"volume": 32824.441155150000, "latest_trade": 1389131798, "bid": 794.000000000000, "high": 950.000000000000, "currency": "USD", "currency_volume": 28589177.824738966940, "ask": 794.962000000000, "close": 794.998000000000, "avg": 870.9722639178415922801816170, "symbol": "btceUSD", "low": 779.800000000000}
        ]"""})
        let lowestAsk = BitcoinCharts.AllMarkets.Lowest_ask "USD"
        jsonString lowestAsk?symbol |> should equal "btceUSD"
        jsonDouble lowestAsk?ask |> should equal 794.962
        

//let lowestAsk_USD = BitcoinCharts.AllMarkets.Lowest_ask "USD"