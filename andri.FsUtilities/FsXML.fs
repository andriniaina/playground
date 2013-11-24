namespace andri.FsUtilities

    module FsXML =
        open System.Xml
        let loadFile (file:string) =
            let doc = new XmlDocument()
            doc.Load(file)
            doc
        let loadStream (stream:System.IO.Stream) =
            let doc = new XmlDocument()
            doc.Load(stream)
            doc
        let loadString (s:string) =
            let doc = new XmlDocument()
            doc.LoadXml(s)
            doc
        let createNavidator (doc:XmlDocument) =
            doc.CreateNavigator()
        let saveToFile (file:string) (doc:XmlDocument) = doc.Save(file)
