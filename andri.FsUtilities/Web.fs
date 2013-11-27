namespace andri.Utilities
    module Web =
        open System
        open System.Net
        open System.IO

        let mutable proxy:IWebProxy = null

        let createWebRequest (uri:Uri) =
            let req = WebRequest.Create(uri)
            if proxy<>null then req.Proxy <- proxy
            req

        let getResponse (uri:Uri) =
            async {
                let req = uri |> createWebRequest
                let! response = req.AsyncGetResponse()
                use stream = response.GetResponseStream()
                use reader = new StreamReader(stream)
                return reader.ReadToEnd()
                }

