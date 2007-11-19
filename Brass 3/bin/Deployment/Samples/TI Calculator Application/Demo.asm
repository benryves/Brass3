.page 0

	; Jump to the main entry point.
	jp Main
	
	; Insert the branch table.
	.branch OffPageCall


Main

	; Clear the screen.
	.bcall _clrLCDFull

	; Set cursor to top-left.
	ld hl,0
	ld (min(curCol,curRow)),hl

	; Copy string from ROM to RAM for displaying.
	ld hl,StringPage0
	ld de,OP1
	ld bc,StringPage0Length
	ldir
	
	; Display the string.
	ld hl,OP1
	.bcall _putS
	
	; Move down a line.
	.bcall _newLine
	
	; Call a function on page 1 (note the _underscore).
	.bcall _OffPageCall
	
	.echoln strformat("{0:X4}", _OffPageCall)
	
	; Wait for a key.
	.bcall _getKey

	; Exit the application
	.bjump _JForceCmdNoChar

	StringPage0 .byte "Brass 3 Demo.", 0 \ StringPage0Length = $-StringPage0

.page 1

OffPageCall

	; Copy string from ROM to RAM for displaying.
	ld hl,StringPage1
	ld de,OP1
	ld bc,StringPage1Length
	ldir
	
	; Display the string.
	ld hl,OP1
	.bcall _putS
	
	ret

	StringPage1 .byte "Press any key.", 0 \ StringPage1Length = $-StringPage1