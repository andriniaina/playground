let $HOME='c:\dev\playground\vim'
set runtimepath+=$HOME\vimfiles
cd $HOME

set termguicolors
set t_Co=256
set encoding=utf-8
let g:airline_powerline_fonts=1
if !exists('g:airline_symbols')
  let g:airline_symbols = {}
endif
let g:airline_left_sep = ''
let g:airline_left_alt_sep = ''
let g:airline_right_sep = ''
let g:airline_right_alt_sep = ''
let g:airline_symbols.branch = ''
let g:airline_symbols.readonly = ''
let g:airline_symbols.linenr = '☰'
let g:airline_symbols.maxlinenr = ''
silent! set guifont=DelugiaCode\ NF:h11



set inccommand=split



source $HOME\_vimrc
