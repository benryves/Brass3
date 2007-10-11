
;
;======================================================================
;           Scan Code =ates
;======================================================================;
skDown          =	01h
skLeft          =	02h
skRight         =	03h
skUp            =	04h
skEnter         =	09h
skAdd           =	0Ah
skSub           =	0Bh
skMul           =	0Ch
skDiv           =	0Dh
skPower         =	0Eh
skClear         =	0Fh
skChs           =	11h
sk3             =	12h
sk6             =	13h
sk9             =	14h
skRParen        =	15h
skTan           =	16h
skVars          =	17h
skDecPnt        =	19h
sk2             =	1Ah
sk5             =	1Bh
sk8             =	1Ch
skLParen        =	1Dh
skCos           =	1Eh
skPrgm          =	1Fh
skStat          =	20h
sk0             =	21h
sk1             =	22h
sk4             =	23h
sk7             =	24h
skComma         =	25h
skSin           =	26h
skMatrix        =	27h
skGraphvar      =	28h
skStore         =	2Ah
skLn            =	2Bh
skLog           =	2Ch
skSquare        =	2Dh
skRecip         =	2Eh
skMath          =	2Fh
skAlpha         =	30h
skGraph         =	31h
skTrace         =	32h
skZoom          =	33h
skWindow        =	34h
skYequ          =	35h
sk2nd           =	36h
skMode          =	37h
skDel           =	38h
;

;======================================================================
;	Direct Input
;======================================================================

DReset		= 0FFh

Group1		= 0FEh
KdDown		= 254 
KdLeft		= 253 
KdRight		= 251 
KdUp		= 247
kdDownLeft	= %11111100
kdDownRight	= %11111010
kdUpLeft	= %11110101
kdUpRight	= %11110011

Group2		= 0FDh
KdEnter		= 254 
KdPlus		= 253 
KdMinus		= 251 
KdMul		= 247 
KdDiv		= 239 
KdPower		= 223 
KdClear		= 191 

Group3		= 0FBh
kdMinus2	= 254 
kdThree		= 253 
kdSix		= 251 
kdNine		= 247 
kdRbracket	= 239 
kdTan		= 223 
kdVars		= 191 

Group4		= 0F7h
KdPoint		= 254 
KdTwo		= 253 
KdFive		= 251
KdEight		= 247
KdLbracket	= 239
KdCos		= 223
KdPrgm		= 191
KdStat		= 127 

Group5		= 0EFh 
KdZero		= 254
KdOne		= 253
KdFour		= 251
KdSeven		= 247
KdComma		= 239
KdSin		= 223
KdMatrx		= 191
KdX		= 127 

Group6		= 0DFh
KdSto		= 253
KdLn		= 251
KdLog		= 247
kdX2		= 239
kdX1		= 223
kdMath		= 191
kdAlpha		= 127 

Group7		= 0BFh
KdGraph		= 254
KdTrace		= 253
KdZoom		= 251
KdWindow	= 247
KdY		= 239
kd2nd		= 223
kdMode		= 191
kdDel		= 127


