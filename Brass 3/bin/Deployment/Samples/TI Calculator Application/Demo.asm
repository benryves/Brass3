.page 0

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
	
	; Wait for a key.
	.bcall _getKey

	; Exit the application
	.bjump _JForceCmdNoChar

StringPage0
	.byte "Brass 3 Demo", 0
StringPage0Length = $-StringPage0