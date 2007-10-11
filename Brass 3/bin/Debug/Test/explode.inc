; ==================================================================================================
; EXPLODE.INC - Handles setting up the odd explosion :)
; ==================================================================================================

Explosion_Buffer: ;(x,y,[enabled|Sprite num])
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
	.db 0,0,0
EO_EB:
	.db 128,128,128	;128 signifies end of table

; ==================================================================================================
; ADD EXPLOSION - Adds an explosion to the buffer (if there is room...) @ (d,e)
; ==================================================================================================

Add_Explosion:
	ld ix,Explosion_Buffer
	ld b,Max_Explosions-1

SlotSearch:
	ld a,(ix+2)
	or a
	jr z,Found_Slot

	dec b
	ld a,b
	or a
	jr z,Not_Found_Slot

	inc ix
	inc ix
	inc ix
	jr SlotSearch

Found_Slot:
	ld (ix+0),d
	ld (ix+1),e
	ld (ix+2),Max_Frames_Explosion

Not_Found_Slot:

	ret	