namespace andri.BtcClient
    open System
    open andri.Log
    open System.Globalization
    open System.Collections.Generic
    open andri.Utilities
    open Newtonsoft.Json
    open Newtonsoft.FsJson
    
    module BitcoinCharts =
        let private BASEURL = "http://api.bitcoincharts.com/v1/"
        let private BASEURI = new Uri(BASEURL)
        let private formatInfo_US = new NumberFormatInfo(NumberDecimalSeparator=".")
        let private parseFloat s = Convert.ToDouble(s, formatInfo_US)
        let private parseInt s = Convert.ToInt32(s, formatInfo_US)
        let private unix0 = new DateTime(1970, 1,1)
        let private parseUnixTicks s = new DateTime(unix0.Ticks+10000000L*Convert.ToInt64(s, formatInfo_US))
        let private toUnixTicks (d:DateTime) = (d.Ticks-unix0.Ticks)/10000000L
        let private csvSplitByRow = Strings.SplitByChar '\n'
        let private csvSplitByColumn = Strings.SplitByChar ','
        type History = {Time:DateTime; Price:float; Amount:float}
        let private csvParseHistoryRow =
            function
                | [|t;p;a|] -> { Time=parseUnixTicks t; Price=parseFloat p; Amount=parseFloat a }
                | row -> failwithf "bad csv format: %i" (row.Length)


        /// mtgoxUSD
        let History (market) (start) =
            let uri = new Uri(BASEURI, sprintf "trades.csv?symbol=%s&start=%i" market (toUnixTicks start))
            async {
                let! response = Web.getResponse uri
                System.IO.File.WriteAllText(@"d:\temp\btchistory.csv", response)
                //let response = System.IO.File.ReadAllText(@"d:\temp\btchistory.csv")
                return response
                        |> csvSplitByRow
                        |> Seq.map (csvSplitByColumn >> csvParseHistoryRow)
                }
        module AllMarkets = 
            let async_Ticker () = 
                let uri = new Uri(BASEURI, "markets.json")
                async{
                    let! response = Web.getResponse uri
                    return response
                }
            let Ticker = async_Ticker >> Async.RunSynchronously

            let async_TickerSimple currency =
                async {
                let! tickers_webresponse = async_Ticker ()
                let tickers = Newtonsoft.Json.Linq.JArray.Parse  tickers_webresponse
                return
                    tickers
                    |> Seq.filter (fun e -> jsonString e?currency = currency)
                    |> Seq.map (fun e-> jsonString e?symbol, jsonDouble e?bid, jsonDouble e?ask)
                    |> Seq.filter (fun (s,b,a) ->
                                        //let s,b,a = e
                                        printfn "%f" b
                                        b>0.0)
                }
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

                

