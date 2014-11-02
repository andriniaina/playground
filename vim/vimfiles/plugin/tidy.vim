" Tidy XML file
function! TidyXml() range
	let encoding = substitute(&l:fileencoding, "-", "", "")
	let cmd = expand('silent! '.a:firstline.','.a:lastline.'!$HOME/tidy --quiet yes --input-xml yes --indent yes --wrap 0 --wrap-attributes no --char-encoding '.encoding)
	execute cmd
	silent! %s/  /	/g
	echo cmd
endfunction
" Tidy HTML5 file
function! TidyHtml5()
	let encoding = substitute(&l:fileencoding, "-", "", "")
	let cmd = expand('silent! %!$HOME/tidy5 --quiet yes --input-xml no --indent auto --wrap 0 --bare yes --clean yes --tidy-mark no --wrap-attributes no --new-pre-tags script --tab-size 2 --char-encoding '.encoding)
	execute cmd
	silent! %s/  /	/g
endfunction
" Tidy HTML file
function! TidyHtml()
	let encoding = substitute(&l:fileencoding, "-", "", "")
	let cmd = expand('silent! %!$HOME/tidy --quiet yes --input-xml no --indent yes --wrap 0 --bare yes --clean yes --tidy-mark no --wrap-attributes no --char-encoding '.encoding)
	execute cmd
	silent! %s/  /	/g
endfunction
" Tidy SQL file
function! TidySQL() range
	let myrange = a:firstline.','.a:lastline
	let cmd = expand('silent! '.myrange.'!$HOME/SqlFormatter')
	execute cmd
	exe 'silent! '.myrange.'s/  /	/g'
	echo cmd
endfunction
