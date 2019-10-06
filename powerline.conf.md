
font: Deja Vu Sans for Powerline
```
Add-PowerLineBlock { $MyInvocation.HistoryId.ToString().PadLeft(3,"0") }
Add-PowerLineBlock { New-PromptText " $(Get-Date -f ""T"") " -ErrorBackgroundColor "#FF0000" }
Add-PowerLineBlock { " $(Get-ShortenedPath -MaximumLength 40)" }
Add-PowerLineBlock { Write-VcsStatus }

Set-PowerLinePrompt -Colors "#2F5063" ,"#52DE49","#FDE640"
Set-PowerLinePrompt -Colors "#2F5063","#11A8CD","#E5E5E5"
```
