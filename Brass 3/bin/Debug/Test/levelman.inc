; ==================================================================================================
; LEVELMAN.INC - manages the level (decompression to RAM) 
; ==================================================================================================

; ==================================================================================================
; LOAD LEVEL. This loads a level into the page file. (in=hl, start of level in RAM).
; ==================================================================================================

Load_Level:
	
	dec de
	ld a,(hl)
	ld c,a
	inc hl
	ld a,(hl)
	ld b,a
	inc hl
	push bc

	push hl
	pop ix

	call decompressRLE


Decompressed_Level:		;Now, add on "evil face":
	pop de	;(Level size)
	ld hl,(LevelLoc)
	add hl,de
	
	ld de,120
	add hl,de	
	ld (Level_LpPoint),hl

	ld de,120	;120 bytes for start platform
	add hl,de




	ld bc,(12*1)+1
	xor a
	bcall(_memset)

	push hl

	;Check to see... is it the EVIL FACE or the START PLATFORM?

	xor a
	ld (Use_Face),a


	ex de,hl

	ld a,(Level_Num)
	bit 0,a
	jr z,Use_End_Face

	
	inc de
	ld hl,Start_Base
	ld bc,11*12

	jr Fix_End_Level

Use_End_Face:
	ld a,1
	ld (Use_Face),a

	ld hl,End_Face
	ld bc,12*14

Fix_End_Level:
	ldir


	;Add on some empty space:
	xor a
	ex de,hl
	ld bc,12*3
	bcall(_memset)


	ld a,(Use_Face)
	or a
	jr nz,USing_Face_Not_Far

	;We're using the end platform-- we therefore want to shift the level endpoint further out into DEEP SPACE.

	ld de,12*5
	add hl,de		;That should be enough.
	
	


USing_Face_Not_Far:
	inc hl
	ld (Level_End),hl

	ld hl,(Level_LpPoint)
	ld de,12*3
	add hl,de
	ld (Level_LpPoint),hl

	pop hl
	
	ld de,(8*12)+4
	add hl,de

	ld (Eye_1),hl

	ld de,3
	add hl,de
	ld (Eye_2),hl

	ret
	

; ==================================================================================================
; FIND LOCATION. For finding the address of a byte in RAM which corresponds to (x,y) [b,c]
; ==================================================================================================

FindLocation:
	;Divide b by 8.
	
	sra b
	sra b
	inc b
	sra b


	;Divide c (y) by 8 then multiply by 12. (Saves the "x8" version, divides by 4, adds them together.).


	ld a,(Scroll_Amt)
	add a,c

	sra a
	sra a
	dec a
	sra a

	;Invert (so 8=top of screen)
	
	ld c,a
	ld a,8
	sub c
	

	;=divided by 8. Now multiply by 12:

	sla a
	sla a
	ld c,a
	sla a
	add a,c

	add a,b

	ld l,a
	ld h,0
	ld de,(Level_Pointer)

	add hl,de

	ld de,-96-(12*2)
	add hl,de

	ret

decompressRLE:

decompressL:
	ld	a,b
	or	c
	ret	z
	ld	a,(ix)
	inc	ix
decompress_patch:
	cp	$91
	jr	z,decompress_gotarun
	ld	(de),a
	inc	de
	dec	bc
	jr	decompressL
decompress_gotarun:
	ld	l,(ix)		;l = run length
	inc	ix
	ld	a,(ix)		;a = run character
	inc	ix
decompress_runL:
	ld	(de),a		;write tile number
	inc	de
	dec	bc
	dec	l
	jr	nz,decompress_runL
	jr	decompressL


	
Level_LpPoint:	.dw 0		;Where to loop to
Level_End:	.dw 0		;Where does the level end? Once hit, loop "evil face".