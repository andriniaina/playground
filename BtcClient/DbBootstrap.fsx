#I @"packages\FSharp.Charting.0.90.5\lib\net40"
#I @"D:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug"
#I @"c:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug"

#r @"PubNubMessaging.Core"
#r @"Newtonsoft.Json"
#r @"Fsharp.Charting"
#r @"System.Windows.Forms"
#r @"System.Windows.Forms.DataVisualization"
#load "../andri.FsUtilities/Finance.fs"
#load "../andri.FsUtilities/ServiceLocator.fs"
open andri.Utilities
#load "../andri.FsUtilities/Web.fs"
#load "../andri.FsUtilities/Queue.fs"
#load "../andri.FsUtilities/Strings.fs"
#load "Log.fs"
#load "Newtonsoft.FsJson.fs"
#load "abstract LiveParamProvider.fs"
#load "_MtGox.fs"
open andri.BtcClient
#load "MtGoxStream.fs"
#load "MtGoxHttp.fs"
open andri.BtcClient


#I @"..\BtcClient.Data\bin\x86\Debug"
#r "BtcClient.Data"
#r "System.Data"
#r "System.Transactions"
#r "System.Data.Sqlite"
#r "System.Data.Linq"
#r "DbLinq"

#load "BitcoinCharts.fs"

open System
open PubNubMessaging.Core
open andri.Utilities
open andri.BtcClient
open Newtonsoft.FsJson
open FSharp.Charting
open System.Collections.Generic
open DbLinq
open System.Linq
open andri.BtcClient
open andri.BtcClient.Data

let byMinute (d:DateTime) = DateTime((d.Ticks/60000000L)*60000000L)
let byHour (d:DateTime) = DateTime((d.Ticks/36000000000L)*36000000000L)
let by30min (d:DateTime) = DateTime((d.Ticks/18000000000L)*18000000000L)
let DATABASEPATH = @"D:\dev\playground\BtcClient.Data\BtcClient.db3"
let connection() =
    new System.Data.SQLite.SQLiteConnection(@"Data Source=D:\dev\playground\BtcClient.Data\BtcClient.db3")

let updateMarketHistory (marketName:string) =
    use conn = connection()
    use ctx = new SQLite.SQLiteConnection(DATABASEPATH,false)
    let startTime =
        ctx.Query<BitcoinChartHistory>("select * from BitcoinChartHistory where Market=? and Now in (select max(Now) from BitcoinChartHistory where Market=?);", marketName, marketName)
            .First().Now
        
    let data =
        BitcoinCharts.HistorySample marketName (by30min(startTime- new TimeSpan(2,0,0)))
        |> Async.RunSynchronously
        
    let endTime = (Seq.last data).Now |> by30min
    // delete from BitcoinChartHistory
    use command = new System.Data.SQLite.SQLiteCommand(@"delete from BitcoinChartHistory where Market=@market and Now>=@start and Now<=@end", conn)
    command.Parameters.AddWithValue("@market", marketName) |> ignore
    command.Parameters.AddWithValue("@start", startTime) |> ignore
    command.Parameters.AddWithValue("@end", endTime) |> ignore
    conn.Open()
    (command.ExecuteNonQuery(),startTime.ToString()) ||> printfn "%i lines deleted starting from %s"
    command.Dispose()
    conn.Close()

    data
    |> Seq.filter (fun o -> o.Now>startTime)
    |> Seq.groupBy (fun o -> by30min(o.Now))
    |> Seq.collect (fun (t,g) ->
        let sumAmount = g |> Seq.sumBy (fun o -> o.Amount)
        let avgWPrice = (g |> Seq.sumBy (fun o -> o.Amount*o.Price)) / sumAmount
        let f = Seq.head g
        seq { yield BitcoinChartHistory(Now=t, Market=f.Market, Price=avgWPrice , Amount=sumAmount)}
    )
    |> ctx.InsertAll

    
updateMarketHistory "mtgoxUSD"
updateMarketHistory "mtgoxEUR"


let showHistory marketName (startTime:DateTime) endTime =
    use ctx = new SQLite.SQLiteConnection(DATABASEPATH,false)
    let data =
        query {
            for d in ctx.Table<BitcoinChartHistory>() do
            where (d.Market=marketName && d.Now>startTime)
            select d
        }
    let coords = data |> Seq.map (fun d -> d.Now, d.Price)
    let min = coords |> Seq.map snd |> Seq.min
    FSharp.Charting.Chart.Line(coords, Name=marketName).WithYAxis(Min=min).WithLegend().ShowChart()

showHistory "mtgoxUSD" (DateTime.UtcNow.Subtract(TimeSpan(24,0,0))) (DateTime.UtcNow)
showHistory "mtgoxEUR" (DateTime.UtcNow.Subtract(TimeSpan(24,0,0))) (DateTime.UtcNow)




