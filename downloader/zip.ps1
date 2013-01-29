dir -Exclude *.zip,*.cbz | foreach { & 'D:\Program Files\7-Zip\7z.exe' "a" "-tzip" ($_.Name+'.cbz') $_.Name -r }
