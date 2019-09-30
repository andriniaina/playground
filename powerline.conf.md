
font: Deja Vu Sans for Powerline
```
Add-PowerLineBlock { $MyInvocation.HistoryId }
Add-PowerLineBlock { New-PromptText " $(Get-Date -f ""T"") " -ErrorBackgroundColor "#FF0000" }
Add-PowerLineBlock { " $(Get-ShortenedPath -MaximumLength 40)" }
Add-PowerLineBlock { Write-VcsStatus }

Set-PowerLinePrompt -Colors "Cyan" ,"#0066FF","Gray"
```
