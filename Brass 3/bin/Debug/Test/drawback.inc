; ==================================================================================================
; DRAWBACK.INC - handles drawing the tiled background, scrolling it &c.
; ==================================================================================================



; ==================================================================================================
; COPY BG. Copies the BG (aligned &c) and scrolls by "a" to graphmem.
; ==================================================================================================

Copy_Bg:
	;First to check: has the background actually changed?
	ld b,a

	ld a,(Last_Scroll_Pos)
	cp b
	jr nz,New_Back_Required

	;As from here, the background hasn't changed so reload it from a buffer. <- makes the game MUCH faster :)

	ld de,PlotsScreen
SetScrollPos:
	ld hl,0000
	ld bc,768
	ldir
	ret

	;OK, so we need a new

New_Back_Required:
	ld a,b
	ld (Last_Scroll_Pos),a

	;Copy the stars to the backbuffer:

SetStarPos:
	ld hl,0000
SetScrollPos_2:
	ld de,0000
	ld bc,768
	ldir

	ld l,a
	ld h,0
	
	add hl,hl	;*2
	add hl,hl	;*4
	push hl
	pop de		;de = original a*4
	add hl,hl	;*8
	add hl,de	;hl=a*12 :)
	push hl

SetMaskPos_2:
	ld de,0000
	add hl,de
	push hl
	pop ix	

	pop hl


	ld de,(PageFileLoc)
	add hl,de

SetScrollPos_3:	
	ld de,0000
	ld bc,768

	
Loop_Parallax:
	ld a,(ix+0)
	or a
	jr z,Skip_Drawing_Back_Tile

	ld a,(hl)
	ld (de),a

Skip_Drawing_Back_Tile:
	inc hl
	inc de
	inc ix
	dec bc
	ld a,b
	or c
	jr nz,Loop_Parallax

	ld a,(GS_Terrain)
	or a
	jr z,SetScrollPos_4

SetScrollPos_5:
	ld hl,0000
	ld bc,768

Invert_Copy_Loop:
	ld a,(hl)
	cpl
	ld (hl),a
	inc hl
	dec bc

	ld a,c
	or b
	jr nz,Invert_Copy_Loop

SetScrollPos_4:
	ld hl,0000
	ld de,PlotsScreen
	ld bc,768
	
	ldir

	ret



; ==================================================================================================
; SHIFT 8. Shifts the entire buffer down 8 pixels
; ==================================================================================================

Shift_8:


Back_copy_1:
	ld hl,0000
Back_copy_2:
	ld de,0000
	ld bc,768
	lddr
	
SetMaskPos_3:
	ld hl,0000
SetMaskPos_4:
	ld de,0000
	ld bc,768
	lddr


SetStarPos_2:
	ld hl,0000
SetStarPos_3:
	ld de,0000
	ld bc,768-12
	lddr

SetStarPos_4:
	ld hl,0000
SetStarPos_5:
	ld de,0000
	ld bc,12
	ldir

	ret
	

; ==================================================================================================
; DRAW ROW. Draws a row of background tiles.
; ==================================================================================================

Draw_Row:
	;1st, clear top row of pixels
	ld a,$FF
	ld bc,12*8
	ld hl,(PageFileLoc)
	bcall(_memset)

	xor a
	ld (Tile_X),a
	ld hl,(Level_Pointer)
	
DrawNextTile:	
	ld a,(hl)
	or a
	jr z,SkipTile

	dec a

	ld l,a
	ld h,0

	add hl,hl
	add hl,hl
	add hl,hl

	ld de,Background_Tiles
	add hl,de
	ex de,hl
	ld a,(Tile_X)

	call putSprite_LargeBuffer

	ld a,$FF	;Mask = $FF.
SkipTile:

	ld b,a

	ld a,(Tile_X)
	ld l,a
	ld h,0
SetMaskPos_5:
	ld de,0000

	add hl,de
	ld de,12

	ld a,b

	ld b,8
putASLoop_3:
        ld      (hl),a
        add     hl,de
        djnz    putASLoop_3
	

	ld hl,(Level_Pointer)
	inc hl
	ld (Level_Pointer),hl

	ld a,(Tile_X)
	inc a
	cp 12
	ret z
	ld (Tile_X),a

	jr DrawNextTile

	
	

; ==================================================================================================
; PUT SPRITE ONTO LARGE BUFFER. DON'T CALL before page file has been created.
; ==================================================================================================

putSprite_LargeBuffer:

	ld h,0
        ld l,a
SetBufPos:
	ld bc,PageFileLoc
        add hl,bc                           ; HL => Where to put sprite

        ld      b,8
putASLoop_2:
        ld      a,(de)
        ld      (hl),a
        inc     de
        push    de

        ld      de,12

        add     hl,de
        pop     de
        djnz    putASLoop_2
        ret





putSprite_LargeBuffer_Y:
        ld      h,0
        add     hl,hl                           ; x2
        add     hl,hl                           ; x4
        add     hl,hl                           ; x8
        add     hl,hl                           ; x16
        add     hl,hl                           ; x32
        ld      b,h
        ld      c,l
        add     hl,hl                           ; x64
        add     hl,bc                           ; x96
        ld      c,a
        ld      b,0
        add     hl,bc
SetBufPosY:
	ld bc,PageFileLoc
        add hl,bc                           ; HL => Where to put sprite

        ld      b,8
putASLoop_2Y:
        ld      a,(ix+0)
        ld      (hl),a
        inc     ix
        push    ix

        ld      de,12

        add     hl,de
        pop     ix
        djnz    putASLoop_2Y
        ret



Level_Pointer:
	.dw 0
Tile_X:
	.db 0

Scroll_Amt:
	.db 0

Scroll_Delay:
	.db 0


