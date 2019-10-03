
font: Deja Vu Sans for Powerline
```
Add-PowerLineBlock { $MyInvocation.HistoryId }
Add-PowerLineBlock { New-PromptText " $(Get-Date -f ""T"") " -ErrorBackgroundColor "#FF0000" }
Add-PowerLineBlock { " $(Get-ShortenedPath -MaximumLength 40)" }
Add-PowerLineBlock { Write-VcsStatus }

Set-PowerLinePrompt -Colors "#2F5063" ,"#52DE49","#FDE640"
```
