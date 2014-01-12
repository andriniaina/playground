// Learn more about F# at http://fsharp.net

#I @"D:\dev\htmlagilitypack-99964\Release\Fizzler.Systems.HtmlAgilityPack\bin\Debug"

#r @"HtmlAgilityPack.dll"
#r @"Fizzler.dll"
#r @"Fizzler.Systems.HtmlAgilityPack.dll"
#r @"D:\dev\playground\andri.htmlagility\bin\debug\andri.htmlagility.dll"

#load @"andri.htmlagility.FsHelpers.fs"


open andri.htmlagility.FsHelpers

open System
open System.IO

let chapters =
    [
//    "Vol 01 Ch 001: The Legendary Swindler","v01/c001";
//    "Vol 01 Ch 002: Flaw","v01/c002";
//    "Vol 01 Ch 003: Declaration of war","v01/c003";
//    "Vol 01 Ch 004: Intimidating the Enemy","v01/c004";
//    "Vol 01 Ch 005: Dissonance","v01/c005";
//    "Vol 01 Ch 006: Game Over","v01/c006";
//    "Vol 01 Ch 007: Game II","v01/c007";
//    "Vol 02 Ch 008: Commence Battle","v02/c008";
//    "Vol 02 Ch 009: Impatience","v02/c009";
//    "Vol 02 Ch 010: Hope","v02/c010";
//    "Vol 02 Ch 011: Alliance","v02/c011";
//    "Vol 02 Ch 012: Catastrophy","v02/c012";
//    "Vol 02 Ch 013: Light vs Dark","v02/c013";
//    "Vol 02 Ch 014: Scheme","v02/c014";
//    "Vol 02 Ch 015: Upper Hand","v02/c015";
//    "Vol 02 Ch 016: Foresight","v02/c016";
//    "Vol 02 Ch 017: Trap","v02/c017";
//    "Vol 03 Ch 018: Revival Round","v03/c018";
//    "Vol 03 Ch 019: Downsizing Game","v03/c019";
//    "Vol 03 Ch 020: Incitement","v03/c020";
//    "Vol 03 Ch 021: Scapegoat","v03/c021";
//    "Vol 03 Ch 022: Battle Plan","v03/c022";
//    "Vol 03 Ch 023: Light vs Dark","v03/c023";
//    "Vol 03 Ch 024: Retribution","v03/c024";
//    "Vol 03 Ch 025: Comeback","v03/c025";
//    "Vol 03 Ch 026: Control","v03/c026";
//    "Vol 03 Ch 027: Inflation","v03/c027";
//    "Vol 03 Ch 028: Salvation","v03/c028";
//    "Vol 04 Ch 029: Game III","v04/c029";
//    "Vol 04 Ch 030: Contraband","v04/c030";
//    "Vol 04 Ch 031: Head Start","v04/c031";
//    "Vol 04 Ch 032: Chicken","v04/c032";
//    "Vol 04 Ch 033: Yokoya","v04/c033";
//    "Vol 04 Ch 034: Clairvoyance","v04/c034";
//    "Vol 04 Ch 035: Confrontation","v04/c035";
//    "Vol 04 Ch 036: Division","v04/c036";
//    "Vol 04 Ch 037: Discovered","v04/c037";
//    "Vol 04 Ch 038: Confession","v04/c038";
//     "Vol 05 Ch 039: Recollection","v05/c039";
//     "Vol 05 Ch 040: Counterattack","v05/c040";
//     "Vol 05 Ch 041: Unrest","v05/c041";
//     "Vol 05 Ch 042: Experiment","v05/c042";
//     "Vol 05 Ch 043: Strategy","v05/c043";
//     "Vol 05 Ch 044: Miscalculation","v05/c044";
//     "Vol 05 Ch 045: Pressure","v05/c045";
//     "Vol 05 Ch 046: Confusion","v05/c046";
//     "Vol 05 Ch 047: Reign","v05/c047";
//     "Vol 05 Ch 048: Preparation","v05/c048";
//     "Vol 05 Ch 049: Tip-Off","v05/c049";
//     "Vol 06 Ch 050: Interrogation","v06/c050";
//     "Vol 06 Ch 051: Confession","v06/c051";
//     "Vol 06 Ch 052: Scheme","v06/c052";
//     "Vol 06 Ch 053: Caution","v06/c053";
//     "Vol 06 Ch 054: Completion","v06/c054";
//     "Vol 06 Ch 055: Disturbance","v06/c055";
//     "Vol 06 Ch 056: Blunder","v06/c056";
//     "Vol 06 Ch 057: Solidarity","v06/c057";
//     "Vol 06 Ch 058: Discrepancy","v06/c058";
//     "Vol 06 Ch 059: Provocation","v06/c059";
//     "Vol 07 Ch 060: No Way Out","v07/c060";
//     "Vol 07 Ch 061: Simulation","v07/c061";
//     "Vol 07 Ch 062: Reload","v07/c062";
//     "Vol 07 Ch 063: Shoot","v07/c063";
//     "Vol 07 Ch 064: Pitfall","v07/c064";
//     "Vol 07 Ch 065: Insight","v07/c065";
//     "Vol 07 Ch 066: Avarice","v07/c066";
//     "Vol 07 Ch 067: Agitation","v07/c067";
//     "Vol 07 Ch 068: Centerfield","v07/c068";
//     "Vol 07 Ch 069: Natural Talent","v07/c069";
//     "Vol 07 Ch 070: Enticement","v07/c070";
//     "Vol 08 Ch 071: Divine Sight","v08/c071";
//     "Vol 08 Ch 072: Counterattack","v08/c072";
//     "Vol 08 Ch 073: Intuition","v08/c073";
//     "Vol 08 Ch 074: Request","v08/c074";
//     "Vol 08 Ch 075: Luck","v08/c075";
//     "Vol 08 Ch 076: Ruse","v08/c076";
//     "Vol 08 Ch 077: General","v08/c077";
//     "Vol 08 Ch 078: Coercion","v08/c078";
//     "Vol 08 Ch 079: Falter","v08/c079";
//     "Vol 08 Ch 080: Secret Maneuver","v08/c080";
//     "Vol 08 Ch 081: Distrust","v08/c081";
//     "Vol 08 Ch 082: Surrender","v08/c082";
//     "Vol 08 Ch 083: Ambition","v08/c083";
//    "Vol 09 Ch 084: Game IV","v09/c084";
//    "Vol 09 Ch 085: Pandemic Game","v09/c085";
//    "Vol 09 Ch 086: Agree","v09/c086";
//    "Vol 09 Ch 087: Virus Outbreak","v09/c087";
//    "Vol 09 Ch 088: Confirmation","v09/c088";
//    "Vol 09 Ch 089: Conspire","v09/c089";
//    "Vol 09 Ch 090: Stalemate","v09/c090";
//    "Vol 09 Ch 091: Doubt","v09/c091";
//    "Vol 09 Ch 092: Anxiety","v09/c092";
//    "Vol 09 Ch 093: Differentiate","v09/c093";
//    "Vol 09 Ch 094: Setup","v09/c094";
//    "Vol 10 Ch 095: Overturn","v10/c095";
+++++++
//    "Vol 10 Ch 096: Intent","v10/c096";
//    "Vol 10 Ch 097: Trust","v10/c097";
//    "Vol 10 Ch 098: Selflessness","v10/c098";
//    "Vol 10 Ch 099: Check","v10/c099";
//    "Vol 10 Ch 100: Lockout","v10/c100";
//    "Vol 10 Ch 101: Bribery","v10/c101";
//    "Vol 10 Ch 102: Closure","v10/c102";
//    "Vol 10 Ch 103: Contest","v10/c103";
//    "Vol 10 Ch 104: Musical Chairs","v10/c104";
//    "Vol 10 Ch 105: Strategy","v10/c105";
//    "Vol 11 Ch 106: Deception","v11/c106";
//    "Vol 11 Ch 107: Maneuver","v11/c107";
//    "Vol 11 Ch 108: Friendship","v11/c108";
//    "Vol 11 Ch 109: Medals","v11/c109";
//    "Vol 11 Ch 110: Foundation","v11/c110";
//    "Vol 11 Ch 111: Bonds","v11/c111";
//    "Vol 11 Ch 112: Union","v11/c112";
//    "Vol 11 Ch 113: Recapture","v11/c113";
//    "Vol 11 Ch 114: Surprise Attack","v11/c114";
//    "Vol 11 Ch 115: Abstaining","v11/c115";
//    "Vol 11 Ch 116: Cause","v11/c116";
    "Vol 12 Ch 117: Breach","v12/c117";
    "Vol 12 Ch 118: Ambush","v12/c118";
    "Vol 12 Ch 119: Alliance","v12/c119";
    "Vol 12 Ch 120: Puppeteer","v12/c120";
    "Vol 12 Ch 121: Dummy","v12/c121";
    "Vol 12 Ch 122: Logical Reasoning","v12/c122";
    "Vol 12 Ch 123: Turn Around","v12/c123";
    "Vol 12 Ch 124: Chance","v12/c124";
    "Vol 12 Ch 125: Demons","v12/c125";
    "Vol 12 Ch 126: Heartless","v12/c126";
    "Vol 12 Ch 127: Betrayal","v12/c127";
    "Vol 13 Ch 128: Prediction","v13/c128";
    "Vol 13 Ch 129: Rift","v13/c129";
    "Vol 13 Ch 130: Negotiations","v13/c130";
    "Vol 13 Ch 131: Make &amp; Break","v13/c131";
    "Vol 13 Ch 132: Trickster","v13/c132";
    "Vol 13 Ch 133: Under Control","v13/c133";
    "Vol 13 Ch 134: Counterattack","v13/c134";
    "Vol 13 Ch 135: Shorn","v13/c135";
    "Vol 13 Ch 136: Overwhelming","v13/c136";
    "Vol 13 Ch 137: Unity","v13/c137";
    "Vol 13 Ch 138: Conclusion","v13/c138";
//    "Vol 14 Ch 139: Resolve","v14/c139";
//    "Vol 14 Ch 140: Twist","v14/c140";
//    "Vol 14 Ch 141: Bid Poker","v14/c141";
//    "Vol 14 Ch 142: Growth","v14/c142";
//    "Vol 14 Ch 143: Unexpected","v14/c143";
//    "Vol 14 Ch 144: Start","v14/c144";
//    "Vol 14 Ch 145: Bad Omen","v14/c145";
//    "Vol 14 Ch 146: Foolhardiness","v14/c146";
//    "Vol 14 Ch 147: Mysterious Power","v14/c147";
//    "Vol 14 Ch 148: Blasphemy","v14/c148";
//    "Vol 14 Ch 148.5: How I Lost a Manga Contest Yet Again...","v14/c148.5";
//    "Vol 14 Ch 149: Insight","v14/c149";
//    "Vol 14 Ch 150: Cornering the Enemy","v14/c150";
//    "Vol 15 Ch 151: Collapse","v15/c151";
//    "Vol 15 Ch 152: Prologue","v15/c152";
//    "Vol 15 Ch 153: Faith","v15/c153";
//    "Vol 15 Ch 154: As One","v15/c154";
//    "Vol 15 Ch 155: Falsehood","v15/c155";
//    "Vol 15 Ch 156: Multi-layered","v15/c156";
//    "Vol 15 Ch 157: Strategist","v15/c157";
//    "Vol 15 Ch 158: Reversal","v15/c158";
//    "Vol 15 Ch 159: Resignation","v15/c159";
//    "Vol 15 Ch 160: Iron-tight","v15/c160";
//    "Vol 15 Ch 161: Underground","v15/c161";
//    "Vol 15 Ch 162: Shutout","v15/c162";
//    "Vol 15 Ch 163: In the Lead","v15/c163"
    ]

let rec downloadImage nbTries baseFolder url = 
    try
        let image_url = getPageContent_noEnconding url |> Async.RunSynchronously |> toHtmlNode |> querySelector "#image" |> getAttributeValue "src"
        printfn "downloading %s" image_url
        let remote_filename = Path.GetFileName((new Uri(image_url)).LocalPath)
        let filename = String.Format("{0}/{1}", baseFolder, remote_filename)
        writePageContent filename image_url |> Async.RunSynchronously
    with
    | ex ->
            System.Threading.Thread.Sleep 500
            if nbTries>2
            then printfn "Erreur dans : %s, %s" url (ex.Message)
            else downloadImage (nbTries+1) baseFolder url

let downloadChapters (chapters:(string * string) list) = 
    for title,url_part in chapters do
        let comments_url = String.Format("http://mangafox.me/manga/liar_game/{0}/", url_part)
        let pages = comments_url |> getPageContent_noEnconding |> Async.RunSynchronously |> toHtmlNode |> querySelectorAll ".page select.m option" |> List.ofSeq
        let n = Int32.Parse(pages.[pages.Length-2] |> getAttributeValue "value")
        
        Directory.CreateDirectory(String.Format("{0}", url_part)) |> ignore

        for i in 1..n do
            let url = String.Format("http://mangafox.me/manga/liar_game/{0}/{1}.html", url_part, i)
            downloadImage 0 url_part url


System.Environment.CurrentDirectory <- @"D:\dev\playground\downloader\Downloads"
downloadChapters chapters

