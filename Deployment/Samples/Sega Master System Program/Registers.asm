/*
 * This is a really bad sample project, sorry, but it's the simplest
 * Master System project I have on my HDD at this moment in time.
 * It dumps the values of all registers at boot time to the display.
 * I wrote it to help debug my emulator by presetting the registers to
 * vaguely sensible values in the absense of a BIOS.
 */

; Boot:

	di
	im 1
	jp Main

; Pause button handler:
.org $0066
	retn

; Main entry point:
Main

	; Preserve SP
	ld ($C000+20),sp
	
	; Dump registers to RAM by pushing:	
	ld sp,$C000+20
	push iy
	push ix
	
	exx
	push hl
	push de
	push bc
	exx
	
	ex af,af'
	push af
	ex af,af'

	push hl
	push de
	push bc
	push af
	
	; Put SP back to somewhere friendly.
	ld sp,$DFF0
	
	; Clear everything:
	
	call Video.Reset
	
	; Detect mode
	ld b,0
	ld a,$55
	out (2),a
	in a,(2)
	cp $55
	jr nz,+
	inc b
+	ld a,$44
	out (2),a
	in a,(2)
	cp $44
	jr nz,+
	inc b
+

	ld c,1
	ld a,b
	or a
	jr z,+
	inc c
+
	ld a,c
	call Video.GotoPalette
	ld a,$FF
	out ($BE),a
	out ($BE),a
	out ($BE),a
	
	ld hl,$0000+8*4*" "
	call Video.GotoHL
	ld hl,Resources.Font
	ld c,3
--	ld b,0
-	ld a,(hl)
	inc hl
	out ($BE),a
	xor a
	out ($BE),a
	out ($BE),a
	out ($BE),a
	djnz -
	dec c
	jr nz,--
	
	ld hl,$3800+64*6+12+10
	call Video.GotoHL
	
	ld hl,Resources.RegisterNames
	ld ix,$C000
	ld c,11
PrintValues
	ld b,3
-	ld a,(hl)
	inc hl
	out ($BE),a
	xor a
	out ($BE),a
	djnz -

	
	ld b,2
-	ld a,"."
	out ($BE),a
	xor a
	out ($BE),a
	djnz -
	
	ld a,"$"
	out ($BE),a
	xor a
	out ($BE),a

	ld a,(ix+1)
	call PrintHex
	ld a,(ix+0)
	call PrintHex
	inc ix
	inc ix
	
	xor a
	ld b,44
-	out ($BE),a
	djnz -
	
	dec c
	jr nz,PrintValues

	
	call Video.ScreenOn
	
-	jp -

PrintHex
	push af
	push af
	srl a
	srl a
	srl a
	srl a
	call GetHexChar
	out ($BE),a
	xor a
	out ($BE),a
	pop af
	and $0F
	call GetHexChar
	out ($BE),a
	xor a
	out ($BE),a
	pop af
	ret
GetHexChar
	cp 10
	jr nc,+
	add a,"0"
	ret
+	add a,"A"-10
	ret

	
.include "Video.asm"
.include "Resources.inc"