
#I @"D:\dev\playground\andri.htmlagility\Fizzler\"
#r "Fizzler.Systems.HtmlAgilityPack.dll"
#r "HtmlAgilityPack.dll"


#load "andri.htmlagility.FsHelpers.fs"

open andri.htmlagility.FsHelpers
open System



let doc = getHtmlDocument "http://onvasortir.com" |> Async.RunSynchronously


let liens = doc |> querySelectorAll ".LienHome0, .LienHome1, .LienHome2" |> Seq.map (fun node -> (node.InnerText, getAttributeValue "href" node)) |> Seq.sort
liens |> List.ofSeq
liens |> Seq.iter (fun kv -> printfn @"{""%s"",""%s""}," (fst kv) (snd kv) )
