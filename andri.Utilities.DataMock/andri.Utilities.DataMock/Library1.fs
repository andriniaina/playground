namespace andri.TestUtilities.Data



module TTT =
    open System.Data
    open FsMocks
    open FsMocks.Syntax2
    open Rhino.Mocks

    let myData =
        dict(
            [
                "col1",1;
                "col2",1;
                "col3",1;
                "col4",1;
                "col5",1;
                "col6",1
            ]
        )

    let MyReader = {new IDataReader with  }
    