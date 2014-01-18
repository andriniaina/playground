type BtcClient.Data.sql | d:\dev\sqlite3.exe BtcClient.db3


D:\dev\DbLinq-0.20.1\DbMetal.exe /provider:Sqlite /conn "Data Source=.\BtcClient.db3" /dbml:BtcClient.dbml
D:\dev\DbLinq-0.20.1\DbMetal.exe /code:BtcClient.Data.cs BtcClient.dbml --namespace=andri.BtcClient.Data
"C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" BtcClient.Data.csproj /p:Configuration=Debug /p:Platform=x86
