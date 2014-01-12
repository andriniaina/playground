#I @"packages\FSharp.Charting.0.90.5\lib\net40"
#I @"D:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug"
#I @"c:\dev\pubnub-c-sharp\csharp.net\3.5\PubNub-Messaging\bin\Debug"
#I @"packages\MathNet.Numerics.2.6.2\lib\net40"
#I @"packages\MathNet.Numerics.FSharp.2.6.0\lib\net40"

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

#r "MathNet.Numerics.dll"
#r "MathNet.Numerics.IO"
#r "MathNet.Numerics.Fsharp"

#load "NeuralNetworks.fs"

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


let marketName ="mtgoxUSD"
let startTime= new TimeSpan(13,0,0) |> DateTime.UtcNow.Subtract
let data =
    BitcoinCharts.HistorySample marketName startTime
    |> Async.RunSynchronously
let coords = data |> Seq.map(fun d -> d.Now, d.Price)
let xdata = coords |> Seq.map fst |> Seq.map (fun d -> d.Ticks) |> Seq.map float
let ydata = coords |> Seq.map snd
let coeffs = NeuralNetworks.RegressionCoefficients 5 xdata ydata

let min = coords |> Seq.map snd |> Seq.min
let xL = xdata |> Seq.head |> int64
let xR = xdata |> Seq.last |> int64
let coords' = [
    for x in xL..5000000000L..xR ->
    let d = DateTime(x)
    d,NeuralNetworks.ValueAt (float x) coeffs
    ]

Chart.Combine([
                FSharp.Charting.Chart.FastLine(coords).WithYAxis(Min=min);
                FSharp.Charting.Chart.FastLine(coords')])
     .WithLegend().WithYAxis(Min=min)
     .ShowChart()

NeuralNetworks.PredictTrend 5 coords
NeuralNetworks.Slope30perMin coords
NeuralNetworks.Slope60perMin coords
let coeffs' = NeuralNetworks.Derive coeffs
coeffs |> NeuralNetworks.ValueAt (DateTime(2014,1,11,22,45,0).Ticks |> float)
coeffs' |> NeuralNetworks.ValueAt (DateTime(2014,1,11,22,00,0).Ticks |> float)



let Prout N (data:(DateTime*float) seq) =

    let FACTOR = (float N)*60000000.0
    let _data = data |> Seq.sortBy fst
    let xStart = (fst(Seq.head _data)).Ticks
    let xdata = _data |> Seq.map (fun d -> float ((fst d).Ticks-xStart)/FACTOR)
    let ydata = _data |> Seq.map (fun d -> snd d)

    let coeffs = NeuralNetworks.RegressionCoefficients 9 xdata ydata
    let xL,xR=Seq.head xdata,Seq.last xdata
    let coords = [
        for x in xL..xR ->
        x,NeuralNetworks.ValueAt (float x) coeffs
    ]
    
    let min = ydata |> Seq.min
    let coeffs' = coeffs |> NeuralNetworks.Derive 
    let coords' = [
        for x in xL..xR ->
        x,NeuralNetworks.ValueAt (float x) coeffs'
    ]
    
    //FSharp.Charting.Chart.FastLine(data |> Seq.map (fun d -> float ((fst d).Ticks-xStart)/FACTOR,snd d)).WithYAxis(Min=min).ShowChart()
    FSharp.Charting.Chart.FastLine(coords).WithYAxis(Min=min).ShowChart()
    //FSharp.Charting.Chart.FastLine(coords).WithYAxis(Min=min).ShowChart()

Prout 30 coords