namespace andri.Utilities


module Strings =
    open System
    let (|StartsWith|) prefix (s:string) =
        s.StartsWith(prefix)

    let Substring start ``end`` (s:string) =
        if ``end``<0 then s.Substring(start, s.Length+``end``-start) else s.Substring(start, ``end``-start)
    let Trim (s:string) =
        s.Trim()
    let Compact (s:string) =
        let spaces = System.Text.RegularExpressions.Regex(@"\s+")
        spaces.Replace(spaces.Replace(Trim s, ""), "")
