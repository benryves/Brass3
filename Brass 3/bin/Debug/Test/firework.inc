; ==================================================================================================
; FIREWORK.INC - funky fireworky effect for completed game!
; ==================================================================================================


#define Spread 5		;Maximum "spread" (x-axis) of particles.
#define Thrust 10		;Maximum initial Vertical motion
#define X_Variation 5		;Deviation from centre
#define Num_Particles 80	;How many particles?
#define Frame_Delay 7		;Used to slow fireworks down



Fireworks:
	ld hl,SavesScreen
	ld bc,768
	ld a,128
	bcall(_memset)

	ld hl,SavesScreen
	ld bc,Num_Particles*4
	ld a,100
	bcall(_memset)

	ld a,Frame_Delay
	ld (SavesScreen+767),a

Fire_Loop:

	ld a,(SavesScreen+767)
	dec a
	ld (SavesScreen+767),a
	or a
	jp nz,Not_Frame_Time

	ld hl,PlotsScreen
	ld bc,768
	ld a,$FF
	bcall(_memset)

	;Copy in the "completed game" text

	ld hl,i_Completed_Game
	ld de,PlotsScreen+(5*12)
	ld bc,21*12
	call RLE

	ld ix,SavesScreen
Move_Particle_Loop:
	ld a,(ix+0)
	cp 128
	jr z,Done_Moved
	ld b,(ix+2)
	add a,b
	ld (ix+0),a
	cp 95
	jr nc,Reposition_Particle
	ld a,(ix+1)
	ld b,(ix+3)
	add a,b
	ld (ix+1),a
	cp 63
	jr nc,Reposition_Particle
	inc (ix+3)

	jr Moved_Particle

Reposition_Particle:
	;Particle has gone off screen -- let's recalc!

	ld b,Spread 
	call ionRandom
	sub Spread/2
	ld (ix+2),a

	ld b,Thrust
	call ionRandom
	inc a
	neg
	ld (ix+3),a

	ld b,X_Variation
	call ionRandom
	sub X_Variation/2
	add a,48
	ld (ix+0),a
	ld (ix+1),62

Moved_Particle:
	push ix


	ld a,(ix+0)
	ld l,(ix+1)
	ld b,2

	ld ix,Firework_Particle

	call ionPutSprite

	pop ix

	inc ix
	inc ix
	inc ix
	inc ix
	jr Move_Particle_Loop
Done_Moved:

	ld a,Frame_Delay
	ld (SavesScreen+767),a

Not_Frame_Time:

	call ionFastCopy

	bcall(_getcsc)
	cp sk2nd
	ret z
	jp Fire_Loop

Firework_Particle:
.db %11000000
.db %11000000