/*
	This very simple project should hopefully demonstrate how simple
	Brass 3 makes knocking out assembly programs.
	
	The include file and shell header "noise" at the start of the
	project is moved out into an external project template.

 */
 
	; Clear the screen.
	.bcall _clrLcdFull
	
	; Move cursor to (0,0).
	xor a
	ld (curCol),a
	ld (curRow),a
	
	; Display the message.
	ld hl,Message
	.bcall _putS
	.bcall _newLine


	; Dummy keyboard read.
	.bcall _getCSC
	
	; Poll the keyboard until we receive a key.
-	.bcall _getCSC
	or a
	jr z,-
	
	; Exit.
	ret

; Message to be displayed.
Message .byte "Brass 3 Demo", 0