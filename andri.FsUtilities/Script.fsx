
#load "FsXML.fs"
#load "FsXPath.fs"
open andri.FsUtilities

open System.IO
let sxml = @"<contacts>
  <contact>
    <name att='s'>Patrick Hines</name>
    <phone>206-555-0144</phone>
    <address>
      <street1>123 Main St</street1>
      <city>Mercer Island</city>
      <state>WA</state>
      <postal>68042</postal>
    </address>
  </contact>
</contacts>"


let doc = FsXML.loadString sxml
doc
    |> FsXML.createNavidator
    |> FsXPath.selectNodes "contacts/contact/name/@att"
    |> Seq.head
        |> FsXPath.setValue "Andri atertsdfgsd"


doc.InnerXml