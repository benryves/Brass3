; ==================================================================================================
; SCORE.INC - Score drawing goodness and "font"
; ==================================================================================================

Numbers:
.db %11100000
.db %10100000
.db %10100000
.db %10100000
.db %11100000

.db %11000000
.db %01000000
.db %01000000
.db %01000000
.db %11100000

.db %11100000
.db %00100000
.db %11100000
.db %10000000
.db %11100000

.db %11100000
.db %00100000
.db %01100000
.db %00100000
.db %11100000

.db %10100000
.db %10100000
.db %11100000
.db %00100000
.db %00100000

.db %11100000
.db %10000000
.db %11100000
.db %00100000
.db %11100000

.db %11100000
.db %10000000
.db %11100000
.db %10100000
.db %11100000

.db %11100000
.db %00100000
.db %00100000
.db %00100000
.db %00100000

.db %11100000
.db %10100000
.db %11100000
.db %10100000
.db %11100000

.db %11100000
.db %10100000
.db %11100000
.db %00100000
.db %11100000

; ==================================================================================================
; Draw Score - Does what it says (draws the score onto the buffer)
; ==================================================================================================

Draw_Score:
	;First, clear a white box:

	ld a,7

	ld hl,PlotsScreen
	ld de,12-2
White_Box_Loop:
	push af
	ld a,255

	ld bc,4
	ld ix,Score_Mask
Score_Mask_Loop:
	ld a,(ix+0)
	ld d,a
	ld a,(hl)
	or d
	ld (hl),a
	inc ix
	inc hl
	dec bc
	ld a,b
	or c
	jr nz,Score_Mask_Loop

	
	ld de,12-4
	add hl,de
	pop af
	dec a
	or a
	jr nz,White_Box_Loop

	;Now comes the time to draw the numerals in:

	ld a,1
	ld (Score_Pos),a

	ld ix,Score

	ld a,6		;Digits in score.
Score_Draw_Loop:
	push af
	push ix
	
	ld a,(ix+0)
	ld b,a
	add a,a
	add a,a
	add a,b
	ld l,a
	ld h,0
	ld de,Numbers
	add hl,de
	push hl
	pop ix

	ld a,(Score_Pos)
	ld l,1
	ld b,5
	call ionPutSprite

	ld a,(Score_Pos)
	add a,4
	ld (Score_Pos),a

	pop ix
	pop af
	inc ix
	
	dec a
	or a
	jr nz,Score_Draw_Loop
	
	

	ret
	

Score_Mask:
	.db $FF,$FF,$FF,$80

; ==================================================================================================
; Add To Score - in=a : Adds the value of A onto the score (not surprising, really!)
; ==================================================================================================

Add_To_Score:

	push af

	ld hl,Score
	ld de,SavesScreen
	ld bc,6
	ldir

	pop af

	push bc
	push ix

	ld ix,Score+5	;Start with units.
Increment_Column:
	ld b,a
	ld a,(ix+0)
	add a,b

	cp 10
	jr nc,Carry_Over_10
	ld (ix+0),a
	pop ix
	pop bc
	jr Added_Score
	
Carry_Over_10:
	;OK, so we have looped over 9 to get a 10 (or above).
	
	;Job #1: What should this new number be?

	sub 10
	ld (ix+0),a

	;Job #2: Now we have that, we need to add 1 to the previous column :(

	dec ix
	ld a,1
	jr Increment_Column

Added_Score:

	ld a,(Game_Type)
	or a
	ret z


	ld a,(Score+3)
	ld b,a
	ld a,(SavesScreen+3)
	cp b
	ret z	;The two are the same!

	
	;Spawn a powerup.
	ld a,-8
	ld (Powerup_Y),a

	ld b,96-8
	call ionRandom
	ld (Powerup_X),a
	ret


		