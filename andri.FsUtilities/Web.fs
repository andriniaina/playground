namespace andri.Utilities
    module Web =
        open System
        open System.Net
        open System.IO

        let createWebRequest (uri:Uri) =
            WebRequest.Create(uri)

        let getResponse (uri:Uri) =
            async {
                let req = uri |> createWebRequest
                let! response = req.AsyncGetResponse()
                use stream = response.GetResponseStream()
                use reader = new StreamReader(stream)
                return reader.ReadToEnd()
                }

