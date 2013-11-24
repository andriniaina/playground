namespace andri.FsUtilities
    open System.Xml.XPath
    module FsXPath =
        let private createNavigator (doc:XPathDocument) = doc.CreateNavigator()
        let loadUri (uri:string) = new XPathDocument(uri) |> createNavigator
        let loadTextReader (reader:System.IO.TextReader) = new XPathDocument(reader) |> createNavigator
        let evalExpression (expr:string) (nav:XPathNavigator) = nav.Evaluate(expr)
        let selectSingleNode (expr:string) (nav:XPathNavigator) =
            nav.SelectSingleNode(expr)
        let selectNodes (expr:string) (nav:XPathNavigator) =
            seq { for node in nav.Select(expr) do yield (node:?>XPathNavigator) }
        let setValue v (nav:XPathNavigator) =
            nav.SetValue v
            nav