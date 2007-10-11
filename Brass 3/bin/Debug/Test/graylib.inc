;==========================================================
; GRAYSCALE LIBRARY INCLUDE FILE
;==========================================================
; 11:20 10-11-2002
;
; by Duck (DP Kingma)
; works under ion 83-/83+
;
; ==DEVELOPER NOTES==
;
; gsPutSprite	=xor sprite
; gsLargeSprite	=large sprite
; gsEnable		=enable grayscale
; gsDisable		=disable grayscale
; gsClearbuffer		=clear buffers
; gsCopybuffer		=copies buffers to active buffers (so screen)
; gsGetk		=tried to make is same as romcall _getk
; gsSetFreq		=set frequency
; (grayscale routines)	=the grayscale interrupts, dont activate them yourself ;)
;
; look at duckgray.txt for information about development!


; interrupt frequency
; can be 2, 4 and 6

InterFrequency:	.db 2

gbuf1	= gbuf
gbuf2	= saferam1

#ifdef NODOUBLEBUFFER
gActivebuf1 = gbuf1
gActivebuf2 = gbuf2
#endif 

#ifdef XORSPRITE
;==========================================================
; gsPutSprite
;==========================================================
;=== gsPutSprite 1.0 by Durk Kingma
;thanks to Joe Wingbermuehle for his ionPutSprite routine
;
; puts XOR grayscale sprite
; b=size of sprite
; l=yc
; a=xc
; ix holds pointer
gsPutSprite:

	push af
	push hl
	push bc

	call	ionPutSprite

	pop bc
	pop hl
	pop af

	ld	e,l
	ld	h,$00
	ld	d,h
	add	hl,de
	add	hl,de
	add	hl,hl
	add	hl,hl
	ld	e,a
	and	$07
	ld	c,a
	srl	e
	srl	e
	srl	e
	add	hl,de
	ld	de,gbuf2
	add	hl,de
gsPutSpriteLoop1:
pgxs_sl1:	ld	d,(ix)
	ld	e,$00
	ld	a,c
	or	a
	jr	z,gsPutSpriteSkip1
gsPutSpriteLoop2:
	srl	d
	rr	e
	dec	a
	jr	nz,gsPutSpriteLoop2
gsPutSpriteSkip1:
	ld	a,(hl)
	xor	d
	ld	(hl),a
	inc	hl
	ld	a,(hl)
	xor	e
	ld	(hl),a
	ld	de,$0B
	add	hl,de
	inc	ix
	djnz	gsPutSpriteLoop1

	ret
#endif

#ifdef ALIGNEDSPRITE
;==========================================================
; gsAlignedSprite
;==========================================================
; puts 8x8 aligned sprite
; a=x l=y ix=sprite
gsAlignedSprite:
	ld h,0
	ld d,h
	ld e,l
	add hl,de
	add hl,de
	add hl,hl
	add hl,hl
	ld e,a
	add hl,de
	push hl
	ld de,gbuf1
	add hl,de
	;now we got right spot in hl
	ld de,12
	ld b,8
gsAlignedSprite_loopbuf1:
	ld a,(ix)
	ld (hl),a
	add hl,de
	inc ix
	djnz gsAlignedSprite_loopbuf1

	pop hl
	ld de,gbuf2
	add hl,de
	ld de,12
	ld b,8
gsAlignedSprite_loopbuf2:
	ld a,(ix)
	ld (hl),a
	add hl,de
	inc ix
	djnz gsAlignedSprite_loopbuf2
	ret
#endif

#IFDEF ALIGNEDMASKEDSPRITE
;==========================================================
; gsAlignedMaskedSprite
;==========================================================
; mask/draw 8x8 aligned sprite
; a=number + 64*(HORIZFLIP) + 128*(VERTFLIP)
; h=x l=y de=sprite table
; output: everything destroyed, but ix=ix+8
gsAlignedMaskedSprite:
	push hl
	push af
	and %00111111

	ld h,0 \ ld l,a
	add hl,hl
	add hl,hl
	add hl,hl
	add hl,hl
	add hl,de
	;database to temp
	ld de,gsTempSprite
	ld bc,16
	ldir

	pop af
	bit 6,a \ call nz,gsFlipHoriz
	bit 7,a \ call nz,gsFlipVert ;not working yet
	ld ix,gsTempSprite
	pop hl
	ld a,h

	ld h,0
	ld b,h
	ld c,l
	add hl,bc
	add hl,bc
	add hl,hl
	add hl,hl
	ld c,a
	add hl,bc
	ld b,h \ ld c,l

	ld hl,gbuf2
	add hl,bc	;de=layer2
	ex de,hl
	ld hl,gbuf1
	add hl,bc	;hl=layer1

	ld b,8
gsMaskedSprite_loop:
	push bc

	push ix

	ld a,(ix) ;get spr1
	ld bc,8
	add ix,bc
	ld c,a	  ;save spr1
	
	ld a,(ix) ;get spr2
	push af
	cpl	  ;flip spr2
	and c	  ;mask ready
	ld b,a	  ;save

	cpl	  ;flip mask
	and c	  ;mask spr1
	ld c,a	  ;save spr1

	ld a,b
	and (hl)  ;mask layer1
	or c	  ;draw sprite
	ld (hl),a ;save

	ld a,b
	cpl
	ld b,a

	pop af	  ;get spr2
	and b	  ;mask spr2
	ld c,a
	
	ld a,b	  ;flip mask
	cpl

	ex de,hl  ;ex layer1/2
	and (hl)  ;mask layer2
	or c	  ;draw sprite
	ld (hl),a ;save

	ld bc,12
	add hl,bc
	ex de,hl  ;ex back
	add hl,bc

	pop ix
	inc ix

	pop bc
	djnz gsMaskedSprite_loop
	ret

;=== horizontal sprite flip
gsFlipHoriz:
	push af
	push hl
	ld de,gsTempSprite+7
	ld hl,gsTempSprite2
	ld b,8
gsFlipHoriz_loop:
	ld a,(de)
	ld (hl),a
	dec de
	inc hl
	djnz gsFlipHoriz_loop
	ld bc,16
	ex de,hl
	add hl,bc
	ex de,hl
	ld b,8
gsFlipHoriz_loop2:
	ld a,(de)
	ld (hl),a
	dec de
	inc hl
	djnz gsFlipHoriz_loop2
	call gsTemp2To1
	pop hl
	pop af
	ret

;=== vertical sprite flip
gsFlipVert:
	push af
	push hl
	ld hl,gsTempSprite
	ld de,gsTempSprite2
	ld b,16
gsFlipVert_loop:
	ld a,(hl)
	ld c,0
	rlca
	rr c
	rlca
	rr c
	rlca
	rr c
	rlca
	rr c
	rlca
	rr c
	rlca
	rr c
	rlca
	rr c
	rlca
	rr c
	ld a,c
	ld (de),a
	inc hl
	inc de
	djnz gsFlipVert_loop
	call gsTemp2To1
	pop hl
	pop af
	ret

gsTemp2To1:
	ld hl,gsTempSprite2
	ld de,gsTempSprite
	ld bc,16
	ldir
	ret

gsTempSprite:
	.db 0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0
gsTempSprite2:
	.db 0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0


#ENDIF


#ifdef LARGESPRITE
;==========================================================
; gsLargeSprite
;==========================================================
;=== gsLargeSprite 1.0 by Durk Kingma
;thanks to Joe Wingbermuehle for his ionPutSprite routine
;
; puts large Grayscale sprite
; b=size of sprite
; l=yc
; a=xc
; ix holds pointer
;-----> Draw a picture
;Input:	ix->sprite
;	a=x
;	l=y
;	b=height	(in pixels)
;	c=width		(in bytes, e.g. 2 would be 16)
;Output: nothing
; All registers are destroyed except bc', de', hl'
gsLargeSprite:

	push af
	push hl
	push bc

	call ionLargeSprite

	pop bc
	pop hl
	pop af

	di
	ex	af,af'
	ld	a,c
	push	af
	ex	af,af'
	ld	e,l
	ld	h,$00
	ld	d,h
	add	hl,de ;x12
	add	hl,de ;
	add	hl,hl ;
	add	hl,hl ;
	ld	e,a
	and	$07
	ld	c,a
	srl	e
	srl	e
	srl	e
	add	hl,de
	ld	de,gbuf2
	add	hl,de
gsLargeSpriteLoop1:
	push	hl
gsLargeSpriteLoop2:
	ld	d,(ix)
	ld	e,$00
	ld	a,c
	or	a
	jr	z,gsLargeSpriteSkip1
gsLargeSpriteLoop3:
	srl	d
	rr	e
	dec	a
	jr	nz,gsLargeSpriteLoop3
gsLargeSpriteSkip1:
	ld	a,(hl)
	xor	d
	ld	(hl),a
	inc	hl
	ld	a,(hl)
	xor	e
	ld	(hl),a
	inc	ix
	ex	af,af'
	dec	a
	push	af
	ex	af,af'
	pop	af
	jr	nz,gsLargeSpriteLoop2
	pop	hl
	pop	af
	push	af
	ex	af,af'
	ld	de,$0C
	add	hl,de
	djnz	gsLargeSpriteLoop1
	pop	af

	
	ld a,(interfrequency) \ out ($04),a
	ld a,$08 \ out ($03),a
	ld a,$0A \ out ($03),a
	ei
	ret
#endif

;==========================================================
; gsClearbuffer
;==========================================================
gsClearbuffer:
	ld hl,gbuf1
	ld de,gbuf1+1
	ld bc,767
	ld (hl),0
	ldir
	ld hl,gbuf2
	ld de,gbuf2+1
	ld bc,767
	ld (hl),0
	ldir
	ret

;==========================================================
; gsCopyBuffer
;==========================================================
gsCopybuffer:
#ifndef NODOUBLEBUFFER
	ld hl,gbuf1
	ld de,gActivebuf1
	ld bc,768
	ldir
	ld hl,gbuf2
	ld de,gActivebuf2
	ld bc,768
	ldir
#endif
	ret

;==========================================================
; gsEnable
;==========================================================
gsEnable:
#IFDEF TI83
	ld hl,8600h
	ld de,8600h+1
	ld bc,256
	ld (hl),87h
	ldir

	ld hl,interrupt_start
	ld de,8787h
	ld bc,INTERRUPT_end-interrupt_start
	ldir
	ld a,86h
	ld i,a
#ENDIF
#IFDEF TI83P
	ld hl,8700h
	ld de,8700h+1
	ld bc,256
	ld (hl),86h
	ldir

	ld hl,interrupt_start
	ld de,8686h
	ld bc,INTERRUPT_end-interrupt_start
	ldir
	ld a,87h
	ld i,a
#ENDIF

	im 2

	ld a,(interfrequency) \ out ($04),a
	ld a,$08 \ out ($03),a
	ld a,$0A \ out ($03),a
	ei

	ret

;==========================================================
; gsDisable
;==========================================================
gsDisable:
	di
	im 1
	ld a,6 \ out ($04),a
	ei
	ret

;==========================================================
; interrupt
;==========================================================
interrupt_start:
#ifdef 3LEVELGRAY
	jp draw3levelGrayscale
#else
	jp draw4levelGrayscale
#endif
interrupt_end:

#ifdef 3LEVELGRAY
;==========================================================
; draw3levelGrayScale INTERRUPT
;==========================================================
draw3levelGrayscale:

	push af \ push bc \ push de \ push hl
	
grayFastCopy:
	ld	a,$80
	out	($10),a


	exx
	ld	hl,gActivebuf2-12-(-(12*64)+1)
	ld	a,(grayvar)
	ld	b,a
	cpl
	ld	c,a
	exx

	ld	hl,gActivebuf1-12-(-(12*64)+1)
	ld	a,$20
	ld	c,a

grayFastCopyAgain:


	ld	b,16

	;=== x-loop
	inc	c
	ld	de,-(12*64)+1
	out	($10),a
	add	hl,de
	ld	de,12

	exx	
	ld	de,-(12*64)+1
	add	hl,de
	ld	de,12
	exx

grayFastCopyLoop:
	;===y-loop
	exx
	add	hl,de
	ld	a,(hl)
	and	b
	exx
	add	hl,de
	or	(hl)
	out	($11),a

	inc bc ; 6 waste

	exx
	add	hl,de
	ld	a,(hl)
	and	c
	exx
	add	hl,de
	or	(hl)
	out	($11),a

	dec bc ; 6 waste

	exx
	add	hl,de
	ld	a,(hl)
	and	b
	exx
	add	hl,de
	or	(hl)
	out	($11),a

	ld a,3 ;waste

	exx
	add	hl,de
	ld	a,(hl)
	and	c
	exx
	add	hl,de
	or	(hl)
	out	($11),a

	djnz	grayFastCopyLoop


	ld	a,c
	cp	$2B+1
	jr	nz,grayFastCopyAgain

	ld a,(grayvar)
	cpl
	ld (grayvar),a
	
	ld a,(interfrequency) \ out ($04),a
	ld a,$08 \ out ($03),a
	ld a,$0A \ out ($03),a

	pop hl \ pop de \ pop bc \ pop af

	ei
	ret

grayvar:
	.db %10101010
#ENDIF	

;==========================================================
; draw4levelGrayScale INTERRUPT
;==========================================================
draw4levelGrayscale:

#ifdef BETTERCOLOR

	push af \ push bc \ push de \ push hl
	
Gray4afastCopy:
	ld	a,$80
	out	($10),a

	exx
	ld	hl,gActivebuf2-12-(-(12*64)+1)
	exx

	ld	hl,gActivebuf1-12-(-(12*64)+1)
	ld	a,$20
	ld	c,a

Gray4afastCopyAgain:

	ld	b,16

	;=== x-loop
	inc	c
	ld	de,-(12*64)+1
	out	($10),a
	add	hl,de
	ld	de,12

	exx	
	ld	de,-(12*64)+1
	add	hl,de
	ld	de,12
	exx

Gray4afastCopyLoop:
	;===y-loop
	push bc

;-------------------------
	exx
	add	hl,de
	ld	a,(hl)
Gray4afc_moda1:
	and	%01011010
	exx
	add	hl,de
	ld	b,a
	ld	a,(hl)
Gray4afc_moda2:
	and	%10100101
	or	b
	out	($11),a
;-------------------------
	exx
	add	hl,de
	ld	a,(hl)
Gray4afc_modb1:
	and	%01011010
	exx
	add	hl,de
	ld	b,a
	ld	a,(hl)
Gray4afc_modb2:
	and	%10100101
	or	b
	out	($11),a
;-------------------------
	exx
	add	hl,de
	ld	a,(hl)
Gray4afc_modc1:
	and	%10101101
	exx
	add	hl,de
	ld	b,a
	ld	a,(hl)
Gray4afc_modc2:
	and	%01010010
	or	b
	out	($11),a
;-------------------------
	exx
	add	hl,de
	ld	a,(hl)
Gray4afc_modd1:
	and	%11010110
	exx
	add	hl,de
	ld	b,a
	ld	a,(hl)
Gray4afc_modd2:
	and	%00101001
	or	b
	out	($11),a
;-------------------------

	pop bc
	djnz	Gray4afastCopyLoop
	;===end y-loop

	ld	a,c
	cp	$2B+1
	jp	nz,Gray4afastCopyAgain
	;===end x-loop

	ld a,(Gray4agrayint)
	inc a
	cp 4 \ jr nz,Gray4afc_grayintinc
	xor a
Gray4afc_grayintinc:
	ld (Gray4agrayint),a


	add a,a
	ld hl,Gray4afc_masktable
	ld b,0 \ ld c,a
	add hl,bc
	;first
	ld a,(hl)
	ld (Gray4afc_moda1+1),a
	inc hl
	ld a,(hl)
	ld (Gray4afc_moda2+1),a
	inc hl
	ld a,(hl)
	ld (Gray4afc_modb1+1),a
	inc hl
	ld a,(hl)
	ld (Gray4afc_modb2+1),a
	inc hl

	ld a,(hl)
	ld (Gray4afc_modc1+1),a
	inc hl
	ld a,(hl)
	ld (Gray4afc_modc2+1),a
	inc hl
	ld a,(hl)
	ld (Gray4afc_modd1+1),a
	inc hl
	ld a,(hl)
	ld (Gray4afc_modd2+1),a



	ld a,(interfrequency) \ out ($04),a
	ld a,$08 \ out ($03),a
	ld a,$0A \ out ($03),a

	pop hl \ pop de \ pop bc \ pop af

	ei
	ret

Gray4agrayint:
	.db 0
Gray4afc_masktable:
	.db %01000100, %10101010, %00100010, %01010101
	.db %00010001, %10101010, %10001000, %01010101

	.db %01000100, %10101010, %00100010, %01010101
	.db %00010001, %10101010, %10001000, %01010101


#endif


#ifndef VTI

	push af \ push bc \ push de \ push hl
	
Gray4fastCopy:
	ld	a,$80
	out	($10),a

	exx
	ld	hl,gActivebuf2-12-(-(12*64)+1)
	exx

	ld	hl,gActivebuf1-12-(-(12*64)+1)
	ld	a,$20
	ld	c,a

Gray4fastCopyAgain:

	ld	b,21

	;=== x-loop
	inc	c
	ld	de,-(12*64)+1
	out	($10),a
	add	hl,de
	ld	de,12

	exx	
	ld	de,-(12*64)+1
	add	hl,de
	ld	de,12
	exx

Gray4fastCopyLoop:
	;===y-loop
	push bc

;-------------------------
	exx
	add	hl,de
	ld	a,(hl)
Gray4fc_moda1:
	and	%10010010
	exx
	add	hl,de
	ld	b,a
	ld	a,(hl)
Gray4fc_moda2:
	and	%01101101
	or	b
	out	($11),a
;-------------------------
	exx
	add	hl,de
	ld	a,(hl)
Gray4fc_modb1:
	and	%01001001
	exx
	add	hl,de
	ld	b,a
	ld	a,(hl)
Gray4fc_modb2:
	and	%10110110
	or	b
	out	($11),a
;-------------------------
	exx
	add	hl,de
	ld	a,(hl)
Gray4fc_modc1:
	and	%00100100
	exx
	add	hl,de
	ld	b,a
	ld	a,(hl)
Gray4fc_modc2:
	and	%11011011
	or	b
	out	($11),a
;----------------

	pop bc
	djnz	Gray4fastCopyLoop
	;===end y-loop

	push bc
	exx
	add	hl,de
	ld	a,(hl)
Gray4fc_modd1:
	and	%10010010
	exx
	add	hl,de
	ld	b,a
	ld	a,(hl)
Gray4fc_modd2:
	and	%01101101
	or	b
	out	($11),a
	pop bc

	ld	a,c
	cp	$2B+1
	jr	nz,Gray4fastCopyAgain
	;===end x-loop

	ld a,(Gray4grayint)
	inc a
	cp 3 \ jr nz,Gray4fc_grayintinc
	xor a
Gray4fc_grayintinc:
	ld (Gray4grayint),a
	ld hl,Gray4fc_masktable
	ld b,0 \ ld c,a
	add hl,bc

	;first
	ld a,(hl)
	ld (Gray4fc_moda1+1),a
	ld (Gray4fc_modd1+1),a
	cpl
	ld (Gray4fc_moda2+1),a
	ld (Gray4fc_modd2+1),a
	inc hl

	;second
	ld a,(hl)
	ld (Gray4fc_modb1+1),a
	cpl
	ld (Gray4fc_modb2+1),a
	inc hl

	;third
	ld a,(hl)
	ld (Gray4fc_modc1+1),a
	cpl
	ld (Gray4fc_modc2+1),a
		
	ld a,(interfrequency) \ out ($04),a
	ld a,$08 \ out ($03),a
	ld a,$0A \ out ($03),a

	pop hl \ pop de \ pop bc \ pop af

	ei
	ret

Gray4grayint:
	.db 0
Gray4fc_masktable:
	.db %10010010, %01001001, %00100100, %10010010, %01001001

#else

;==========================================================
; SPECIAL VTI GRAYSCALE
;==========================================================

	push af \ push bc \ push de \ push hl

vtifastCopy:
	di
	ld	a,$80				; 7
	out	($10),a				; 11
	ld a,(vtigrayint)
	or a \ jr z,vtiBuf2
vtiBuf1:
	ld	hl,gActivebuf1-12-(-(12*64)+1)	
	jr vtia
vtiBuf2:
	ld	hl,gActivebuf2-12-(-(12*64)+1)	
vtia:

	ld	a,$20				; 7
	ld	c,a				; 4
vtifastCopyAgain:
	ld	b,64				; 7
	inc	c				; 4
	ld	de,-(12*64)+1			; 10
	out	($10),a				; 11
	add	hl,de				; 11
	ld	de,12				; 10
vtifastCopyLoop:
	add	hl,de				; 11
	inc hl
	dec hl					; 6
	ld	a,(hl)				; 7
	out	($11),a				; 11
						; 6
	djnz	vtifastCopyLoop			; 13/8
	ld	a,c				; 4
	cp	$2B+1				; 7
	jr	nz,vtifastCopyAgain		; 10/1

	ld a,(vtigrayint)
	inc a
	cp 3 \ jr nz,vtib
	xor a
vtib:	ld (vtigrayint),a

	ld a,(interfrequency) \ out ($04),a
	ld a,$08 \ out ($03),a
	ld a,$0A \ out ($03),a
	pop hl \ pop de \ pop bc \ pop af
	ei
	ret

vtigrayint:
	.db 0
#endif

;==========================================================
; gsGetK
;==========================================================
gsGetK:
	push bc
	push de
	push hl
gs_GetK2:
	ld b,7
gs_GetK_loop:

	ld a,7
	sub b
	ld hl,gs_keygroups
	ld d,0 \ ld e,a
	add hl,de
	ld a,(hl)
	ld c,a

	ld a,0ffh           ;resets the keypad.
	out (1),a           ;'Required syntax' :)
	ld a,c
	out (1),a
	in a,(1)            ;'Required Syntax'

	ld de,0
	cp 254 \ jr z,gs_GetK_254
	cp 253 \ jr z,gs_GetK_253
	cp 251 \ jr z,gs_GetK_251
	cp 247 \ jr z,gs_GetK_247
	cp 239 \ jr z,gs_GetK_239
	cp 223 \ jr z,gs_GetK_223
	cp 191 \ jr z,gs_GetK_191
	cp 127 \ jr z,gs_GetK_127

gs_GetK_loopend:

	djnz gs_GetK_loop

	xor a
	ld (gs_keymem),a
	jr gs_GetK_end

gs_GetK_127:
	inc e
gs_GetK_191:
	inc e
gs_GetK_223:
	inc e
gs_GetK_239:
	inc e
gs_GetK_247:
	inc e
gs_GetK_251:
	inc e
gs_GetK_253:
	inc e
gs_GetK_254:

	push de
	ld a,7
	sub b
	add a,a \ add a,a \ add a,a
	ld d,0 \ ld e,a
	ld hl,gs_keygroup1
	add hl,de
	pop de
	add hl,de
	ld a,(hl)

	;push hl
	;push de
	;push af
	;bcall(_puts)
	;ld	hl,2567
	;ld	(currow),hl
	;ld	h,0
	;ld	l,a
	;bcall(_disphl)
	;pop af
	;pop de
	;pop hl

	ld d,a
	ld a,(gs_keymem)
	cp d \ jr z,gs_getK_end
	ld a,d
	ld (gs_keymem),a


gs_GetK_end:

;	ld b,255
;gs_GetK_endloopc:
;	push bc
;	ld b,255
;gs_Getk_endloopa:
;	push bc
;	ld b,255
;gs_GetK_endloopb:
;	push hl
;	pop hl
;	djnz gs_GetK_endloopb
;	pop bc
;	djnz gs_GetK_endloopa
;	pop bc
;	djnz gs_GetK_endloopc

	pop hl
	pop de
	pop bc
	ret

gs_keygroups:
	.db $fe, $fd, $fb, $f7, $ef, $df, $bf
gs_keygroup1:
	.db 01h, 02h, 03h, 04h, 00h, 00h, 00h, 00h
gs_keygroup2:
	.db 09h, 0Ah, 0Bh, 0Ch, 0Dh, 00h, 0Fh, 00h
gs_keygroup3:
	.db 8Ch, 12h, 13h, 14h, 15h, 16h, 17h, 00h
gs_keygroup4:
	.db 8Dh, 1Ah, 1Bh, 1Ch, 1Dh, 1Eh, 1Fh, 20h
gs_keygroup5:
	.db 21h, 22h, 23h, 24h, 25h, 26h, 27h, 28h
gs_keygroup6:
	.db 00h, 2Ah, 2Bh, 2Ch, 2Dh, 2Eh, 2Fh, 30h
gs_keygroup7:
	.db 31h, 32h, 33h, 34h, 35h, 36h, 37h, 38h 
gs_keymem:
	.db 0

;==========================================================
; gsSetFreq
;==========================================================
; call this routine to set up the interrupt frequency.
gsSetFreq:

gsSetFreq_loop:
	;=== create upper line, 10 bytes
	ld hl,gBuf1+(47*12)
	ld de,gBuf2+(47*12)
	ld a,%11111111
	ld b,12
gsSetFreq_a:
	ld (hl),a
	inc hl
	ld (de),a
	inc de
	djnz gsSetFreq_a

	;=== clear the used area
	ld hl,gBuf1+(48*12)
	ld de,gBuf1+(48*12)+1
	ld (hl),0
	ld bc,(12*16)-1
	ldir
	;=== clear the used area
	ld hl,gBuf2+(48*12)
	ld de,gBuf2+(48*12)+1
	ld (hl),0
	ld bc,(12*16)-1
	ldir

	
	;=== set buffer text
	set textwrite,(iy+sgrflags)

	;=== draw the texts
	ld	hl,49*256 + 10
	ld	(pencol),hl
	ld	hl,gsSetFreq_text
	bcall(_vputs)
	ld	hl,57*256 + 18
	ld	(pencol),hl
	ld	hl,gsSetFreq_zerot
	bcall(_vputs)
	ld	hl,57*256 + 34
	ld	(pencol),hl
	ld	hl,gsSetFreq_twot
	bcall(_vputs)
	ld	hl,57*256 + 50
	ld	(pencol),hl
	ld	hl,gsSetFreq_fourt
	bcall(_vputs)
	ld	hl,57*256 + 66
	ld	(pencol),hl
	ld	hl,gsSetFreq_sixt
	bcall(_vputs)
	call gsEnable

	;=== draw the cursor
	ld hl,gBuf1+(57*12)
	ld b,0
	ld a,(interfrequency)
	add a,2
	ld c,a
	add hl,bc
	ld b,7
	ld a,255
	ld de,12
gsSetFreq_cursorloop:
	ld a,(hl)
	cpl
	ld (hl),a
	add hl,de
	djnz gsSetFreq_cursorloop
	call gsCopyBuffer


	;=== wait for key input
gsSetFreq_waitkey:
	call gsGetk
	or a \ jr z,gsSetFreq_waitkey
	cp gEnter \ ret z
	cp g0 \ jr z,gsSetFreq_zero
	cp g2 \ jr z,gsSetFreq_two
	cp g4 \ jr z,gsSetFreq_four
	cp g6 \ jr z,gsSetFreq_six
	jp gsSetFreq_loop

gsSetFreq_zero:
	xor a
	ld (interfrequency),a
	jp gsSetFreq_loop
gsSetFreq_two:
	ld a,2
	ld (interfrequency),a
	jp gsSetFreq_loop
gsSetFreq_four:
	ld a,4
	ld (interfrequency),a
	jp gsSetFreq_loop
gsSetFreq_six:
	ld a,6
	ld (interfrequency),a
	jp gsSetFreq_loop
	
gsSetFreq_text:	.db "set interrupt frequency:",0
gsSetFreq_zerot:	.db "0",0
gsSetFreq_twot:		.db "2",0
gsSetFreq_fourt:	.db "4",0
gsSetFreq_sixt:		.db "6",0

;==========================================================
; gsbuf1Tobuf2
;==========================================================
; copies buffer 1 to buffer 2
gsbuf1Tobuf2:
	ld hl,gbuf1
	ld de,gbuf2
	ld bc,768
	ldir
	ret

#ifndef NODOUBLEBUFFER
gActivebuf1:
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
gActivebuf2:
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
	.db 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
#endif

