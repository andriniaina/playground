namespace BtcClientTests

open andri.Utilities
open andri.BtcClient
open Xunit
open FsUnit.Xunit
open Newtonsoft.FsJson
open System
open Newtonsoft.Json.Linq
open System.Collections.Generic

module CyclicObservableQueueTests =
    let [<Fact>] ``Subscribe to queue``() =
        let nbCalls = ref 0
        let lastValue = ref (new List<int>() :> int IList)
        let callback o =
            nbCalls := !nbCalls + 1
            lastValue := o
        let queue = new CyclicObservableQueue<int>(4)
        use subscriber = Observable.subscribe callback queue
        queue.Enqueue(1)
        queue.Enqueue(2)
        queue.Enqueue(3)
        queue.Enqueue(4)
        queue.Enqueue(5)

        !nbCalls |> should equal 5
        (!lastValue).Count |> should equal 4
        (!lastValue).[0] |> should equal 2
        (!lastValue).[1] |> should equal 3
        (!lastValue).[2] |> should equal 4
        (!lastValue).[3] |> should equal 5
