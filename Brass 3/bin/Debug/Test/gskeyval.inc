; #########################################
; ## "All the Keys v1.0" by John Wyrwas  ##
; ##       20002016 - 20002019           ##
; #########################################
;
; Includes the Getkey keys values, Getcsc (getk), and Direct Input
; key values.  They start with Gk, G, and K respectively and most contain
; the writing on the key.
;
; To use, unzip in your tasm directory and use #include keyval.inc
;
;
; #################
; ## Getkey Keys ##
; #################
; (the TI-OS does NOT recognize values from lowercase letter input- all are 0FC)
;
;Numbers
Gk0		.equ $8E
Gk1		.equ $8F
Gk2		.equ $90
Gk3		.equ $91
Gk4		.equ $92
Gk5		.equ $93
Gk6		.equ $94
Gk7		.equ $95
Gk8		.equ $96
Gk9		.equ $97
; Uppercase Letters
GkA		.equ $9A
GkB		.equ $9B
GkC		.equ $9C
GkD		.equ $9D
GkE		.equ $9E
GkF		.equ $9F
GkG		.equ $A0
GkH		.equ $A1
GkI		.equ $A2
GkJ		.equ $A3
GkK		.equ $A4
GkL		.equ $A5
GkM		.equ $A6
GkN		.equ $A7
GkO		.equ $A8
GkP		.equ $A9
GkQ		.equ $AA
GkR		.equ $AB
GkS		.equ $AC
GkT		.equ $AD
GkU		.equ $AE
GkV		.equ $AF
GkW		.equ $B0
GkX		.equ $B1
GkY		.equ $B2
GkZ		.equ $B3
; Other Keys
GkMode		.equ $45
GkIns		.equ $0B
GkDel		.equ $0A
GkRight		.equ $01
GkLeft		.equ $02
GkUp		.equ $03
GkDown		.equ $04
GkXTN		.equ $B4
GkLink		.equ $41
GkStat		.equ $31
GkList		.equ $3A
GkMath		.equ $32
GkTest		.equ $33
GkApps		.equ $2C
GkAngle		.equ $39
GkPrgm		.equ $2D
GkDraw		.equ $2F
GkVars		.equ $35
GkDistr		.equ $38
GkClear		.equ $09
GkX1		.equ $B6
GkMatrix	.equ $37
GkSin		.equ $B7
GkSin1		.equ $B8
GkCos		.equ $B9
GkCos1		.equ $BA
GkTan		.equ $BB
GkTan1		.equ $BC
GkPower		.equ $84
GkPi		.equ $B5
GkX2		.equ $BD
GkRoot		.equ $BE
GkComma		.equ $8B
GkEE		.equ $98

GkRightPar	.equ 86h
GkLeftPar	.equ 85h

GkDivide	.equ 83h
GkMultiply	.equ 82h
GkSubtract	.equ 81h
GkAdd		.equ 80h
GkEnter		.equ 05h

GkLittleE	.equ $EF
GkLeftBracket	.equ 87h
GkRightBracket	.equ 88h
GkMem		.equ 36h
GkEntry		.equ 0Dh
GkQuotes	.equ $CB
GkSolve		.equ 05h


GkLog		.equ $C1
GkLn		.equ $BF
GkSto		.equ $8A
Gk10X		.equ $C2
GkeX		.equ $C0
GkRcl		.equ $0C

GkLittleU 	.equ $F9
GkLittleV	.equ $FA
GkLittleW	.equ $FB
GkLittleI	.equ $EE

GkL1		.equ $F3
GkL2		.equ $F4
GkL3		.equ $F5
GkL4		.equ $F6
GkL5		.equ $F7
GkL6		.equ $F8

GkCatalog	.equ 3Eh
GkAns		.equ $C5
GkSpace		.equ 99h
GkColon		.equ $C6
GkQuestion	.equ $CA
GkPeriod	.equ 8Dh
GkNegative	.equ 8Ch

GkF1		.equ 49h
GkF2 		.equ 48h
GkF3		.equ 2Eh
GkF4		.equ 5Ah
GkF5		.equ 44h

GkStatPlot	.equ 30h
GkTblset	.equ 4Bh
GkFormat	.equ 57h
GkCalc		.equ 3Bh
GkTable		.equ 4Ah


;############################
;##     Getcsc/Getk Keys   ##
;############################

GF1		.equ 35h
GF2		.equ 34h
GF3		.equ 33h
GF4		.equ 32h
GF5		.equ 31h

G2nd		.equ 36h
GMode		.equ 37h
GDel		.equ 38h

GDown		.equ 01h
GLeft		.equ 02h
GRight		.equ 03h
GUp		.equ 04h

GAlpha		.equ 30h
GXTN		.equ 28h
GStat		.equ 20h
GMath		.equ 2Fh
GApps		.equ 27h
GPrgm		.equ 1Fh
GVars		.equ 17h
GClear		.equ 0Fh
GX1		.equ 2Eh
GSin		.equ 26h
GCos		.equ 1Eh
GTan		.equ 16h
GPower		.equ 0Eh
GX2		.equ 2Dh
GComma 		.equ 25h
GLeftPara	.equ 1Dh
GRightPara	.equ 15h

GDivide		.equ 0Dh
GMultiply	.equ 0Ch
GSubtract	.equ 0Bh
GAdd		.equ 0Ah
GEnter		.equ 09h
GLog		.equ 2Ch
GLn		.equ 2Bh
GSto		.equ 2Ah

G0		.equ 21h
G1		.equ 22h
G2		.equ 1Ah
G3		.equ 12h
G4		.equ 23h
G5		.equ 1Bh
G6		.equ 13h
G7		.equ 24h
G8		.equ 1Ch
G9		.equ 14h

GPeriod 	.equ 8Dh
GNegative	.equ 8Ch

;#######################
;## Direct Input Keys ##
;#######################

;Group 1
KeyRow_Pad	.equ $fe
KDown		.equ 254
KLeft		.equ 253
KRight		.equ 251
KUp		.equ 247

;Group 2
KeyRow_5 	.equ $fd
KEnter		.equ 254
KPlus		.equ 253
KMinus		.equ 251
KMul		.equ 247
KDiv		.equ 239
KPower		.equ 223
KClear		.equ 191

;Group 3
KeyRow_4 	.equ $fb
KMinus2		.equ 254
KThree		.equ 253
KSix		.equ 251
KNine		.equ 247
KRbracket	.equ 239
KTan		.equ 223
KVars		.equ 191

;Group 4
KeyRow_3	.equ $f7
KPoint		.equ 254
KTwo		.equ 253
KFive		.equ 251
KEight		.equ 247
KLbracket	.equ 239
KCos		.equ 223
KPrgm		.equ 191
KStat		.equ 127

;Group 5
KeyRow_2 	.equ $ef
KZero		.equ 254
KOne		.equ 24453
KFour		.equ 251
KSeven		.equ 247
KComma		.equ 239
KSin		.equ 223
KMatrx		.equ 191
KX		.equ 127

;Group 6
KeyRow_1 	.equ $df
KSto		.equ 253
KLn		.equ 251
KLog		.equ 247
kX2		.equ 239
kX1		.equ 223
kMath		.equ 191
kAlpha		.equ 127

;Group 7
KeyRow_Top 	.equ $bf
KGraph		.equ 254
KTrace		.equ 253
KZoom		.equ 251
KWindow		.equ 247
KY		.equ 239
k2nd		.equ 223
kMode		.equ 191
kDel		.equ 127


