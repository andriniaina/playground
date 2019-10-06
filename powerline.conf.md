
font: Deja Vu Sans for Powerline
```
Add-PowerLineBlock { $MyInvocation.HistoryId.ToString().PadLeft(3,"0") }
Add-PowerLineBlock { New-PromptText " $(Get-Date -f ""T"") " -ErrorBackgroundColor "#FF0000" }
Add-PowerLineBlock { " $(Get-ShortenedPath -MaximumLength 40)" }
Add-PowerLineBlock { Write-VcsStatus }

Set-PowerLinePrompt -Colors "#2F5063" ,"#52DE49","#FDE640"
Set-PowerLinePrompt -Colors "#2F5063","#11A8CD","#E5E5E5"
```


Windows Terminal profiles.json :
```

  "profiles": [
    {
      "useAcrylic": true,
      "fontFace": "DelugiaCode NF",
      "colorScheme": "One Half Dark",
      "acrylicOpacity": 0.9,

      // Make changes here to the powershell.exe profile
      "guid": "{61c54bbd-c2c6-5271-96e7-009a87ff44bf}",
      "name": "Windows PowerShell",
      "commandline": "powershell.exe",
      "hidden": false
    },
```
