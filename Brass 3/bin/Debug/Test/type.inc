; ==================================================================================================
; TYPE.INC - routines for typing in.
; ==================================================================================================

GetLetter:
	bcall(_getcsc)
	or a
	jr z,GetLetter

	;Look up keypress:
	ld bc,t_EOKeyVals-t_KeyVals
	ld hl,t_KeyVals
	cpir
	jr nz,NotLetter		;Not a letter key!

	ld de,(t_EOKeyVals-t_KeyVals)-1
	add hl,de
	ld a,(hl)
	ret

NotLetter:
	cp sk2nd
	ret z
	cp skLeft
	ret z
	cp skRight
	ret z
	xor a
	ret
	
t_KeyVals:
	.db skMath	;a
	.db skApps	;b
	.db skPrgm	;c
	.db skRecip	;d
	.db skSin	;e
	.db skCos	;f
	.db skTan	;g
	.db skPower	;h
	.db skSquare	;i
	.db skComma	;j
	.db skLParen	;k
	.db skRParen	;l
	.db skDiv	;m
	.db skLog	;n
	.db sk7		;o
	.db sk8		;p
	.db sk9		;q
	.db skMul	;r
	.db skLn	;s
	.db sk4		;t
	.db sk5		;u
	.db sk6		;v
	.db skSub	;w
	.db skStore	;x
	.db sk1		;y
	.db sk2		;z
	.db sk0		;_
t_EOKeyVals:

t_Ascii:
	.db "ABCDEFGHIJ"
	.db "KLMNOPQRST"
	.db "UVWXYZ "
