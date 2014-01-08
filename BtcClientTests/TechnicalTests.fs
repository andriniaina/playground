namespace BtcClientTests

open andri.Utilities
open andri.BtcClient
open Xunit
open FsUnit.Xunit
open Newtonsoft.FsJson
open System


module TechnicalTests = 
    let [<Fact>] ``BitcoinCharts_parseFloat``()=
        BitcoinCharts.parseFloat "1.181851570000" |> should equal 1.181851570000
    
    let [<Fact>] ``BitcoinCharts_parseInt``()=
        BitcoinCharts.parseInt "18185157" |> should equal 18185157
        
    let [<Fact>] toUnixTicksTest() =
        BitcoinCharts.toUnixTicks (new DateTime(2013,12,1,5,6,7)) |> should equal 1385874367L

    let [<Fact>] parseUnixTicks() =
        BitcoinCharts.parseUnixTicks "1385874367" |> should equal (new DateTime(2013,12,1,5,6,7))


