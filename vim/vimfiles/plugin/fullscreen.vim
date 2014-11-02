let s:isFullScreen = 0
function ToggleFullscreen()
	if s:isFullScreen
		winc =
		let s:isFullScreen = 0
	else
		vertical resize | resize
		let s:isFullScreen = 1
	endif
endfunction

