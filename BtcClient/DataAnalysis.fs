namespace andri.BtcClient

open System
open System.Net
open andri.Utilities
open andri.Log
open System.Collections.Generic
open MathNet.Numerics.LinearAlgebra.Double
open System
open FSharp.Charting

module DataAnalysis =
    let RegressionCoefficients degree (xdata:float seq) (ydata:float seq) = 
        let X = DenseMatrix.ofRowVectors ( xdata |> Seq.map (fun e -> [for j in 0..degree -> Math.Pow(e,float j) ] ) |> Seq.map (DenseVector.ofList) |> List.ofSeq)
        let Y = DenseVector.ofSeq ydata
        
        X.QR().Solve(Y)

    let ValueAt (coeffs:float MathNet.Numerics.LinearAlgebra.Generic.Vector) x =
        coeffs
        |> Seq.mapi (fun i coef -> coef,i)
        |> Seq.fold (fun (s) (coef,p) -> s+coef*Math.Pow(x, float p)) 0.0
        
    let Derive (coeffs:float MathNet.Numerics.LinearAlgebra.Generic.Vector) =
        coeffs
            |> Seq.mapi(fun i e -> (float i)*e)
            |> Seq.skip 1
            |> DenseVector.ofSeq
            :> float MathNet.Numerics.LinearAlgebra.Generic.Vector

    /// régression polynomiale de degré m
    let _Regress m (xdata:float seq) (ydata:float seq) =
        let coeffs = RegressionCoefficients m xdata ydata
        ValueAt coeffs

    let Regress m (data:(DateTime*float) seq) =
        let xdata = data |> Seq.map fst |> Seq.map (fun d -> float (d.Ticks))
        let ydata = data |> Seq.map snd

        _Regress m xdata ydata

    /// retourne la pente attendue pour une prédiction de N min
    let PredictTrend M degree (data:(DateTime*float) seq) =
        let FACTOR = (float M)*600000000.0
        let _data = data// |> Seq.sortBy fst
        let xStart = (fst(Seq.head _data)).Ticks
        let xdata = _data |> Seq.map (fun d -> float ((fst d).Ticks-xStart)/FACTOR)
        let ydata = _data |> Seq.map snd

        let x1,y1 = Seq.last xdata, Seq.last ydata
        let x2 = x1+(float (System.TimeSpan(0,M,0).Ticks)/FACTOR)
        let y2 = _Regress degree xdata ydata (x2)
        (y2-y1)/(x2-x1)
        
    let private shrinkScale samplingRate (data:(DateTime*float) seq) =
        let FACTOR = (float samplingRate)*60000000.0
        let xStart = (data |> Seq.minBy fst |> fst).Ticks

        let data' = data |> Seq.map (fun (d,p) -> float(d.Ticks-xStart)/FACTOR , p )
        data',FACTOR,xStart

    type TradingAdvice = | Buy of float | Sell of float
        with
        override x.ToString() =
            match x with | Buy(a) -> sprintf "Buy(%f)" a | Sell(a) -> sprintf "Sell(%f)" a

    let PredictTrend2 samplingRate degree (data:(DateTime*float) seq) =
        let _data,factor,xStart = shrinkScale samplingRate data
        let xdata = _data |> Seq.map fst
        let ydata = _data |> Seq.map snd
        let coeffs = RegressionCoefficients degree xdata ydata
        let coeffs' = coeffs |> Derive
        let coeffs'' = coeffs' |> Derive

        let i = (Seq.length xdata)*98/100
        let xi = Seq.nth i xdata
        let derivee1 = ValueAt coeffs' xi
        let derivee2 = ValueAt coeffs' xi
        if (derivee2 > 0.0) then Buy(derivee1) else Sell(derivee2)


    /// variation attendue par minute, sur une vision de 30min
    let Slope30perMin data = float(PredictTrend 30 5 data)/30.0
    /// variation attendue par minute, sur une vision de 60min
    let Slope60perMin data = float(PredictTrend 60 5 data)/60.0
    
    let BuildChart (coords:(DateTime*float) seq) =
        let count = Seq.length coords
        //let min = coords |> Seq.map snd |> Seq.min
        FSharp.Charting.Chart.FastLine(coords)
            //.WithYAxis(Min=min)
            .ShowChart()
    let ShowChart samplingRate degree (data:(DateTime*float) seq) =

        let FACTOR = samplingRate*60000000.0
        let _data = data //|> Seq.sortBy fst
        let xStart = (fst(Seq.head _data)).Ticks
        let xdata = _data |> Seq.map (fun d -> float ((fst d).Ticks-xStart)/FACTOR)
        let ydata = _data |> Seq.map (fun d -> snd d)

        let coeffs = RegressionCoefficients degree xdata ydata
        let xL,xR=Seq.head xdata,Seq.last xdata
        let coords = [
            for x in xL..xR ->
            x,ValueAt coeffs (float x)
        ]
    
        let min = ydata |> Seq.min
        (*
        let coeffs' = coeffs |> Derive 
        let coords' = [
            for x in xL..xR ->
            x,ValueAt coeffs' (float x)
        ]*)
    
        Chart.Combine([
                            FSharp.Charting.Chart.Line(data).WithYAxis(Min=min);
                            FSharp.Charting.Chart.Line(coords |> Seq.map (fun (d,p) -> DateTime(int64(d*FACTOR)+ xStart) ,p)).WithYAxis(Min=min)
            ])
            //.WithLegend()
            .WithYAxis(Min=min)
            .ShowChart()

    