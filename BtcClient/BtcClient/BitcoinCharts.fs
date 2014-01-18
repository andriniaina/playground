namespace andri.BtcClient
    open System
    open andri.Log
    open System.Globalization
    open System.Collections.Generic
    open andri.Utilities
    open Newtonsoft.Json
    open Newtonsoft.FsJson
    open andri.BtcClient.Data
    
    module BitcoinCharts =
        let private BASEURL = "http://api.bitcoincharts.com/v1/"
        let private BASEURI = new Uri(BASEURL)
        let private formatInfo_US = new NumberFormatInfo(NumberDecimalSeparator=".")
        let parseFloat s = Convert.ToDouble(s, formatInfo_US)
        let parseInt s = Convert.ToInt32(s, formatInfo_US)
        let private unix0 = new DateTime(1970, 1,1)
        let parseUnixTicks s = new DateTime(unix0.Ticks+10000000L*Convert.ToInt64(s, formatInfo_US))
        let toUnixTicks (d:DateTime) = (d.Ticks-unix0.Ticks)/10000000L
        let private csvSplitByRow = Strings.SplitByChar '\n'
        let private csvSplitByColumn = Strings.SplitByChar ','
        let private csvParseHistoryRow market =
            function
                | [|t;p;a|] -> new BitcoinChartHistory(Market=market, Now=parseUnixTicks t, Price=parseFloat p, Amount=parseFloat a)
                | row -> failwithf "bad csv format: %i" (row.Length)

        let AsTimeSeries (data:BitcoinChartHistory seq) =
            data |> Seq.map (fun o -> o.Now,o.Price)

        /// mtgoxUSD
        let HistorySample (market) (start) =
            let uri = new Uri(BASEURI, sprintf "trades.csv?symbol=%s&start=%i" market (toUnixTicks start))
            async {
                printfn "downloading %s %s" market (start.ToString())
                let! response = Web.getResponse uri
                return response
                        |> csvSplitByRow
                        |> Seq.map (csvSplitByColumn >> csvParseHistoryRow market)
                        |> Seq.sortBy (fun o -> o.Now)
                }
        let HistorySampleTask (market) (start) =
            HistorySample (market) (start) |> Async.StartAsTask

        let rec HistoryFull (market) (d1) (d2) : seq<BitcoinChartHistory> =
                let data = HistorySample market d1 |> Async.RunSynchronously
                let d1' = (Seq.last data).Now
                if d1'=d1 then
                    data
                else
                    data |> Seq.append (HistoryFull market d1' d2)

        module AllMarkets = 
            let async_Ticker () = 
                let uri = new Uri(BASEURI, "markets.json")
                async{
                    let! response = Web.getResponse uri
                    return response
                }
            let Ticker = async_Ticker >> Async.RunSynchronously >> Newtonsoft.Json.Linq.JArray.Parse

            let async_TickerSimple currency =
                async {
                let! tickers_webresponse = async_Ticker ()
                let tickers = Newtonsoft.Json.Linq.JArray.Parse  tickers_webresponse
                return
                    tickers
                    |> Seq.filter (fun e -> jsonString e?currency = currency)
                    |> Seq.map (fun e-> jsonString e?symbol, jsonDouble e?bid, jsonDouble e?ask, jsonDouble e?close)
                    |> Seq.filter (fun (s,b,a,c) -> b>0.0)
                }
            /// symbol,bid,ask,close
            let TickerSimple = async_TickerSimple >> Async.RunSynchronously

            /// ne pas faire confiance au marché
            let async_Highest_bid currency =
                async{
                    let! webresponse_tickers = async_Ticker()
                    return Newtonsoft.Json.Linq.JArray.Parse webresponse_tickers
                        |> Seq.filter (fun e -> jsonString e?currency=currency && jsonDouble e?ask>0.0 )
                        |> Seq.maxBy (fun e -> jsonDouble e?bid)
                }
            /// ne pas faire confiance au marché
            let Highest_bid = async_Highest_bid >> Async.RunSynchronously
            /// ne pas faire confiance au marché
            let async_Lowest_ask currency =
                async{
                    let! webresponse_tickers = async_Ticker()
                    return Newtonsoft.Json.Linq.JArray.Parse webresponse_tickers
                        |> Seq.filter (fun e -> jsonString e?currency=currency && jsonDouble e?ask>0.0 )
                        |> Seq.minBy (fun e -> jsonDouble e?ask)
                }
            /// ne pas faire confiance au marché
            let Lowest_ask =async_Lowest_ask >> Async.RunSynchronously

                

