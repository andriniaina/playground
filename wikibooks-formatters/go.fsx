
#r "System.dll"

#r @"D:\dev\playground\andri.htmlagility\bin\Debug\HtmlAgilityPack.dll"
#r @"D:\dev\playground\andri.htmlagility\bin\Debug\andri.htmlagility.dll"

open System
open System.IO
open System.Net
open andri.htmlagility

#r @"D:\dev\playground\andri.htmlagility\bin\Debug\Fizzler.Systems.HtmlAgilityPack.dll"
open Fizzler.Systems.HtmlAgilityPack
open HtmlAgilityPack

let createWebRequestFromUri (uri:System.Uri) =
    HttpWebRequest.Create(uri)

let createWebRequest (path:string) =
    createWebRequestFromUri (new Uri(path))

let writePageContent filename uri =
    let request = createWebRequest uri
    use response = request.GetResponse()
    use responseStream = response.GetResponseStream()
    use filestream = new FileStream(filename, FileMode.Create)
    responseStream.CopyTo(filestream)

let getPageContent uri =
    let request = createWebRequest uri
    use response = request.GetResponse()
    use stream = response.GetResponseStream()
    use reader = new System.IO.StreamReader(stream)
    reader.ReadToEnd() |> WebUtility.HtmlDecode

let toHtmlDocument content =
    let doc = new HtmlAgilityPack.HtmlDocument()
    doc.LoadHtml(content)
    doc

let getHtmlDocument uri = 
    getPageContent uri |> toHtmlDocument

let applyFilter (filter:IFilter) doc =
    filter.Apply(doc)
    doc

let selectNode selector (doc:HtmlAgilityPack.HtmlDocument) =
    HtmlNodeSelection.QuerySelector(doc.DocumentNode,selector)
let selectNodes selector (doc:HtmlAgilityPack.HtmlDocument) =
    HtmlNodeSelection.QuerySelectorAll(doc.DocumentNode,selector)

let selectAttributeValue (attributeName:string) (node:HtmlAgilityPack.HtmlNode) = 
        node.Attributes.[attributeName].Value


type HeadingArticle_to_h1() = 
    inherit TransformNodeFilter("p[class=HeadingArticle]", fun node -> node.Name<-"h1")
type Heading1_to_h2() = 
    inherit TransformNodeFilter("p[class=Heading1]", fun node -> node.Name<-"h2")
type Heading2_to_h3() = 
    inherit TransformNodeFilter("p[class=Heading2]", fun node -> node.Name<-"h3")
type Heading3_to_h4() = 
    inherit TransformNodeFilter("p[class=Heading3]", fun node -> node.Name<-"h4")
type Filter_preformatted() = 
    inherit TransformNodeFilter("p[class=Preformatted]", fun node ->  node.SetAttributeValue("style", "background-color:#e6e6e6;font-size:smaller;font-family:'Courier New'") |> ignore)

let clean (htmlFile:string) = 
    use reader = new StreamReader(htmlFile)
    let doc = new HtmlDocument()
    doc.Load(reader)
    doc
        |> applyFilter (new NegativeFilter("style"))
        |> applyFilter (new Filter_preformatted())
        |> applyFilter (new HeadingArticle_to_h1())
        |> applyFilter (new Heading1_to_h2())
        |> applyFilter (new Heading2_to_h3())
        |> applyFilter (new Heading3_to_h4())

let doc = clean "F2.html"
printfn "%s" doc.DocumentNode.OuterHtml
