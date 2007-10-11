; ==================================================================================================
; INTRO.INC - The funky flaming intro effect!
; ==================================================================================================


#define Bottom_Of_Screen	64
#define Top_Of_Screen	   0
#define FlameHeight 16

Intro:

	;DITHER = 24x16
FireLoop:


; (1) Shift all the bytes in the buffer up one.

	ld hl,DitherBuffer+24
	ld de,DitherBuffer
	ld bc,(FlameHeight-1)*24
	ldir

; (2) Add a line of fire at the bottom edge of the screen.

	call GetPoints
	ld a,1
	call SetMemory

	call GetPoints
	ld a,2
	call SetMemory

	call GetPoints
	ld a,3
	call SetMemory



; (3) Soften previous lines.

	ld ix,DitherBuffer
	

	ld d,(FlameHeight-1)
NextSoftenLine:
	ld c,22
SoftenAlongLine:
	ld a,(ix+0)
	add a,a
	add a,a
	ld b,a

	ld a,(ix+2)
	add a,a
	add a,a
	add a,b
	ld b,a

	ld a,(ix+25)
	add a,a
	add a,a
	
	add a,b
	
	ld b,0

	cp 15
	jr c,Make_0
	cp 23
	jr c,Make_1
	cp 32
	jr c,Make_2

Make_3:
	inc b
Make_2:
	inc b
Make_1:
	inc b
Make_0:
	
	ld a,b
	ld (ix+1),a
	inc ix
	dec c
	ld a,c
	or a
	jr nz,SoftenAlongLine
	
	inc ix
	inc ix
	
	dec d
	ld a,d
	or a
	jr nz,NextSoftenLine
	


; (4) Copy to screen, pool keypresses &c.

	call RenderDither


	ld a,(_2nd_Flash)
	or a
	jr nz,Dont_Flash

	ld hl,Press_2nd
	ld de,PlotsScreen+(55*12)
	ld bc,9*12
	call RLE
Dont_Flash:

	ld a,(Flash_Delay)
	dec a
	ld (Flash_Delay),a
	or a
	jr nz,Dont_Toggle_Flash

	ld a,Flash_Delay_Time
	ld (Flash_Delay),a

	ld a,(_2nd_Flash)
	xor 1
	ld (_2nd_Flash),a   


Dont_Toggle_Flash:


	call ionFastCopy
	
	bcall(_getcsc)
	or a
	jp z,FireLoop
	ret

RenderDither:   ;Renders the dither to the plotsscreen.
	
	bcall(_grbufclr)

	ld hl,Fire_Track_2
	ld de,PlotsScreen
	ld bc,12*25
	call RLE

	xor a
	ld (X_Pos),a
	ld a,Top_Of_Screen
	ld (Y_Pos),a

	ld ix,DitherBuffer

	ld (Pointer),ix

NextByte:
	ld ix,(Pointer)
	ld a,(ix+0)
	inc ix
	ld (Pointer),ix

	add a,a
	add a,a


	ld l,a
	ld h,0
	ld de,Dithers
	add hl,de

	push hl
	pop ix

	ld a,(Y_Pos)
	ld l,a
	ld a,(X_Pos)
	
	ld b,4

	call ionputSprite
	
	ld a,(X_Pos)
	add a,4
	cp 96
	jr nz,SaveA

	ld a,(Y_Pos)
	add a,4
	cp Bottom_Of_Screen
	ret z

	ld (Y_Pos),a
	xor a

SaveA:
	ld (X_Pos),a
	jr NextByte


x_Pos:  .db 0
Y_Pos:  .db 0
Pointer:	.dw 0

GetPoints:
	ld b,12
	call ionRandom
	inc a
	ld e,a

	ld b,10
	call ionRandom
	add a,2
	ld c,a
	ret

SetMemory:
	ld hl,DitherBuffer+(24*(FlameHeight-1))
	ld d,0
	add hl,de
	ld b,0
	bcall(_memset)
	ret 
	


_2nd_Flash:
.db 0
Flash_Delay:
.db 1

Dithers:

	.db %11110000
	.db %11110000
	.db %11110000
	.db %11110000

	.db %01010000
	.db %11110000
	.db %01010000
	.db %11110000

	.db %10100000
	.db %00000000
	.db %10100000
	.db %00000000

	.db %00000000
	.db %00000000
	.db %00000000
	.db %00000000