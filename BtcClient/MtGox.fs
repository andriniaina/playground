namespace andri.BtcClient
    open PubNubMessaging.Core
    open Newtonsoft.FsJson
    open System
    open System.Collections.Generic
    open _MtGox

    module MtGox =
        let PUBNUB_KEY = "sub-c-50d56e1e-2fd9-11e3-a041-02ee2ddab7fe"

        let private pubnub = new Pubnub("", PUBNUB_KEY)

        type MtGoxLiveTickerProvider(name:string, historyMaxCount:int) =
            inherit andri.BtcClient.LiveTickerProvider(name)
            let tickerUpdated = new Event<Ticker>()
            let queue = new andri.Utilities.EnumerableQueue<DateTime*double>(historyMaxCount)
            let dvSort (d:DateTime,v:double) = d.Ticks
            member x.History_Last:seq<DateTime*double> = queue |> Seq.sortBy dvSort// :> seq<DateTime*double>
            member x.PushResponse (response:Newtonsoft.Json.Linq.JObject) =
                let channel = jsonString response?channel
                let ticker = response?ticker
                let now = jsonLong ticker?now |> _MtGox.ticksToDateTime
                let high = jsonDouble ticker?high?value
                let low = jsonDouble ticker?low?value
                let avg = jsonDouble ticker?avg?value
                let vwap = jsonDouble ticker?vwap?value // weighted average 
                let vol = jsonDouble ticker?vol?value   // volume
                let last_local = jsonDouble ticker?last_local?value // include only the last trade in the selected currency
                let last_orig = jsonDouble ticker?last_orig?value   // include data of the original last trade 
                let last_all = jsonDouble ticker?last_all?value // last trade in ANY currency, converted to your currency
                let last = jsonDouble ticker?last?value // is always the same as last_all
                let buy = jsonDouble ticker?buy?value
                let sell = jsonDouble ticker?sell?value

                if queue.Count>historyMaxCount then queue.Dequeue() |> ignore
                queue.Enqueue (now, last)

                printfn "(%s) currency_spread = last-last_all = %f-%f = %f" name last last_all (last-last_all)

                tickerUpdated.Trigger({Name=name; Last= x.History_Last})
            member x.pubnubUserCallback(o:obj System.Collections.Generic.List) =
                let response = o |> Seq.head :?> Newtonsoft.Json.Linq.JObject
                x.PushResponse(response)

            override x.TickerUpdated with get() = tickerUpdated.Publish
            
        let LiveTickerFactory(channel, friendlyName):LiveTickerProvider =
            let MAXCOUNT = 200
            let ticker = new MtGoxLiveTickerProvider(friendlyName, MAXCOUNT)
            let historyCallback (provider:MtGoxLiveTickerProvider) (o:IList<obj>) =
                let responses = o.[0] :?> IList<obj>
                responses |> Seq.map (fun e -> e :?> Newtonsoft.Json.Linq.JObject) |> Seq.iter provider.PushResponse
                
            pubnub.DetailedHistory(channel, MAXCOUNT, historyCallback ticker, errorCallback_generic) |> ignore
            pubnub.Subscribe(channel, ticker.pubnubUserCallback, connectCallback_generic, errorCallback_generic)
            ticker :> LiveTickerProvider
            
        let PUBNUB_CHANNELS =  {
           ticker_LTCGBP= "0102a446-e4d4-4082-8e83-cc02822f9172";
           ticker_LTCCNY= "0290378c-e3d7-4836-8cb1-2bfae20cc492";
           depth_BTCHKD= "049f65dc-3af3-4ffd-85a5-aac102b2a579";
           depth_BTCEUR= "057bdc6b-9f9c-44e4-bc1a-363e4443ce87";
           ticker_NMCAUD= "08c65460-cbd9-492e-8473-8507dfa66ae6";
           ticker_BTCEUR= "0bb6da8b-f6c6-4ecf-8f0d-a544ad948c15";
           depth_BTCKRW= "0c84bda7-e613-4b19-ae2a-6d26412c9f70";
           depth_BTCCNY= "0d1ecad8-e20f-459e-8bed-0bdcf927820f";
           ticker_BTCCAD= "10720792-084d-45ba-92e3-cf44d9477775";
           depth_BTCCHF= "113fec5f-294d-4929-86eb-8ca4c3fd1bed";
           ticker_LTCNOK= "13616ae8-9268-4a43-bdf7-6b8d1ac814a2";
           ticker_LTCUSD= "1366a9f3-92eb-4c6c-9ccc-492a959eca94";
           ticker_BTCBTC= "13edff67-cfa0-4d99-aa76-52bd15d6a058";
           ticker_LTCCAD= "18b55737-3f5c-4583-af63-6eb3951ead72";
           ticker_NMCCNY= "249fdefd-c6eb-4802-9f54-064bc83908aa";
           depth_BTCUSD= "24e67e0d-1cad-4cc0-9e7a-f8523ef460fe";
           ticker_BTCCHF= "2644c164-3db7-4475-8b45-c7042efe3413";
           depth_BTCAUD= "296ee352-dd5d-46f3-9bea-5e39dede2005";
           ticker_BTCCZK= "2a968b7f-6638-40ba-95e7-7284b3196d52";
           ticker_BTCSGD= "2cb73ed1-07f4-45e0-8918-bcbfda658912";
           ticker_NMCJPY= "314e2b7a-a9fa-4249-bc46-b7f662ecbc3a";
           ticker_BTCNMC= "36189b8c-cffa-40d2-b205-fb71420387ae";
           depth_BTCINR= "414fdb18-8f70-471c-a9df-b3c2740727ea";
           depth_BTCSGD= "41e5c243-3d44-4fad-b690-f39e1dbb86a8";
           ticker_BTCLTC= "48b6886f-49c0-4614-b647-ba5369b449a9";
           ticker_LTCEUR= "491bc9bb-7cd8-4719-a9e8-16dad802ffac";
           ticker_BTCINR= "55e5feb8-fea5-416b-88fa-40211541deca";
           ticker_LTCJPY= "5ad8e40f-6df3-489f-9cf1-af28426a50cf";
           depth_BTCCAD= "5b234cc3-a7c1-47ce-854f-27aee4cdbda5";
           ticker_BTCNZD= "5ddd27ca-2466-4d1a-8961-615dedb68bf1";
           depth_BTCGBP= "60c3af1b-5d40-4d0e-b9fc-ccab433d2e9c";
           depth_BTCNOK= "66da7fb4-6b0c-4a10-9cb7-e2944e046eb5";
           depth_BTCTHB= "67879668-532f-41f9-8eb0-55e7593a5ab8";
           ticker_BTCSEK= "6caf1244-655b-460f-beaf-5c56d1f4bea7";
           ticker_BTCNOK= "7532e866-3a03-4514-a4b1-6f86e3a8dc11";
           ticker_BTCGBP= "7b842b7d-d1f9-46fa-a49c-c12f1ad5a533";
           trade_lag= "85174711-be64-4de1-b783-0628995d7914";
           depth_BTCSEK= "8f1fefaa-7c55-4420-ada0-4de15c1c38f3";
           depth_BTCDKK= "9219abb0-b50c-4007-b4d2-51d1711ab19c";
           depth_BTCJPY= "94483e07-d797-4dd4-bc72-dc98f1fd39e3";
           ticker_NMCUSD= "9aaefd15-d101-49f3-a2fd-6b63b85b6bed";
           ticker_LTCAUD= "a046600a-a06c-4ebf-9ffb-bdc8157227e8";
           ticker_BTCJPY= "a39ae532-6a3c-4835-af8c-dda54cb4874e";
           depth_BTCCZK= "a7a970cf-4f6c-4d85-a74e-ac0979049b87";
           ticker_LTCDKK= "b10a706e-e8c7-4ea8-9148-669f86930b36";
           ticker_BTCPLN= "b4a02cb3-2e2d-4a88-aeea-3c66cb604d01";
           ticker_BTCRUB= "bd04f720-3c70-4dce-ae71-2422ab862c65";
           ticker_NMCGBP= "bf5126ba-5187-456f-8ae6-963678d0607f";
           ticker_BTCKRW= "bf85048d-4db9-4dbe-9ca3-5b83a1a4186e";
           ticker_BTCCNY= "c251ec35-56f9-40ab-a4f6-13325c349de4";
           depth_BTCNZD= "cedf8730-bce6-4278-b6fe-9bee42930e95";
           ticker_BTCHKD= "d3ae78dd-01dd-4074-88a7-b8aa03cd28dd";
           ticker_BTCTHB= "d58e3b69-9560-4b9e-8c58-b5c0f3fda5e1";
           ticker_BTCUSD= "d5f06780-30a8-4a48-a2f8-7ed181b4a13f";
           depth_BTCRUB= "d6412ca0-b686-464c-891a-d1ba3943f3c6";
           ticker_NMCEUR= "d8512d04-f262-4a14-82f2-8e5c96c15e68";
           trade_BTC= "dbf1dee9-4f2e-4a08-8cb7-748919a71b21";
           ticker_NMCCAD= "dc28033e-7506-484c-905d-1c811a613323";
           depth_BTCPLN= "e4ff055a-f8bf-407e-af76-676cad319a21";
           ticker_BTCDKK= "e5ce0604-574a-4059-9493-80af46c776b3";
           ticker_BTCAUD= "eb6aaa11-99d0-4f64-9e8c-1140872a423d"
        }