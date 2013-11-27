namespace andri.BtcClient
    open System
    open andri.Log
    open System.Globalization
    open System.Collections.Generic
    open andri.Utilities
    
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

                

