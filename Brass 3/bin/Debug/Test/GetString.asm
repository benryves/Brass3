.module TextBox

_get_string:
	ld (_cursor_pos),hl
	ld (_string_pos),hl
	ld (_root_cur),hl
	add hl,bc
	ld (_string_end),hl
	ld hl,0
	ld (_string_len),hl
	ld (_scroll),hl
	ret

_update:
	ld a,(_top)
	ld (TI.curRow),a
	ld a,(_left)
	ld (TI.curCol),a


	ld bc,(_string_len)
	ld a,b
	or c
	ret z	; Done the entire string
	ld hl,(_root_cur)

	ld a,(hl)
	cp 32
	jp nc,_is_not_control_code
	
	cp $0A
	jr nz,_not_lf

	ld a,(_left)
	ld (TI.curCol),a
	ld a,(TI.curRow)
	inc a
	ld a,0
	push bc


_not_lf:



_left:		.db 0	; Left edge of the control coordinate.
_right:	.db 15	; Right edge of the control coordinate.
_top:		.db 0	; Top edge of the control coordinate.
_bottom:	.db 7	; Bottom edge of the control coordinate.

_cursor_pos:	.dw 0	; Current cursor position inside the string.
_string_pos:	.dw 0	; Location of the string in memory.
_string_end:	.dw 0	; End location of the string in memory. (Do not write to it!)

_string_len:	.dw 0	; Length of the string

_scroll:	.dw 0	; How many elements have we scrolled?

_root_cur:	.dw 0	; Which character is the top-left of the control looking at?

_putmap:
	cp $FF
	ret z

	push af
	ld a,' '
	bcall(TI._putMap)
	pop af

	cp 32
	jr nc,_is_not_control_code
	
	cp $07	; ^G BEL
	jr nz,_not_bell
	; Ring the bell
	ret

_not_bell:
	cp $08	; ^H BS
	jr nz,_not_backspace

	ld a,(TI.curCol)
	dec a
	cp -1
	jr nz,_not_bs_off_left

	ld a,(TI.curRow)
	dec a
	cp -1
	ret z
	ld (TI.curRow),a
	ld a,15
_not_bs_off_left:
	ld (TI.curCol),a
	ld a,' '
	bcall(TI._putMap)
	ret

_not_backspace:

	cp $09	; ^I TAB
	jr nz,_not_tab

	ld a,' '
	bcall(TI._putMap)

	ld a,(TI.curCol)
	add a,4
	and %11111100
	cp 15
	jr c,_not_tabbed_off_right
	bcall(TI._newLine)
	xor a
_not_tabbed_off_right:
	ld (TI.curCol),a
	ret

_not_tab:

	cp $0A	; ^J LF
	jr nz,_not_linefeed
	bcall(TI._newLine)
	ret
_not_linefeed:

	cp $0D	; ^M CR
	jr nz,_not_carriage_return
	xor a
	ld (TI.curCol),a
	ret
_not_carriage_return:

	; What is it?
	; God knows.
	add a,'@'
	push af
	ld a,'^'
	bcall(TI._putC)
	pop af

	cp '['
	jr nz,_not_thetatime
	ld a,$C1
_not_thetatime:
	bcall(TI._putC)
	ret

_is_not_control_code:
	
	; Now we check for normal codes that TI stuck in the 128->255 range

	cp $C1			; [
	jr z,_dont_ignore_me	

	bit 7,a
	ret nz	; We don't handle 128-255 at ALL. These are special (non-character) codes.
_dont_ignore_me:
	bcall(TI._putC)
	ret 