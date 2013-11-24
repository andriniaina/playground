
#load "XML.fs"
#load "XPath.fs"
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


let doc = fXML.loadString sxml
doc
    |> fXML.createNavidator
    |> fXPath.selectNodes "contacts/contact/name/@att"
    |> Seq.head
        |> fXPath.setValue "Andri atertsdfgsd"


doc.InnerXml