namespace andri.Utilities
    module Web =
        open System
        open System.Net
        open System.IO

        let createWebRequest (uri:Uri) =
            let req = WebRequest.Create(uri)
            req.Proxy <- ServiceLocator.get "webproxy" (fun()-> null :> IWebProxy)
            req

        let getResponse (uri:Uri) =
            ServiceLocator.get (sprintf "webrequest:%s" uri.PathAndQuery) (fun()->
                async {
                    let req = uri |> createWebRequest
                    let! response = req.AsyncGetResponse()
                    use stream = response.GetResponseStream()
                    use reader = new StreamReader(stream)
                    return reader.ReadToEnd()
                    }
            )

