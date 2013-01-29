rem 1 original
rem 2 modified
rem 3 base
rem 4 merged
copy /Y %2 %4
"C:\Program Files\Vim\vim73\gvim.exe" -d %3 %4 %1 -s p:\vim_tfs_merge.vim
