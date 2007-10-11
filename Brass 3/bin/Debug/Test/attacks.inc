; ==================================================================================================
; ATTACKS.INC - Holds details on different attack patterns.
; ==================================================================================================

;Format: Enemy sprite, type of attack, extra details:


;Paths:
;Sin_Wave Path 	#1
;Loop 		#2
;In_Cross_Out	#3
;In_From_Left	#4
;In_Wiggle_Out	#5

;Type of attack:
;#1- Predetermined path ("extra details" = path number)
;#2- Scroll enemies down (no left-right action at all!)
;#3- Save as #2, but enemies "home in".

Attack_List:	.db 00,01,01 ;01
		.db 02,01,04 ;02
		.db 05,01,03 ;03
		.db 01,01,02 ;04
		.db 06,01,05 ;05
		.db 04,02,01 ;06
		.db 00,02,01 ;07
		.db 03,03,01 ;08
End_Attack_List:

;Sprites:
;#0- Standard type.
;#1- Ball type.
;#2- Spiral type.
;#3- Small speed attackers.
;#4- Blocker ship (dumbells)
;#5- Shield-shaped ship
;#6- Side-on ship.


Points_Table:	;How many points are awarded for the different ships?
.db 2
.db 2
.db 5
.db 10
.db 0
.db 5
.db 2

; ==================================================================================================
; Start Attack: this will initialise one of the above attack patterns. In= a (Attack number).
; ==================================================================================================


Start_Attack:

	push af


	ld a,(Delay_Enemies)
	inc a
	ld (Delay_Between_Enemies),a

	ld hl,Enemies
	ld bc,4*(Num_Enemies+1)
	ld a,128
	bcall(_memset)

	ld hl,Enemies
	ld bc,4*(Num_Enemies)
	xor a
	bcall(_memset)

	ld hl,Enemies+(4*Num_Enemies)
	ld bc,4
	ld a,128
	bcall(_memset)

	pop af

	ld l,a
	ld h,0
	ld e,a
	ld d,0
	add hl,hl
	add hl,de

	ld de,Attack_List-3
	add hl,de
	push hl
	pop ix

;Copy in the correct sprite:

	ld a,(ix+0)
	;a = sprite #.

	push af

	cp 4
	jr nz,Not_Blocker

	ld a,1
	ld (Blocker),a
	jr Blocker_Set
Not_Blocker:
	xor a
	ld (Blocker),a
Blocker_Set:
	pop af

	ld l,a
	ld h,0
	push hl
	add hl,hl
	add hl,hl
	add hl,hl
	add hl,hl
	ld de,Enemy_Sprites
	add hl,de
	ld de,Enemy_Cache
	ld bc,16
	ldir

	pop hl
	ld de,Points_Table
	add hl,de
	ld a,(hl)
	ld (E_PointsAvailable),a
	

	ld a,(ix+1)
	cp 1
	jr z,Path_Based_Attack
	cp 2
	jr z,Scroll_Based_Attack
	cp 3
	jr z,Scroll_Based_Attack
	

Path_Based_Attack:
	ld a,(ix+2)
	call Enable_Path
	ld a,1
	ld (Attack_Type),a
	ret


Scroll_Based_Attack:
	ld (Attack_Type),a

	;Put all the ships in random places:

	ld ix,Enemies
Randomize_Ships_Positions:

	ld b,96-8
	call ionRandom
	ld (ix+0),a

	ld b,96
	call ionRandom

	sub 96+8

	ld (ix+1),a

	ld (ix+2),2	;Not disabled, but not yet enabled.
	ld (ix+3),1
	
	inc ix
	inc ix
	inc ix
	inc ix
	inc ix
	inc ix
	inc ix
	inc ix

	ld a,(ix+2)
	cp 128
	jr nz,Randomize_Ships_Positions
	
	;Now all the ships are in random positions!


	ret