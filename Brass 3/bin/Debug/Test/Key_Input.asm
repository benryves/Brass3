; Key_Input.asm
; Benjamin Ryves 2005 [benryves@benryves.com]
; Keyboard helpful routines

.module KeyboardInput

; Load a keyboard table
; This function searches for a scancode translation table
; on the calculator's memory using Ion's 
keyi_load_table:
	ld ix,_table_name
	ld hl,(TI.progPtr)
	call TI.ionDetect
	ret nz
	ld (_table_location),hl
	xor a
	ret
_table_location:
	.dw 0
_table_name:
	.db "EM1",0

; Translate a key based from the scancode to an ASCII equivalent.
; Note that SOME characters are NOT printable by the TIOS.
; The control codes ($00-$1F) are not handled by the TIOS's text routines
; (eg _putC).
; Also, some keys do return an Emerson code but are not printable in any
; way. These will return a special code. You can detect whether to print
; them by checking the 7th bit (they are all in the $80-$FF range).
; Finally, the ASCII translation tables should map all keys to TIOS-
; -friendly codes. For example, '[' should be mapped as $C1 and not
; the more usual $5B bcause the TIOS is silly and wants Theta to be $5B.
; One further point to note - these routines handle control codes.
; Pressing Ctrl and H together will, for example, return ^H (backspace).

keyi_translate:

	push af

	; Before anything else, handle the easy case of having NumLock pressed.
	ld a,(keyb_status)
	and at_stat_num
	jr z,_no_numlock	; NumLock not pressed

	; Jump to the NumLock table:

	ld de,108*2		; 108 keys, 2 sets
	ld hl,(_table_location)
	add hl,de
	ld e,(hl)
	ld d,0
	inc hl
	add hl,de
	ld c,(hl)
	ld b,0
	push bc
	pop de			; We might need this later!
	inc hl

	pop af			; Get our beloved Emerson code back!
	push af

	cpir
	jr nz,_no_numlock	; NumLock was pressed, but the actual Emerson code
				; was not on the number pad.

	add hl,de
	pop af			; We won't be needing this...
	dec hl
	ld a,(hl)
	ret

_no_numlock:



	; Now, we need to know if CapsLock is pressed or not.
	ld a,(keyb_status)
	ld b,a
	and at_stat_caps
	jr z,_not_capslock

	; So, caps lock is pressed.
	; If the key is in our "CapsLockable" range, we'll force Shift down.
	; We need to check else CapsLock acts like Shift Lock on a BBC Micro. :)

	; Let's find that part of our tables.

	ld de,108*2	; 108 keys, 2 sets
	ld hl,(_table_location)
	add hl,de

	ld c,(hl)	; Number of capslockable keys

	pop af		; Reclaim our Emerson code off the stack
	push af

	push bc	; Preserve our status byte

	ld b,0
	inc hl

	cpir		; Is it valid?

	pop bc		; Recall our status byte

	jr nz,_not_capslock

	; Toggle shift.
	ld a,b
	xor at_stat_shift
	ld b,a

_not_capslock:

	pop af

	ld hl,(_table_location)
	ld e,a
	ld d,0
	add hl,de
	dec hl

	; Are we shifting?
	ld a,b
	and at_stat_shift
	jr z,_not_shifting

	ld de,108
	add hl,de

_not_shifting:
	ld a,(hl)

	; Last of all we need to work out if this is a Ctrl code or not

	push af

	ld a,(keyb_status)
	and at_stat_ctrl
	jr nz,_ctrl_pressed
	pop af
	ret

_ctrl_pressed:
	; If the key is a->z, we need to uppercase it.
	pop af	; Reclaim the ASCII code
	push af
	cp 'a'
	jr c,_not_lower
	cp ('z'*1)+1
	jr nc,_not_lower
	add a,'A'-'a'
_not_lower:

	; Swap theta / [
	; [ = %11000001
	; T = %01011011

	cp $C1
	jr z,_need_swap
	cp $5B
	jr nz,_no_need_swap
_need_swap:
	xor $C1^$5B
_no_need_swap:

; Adjust!
	sub '@'

	cp 32		; Check for valid ctrl code
	jr c,_valid_ctrl
	pop af		; Reclaim ASCII code
	ret
_valid_ctrl:
	pop bc	; pop into dud register
	ret

; Get a character code that can be used to print a cursor.
; It is based on the status of the keyboard, and looks like:
; _ (Underscore) - normal state.
; ^ (Up arrow on underscore) - shift is held.
; A (Capital A on underscore) - caps lock is on.
; a (Lower case a on underscore) - caps lock + shift.

keyi_get_status_icon:
	ld a,(keyb_status)
	and at_stat_shift|at_stat_caps
	ld hl,_status_icons
	ld bc,4
	cpir
	ld de,$E3-_status_icons
	add hl,de
	ld a,l
	ret

_status_icons:
.db 0
.db at_stat_shift
.db at_stat_caps
.db at_stat_shift|at_stat_shift

#include "Keyboard.asm"