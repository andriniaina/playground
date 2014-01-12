namespace andri.BtcClient

open System
open System.Net
open andri.Utilities
open andri.Log
open System.Collections.Generic
open MathNet.Numerics.LinearAlgebra.Double
open System

module NeuralNetworks =
    let RegressionCoefficients degree (xdata:float seq) (ydata:float seq) = 
        let X = DenseMatrix.ofRowVectors ( xdata |> Seq.map (fun e -> [for j in 0..degree -> Math.Pow(e,float j) ] ) |> Seq.map (DenseVector.ofList) |> List.ofSeq)
        let Y = DenseVector.ofSeq ydata
        
        X.QR().Solve(Y)

    let ValueAt x (coeffs:float MathNet.Numerics.LinearAlgebra.Generic.Vector) =
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
    let _Regress m (xdata:float seq) (ydata:float seq) (futurePoint:float) :float =
        let coeffs = RegressionCoefficients m xdata ydata
        let x2 = float futurePoint
        let y2 = ValueAt x2 coeffs

        y2

    let Regress m (data:(DateTime*float) seq) (futurePoint:DateTime) =
        let xdata = data |> Seq.map fst |> Seq.map (fun d -> float (d.Ticks))
        let ydata = data |> Seq.map snd

        _Regress m xdata ydata (float futurePoint.Ticks)

    /// retourne la pente attendue pour une prédiction de N min
    let PredictTrend N (data:(DateTime*float) seq) =
        let FACTOR = (float N)*600000000.0
        let _data = data |> Seq.sortBy fst
        let xStart = (fst(Seq.head _data)).Ticks
        let xdata = _data |> Seq.map (fun d -> float ((fst d).Ticks-xStart)/FACTOR)
        let ydata = _data |> Seq.map (fun d -> snd d)

        let x1,y1 = Seq.last xdata, Seq.last ydata
        let x2 = x1+(float (System.TimeSpan(0,N,0).Ticks)/FACTOR)
        let y2 = _Regress 5 xdata ydata (x2)
        (y2-y1)/(x2-x1)

    /// variation attendue par minute, sur une vision de 30min
    let Slope30perMin data = float(PredictTrend 30 data)/30.0
    /// variation attendue par minute, sur une vision de 60min
    let Slope60perMin data = float(PredictTrend 60 data)/60.0
