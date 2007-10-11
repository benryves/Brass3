; ==================================================================================================
; HISCORE.INC - High-score handling.
; ==================================================================================================

;Three high score tables:
;#1 = Temporary, used when displaying/editing.
;#2 = Saved scores for classic mode.
;#3 = Saved scores for extended mode.

High_Score_1:	.db 0,0,0,0,0,0,"xxx"
High_Score_2:	.db 0,0,0,0,0,0,"xxx"
High_Score_3:	.db 0,0,0,0,0,0,"xxx"


Classic_Scores:
.db 0,0,0,0,0,0,"CLA"
.db 0,0,0,0,0,0,"CLA"
.db 0,0,0,0,0,0,"CLA"

Extended_Scores:
.db 0,0,0,0,0,0,"EXT"
.db 0,0,0,0,0,0,"EXT"
.db 0,0,0,0,0,0,"EXT"



; ==================================================================================================
; CompareScore: in = (hl) (score #1), (de) (score #2). out = a (0 hl=de, 1 hl>de, 2 hl<de)
; ==================================================================================================

CompareScore:
	ld c,6

Check_Score_Loop:
	ld a,(hl)
	ld b,a
	ld a,(de)
	cp b
	jr z,Undecided
	jr c,HL_Is_Bigger

DE_Is_Bigger:
	ld a,2
	ret
HL_Is_Bigger:
	ld a,1
	ret

Undecided:
	
	inc hl
	inc de
	
	dec c
	ld a,c
	or a
	jr nz,Check_Score_Loop
	
	ret


; ==================================================================================================
; DisplayScores - Displays the high score table.
; ==================================================================================================
DisplayScores:

	set textInverse, (IY + textFlags)
	set textWrite, (IY + sGrFlags)

	ld a,$FF
	ld hl,PlotsScreen
	ld bc,768
	bcall(_memset)

	ld hl,High_Scores
	ld de,PlotsScreen+(10*12)
	ld bc,11*12
	call RLE


	ld a,30
	ld (penRow),a


	ld a,3
	ld hl,High_Score_1
Draw_Scores_Loop:
	push af
	push hl
	ld a,49
	ld (penCol),a
	call Draw_Number
	pop hl
	ld de,9
	add hl,de

	ld a,(penRow)
	add a,8
	ld (penRow),a

	pop af
	dec a
	or a
	jr nz,Draw_Scores_Loop
	

	


	res textInverse, (IY + textFlags)
	res textWrite, (IY + sGrFlags)	

	call ionFastCopy

	ret


Draw_Number:

	ld a,6
Still_Drawing_Number:
	push af
	ld a,(hl)
	add a,NUMS	;NUMS = start of numbers (ascii)
	inc hl
	bcall(_vputmap)
	pop af
	dec a
	or a
	jr nz,Still_Drawing_Number

	ld a,(penCol)
	sub 50
	ld (penCol),a

	ld a,3
Still_Drawing_Names:
	push af
	ld a,(hl)
	inc hl
	bcall(_vputmap)
	pop af
	dec a
	or a
	jr nz,Still_Drawing_Names

	ret