" This creates a keyword ERROR and puts it in the highlight group called logError
:syn keyword logError ERROR
:syn keyword logInfo INFO
:syn keyword logWarn WARN
" This creates a match on the date and puts in the highlight group called logDate.  The
" nextgroup and skipwhite makes vim look for logTime after the match
:syn match logDate /^\d\{4}-\d\{2}-\d\{2}/ nextgroup=logTime skipwhite
:syn match logClass /\(\w\+\.\)\+\w\+/
:syn match logId /\[.\+\]/

" This creates a match on the time (but only if it follows the date)
:syn match logTime /\d\{2}:\d\{2}:\d\{2},\d\{3}/

hi def logWarn guifg=orange

" Now make them appear:
" Link just links logError to the colouring for error
hi link logDate Comment
hi link logTime Identifier
hi link logError Error
hi link logWarn Error
hi link logInfo Statement
hi link logClass Comment
hi link logId Comment
" Def means default colour - colourschemes can override
"hi def logDate guibg=yellow guifg=blue
"hi def logTime guibg=green guifg=white
