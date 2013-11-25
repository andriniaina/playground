module Newtonsoft.FsJson
    open System
    open System.Globalization
    open Newtonsoft.Json.Linq

    let private FormatInfo_US = new NumberFormatInfo(NumberDecimalSeparator=".")

    let inline (?) (o:JToken) (p:string) = o.[p]
    let jsonString (t:JToken) = Convert.ToString((t :?> Newtonsoft.Json.Linq.JValue).Value)
    let jsonDouble (t:JToken) =  Convert.ToDouble((t :?> Newtonsoft.Json.Linq.JValue).Value, FormatInfo_US)
    let jsonLong (t:JToken) =  Convert.ToInt64((t :?> Newtonsoft.Json.Linq.JValue).Value, FormatInfo_US)