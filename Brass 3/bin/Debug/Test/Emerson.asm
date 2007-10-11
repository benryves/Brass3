#DEFINE TI83PI

.module TI
; ==========================================
; General TI-83 Plus
; ==========================================

#ifdef TI83P
#define bcall(label) RST 28h \ .dw label
#define bjump(label) call 50h \ .dw label
#define errhandon(label) ld hl,label \ call 59h
#define errhandoff() call 5Ch
.org 9D93h
.db $BB,$6D
.NOLIST
#DEFINE EQU .EQU
#include "ti83plus.inc"
.LIST
#endif

; ==========================================
; TI-83 Plus Ion
; ==========================================

#ifdef TI83PI
#define bcall(label) RST 28h \ .dw label
#define bjump(label) call 50h \ .dw label
#define errhandon(label) ld hl,label \ call 59h
#define errhandoff() call 5Ch
.org 9D93h
.db $BB,$6D
.NOLIST
#DEFINE EQU .EQU
#include "ti83plus.inc"
.LIST
	ret
	jr nc,init_all
#include "ion8X.inc" 
#endif   

; ==========================================
; TI-83 Plus MirageOS
; ==========================================

#ifdef TI83PM
#define bcall(label) RST 28h \ .dw label
#define bjump(label) call 50h \ .dw label
#define errhandon(label) ld hl,label \ call 59h
#define errhandoff() call 5Ch
.org 9D93h
.db $BB,$6D
.NOLIST
#DEFINE EQU .EQU
#include "ti83plus.inc"
.LIST
	ret
.db 1
#include "icon.inc"
#include "mirage.inc"
#endif

; ==========================================
; TI-83 Ion
; ==========================================
  

#ifdef TI83I
#define bcall(label) call label
#define bjump(label) jp label
.org 9327h
.NOLIST
#DEFINE EQU .EQU
#include "ti83asm.inc"
#include "tokens.inc"
.LIST
	ret
	jr nc,init_all
#include "ion83.inc"
#endif 

.endmodule

#include "keyval.inc"

; ==========================================
; Description (ignored if not Ion/MirageOS)
; ==========================================
#ifndef TI83P
.db "Emerson",0
#endif

; ==========================================
; Program entry point
; ==========================================

init_all:
	; We need to scroll the screen automatically:
	set TI.appAutoScroll,(iy+TI.appFlags)  
	ld a,10
	; See if we can find a translation table:

	call keyi_load_table
	jr z,_loaded_table
	bcall(TI._clrLCDFull)
	bcall(TI._homeUp)
	ld hl,_no_table_string
	bcall(TI._putS)
_no_table_found:
	or a
	jr z,_no_table_found
	ret
_no_table_string:
	.db "Keyboard layout "
	.db "table not found.",0
_loaded_table:

	; Reset the keyboard
	ld a,at_cmd_reset
	call at_send_byte

	bcall(TI._homeUp)

	; Load custom event handlers:
	ld hl,key_down
	call keyb_set_keydown

main_loop:

	call keyb_update

	call keyi_get_status_icon
	bcall(TI._putMap)

	ld a,$FF
	out (1),a
	ld a,KeyRow_5
	out (1),a
	in a,(1)
	cp dkClear
	ret z
	cp dKEnter
	jr nz,main_loop
	jr main_loop



; Key handlers:

key_down:
	call keyi_translate
	ret

#include "GetString.asm"
#include "key_input.asm"

.end