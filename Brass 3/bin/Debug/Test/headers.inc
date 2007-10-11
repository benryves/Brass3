; ==========================================
; TI-83 Plus Ion
; ==========================================

#ifdef TI83PI
#define	bcall(label) rst 28h \ .dw label
#define bjump(label) call 50h \ .dw label
#define errhandon(label) ld hl,label \ call 59h
#define errhandoff() call 5Ch
.nolist
#define EQU .equ
#include "ti83plus.inc"
.list
.org $9D93
.db $BB,$6D
	ret
	jr nc,init_all
#include "ion8X.inc"
#endif

; ==========================================
; TI-83 Plus MirageOS
; ==========================================

#ifdef TI83PM
#define	bcall(label) rst 28h \ .dw label
#define bjump(label) call 50h \ .dw label
#define errhandon(label) ld hl,label \ call 59h
#define errhandoff() call 5Ch
.nolist
#define EQU .equ
#include "ti83plus.inc"
.list
.org $9D93
.db $BB,$6D
	ret
.db 1
#include "icon.inc"
#include "mirage.inc"
#endif

; ==========================================
; General TI-83 Plus
; ==========================================

#ifdef TI83P
#define	bcall(label) rst 28h \ .dw label
#define bjump(label) call 50h \ .dw label
#define errhandon(label) ld hl,label \ call 59h
#define errhandoff() call 5Ch
.nolist
#define EQU .equ
#include "ti83plus.inc"
.list
.org $9D93
.db $BB,$6D
#endif

; ==========================================
; TI-83 Ion
; ==========================================

#ifdef TI83I
#define	bcall(label) call label
#define bjump(label) jp label
.org $9327
.nolist
#define EQU .equ
#include "ti83asm.inc"
#include "tokens.inc"
.list
	ret
	jr nc,init_all
#include "ion83.inc"
#endif