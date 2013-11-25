module Newtonsoft.FsJson
    open System
    open System.Globalization
    open Newtonsoft.Json.Linq

    let private FormatInfo_US = new NumberFormatInfo(NumberDecimalSeparator=".")

    let inline (?) (o:JToken) (p:string) = o.[p]
    let jsonString (t:JToken) = Convert.ToString((t :?> Newtonsoft.Json.Linq.JValue).Value)
    let jsonDouble (t:JToken) =  Convert.ToDouble((t :?> Newtonsoft.Json.Linq.JValue).Value, FormatInfo_US)
    (*
        type JsonWrapper = | JsonWrapper_JObject of JObject | JsonWrapper_JToken of JToken
        let (?) (w:JsonWrapper) (p:string) =
            match w with
            | JsonWrapper_JObject(o) -> o.[p]
            | JsonWrapper_JToken(o) -> o.[p]
        //let (?) (o:JObject) (i:int) = o.[i]
            let wrap (a:obj) =
                match a with 
                | :? JObject as o -> JsonWrapper_JObject(o)
                | :? JToken as o -> JsonWrapper_JToken(o)
                | _ -> failwith "Unknwn type"
*)