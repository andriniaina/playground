namespace andri.htmlagility

module FsHelpers =


    open System
    open System.IO
    open System.Net
    //open andri.htmlagility
    open Fizzler.Systems.HtmlAgilityPack

    let createWebRequestFromUri (uri:System.Uri) =
        uri |> HttpWebRequest.Create

    let createHttpWebRequestFromUri (uri:System.Uri) =
        createWebRequestFromUri uri :?> HttpWebRequest
        
    let createWebRequest (path:string) =
        createWebRequestFromUri (new Uri(path))
#if PORTABLE
#else
    let writePageContent filename uri =
        async {
            let request = createWebRequest uri
            use! response = request.AsyncGetResponse()
            use responseStream = response.GetResponseStream()
            use filestream = new FileStream(filename, FileMode.Create)
            responseStream.CopyTo(filestream)
            }
#endif
    let getPageContent uri =
        async {
            let request = createWebRequest uri
            use! response = request.AsyncGetResponse()
            let contentType_dic = response.ContentType.Split(';') |> Array.map (fun v -> v.Split([|'='|])) |> Array.map (fun a -> (a.[0].Trim(), if a.Length>1 then a.[1].Trim() else null)) |> dict// |> Array.find (fun v -> "charset"=(fst v))
            let encoding = System.Text.Encoding.GetEncoding(contentType_dic.["charset"])
            use stream = response.GetResponseStream()
            use reader = new System.IO.StreamReader(stream, encoding)
            return reader.ReadToEnd()// |> WebUtility.HtmlDecode
        }

    let toHtmlDocument content =
        let doc = new HtmlAgilityPack.HtmlDocument()
        doc.LoadHtml(content)
        doc

    let toHtmlNode content =
        let doc = toHtmlDocument content
        doc.DocumentNode
    
    let insertBefore (refNode:HtmlAgilityPack.HtmlNode) (newNode:HtmlAgilityPack.HtmlNode) =
        refNode.ParentNode.InsertBefore(newNode, refNode)
    
    let insertAfter (refNode:HtmlAgilityPack.HtmlNode) (newNode:HtmlAgilityPack.HtmlNode) =
        refNode.ParentNode.InsertAfter(newNode, refNode)
        (*
    let toHtmlNode (owner:HtmlAgilityPack.HtmlDocument) nodeName content =
        let el = owner.CreateElement("div")
        el.InnerHtml <- content
        el
        *)
    let getHtmlDocument uri = 
        async {
            let! content = getPageContent uri
            return toHtmlDocument content
            }

        (*
    let applyFilter (filter:IFilter) doc =
        filter.Apply(doc)
        doc
        *)
    let querySelector selector (node:HtmlAgilityPack.HtmlNode) =
        HtmlNodeSelection.QuerySelector(node,selector)

    let querySelectorAll selector (node:HtmlAgilityPack.HtmlNode) =
        HtmlNodeSelection.QuerySelectorAll(node,selector)
    
    let getAttributeValue (attributeName:string) (node:HtmlAgilityPack.HtmlNode) = 
        node.Attributes.[attributeName].Value
    let getAttributeValue_failSafe (attributeName:string) (node:HtmlAgilityPack.HtmlNode) = 
        if node.Attributes.Contains(attributeName) then node.Attributes.[attributeName].Value else ""

    let setAttributeValue (attributeName:string) value (node:HtmlAgilityPack.HtmlNode) = 
        node.SetAttributeValue(attributeName, value) |> ignore
        node

    let getInnerText (node:HtmlAgilityPack.HtmlNode) =
        node.InnerText
    let getInnerHtml (node:HtmlAgilityPack.HtmlNode) =
        node.InnerHtml
    let getOuterHtml (node:HtmlAgilityPack.HtmlNode) =
        node.OuterHtml
    