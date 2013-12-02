module Newtonsoft.FsJson
    open System
    open System.Globalization
    open Newtonsoft.Json.Linq

    let private formatInfo_US = new NumberFormatInfo(NumberDecimalSeparator=".")

    let inline (?) (o:JToken) (p:string) = o.[p]
    let jsonString (t:JToken) = Convert.ToString((t :?> Newtonsoft.Json.Linq.JValue).Value)
    let jsonDouble (t:JToken) =  Convert.ToDouble((t :?> Newtonsoft.Json.Linq.JValue).Value, formatInfo_US)
    let jsonLong (t:JToken) =  Convert.ToInt64((t :?> Newtonsoft.Json.Linq.JValue).Value, formatInfo_US)
    let jsonInt (t:JToken) =  Convert.ToInt32((t :?> Newtonsoft.Json.Linq.JValue).Value, formatInfo_US)