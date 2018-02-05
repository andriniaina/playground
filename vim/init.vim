let $HOME='u:\dev\playground\vim'
set runtimepath+=$HOME\vimfiles
cd $HOME

set termguicolors
set t_Co=256
set encoding=utf-8
let g:airline_powerline_fonts=1
if !exists('g:airline_symbols')
  let g:airline_symbols = {}
endif
let g:airline_left_sep = "\u2b80"
let g:airline_left_alt_sep = "\u2b81"
let g:airline_right_sep = "\u2b82"
let g:airline_right_alt_sep = "\u2b83"
let g:airline_symbols.branch = "\u2b60"
let g:airline_symbols.readonly = "\u2b64"
let g:airline_symbols.linenr = "\u2b61"
let g:airline_symbols.space = "\ua0"
let g:airline_symbols.whitespace = "\u02fd"
let g:airline_symbols.modified = "*"



source $HOME\_vimrc
