; ==================================================================================================
; PAGEFILE.INC - manages the page file. Not a pleasant task, but it does it!
; ==================================================================================================

#define PageFileSize (864*4)+Max_Level_Size

; ==================================================================================================
; PAGE FILE CREATE. This creates a page file in RAM for use with the above buffer.
; ==================================================================================================

Create_Page_File:
	bcall(_MemChk)		; Free RAM => hl.
	ld de,PageFileSize+8 	; Size of page file => de (+8 for name -- caused some crashes!)
	bcall(_cphlde)		; Compare
	jp c,Low_RAM		; Not enough RAM? Shame! RET.


;Create 'page file':
	bcall(_zeroop1)		; Set Op1 to zero
	ld hl,PageFileName	; the name of the prog
	ld de,op1		; copy it to op1
	ld bc,8			; 7 chars in name

	ldir			; do the copying
	bcall(_chkfindsym)	; look it up
	jr c,NoNeedDelete
	bcall(_delvar)		; if it is there delete it (to keep things clean)
NoNeedDelete:
	ld hl,PageFileSize 	; length of program (in bytes)
	bcall(_createprog)	; create it
	inc de			; skip 1st length byte
	inc de			; skip 2nd length byte
	ld (PageFileLoc),de	; DE = Start Location of Page file- preserve it to (LevelLoc)
	ld (SetBufPos+1),de	; Hard-code into sprite routine
	ld (SetBufPosY+1),de	; Hard-code into sprite routine
	push de

	ex de,hl
	ld de,864
	add hl,de
	ld (SetMaskPos_2+1),hl
	ld (SetMaskPos_3+1),hl
	ld (SetMaskPos_4+1),hl
	ld (SetMaskPos_5+1),hl
	add hl,de
	ld (SetScrollPos+1),hl
	ld (SetScrollPos_2+1),hl
	ld (SetScrollPos_3+1),hl
	ld (SetScrollPos_4+1),hl
	ld (SetScrollPos_5+1),hl
	add hl,de
	ld (SetStarPos+1),hl
	ld (SetStarPos_5+1),hl
	pop de
	ld hl,864*4
	add hl,de
	ld (LevelLoc),hl
	ret
Low_RAM:
	bcall(_clrlcdfull)
	bcall(_grbufclr)
	
	ld a,44
	ld l,10
	ld b,15
	ld ix,Exclamation
	call ionPutSprite
	call ionFastCopy

	bcall(_homeup)
	ld a,5
	ld (currow),a
	ld hl,Low_RAM_Error
	bcall(_puts)
	call Wait_2nd
	pop hl			;Fix stack
	ret			;Quit

Low_RAM_Error:
	.db "ERROR! - Low RAM"
	.db "  2ND to Quit.",0  

; ==================================================================================================
; PAGE FILE DELETE. This deletes the page file from RAM.
; ==================================================================================================

Delete_Page_File:

	bcall(_zeroop1)		; Set Op1 to zero
	ld hl,PageFileName	; the name of the prog
	ld de,op1		; copy it to op1
	ld bc,8			; 7 chars in name

	ldir			; do the copying
	bcall(_chkfindsym)	; look it up
	ret c
	bcall(_delvar)		; if it is there delete it (to keep things clean)	
	ret

; ==================================================================================================
; Empty Page File - This clears the level part of the page file.
; ==================================================================================================
EmptyPageFile:
	ld hl,(PageFileLoc)
	ld bc,PageFileSize
	xor a
	bcall(_memset)
	ret


PageFileLoc:	.dw 0
LevelLoc:	.dw 0
PageFileName:	.db 5,"ft2page",0
