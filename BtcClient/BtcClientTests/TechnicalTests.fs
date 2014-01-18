namespace BtcClientTests

open andri.Utilities
open andri.BtcClient
open Xunit
open FsUnit.Xunit
open Newtonsoft.FsJson
open System
open andri.BtcClient
open MathNet.Numerics.LinearAlgebra.Double

module TechnicalTests = 
    let round6 (x:float) = Math.Round(x,6)
    let [<Fact>] ValueAt() =
        let coeffs = [5.0 ; 2.0; 3.0] |> DenseVector.ofList
        DataAnalysis.ValueAt coeffs 10.0 |> should equal (5.0 + 10.0*2.0 + 3.0*10.0*10.0)

    let [<Fact>] RegressionCoefficients() =
        let xdata = [ 1.0; 2.5; 3.0 ]
        let ydata = xdata |> List.map (fun x -> 5.0+2.0*x+3.0*x*x)
        let coeffs =
            DataAnalysis.RegressionCoefficients 2 xdata ydata
            |> Seq.map round6
            |> List.ofSeq
        coeffs |> should equal [5.0 ; 2.0; 3.0]
    
    let [<Fact>] Derive() =
        let xdata = [ 1.0; 2.5; 3.0 ]
        let ydata = xdata |> List.map (fun x -> 5.0+2.0*x+3.0*x*x)
        let coeffs = 
            DataAnalysis.RegressionCoefficients 2 xdata ydata
        let coeffs' =
            coeffs
            |> DataAnalysis.Derive
        coeffs' |> Seq.map round6 |> List.ofSeq |> should equal [2.0; 6.0]
        DataAnalysis.ValueAt coeffs' 1.33 |> round6 |> should equal (2.0+6.0*1.33)


    let [<Fact>] ``Regress``() =
        let xdata = [ 1.0; 2.5; 3.0 ]
        let ydata = [ 15.0; 25.0; 30.0 ]
        Math.Round(DataAnalysis._Regress  2 (xdata) (ydata)    1.0  , 6) |> should equal 15.0
        Math.Round(DataAnalysis._Regress  2 (xdata) (ydata)    2.0  , 6) |> should equal 20.833333
        Math.Round(DataAnalysis._Regress  2 (xdata) (ydata)    2.5  , 6) |> should equal 25.0
        Math.Round(DataAnalysis._Regress  2 (xdata) (ydata)    3.0  , 6) |> should equal 30.0
        Math.Round(DataAnalysis._Regress  2 (xdata) (ydata)    4.0  , 6) |> should equal 42.5
        
    let [<Fact>] ``PredictTrend fonction croissante lineaire, +1 per min``() =
        let start = DateTime.Now
        let data = [ for i in 10..20 -> start + TimeSpan(0,i,0),  float i]
        Math.Round(DataAnalysis.PredictTrend 30 2 data, 6) |> should equal 30.0
        Math.Round(DataAnalysis.PredictTrend 60 2 data, 6) |> should equal 60.0
        
    let [<Fact>] ``Slope fonction croissante lineaire, +1 per min``() =
        let start = DateTime.Now
        let data = [ for i in 10..20 -> start + TimeSpan(0,i,0), float i]
        DataAnalysis.Slope30perMin data |> round6 |> should equal 1.0
        DataAnalysis.Slope60perMin data |> round6 |> should equal 1.0

    let [<Fact>] ``Slope fonction decroissante lineaire, -1 per min``() =
        let start = DateTime.Now
        let data = [ for i in 0..60 -> start + TimeSpan(0,i,0), -(float i)]
        DataAnalysis.Slope30perMin data |> round6 |> should equal -1.0

    let [<Fact>] ``BitcoinCharts_parseFloat``()=
        BitcoinCharts.parseFloat "1.181851570000" |> should equal 1.181851570000
    
    let [<Fact>] ``BitcoinCharts_parseInt``()=
        BitcoinCharts.parseInt "18185157" |> should equal 18185157
        
    let [<Fact>] toUnixTicksTest() =
        BitcoinCharts.toUnixTicks (new DateTime(2013,12,1,5,6,7)) |> should equal 1385874367L

    let [<Fact>] parseUnixTicks() =
        BitcoinCharts.parseUnixTicks "1385874367" |> should equal (new DateTime(2013,12,1,5,6,7))


