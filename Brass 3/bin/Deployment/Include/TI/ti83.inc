;
;       TI83 SYSTEM ROUTINE EQUATES
;
_getcsc                 =     4014h
contrast                =     8008h

_memChk                 =     441Ah
_CLRLCDFULL             =     4755h
_clrScrnFull            =     475Dh
_CLRTXTSHD              =     4765h
_dispDone               =     47F1h
_dispHL                 =     4709h
_DISPOP1A               =     51D4h
_eraseEOL               =     476Dh
_formDCplx              =     4D2Eh
_formReal               =     4D32h
_GDISPTOKEN             =     4B20h
_homeUp                 =     4775h
_lcd_busy               =     4066h
_moveup                 =     474Dh
_newline                =     473Dh
_outputExpr             =     5106h
_putc                   =     4705h
_putmap                 =     4701h
_putps                  =     4715h
_puts                   =     470Dh
_runIndicOff            =     4795h
_runIndicOn             =     4791h
_scrollDown             =     4751h
_scrollUp               =     4745h
_setPenT                =     51B9h
_setPenX                =     51A7h
_setPenY                =     51B0h
_SFONT_LEN              =     4A6Ch
_VPUTBLANK              =     4C53h
_vputmap                =     477Dh
_vputs                  =     4781h
_vputsn                 =     4785h
_Axes                   =     519Eh
_CLine                  =     4A84h
_CPointS                =     4B00h
_GRBUFCLR               =     515Bh
_GRBUFCPY_V             =     5164h
_HORIZCMD               =     4BEFh
_ILINE                  =     4AE4h
_IPOINT                 =     4AE8h
_moveDown               =     4741h
_PDspGrph               =     4BEBh
_PUTXY                  =     4BE4h
_REGRAPH                =     4BCCh
_VERTCMD                =     4BF3h
_GetK                   =     4A18h
_getkey                 =     4CFEh
_ADRLELE                =     489Dh
_GETLTOOP1              =     48A9h
_PUTTOL                 =     48BDh
_ADRMELE                =     486Dh
_GETMTOOP1              =     487Dh
_PUTTOMAT               =     4889h
_ACOS                   =     4122h
_ACOSH                  =     413Ah
_ANGLE                  =     4152h
_ASIN                   =     412Ah
_ASINH                  =     4136h
_ATAN                   =     4126h
_ATANH                  =     4132h
_Ceiling                =     4BE0h
_COS                    =     40FAh
_COSH                   =     410Ah
_CUBE                   =     409Eh
_DTOR                   =     4096h
_ETOX                   =     40EAh
_FACTORIAL              =     50C6h
_FPADD                  =     4092h
_FPDIV                  =     40C6h
_FPMULT                 =     40AAh
_FPRECIP                =     40C2h
_FPSQUARE               =     40A6h
_FPSUB                  =     408Eh
_FRAC                   =     40BEh
_INT                    =     40D6h
_INTGR                  =     4076h
_INVOP1SC               =     40B2h
_INVSUB                 =     407Eh
_LNX                    =     40DEh
_LOGX                   =     40E2h
_MAX                    =     406Eh
_MIN                    =     406Ah
_MINUS1                 =     408Ah
_PLUS1                  =     4086h
_PTOR                   =     413Eh
_RANDOM                 =     50B6h
_RNDGUARD               =     40CEh
_ROUND                  =     40DAh
_RTOD                   =     409Ah
_RTOP                   =     4142h
_SIN                    =     40F6h
_SINH                   =     410Eh
_SQROOT                 =     40CAh
_STORAND                =     50BAh
_TAN                    =     40FEh
_TANH                   =     4106h
_TENX                   =     40EEh
_TIMES2                 =     4082h
_TIMESPT5               =     40A2h
_TRUNC                  =     407Ah
_XROOTY                 =     4A8Ch
_YTOX                   =     4A90h
_CHKFINDSYM             =     442Ah
_DELVAR                 =     44AAh
_RCLSYSTOK              =     4EF0h
_RCLVARSYM              =     4EECh
_RCLX                   =     4EE8h
_RCLY                   =     4EE0h
_RSTRSHADOW             =     47A1h
_SAVESHADOW             =     479Dh
_STOOTHER               =     4ED8h
_STOSYSTOK              =     4EB8h
_STOX                   =     4ED4h
_STOY                   =     4EC8h
_CKINT                  =     4322h
_CKODD                  =     4326h
_CKOP1FP0               =     4312h
_CKOP1POS               =     435Ah
_CKOP1REAL              =     414Eh
_CKOP2FP0               =     4316h
_CKOP2POS               =     4356h
_CKOP2REAL              =     4412h
_CKPOSINT               =     431Eh
_CLROP1S                =     4362h
_CLROP2S                =     435Eh
_CONVOP1                =     4EFCh
_cphlde                 =     4004h
_CPOP1OP2               =     4166h
_divHLbyA               =     400Ch
_HTIMESL                =     4382h
_INVOP1S                =     40B6h
_INVOP2S                =     40BAh
_ldHLind                =     4000h
_MOVFROP1               =     4212h
_OP1EXOP2               =     4306h
_OP1EXOP3               =     42FEh
_OP1EXOP4               =     4302h
_OP1EXOP5               =     42EEh
_OP1EXOP6               =     42F2h
_OP1SET0                =     4286h
_OP1SET1                =     4232h
_OP1SET2                =     4242h
_OP1SET3                =     423Ah
_OP1SET4                =     4236h
_OP1TOOP2               =     418Eh
_OP1TOOP3               =     417Eh
_OP1TOOP4               =     416Eh
_OP1TOOP5               =     41BEh
_OP1TOOP6               =     41BAh
_OP2EXOP4               =     42F6h
_OP2EXOP5               =     42FAh
_OP2EXOP6               =     42E6h
_OP2SET0                =     4282h
_OP2SET1                =     4252h
_OP2SET2                =     4246h
_OP2SET3                =     422Eh
_OP2SET4                =     422Ah
_OP2SET5                =     4222h
_OP2SET60               =     4EA8h
_OP2SET8                =     421Eh
_OP2SETA                =     4226h
_OP2TOOP1               =     41C2h
_OP2TOOP3               =     41F6h
_OP2TOOP4               =     4172h
_OP2TOOP5               =     41B2h
_OP2TOOP6               =     41B6h
_OP3SET0                =     427Eh
_OP3SET1                =     421Ah
_OP3SET2                =     423Eh
_OP3TOOP1               =     41A2h
_OP3TOOP2               =     417Ah
_OP3TOOP4               =     416Ah
_OP3TOOP5               =     41AEh
_OP4SET0                =     427Ah
_OP4SET1                =     4216h
_OP4TOOP1               =     419Ah
_OP4TOOP2               =     4176h
_OP4TOOP3               =     41FAh
_OP4TOOP5               =     41AAh
_OP4TOOP6               =     4202h
_OP5EXOP6               =     42EAh
_OP5SET0                =     4276h
_OP5TOOP1               =     419Eh
_OP5TOOP2               =     4182h
_OP5TOOP3               =     41FEh
_OP5TOOP4               =     418Ah
_OP5TOOP6               =     4186h
_OP6TOOP1               =     4196h
_OP6TOOP2               =     4192h
_OP6TOOP5               =     41A6h
_PUSHREALO1             =     4536h
_SETXXOP1               =     4A74h
_SETXXOP2               =     4A78h
_SETXXXXOP2             =     4A7Ch
_ZEROOOP1               =     428Eh
_IO_EXEC                =     51EFh

;
;       OP1 TO OP6 RAM EQUATES
;
OP2         =                 8044h
op2exp      =                 8045h
op2m        =                 8046H
OP1M        =                 803Bh
OP1         =                 8039h
OP3         =                 804Fh
OP3EXP      =                 8050h
OP3EXT      =                 8058h
OP3M        =                 8051h
OP4         =                 805Ah
OP4EXP      =                 805Bh
OP4EXT      =                 8063h
OP4M        =                 805Ch
OP5         =                 8065h
OP5EXP      =                 8066h
OP5EXT      =                 806Eh
OP5M        =                 8067h
OP6         =                 8070h
OP6EXP      =                 8071h
OP6EXT      =                 8079h
OP6M        =                 8072h
;
;       POINTERS
;
IMATHPTR1   =                 8094h
IMATHPTR2   =                 8096h
IMATHPTR3   =                 8098h
IMATHPTR4   =                 809Ah
IMATHPTR5   =                 809Ch
;
;       POIONTER TO START OF PROGRAM/LIST SYMBOL ENTRIES
;
PROGPTR     =                 9319h
;
;       ADDRESS OF LCD DRIVER INSTRUCTION PORT
;
LCDINSTPORT  =                10h
;
;       SCREEN SHADOWS
;
TEXTSHADOW  =                 80C9h
CMDSHADOW   =                 9157h
;
;       GRAPH BACKUP BUFFER
;
PLOTSSCREEN =                 8E29h
;
;       SAFE RAM LOCATIONS
;
SAVESSCREEN =                 8265h
STATVARS    =                 858Fh
;
;       START OF SYMBOL TABLE
;
SYMTABLE    =                 0FE6Eh
;
;       RAM EQUATES DEALING WITH DISPLAY ROUTINES
;
CURROW      =                 800Ch
CURCOL      =                 800Dh
PENCOL      =                 8252h
PENROW      =                 8253h
CURGY       =                 886Dh
CURGX       =                 886Eh
;
;       INDIRECT CALL BYTE
;
ASM_IND_CALL    =             80C8h
;
;       LINK PORT WRITE EQUATES
;
D0LD1L      =              0C3h
D0LD1H      =              0C1h
D0HD1L      =              0C2h
D0HD1H      =              0C0h
BPORT       =              0
;
;       SYSTEM FLAG EQUATES
;       ALL OF THESE FLAGS CAN BE ACCESSED THROUGH THE 'IY' REGISTER
;
trigflags       =       0           ; IY OFFSET VALUE
trigdeg         =       2
;
plotflags       =       2           ; IY OFFSET VALUE
plotloc         =       1
plotdisp        =       2
;
grfmodeflags    =       2           ; IY OFFSET VALUE
grffuncm        =       4
grfpolarm       =       5
grfparamm       =       6
grfrecurm       =       7
;
graphflags      =       3           ; IY OFFSET VALUE
graphdraw       =       0
graphcursor     =       2
;
grfdbflags      =       4           ; IY OFFSET VALUE
grfdot          =       0
grfsimul        =       1
grfgrid         =       2
grfpolar        =       3
grfnocoord      =       4
grfnoaxis       =       5
grflabel        =       6
;
textflags       =       5           ; IY OFFSET VALUE
textEraseBelow  =       1
textScrolled    =       2
textInverse     =       3
;
onflags         =       9           ; IY OFFSET VALUE
onRunning       =       3
onInterrupt     =       4
;
statflags       =       9           ; IY OFFSET VALUE
statsvalid      =       6
;
fmtflags        =       10          ; IY OFFSET VALUE
fmtExponent     =       0
fmtEng          =       1
;
nummode         =       10          ; IY OFFSET VALUE
FMTREAL         =       5
FMTRECT         =       6
FMTPOLAR        =       7
;
curflags        =       12          ; IY OFFSET VALUE
curAble         =       2
curOn           =       3
curLock         =       4
;
appflags        =       13          ; IY OFFSET VALUE
appTextSave     =       1
appAutoScroll   =       2
;
PLOTFLAG2       =       17          ; IY OFFSET VALUE
EXPR_PARAM      =       3
EXPR_WRITING    =       4
;
indicflags      =       18          ; IY OFFSET VALUE
indicRun        =       0
indicOnly       =       2
;
shiftflags      =       18          ; IY OFFSET VALUE
shift2nd        =       3
shiftAlpha      =       4
shiftALock      =       6
;
tblflags        =       19          ; IY OFFSET VALUE
AutoFill        =       4
AutoCalc        =       5
;
sgrflags        =       20          ; IY OFFSET VALUE
grfSplit        =       0
VertSplit       =       1
WRITE_ON_GRAPH  =       4
textwrite       =       7
;
asm_flag1       =       33          ; IY OFFSET VALUE
asm_flag1_0     =       0
asm_flag1_1     =       1
asm_flag1_2     =       2
asm_flag1_3     =       3
asm_flag1_4     =       4
asm_flag1_5     =       5
asm_flag1_6     =       6
asm_flag1_7     =       7
;
asm_flag2       =       34          ; IY OFFSET VALUE
asm_flag2_0     =       0
asm_flag2_1     =       1
asm_flag2_2     =       2
asm_flag2_3     =       3
asm_flag2_4     =       4
asm_flag2_5     =       5
asm_flag2_6     =       6
asm_flag2_7     =       7
;
asm_flag3       =       35          ; IY OFFSET VALUE
asm_flag3_0     =       0
asm_flag3_1     =       1
asm_flag3_2     =       2
asm_flag3_3     =       3
asm_flag3_4     =       4
asm_flag3_5     =       5
asm_flag3_6     =       6
asm_flag3_7     =       7
;
;       SYSTEM VAR EQUATES, FOR STORING AND RECALLING THEM
;
XSCLt       =  2        ; XSCALE
YSCLt       =  3        ; YSCALE
XMINt       =  0AH      ; XMIN
XMAXt       =  0BH      ; XMAX
YMINt       =  0CH      ; YMIN
YMAXt       =  0DH      ; XMAX
TMINt       =  0EH      ; TMIN
TMAXt       =  0FH      ; TMAX
THETMINt    =  10H      ; THETA MIN
THETMAXt    =  11H      ; THETA MAX
TBLMINt     =  1AH      ; TABLE MIN
PLOTSTARTt  =  1BH      ; PLOT START
NMAXt       =  1DH      ; NMAX
nmint       =  1FH      ; NMIN
TBLSTEPt    =  21H      ; TABLE STEP
TSTEPt      =  22H      ; T STEP
THETSTEPt   =  23H      ; THETA STEP
DELTAXt     =  26H      ; DELTA X
DELTAYt     =  27H      ; DELTA Y
XFACTt      =  28H      ; X ZOOM FACTOR
YFACTt      =  29H      ; Y ZOOM FACTOR
FINnT       =  2Bh      ; TVM n
FINiT       =  2Ch      ; TVM i
FINpvT      =  2Dh      ; TVM pv
FINpmtT     =  2Eh      ; TVM pmt
FINfvT      =  2Fh      ; TVM fv
FINpyT      =  30h      ; TVM p/y
FINcyT      =  31h      ; TVM c/y
PLOTSTEPT   =  34h      ; PLOT STEP
XREST       =  36h      ; X RES

















;
;       CHARACTER FONT EQUATES
;
LSEQ_N          =     001h
LSEQ_U          =     002h
LSEQ_V          =     003h
LSEQ_W          =     004h
Lconvert        =     005h
LsqUp           =     006h
LsqDown         =     007h
Lintegral       =     008h
Lcross          =     009h
LBOXICON        =     00Ah
LCROSSICON      =     00Bh
LDOTICON        =     00Ch
LsubT           =     00Dh            ;small capital T for parametric mode.
LcubeR          =     00Eh            ;slightly different 3 for cubed root.
LhexF           =     00Fh
Lroot           =     010h
Linverse        =     011h
Lsquare         =     012h
Langle          =     013h
Ldegree         =     014h
Lradian         =     015h
Ltranspose      =     016h
LLE             =     017h
LNE             =     018h
LGE             =     019h
Lneg            =     01Ah
Lexponent       =     01Bh
Lstore          =     01Ch
Lten            =     01Dh
LupArrow        =     01Eh
LdownArrow      =     01Fh
Lspace          =     020h
Lexclam         =     021h
Lquote          =     022h
Lpound          =     023h
LFOURTH         =     024h
Lpercent        =     025h
Lampersand      =     026h
Lapostrophe     =     027h
LlParen         =     028h
LrParen         =     029h
Lasterisk       =     02Ah
LplusSign       =     02Bh
Lcomma          =     02Ch
Ldash           =     02Dh
Lperiod         =     02Eh
Lslash          =     02Fh
L0              =     030h
L1              =     031h
L2              =     032h
L3              =     033h
L4              =     034h
L5              =     035h
L6              =     036h
L7              =     037h
L8              =     038h
L9              =     039h
Lcolon          =     03Ah
Lsemicolon      =     03Bh
LLT             =     03Ch
LEQ             =     03Dh
LGT             =     03Eh
Lquestion       =     03Fh
LatSign         =     040h
LcapA           =     041h
LcapB           =     042h
LcapC           =     043h
LcapD           =     044h
LcapE           =     045h
LcapF           =     046h
LcapG           =     047h
LcapH           =     048h
LcapI           =     049h
LcapJ           =     04Ah
LcapK           =     04Bh
LcapL           =     04Ch
LcapM           =     04Dh
LcapN           =     04Eh
LcapO           =     04Fh
LcapP           =     050h
LcapQ           =     051h
LcapR           =     052h
LcapS           =     053h
LcapT           =     054h
LcapU           =     055h
LcapV           =     056h
LcapW           =     057h
LcapX           =     058h
LcapY           =     059h
LcapZ           =     05Ah
Ltheta          =     05Bh
Lbackslash      =     05Ch
LrBrack         =     05Dh
Lcaret          =     05Eh
Lunderscore     =     05Fh
Lbackquote      =     060h
La              =     061h
Lb              =     062h
Lc              =     063h
Ld              =     064h
Le              =     065h
Lf              =     066h
Lg              =     067h
Lh              =     068h
Li              =     069h
Lj              =     06Ah
Lk              =     06Bh
Ll              =     06Ch
Lm              =     06Dh
Ln              =     06Eh
Lo              =     06Fh
Lp              =     070h
Lq              =     071h
Lr              =     072h
Ls              =     073h
Lt              =     074h
Lu              =     075h
Lv              =     076h
Lw              =     077h
Lx              =     078h
Ly              =     079h
Lz              =     07Ah
LlBrace         =     07Bh
Lbar            =     07Ch
LrBrace         =     07Dh
Ltilde          =     07Eh
LinvEQ          =     07Fh
Lsub0           =     080h
Lsub1           =     081h
Lsub2           =     082h
Lsub3           =     083h
Lsub4           =     084h
Lsub5           =     085h
Lsub6           =     086h
Lsub7           =     087h
Lsub8           =     088h
Lsub9           =     089h
LcapAAcute      =     08Ah
LcapAGrave      =     08Bh
LcapACaret      =     08Ch
LcapADier       =     08Dh
LaAcute         =     08Eh
LaGrave         =     08Fh
LaCaret         =     090h
LaDier          =     091h
LcapEAcute      =     092h
LcapEGrave      =     093h
LcapECaret      =     094h
LcapEDier       =     095h
LeAcute         =     096h
LeGrave         =     097h
LeCaret         =     098h
LeDier          =     099h
LcapIAcute      =     09Ah
LcapIGrave      =     09Bh
LcapICaret      =     09Ch
LcapIDier       =     09Dh
LiAcute         =     09Eh
LiGrave         =     09Fh
LiCaret         =     0A0h
LiDier          =     0A1h
LcapOAcute      =     0A2h
LcapOGrave      =     0A3h
LcapOCaret      =     0A4h
LcapODier       =     0A5h
LoAcute         =     0A6h
LoGrave         =     0A7h
LoCaret         =     0A8h
LoDier          =     0A9h
LcapUAcute      =     0AAh
LcapUGrave      =     0ABh
LcapUCaret      =     0ACh
LcapUDier       =     0ADh
LuAcute         =     0AEh
LuGrave         =     0AFh
LuCaret         =     0B0h
LuDier          =     0B1h
LcapCCed        =     0B2h
LcCed           =     0B3h
LcapNTilde      =     0B4h
LnTilde         =     0B5h
Laccent         =     0B6h
Lgrave          =     0B7h
Ldieresis       =     0B8h
LquesDown       =     0B9h
LexclamDown     =     0BAh
Lalpha          =     0BBh
Lbeta           =     0BCh
Lgamma          =     0BDh
LcapDelta       =     0BEh
Ldelta          =     0BFh
Lepsilon        =     0C0h
LlBrack         =     0C1h
Llambda         =     0C2h
Lmu             =     0C3h
Lpi             =     0C4h
Lrho            =     0C5h
LcapSigma       =     0C6h
Lsigma          =     0C7h
Ltau            =     0C8h
Lphi            =     0C9h
LcapOmega       =     0CAh
LxMean          =     0CBh
LyMean          =     0CCh
LsupX           =     0CDh
Lellipsis       =     0CEh
Lleft           =     0CFh
Lblock          =     0D0h
Lper            =     0D1h
Lhyphen         =     0D2h
Larea           =     0D3h
Ltemp           =     0D4h
Lcube           =     0D5h
Lenter          =     0D6h
LimagI          =     0D7h
Lphat           =     0D8h
Lchi            =     0D9h
LstatF          =     0DAh
Llne            =     0DBh
LlistL          =     0DCh
LfinanN         =     0DDh

;======================================================================
;Scan Code Equates
;======================================================================
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
skApps        	=	27h
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
skYEqu          =	35h
sk2nd           =	36h
skMode          =	37h
skDel           =	38h


;                                             length (+80h for leading blank)
;       # TOKENS IN USE = 249
;						     (+40h for trailing blank)
EOSSTART        =       0
;
;
;   DISPLAY CONVERSIONS  COME IMMEDIATELY BEFORE 'TSTORE'
;
DCONV   =     01H
;
TTODMS          =       DCONV       ; 01H
TTODEC          =       DCONV+1     ; 02H
TTOAbc          =       DCONV+2     ; 03H    > A b/c
;
TSTORE          =       DCONV+3     ; 04H Lstore                01
;
TBOXPLOT        =       05h
;
BRACKS          =       06H
;
tLBrack         =       BRACKS      ; 06h '['
tRBrack         =       BRACKS+1    ; 07h ']'
tLBrace         =       BRACKS+2    ; 08h '{'
tRBrace         =       BRACKS+3    ; 09h '}'
;
TPOST1          =       BRACKS+4
;
tFromRad        =       TPOST1       ; 0Ah Lradian
tFromDeg        =       TPOST1+1     ; 0Bh Ldegree
tRecip          =       TPOST1+2     ; 0Ch Linverse
tSqr            =       TPOST1+3     ; 0Dh Lsquare
tTrnspos        =       TPOST1+4     ; 0Eh Ltranspose
TCUBE           =       TPOST1+5     ; 0Fh '^3'
;
tLParen         =       10H         ; 10h '('
tRParen         =       11H         ; 11h ')'
;
;
;   IMMEDIATE UNARY TOKENS GO FROM HERE TO 'EOSEL-1'
;
IMUN            =       12H
;
tRound          =       IMUN        ; 12h 'round'
TPXTST          =       IMUN+1      ; 13h 'PXL-TEST'
tAug            =       IMUN+2      ; 14h 'aug'
tRowSwap        =       IMUN+3      ; 15h 'rSwap'
tRowPlus        =       IMUN+4      ; 16h 'rAdd'
tmRow           =       IMUN+5      ; 17h 'multR'
tmRowPlus       =       IMUN+6      ; 18h 'mRAdd'
tMax            =       IMUN+7      ; 19H 'max'
tMin            =       IMUN+8      ; 1AH 'min'
TRTOPr          =       IMUN+9      ; 1BH 'R>Pr
TRTOPo          =       IMUN+10     ; 1CH 'R>Po
TPTORx          =       IMUN+11     ; 1DH 'P>Rx
TPTORy          =       IMUN+12     ; 1EH 'P>Ry
tMEDIAN         =       IMUN+13     ; 1Fh 'MEDIAN
tRandM          =       IMUN+14     ; 20h 'randM'
TMEAN           =       IMUN+15     ; 21h
TROOT           =       IMUN+16     ; 22h 'ROOT'
tSeries         =       IMUN+17     ; 23h 'seq'
tFnInt          =       IMUN+18     ; 24h 'fnInt'
tNDeriv         =       IMUN+19     ; 25h 'fnIr'
TEVALF          =       IMUN+20     ; 26h
TFMIN           =       IMUN+21     ; 27h
TFMAX           =       IMUN+22     ; 28h
;
;
;
;  TOKEN 29H CANNOT BE AN EOS FUNCTION SINCE EL(=29H ALREADY
;
tEOSEL          =       IMUN+23
;
TSPACE          =       tEOSEL       ; 29h   ' '
tString         =       tEOSEL+1     ; 2Ah  '"'
tComma          =       tEOSEL+2     ; 2Bh  ','
;
;
;
Tii             =       2Ch         ; i
;
; ===== Postfix functions
;
TPOST           =       2Dh
;
tFact           =       TPOST       ; 2Dh '!'



TCUBICR         =       2Eh
TQUARTR         =       2Fh

;
;       NUMBER TOKENS
;
NUMS            =       30h
;
T0              =       NUMS        ; 30h
T1              =       NUMS+1      ; 31h
T2              =       NUMS+2      ; 32h
T3              =       NUMS+3      ; 33h
T4              =       NUMS+4      ; 34h
T5              =       NUMS+5      ; 35h
T6              =       NUMS+6      ; 36h
T7              =       NUMS+7      ; 37h
T8              =       NUMS+8      ; 38h
T9              =       NUMS+9      ; 39h
TDECPT          =       NUMS+10     ; 3Ah
Tee             =       NUMS+11     ; 3Bh
;
;   BINARY OP
;
tOr             =       3Ch         ; 3Ch  '_or_'
TXOR            =       3Dh         ; 3Dh
;
tColon          =       3Eh         ; 3Eh ':'
tEnter          =       3Fh         ; 3Fh  Lenter
;
;   BINARY OP
;
tAnd            =       40h         ; 40h  '_and_'               05
;
;       LETTER TOKENS
;
LET             =       41h
;
TA              =       LET         ; 41h
TB              =       LET+1       ; 42h
TC              =       LET+2       ; 43h
TD              =       LET+3       ; 44h
TE              =       LET+4       ; 45h
TF              =       LET+5       ; 46h
TG              =       LET+6       ; 47h
TH              =       LET+7       ; 48h
TI              =       LET+8       ; 49h
TJ              =       LET+9       ; 4Ah
TK              =       LET+10      ; 4Bh
TL              =       LET+11      ; 4Ch
TM              =       LET+12      ; 4Dh
TN              =       LET+13      ; 4Eh
TO              =       LET+14      ; 4Fh
TP              =       LET+15      ; 50h
TQ              =       LET+16      ; 51h
TR              =       LET+17      ; 52h
TS              =       LET+18      ; 53h
TT              =       LET+19      ; 54h
TU              =       LET+20      ; 55h
TV              =       LET+21      ; 56h
TW              =       LET+22      ; 57h
TX              =       LET+23      ; 58h
TY              =       LET+24      ; 59h
TZ              =       LET+25      ; 5Ah
TTHETA          =       LET+26      ; 5Bh
;
;
;   THESE VAR TOKENS ARE 1ST OF A DOUBLE TOKEN
;
;   SEE 'VARS.DOC' FOR MORE INFO ON VARS
;
VTOKS   =     LET+27    ;
;
;       USER MATRIX TOKEN, 2ND TOKEN NEEDED FOR NAME
;
TVARMAT         =       VTOKS       ; 5Ch
;
;       USER LIST TOKEN, 2ND TOKEN NEEDED FOR NAME
;
TVARLST         =       VTOKS+1     ; 5Dh
;
;       USER EQUATION TOKEN, 2ND TOKEN NEEDED FOR NAME
;
TVAREQU         =       VTOKS+2     ; 5Eh
;
;       THIS IS A SINGLE TOKEN, I WILL HAVE TO READ THE NAME IN
;       EVERY SINGLE TIME I EXECUTE
;
TPROG           =       VTOKS+3     ; 5Fh
;
;       USER PICT TOKEN, 2ND TOKEN NEEDED FOR NAME
;
TVARPICT        =       VTOKS+4     ; 60h
;
;       USER GDB TOKEN, 2ND TOKEN NEEDED FOR NAME
;
TVARGDB         =       VTOKS+5     ; 61h
;
;       SYSTEM VARS THAT ARE USED FOR OUTPUT ONLY
;       CANNOT BE STORED TO BY THE USER
;
tVarOut         =       VTOKS+6     ; 62h
;
;       SYSTEM VARS THAT ARE NOT IN THE SYMBOL TABLE
;       BUT THE USER CAN STORE TO THEM.
;
tVarSys         =       VTOKS+7     ; 63h
;
;
; ===== Mode setting commands
;
MODESA          =       VTOKS+8     ; 64h
;
tRad            =       MODESA      ; 64h 'Radian'
tDeg            =       MODESA+1    ; 65h 'Degree'
tNormF          =       MODESA+2    ; 66h 'Normal'
tSci            =       MODESA+3    ; 67h 'Sci'
tEng            =       MODESA+4    ; 68h 'Eng'
tFloat          =       MODESA+5    ; 69h 'Float'
;
CMPS            =       6Ah
;
tEQ             =       CMPS        ; 6Ah '=='
tLT             =       CMPS+1      ; 6Bh '<'
tGT             =       CMPS+2      ; 6Ch '>'
tLE             =       CMPS+3      ; 6Dh LLE
tGE             =       CMPS+4      ; 6Eh LGE
tNE             =       CMPS+5      ; 6Fh LNE
;
;   BINARY OP
;
tAdd            =       70h         ; 70h '+'
tSub            =       71h         ; 71h '-'
;
TANS            =       72H         ; 72h
;
; ===== Mode setting commands
;
MODES           =       73h
;
tFix            =       MODES       ; 73h 'Fix_'
TSPLITON        =       MODES+1     ; 74h
TFULLSCREEN     =       MODES+2     ; 75h
tStndrd         =       MODES+3     ; 76h 'Func'
tParam          =       MODES+4     ; 77h 'Param'
tPolar          =       MODES+5     ; 78h 'Pol'
TSEQG           =       MODES+6     ; 79h
TAFILLON        =       MODES+7     ; 7Ah 'AUTO FILL ON
TAFILLOFF       =       MODES+8     ; 7Bh
TACALCON        =       MODES+9     ; 7Ch
TACALCOFF       =       MODES+10    ; 7Dh 'AUTOFILL OFF
;
;
;       GRAPH FORMAT TOKENS ARE 2 BYTE TOKENS
;
TGFORMAT        =       MODES+11    ; 7Eh
;
TBOXICON        =       7Fh
TCROSSICON      =       80h
TDOTICON        =       81h
;
;       BINARY OP
;
tMul            =       82h         ; 82h  '*'
tDiv            =       83h         ; 83h  '/'
;
;       SOME GRAPH COMMANDS
;
GRCMDS          =       84h
;
tTrace          =       GRCMDS      ; 84h 'Trace'
tClDrw          =       GRCMDS+1    ; 85h 'ClDrw'
tZoomStd        =       GRCMDS+2    ; 86h 'ZStd'
tZoomtrg        =       GRCMDS+3    ; 87h 'Ztrg'
tZOOMBOX        =       GRCMDS+4    ; 88h 'ZBOX'
tZoomIn         =       GRCMDS+5    ; 89h 'ZIn'
tZoomOut        =       GRCMDS+6    ; 8Ah 'ZOut'
tZoomSqr        =       GRCMDS+7    ; 8Bh 'ZSqr'
tZoomInt        =       GRCMDS+8    ; 8Ch 'ZInt'
tZoomPrev       =       GRCMDS+9    ; 8Dh 'ZPrev'
tZoomDec        =       GRCMDS+10   ; 8Eh 'ZDecm'
TZOOMSTAT       =       GRCMDS+11   ; 8Fh 'ZSTAT
tUsrZm          =       GRCMDS+12   ; 90h 'ZRcl'
tPrtScrn        =       GRCMDS+13   ; 91h 'PrtScrn'
tZOOMSTO        =       GRCMDS+14   ; 92H  ZOOM STORE
TTEXT           =       GRCMDS+15   ; 93h
;
;       BINARY OP
;
tnPr            =       GRCMDS+16   ; 94h '_nPr_'
tnCr            =       GRCMDS+17   ; 95h '_nCr_'
;
;       MORE GRAPH COMMANDS
;
tYOn            =       GRCMDS+18   ; 96h 'FnOn_'
tYOff           =       GRCMDS+19   ; 97h 'FnOff_'
tStPic          =       GRCMDS+20   ; 98h 'StPic_'
tRcPic          =       GRCMDS+21   ; 99h 'RcPic_'
tStoDB          =       GRCMDS+22   ; 9Ah 'StGDB_'
tRclDB          =       GRCMDS+23   ; 9Bh 'RcGDB_'
tLine           =       GRCMDS+24   ; 9Ch 'Line'
tVert           =       GRCMDS+25   ; 9Dh 'Vert_'
tPtOn           =       GRCMDS+26   ; 9Eh 'PtOn'
tPtOff          =       GRCMDS+27   ; 9Fh 'PtOff'
;
;  TOKEN A0 CANNOT BE AN EOS FUNCTION SINCE LOW MULT=A0 ALREADY
;
tPtChg          =       GRCMDS+28   ; A0h 'PtChg'
TPXON           =       GRCMDS+29   ; A1h
TPXOFF          =       GRCMDS+30   ; A2h
TPXCHG          =       GRCMDS+31   ; A3h
tShade          =       GRCMDS+32   ; A4h 'Shade'
tCircl          =       GRCMDS+33   ; A5h 'Circl'
tHORZ           =       GRCMDS+34   ; A6h 'HORIZONTAL'
tTanLn          =       GRCMDS+35   ; A7h 'TanLn'
tDrInv          =       GRCMDS+36   ; A8h 'DrInv_'
tDrawF          =       GRCMDS+37   ; A9h 'DrawF_'
;
TVARSTRNG       =       0AAh
;
; ===== Functions with no arguments
;
NOARG   =     0ABh
;
tRand           =       NOARG       ; ABh 'rand'
tPi             =       NOARG+1     ; ACh  Lpi
tGetKey         =       NOARG+2     ; ADh 'getKy'
;
;
TAPOST          =       TGETKEY+1   ; APOSTROPHY
TQUEST          =       TAPOST+1    ; QUESTION MARK
;
;
; ===== Unary functions
;
;       UNARY : THESE UNARY FUNCTIONS TAKE A COMBINATION OF
;               ARGUMENTS, REAL,LIST,MATRIX
;
UNARY   =     TQUEST+1              ; B0h
;
tChs            =       UNARY       ; B0h
tInt            =       UNARY+1     ; B1h
TABS            =       UNARY+2     ; B2h
tDet            =       UNARY+3     ; B3h
tIdent          =       UNARY+4     ; B4h
tDim            =       UNARY+5     ; B5h
tSum            =       UNARY+6     ; B6h
tProd           =       UNARY+7     ; B7h
tNot            =       UNARY+8     ; B8h
tIPart          =       UNARY+9     ; B9h
tFPart          =       UNARY+10    ; BAh
;
;
;       NEW 2 BYTE TOKENS
;
T2BYTETOK       =       0BBh         ;
;
;
;       UNARYLR : UNARY FUNCTIONS THAT ONLY WORK WITH
;                 REAL NUMBERS AND LISTS
;
UNARYLR =     UNARY+12
;
tSqrt           =       UNARYLR     ; BCh
TCUBRT          =       UNARYLR+1   ; BDh
tLn             =       UNARYLR+2   ; BEh
tExp            =       UNARYLR+3   ; BFh
tLog            =       UNARYLR+4   ; C0h
tALog           =       UNARYLR+5   ; C1h
tSin            =       UNARYLR+6   ; C2h
tASin           =       UNARYLR+7   ; C3h
tCos            =       UNARYLR+8   ; C4h
tACos           =       UNARYLR+9   ; C5h
tTan            =       UNARYLR+10  ; C6h
tATan           =       UNARYLR+11  ; C7h
tSinh           =       UNARYLR+12  ; C8h
tASinh          =       UNARYLR+13  ; C9h
tCosh           =       UNARYLR+14  ; CAh
tACosh          =       UNARYLR+15  ; CBh
tTanh           =       UNARYLR+16  ; CCh
tATanh          =       UNARYLR+17  ; CDh
;
;       SOME PROGRAMMING COMMANDS
;
PROGTOK =     UNARYLR+18
;
tIf             =       PROGTOK     ; CEh
tThen           =       PROGTOK+1   ; CFh
tElse           =       PROGTOK+2   ; D0h
tWhile          =       PROGTOK+3   ; D1h
tRepeat         =       PROGTOK+4   ; D2h
tFor            =       PROGTOK+5   ; D3h
tEnd            =       PROGTOK+6   ; D4h
tReturn         =       PROGTOK+7   ; D5h
tLbl            =       PROGTOK+8   ; D6h   'Lbl_'
tGoto           =       PROGTOK+9   ; D7h   'Goto_'
tPause          =       PROGTOK+10  ; D8h   'Pause_'
tStop           =       PROGTOK+11  ; D9h   'Stop'
tISG            =       PROGTOK+12  ; DAh   'IS>'
tDSL            =       PROGTOK+13  ; DBh   'DS<'
tInput          =       PROGTOK+14  ; DCh   'Input_'
tPrompt         =       PROGTOK+15  ; DDh   'Prompt_'
tDisp           =       PROGTOK+16  ; DEh   'Disp_'
tDispG          =       PROGTOK+17  ; DFh   'DispG'
;
;  TOKEN E0 CANNOT BE AN EOS FUNCTION SINCE HI MU
;
tOutput         =       PROGTOK+18  ; E0H    'Outpt'
tClLCD          =       PROGTOK+19  ; E1H    'ClLCD'
tConst          =       PROGTOK+20  ; E2H    'Fill'
tSortA          =       PROGTOK+21  ; E3H    'sortA_'
tSortD          =       PROGTOK+22  ; E4H    'sortD_'
TDISPTAB        =       PROGTOK+23  ; E5h    'DISP TABLE
tMenu           =       PROGTOK+24  ; E6H    'Menu'
tSENDMBL        =       PROGTOK+25  ; E7H    'SEND'
tGETMBL         =       PROGTOK+26  ; E8H    'GET'
;
;       STAT PLOT COMMANDS
;
STATPCMD        =       PROGTOK+27
;
TPLOTON         =       STATPCMD    ; E9h     ' PLOTSON'
TPLOTOFF        =       STATPCMD+1   ; EAh     ' PLOTSOFF
;
TLISTNAME       =       0EBH        ; LIST DESIGNATOR
;
;
;
TPLOT1          =       0ECH
TPLOT2          =       0EDH
TPLOT3          =       0EEH
;
tPower          =       0F0h        ;        '^'
tXRoot          =       0F1h        ;        LsupX,Lroot

STATCMD         =       0F2h
;
tOneVar         =       STATCMD     ; F2h    'OneVar_'
TTWOVAR         =       STATCMD+1   ; F3h
tLR             =       STATCMD+2   ; F4h    'LinR(A+BX
tLRExp          =       STATCMD+3   ; F5h    'ExpR_'
tLRLn           =       STATCMD+4   ; F6h    'LnR_'
tLRPwr          =       STATCMD+5   ; F7h    'PwrR_'
TMEDMED         =       STATCMD+6   ; F8h
TQUAD           =       STATCMD+7   ; F9h
tCLRLST         =       STATCMD+8   ; FAh    'CLEAR LIST
TCLRTBL         =       STATCMD+9   ; FBH    CLEAR TABLE
tHist           =       STATCMD+10  ; FCh    'Hist_'
txyLine         =       STATCMD+11  ; FDH    'xyline_'
tScatter        =       STATCMD+12  ; FEh    'Scatter_'
tLR1            =       STATCMD+13  ; FFh    'LINR(AX+B


;
; 2ND HALF OF GRAPH FORMAT TOKENS
;
; ===== Format settings commands
;
GFMT            =       0
;
tSeq            =       GFMT        ; 'SeqG'
tSimulG         =       GFMT+1      ; 'SimulG'
tPolarG         =       GFMT+2      ; 'PolarGC'
tRectG          =       GFMT+3      ; 'RectGC'
tCoordOn        =       GFMT+4      ; 'CoordOn'
tCoordOff       =       GFMT+5      ; 'CoordOff'
tDrawLine       =       GFMT+6      ; 'DrawLine'
tDrawDot        =       GFMT+7      ; 'DrawDot'
tAxisOn         =       GFMT+8      ; 'AxesOn'
tAxisOff        =       GFMT+9      ; 'AxesOff'
tGridOn         =       GFMT+10     ; 'GridOn'
tGridOff        =       GFMT+11     ; 'GridOff'
tLblOn          =       GFMT+12     ; 'LabelOn'
tLblOff         =       GFMT+13     ; 'LabelOff'
tWebOn          =       GFMT+14     ; 'WebOn'
tWebOff         =       GFMT+15     ; 'WebOFF'
TUV             =       GFMT+16     ; U vs V
TVW             =       GFMT+17     ; V vs W
TUW             =       GFMT+18     ; U vs W
;
;       2ND HALF OF USER MATRIX TOKENS
;
TMATA           =       00h         ; MAT A
TMATB           =       01h         ; MAT B
TMATC           =       02h         ; MAT C
TMATD           =       03h         ; MAT D
TMATE           =       04h         ; MAT E
TMATF           =       05h         ; MAT F
TMATG           =       06h         ; MAT G
TMATH           =       07h         ; MAT H
TMATI           =       08h         ; MAT I
TMATJ           =       09h         ; MAT J
;
;       2ND HALF OF USER LIST TOKENS
;
TL1             =       00h         ; LIST 1
TL2             =       01h         ; LIST 2
TL3             =       02h         ; LIST 3
TL4             =       03h         ; LIST 4
TL5             =       04h         ; LIST 5
TL6             =       05h         ; LIST 6

;
;       2ND HALF OF USER EQUATION TOKENS
;
;
;       "Y" EQUATIONS HAVE BIT 4 SET
;
TY1             =       10h         ; Y1
TY2             =       11h         ; Y2
TY3             =       12h         ; Y3
TY4             =       13h         ; Y4
TY5             =       14h         ; Y5
TY6             =       15h         ; Y6
TY7             =       16h         ; Y7
TY8             =       17h         ; Y8
TY9             =       18h         ; Y9
TY0             =       19H         ; Y0
;
;       PARAM EQUATIONS HAVE BIT 5 SET
;
TX1T            =       20h         ; X1t
TY1T            =       21h         ; Y1t
TX2T            =       22h         ; X2t
TY2T            =       23h         ; Y2t
TX3T            =       24h         ; X3t
TY3T            =       25h         ; Y3t
TX4T            =       26h         ; X4t
TY4T            =       27h         ; Y4t
TX5T            =       28h         ; X5t
TY5T            =       29h         ; Y5t
TX6T            =       2Ah         ; X6t
TY6T            =       2Bh         ; Y6t
;
;       POLAR EQUATIONS HAVE BIT 6 SET
;
TR1             =       40h         ; R1
TR2             =       41h         ; R2
TR3             =       42h         ; R3
TR4             =       43h         ; R4
TR5             =       44h         ; R5
TR6             =       45h         ; R6
;
;       RECURSION EQUATIONS HAVE BIT 7 SET
;
TUN             =       80h         ; Un
TVN             =       81h         ; Vn
TWN             =       82h         ; Wn
;
;       2ND HALF OF USER PICTURE TOKENS
;
TPIC1           =       00h         ; PIC1
TPIC2           =       01h         ; PIC2
TPIC3           =       02h         ; PIC3
TPIC4           =       03h         ; PIC4
TPIC5           =       04h         ; PIC5
TPIC6           =       05h         ; PIC6
TPIC7           =       06h         ; PIC7
TPIC8           =       07h         ; PIC8
TPIC9           =       08h         ; PIC9
TPIC0           =       09h         ; PIC0
;
;       2ND HALF OF USER GRAPH DATABASE TOKENS
;
TGDB1           =       00h         ; GDB1
TGDB2           =       01h         ; GDB2
TGDB3           =       02h         ; GDB3
TGDB4           =       03h         ; GDB4
TGDB5           =       04h         ; GDB5
TGDB6           =       05h         ; GDB6
TGDB7           =       06h         ; GDB7
TGDB8           =       07h         ; GDB8
TGDB9           =       08h         ; GDB9
TGDB0           =       09h         ; GDB0
;
;       2ND HALF OF STRING VARS
;
TSTR1           =       00h
TSTR2           =       01h
TSTR3           =       02h
TSTR4           =       03h
TSTR5           =       04h
TSTR6           =       05h
TSTR7           =       06h
TSTR8           =       07h
TSTR9           =       08h
TSTR0           =       09h

;
;       2ND HALF OF SYSTEM OUTPUT ONLY VARIABLES
;
;
;   OPEN        =       00H
;
TREGEQ          =       01h         ; REGRESSION EQUATION
;
TSTATN          =       02h         ; STATISTICS N
;
TXMEAN          =       03h         ; X MEAN
TSUMX           =       04h         ; SUM(X)
TSUMXSQR        =       05h         ; SUM(X^2)
TSTDX           =       06h         ; STANDARD DEV X
TSTDPX          =       07h         ; STANDARD DEV POP X
TMINX           =       08h         ; MIN X VALUE
TMAXX           =       09h         ; MAX X VALUE
;
TMINY           =       0Ah         ; MIN Y VALUE
TMAXY           =       0Bh         ; MAX Y VALUE
TYMEAN          =       0Ch         ; Y MEAN
TSUMY           =       0Dh         ; SUM(Y)
TSUMYSQR        =       0Eh         ; SUM(Y^2)
TSTDY           =       0Fh         ; STANDARD DEV Y
TSTDPY          =       10h         ; STANDARD DEV POP Y
;
TSUMXY          =       11h         ; SUM(XY)
TCORR           =       12h         ; CORRELATION
TMEDX           =       13h         ; MED(X)
TQ1             =       14h         ; 1ST QUADRANT OF X
TQ3             =       15h         ; 3RD QUADRANT OF X
TQUADa          =       16h         ; 1ST TERM OF QUAD POLY REG/ Y-INT
TQUADb          =       17h         ; 2ND TERM OF QUAD POLY REG/ SLOPE
TQUADc          =       18h         ; 3RD TERM OF QUAD POLY REG
TCUBEd          =       19h         ; 4TH TERM OF CUBIC POLY REG
TQUARTe         =       1Ah         ; 5TH TERM OF QUART POLY REG
TMEDX1          =       1Bh         ; x1 FOR MED-MED
TMEDX2          =       1Ch         ; x2 FOR MED-MED
TMEDX3          =       1Dh         ; x3 FOR MED-MED
TMEDY1          =       1Eh         ; y1 FOR MED-MED
TMEDY2          =       1Fh         ; y2 FOR MED-MED
TMEDY3          =       20h         ; y3 FOR MED-MED
;
TRECURN           =       21h       ; RECURSION N
TSTATP          =       22h
TSTATZ          =       23h
TSTATT          =       24h
TSTATCHI        =       25h
TSTATF          =       26h
TSTATDF         =       27h
TSTATPHAT       =       28h
TSTATPHAT1      =       29h
TSTATPHAT2      =       2Ah
TSTATMEANX1     =       2Bh
TSTATSTDX1      =       2Ch
TSTATN1         =       2Dh
TSTATMEANX2     =       2Eh
TSTATSTDX2      =       2Fh
TSTATN2         =       30h
TSTATSTDXP      =       31h
TSTATLOWER      =       32h
TSTATUPPER      =       33h
TSTAT_S         =       34h
TLRSQR          =       35h         ; r^2
TBRSQR          =       36h         ; R^2
;
;       these next tokens are only used to acess the data
;       they are display only and the user cannot access them at all
;
TF_DF    =     37h       ; ANOFAV FACTOR DF
TF_SS    =     38h       ; ANOFAV FACTOR SS
TF_MS    =     39h       ; ANOFAV FACTOR MS
TE_DF    =     3Ah       ; ANOFAV ERROR DF
TE_SS    =     3Bh       ; ANOFAV ERROR SS
TE_MS    =     3Ch       ; ANOFAV ERROR MS
;

;
;       2ND HALF OF SYSTEM INPUT/OUTPUT VARIABLES
;

; SYSTEM VARIABLE EQUATES
;
;       VARS WITH NO RESTRICTION
;
TUXSCL       =  0
TUYSCL       =  1
TXSCL        =  2
TYSCL        =  3
TRECURU0     =  4        ; U 1ST INITIAL COND
TRECURV0     =  5        ; V 1ST INITIAL COND
TUN1         =  6        ; U(N-1)    ; NOT USED
TVN1         =  7        ; V(N-1)    ; NOT USED
TURECURU0    =  8        ;
TURECURV0    =  9        ;
;
;       VARS ROUNDED TO 10 DIGITS
;
TXMIN        =  0AH
TXMAX        =  0BH
TYMIN        =  0CH
TYMAX        =  0DH
TTMIN        =  0EH
TTMAX        =  0FH
TTHETAMIN    =  10H
TTHETAMAX    =  11H
TUXMIN       =  12H
TUXMAX       =  13H
TUYMIN       =  14H
TUYMAX       =  15H
TUTHETMIN    =  16H
TUTHETMAX    =  17H
TUTMIN       =  18H
TUTMAX       =  19H
TTBLMIN      =  1AH
TPLOTSTART   =  1BH
TUPLOTSTART  =  1CH
TNMAX        =  1DH
TUNMAX       =  1EH
TNMIN        =  1FH
TUNMIN       =  20H
;
;       VARS CANNOT BE 0
;
TTBLSTEP     =  21H
TTSTEP       =  22H
TTHETASTEP    =  23H
TUTSTEP      =  24H
TUTHETSTEP     =  25H
;
;       SPECIAL LIMITS ON THESE
;
TDELTAX      =  26H
TDELTAY      =  27H
;
;       SPECIAL LIMITS ON THESE
;
TXFACT       =  28H
TYFACT       =  29H
;
TTBLINPUT       =       2Ah
;
;       ANY RESTRICTIONS ??????
;
TFINn           =       2Bh
TFINi           =       2Ch
TFINpv          =       2Dh
TFINpmt         =       2Eh
TFINfv          =       2Fh
TFINpy          =       30h
TFINcy          =       31h
;
TRECURW0        =       32h         ; W0(1)
TURECURW0       =       33h
;
TPLOTSTEP       =       34h
TUPLOTSTEP      =       35h
;
TXRES           =       36h
TUXRES          =       37h
;
;       ALL THE TOKENS BELOW DO NOT HAVE STRINGS ASSOCIATED
;       THEY ARE HERE JUST FOR STORING AND RECALLING
;
TRECURU02       =       38h         ; U0(2)
TURECURU02      =       39h
TRECURV02       =       3Ch         ; V0(2)
TURECURV02      =       3Dh
TRECURW02       =       3Eh         ; W0(2)
TURECURW02      =       3Fh
;
; NO RESTRICTIONS
;
;TWN1           =       40h         ; W(N-1)
;UN2           =       41h         ; U(N-2)
;VN2           =       42h         ; V(N-2)
;WN2           =       43h         ; W(N-2)
;
;
;       DON'T KNOW IF NEED THESE YET ?
;
;TUI12
;TUI22
;TUUI12
;TUUI22
;
;

TFINnpv         =       00h         ; immediate unary
TFINirr         =       01h         ; immediate unary
TFINbal         =       02h         ; immediate unary
TFINprn         =       03h         ; immediate unary
TFINint         =       04h         ; immediate unary
TFINtonom       =       05h         ; immediate unary
TFINtoeff       =       06h         ; immediate unary
TFINdbd         =       07h         ; immediate unary
TLCM            =       08h         ; immediate unary
TGCD            =       09h         ; immediate unary
TRANDINT        =       0Ah         ; immediate unary
TRANDBIN        =       0Bh         ; immediate unary
TSUBSTRNG       =       0Ch         ; immediate unary
TSTDDEV         =       0Dh         ; immediate unary
TVARIANCE       =       0Eh         ; immediate unary
TINSTRNG        =       0Fh         ; immediate unary
TDNORMAL        =       10h         ; immediate unary
TINVNORM        =       11h         ; immediate unary
TDT             =       12h         ; immediate unary
TCHI            =       13h         ; immediate unary
TDF             =       14h         ; immediate unary
TBINPDF         =       15h         ; immediate unary
TBINCDF         =       16h         ; immediate unary
TPOIPDF         =       17h         ; immediate unary
TPOICDF         =       18h         ; immediate unary
TGEOPDF         =       19h         ; immediate unary
TGEOCDF         =       1Ah         ; immediate unary
TNORMALPDF      =       1Bh         ; immediate unary
TTPDF           =       1Ch         ; immediate unary
TCHIPDF         =       1Dh         ; immediate unary
TFPDF           =       1Eh         ; immediate unary
;TRANDBIN        =       1Fh         ; immediate unary
TRANDNORM       =       1Fh         ; immediate unary
TFINFpmt        =       20h         ; 0 or multy arg function
TFINFi          =       21h         ; 0 or multy arg function
TFINFpv         =       22h         ; 0 or multy arg function
TFINFn          =       23h         ; 0 or multy arg function
TFINFfv         =       24h         ; 0 or multy arg function
TCONJ           =       25h         ; unary
TREAL           =       26h         ; unary
TIMAG           =       27h         ; unary
TANGLE          =       28h         ; unary
TCUMSUM         =       29h         ; unary
TEXPR           =       2Ah         ; unary
TLENGTH         =       2Bh         ; unary
TDELTALST       =       2Ch         ; unary
TREF            =       2Dh         ; unary
TRREF           =       2Eh         ; unary
TTORECT         =       2Fh         ; display conversion
TTOPOLAR        =       30h         ; display conversion
TCONSTE         =       31h         ; function no arg
TSINREG         =       32h         ; command opt push
TLOGISTIC       =       33h         ; command opt push
TLINREGTTEST    =       34h         ; command opt push
TSHADENORM      =       35h         ; command pushed
TSHADET         =       36h         ; command pushed
TSHADECHI       =       37h         ; command pushed
TSHADEF         =       38h         ; command pushed
TMATTOLST       =       39h         ; command pushed
TLSTTOMAT       =       3Ah         ; command pushed
TZTEST          =       3Bh         ; command pushed
TTTEST          =       3Ch         ; command pushed
T2SAMPZTEST     =       3Dh         ; command pushed
T1PROPZTEST     =       3Eh         ; command pushed
T2PROPZTEST     =       3Fh         ; command pushed
TCHITEST        =       40h         ; command pushed
TZINTVAL        =       41h         ; command pushed
T2SAMPZINT      =       42h         ; command pushed
T1PROPZINT      =       43h         ; command pushed
T2PROPZINT      =       44h         ; command pushed
TGRAPHSTYLE     =       45h         ; command pushed
T2SAMPTTEST     =       46h         ; command opt pushed
T2SAMPFTEST     =       47h         ; command opt pushed
TTINTVAL        =       48h         ; command opt pushed
T2SAMPTINT      =       49h         ; command opt pushed
TSETUPLST       =       4Ah         ; command opt pushed
TFINpmtend      =       4Bh         ; mode command
TFINpmtbeg      =       4Ch         ; mode command
TREALM          =       4Dh         ; mode command
TPOLARM         =       4Eh         ; mode command
TRECTM          =       4Fh         ; mode command
TEXPRON         =       50h         ; mode command
TEXPROFF        =       51h         ; mode command
TCLRALLLST      =       52h         ; command not pushed
TGETCALC        =       53h         ; command not pushed
TDELVAR         =       54h         ; command not pushed
TEQUTOSTRNG     =       55h         ; command not pushed
TSTRNGTOEQU     =       56h         ; command not pushed
tdellast        =       57h         ; command not pushed
TSELECT         =       58h         ; command not pushed
TANOVA          =       59h         ; command not pushed
TMODBOX         =       5Ah         ; icon
TNORMPROB       =       5Bh         ; icon
;
;       tokens 5Ch to 63h CANNOT BE THE 2ND BYTE OF
;       TWO BYTE TOKENS. THESE ARE ALREADY THE 1ST BYTE VALUE
;       OF TWO BYTE TOKENS. READING TOKENS BACKWARDS WILL NOT
;       WORK IF THIS RESTRICTION IS NOT IN PLACE.
;
;
;       NO USE  =       5Ch
;       NO USE  =       5Dh
;       NO USE  =       5Eh
;       NO USE  =       5Fh
;       NO USE  =       60h
;       NO USE  =       61h
;       NO USE  =       62h
;       NO USE  =       63h
;
TMGT            =       64h         ; VERTICAL SPLIT
TZFIT           =       65h         ; ZOOM FIT
TDIAG_ON        =       66h         ; DIANOSTIC DISPLAY ON
TDIAG_OFF       =       67h         ; DIANOSTIC DISPLAY OFF
;
;