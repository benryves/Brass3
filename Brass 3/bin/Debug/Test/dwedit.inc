; Dwedit's huge include file
; 73/82/83/83+ compatible
; Include file version 5.08
;
; Include this file at the top of your program, then include a label tTitle
; followed by the description of your program, a label lblStart, then finally
; the program code!  Like this:
;
; #include "Dwedit.inc"
; tTitle: .db "Program Description!",0
; lblStart:
;   ret ;code goes here
;
;
; Available options:  (use these before you include the file, or put them in a
; batch file)
; #DEFINE TI83 - makes a program for the TI83
; #DEFINE TI83P - makes a program for the TI83+
; #DEFINE TI82 - makes a program for the TI82
; #DEFINE TI73 - makes an APP for the TI73
;
; Supported TI83 shells:
;   #DEFINE ION
;     Yeah, it's ION... yay
;   #DEFINE VENUS
;     Venus is the best TI83 shell by far, use it!
;   #DEFINE SOS
;     For SOS, you can #DEFINE NOLIB or #DEFINE LIB to indicate whether your
;     program uses SOS libs or not.
;   #DEFINE ZES
;     Nobody uses this shell, but it's supported anyway.
;
; Supported TI83+ shells:
;   #DEFINE ION
;     For ION, you can #DEFINE NOIONLIB to not equate the ION libraries.  Use
;     this when your program exceeds 8.8k
;   #DEFINE MIRAGEOS
;     MirageOS is pretty much untested.  Should work though.
;   #DEFINE APP
;     Makes a Flash Application.  Use #DEFINE NOAPPHEADER to exclude the app
;     header from page 2 and above.
;
; Supported TI82 shells:
;   #DEFINE CRASH
;     CrASH is the more popular shell, but does NOT run on the newest TI82's.
;   #DEFINE ACE
;     ACE is newer than CrASH, and is also supported on the newest TI82's.
;
; Supported TI73 shells:
;   #DEFINE APP
;     There's no ASM for TI73 outside of apps, remember that the TI73 has
;     NO ARCHIVE!
;
;  Other options:
;    #DEFINE NOIONLIB - Don't equate the ion libs, use this when your ion
;      program exceeds 8.8k and you want to put the ion libs from ionf.z80
;      into your program yourself
;    #DEFINE NOAPPHEADER - Excludes the Flash Application header.  Use this
;      on the second and above pages of the flash application.
;    #DEFINE NOTOKENS - Excludes token equates.  Use this if you have lots
;      of name conflicts, or just don't want tokens.
;    #DEFINE TOKENS - Enables equates of tokens, 
;
;  Notes:
;    Make sure your program doesn't confilct with all the names.  There are
;    lots of reserved TI's names everywhere, especially in the tokens.
;
;    If you don't specify a shell, it will just not include any shell-specific
;    equates.  The TI82 must use a shell, and the TI73 must be an app.
;
;
.nolist
#IFNDEF NOEND
;.end
#ENDIF

#DEFINE EQU .EQU
#DEFINE equ .EQU

;No tokens by default
#IFNDEF TOKENS
#DEFINE NOTOKENS
#ENDIF



#ifdef	TI83
;88888888888 8888888        .d8888b.   .d8888b.  
;    888       888         d88P  Y88b d88P  Y88b 
;    888       888         Y88b. d88P      .d88P 
;    888       888  888888  "Y88888"      8888"  
;    888       888  888888 .d8P""Y8b.      "Y8b. 
;    888       888         888    888 888    888 
;    888       888         Y88b  d88P Y88b  d88P 
;    888     8888888        "Y8888P"   "Y8888P"

;----> Safe Memory Areas
; saferam1 = 768 bytes (apdram)
; saferam2 = 531 bytes (statram)
; saferam3 = 128 bytes (textmem)
; saferam4 = 66 bytes (ops)
; saferam5 = 10 bytes (imathptrs)
#define icall(xxxx)	call xxxx
#define bcall(xxxx)	call xxxx
#define	bcallz(xxxx)	call	z,xxxx
#define	bcallnz(xxxx)	call	nz,xxxx
#define	bcallc(xxxx)	call	c,xxxx
#define	bcallnc(xxxx)	call	nc,xxxx
#define bjump(xxxx)	jp xxxx
#define bjp(xxxx) jp xxxx
#define ijp(xxxx) jp xxxx
 .addinstr B_CALL *       CD   3 NOP 1

ApdSubTimer = $8009
ApdTimer = $800A

ptemp		=9317h
flags = $8567
_iy_table = flags

progstart	= 9327h
usermem = progstart

CURROW      =                 800Ch
CURCOL      =                 800Dh
OP1         =                 8039h
OP1M        =                 803Bh
OP2         =                 8044h
op2exp      =                 8045h
op2m        =                 8046H
OP3         =                 804Fh
OP3EXP      =                 8050h
OP3M        =                 8051h
OP3EXT      =                 8058h
OP4         =                 805Ah
OP4EXP      =                 805Bh
OP4M        =                 805Ch
OP4EXT      =                 8063h
OP5         =                 8065h
OP5EXP      =                 8066h
OP5M        =                 8067h
OP5EXT      =                 806Eh
OP6         =                 8070h
OP6EXP      =                 8071h
OP6M        =                 8072h
OP6EXT      =                 8079h
IMATHPTR1   =                 8094h
IMATHPTR2   =                 8096h
IMATHPTR3   =                 8098h
IMATHPTR4   =                 809Ah
IMATHPTR5   =                 809Ch
ASM_IND_CALL    =             80C8h
TEXTSHADOW  =                 80C9h
PENCOL      =                 8252h
PENROW      =                 8253h
SAVESSCREEN =                 8265h
usermem_offset  =             8565h
STATVARS    =                 858Fh
CURGY       =                 886Dh
CURGX       =                 886Eh
PLOTSSCREEN =                 8E29h
CMDSHADOW   =                 9157h
PROGPTR     =                 9319h
SYMTABLE    =                 0FE6Eh

vat = progptr

___bank_call       =           46EDh
___bank_entry      =           46F9h
___bank_jump       =           46F5h
___bank_ret        =           46F1h
__bank_call        =           0000h
__bank_entry       =           0000h
__bank_jump        =           0000h
__bank_ret         =           0000h
_A2POINTHLIND      =           4036h
_AbortPrgmode      =           4861h
_ABSO1O2CP         =           4162h
_ABSO1PABSO2       =           4072h
_ACOS              =           4122h
_ACOSH             =           413Ah
_ACOSRAD           =           4112h
_ADD14D            =           42DAh
_ADD16D            =           42D6h
_ADDPROP           =           42CEh
_ADDPROPLP         =           42D2h
_ADJM7             =           449Eh
_ADJMATH           =           449Ah
_ADJPARSER         =           4496h
_ADJSYMPTRS        =           44C6h
_ADRLELE           =           489Dh
_ADRMELE           =           486Dh
_ADRMROW           =           4869h
_AHEADequAL        =           5022h
_ALLEQ             =           4BACh
_ALLOCFPS          =           451Ah
_ALLOCFPS1         =           451Eh
_ANGLE             =           4152h
_ANOVA_SPEC        =           50FEh
_ANSNAME           =           503Dh
_appInit           =           405Ah
_appInitIfDec      =           405Eh
_ASIN              =           412Ah
_ASINH             =           4136h
_ASINRAD           =           411Eh
_ATAN              =           4126h
_ATAN2             =           412Eh
_ATAN2RAD          =           411Ah
_ATANH             =           4132h
_ATANRAD           =           4116h
_AutoSelect        =           4D72h
_Axes              =           519Eh
_BC2NONREAL        =           4E74h
_BINOPEXEC         =           48E5h
_BLINKGCUR         =           4BBCh
_BRKINC            =           5097h
_bufClear          =           4CAEh
_bufDelete         =           4C7Eh
_bufInsert         =           4C72h
_bufLeft           =           4C6Ah
_BUFPEEK           =           4C82h
_BUFPEEK1          =           4C86h
_BUFPEEK2          =           4C8Ah
_BUFPEEK3          =           4C8Eh
_bufQueueChar      =           4C76h
_bufReplace        =           4C7Ah
_BUFRIGHT          =           4C6Eh
_BufToBtm          =           4C92h
_bufToTop          =           4C9Ah
_CallCommon        =           4A30h
_callMain          =           4052h
_canIndic          =           4D02h
_CATALOGCHK        =           4E64h
_Ceiling           =           4BE0h
_CentCursor        =           510Ah
_CheckTOP          =           4AF4h
_CheckXY           =           4AF8h
_CHK_HL_999        =           43E6h
_CHKDEL            =           448Eh
_CHKDELA           =           4492h
_CHKERRBREAK       =           43BAh
_CHKFINDSYM        =           442Ah
_CHKTEMPDIRT       =           43AEh
_CHKTEXTCURS       =           4BC8h
_CIRCCMD           =           4AD4h
_CKENDEXP          =           4FF5h
_CKENDLIN          =           4FECh
_CKENDLINERR       =           4EA4h
_CKFETCHVAR        =           4FCDh
_CKINT             =           4322h
_CKODD             =           4326h
_CKOP1C0           =           430Eh
_CKOP1CPLX         =           414Ah
_CKOP1FP0          =           4312h
_CKOP1M            =           432Ah
_CKOP1POS          =           435Ah
_CKOP1REAL         =           414Eh
_CKOP2FP0          =           4316h
_CKOP2POS          =           4356h
_CKOP2REAL         =           4412h
_CKPARSEND         =           4FFEh
_CKPOSINT          =           431Eh
_CKVALDELTA        =           5152h
_CKVALDELX         =           5149h
_CKVALIDNUM        =           437Ah
_CLEANALL          =           4E2Ah
_CLine             =           4A84h
_CLineS            =           4A88h
_closeEditBuf      =           4C2Bh
_CloseEditBufNo    =           4A4Dh
_closeEditEqu      =           4CF2h
_closeEquField     =           4D6Eh
_CLOSEPROG         =           4E06h
_CLRGRAPHREF       =           4E0Ah
_CLRGRAPHXY        =           5176h
_clrLCD            =           4759h
_CLRLCDFULL        =           4755h
_CLRLP             =           429Eh
_CLROP1S           =           4362h
_CLROP2S           =           435Eh
_CLRPREVPLOT       =           4ABCh
_clrScrn           =           4761h
_clrScrnFull       =           475Dh
_clrTR             =           4E68h
_CLRTXTSHD         =           4765h
_clrWindow         =           4769h
_CMATFUN           =           4891h
_CMP_NUM_INIT      =           48E1h
_CMP_STATPTR       =           4EB0h
_CMPBOXINFO        =           4AB0h
_CMPMEMNEED        =           443Eh
_CMPPRGNAMLEN      =           4422h
_CMPPRGNAMLEN1     =           441Eh
_CMPSYMS           =           4E22h
_COMMAERR          =           4F97h
_COMMAERRF         =           4F8Eh
_CommonKeys        =           4A34h
_CONVDIM           =           5010h
_CONVDIM00         =           5019h
_CONVKEYTOTOK      =           4DBEh
_CONVLCTOLR        =           4DEEh
_CONVLRTOLC        =           4E32h
_CONVOP1           =           4EFCh
_COORDISP          =           4B2Ch
_coorMon           =           4018h
_COP1SET0          =           4156h
_COPYRNG           =           4B40h
_COS               =           40FAh
_COSH              =           410Ah
_cphlde            =           4004h
_CPointS           =           4B00h
_CPOP1OP2          =           4166h
_CPOP4OP3          =           415Ah
_CPTDELX           =           4B60h
_CPTDELY           =           4B5Ch
_CPYCTO1FPS3       =           459Ah
_CPYFPS1           =           45DAh
_CPYFPS2           =           45E6h
_CPYFPS3           =           45A2h
_CPYFPST           =           45C6h
_CPYO1TOES1        =           49B0h
_CPYO1TOES10       =           49F8h
_CPYO1TOES11       =           49FCh
_CPYO1TOES12       =           4A00h
_CPYO1TOES13       =           4A04h
_CPYO1TOES14       =           4A08h
_CPYO1TOES15       =           4A0Ch
_CPYO1TOES2        =           49BCh
_CPYO1TOES3        =           49C4h
_CPYO1TOES4        =           49C8h
_CPYO1TOES5        =           49CCh
_CPYO1TOES6        =           49D0h
_CPYO1TOES7        =           49D4h
_CPYO1TOES8        =           49F0h
_CPYO1TOES9        =           49F4h
_CPYO1TOEST        =           49ACh
_CPYO1TOFPS1       =           460Eh
_CPYO1TOFPS2       =           4622h
_CPYO1TOFPS3       =           4632h
_CPYO1TOFPS4       =           464Ah
_CPYO1TOFPS5       =           4642h
_CPYO1TOFPS6       =           463Ah
_CPYO1TOFPS7       =           463Eh
_CPYO1TOFPST       =           45F6h
_CPYO2TOES2        =           49C0h
_CPYO2TOES4        =           49D8h
_CPYO2TOES5        =           49DCh
_CPYO2TOES6        =           49E0h
_CPYO2TOES7        =           49E4h
_CPYO2TOES8        =           49E8h
_CPYO2TOES9        =           49ECh
_CPYO2TOFPS1       =           460Ah
_CPYO2TOFPS2       =           4616h
_CPYO2TOFPS3       =           462Eh
_CPYO2TOFPS4       =           4646h
_CPYO2TOFPST       =           45EEh
_CPYO3TOFPS1       =           4602h
_CPYO3TOFPS2       =           461Ah
_CPYO3TOFPST       =           45EAh
_CPYO5TOFPS1       =           4606h
_CPYO5TOFPS3       =           462Ah
_CPYO6TOES1        =           49B4h
_CPYO6TOES3        =           49B8h
_CPYO6TOFPS2       =           461Eh
_CPYO6TOFPST       =           45F2h
_CPYSTACK          =           45CAh
_CPYTO1ES1         =           4914h
_CPYTO1ES10        =           4978h
_CPYTO1ES11        =           4980h
_CPYTO1ES12        =           4988h
_CPYTO1ES13        =           4990h
_CPYTO1ES14        =           4994h
_CPYTO1ES15        =           49A4h
_CPYTO1ES16        =           4998h
_CPYTO1ES17        =           499Ch
_CPYTO1ES18        =           49A0h
_CPYTO1ES2         =           4924h
_CPYTO1ES3         =           492Ch
_CPYTO1ES4         =           493Ch
_CPYTO1ES5         =           4944h
_CPYTO1ES6         =           4958h
_CPYTO1ES7         =           4960h
_CPYTO1ES8         =           4968h
_CPYTO1ES9         =           496Ch
_CPYTO1EST         =           4950h
_CPYTO1FPS1        =           45D6h
_CPYTO1FPS10       =           4582h
_CPYTO1FPS11       =           455Eh
_CPYTO1FPS2        =           45E2h
_CPYTO1FPS3        =           459Eh
_CPYTO1FPS4        =           45A6h
_CPYTO1FPS5        =           4566h
_CPYTO1FPS6        =           456Eh
_CPYTO1FPS7        =           4576h
_CPYTO1FPS8        =           457Ah
_CPYTO1FPS9        =           4586h
_CPYTO1FPST        =           45C2h
_CPYTO2ES1         =           490Ch
_CPYTO2ES10        =           4974h
_CPYTO2ES11        =           497Ch
_CPYTO2ES12        =           4984h
_CPYTO2ES13        =           498Ch
_CPYTO2ES15        =           49A8h
_CPYTO2ES2         =           4920h
_CPYTO2ES3         =           4928h
_CPYTO2ES4         =           4938h
_CPYTO2ES5         =           4940h
_CPYTO2ES6         =           4954h
_CPYTO2ES7         =           495Ch
_CPYTO2ES8         =           4964h
_CPYTO2ES9         =           4970h
_CPYTO2EST         =           494Ch
_CPYTO2FPS1        =           45D2h
_CPYTO2FPS2        =           45DEh
_CPYTO2FPS3        =           4596h
_CPYTO2FPS4        =           458Ah
_CPYTO2FPS5        =           4562h
_CPYTO2FPS6        =           456Ah
_CPYTO2FPS7        =           4572h
_CPYTO2FPS8        =           457Eh
_CPYTO2FPST        =           45BEh
_CPYTO3ES1         =           4918h
_CPYTO3ES2         =           491Ch
_CPYTO3ES4         =           4930h
_CPYTO3FPS1        =           45CEh
_CPYTO3FPS2        =           45AAh
_CPYTO3FPST        =           45BAh
_CPYTO4EST         =           4948h
_CPYTO4FPST        =           45B6h
_CPYTO5FPST        =           45AEh
_CPYTO6ES1         =           4910h
_CPYTO6ES3         =           4934h
_CPYTO6FPS2        =           4592h
_CPYTO6FPS3        =           458Eh
_CPYTO6FPST        =           45B2h
_CPYTOFPS1         =           4612h
_CPYTOFPS2         =           4626h
_CPYTOFPS3         =           4636h
_CPYTOFPST         =           45FAh
_CPYTOSTACK        =           45FEh
_CREATE0equ        =           4476h
_CREATECLIST       =           4462h
_CREATECPLX        =           444Eh
_CREATEequ         =           447Eh
_CREATEGDB         =           4486h
_createNumEditBUF  =           4A2Ch
_CREATEPAIR        =           4F6Ah
_CREATEPICT        =           4482h
_CREATEPROG        =           448Ah
_CREATEPVAR3       =           4446h
_CREATEPVAR4       =           4442h
_CREATEREAL        =           4452h
_CREATERLIST       =           445Ah
_CREATERMAT        =           446Ah
_CREATESTRNG       =           4472h
_CREATETCLIST      =           445Eh
_CREATETEMP        =           4E26h
_CREATETequ        =           447Ah
_CREATETLIST       =           48F5h
_CREATETRLIST      =           4456h
_CREATETRMAT       =           4466h
_CREATETSTRNG      =           446Eh
_CREATEVAR3        =           444Ah
_CUBE              =           409Eh
_CUMSUM            =           48D1h
_curBlink          =           47F9h
_CURFETCH          =           50A9h
_CurNameLength     =           4C5Fh
_cursorDown        =           4CC2h
_cursorLeft        =           4CB8h
_CURSOROFF         =           47FDh
_cursorOn          =           4805h
_cursorToOffset    =           4CC6h
_DARKLINE          =           4AE0h
_DARKPNT           =           4AFCh
_DATASIZE          =           44CEh
_DATASIZEA         =           44CAh
_DEALLOCFPS        =           4512h
_DEALLOCFPS1       =           4516h
_DECO1EXP          =           436Eh
_dellistel         =           4DFEh
_DELMEM            =           44B2h
_DELMEMA           =           44A2h
_DELRES            =           4DEAh
_DELVAR            =           44AAh
_DELVAR3C          =           44BAh
_DELVAR3D          =           44B6h
_DELVAR3DC         =           44BEh
_DELVARIO          =           44AEh
_DFMIN2            =           4D0Ah
_disarmScroll      =           4D3Eh
_DISP_INTERVAL     =           4D5Eh
_dispDone          =           47F1h
_dispEOL           =           47DDh
_dispEOW           =           4CD6h
_dispErrorScreeN   =           4D8Eh
_dispForward       =           4D82h
_dispHead          =           4CDAh
_dispHL            =           4709h
_DispLAlphaName    =           485Dh
_dispListName      =           484Dh
_dispListNameY     =           4C5Bh
_DisplstName       =           4D62h
_dispNumEOS        =           4D7Ah
_DISPOP1A          =           51D4h
_dispSLstNameHL    =           4D66h
_dispTail          =           4CDEh
_dispTblbody       =           4C4Fh
_dispTblBot        =           4C47h
_DispTblTop        =           4C4Bh
_dispVarVal        =           4A20h
_DISPYEOS          =           4D76h
_DispYPrompt2      =           4D86h
_DIST_FUN          =           50E6h
_divHLby10         =           4008h
_divHLbyA          =           400Ch
_DOREFFLAGS02      =           4BD0h
_DRAWCMD           =           4C13h
_DRAWCMD_INIT      =           4C0Fh
_DRAWSPLITLINE     =           4BA0h
_DRAWSPLOT         =           4A9Ch
_DspCurTbl         =           4C37h
_DSPGRTBL          =           4C3Bh
_dsplsts           =           4C27h
_dspmattable       =           4C23h
_DTOR              =           4096h
_EditEqu           =           4D6Ah
_EDITPROG          =           4E02h
_ENDDRAW           =           51E6h
_ENOUGHMEM         =           443Ah
_EOP1NOTREAL       =           4386h
_EQSELUNSEL        =           50CEh
_equ_or_NEWequ     =           43EAh
_eraseEOL          =           476Dh
_eraseEOW          =           4771h
_errArgument       =           4672h
_errBadGuess       =           4690h
_ERRBREAK          =           4684h
_ERRD_OP1_0        =           4402h
_ERRD_OP1_LE_0     =           43FEh
_ERRD_OP1NOT_R     =           43F6h
_ERRD_OP1NOTPOS    =           43F2h
_ERRD_OP1NOTPOSINT =           43FAh
_ERRDATATYPE       =           466Fh
_ERRDIMENSION      =           4678h
_errDimMismatch    =           4675h
_ERRDIVBY0         =           465Dh
_ERRDOMAIN         =           4663h
_ERREND            =           4F85h
_errIncrement      =           4666h
_ERRINVALID        =           4681h
_errIterations     =           468Dh
_errLinkXmit       =           4699h
_ERRMEMORY         =           467Eh
_ERRNON_REAL       =           4669h
_ERRNONREAL        =           4E78h
_ERRNOTENOUGHMEM   =           464Eh
_ErrorEP           =           404Eh
_ERROVERFLOW       =           465Ah
_errSignChange     =           468Ah
_errSingularMat    =           4660h
_errStat           =           4687h
_ERRSTATPLOT       =           4696h
_errSyntax         =           466Ch
_errTolTooSmall    =           4693h
_ERRUNDEFINED      =           467Bh
_ETOX              =           40EAh
_EVALF3A           =           4A10h
_EXCH9             =           455Ah
_EXEC_ASSEMBLY     =           5102h
_EXLP              =           430Ah
_EXMCPLXO1         =           4556h
_EXMEAN1           =           48E9h
_EXPTOHEX          =           434Eh
_ExTest_INT        =           50E2h
_FACTORIAL         =           50C6h
_fClear            =           4A3Eh
_fDel              =           4A3Ah
_FDISPBOL          =           4CD2h
_FDISPBOL1         =           4CCEh
_fdispEOL          =           47E1h
_FDIV10            =           436Ah
_FDIV100           =           4366h
_FETCHVAR          =           4FE3h
_FETCHVARA         =           4FD6h
_FIND_E_UNDEF      =           507Ch
_Find_Parse_ForMULA  =           4F04h
_FindAlphadn       =           4E1Eh
_FindAlphaup       =           4E1Ah
_FINDEOL           =           508Eh
_FINDPROGSYM       =           4426h
_FINDSYM           =           442Eh
_FINDSYM_GET_SIZE  =           4406h
_finishoutput      =           47F5h
_FINISHSPEC        =           511Ch
_finsDisp          =           4A42h
_FinsDisp02        =           4A45h
_FIVEEXEC          =           4908h
_FIXTEMPCNT        =           4E0Eh
_fndallseleq       =           4BB0h
_FNDDB             =           4BA8h
_FNDSELEQ          =           516Dh
_formDCplx         =           4D2Eh
_formDisp          =           4D0Eh
_formEReal         =           4D26h
_formERealTOK      =           4D2Ah
_formMatrix        =           4D12h
_formReal          =           4D32h
_formScrollUp      =           4D36h
_FORMTOTOK         =           4D5Ah
_FORSEQINIT        =           4E80h
_FOUREXEC          =           4904h
_FPADD             =           4092h
_FPDIV             =           40C6h
_FPMULT            =           40AAh
_FPOPCPLX          =           4506h
_FPOPFPS           =           450Eh
_FPOPREAL          =           450Ah
_FPRECIP           =           40C2h
_FPSMINUS9         =           4652h
_FPSQUARE          =           40A6h
_FPSUB             =           408Eh
_FRAC              =           40BEh
_GDISPTOKEN        =           4B20h
_GEQNAMEA          =           46E5h
_GET_FORM_NUM      =           44A6h
_GET_INDEX_LST     =           4AC4h
_GET_PLUS1_SAVE    =           43EEh
_GET_TOK_STRNG     =           47C5h
_GET_X_INDIRECT    =           4416h
_GETCON            =           4332h
_GETCON1           =           432Eh
_getcsc            =           4014h
_getcurloc         =           4779h
_GETDEPTR          =           504Fh
_GETEXP            =           437Eh
_GetK              =           4A18h
_getkey            =           4CFEh
_GETKEYPRESS       =           47BDh
_GETL1TOOP1        =           48A1h
_GETL1TOOP2        =           48ADh
_GETL1TOP1A        =           48A5h
_GETL1TOP2A        =           48B1h
_GETL2TOP1A        =           48B5h
_GETLASTENTRY      =           43C2h
_GETLASTENTRYPTR   =           43C6h
_GETLTOOP1         =           48A9h
_GETM1TOOP1        =           4875h
_GETM1TOP1A        =           4879h
_GETMATOP1A        =           4871h
_GETMTOOP1         =           487Dh
_GETNUMLET         =           4F15h
_GETPARSE          =           4E98h
_GETPREVTOK        =           4CFAh
_GETSTATPTR        =           4EACh
_GETTOKLEN         =           47C1h
_GETTOKSTRING      =           47C9h
_GETVARCMD         =           4DD6h
_GRAPHQUAD         =           4E70h
_GRBUFCLR          =           515Bh
_GRBUFCPY          =           4B9Ch
_GRBUFCPY_V        =           5164h
_GRDECODA          =           4B24h
_GRITSOLVER        =           50D6h
_GRLABELS          =           4B34h
_GRPHCIRC          =           4AD8h
_GRPHPARS          =           4E84h
_GRPPutAway        =           5140h
_GRPUTAWAY         =           4B48h
_GrRedisp          =           4B1Ch
_GRReset           =           4B50h
_HEAP_SORT         =           4AC8h
_hideCursor        =           4801h
_HLMINUS9          =           4656h
_HLTIMES9          =           4146h
_homeUp            =           4775h
_HORIZCMD          =           4BEFh
_HTIMESL           =           4382h
_ILINE             =           4AE4h
_INCCURPCERREND    =           4F7Ch
_INCEXP            =           4376h
_INCFETCH          =           50A0h
_INCLSTSIZE        =           4DF6h
_INCO1EXP          =           4372h
_INIT_PROG_LIST    =           4F1Eh
_initialize        =           4062h
_initNamePrompt    =           4E5Ch
_INITNEWTRACEP     =           4AA0h
_INITNSEQ          =           4BD4h
_InitNumVec        =           4A70h
_InitPFlgs         =           4EA0h
_INITSTATANS2      =           50FAh
_INPARG            =           4FB2h
_insDisp           =           4CCAh
_insertlist        =           4DFAh
_INSERTMEM         =           4432h
_INSERTMEMA        =           4436h
_INT               =           40D6h
_INTGR             =           4076h
_INVCMD            =           4C1Bh
_INVOP1S           =           40B6h
_INVOP1SC          =           40B2h
_INVOP2S           =           40BAh
_INVSUB            =           407Eh
_IORESETALL        =           4DE6h
_IPOINT            =           4AE8h
_IS_A_LSTorCLST    =           43E2h
_IS_FULLCNTX       =           4865h
_ISA2BYTETOK       =           43BEh
_isaletter         =           4F00h
_isAtBtm           =           4CAAh
_ISATTOP           =           4CA6h
_isEditEmpty       =           4CA2h
_isEditFull        =           4C9Eh
_isInSet           =           46B5h
_ISO1NONTEMPLST    =           43DEh
_ISO1NONTLSTorPROG =           43DAh
_itemName          =           4DB2h
_ITSOLVER          =           50D2h
_ITSOLVERB         =           50DAh
_ITSOLVERNB        =           50DEh
_JcursorFirst      =           4CB2h
_JcursorLast       =           4CB5h
_JcursorRight      =           4CBCh
_JcursorUp         =           4CBFh
_JError            =           469Ch
_JErrorNo          =           469Fh
_JforceCmd         =           4027h
_JforceCmdNoChaR   =           4024h
_jpromptcursor     =           4C67h
_kbdScan           =           4010h
_KEYSCNLNK         =           4DE2h
_KEYTOSTRING       =           480Dh
_KEYTOTOK2BYTE     =           4DC6h
_KEYTOTOKNEW2B     =           4DC2h
_LABCOOR           =           4B28h
_lcd_busy          =           4066h
_LCD_DRIVERON      =           4D06h
_ldHLind           =           4000h
_Leftmore          =           4A37h
_LHIGH             =           48C9h
_LINECMD           =           4BF7h
_LJNORND           =           40E6h
_LJRND             =           40AEh
_LLOW              =           48C5h
_LNX               =           40DEh
_LOAD_LFONT        =           4DCEh
_LOAD_SFONT        =           4A68h
_loadCurCat        =           4821h
_LoadMenuNum       =           4829h
_LoadMenuNumL      =           482Dh
_loadnoeentry      =           4D96h
_LOGGAMMA          =           50EAh
_LOGX              =           40E2h
_LSUM              =           48CDh
_MAKEROWCMD        =           47E5h
_MANTPA            =           42CAh
_MAT_EL_DIV        =           488Dh
_MATHTANLN         =           51DDh
_MAX               =           406Eh
_MAXMINLST         =           48C1h
_MEMCHK            =           441Ah
_MenCatRet         =           4835h
_MenuCatCommon     =           4815h
_MenuEdKey         =           4831h
_MIN               =           406Ah
_MinToEdit         =           4D46h
_MINUS1            =           408Ah
_mon               =           401Bh
_monErrHand        =           4056h
_monForceKey       =           401Eh
_MOV10B            =           41CAh
_MOV14             =           41DEh
_MOV18             =           41D2h
_MOV18B            =           4ADCh
_MOV2B             =           41F2h
_MOV3B             =           41EEh
_MOV4B             =           41EAh
_MOV5B             =           41E6h
_MOV6B             =           41E2h
_MOV7B             =           41DAh
_MOV8B             =           41D6h
_MOV9B             =           41CEh
_MOV9OP1OP2        =           420Ah
_MOV9OP2CP         =           415Eh
_MOV9TOOP1         =           4206h
_MOV9TOOP2         =           420Eh
_MOVCPLX           =           44DAh
_moveDown          =           4741h
_MOVETONEXTSYM     =           4E2Eh
_moveUp            =           474Dh
_MOVFROP1          =           4212h
_MOVREG            =           41C6h
_NamePrompt2       =           4E60h
_NameToBuf         =           4C63h
_nameToOP1         =           4A59h
_NBCURSOR          =           4BC0h
_NCifprgmedmode    =           4825h
_newContext        =           402Eh
_newContext0       =           4032h
_NEWINDEP          =           5195h
_newline           =           473Dh
_NEXT_Y_STYLE      =           517Fh
_NEXTEQ            =           4BB4h
_NEXTPLOT          =           4AB4h
_noErrorEntry      =           46A2h
_notalphnum        =           4839h
_numError02        =           4A65h
_numPPutAway       =           4A5Dh
_numRedisp         =           4A61h
_NXTFETCH          =           4FC4h
_ONEVAR            =           50EEh
_ONEVARS_0         =           50F2h
_OP1EXOP2          =           4306h
_OP1EXOP3          =           42FEh
_OP1EXOP4          =           4302h
_OP1EXOP5          =           42EEh
_OP1EXOP6          =           42F2h
_OP1EXPMDE         =           43B6h
_OP1EXPTODEC       =           4352h
_OP1MOP2EXP        =           43B2h
_OP1SET0           =           4286h
_OP1SET1           =           4232h
_OP1SET2           =           4242h
_OP1SET3           =           423Ah
_OP1SET4           =           4236h
_OP1toEdit         =           4D42h
_OP1TOOP2          =           418Eh
_OP1TOOP3          =           417Eh
_OP1TOOP4          =           416Eh
_OP1TOOP5          =           41BEh
_OP1TOOP6          =           41BAh
_OP2EXOP4          =           42F6h
_OP2EXOP5          =           42FAh
_OP2EXOP6          =           42E6h
_OP2SET0           =           4282h
_OP2SET1           =           4252h
_OP2SET2           =           4246h
_OP2SET3           =           422Eh
_OP2SET4           =           422Ah
_OP2SET5           =           4222h
_OP2SET60          =           4EA8h
_OP2SET8           =           421Eh
_OP2SETA           =           4226h
_OP2TOOP1          =           41C2h
_OP2TOOP3          =           41F6h
_OP2TOOP4          =           4172h
_OP2TOOP5          =           41B2h
_OP2TOOP6          =           41B6h
_OP3SET0           =           427Eh
_OP3SET1           =           421Ah
_OP3SET2           =           423Eh
_OP3TOOP1          =           41A2h
_OP3TOOP2          =           417Ah
_OP3TOOP4          =           416Ah
_OP3TOOP5          =           41AEh
_OP4SET0           =           427Ah
_OP4SET1           =           4216h
_OP4TOOP1          =           419Ah
_OP4TOOP2          =           4176h
_OP4TOOP3          =           41FAh
_OP4TOOP5          =           41AAh
_OP4TOOP6          =           4202h
_OP5EXOP6          =           42EAh
_OP5SET0           =           4276h
_OP5TOOP1          =           419Eh
_OP5TOOP2          =           4182h
_OP5TOOP3          =           41FEh
_OP5TOOP4          =           418Ah
_OP5TOOP6          =           4186h
_OP6TOOP1          =           4196h
_OP6TOOP2          =           4192h
_OP6TOOP5          =           41A6h
_ORDSTAT           =           50F6h
_outputExpr        =           5106h
_PARSAHEAD         =           5034h
_PARSAHEADS        =           502Bh
_PARSE_FORMULA     =           4F08h
_PARSECMD          =           4F5Ah
_parseEditBuf      =           4C2Fh
_PARSEinp          =           4E8Ch
_PARSELOOP         =           4F42h
_PARSEOFF          =           4E90h
_PARSEON           =           4F52h
_PARSEONC          =           4F4Bh
_PARSESCAN         =           4E94h
_PDspGrph          =           4BEBh
_PICON             =           4342h
_PIDIV2            =           4336h
_PIDIV2A           =           434Ah
_PIDIV4            =           433Ah
_PIDIV4A           =           4346h
_PIXELCMD          =           4C07h
_PIXELTEST         =           4C03h
_PLOTPARS          =           4E88h
_PLOTPT            =           518Ch
_PLOTPTXY2         =           4BDCh
_PLUS1             =           4086h
_POINT_STAT_HLP    =           4A98h
_POINTCMD          =           4BFFh
_POINTHLIND        =           403Ah
_POP2BOPER         =           5061h
_POPCX             =           4D92h
_popErrorHandleR   =           46A9h
_POPMCPLX          =           44D6h
_POPMCPLXO1        =           44D2h
_POPOP1            =           44E6h
_POPOP3            =           44E2h
_POPOP5            =           44DEh
_POPOPER           =           5073h
_POPREAL           =           4502h
_POPREALO1         =           44FEh
_POPREALO2         =           44FAh
_POPREALO3         =           44F6h
_POPREALO4         =           44F2h
_POPREALO5         =           44EEh
_POPREALO6         =           44EAh
_POSNO0INT         =           431Ah
_PPutAway          =           4042h
_PPutAwayPrompt    =           403Eh
_PREVEQ            =           4BB8h
_PREVPLOT          =           4AB8h
_PRGRDLP           =           4F27h
_PRINTSCREEN       =           4DDEh
_PTOR              =           413Eh
_PULLDOWNCHK       =           4811h
_PUSH2BOPER        =           5058h
_pushErrorHandlER  =           46A5h
_PUSHMCPLX         =           4552h
_PUSHMCPLXO1       =           454Eh
_PUSHMCPLXO3       =           4546h
_PUSHNUM           =           4F73h
_PUSHOP1           =           454Ah
_PUSHOP3           =           4542h
_PUSHOP5           =           453Eh
_PUSHOPER          =           506Ah
_PUSHREAL          =           453Ah
_PUSHREALO1        =           4536h
_PUSHREALO2        =           4532h
_PUSHREALO3        =           452Eh
_PUSHREALO4        =           452Ah
_PUSHREALO5        =           4526h
_PUSHREALO6        =           4522h
_PUT_INDEX_LST     =           4AC0h
_putAns            =           51CBh
_PutAway           =           4046h
_putbPAT           =           47D5h
_PUTBPATBUF        =           47D1h
_PUTBPATBUF2       =           47CDh
_putbuf            =           471Dh
_putbuf1           =           4721h
_putc              =           4705h
_putcCheckScrolL   =           47D9h
_PUTequNO          =           4BE7h
_putmap            =           4701h
_putps             =           4715h
_putpsb            =           4711h
_puts              =           470Dh
_putsm             =           4C33h
_PutTokString      =           4CE2h
_PUTTOL            =           48BDh
_PUTTOLA1          =           48B9h
_PUTTOM1A          =           4881h
_PUTTOMA1          =           4885h
_PUTTOMAT          =           4889h
_PUTXY             =           4BE4h
_QUAD              =           4E6Ch
_RandInit          =           50BEh
_RANDOM            =           50B6h
_RCL_SEQ_X         =           512Eh
_RCL_STATVAR       =           440Eh
_RCLANS            =           4EDCh
_RCLENTRYTOEDIT    =           4D52h
_RCLGDB2           =           4AD0h
_RCLKEY            =           4E52h
_rclkeyRtn         =           4E4Eh
_RCLN              =           4EE4h
_RCLREGEQ          =           4E59h
_RCLREGEQ_CALL     =           4E55h
_RCLSEQ2           =           5137h
_RCLSTAT           =           4F39h
_RCLSYSTOK         =           4EF0h
_rclToQueue        =           4D56h
_RCLVARSYM         =           4EECh
_rclVarToEdit      =           4D4Ah
_rclVarToEditPtR   =           4D4Eh
_RCLX              =           4EE8h
_RCLY              =           4EE0h
_READDISP2         =           46FDh
_REALNAME          =           43A6h
_RecallEd          =           4A24h
_RECNAME           =           46E9h
_RECURNNAME        =           4396h
_REDIMMAT          =           4DF2h
_REGEQNAME         =           4392h
_REGRAPH           =           4BCCh
_REGRCLRCHNG       =           43CAh
_releaseBuffer     =           4A51h
_resetStacks       =           50C2h
_RESETWINTOP       =           43CEh
_RESTOREDATA       =           4E16h
_RestoreDisp       =           4BA4h
_RESTOREERRNO      =           4901h
_restoreTR         =           47B9h
_RETSCREEN         =           4D9Eh
_RetScreenErr      =           4DA2h
_RNAME             =           438Eh
_RNDGUARD          =           40CEh
_RNFX              =           40D2h
_ROUND             =           40DAh
_ROWECH_POLY       =           4895h
_ROWECHELON        =           4899h
_RSTGFLAGS         =           4B4Ch
_rstrbotrow        =           47B1h
_rstrCurRow        =           47A9h
_RstrOScreen       =           4855h
_rstrpartial       =           47A5h
_rstrShadow        =           47A1h
_RstrSmallText     =           4849h
_RstrSomeFlags     =           4845h
_rstrUnderMenu     =           47ADh
_RTOD              =           409Ah
_RTOP              =           4142h
_runIndicOff       =           4795h
_runIndicOn        =           4791h
_saveCmdShadow     =           4799h
_SAVEDATA          =           4E12h
_SaveOscreen       =           4859h
_SAVEPARSE         =           4E9Ch
_SaveSavedFlags    =           483Dh
_SaveScreen        =           4D9Ah
_saveShadow        =           479Dh
_saveTR            =           47B5h
_SCREENUP          =           4E3Eh
_SCREENUPDOWN      =           4E42h
_scrollDown        =           4751h
_scrollUp          =           4745h
_sDone             =           46B9h
_SeeIfErrorCx      =           4851h
_SEND1BERR         =           4DD2h
_sendKPress        =           4021h
_SENDVARCMD        =           4DDAh
_seqn              =           46D5h
_SEQSET            =           48D9h
_SEQSOLVE          =           48DDh
_serrort           =           46BDh
_SET14A            =           426Eh
_SET14D            =           4272h
_SET16A            =           426Ah
_SET2MVLPTRS       =           48EDh
_setEmptyEditEqU   =           4CEAh
_setEmptyEditPtR   =           4CEEh
_SETesTOfps        =           43AAh
_SETFUNCM          =           4B64h
_setIndicator      =           4A49h
_SETMANT           =           4262h
_SETMANT1          =           424Eh
_SETMAT1           =           48F1h
_SetMenuFlags      =           4841h
_SETNORM_VALS      =           4DB6h
_SETNUM            =           425Ah
_SETNUM0           =           428Ah
_SETNUM1           =           4256h
_SETNUM2           =           424Ah
_SETNUMA           =           425Eh
_setNumWindow      =           4739h
_SETPARM           =           4B70h
_setPenT           =           51B9h
_setPenX           =           51A7h
_setPenY           =           51B0h
_SETPOLM           =           4B6Ch
_SETSEQM           =           4B68h
_settblrefs        =           4C43h
_setTitlE          =           4A1Ch
_setupBuffer       =           4A28h
_setupdispeq       =           4D7Eh
_setupEditCmd      =           4CE6h
_setupEditEqu      =           4C96h
_SETUZM            =           4B84h
_SETVARNAME        =           47EDh
_SETVERT_VALS      =           4DBAh
_setwinabove       =           4D3Ah
_SetXMinMax        =           4B94h
_SETXUP            =           43D6h
_SETXXOP1          =           4A74h
_SETXXOP2          =           4A78h
_SETXXXXOP2        =           4A7Ch
_SETYUP            =           43D2h
_sFAIL             =           46C9h
_SFONT_LEN         =           4A6Ch
_SHADECMD          =           4C17h
_SHL14             =           42BEh
_SHL16             =           42BAh
_SHLACC            =           42A6h
_showCursor        =           4809h
_SHR14             =           42B6h
_SHR16             =           42B2h
_SHR18             =           42AAh
_SHR18A            =           42AEh
_SHRACC            =           42A2h
_SHRDRND           =           42C6h
_shrinkWindow      =           4749h
_SIN               =           40F6h
_SINCOSRAD         =           40F2h
_SINH              =           410Eh
_SINHCOSH          =           4102h
_SizeWind          =           404Ah
_sName             =           46CDh
_sNameEq           =           46C1h
_sOK               =           46D1h
_SolveDisp         =           4DAEh
_SolveRedisp       =           4DAAh
_SplitChange       =           4DA6h
_SPLOTCOORD        =           4AA4h
_SPLOTLEFT         =           4AACh
_SPLOTRIGHT        =           4AA8h
_SQROOT            =           40CAh
_SRDO1             =           42C2h
_SRECURN           =           46E1h
_Sselect           =           46D9h
_STATMARK          =           4BC4h
_STATSHADE         =           4C1Fh
_STEQARG           =           4FA9h
_STEQARG2          =           4FA0h
_STEQARG3          =           4FBBh
_STLSTVECEL        =           4EF8h
_StMatEl           =           4EF4h
_STO_STATVAR       =           440Ah
_STOANS            =           4EBCh
_STOCMPREALS       =           5046h
_STOGDB2           =           4ACCh
_STON              =           4ECCh
_STOOTHER          =           4ED8h
_STOR              =           4EC4h
_STORAND           =           50BAh
_STOSYSTOK         =           4EB8h
_STOT              =           4ED0h
_STOTHETA          =           4EC0h
_STOTYPE           =           4F61h
_STOTYPEARG        =           5007h
_STOX              =           4ED4h
_STOY              =           4EC8h
_STransmit         =           46DDh
_strCat            =           46B1h
_strCopy           =           46ADh
_stringwidth       =           4D8Ah
_STRNGENT1         =           4F0Ch
_STTMPEQ           =           5085h
_SUB14D            =           42E2h
_SUB16D            =           42DEh
_sUnderScr         =           46C5h
_SYM_PROG_NON_T_LST =           44C2h
_sysErrHandler     =           402Ah
_TAN               =           40FEh
_TAN_equ_DISP      =           51C2h
_TANH              =           4106h
_TANLNF            =           4C0Bh
_TblScreenDn       =           4E36h
_TblScreenUp       =           4E3Ah
_TBLTRACE          =           4C57h
_TENX              =           40EEh
_TEXT              =           5113h
_THETANAME         =           438Ah
_THREEEXEC         =           48FDh
_TIMES2            =           4082h
_TIMESPT5          =           40A2h
_TMPequNOSRC       =           4B30h
_TNAME             =           43A2h
_TOFRAC            =           48D5h
_toggleIns         =           4CF6h
_TOKTOKEY          =           4DCAh
_TOTOSTRP          =           47E9h
_TraceOff          =           4B18h
_TRCYFUNC          =           5125h
_TRUNC             =           407Ah
_TWOPI             =           433Eh
_UCLINES           =           4A80h
_UNLINECMD         =           4BFBh
_UNOPEXEC          =           48F9h
_VALCUR            =           4B44h
_VARENT            =           4F30h
_varnameToOP1hl    =           4A55h
_VARSYSADR         =           4EB4h
_VERTCMD           =           4BF3h
_VPUTBLANK         =           4C53h
_vputmap           =           477Dh
_vputs             =           4781h
_vputsn            =           4785h
_vputsnG           =           4789h
_vputsnT           =           478Dh
_VtoWHLDE          =           4B08h
_wdispEOL          =           4731h
_whomeUp           =           4735h
_wputc             =           4725h
_wputps            =           4719h
_wputs             =           4729h
_wputsEOL          =           472Dh
_WRITE_TEXT        =           4E7Ch
_wscrollDown       =           4D1Eh
_wscrollLeft       =           4D16h
_wscrollRight      =           4D22h
_wscrollUp         =           4D1Ah
_WTOV              =           4B04h
_XFTOI             =           4B14h
_XItoF             =           4B0Ch
_XNAME             =           439Ah
_XPIXSET           =           4B3Ch
_XROOTY            =           4A8Ch
_XYCENT            =           4B54h
_XYRND             =           4AF0h
_XYRNDBOTH         =           4AECh
_YFTOI             =           4B10h
_YNAME             =           439Eh
_YONOFF            =           50CAh
_YPIXSET           =           4B38h
_YRES              =           4BD8h
_YTOX              =           4A90h
_ZERO16D           =           4266h
_ZEROOP            =           429Ah
_ZEROOP1           =           428Eh
_ZEROOP2           =           4292h
_ZEROOP3           =           4296h
_ZeroTemplate      =           4C3Fh
_ZIfCatalog        =           4819h
_ZIfMatrixMenu     =           481Dh
_ZifRclHandler     =           4E46h
_zifrclkapp        =           4E4Ah
_ZMDECML           =           4B78h
_ZMFIT             =           4B88h
_ZMINT             =           4B74h
_ZMPREV            =           4B7Ch
_ZMSQUARE          =           4B8Ch
_ZMSTATS           =           4A94h
_ZMTRIG            =           4B90h
_ZMUSR             =           4B80h
_ZooDefault        =           4B98h
_ZOOMXYCMD         =           4B58h
COMPLEX_EXEC       =           4A14h
EXECUTE_Z80        =           52E5h
GRAPH_EXEC         =           5188h
IO_EXEC            =           51EFh
PARSER_EXEC        =           4FDFh
PGMIO_EXEC         =           50B2h


;======================================================================
;           I/O equates                                                          
;======================================================================
;
D0LD1L      EQU              0C3h
D0LD1H      EQU              0C1h
D0HD1L      EQU              0C2h
D0HD1H      EQU              0C0h
bport		EQU  0			; 4-BIT LINK PORT (I/O) 

;======================================================================
;	Graph style definitions
;======================================================================
;
GSline          equ     0		;Func, Param, and Polar modes default
GSthick         equ     1
GSabove         equ     2
GSbelow         equ     3
GSpath          equ     4
GSanimate       equ     5
GSdot           equ     6		;Recursive mode default

;======================================================================
;	LCD Driver equates
;======================================================================
LCDinstPort	equ 10h
LCDdataPort equ 11h

;======================================================================
;           System and State Flags
;======================================================================
;

trigFlags	EQU  0		;Trigonometry mode settings
trigDeg 	EQU    2	; 1=degrees, 0=radians

kbdFlags	EQU  0		;Keyboard scan
kbdSCR		EQU    3	; 1=scan code ready
kbdKeyPress	EQU    4	; 1=key has been pressed

doneFlags	EQU  0		;display "Done"
donePrgm	EQU    5	; 1=display "Done" after prgm

ioDelFlag	EQU  0
inDelete	EQU    0	;1 = DELETE SCREEN 

;----------------------------------------------------------------------
editFlags	EQU  1
editOpen	EQU    2	; 1=edit buffer is open

monFlags	EQU  1		;monitor flags
monAbandon	EQU    4	; 1=don't start any long process
				; in put away (#715)
;----------------------------------------------------------------------
plotFlags	EQU  2		;plot generation flags
plotTrace	equ  0
plotLoc 	EQU    1	; 0=bkup & display, 1=display only
plotDisp	EQU    2	; 1=plot is in display, 0=text in display


grfModeFlags	EQU  2		;graph mode settings
grfFuncM	EQU    4	; 1=function graph
grfPolarM	EQU    5	; 1=polar graph
grfParamM	EQU    6	; 1=parametric graph
grfRecurM       EQU    7        ; 1=RECURSION graph

graphFlags	EQU  3
graphDraw	EQU    0	; 0=graph is valid, 1=redraw graph

grfDBFlags	EQU  4
grfDot		EQU    0	; 0=line, 1=dot
grfSimul	EQU    1	; 0=sequential, 1=simultaneous
grfGrid 	EQU    2	; 0=no grid, 1=grid
grfPolar	EQU    3	; 0=rectangular, 1=polar coordinates
grfNoCoord	EQU    4	; 0=display coordinates, 1=off
grfNoAxis	EQU    5	; 0=axis, 1=no axis
grfLabel	EQU    6	; 0=off, 1=axis label

textFlags	EQU  5		;Text output flags
textEraseBelow	EQU    1	; 1=erase line below small char
textScrolled	EQU    2	; 1=screen scrolled
textInverse	EQU    3	; 1=display inverse bit-map
textInsMode	EQU    4	; 0=overstrike, 1=insert mode

ParsFlag2	EQU  7		;PARSER flags
numOP1		EQU    0	; 1=RESULT IN OP1, 0=NO RESULT

newDispF        EQU  8		;Derivative mode flags
preClrForMode   EQU    0	; 1=HELP BLINK ON MODE SCREEN

apdFlags	EQU  8		;Automatic power-down
apdAble 	EQU    2	; 1=APD enabled
apdRunning	EQU    3	; 1=APD clock running


web_err_mask    EQU  60h

onFlags 	EQU  9		;on key flags
onRunning	EQU  3		; 1 = calculator is running
onInterrupt	EQU    4	; 1=on key interrupt request

statFlags	EQU  9		;statistics flags
statsValid	EQU    6	; 1=stats are valid
statANSDISP	EQU  7		; 1=display stat results


fmtFlags	EQU  10		;numeric format flags
fmtExponent	EQU    0	; 1=show exponent, 0=no exponent
fmtEng		EQU    1	; 1=engineering notion, 0=scientific
fmtHex		EQU    2	; 1=hexadecimal
fmtOct		EQU    3	; 1=octal
fmtBin		EQU    4	; 1=binary
;
numMode         EQU  10
fmtReal         EQU    5
fmtRect         EQU    6
fmtPolar        EQU    7

realMode        EQU    5
rectMode        EQU    6
polarMode       EQU    7
;					;   if Hex and Oct both = 1
;					;   then Bin=0 means >Frac
;					;	 Bin=1 means >DMS
fmtBaseMask     EQU  00011100B		; mask to base flags
fmtBaseShift    EQU  2			; offset to base flags
;
;       CHECK IF THESE ARE USED BY NUMFORM,
;
;               EQU  6
;               EQU  7

fmtOverride	EQU  11		;copy of fmtFlags with conversion override

fmtEditFlags	EQU  12		;numeric editing flags
fmtEdit 	EQU    0	; 1=format number for editing

curFlags	EQU  12		;Cursor
curAble 	EQU    2	; 1=cursor flash is enabled
curOn		EQU    3	; 1=cursor is showing
curLock 	EQU    4	; 1=cursor is locked off

appFlags	EQU  13		;application flags
appWantIntrpt	EQU    0		; 1=want ON key interrupts
appTextSave	EQU    1		; 1=save characters in textShadow
appAutoScroll	EQU    2		; 1=auto-scroll text on last line
appMenus	EQU    3		; 1=process keys that bring up menus
					; 0=check Lock menu flag
appLockMenus	EQU    4		; 1=ignore menu keys
					; 0=switch to home screen and bring up menu
appCurGraphic	EQU    5		; 1=graphic cursor
appCurWord	EQU    6		; 1=text cursor covers entire word
appExit 	EQU    7		; 1=application handles [EXIT] key itself

appWantIntrptF	EQU	1<<appWantIntrpt
appTextSaveF	EQU	1<<appTextSave
appAutoScrollF	EQU	1<<appAutoScroll
appMenusF	EQU	1<<appMenus
appLockMenusF	EQU	1<<appLockMenus
appCurGraphicF	EQU	1<<appCurGraphic
appCurWordF	EQU	1<<appCurWord
appExitF	EQU	1<<appExit


seqFlags        EQU  15              ; Sequential Graph flags
webMode         EQU    0             ; 0 = NORMAL SEQ MODE, 1 = WEB MODE
webVert         EQU    1             ;
sequv           EQU    2             ; U vs V
seqvw           EQU    3             ; V vs W
sequw           EQU    4             ; U vs W


promptFlags	EQU  17		;prompt line flags
promptEdit	EQU    0	; 1=editing in prompt buffer

indicFlags	EQU  18		;Indicator flags
indicRun	EQU    0	; 1=run indicator ON
indicInUse	EQU    1	; indicator save area in use=1, free=0
indicOnly	EQU    2	; interrupt handler only checks run indicator

shiftFlags	EQU  18		;[2nd] and [ALPHA] flags
shift2nd	EQU    3	; 1=[2nd] has been pressed
shiftAlpha	EQU    4	; 1=[ALPHA] has been pressed
shiftLwrAlph	EQU    5	; 1=lower case, 0=upper case
shiftALock	EQU    6	; 1=alpha lock has been pressed
shiftKeepAlph	EQU    7	; 1=cannot cancel alpha shift


tblFlags        EQU  19		;table flags.
autoFill        EQU    4	; 1=prompt, 0=fillAuto
autoCalc        EQU    5	; 1=prompt, 0=CalcAuto
reTable         EQU    6	; 0=table is ok, 1=must recompute table.

sGrFlags	EQU  20
grfSplit        EQU    0	; 1=Split Graph, 0=Normal
vertSplit       EQU    1	; 1=Vertical (left-right) Split
grfSChanged     EQU    2	; 1=Graph just changed Split <-> normal
grfSplitOverride EQU   3	; 1 = ignore graph split flag if set
write_on_graph  EQU    4	; 1 = TEXT OR EQU WRITING TO GRAPH SCREEN
g_style_active  EQU    5	; 1 = GRAPH STYLES ARE ENABLED, USE THEM
cmp_mod_box     EQU    6	; 1 = DOING MOD BOX PLOT COMPUTATION
textWrite       EQU    7	; 1 = Small font writes to buffer
							; 0 = Small font writes to display
;

newIndicFlags   EQU  21
extraIndic      EQU    0
saIndic         EQU    1

newFlags2	    EQU  22
noRestores	    EQU  5

smartFlags      EQU  23
;
;----------------------------------------------------------------------
; Note: Fix these Equates if smartFlags are moved                            
;----------------------------------------------------------------------
;
smarter_mask    EQU  3
smarter_test    EQU  1
smartGraph      EQU  0
smartGraph_inv  EQU  1                                                 

more_Flags	EQU 26

No_Del_Stat	EQU 2
;----------------------------------------------------------------------
;           Available for ASM programming
;----------------------------------------------------------------------
;
asm_flag1       EQU       33          ; IY OFFSET VALUE
asm_flag1_0     EQU       0
asm_flag1_1     EQU       1
asm_flag1_2     EQU       2
asm_flag1_3     EQU       3
asm_flag1_4     EQU       4
asm_flag1_5     EQU       5
asm_flag1_6     EQU       6
asm_flag1_7     EQU       7
;
asm_flag2       EQU       34          ; IY OFFSET VALUE
asm_flag2_0     EQU       0
asm_flag2_1     EQU       1
asm_flag2_2     EQU       2
asm_flag2_3     EQU       3
asm_flag2_4     EQU       4
asm_flag2_5     EQU       5
asm_flag2_6     EQU       6
asm_flag2_7     EQU       7
;
asm_flag3       EQU       35          ; IY OFFSET VALUE
asm_flag3_0     EQU       0
asm_flag3_1     EQU       1
asm_flag3_2     EQU       2
asm_flag3_3     EQU       3
asm_flag3_4     EQU       4
asm_flag3_5     EQU       5
asm_flag3_6     EQU       6
asm_flag3_7     EQU       7


;
;       SYSTEM VAR EQUATES, FOR STORING AND RECALLING THEM
;
XSCLt       EQU  2        ; XSCALE
YSCLt       EQU  3        ; YSCALE
XMINt       EQU  0AH      ; XMIN
XMAXt       EQU  0BH      ; XMAX
YMINt       EQU  0CH      ; YMIN
YMAXt       EQU  0DH      ; XMAX
TMINt       EQU  0EH      ; TMIN
TMAXt       EQU  0FH      ; TMAX
THETMINt    EQU  10H      ; THETA MIN
THETMAXt    EQU  11H      ; THETA MAX
TBLMINt     EQU  1AH      ; TABLE MIN
PLOTSTARTt  EQU  1BH      ; PLOT START
NMAXt       EQU  1DH      ; NMAX
nmint       EQU  1FH      ; NMIN
TBLSTEPt    EQU  21H      ; TABLE STEP
TSTEPt      EQU  22H      ; T STEP
THETSTEPt   EQU  23H      ; THETA STEP
DELTAXt     EQU  26H      ; DELTA X
DELTAYt     EQU  27H      ; DELTA Y
XFACTt      EQU  28H      ; X ZOOM FACTOR
YFACTt      EQU  29H      ; Y ZOOM FACTOR
FINnT       EQU  2Bh      ; TVM n
FINiT       EQU  2Ch      ; TVM i
FINpvT      EQU  2Dh      ; TVM pv
FINpmtT     EQU  2Eh      ; TVM pmt
FINfvT      EQU  2Fh      ; TVM fv
FINpyT      EQU  30h      ; TVM p/y
FINcyT      EQU  31h      ; TVM c/y
PLOTSTEPT   EQU  34h      ; PLOT STEP
XREST       EQU  36h      ; X RES

;======================================================================
;           Run indicators
;======================================================================
;
busyPause	EQU	10101010b
busyNormal	EQU	11110000b


;
;======================================================================
;           Data Type Equates   (TI83+) 
;======================================================================
;
RealObj         EQU       0
ListObj         EQU       1
MatObj          EQU       2
EquObj          EQU       3
StrngObj        EQU       4
ProgObj         EQU       5
ProtProgObj     EQU       6
PictObj         EQU       7
GDBObj          EQU       8
UnknownObj      EQU       9
UnknownEquObj   EQU       0Ah
NewEquObj       EQU       0Bh
CplxObj         EQU       0Ch
CListObj        EQU       0Dh
UndefObj        EQU       0Eh
WindowObj       EQU       0Fh
ZStoObj         EQU       10h
TblRngObj       EQU       11h
LCDObj          EQU       12h
BackupObj       EQU       13h
AppObj		EQU	  14h		;application, only used in menus/link
AppVarObj	EQU	  15h		;application variable
TempProgObj     EQU       16h           ;program, home deletes when finished
GroupObj        EQU       17h           ;group.

#IFNDEF NOTOKENS
;======================================================================
;           TOKENS.INC file:
;======================================================================
;                                             length (+80h for leading blank)
;       # TOKENS IN USE = 249
;						     (+40h for trailing blank)
EOSSTART        EQU       0
;
;
;   DISPLAY CONVERSIONS  COME IMMEDIATELY BEFORE 'TSTORE'
;
DCONV   EQU     01H
;
TTODMS          equ       DCONV       ; 01H
TTODEC          equ       DCONV+1     ; 02H
TTOAbc          EQU       DCONV+2     ; 03H    > A b/c
;
TSTORE          equ       DCONV+3     ; 04H Lstore                01
;
TBOXPLOT        EQU       05h
;
BRACKS          EQU       06H
;
tLBrack         equ       BRACKS      ; 06h '['
tRBrack         equ       BRACKS+1    ; 07h ']'
tLBrace         equ       BRACKS+2    ; 08h '{'
tRBrace         equ       BRACKS+3    ; 09h '}'
;
TPOST1          EQU       BRACKS+4
;
tFromRad        equ       TPOST1       ; 0Ah Lradian
tFromDeg        equ       TPOST1+1     ; 0Bh Ldegree
tRecip          equ       TPOST1+2     ; 0Ch Linverse
tSqr            equ       TPOST1+3     ; 0Dh Lsquare
tTrnspos        equ       TPOST1+4     ; 0Eh Ltranspose
TCUBE           EQU       TPOST1+5     ; 0Fh '^3'
;
tLParen         equ       10H         ; 10h '('
tRParen         equ       11H         ; 11h ')'
;
;
;   IMMEDIATE UNARY TOKENS GO FROM HERE TO 'EOSEL-1'
;
IMUN            EQU       12H
;
tRound          equ       IMUN        ; 12h 'round'
TPXTST          equ       IMUN+1      ; 13h 'PXL-TEST'
tAug            equ       IMUN+2      ; 14h 'aug'
tRowSwap        EQU       IMUN+3      ; 15h 'rSwap'
tRowPlus        equ       IMUN+4      ; 16h 'rAdd'
tmRow           EQU       IMUN+5      ; 17h 'multR'
tmRowPlus       EQU       IMUN+6      ; 18h 'mRAdd'
tMax            equ       IMUN+7      ; 19H 'max'
tMin            equ       IMUN+8      ; 1AH 'min'
TRTOPr          EQU       IMUN+9      ; 1BH 'R>Pr
TRTOPo          EQU       IMUN+10     ; 1CH 'R>Po
TPTORx          EQU       IMUN+11     ; 1DH 'P>Rx
TPTORy          EQU       IMUN+12     ; 1EH 'P>Ry
tMEDIAN         equ       IMUN+13     ; 1Fh 'MEDIAN
tRandM          equ       IMUN+14     ; 20h 'randM'
TMEAN           EQU       IMUN+15     ; 21h
TROOT           EQU       IMUN+16     ; 22h 'ROOT'
tSeries         equ       IMUN+17     ; 23h 'seq'
tFnInt          equ       IMUN+18     ; 24h 'fnInt'
tNDeriv         equ       IMUN+19     ; 25h 'fnIr'
TEVALF          EQU       IMUN+20     ; 26h
TFMIN           EQU       IMUN+21     ; 27h
TFMAX           EQU       IMUN+22     ; 28h
;
;
;
;  TOKEN 29H CANNOT BE AN EOS FUNCTION SINCE EL(=29H ALREADY
;
tEOSEL          EQU       IMUN+23
;
TSPACE          EQU       tEOSEL       ; 29h   ' '
tString         equ       tEOSEL+1     ; 2Ah  '"'
tComma          equ       tEOSEL+2     ; 2Bh  ','
;
;
;
Tii             equ       2Ch         ; i
;
; ===== Postfix functions
;
TPOST           EQU       2Dh
;
tFact           equ       TPOST       ; 2Dh '!'



TCUBICR         EQU       2Eh
TQUARTR         EQU       2Fh

;
;       NUMBER TOKENS
;
NUMS            EQU       30h
;
T0              EQU       NUMS        ; 30h
T1              EQU       NUMS+1      ; 31h
T2              EQU       NUMS+2      ; 32h
T3              EQU       NUMS+3      ; 33h
T4              EQU       NUMS+4      ; 34h
T5              EQU       NUMS+5      ; 35h
T6              EQU       NUMS+6      ; 36h
T7              EQU       NUMS+7      ; 37h
T8              EQU       NUMS+8      ; 38h
T9              EQU       NUMS+9      ; 39h
TDECPT          EQU       NUMS+10     ; 3Ah
Tee             EQU       NUMS+11     ; 3Bh
;
;   BINARY OP
;
tOr             equ       3Ch         ; 3Ch  '_or_'
TXOR            equ       3Dh         ; 3Dh
;
tColon          equ       3Eh         ; 3Eh ':'
tEnter          equ       3Fh         ; 3Fh  Lenter
;
;   BINARY OP
;
tAnd            equ       40h         ; 40h  '_and_'               05
;
;       LETTER TOKENS
;
LET             EQU       41h
;
TA              EQU       LET         ; 41h
TB              EQU       LET+1       ; 42h
TC              EQU       LET+2       ; 43h
TD              EQU       LET+3       ; 44h
TE              EQU       LET+4       ; 45h
TF              EQU       LET+5       ; 46h
TG              EQU       LET+6       ; 47h
TH              EQU       LET+7       ; 48h
TI              EQU       LET+8       ; 49h
TJ              EQU       LET+9       ; 4Ah
TK              EQU       LET+10      ; 4Bh
TL              EQU       LET+11      ; 4Ch
TM              EQU       LET+12      ; 4Dh
TN              EQU       LET+13      ; 4Eh
TO              EQU       LET+14      ; 4Fh
TP              EQU       LET+15      ; 50h
TQ              EQU       LET+16      ; 51h
TR              EQU       LET+17      ; 52h
TS              EQU       LET+18      ; 53h
TT              EQU       LET+19      ; 54h
TU              EQU       LET+20      ; 55h
TV              EQU       LET+21      ; 56h
TW              EQU       LET+22      ; 57h
TX              EQU       LET+23      ; 58h
TY              EQU       LET+24      ; 59h
TZ              EQU       LET+25      ; 5Ah
TTHETA          EQU       LET+26      ; 5Bh
;
;
;   THESE VAR TOKENS ARE 1ST OF A DOUBLE TOKEN
;
;   SEE 'VARS.DOC' FOR MORE INFO ON VARS
;
VTOKS   EQU     LET+27    ;
;
;       USER MATRIX TOKEN, 2ND TOKEN NEEDED FOR NAME
;
TVARMAT         EQU       VTOKS       ; 5Ch
;
;       USER LIST TOKEN, 2ND TOKEN NEEDED FOR NAME
;
TVARLST         EQU       VTOKS+1     ; 5Dh
;
;       USER EQUATION TOKEN, 2ND TOKEN NEEDED FOR NAME
;
TVAREQU         EQU       VTOKS+2     ; 5Eh
;
;       THIS IS A SINGLE TOKEN, I WILL HAVE TO READ THE NAME IN
;       EVERY SINGLE TIME I EXECUTE
;
TPROG           EQU       VTOKS+3     ; 5Fh
;
;       USER PICT TOKEN, 2ND TOKEN NEEDED FOR NAME
;
TVARPICT        EQU       VTOKS+4     ; 60h
;
;       USER GDB TOKEN, 2ND TOKEN NEEDED FOR NAME
;
TVARGDB         EQU       VTOKS+5     ; 61h
;
;       SYSTEM VARS THAT ARE USED FOR OUTPUT ONLY
;       CANNOT BE STORED TO BY THE USER
;
tVarOut         equ       VTOKS+6     ; 62h
;
;       SYSTEM VARS THAT ARE NOT IN THE SYMBOL TABLE
;       BUT THE USER CAN STORE TO THEM.
;
tVarSys         equ       VTOKS+7     ; 63h
;
;
; ===== Mode setting commands
;
MODESA          EQU       VTOKS+8     ; 64h
;
tRad            equ       MODESA      ; 64h 'Radian'
tDeg            equ       MODESA+1    ; 65h 'Degree'
tNormF          equ       MODESA+2    ; 66h 'Normal'
tSci            equ       MODESA+3    ; 67h 'Sci'
tEng            equ       MODESA+4    ; 68h 'Eng'
tFloat          equ       MODESA+5    ; 69h 'Float'
;
CMPS            EQU       6Ah
;
tEQ             equ       CMPS        ; 6Ah '=='
tLT             equ       CMPS+1      ; 6Bh '<'
tGT             equ       CMPS+2      ; 6Ch '>'
tLE             equ       CMPS+3      ; 6Dh LLE
tGE             equ       CMPS+4      ; 6Eh LGE
tNE             equ       CMPS+5      ; 6Fh LNE
;
;   BINARY OP
;
tAdd            equ       70h         ; 70h '+'
tSub            equ       71h         ; 71h '-'
;
TANS            EQU       72H         ; 72h
;
; ===== Mode setting commands
;
MODES           EQU       73h
;
tFix            equ       MODES       ; 73h 'Fix_'
TSPLITON        equ       MODES+1     ; 74h
TFULLSCREEN     EQU       MODES+2     ; 75h
tStndrd         EQU       MODES+3     ; 76h 'Func'
tParam          equ       MODES+4     ; 77h 'Param'
tPolar          EQU       MODES+5     ; 78h 'Pol'
TSEQG           equ       MODES+6     ; 79h
TAFILLON        equ       MODES+7     ; 7Ah 'AUTO FILL ON
TAFILLOFF       equ       MODES+8     ; 7Bh
TACALCON        EQU       MODES+9     ; 7Ch
TACALCOFF       equ       MODES+10    ; 7Dh 'AUTOFILL OFF
;
;
;       GRAPH FORMAT TOKENS ARE 2 BYTE TOKENS
;
TGFORMAT        EQU       MODES+11    ; 7Eh
;
TBOXICON        equ       7Fh
TCROSSICON      EQU       80h
TDOTICON        EQU       81h
;
;       BINARY OP
;
tMul            equ       82h         ; 82h  '*'
tDiv            equ       83h         ; 83h  '/'
;
;       SOME GRAPH COMMANDS
;
GRCMDS          EQU       84h
;
tTrace          equ       GRCMDS      ; 84h 'Trace'
tClDrw          equ       GRCMDS+1    ; 85h 'ClDrw'
tZoomStd        equ       GRCMDS+2    ; 86h 'ZStd'
tZoomtrg        equ       GRCMDS+3    ; 87h 'Ztrg'
tZOOMBOX        EQU       GRCMDS+4    ; 88h 'ZBOX'
tZoomIn         equ       GRCMDS+5    ; 89h 'ZIn'
tZoomOut        equ       GRCMDS+6    ; 8Ah 'ZOut'
tZoomSqr        equ       GRCMDS+7    ; 8Bh 'ZSqr'
tZoomInt        equ       GRCMDS+8    ; 8Ch 'ZInt'
tZoomPrev       equ       GRCMDS+9    ; 8Dh 'ZPrev'
tZoomDec        equ       GRCMDS+10   ; 8Eh 'ZDecm'
TZOOMSTAT       EQU       GRCMDS+11   ; 8Fh 'ZSTAT
tUsrZm          equ       GRCMDS+12   ; 90h 'ZRcl'
tPrtScrn        equ       GRCMDS+13   ; 91h 'PrtScrn'
tZOOMSTO        EQU       GRCMDS+14   ; 92H  ZOOM STORE
TTEXT           EQU       GRCMDS+15   ; 93h
;
;       BINARY OP
;
tnPr            equ       GRCMDS+16   ; 94h '_nPr_'
tnCr            equ       GRCMDS+17   ; 95h '_nCr_'
;
;       MORE GRAPH COMMANDS
;
tYOn            equ       GRCMDS+18   ; 96h 'FnOn_'
tYOff           equ       GRCMDS+19   ; 97h 'FnOff_'
tStPic          equ       GRCMDS+20   ; 98h 'StPic_'
tRcPic          equ       GRCMDS+21   ; 99h 'RcPic_'
tStoDB          equ       GRCMDS+22   ; 9Ah 'StGDB_'
tRclDB          equ       GRCMDS+23   ; 9Bh 'RcGDB_'
tLine           equ       GRCMDS+24   ; 9Ch 'Line'
tVert           equ       GRCMDS+25   ; 9Dh 'Vert_'
tPtOn           equ       GRCMDS+26   ; 9Eh 'PtOn'
tPtOff          equ       GRCMDS+27   ; 9Fh 'PtOff'
;
;  TOKEN A0 CANNOT BE AN EOS FUNCTION SINCE LOW MULT=A0 ALREADY
;
tPtChg          equ       GRCMDS+28   ; A0h 'PtChg'
TPXON           EQU       GRCMDS+29   ; A1h
TPXOFF          EQU       GRCMDS+30   ; A2h
TPXCHG          EQU       GRCMDS+31   ; A3h
tShade          equ       GRCMDS+32   ; A4h 'Shade'
tCircl          equ       GRCMDS+33   ; A5h 'Circl'
tHORZ           equ       GRCMDS+34   ; A6h 'HORIZONTAL'
tTanLn          equ       GRCMDS+35   ; A7h 'TanLn'
tDrInv          equ       GRCMDS+36   ; A8h 'DrInv_'
tDrawF          equ       GRCMDS+37   ; A9h 'DrawF_'
;
TVARSTRNG       EQU       0AAh
;
; ===== Functions with no arguments
;
NOARG   EQU     0ABh
;
tRand           equ       NOARG       ; ABh 'rand'
tPi             equ       NOARG+1     ; ACh  Lpi
tGetKey         equ       NOARG+2     ; ADh 'getKy'
;
;
TAPOST          EQU       tGetKey+1   ; APOSTROPHY
TQUEST          EQU       TAPOST+1    ; QUESTION MARK
;
;
; ===== Unary functions
;
;       UNARY : THESE UNARY FUNCTIONS TAKE A COMBINATION OF
;               ARGUMENTS, REAL,LIST,MATRIX
;
UNARY   EQU     TQUEST+1              ; B0h
;
tChs            equ       UNARY       ; B0h
tInt            equ       UNARY+1     ; B1h
TABS            equ       UNARY+2     ; B2h
tDet            equ       UNARY+3     ; B3h
tIdent          equ       UNARY+4     ; B4h
tDim            equ       UNARY+5     ; B5h
tSum            equ       UNARY+6     ; B6h
tProd           equ       UNARY+7     ; B7h
tNot            equ       UNARY+8     ; B8h
tIPart          equ       UNARY+9     ; B9h
tFPart          equ       UNARY+10    ; BAh
;
;
;       NEW 2 BYTE TOKENS
;
T2BYTETOK       EQU       0BBh         ;
;
;
;       UNARYLR : UNARY FUNCTIONS THAT ONLY WORK WITH
;                 REAL NUMBERS AND LISTS
;
UNARYLR EQU     UNARY+12
;
tSqrt           equ       UNARYLR     ; BCh
TCUBRT          equ       UNARYLR+1   ; BDh
tLn             equ       UNARYLR+2   ; BEh
tExp            equ       UNARYLR+3   ; BFh
tLog            equ       UNARYLR+4   ; C0h
tALog           equ       UNARYLR+5   ; C1h
tSin            equ       UNARYLR+6   ; C2h
tASin           equ       UNARYLR+7   ; C3h
tCos            equ       UNARYLR+8   ; C4h
tACos           equ       UNARYLR+9   ; C5h
tTan            equ       UNARYLR+10  ; C6h
tATan           equ       UNARYLR+11  ; C7h
tSinh           equ       UNARYLR+12  ; C8h
tASinh          equ       UNARYLR+13  ; C9h
tCosh           equ       UNARYLR+14  ; CAh
tACosh          equ       UNARYLR+15  ; CBh
tTanh           equ       UNARYLR+16  ; CCh
tATanh          equ       UNARYLR+17  ; CDh
;
;       SOME PROGRAMMING COMMANDS
;
PROGTOK EQU     UNARYLR+18
;
tIf             equ       PROGTOK     ; CEh
tThen           equ       PROGTOK+1   ; CFh
tElse           equ       PROGTOK+2   ; D0h
tWhile          equ       PROGTOK+3   ; D1h
tRepeat         equ       PROGTOK+4   ; D2h
tFor            equ       PROGTOK+5   ; D3h
tEnd            equ       PROGTOK+6   ; D4h
tReturn         equ       PROGTOK+7   ; D5h
tLbl            equ       PROGTOK+8   ; D6h   'Lbl_'
tGoto           equ       PROGTOK+9   ; D7h   'Goto_'
tPause          equ       PROGTOK+10  ; D8h   'Pause_'
tStop           equ       PROGTOK+11  ; D9h   'Stop'
tISG            equ       PROGTOK+12  ; DAh   'IS>'
tDSL            equ       PROGTOK+13  ; DBh   'DS<'
tInput          equ       PROGTOK+14  ; DCh   'Input_'
tPrompt         equ       PROGTOK+15  ; DDh   'Prompt_'
tDisp           equ       PROGTOK+16  ; DEh   'Disp_'
tDispG          equ       PROGTOK+17  ; DFh   'DispG'
;
;  TOKEN E0 CANNOT BE AN EOS FUNCTION SINCE HI MU
;
tOutput         equ       PROGTOK+18  ; E0H    'Outpt'
tClLCD          equ       PROGTOK+19  ; E1H    'ClLCD'
tConst          equ       PROGTOK+20  ; E2H    'Fill'
tSortA          equ       PROGTOK+21  ; E3H    'sortA_'
tSortD          equ       PROGTOK+22  ; E4H    'sortD_'
TDISPTAB        EQU       PROGTOK+23  ; E5h    'DISP TABLE
tMenu           equ       PROGTOK+24  ; E6H    'Menu'
tSENDMBL        EQU       PROGTOK+25  ; E7H    'SEND'
tGETMBL         EQU       PROGTOK+26  ; E8H    'GET'
;
;       STAT PLOT COMMANDS
;
STATPCMD        EQU       PROGTOK+27
;
TPLOTON         EQU       STATPCMD    ; E9h     ' PLOTSON'
TPLOTOFF        EQU       STATPCMD+1   ; EAh     ' PLOTSOFF
;
TLISTNAME       EQU       0EBH        ; LIST DESIGNATOR
;
;
;
TPLOT1          EQU       0ECH
TPLOT2          EQU       0EDH
TPLOT3          EQU       0EEH
;
tPower          equ       0F0h        ;        '^'
tXRoot          equ       0F1h        ;        LsupX,Lroot

STATCMD         EQU       0F2h
;
tOneVar         equ       STATCMD     ; F2h    'OneVar_'
TTWOVAR         EQU       STATCMD+1   ; F3h
tLR             equ       STATCMD+2   ; F4h    'LinR(A+BX
tLRExp          equ       STATCMD+3   ; F5h    'ExpR_'
tLRLn           equ       STATCMD+4   ; F6h    'LnR_'
tLRPwr          equ       STATCMD+5   ; F7h    'PwrR_'
TMEDMED         EQU       STATCMD+6   ; F8h
TQUAD           EQU       STATCMD+7   ; F9h
tCLRLST         equ       STATCMD+8   ; FAh    'CLEAR LIST
TCLRTBL         EQU       STATCMD+9   ; FBH    CLEAR TABLE
tHist           equ       STATCMD+10  ; FCh    'Hist_'
txyLine         equ       STATCMD+11  ; FDH    'xyline_'
tScatter        equ       STATCMD+12  ; FEh    'Scatter_'
tLR1            equ       STATCMD+13  ; FFh    'LINR(AX+B
;
; 2ND HALF OF GRAPH FORMAT TOKENS
;
; ===== Format settings commands
;
GFMT            EQU       0
;
tSeq            equ       GFMT        ; 'SeqG'
tSimulG         equ       GFMT+1      ; 'SimulG'
tPolarG         equ       GFMT+2      ; 'PolarGC'
tRectG          equ       GFMT+3      ; 'RectGC'
tCoordOn        equ       GFMT+4      ; 'CoordOn'
tCoordOff       equ       GFMT+5      ; 'CoordOff'
tDrawLine       equ       GFMT+6      ; 'DrawLine'
tDrawDot        equ       GFMT+7      ; 'DrawDot'
tAxisOn         equ       GFMT+8      ; 'AxesOn'
tAxisOff        equ       GFMT+9      ; 'AxesOff'
tGridOn         equ       GFMT+10     ; 'GridOn'
tGridOff        equ       GFMT+11     ; 'GridOff'
tLblOn          equ       GFMT+12     ; 'LabelOn'
tLblOff         equ       GFMT+13     ; 'LabelOff'
tWebOn          equ       GFMT+14     ; 'WebOn'
tWebOff         equ       GFMT+15     ; 'WebOFF'
TUV             EQU       GFMT+16     ; U vs V
TVW             EQU       GFMT+17     ; V vs W
TUW             EQU       GFMT+18     ; U vs W
;
;       2ND HALF OF USER MATRIX TOKENS
;
TMATA           EQU       00h         ; MAT A
TMATB           EQU       01h         ; MAT B
TMATC           EQU       02h         ; MAT C
TMATD           EQU       03h         ; MAT D
TMATE           EQU       04h         ; MAT E
TMATF           EQU       05h         ; MAT F
TMATG           EQU       06h         ; MAT G
TMATH           EQU       07h         ; MAT H
TMATI           EQU       08h         ; MAT I
TMATJ           EQU       09h         ; MAT J
;
;       2ND HALF OF USER LIST TOKENS
;
TL1             EQU       00h         ; LIST 1
TL2             EQU       01h         ; LIST 2
TL3             EQU       02h         ; LIST 3
TL4             EQU       03h         ; LIST 4
TL5             EQU       04h         ; LIST 5
TL6             EQU       05h         ; LIST 6
;
;       2ND HALF OF USER EQUATION TOKENS
;
;
;       "Y" EQUATIONS HAVE BIT 4 SET
;
TY1             EQU       10h         ; Y1
TY2             EQU       11h         ; Y2
TY3             EQU       12h         ; Y3
TY4             EQU       13h         ; Y4
TY5             EQU       14h         ; Y5
TY6             EQU       15h         ; Y6
TY7             EQU       16h         ; Y7
TY8             EQU       17h         ; Y8
TY9             EQU       18h         ; Y9
TY0             EQU       19H         ; Y0
;
;       PARAM EQUATIONS HAVE BIT 5 SET
;
TX1T            EQU       20h         ; X1t
TY1T            EQU       21h         ; Y1t
TX2T            EQU       22h         ; X2t
TY2T            EQU       23h         ; Y2t
TX3T            EQU       24h         ; X3t
TY3T            EQU       25h         ; Y3t
TX4T            EQU       26h         ; X4t
TY4T            EQU       27h         ; Y4t
TX5T            EQU       28h         ; X5t
TY5T            EQU       29h         ; Y5t
TX6T            EQU       2Ah         ; X6t
TY6T            EQU       2Bh         ; Y6t
;
;       POLAR EQUATIONS HAVE BIT 6 SET
;
TR1             EQU       40h         ; R1
TR2             EQU       41h         ; R2
TR3             EQU       42h         ; R3
TR4             EQU       43h         ; R4
TR5             EQU       44h         ; R5
TR6             EQU       45h         ; R6
;
;       RECURSION EQUATIONS HAVE BIT 7 SET
;
TUN             EQU       80h         ; Un
TVN             EQU       81h         ; Vn
TWN             EQU       82h         ; Wn
;
;       2ND HALF OF USER PICTURE TOKENS
;
TPIC1           EQU       00h         ; PIC1
TPIC2           EQU       01h         ; PIC2
TPIC3           EQU       02h         ; PIC3
TPIC4           EQU       03h         ; PIC4
TPIC5           EQU       04h         ; PIC5
TPIC6           EQU       05h         ; PIC6
TPIC7           EQU       06h         ; PIC7
TPIC8           EQU       07h         ; PIC8
TPIC9           EQU       08h         ; PIC9
TPIC0           EQU       09h         ; PIC0
;
;       2ND HALF OF USER GRAPH DATABASE TOKENS
;
TGDB1           EQU       00h         ; GDB1
TGDB2           EQU       01h         ; GDB2
TGDB3           EQU       02h         ; GDB3
TGDB4           EQU       03h         ; GDB4
TGDB5           EQU       04h         ; GDB5
TGDB6           EQU       05h         ; GDB6
TGDB7           EQU       06h         ; GDB7
TGDB8           EQU       07h         ; GDB8
TGDB9           EQU       08h         ; GDB9
TGDB0           EQU       09h         ; GDB0
;
;       2ND HALF OF STRING VARS
;
TSTR1           EQU       00h
TSTR2           EQU       01h
TSTR3           EQU       02h
TSTR4           EQU       03h
TSTR5           EQU       04h
TSTR6           EQU       05h
TSTR7           EQU       06h
TSTR8           EQU       07h
TSTR9           EQU       08h
TSTR0           EQU       09h
;
;       2ND HALF OF SYSTEM OUTPUT ONLY VARIABLES
;
;
;   OPEN        EQU       00H
;
TREGEQ          EQU       01h         ; REGRESSION EQUATION
;
TSTATN          EQU       02h         ; STATISTICS N
;
TXMEAN          EQU       03h         ; X MEAN
TSUMX           EQU       04h         ; SUM(X)
TSUMXSQR        EQU       05h         ; SUM(X^2)
TSTDX           EQU       06h         ; STANDARD DEV X
TSTDPX          EQU       07h         ; STANDARD DEV POP X
TMINX           EQU       08h         ; MIN X VALUE
TMAXX           EQU       09h         ; MAX X VALUE
;
TMINY           EQU       0Ah         ; MIN Y VALUE
TMAXY           EQU       0Bh         ; MAX Y VALUE
TYMEAN          EQU       0Ch         ; Y MEAN
TSUMY           EQU       0Dh         ; SUM(Y)
TSUMYSQR        EQU       0Eh         ; SUM(Y^2)
TSTDY           EQU       0Fh         ; STANDARD DEV Y
TSTDPY          EQU       10h         ; STANDARD DEV POP Y
;
TSUMXY          EQU       11h         ; SUM(XY)
TCORR           EQU       12h         ; CORRELATION
TMEDX           EQU       13h         ; MED(X)
TQ1             EQU       14h         ; 1ST QUADRANT OF X
TQ3             EQU       15h         ; 3RD QUADRANT OF X
TQUADa          EQU       16h         ; 1ST TERM OF QUAD POLY REG/ Y-INT
TQUADb          EQU       17h         ; 2ND TERM OF QUAD POLY REG/ SLOPE
TQUADc          EQU       18h         ; 3RD TERM OF QUAD POLY REG
TCUBEd          EQU       19h         ; 4TH TERM OF CUBIC POLY REG
TQUARTe         EQU       1Ah         ; 5TH TERM OF QUART POLY REG
TMEDX1          EQU       1Bh         ; x1 FOR MED-MED
TMEDX2          EQU       1Ch         ; x2 FOR MED-MED
TMEDX3          EQU       1Dh         ; x3 FOR MED-MED
TMEDY1          EQU       1Eh         ; y1 FOR MED-MED
TMEDY2          EQU       1Fh         ; y2 FOR MED-MED
TMEDY3          EQU       20h         ; y3 FOR MED-MED
;
TRECURN           EQU       21h       ; RECURSION N
TSTATP          EQU       22h
TSTATZ          EQU       23h
TSTATT          EQU       24h
TSTATCHI        EQU       25h
TSTATF          EQU       26h
TSTATDF         EQU       27h
TSTATPHAT       EQU       28h
TSTATPHAT1      EQU       29h
TSTATPHAT2      EQU       2Ah
TSTATMEANX1     EQU       2Bh
TSTATSTDX1      EQU       2Ch
TSTATN1         EQU       2Dh
TSTATMEANX2     EQU       2Eh
TSTATSTDX2      EQU       2Fh
TSTATN2         EQU       30h
TSTATSTDXP      EQU       31h
TSTATLOWER      EQU       32h
TSTATUPPER      EQU       33h
TSTAT_S         EQU       34h
TLRSQR          EQU       35h         ; r^2
TBRSQR          EQU       36h         ; R^2
;
;       these next tokens are only used to acess the data
;       they are display only and the user cannot access them at all
;
TF_DF    EQU     37h       ; ANOFAV FACTOR DF
TF_SS    EQU     38h       ; ANOFAV FACTOR SS
TF_MS    EQU     39h       ; ANOFAV FACTOR MS
TE_DF    EQU     3Ah       ; ANOFAV ERROR DF
TE_SS    EQU     3Bh       ; ANOFAV ERROR SS
TE_MS    EQU     3Ch       ; ANOFAV ERROR MS
;
;
;       2ND HALF OF SYSTEM INPUT/OUTPUT VARIABLES
;

; SYSTEM VARIABLE EQUATES
;
;       VARS WITH NO RESTRICTION
;
TUXSCL       EQU  0
TUYSCL       EQU  1
TXSCL        EQU  2
TYSCL        EQU  3
TRECURU0     EQU  4        ; U 1ST INITIAL COND
TRECURV0     EQU  5        ; V 1ST INITIAL COND
TUN1         EQU  6        ; U(N-1)    ; NOT USED
TVN1         EQU  7        ; V(N-1)    ; NOT USED
TURECURU0    EQU  8        ;
TURECURV0    EQU  9        ;
;
;       VARS ROUNDED TO 10 DIGITS
;
TXMIN        EQU  0AH
TXMAX        EQU  0BH
TYMIN        EQU  0CH
TYMAX        EQU  0DH
TTMIN        EQU  0EH
TTMAX        EQU  0FH
TTHETAMIN    EQU  10H
TTHETAMAX    EQU  11H
TUXMIN       EQU  12H
TUXMAX       EQU  13H
TUYMIN       EQU  14H
TUYMAX       EQU  15H
TUTHETMIN    EQU  16H
TUTHETMAX    EQU  17H
TUTMIN       EQU  18H
TUTMAX       EQU  19H
TTBLMIN      EQU  1AH
TPLOTSTART   EQU  1BH
TUPLOTSTART  EQU  1CH
TNMAX        EQU  1DH
TUNMAX       EQU  1EH
TNMIN        EQU  1FH
TUNMIN       EQU  20H
;
;       VARS CANNOT BE 0
;
TTBLSTEP     EQU  21H
TTSTEP       EQU  22H
TTHETASTEP    EQU  23H
TUTSTEP      EQU  24H
TUTHETSTEP     EQU  25H
;
;       SPECIAL LIMITS ON THESE
;
TDELTAX      EQU  26H
TDELTAY      EQU  27H
;
;       SPECIAL LIMITS ON THESE
;
TXFACT       EQU  28H
TYFACT       EQU  29H
;
TTBLINPUT       EQU       2Ah
;
;       ANY RESTRICTIONS ??????
;
TFINn           EQU       2Bh
TFINi           EQU       2Ch
TFINpv          EQU       2Dh
TFINpmt         EQU       2Eh
TFINfv          EQU       2Fh
TFINpy          EQU       30h
TFINcy          EQU       31h
;
TRECURW0        EQU       32h         ; W0(1)
TURECURW0       EQU       33h
;
TPLOTSTEP       EQU       34h
TUPLOTSTEP      EQU       35h
;
TXRES           EQU       36h
TUXRES          EQU       37h
;
;       ALL THE TOKENS BELOW DO NOT HAVE STRINGS ASSOCIATED
;       THEY ARE HERE JUST FOR STORING AND RECALLING
;
TRECURU02       EQU       38h         ; U0(2)
TURECURU02      EQU       39h
TRECURV02       EQU       3Ch         ; V0(2)
TURECURV02      EQU       3Dh
TRECURW02       EQU       3Eh         ; W0(2)
TURECURW02      EQU       3Fh
;
; NO RESTRICTIONS
;
;TWN1           EQU       40h         ; W(N-1)
;UN2           EQU       41h         ; U(N-2)
;VN2           EQU       42h         ; V(N-2)
;WN2           EQU       43h         ; W(N-2)
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

TFINnpv         EQU       00h         ; immediate unary
TFINirr         EQU       01h         ; immediate unary
TFINbal         EQU       02h         ; immediate unary
TFINprn         EQU       03h         ; immediate unary
TFINint         EQU       04h         ; immediate unary
TFINtonom       EQU       05h         ; immediate unary
TFINtoeff       EQU       06h         ; immediate unary
TFINdbd         EQU       07h         ; immediate unary
TLCM            EQU       08h         ; immediate unary
TGCD            EQU       09h         ; immediate unary
TRANDINT        EQU       0Ah         ; immediate unary
TRANDBIN        EQU       0Bh         ; immediate unary
TSUBSTRNG       EQU       0Ch         ; immediate unary
TSTDDEV         EQU       0Dh         ; immediate unary
TVARIANCE       EQU       0Eh         ; immediate unary
TINSTRNG        EQU       0Fh         ; immediate unary
TDNORMAL        EQU       10h         ; immediate unary
TINVNORM        EQU       11h         ; immediate unary
TDT             EQU       12h         ; immediate unary
TCHI            EQU       13h         ; immediate unary
TDF             EQU       14h         ; immediate unary
TBINPDF         EQU       15h         ; immediate unary
TBINCDF         EQU       16h         ; immediate unary
TPOIPDF         EQU       17h         ; immediate unary
TPOICDF         EQU       18h         ; immediate unary
TGEOPDF         EQU       19h         ; immediate unary
TGEOCDF         EQU       1Ah         ; immediate unary
TNORMALPDF      EQU       1Bh         ; immediate unary
TTPDF           EQU       1Ch         ; immediate unary
TCHIPDF         EQU       1Dh         ; immediate unary
TFPDF           EQU       1Eh         ; immediate unary
;TRANDBIN        EQU       1Fh         ; immediate unary
TRANDNORM       EQU       1Fh         ; immediate unary
TFINFpmt        EQU       20h         ; 0 or multy arg function
TFINFi          EQU       21h         ; 0 or multy arg function
TFINFpv         EQU       22h         ; 0 or multy arg function
TFINFn          EQU       23h         ; 0 or multy arg function
TFINFfv         EQU       24h         ; 0 or multy arg function
TCONJ           EQU       25h         ; unary
TREAL           EQU       26h         ; unary
TIMAG           EQU       27h         ; unary
TANGLE          EQU       28h         ; unary
TCUMSUM         EQU       29h         ; unary
TEXPR           EQU       2Ah         ; unary
TLENGTH         EQU       2Bh         ; unary
TDELTALST       EQU       2Ch         ; unary
TREF            EQU       2Dh         ; unary
TRREF           EQU       2Eh         ; unary
TTORECT         EQU       2Fh         ; display conversion
TTOPOLAR        EQU       30h         ; display conversion
TCONSTE         EQU       31h         ; function no arg
TSINREG         EQU       32h         ; command opt push
TLOGISTIC       EQU       33h         ; command opt push
TLINREGTTEST    EQU       34h         ; command opt push
TSHADENORM      EQU       35h         ; command pushed
TSHADET         EQU       36h         ; command pushed
TSHADECHI       EQU       37h         ; command pushed
TSHADEF         EQU       38h         ; command pushed
TMATTOLST       EQU       39h         ; command pushed
TLSTTOMAT       EQU       3Ah         ; command pushed
TZTEST          EQU       3Bh         ; command pushed
TTTEST          EQU       3Ch         ; command pushed
T2SAMPZTEST     EQU       3Dh         ; command pushed
T1PROPZTEST     EQU       3Eh         ; command pushed
T2PROPZTEST     EQU       3Fh         ; command pushed
TCHITEST        EQU       40h         ; command pushed
TZINTVAL        EQU       41h         ; command pushed
T2SAMPZINT      EQU       42h         ; command pushed
T1PROPZINT      EQU       43h         ; command pushed
T2PROPZINT      EQU       44h         ; command pushed
TGRAPHSTYLE     EQU       45h         ; command pushed
T2SAMPTTEST     EQU       46h         ; command opt pushed
T2SAMPFTEST     EQU       47h         ; command opt pushed
TTINTVAL        EQU       48h         ; command opt pushed
T2SAMPTINT      EQU       49h         ; command opt pushed
TSETUPLST       EQU       4Ah         ; command opt pushed
TFINpmtend      EQU       4Bh         ; mode command
TFINpmtbeg      EQU       4Ch         ; mode command
TREALM          EQU       4Dh         ; mode command
TPOLARM         EQU       4Eh         ; mode command
TRECTM          EQU       4Fh         ; mode command
TEXPRON         EQU       50h         ; mode command
TEXPROFF        EQU       51h         ; mode command
TCLRALLLST      EQU       52h         ; command not pushed
TGETCALC        EQU       53h         ; command not pushed
TDELVAR         EQU       54h         ; command not pushed
TEQUTOSTRNG     EQU       55h         ; command not pushed
TSTRNGTOEQU     EQU       56h         ; command not pushed
tdellast        EQU       57h         ; command not pushed
TSELECT         EQU       58h         ; command not pushed
TANOVA          EQU       59h         ; command not pushed
TMODBOX         EQU       5Ah         ; icon
TNORMPROB       EQU       5Bh         ; icon
;
;       tokens 5Ch to 63h CANNOT BE THE 2ND BYTE OF
;       TWO BYTE TOKENS. THESE ARE ALREADY THE 1ST BYTE VALUE
;       OF TWO BYTE TOKENS. READING TOKENS BACKWARDS WILL NOT
;       WORK IF THIS RESTRICTION IS NOT IN PLACE.
;
;
;       NO USE  EQU       5Ch
;       NO USE  EQU       5Dh
;       NO USE  EQU       5Eh
;       NO USE  EQU       5Fh
;       NO USE  EQU       60h
;       NO USE  EQU       61h
;       NO USE  EQU       62h
;       NO USE  EQU       63h
;
TMGT            EQU       64h         ; VERTICAL SPLIT
TZFIT           EQU       65h         ; ZOOM FIT
TDIAG_ON        EQU       66h         ; DIANOSTIC DISPLAY ON
TDIAG_OFF       EQU       67h         ; DIANOSTIC DISPLAY OFF
;
;
#ENDIF




;Extra and misnamed romcalls defined in ION.INC:
_memfree = _memchk
_cleargbuf = _GrBufClr
_indicatorOn = _RunIndicOn
_indicatorOff = _RunIndicOff
_copygbuf = _GrBufCpy
_clrlcdf = _clrlcdfull
_cleartextshad = _ClrTxtShd
_clrscr = _ClrLCD
_clrscrf = _ClrScrnFull
_chksysvar = _ChkFindSym
_deletemem = _delmem
_lcdbusy = _LCD_BUSY

;Saferams
saferam1	= savesscreen
saferam2	= statvars
saferam3	= textshadow
saferam4	= op1

;Alternate equate names
gbuf = plotsscreen
grbuf = plotsscreen
sram = saferam1
apdram = saferam1
statram = saferam2
cmdshad = cmdshadow

;CrASH names
GRAPH_MEM = plotsscreen
APD_BUF = savesscreen
TEXT_MEM = textshadow
TEXT_MEM2 = cmdshadow
CURSOR_ROW = currow
CURSOR_COL = curcol
GRAF_CURS = pencol
CURSOR_X = pencol
CURSOR_Y = penrow
VAT_START = symtable
FREE_A1 = plotsscreen
FREE_A2 = saferam3
FREE_A3 = OP1
FREE_B = saferam2
FREE_C = saferam1  
START_ADDR = progstart

;CrASH Romcalls
CR_KHAND = _getkey
LD_HL_MHL = _ldhlind
CP_HL_DE = _cphlde
UNPACK_HL = _divhlby10
DIV_HL_A = _divhlbya
GET_KEY = _getcsc
DISP_DELAY = _lcdbusy

;CrASH ROM_CALLs
TR_CHARPUT = _putc
TX_CHARPUT = _putmap
D_ZT_STR = _puts
D_HL_DECI = _disphl
NEXT_LINE = _newline
SCROLL_UP = _scrollup

M_CHARPUT = _vputmap
D_ZM_STR = _vputs

CLEARLCD = _clrlcdfull
DISP_GRAPH = _grbufcpy

#ENDIF

#IFDEF TI83P

;88888888888 8888888        .d8888b.   .d8888b.
;    888       888         d88P  Y88b d88P  Y88b
;    888       888         Y88b. d88P      .d88P
;    888       888  888888  "Y88888"      8888"    888
;    888       888  888888 .d8P""Y8b.      "Y8b. 8888888
;    888       888         888    888 888    888   888
;    888       888         Y88b  d88P Y88b  d88P
;    888     8888888        "Y8888P"   "Y8888P"

#define bcall(xxxx)	rst 28h \ .dw xxxx
#define	bcallz(xxxx)	jr nz,$+5 \ rst 28h \ .dw xxxx
#define	bcallnz(xxxx)	jr	z,$+5 \ rst 28h \ .dw xxxx
#define	bcallc(xxxx)	jr	nc,$+5 \ rst 28h \ .dw xxxx
#define	bcallnc(xxxx)	jr	c,$+5	\ rst 28h \ .dw xxxx
#define bjump(xxxx)	call 50h \ .dw xxxx
#define bjp(xxxx)	bcall(xxxx) \ ret
#define icall(xxxx) bcall(xxxx)
#define ijp(xxxx) bjp(xxxx)
 .addinstr B_CALL *       EF   3 NOP 1

APP_PUSH_ERRORH .EQU	59h
APP_POP_ERRORH	.EQU	5Ch
#define AppOnErr(xxxx) ld hl,xxxx \ call APP_PUSH_ERRORH
#define AppOffErr call APP_POP_ERRORH

#IFDEF APP
progstart =$4000
#ELSE
progstart	=9D95h
#ENDIF

rOP1TOOP2	.EQU  08h
rFINDSYM	.EQU  10h
rPUSHREALO1	.EQU  18h
rMOV9TOOP1	.EQU  20h
rFPADD		.EQU  30h

;======================================================================
;           System Variable Equates
;======================================================================
;       Entry Points : RclSysTok, StoSysTok
;
XSCLt		EQU  2
YSCLt		EQU  3
XMINt		EQU  0Ah
XMAXt		EQU  0Bh
YMINt		EQU  0Ch
YMAXt		EQU  0Dh
TMINt		EQU  0Eh
TMAXt		EQU  0Fh
THETMINt	EQU  10h
THETMAXt	EQU  11h
TBLMINt		EQU  1Ah
PLOTSTARTt	EQU  1Bh
NMAXt		EQU  1Dh
NMINt		EQU  1Fh
TBLSTEPt	EQU  21h
TSTEPt		EQU  22h
THETSTEPt	EQU  23h
DELTAXt		EQU  26h
DELTAYt		EQU  27h
XFACTt		EQU  28h
YFACTt		EQU  29h
FINNt		EQU  2Bh
FINIt		EQU  2Ch
FINPVt		EQU  2Dh
FINPMTt		EQU  2Eh
FINFVt		EQU  2Fh
FINPYt		EQU  30h
FINCYt		EQU  31h
PLOTSTEPt	EQU  34h
XRESt		EQU  36h
;

;======================================================================
;           Run indicators
;======================================================================
;
busyPause	EQU	10101010b
busyNormal	EQU	11110000b

;======================================================================
;           Entry points  
;======================================================================
;
_AbsO1O2Cp	EQU 410Eh
_AbsO1PAbsO2	EQU 405Ah
_ACos		EQU 40DEh
_ACosH		EQU 40F0h
_ACosRad	EQU 40D2h
_AdrLEle	EQU 462Dh
_AdrMEle	EQU 4609h
_AdrMRow	EQU 4606h
_AllEq		EQU 4876h
_AllocFPS	EQU 43A5h
_AllocFPS1	EQU 43A8h
_Angle		EQU 4102h
_AnsName	EQU 4B52h
_ApdSetup	EQU 4C93h
_AppGetCalc	EQU 4C78h
_AppGetCbl	EQU 4C75h
_AppInit	EQU 404Bh  ;***
_Arc_Unarc	EQU 4FD8h
_ASin		EQU 40E4h
_ASinH		EQU 40EDh
_ASinRad	EQU 40DBh
_ATan		EQU 40E1h
_ATan2		EQU 40E7h
_ATan2Rad	EQU 40D8h
_ATanH		EQU 40EAh
_ATanRad	EQU 40D5h

_BinOPExec	EQU 4663h
_Bit_VertSplit	EQU 4FA8h
_BufClr		EQU 5074h
_BufCpy		EQU 5071h

_CAbs		EQU 4E97h
_CAdd		EQU 4E88h
_CanAlphIns	EQU 4C69h
_CDiv		EQU 4E94h
_CDivByReal	EQU 4EBBh
_CEtoX		EQU 4EA9h
_CFrac		EQU 4EC1h
_CheckSplitFlag	EQU 49F0h
_Chk_Batt_Low	EQU 50B3h  ;***
_ChkFindSym	EQU 42F1h
_CIntgr		EQU 4EC4h
_CircCmd	EQU 47D4h
_CkInt		EQU 4234h
_CkOdd		EQU 4237h
_CkOP1C0	EQU 4225h
_CkOP1Cplx	EQU 40FCh
_CkOP1FP0	EQU 4228h
_CkOP1Pos	EQU 4258h
_CkOP1Real	EQU 40FFh
_CkOP2FP0	EQU 422Bh
_CkOP2Pos	EQU 4255h
_CkOP2Real	EQU 42DFh
_CkPosInt	EQU 4231h
_CkValidNum	EQU 4270h
_CleanAll	EQU 4A50h
_ClearRect	EQU 4D5Ch
_ClearRow	EQU 4CEDh
_CLine		EQU 4798h
_CLineS		EQU 479Bh
_CLN		EQU 4EA0h
_CLog		EQU 4EA3h
_CloseEditBuf	EQU 48D3h  ;***
_CloseEditBufNoR	EQU 476Eh
_CloseEditEqu	EQU 496Ch  ;***
_CloseProg	EQU 4A35h
_ClrGraphRef	EQU 4A38h
_ClrLCD		EQU 4543h
_ClrLCDFull	EQU 4540h
_ClrLp		EQU 41D1h
_ClrOP1S	EQU 425Eh
_ClrOP2S	EQU 425Bh
_ClrScrn	EQU 4549h
_ClrScrnFull	EQU 4546h
_ClrTxtShd	EQU 454Ch
_CMltByReal	EQU 4EB8h
_CmpSyms	EQU 4A4Ah
_CMult		EQU 4E8Eh
_Conj		EQU 4EB5h
_ConvDim	EQU 4B43h
_ConvDim00	EQU 4B46h
_ConvLcToLr	EQU 4A23h
_ConvLrToLc	EQU 4A56h
_ConvOP1	EQU 4AEFh
_COP1Set0	EQU 4105h
_Cos		EQU 40C0h
_CosH		EQU 40CCh
_CpHLDE		EQU 400Ch
_CPoint		EQU 4DC8h
_CPointS	EQU 47F5h
_CpOP1OP2	EQU 4111h
_CpOP4OP3	EQU 4108h
_CpyO1ToFPS1	EQU 445Ch
_CpyO1ToFPS2	EQU 446Bh
_CpyO1ToFPS3	EQU 4477h
_CpyO1ToFPS4	EQU 4489h
_CpyO1ToFPS5	EQU 4483h
_CpyO1ToFPS6	EQU 447Dh
_CpyO1ToFPS7	EQU 4480h
_CpyO1ToFPST	EQU 444Ah
_CpyO2ToFPS1	EQU 4459h
_CpyO2ToFPS2	EQU 4462h
_CpyO2ToFPS3	EQU 4474h
_CpyO2ToFPS4	EQU 4486h
_CpyO2ToFPST	EQU 4444h
_CpyO3ToFPS1	EQU 4453h
_CpyO3ToFPS2	EQU 4465h
_CpyO3ToFPST	EQU 4441h
_CpyO5ToFPS1	EQU 4456h
_CpyO5ToFPS3	EQU 4471h
_CpyO6ToFPS2	EQU 4468h
_CpyO6ToFPST	EQU 4447h
_CpyStack	EQU 4429h
_CpyTo1FPS1	EQU 4432h
_CpyTo1FPS10	EQU 43F3h
_CpyTo1FPS11	EQU 43D8h
_CpyTo1FPS2	EQU 443Bh
_CpyTo1FPS3	EQU 4408h
_CpyTo1FPS4	EQU 440Eh
_CpyTo1FPS5	EQU 43DEh
_CpyTo1FPS6	EQU 43E4h
_CpyTo1FPS7	EQU 43EAh
_CpyTo1FPS8	EQU 43EDh
_CpyTo1FPS9	EQU 43F6h
_CpyTo1FPST	EQU 4423h
_CpyTo2FPS1	EQU 442Fh
_CpyTo2FPS2	EQU 4438h
_CpyTo2FPS3	EQU 4402h
_CpyTo2FPS4	EQU 43F9h
_CpyTo2FPS5	EQU 43DBh
_CpyTo2FPS6	EQU 43E1h
_CpyTo2FPS7	EQU 43E7h
_CpyTo2FPS8	EQU 43F0h
_CpyTo2FPST	EQU 4420h
_CpyTo3FPS1	EQU 442Ch
_CpyTo3FPS2	EQU 4411h
_CpyTo3FPST	EQU 441Dh
_CpyTo4FPST	EQU 441Ah
_CpyTo5FPST	EQU 4414h
_CpyTo6FPS2	EQU 43FFh
_CpyTo6FPS3	EQU 43FCh
_CpyTo6FPST	EQU 4417h
_CpyToFPS1	EQU 445Fh
_CpyToFPS2	EQU 446Eh
_CpyToFPS3	EQU 447Ah
_CpyToFPST	EQU 444Dh
_CpyToStack	EQU 4450h
_Create0Equ	EQU 432Ah
_CreateAppVar	EQU 4E6Ah
_CreateCList	EQU 431Bh
_CreateCplx	EQU 430Ch
_CreateEqu	EQU 4330h
_CreatePair	EQU 4B0Dh
_CreatePict	EQU 4333h
_CreateProg	EQU 4339h
_CreateProtProg	EQU 4E6Dh
_CreateReal	EQU 430Fh
_CreateRList	EQU 4315h
_CreateRMat	EQU 4321h
_CreateStrng	EQU 4327h
_CRecip		EQU 4E91h
_CSqRoot	EQU 4E9Dh
_CSquare	EQU 4E8Bh
_CSub		EQU 4E85h
_CTenX		EQU 4EA6h
_CTrunc		EQU 4EBEh
_Cube		EQU 407Bh
_CursorOff	EQU 45BEh
_CursorOn	EQU 45C4h
_CXrootY	EQU 4EACh
_CYtoX		EQU 4EB2h

_DarkLine	EQU 47DDh
_DarkPnt	EQU 47F2h
_DataSize	EQU 436Ch
_DataSizeA	EQU 4369h
_DeallocFPS	EQU 439Fh
_DeallocFPS1	EQU 43A2h
_DecO1Exp	EQU 4267h
_DelListEl	EQU 4A2Fh
_DelMem		EQU 4357h
_DelRes		EQU 4A20h
_DelVar		EQU 4351h
_DelVarArc	EQU 4FC6h
_DelVarNoArc	EQU 4FC9h
_DisableApd	EQU 4C84h
_Disp		EQU 4F45h
_DispDone	EQU 45B5h
_DispEOL	EQU 45A6h
_DispHL		EQU 4507h
_DisplayImage	EQU 4D9Bh
_DispMenuTitle	EQU 5065h
_DispOP1A	EQU 4BF7h
_DivHLBy10	EQU 400Fh
_DivHLByA	EQU 4012h
_DrawCirc2	EQU 4C66h
_DrawCmd	EQU 48C1h
_DrawRectBorder	EQU 4D7Dh
_DrawRectBorderClear	EQU 4D8Ch
_DToR		EQU 4075h

_EditProg	EQU 4A32h
_EnableApd	EQU 4C87h
_EnoughMem	EQU 42FDh
_EOP1NotReal	EQU 4279h
_Equ_or_NewEqu	EQU 42C4h
_EraseEOL	EQU 4552h
_EraseRectBorder	EQU 4D86h
_ErrArgument	EQU 44ADh
_ErrBadGuess	EQU 44CBh
_ErrBreak	EQU 44BFh
_ErrD_OP1_0	EQU 42D3h
_ErrD_OP1_LE_0	EQU 42D0h
_ErrD_OP1Not_R	EQU 42CAh
_ErrD_OP1NotPos	EQU 42C7h
_ErrD_OP1NotPosInt	EQU 42CDh
_ErrDataType	EQU 44AAh
_ErrDimension	EQU 44B3h
_ErrDimMismatch	EQU 44B0h
_ErrDivBy0	EQU 4498h
_ErrDomain	EQU 449Eh
_ErrIncrement	EQU 44A1h
_ErrInvalid	EQU 44BCh
_ErrIterations	EQU 44C8h
_ErrLinkXmit	EQU 44D4h
_ErrMemory	EQU 44B9h
_ErrNon_Real	EQU 44A4h
_ErrNonReal	EQU 4A8Ch
_ErrNotEnoughMem	EQU 448Ch
_ErrOverflow	EQU 4495h
_ErrSignChange	EQU 44C5h
_ErrSingularMat	EQU 449Bh
_ErrStat	EQU 44C2h
_ErrStatPlot	EQU 44D1h
_ErrSyntax	EQU 44A7h
_ErrTolTooSmall	EQU 44CEh
_ErrUndefined	EQU 44B6h
_EToX		EQU 40B4h
_Exch9		EQU 43D5h
_ExLp		EQU 4222h
_ExpToHex	EQU 424Fh

_Factorial	EQU 4B85h
_FillRect	EQU 4D62h
_FillRectPattern	EQU 4D89h
_Find_Parse_Formula	EQU 4AF2h
_FindAlphaDn	EQU 4A47h
_FindAlphaUp	EQU 4A44h
_FindApp		EQU 4C4Eh
_FindAppDn		EQU 4C4Bh
_FindAppNumPages	EQU 509Bh
_FindAppUp		EQU 4C48h
_FindSym	EQU 42F4h
_FiveExec	EQU 467Eh
_FixTempCnt	EQU 4A3Bh
_FlashToRam	EQU 5017h
_ForceFullScreen	EQU 508Fh
_FormBase	EQU 50AAh
_FormDCplx	EQU 4996h
_FormEReal	EQU 4990h
_FormReal	EQU 4999h
_FourExec	EQU 467Bh
_FPAdd		EQU 4072h
_FPDiv		EQU 4099h
_FPMult		EQU 4084h
_FPRecip	EQU 4096h
_FPSquare	EQU 4081h
_FPSub		EQU 406Fh
_Frac		EQU 4093h

_Get_Tok_Strng	EQU 4594h  ;***
_GetBaseVer	EQU 4C6Fh
_GetCSC		EQU 4018h
_GetKey		EQU 4972h
_GetLToOP1	EQU 4636h
_GetMToOP1	EQU 4615h
_GetSysInfo 	EQU 50DDh  ; v. 1.13+ only
_GetTokLen	EQU 4591h  ;***
_GrBufClr	EQU 4BD0h
_GrBufCpy	EQU 486Ah
_GrphCirc	EQU 47D7h

_HLTimes9	EQU 40F9h
_HorizCmd	EQU 48A6h
_HTimesL	EQU 4276h

_IBounds	EQU 4C60h
_IBoundsFull	EQU 4D98h
_ILine		EQU 47E0h
_IncLstSize	EQU 4A29h
_InsertList	EQU 4A2Ch
_InsertMem	EQU 42F7h
_Int		EQU 40A5h
_Intgr		EQU 405Dh
_InvCmd		EQU 48C7h
_InvertRect	EQU 4D5Fh
_InvOP1S	EQU 408Dh
_InvOP1SC	EQU 408Ah
_InvOP2S	EQU 4090h
_InvSub		EQU 4063h
_IOffset	EQU 4C63h
_IPoint		EQU 47E3h
_IsA2ByteTok	EQU 42A3h  ;***
_IsEditEmpty	EQU 492Dh  ;***

_JError		EQU 44D7h
_JErrorNo	EQU 4000h
_JForceCmdNoChar	EQU 4027h
_JForceGraphKey	EQU 5005h
_JForceGraphNoKey	EQU 5002h

_KeyToString	EQU 45CAh

_LCD_BUSY	EQU 4051h  ;***
_LCD_DRIVERON	equ 4978h  ;***
_LdHLind	equ 4009h  ;***
_LineCmd	EQU 48ACh
_LnX		EQU 40ABh
_Load_SFont	EQU 4783h
_LoadCIndPaged	EQU 501Dh
_LoadDEIndPaged	EQU 501Ah
_LoadPattern	EQU 4CB1h
_LogX		EQU 40AEh

_Max		EQU 4057h
_MemChk		EQU 42E5h
_MemClear	EQU 4C30h
_MemSet		EQU 4C33h
_Min		EQU 4054h
_Minus1		EQU 406Ch
_Mov10B		EQU 415Ch
_Mov18B		EQU 47DAh
_Mov7B		EQU 4168h
_Mov8B		EQU 4165h
_Mov9B		EQU 415Fh
_Mov9OP1OP2	EQU 417Dh
_Mov9OP2Cp	EQU 410Bh
_Mov9ToOP1	EQU 417Ah
_Mov9ToOP2	EQU 4180h
_MovFrOP1	EQU 4183h

_NewLine	EQU 452Eh  ;***
_NZIf83Plus	EQU 50E0h  ; v. 1.13+ only

_OneVar		EQU 4BA3h
_OP1ExOP2	EQU 421Fh
_OP1ExOP3	EQU 4219h
_OP1ExOP4	EQU 421Ch
_OP1ExOP5	EQU 420Dh
_OP1ExOP6	EQU 4210h
_OP1ExpToDec	EQU 4252h
_OP1Set0	EQU 41BFh
_OP1Set1	EQU 419Bh
_OP1Set2	EQU 41A7h
_OP1Set3	EQU 41A1h
_OP1Set4	EQU 419Eh
_OP1ToOP2	EQU 412Fh
_OP1ToOP3	EQU 4123h
_OP1ToOP4	EQU 4117h
_OP1ToOP5	EQU 4153h
_OP1ToOP6	EQU 4150h
_OP2ExOP4	EQU 4213h
_OP2ExOP5	EQU 4216h
_OP2ExOP6	EQU 4207h
_OP2Set0	EQU 41BCh
_OP2Set1	EQU 41ADh
_OP2Set2	EQU 41AAh
_OP2Set3	EQU 4198h
_OP2Set4	EQU 4195h
_OP2Set5	EQU 418Fh
_OP2Set60	EQU 4AB0h
_OP2Set8	EQU 418Ch
_OP2SetA	EQU 4192h
_OP2ToOP1	EQU 4156h
_OP2ToOP3	EQU 416Eh
_OP2ToOP4	EQU 411Ah
_OP2ToOP5	EQU 414Ah
_OP2ToOP6	EQU 414Dh
_OP3Set0	EQU 41B9h
_OP3Set1	EQU 4189h
_OP3Set2	EQU 41A4h
_OP3ToOP1	EQU 413Eh
_OP3ToOP2	EQU 4120h
_OP3ToOP4	EQU 4114h
_OP3ToOP5	EQU 4147h
_OP4Set0	EQU 41B6h
_OP4Set1	EQU 4186h
_OP4ToOP1	EQU 4138h
_OP4ToOP2	EQU 411Dh
_OP4ToOP3	EQU 4171h
_OP4ToOP5	EQU 4144h
_OP4ToOP6	EQU 4177h
_OP5ExOP6	EQU 420Ah
_OP5Set0	EQU 41B3h
_OP5ToOP1	EQU 413Bh
_OP5ToOP2	EQU 4126h
_OP5ToOP3	EQU 4174h
_OP5ToOP4	EQU 412Ch
_OP5ToOP6	EQU 4129h
_OP6ToOP1	EQU 4135h
_OP6ToOP2	EQU 4132h
_OP6ToOP5	EQU 4141h
_OutputExpr	EQU 4BB2h

_PagedGet	EQU 5023h
_ParseInp	EQU 4A9Bh
_PDspGrph	EQU 48A3h
_PixelTest	EQU 48B5h
_Plus1		EQU 4069h
_PointCmd	EQU 48B2h
_PointOn	EQU 4C39h
_PopMCplxO1	EQU 436Fh  ;***
_PopOP1		EQU 437Eh
_PopOP3		EQU 437Bh
_PopOP5		EQU 4378h
_PopReal	EQU 4393h
_PopRealO1	EQU 4390h
_PopRealO2	EQU 438Dh
_PopRealO3	EQU 438Ah
_PopRealO4	EQU 4387h
_PopRealO5	EQU 4384h
_PopRealO6	EQU 4381h
_PosNo0Int	EQU 422Eh
_PToR		EQU 40F3h
_PushMCplxO1	EQU 43CFh  ;***
_PushMCplxO3	EQU 43C6h  ;***
_PushOP1	EQU 43C9h
_PushOP3	EQU 43C3h
_PushOP5	EQU 43C0h
_PushReal	EQU 43BDh
_PushRealO1	EQU 43BAh
_PushRealO2	EQU 43B7h
_PushRealO3	EQU 43B4h
_PushRealO4	EQU 43B1h
_PushRealO5	EQU 43AEh
_PushRealO6	EQU 43ABh
_PutAway	EQU 403ch  ;***
_PutC		EQU 4504h
_PutMap		EQU 4501h
_PutPS		EQU 4510h
_PutPSB		EQU 450dh  ;***
_PutS		EQU 450Ah
_PutTokString	EQU 4960h
_PutToL		EQU 4645h
_PutToMat	EQU 461Eh

_RandInit	EQU 4B7Fh
_Random		EQU 4B79h
_Rcl_StatVar	EQU 42DCh
_RclAns		EQU 4AD7h
_RclGDB2	EQU 47D1h
_RclN		EQU 4ADDh
_RclSysTok	EQU 4AE6h
_RclVarSym	EQU 4AE3h
_RclX		EQU 4AE0h
_RclY		EQU 4ADAh
_Rec1stByte	EQU 4EFAh
_Rec1stByteNC	EQU 4EFDh
_RecAByteIO	EQU 4F03h
_RedimMat	EQU 4A26h
_Regraph	EQU 488Eh
_ReleaseBuffer	EQU 4771h  ;***
_ReloadAppEntryVecs	EQU 4C36h
_RestoreDisp	EQU 4870h
_RName		EQU 427Fh
_RndGuard	EQU 409Fh
_RnFx		EQU 40A2h
_Round		EQU 40A8h
_RToD		EQU 4078h
_RToP		EQU 40F6h
_RunIndicOff	EQU 4570h
_RunIndicOn	EQU 456Dh

_SaveDisp	EQU 4C7Bh
_SendAByte	EQU 4EE5h
_SendVarCmd	EQU 4A14h ;***
_SetAllPlots	EQU 4FCCh
_SetExSpeed 	EQU 50BFh ; v. 1.13+ only
_SetFuncM	EQU 4840h
_SetNorm_Vals	EQU 49FCh
_SetParM	EQU 4849h
_SetPolM	EQU 4846h
_SetSeqM	EQU 4843h
_SetTblGraphDraw	EQU 4C00h
_SetupPagedPtr	EQU 5020h
_SetXXOP1	EQU 478Ch
_SetXXOP2	EQU 478Fh
_SetXXXXOP2	EQU 4792h
_SFont_Len	EQU 4786h
_ShRAcc		equ 41D4h
_Sin		EQU 40BDh
_SinCosRad	EQU 40BAh
_SinH		EQU 40CFh
_SinHCosH	EQU 40C6h
_SqRoot		EQU 409Ch
_SrchVLstDn	EQU 4F12h
_SrchVLstUp	EQU 4F0Fh
_SStringLength	EQU 4CB4h
_StMatEl	EQU 4AE9h
_StoAns		EQU 4ABFh
_StoGDB2	EQU 47CEh
_StoN		EQU 4ACBh
_StoOther	EQU 4AD4h
_StoR		EQU 4AC5h
_StoRand	EQU 4B7Ch
_StoSysTok	EQU 4ABCh
_StoT		EQU 4ACEh
_StoTheta	EQU 4AC2h
_StoX		EQU 4AD1h
_StoY		EQU 4AC8h
_StrCopy	EQU 44E3h
_StrLength	EQU 4C3Fh   

_Tan		EQU 40C3h
_TanH		EQU 40C9h
_TanLnF		EQU 48BBh
_TenX		EQU 40B7h
_ThetaName	EQU 427Ch
_ThreeExec	EQU 4675h
_Times2		EQU 4066h
_TimesPt5	EQU 407Eh
_TName		EQU 428Eh
_ToFrac		EQU 4657h
_Trunc		EQU 4060h
_TwoVarSet	EQU 50D4h  ; v. 1.13+ only

_UCLineS	EQU 4795h
_UnLineCmd	EQU 48AFh
_UnOPExec	EQU 4672h

_VertCmd	EQU 48A9h
_VPutMap	EQU 455Eh
_VPutS		EQU 4561h
_VPutSN		EQU 4564h
_VtoWHLDE	EQU 47FBh


_XftoI		EQU 4804h
_Xitof		EQU 47FEh
_XName		EQU 4288h
_XRootY		EQU 479Eh

_YftoI		EQU 4801h
_YName		EQU 428Bh
_YToX		EQU 47A1h

_Zero16D	EQU 41B0h
_ZeroOP		EQU 41CEh
_ZeroOP1	EQU 41C5h
_ZeroOP2	EQU 41C8h
_ZeroOP3	EQU 41CBh
_ZmDecml	EQU 484Fh
_ZmFit		EQU 485Bh
_ZmInt		EQU 484Ch
_ZmPrev		EQU 4852h
_ZmSquare	EQU 485Eh
_ZmStats	EQU 47A4h
_ZmTrig		EQU 4861h
_ZmUsr		EQU 4855h
_ZooDefault	EQU 4867h
;
;======================================================================
;           System-defined RAM Variable Address Equates
;======================================================================
;

ramStart             EQU  8000h
appData              EQU  8000h
ramCode              EQU  8100h
ramCodeEnd           EQU  822Fh
baseAppBrTab         EQU  8230h
bootTemp             EQU  8251h
appSearchPage        EQU  82A3h
tempSwapArea         EQU  82A5h
appID                EQU  838Dh
ramReturnData        EQU  83EDh
arcInfo              EQU  83EEh
savedArcInfo         EQU  8406h
appInfo              EQU  8432h
appBank_jump         EQU  843Ch
appPage              EQU  843Eh
kbdScanCode          EQU  843Fh
kbdKey               EQU  8444h
kbdGetKy             EQU  8445h
keyExtend            EQU  8446h
contrast             EQU  8447h
apdSubTimer          EQU  8448h
apdTimer             EQU  8449h
curTime              EQU  844Ah
curRow               EQU  844Bh
curCol               EQU  844Ch
curOffset            EQU  844Dh
curUnder             EQU  844Eh
curY                 EQU  844Fh
curType              EQU  8450h
curXRow              EQU  8451h
prevDData            EQU  8452h
lFont_record         EQU  845Ah
sFont_record         EQU  8462h
tokVarPtr            EQU  846Ah
tokLen               EQU  846Ch
indicMem             EQU  846Eh
indicCounter         EQU  8476h
indicBusy            EQU  8477h
OP1                  EQU  8478h
OP1M                 EQU  847Ah
OP2                  EQU  8483h
OP2M                 EQU  8485h
OP2EXT               EQU  848Ch
OP3                  EQU  848Eh
OP3M                 EQU  8490h
OP4                  EQU  8499h
OP4M                 EQU  849Bh
OP5                  EQU  84A4h
OP5M                 EQU  84A6h
OP6                  EQU  84AFh
OP6M                 EQU  84B1h
OP6EXT               EQU  84B8h
progToEdit           EQU  84BFh
nameBuff             EQU  84C7h
equ_edit_save        EQU  84D2h
iMathPtr1            EQU  84D3h
iMathPtr2            EQU  84D5h
iMathPtr3            EQU  84D7h
iMathPtr4            EQU  84D9h
iMathPtr5            EQU  84DBh
chkDelPtr1           EQU  84DDh
chkDelPtr2           EQU  84DFh
insDelPtr            EQU  84E1h
upDownPtr            EQU  84E3h
fOutDat              EQU  84E5h
asm_data_ptr1        EQU  84EBh
asm_data_ptr2        EQU  84EDh
asm_sym_ptr1         EQU  84EFh
asm_sym_ptr2         EQU  84F1h
asm_ram              EQU  84F3h
asm_ind_call         EQU  8507h
textShadow           EQU  8508h
textShadCur          EQU  8588h
textShadTop          EQU  858Ah
textShadAlph         EQU  858Bh
textShadIns          EQU  858Ch
cxMain               EQU  858Dh
cxPPutAway           EQU  858Fh
cxPutAway            EQU  8591h
cxRedisp             EQU  8593h
cxErrorEP            EQU  8595h
cxSizeWind           EQU  8597h
cxPage               EQU  8599h
cxCurApp             EQU  859Ah
cxPrev               EQU  859Bh
monQH                EQU  85AAh
monQT                EQU  85ABh
monQueue             EQU  85ACh
onSP                 EQU  85BCh
promptRow            EQU  85C0h
promptCol            EQU  85C1h
promptIns            EQU  85C2h
promptShift          EQU  85C3h
promptRet            EQU  85C4h
promptValid          EQU  85C6h
promptTop            EQU  85C8h
promptCursor         EQU  85CAh
promptTail           EQU  85CCh
promptBtm            EQU  85CEh
varType              EQU  85D0h
varCurrent           EQU  85D1h
varClass             EQU  85D9h
menuActive           EQU  85DCh
menuAppDepth         EQU  85DDh
MenuCurrent          EQU  85DEh
ProgCurrent          EQU  85E8h
userMenuSA           EQU  85FEh
ioPrompt             EQU  865Fh
dImageWidth          EQU  8660h
RectFillPHeight      equ  8660h
RectFillPWidth       equ  8661h
RectFillPattern      equ  8662h
ioFlag               EQU  8670h
sndRecState          EQU  8672h
ioErrState           EQU  8673h
header               EQU  8674h
ioData               EQU  867Dh
ioNewData            EQU  8689h
bakHeader            EQU  868Bh
penCol               EQU  86D7h
penRow               EQU  86D8h
rclQueue             EQU  86D9h
rclQueueEnd          EQU  86DBh
errNo                EQU  86DDh
errSP                EQU  86DEh
errOffset            EQU  86E0h
saveSScreen          EQU  86ECh
bstCounter           EQU  89EEh
flags                EQU  89F0h
statVars             EQU  8A3Ah
anovaf_vars          EQU  8C17h
infVars              EQU  8C4Dh
curGStyle            EQU  8D17h
curGY                EQU  8D18h
curGX                EQU  8D19h
curGY2               EQU  8D1Ah
curGX2               EQU  8D1Bh
freeSaveY            EQU  8D1Ch
freeSaveX            EQU  8D1Dh
XOffset              EQU  8DA1h
YOffset              EQU  8DA2h
lcdTallP             EQU  8DA3h
pixWideP             EQU  8DA4h
pixWide_m_1          EQU  8DA5h
pixWide_m_2          EQU  8DA6h
lastEntryPTR         EQU  8DA7h
lastEntryStk         EQU  8DA9h
numLastEntries       EQU  8E29h
currLastEntry        EQU  8E2Ah
curInc               EQU  8E67h
uXmin                EQU  8E7Eh
uXmax                EQU  8E87h
uXscl                EQU  8E90h
uYmin                EQU  8E99h
uYmax                EQU  8EA2h
uYscl                EQU  8EABh
uThetMin             EQU  8EB4h
uThetMax             EQU  8EBDh
uThetStep            EQU  8EC6h
uTmin                EQU  8ECFh
uTmax                EQU  8ED8h
uTStep               EQU  8EE1h
uPlotStart           EQU  8EEAh
unMax                EQU  8EF3h
uu0                  EQU  8EFCh
uv0                  EQU  8F05h
unMin                EQU  8F0Eh
uu02                 EQU  8F17h
uv02                 EQU  8F20h
uw0                  EQU  8F29h
uPlotStep            EQU  8F32h
uXres                EQU  8F3Bh
uw02                 EQU  8F44h
Xmin                 EQU  8F50h
Xmax                 EQU  8F59h
Xscl                 EQU  8F62h
Ymin                 EQU  8F6Bh
Ymax                 EQU  8F74h
Yscl                 EQU  8F7Dh
ThetaMin             EQU  8F86h
ThetaMax             EQU  8F8Fh
ThetaStep            EQU  8F98h
TminPar              EQU  8FA1h
TmaxPar              EQU  8FAAh
Tstep                EQU  8FB3h
PlotStart            EQU  8FBCh
nMax                 EQU  8FC5h
u0                   EQU  8FCEh
v0                   EQU  8FD7h
nMin                 EQU  8FE0h
u02                  EQU  8FE9h
v02                  EQU  8FF2h
w0                   EQU  8FFBh
PlotStep             EQU  9004h
XresO                EQU  900Dh
w02                  EQU  9016h
un1                  EQU  901Fh
un2                  EQU  9028h
vn1                  EQU  9031h
vn2                  EQU  903Ah
wn1                  EQU  9043h
wn2                  EQU  904Ch
fin_N                EQU  9055h
fin_I                EQU  905Eh
fin_PV               EQU  9067h
fin_PMT              EQU  9070h
fin_FV               EQU  9079h
fin_PY               EQU  9082h
fin_CY               EQU  908Bh
cal_N                EQU  9094h
cal_I                EQU  909Dh
cal_PV               EQU  90A6h
cal_PMT              EQU  90AFh
cal_FV               EQU  90B8h
cal_PY               EQU  90C1h
smallEditRAM         EQU  90D3h
XFact                EQU  913Fh
YFact                EQU  9148h
Xres_int             EQU  9151h
deltaX               EQU  9152h
deltaY               EQU  915Bh
shortX               EQU  9164h
shortY               EQU  916Dh
lower                EQU  9176h
upper                EQU  917Fh
XOutSym              EQU  918Ch
XOutDat              EQU  918Eh
YOutSym              EQU  9190h
YOutDat              EQU  9192h
inputSym             EQU  9194h
inputDat             EQU  9196h
prevData             EQU  9198h
TblMin               EQU  92B3h
TblStep              EQU  92BCh
P1Type		     EQU  92C9h
SavX1List	     EQU  92CAh
SavY1List	     EQU  92CFh
SavF1List	     EQU  92D4h
P1FrqOnOff	     EQU  92D9h
P2Type		     EQU  92DAh
SavX2List	     EQU  92DBh
SavY2List	     EQU  92E0h
SavF2List	     EQU  92E5h
P2FrqOnOff	     EQU  92EAh
P3Type		     EQU  92EBh	
SavX3List	     EQU  92ECh
SavY3List	     EQU  92F1h
SavF3List	     EQU  92F6h
P3FrqOnOff	     EQU  92FBh
plotSScreen          EQU  9340h
seed1                EQU  9640h
seed2                EQU  9649h
cmdShadow            EQU  966Eh
cmdShadCur           EQU  96EEh
cmdShadAlph          EQU  96F0h
cmdShadIns           EQU  96F1h
cmdCursor            EQU  96F2h
editTop              EQU  96F4h
editCursor           EQU  96F6h
editTail             EQU  96F8h
editBtm              EQU  96FAh
editSym              EQU  9706h
editDat              EQU  9708h
GY0		EQU 977Fh
GY1		EQU 9776h
GY2		EQU 9777h
GY3		EQU 9778h
GY4		EQU 9779h
GY5		EQU 977Ah
GY6		EQU 977Bh
GY7		EQU 977Ch
GY8		EQU 977Dh
GY9		EQU 977Eh
GX1		EQU 9780h
GX2		EQU 9781h
GX3		EQU 9782h
GX4		EQU 9783h
GX5		EQU 9784h
GX6		EQU 9785h
GU		EQU 978Ch
GV		EQU 978Dh
GW		EQU 978Eh
GR1		EQU 9786h
GR2		EQU 9787h
GR3		EQU 9788h
GR4		EQU 9789h
GR5		EQU 978Ah
GR6		EQU 978Bh
winTop               EQU  97A5h
winBtm               EQU  97A6h
winLeftEdge          EQU  97A7h
winLeft              EQU  97A8h
winAbove             EQU  97AAh
winRow               EQU  97ACh
winCol               EQU  97AEh
fmtDigits            EQU  97B0h
fmtString            EQU  97B1h
fmtConv              EQU  97F2h
fmtLeft              EQU  9804h
fmtIndex             EQU  9806h
fmtMatSym            EQU  9808h
fmtMatMem            EQU  980Ah
EQS                  EQU  980Ch
tSymPtr1             EQU  9818h
tSymPtr2             EQU  981Ah
chkDelPtr3           EQU  981Ch
chkDelPtr4           EQU  981Eh
tempMem              EQU  9820h
fpBase               EQU  9822h
FPS                  EQU  9824h
OPBase               EQU  9826h
OPS                  EQU  9828h
pTempCnt             EQU  982Ah
cleanTmp             EQU  982Ch
pTemp                EQU  982Eh
progPtr              EQU  9830h
newDataPtr           EQU  9832h
pagedCount           EQU  9834h
pagedPN              EQU  9835h
pagedGetPtr          EQU  9836h
pagedPutPtr          EQU  9838h
pagedBuf             EQU  983Ah
appErr1              EQU  984Dh
appErr2              EQU  985Ah
flashByte1           EQU  9867h
flashByte2           EQU  9868h
freeArcBlock         EQU  9869h
arcPage              EQU  986Bh
arcPtr               EQU  986Ch
appRawKeyHandle      EQU  9870h
appBackUpScreen      EQU  9872h
customHeight         EQU  9B72h
localLanguage        EQU  9B73h
cursorHookPtr        EQU  9B7Ch
rawKeyHookPtr        EQU  9B84h
getKeyHookPtr        EQU  9B88h
fontHookPtr          EQU  9B9Ch
restartClr           EQU  9BD0h
localTokStr          EQU  9D65h
keyForStr            EQU  9D76h
keyToStrRam          EQU  9D77h
sedMonSp             EQU  9D88h
bpSave               EQU  9D8Ah
userMem              EQU  9D95h
symTable             EQU  0FE66h

vat = progptr

;
;======================================================================
;           System and State Flags
;======================================================================
;

trigFlags	EQU  0		;Trigonometry mode settings
trigDeg 	EQU    2	; 1=degrees, 0=radians

kbdFlags	EQU  0		;Keyboard scan
kbdSCR		EQU    3	; 1=scan code ready
kbdKeyPress	EQU    4	; 1=key has been pressed

doneFlags	EQU  0		;display "Done"
donePrgm	EQU    5	; 1=display "Done" after prgm

ioDelFlag	EQU  0
inDelete	EQU    0	;1 = DELETE SCREEN 

;----------------------------------------------------------------------
editFlags	EQU  1
editOpen	EQU    2	; 1=edit buffer is open

monFlags	EQU  1		;monitor flags
monAbandon	EQU    4	; 1=don't start any long process
				; in put away (#715)
;----------------------------------------------------------------------
plotFlags	EQU  2		;plot generation flags
plotTrace	equ  0
plotLoc 	EQU    1	; 0=bkup & display, 1=display only
plotDisp	EQU    2	; 1=plot is in display, 0=text in display


grfModeFlags	EQU  2		;graph mode settings
grfFuncM	EQU    4	; 1=function graph
grfPolarM	EQU    5	; 1=polar graph
grfParamM	EQU    6	; 1=parametric graph
grfRecurM       EQU    7        ; 1=RECURSION graph

graphFlags	EQU  3
graphDraw	EQU    0	; 0=graph is valid, 1=redraw graph

grfDBFlags	EQU  4
grfDot		EQU    0	; 0=line, 1=dot
grfSimul	EQU    1	; 0=sequential, 1=simultaneous
grfGrid 	EQU    2	; 0=no grid, 1=grid
grfPolar	EQU    3	; 0=rectangular, 1=polar coordinates
grfNoCoord	EQU    4	; 0=display coordinates, 1=off
grfNoAxis	EQU    5	; 0=axis, 1=no axis
grfLabel	EQU    6	; 0=off, 1=axis label

textFlags	EQU  5		;Text output flags
textEraseBelow	EQU    1	; 1=erase line below small char
textScrolled	EQU    2	; 1=screen scrolled
textInverse	EQU    3	; 1=display inverse bit-map
textInsMode	EQU    4	; 0=overstrike, 1=insert mode

ParsFlag2	EQU  7		;PARSER flags
numOP1		EQU    0	; 1=RESULT IN OP1, 0=NO RESULT

newDispF        EQU  8		;Derivative mode flags
preClrForMode   EQU    0	; 1=HELP BLINK ON MODE SCREEN

apdFlags	EQU  8		;Automatic power-down
apdAble 	EQU    2	; 1=APD enabled
apdRunning	EQU    3	; 1=APD clock running


web_err_mask    EQU  60h

onFlags 	EQU  9		;on key flags
onRunning	EQU  3		; 1 = calculator is running
onInterrupt	EQU    4	; 1=on key interrupt request

statFlags	EQU  9		;statistics flags
statsValid	EQU    6	; 1=stats are valid
statANSDISP	EQU  7		; 1=display stat results


fmtFlags	EQU  10		;numeric format flags
fmtExponent	EQU    0	; 1=show exponent, 0=no exponent
fmtEng		EQU    1	; 1=engineering notion, 0=scientific
fmtHex		EQU    2	; 1=hexadecimal
fmtOct		EQU    3	; 1=octal
fmtBin		EQU    4	; 1=binary
;
numMode         EQU  10
fmtReal         EQU    5
fmtRect         EQU    6
fmtPolar        EQU    7

realMode        EQU    5
rectMode        EQU    6
polarMode       EQU    7
;					;   if Hex and Oct both = 1
;					;   then Bin=0 means >Frac
;					;	 Bin=1 means >DMS
fmtBaseMask     EQU  00011100B		; mask to base flags
fmtBaseShift    EQU  2			; offset to base flags
;
;       CHECK IF THESE ARE USED BY NUMFORM,
;
;               EQU  6
;               EQU  7

fmtOverride	EQU  11		;copy of fmtFlags with conversion override

fmtEditFlags	EQU  12		;numeric editing flags
fmtEdit 	EQU    0	; 1=format number for editing

curFlags	EQU  12		;Cursor
curAble 	EQU    2	; 1=cursor flash is enabled
curOn		EQU    3	; 1=cursor is showing
curLock 	EQU    4	; 1=cursor is locked off

appFlags	EQU  13		;application flags
appWantIntrpt	EQU    0		; 1=want ON key interrupts
appTextSave	EQU    1		; 1=save characters in textShadow
appAutoScroll	EQU    2		; 1=auto-scroll text on last line
appMenus	EQU    3		; 1=process keys that bring up menus
					; 0=check Lock menu flag
appLockMenus	EQU    4		; 1=ignore menu keys
					; 0=switch to home screen and bring up menu
appCurGraphic	EQU    5		; 1=graphic cursor
appCurWord	EQU    6		; 1=text cursor covers entire word
appExit 	EQU    7		; 1=application handles [EXIT] key itself

appWantIntrptF	EQU	1<<appWantIntrpt
appTextSaveF	EQU	1<<appTextSave
appAutoScrollF	EQU	1<<appAutoScroll
appMenusF	EQU	1<<appMenus
appLockMenusF	EQU	1<<appLockMenus
appCurGraphicF	EQU	1<<appCurGraphic
appCurWordF	EQU	1<<appCurWord
appExitF	EQU	1<<appExit


seqFlags        EQU  15              ; Sequential Graph flags
webMode         EQU    0             ; 0 = NORMAL SEQ MODE, 1 = WEB MODE
webVert         EQU    1             ;
sequv           EQU    2             ; U vs V
seqvw           EQU    3             ; V vs W
sequw           EQU    4             ; U vs W


promptFlags	EQU  17		;prompt line flags
promptEdit	EQU    0	; 1=editing in prompt buffer

indicFlags	EQU  18		;Indicator flags
indicRun	EQU    0	; 1=run indicator ON
indicInUse	EQU    1	; indicator save area in use=1, free=0
indicOnly	EQU    2	; interrupt handler only checks run indicator

shiftFlags	EQU  18		;[2nd] and [ALPHA] flags
shift2nd	EQU    3	; 1=[2nd] has been pressed
shiftAlpha	EQU    4	; 1=[ALPHA] has been pressed
shiftLwrAlph	EQU    5	; 1=lower case, 0=upper case
shiftALock	EQU    6	; 1=alpha lock has been pressed
shiftKeepAlph	EQU    7	; 1=cannot cancel alpha shift


tblFlags        EQU  19		;table flags.
autoFill        EQU    4	; 1=prompt, 0=fillAuto
autoCalc        EQU    5	; 1=prompt, 0=CalcAuto
reTable         EQU    6	; 0=table is ok, 1=must recompute table.

sGrFlags	EQU  20
grfSplit        EQU    0	; 1=Split Graph, 0=Normal
vertSplit       EQU    1	; 1=Vertical (left-right) Split
grfSChanged     EQU    2	; 1=Graph just changed Split <-> normal
grfSplitOverride EQU   3	; 1 = ignore graph split flag if set
write_on_graph  EQU    4	; 1 = TEXT OR EQU WRITING TO GRAPH SCREEN
g_style_active  EQU    5	; 1 = GRAPH STYLES ARE ENABLED, USE THEM
cmp_mod_box     EQU    6	; 1 = DOING MOD BOX PLOT COMPUTATION
textWrite       EQU    7	; 1 = Small font writes to buffer
							; 0 = Small font writes to display
;

newIndicFlags   EQU  21
extraIndic      EQU    0
saIndic         EQU    1

newFlags2	    EQU  22
noRestores	    EQU  5

smartFlags      EQU  23
;
;----------------------------------------------------------------------
; Note: Fix these Equates if smartFlags are moved                            
;----------------------------------------------------------------------
;
smarter_mask    EQU  3
smarter_test    EQU  1
smartGraph      EQU  0
smartGraph_inv  EQU  1                                                 

more_Flags	EQU 26

No_Del_Stat	EQU 2
;----------------------------------------------------------------------
;           Available for ASM programming
;----------------------------------------------------------------------

asm_Flag1       EQU  33          ; ASM CODING
asm_Flag2       EQU  34          ; ASM CODING
asm_Flag3       EQU  35          ; ASM CODING

;======================================================================
;           Data Type Equates                                                    
;======================================================================
;
RealObj         EQU       0
ListObj         EQU       1
MatObj          EQU       2
EquObj          EQU       3
StrngObj        EQU       4
ProgObj         EQU       5
ProtProgObj     EQU       6
PictObj         EQU       7
GDBObj          EQU       8
UnknownObj      EQU       9
UnknownEquObj   EQU       0Ah
NewEquObj       EQU       0Bh
CplxObj         EQU       0Ch
CListObj        EQU       0Dh
UndefObj        EQU       0Eh
WindowObj       EQU       0Fh
ZStoObj         EQU       10h
TblRngObj       EQU       11h
LCDObj          EQU       12h
BackupObj       EQU       13h
AppObj		EQU	  14h		;application, only used in menus/link
AppVarObj	EQU	  15h		;application variable
TempProgObj     EQU       16h           ;program, home deletes when finished
GroupObj        EQU       17h           ;group.

;======================================================================
;           I/O equates                                                          
;======================================================================
;
D0D1_bits	EQU  03h
D0LD1L		EQU  03h
D0LD1H		EQU  01h
D0HD1L		EQU  02h
D0HD1H		EQU  00h
bport		EQU  0			; 4-BIT LINK PORT (I/O) 
;
;======================================================================
;	Interrupt equates
;======================================================================
;
iAll	EQU 1011b
intrptEnPort	equ 3 ; interrupt enable (in/out) 
;
;======================================================================
;	Memory paging equates
;======================================================================
;
memPageAPort	equ 6 ; memory page A (in/out)
;
;======================================================================
;	LCD Driver equates
;======================================================================
LCDinstPort	equ 10h
LCDdataPort equ 11h
;
;======================================================================
;	Graph style definitions
;======================================================================
;
GSline          equ     0		;Func, Param, and Polar modes default
GSthick         equ     1
GSabove         equ     2
GSbelow         equ     3
GSpath          equ     4
GSanimate       equ     5
GSdot           equ     6		;Recursive mode default

;======================================================================
;           DEVICE CODES                                                       
;======================================================================
;
;
TI82DEV		EQU  82h
PC82DEV		EQU  02h
MAC82DEV	EQU  12h
;
TI83FDEV	EQU  73h
LINK83FDEV	EQU  23h
;
TI83DEV		EQU  83h
PC83DEV		EQU  03h
MAC83DEV	EQU  13h
;
TI85DEV		EQU  95h      ; DIFF THEN REAL 85 SO ME TALK
PC85DEV		EQU  05h
MAC85DEV	EQU  15h
;
TI73DEV		EQU  74h      ; Device x3 is always an 83
PC73DEV		EQU  07h
MAC73DEV	EQU  17h
;
LINK73FDEV	EQU  23h
PC83FDEV	EQU  23h
;
;======================================================================
;           System Error Codes                                                  
;======================================================================
;
E_EDITF		EQU	7		;allow re-entering application
E_EDIT		EQU	1<<E_EDITF
E_Mask		EQU	7Fh

E_Overflow	EQU	1+E_EDIT
E_DivBy0	EQU	2+E_EDIT
E_SingularMat	EQU	3+E_EDIT
E_Domain	EQU	4+E_EDIT
E_Increment	EQU	5+E_EDIT
E_Break		EQU	6+E_EDIT
E_NonReal       EQU     7+E_EDIT
E_Syntax        EQU     8+E_EDIT
E_DataType      EQU     9+E_EDIT
E_Argument      EQU     10+E_EDIT
E_DimMismatch   EQU     11+E_EDIT
E_Dimension     EQU     12+E_EDIT
E_Undefined     EQU     13+E_EDIT
E_Memory        EQU     14+E_EDIT
E_Invalid       EQU     15+E_EDIT
E_IllegalNest   EQU     16+E_EDIT
E_Bound         EQU     17+E_EDIT
E_GraphRange    EQU     18+E_EDIT
E_Zoom          EQU     19+E_EDIT
E_Label         EQU     20
E_Stat          EQU     21
E_Solver        EQU     22+E_EDIT
E_Singularity   EQU     23+E_EDIT
E_SignChange    EQU     24+E_EDIT
E_Iterations    EQU     25+E_EDIT
E_BadGuess      EQU     26+E_EDIT
E_StatPlot      EQU     27
E_TolTooSmall   EQU     28+E_EDIT
E_Reserved      EQU     29+E_EDIT
E_Mode          EQU     30+E_EDIT
E_LnkErr        EQU     31+E_EDIT
E_LnkMemErr     EQU     32+E_EDIT
E_LnkTransErr   EQU     33+E_EDIT
E_LnkDupErr     EQU     34+E_EDIT
E_LnkMemFull    EQU     35+E_EDIT
E_Unknown       EQU     36+E_EDIT

E_Scale         EQU     37+E_EDIT
E_IdNotFound    EQU     38
E_NoMode        EQU     39+E_EDIT
E_Validation    EQU     40
E_Length        EQU     41+E_EDIT
E_Application   EQU     42+E_EDIT
E_AppErr1       EQU     43+E_EDIT
E_AppErr2       EQU     44+E_EDIT
E_ExpiredApp	EQU	45
E_BadAddr	EQU	46
E_Archived      EQU     47+E_EDIT
E_Version       EQU     48
E_ArchFull      EQU     49
E_Variable      EQU     50+E_EDIT
E_Duplicate     EQU     51+E_EDIT
HigErrNum       EQU     51


; obsolete error numbers34              ;first LINK error
E_LinkIOChkSum	EQU	34
E_LinkIOTimeOut	EQU	35
E_LinkIOBusy	EQU	36
E_LinkIOVer	EQU	37

;
;======================================================================
;           EQUATES TO RAM LOCATIONS FOR STAT VARS 
;======================================================================
;
;
FPLEN	EQU	9		;Length of a floating-point number.
StatN   EQU     statVars
XMean   EQU     StatN      + FPLEN
SumX    EQU     XMean      + FPLEN
SumXSqr EQU     SumX       + FPLEN
StdX    EQU     SumXSqr    + FPLEN
StdPX   EQU     StdX       + FPLEN
MinX    EQU     StdPX      + FPLEN
MaxX    EQU     MinX       + FPLEN
MinY    EQU     MaxX       + FPLEN
MaxY    EQU     MinY       + FPLEN
YMean   EQU     MaxY       + FPLEN
SumY    EQU     YMean      + FPLEN
SumYSqr EQU     SumY       + FPLEN
StdY    EQU     SumYSqr    + FPLEN
StdPY   EQU     StdY       + FPLEN
SumXY   EQU     StdPY      + FPLEN
Corr    EQU     SumXY      + FPLEN
MedX    EQU     Corr       + FPLEN
Q1      EQU     MedX       + FPLEN
Q3      EQU     Q1         + FPLEN
QuadA   EQU     Q3         + FPLEN
QuadB   EQU     QuadA      + FPLEN
QuadC   EQU     QuadB      + FPLEN
CubeD   EQU     QuadC      + FPLEN
QuartE  EQU     CubeD      + FPLEN
MedX1   EQU     QuartE     + FPLEN
MedX2   EQU     MedX1      + FPLEN
MedX3   EQU     MedX2      + FPLEN
MedY1   EQU     MedX3      + FPLEN
MedY2   EQU     MedY1      + FPLEN
MedY3   EQU     MedY2      + FPLEN
PStat   EQU     MedY3      + 2*FPLEN
ZStat   EQU     PStat      + FPLEN
TStat   EQU     ZStat      + FPLEN
ChiStat EQU     TStat      + FPLEN
FStat   EQU     ChiStat    + FPLEN
DF      EQU     FStat      + FPLEN
Phat    EQU     DF         + FPLEN
Phat1   EQU     Phat       + FPLEN
Phat2   EQU     Phat1      + FPLEN
MeanX1  EQU     Phat2      + FPLEN
StdX1   EQU     MeanX1     + FPLEN
StatN1  EQU     StdX1      + FPLEN
MeanX2  EQU     StatN1     + FPLEN
StdX2   EQU     MeanX2     + FPLEN
StatN2  EQU     StdX2      + FPLEN
StdXP2  EQU     StatN2     + FPLEN
SLower  EQU     StdXP2     + FPLEN
SUpper  EQU     SLower     + FPLEN
SStat   EQU     SUpper     + FPLEN

F_DF	EQU     anovaf_vars
F_SS	EQU     F_DF + FPLEN
F_MS	EQU     F_SS + FPLEN
E_DF	EQU     F_MS + FPLEN
E_SS	EQU     E_DF + FPLEN
E_MS	EQU     E_SS + FPLEN

#IFNDEF NOTOKENS
;======================================================================
;           Tokens
;======================================================================
;
EOSSTART        EQU       0
;
;
;   DISPLAY CONVERSIONS COME IMMEDIATELY BEFORE 'TSTORE'
;
DCONV   EQU     01h
;
tToDMS          EQU       DCONV       ; 01h
tToDEC          EQU       DCONV+1     ; 02h
tToAbc          EQU       DCONV+2     ; 03h    > A b/c
;
tStore          EQU       DCONV+3     ; 04h Lstore                01
;
tBoxPlot        EQU       05h
;
BRACKS          EQU       06h
;
tLBrack         EQU       BRACKS      ; 06h '['
tRBrack         EQU       BRACKS+1    ; 07h ']'
tLBrace         EQU       BRACKS+2    ; 08h '{'
tRBrace         EQU       BRACKS+3    ; 09h '}'
;
tPOST1          EQU       BRACKS+4
;
tFromRad        EQU       tPOST1       ; 0Ah Lradian
tFromDeg        EQU       tPOST1+1     ; 0Bh Ldegree
tRecip          EQU       tPOST1+2     ; 0Ch Linverse
tSqr            EQU       tPOST1+3     ; 0Dh Lsquare
tTrnspos        EQU       tPOST1+4     ; 0Eh Ltranspose
tCube           EQU       tPOST1+5     ; 0Fh '^3'
;
tLParen         EQU       10h         ; 10h '('
tRParen         EQU       11h         ; 11h ')'
;
;
IMUN            EQU       12h
;
tRound          EQU       IMUN        ; 12h 'round'
tPxTst          EQU       IMUN+1      ; 13h 'PXL-TEST'
tAug            EQU       IMUN+2      ; 14h 'aug'
tRowSwap        EQU       IMUN+3      ; 15h 'rSwap'
tRowPlus        EQU       IMUN+4      ; 16h 'rAdd'
tmRow           EQU       IMUN+5      ; 17h 'multR'
tmRowPlus       EQU       IMUN+6      ; 18h 'mRAdd'
tMax            EQU       IMUN+7      ; 19h 'max'
tMin            EQU       IMUN+8      ; 1Ah 'min'
tRToPr          EQU       IMUN+9      ; 1Bh 'R>Pr
tRToPo          EQU       IMUN+10     ; 1Ch 'R>Po
tPToRx          EQU       IMUN+11     ; 1Dh 'P>Rx
tPToRy          EQU       IMUN+12     ; 1Eh 'P>Ry
tMedian         EQU       IMUN+13     ; 1Fh 'MEDIAN
tRandM          EQU       IMUN+14     ; 20h 'randM'
tMean           EQU       IMUN+15     ; 21h
tRoot           EQU       IMUN+16     ; 22h 'ROOT'
tSeries         EQU       IMUN+17     ; 23h 'seq'
tFnInt          EQU       IMUN+18     ; 24h 'fnInt'
tNDeriv         EQU       IMUN+19     ; 25h 'fnIr'
tEvalF          EQU       IMUN+20     ; 26h
tFmin           EQU       IMUN+21     ; 27h
tFmax           EQU       IMUN+22     ; 28h
;
tEOSEL          EQU       IMUN+23
;
tSpace          EQU       tEOSEL       ; 29h   ' '
tString         EQU       tEOSEL+1     ; 2Ah  '"'
tComma          EQU       tEOSEL+2     ; 2Bh  ','
;
;
tii             EQU       2Ch         ; i
;
;======================================================================
;           Postfix Functions
;======================================================================
;
tPost           EQU       2Dh
;
tFact           EQU       tPost       ; 2Dh '!'
;
tCubicR         EQU       2Eh
tQuartR         EQU       2Fh

;======================================================================
;           Number Tokens 
;======================================================================
;
NUMS            EQU       30h
;
t0              EQU       NUMS        ; 30h
t1              EQU       NUMS+1      ; 31h
t2              EQU       NUMS+2      ; 32h
t3              EQU       NUMS+3      ; 33h
t4              EQU       NUMS+4      ; 34h
t5              EQU       NUMS+5      ; 35h
t6              EQU       NUMS+6      ; 36h
t7              EQU       NUMS+7      ; 37h
t8              EQU       NUMS+8      ; 38h
t9              EQU       NUMS+9      ; 39h
tDecPt          EQU       NUMS+10     ; 3Ah
tee             EQU       NUMS+11     ; 3Bh
;
;======================================================================
;            BINARY OP
;======================================================================
;
tOr             EQU       3Ch         ; 3Ch  '_or_'
tXor            EQU       3Dh         ; 3Dh
;
tColon          EQU       3Eh         ; 3Eh ':'
tEnter          EQU       3Fh         ; 3Fh  Lenter
;
tAnd            EQU       40h         ; 40h  '_and_'       
;
;======================================================================
;            LETTER TOKENS   
;======================================================================

;
LET             EQU       41h
;
tA              EQU       LET         ; 41h
tB              EQU       LET+1       ; 42h
tC              EQU       LET+2       ; 43h
tD              EQU       LET+3       ; 44h
tE              EQU       LET+4       ; 45h
tF              EQU       LET+5       ; 46h
tG              EQU       LET+6       ; 47h
tH              EQU       LET+7       ; 48h
tI              EQU       LET+8       ; 49h
tJ              EQU       LET+9       ; 4Ah
tK              EQU       LET+10      ; 4Bh
tL              EQU       LET+11      ; 4Ch
tM              EQU       LET+12      ; 4Dh
tN              EQU       LET+13      ; 4Eh
tO              EQU       LET+14      ; 4Fh
tP              EQU       LET+15      ; 50h
tQ              EQU       LET+16      ; 51h
tR              EQU       LET+17      ; 52h
tS              EQU       LET+18      ; 53h
tT              EQU       LET+19      ; 54h
tU              EQU       LET+20      ; 55h
tV              EQU       LET+21      ; 56h
tW              EQU       LET+22      ; 57h
tX              EQU       LET+23      ; 58h
tY              EQU       LET+24      ; 59h
tZ              EQU       LET+25      ; 5Ah
tTheta          EQU       LET+26      ; 5Bh
;
;======================================================================
;   THESE VAR TOKENS ARE 1ST OF A DOUBLE TOKEN 
;======================================================================

;
vToks   EQU     LET+27    ;
;
;       USER MATRIX TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarMat         EQU       vToks       ; 5Ch
;
;       USER LIST TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarLst         EQU       vToks+1     ; 5Dh
;
;       USER EQUATION TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarEqu         EQU       vToks+2     ; 5Eh
tProg           EQU       vToks+3     ; 5Fh
;
;       USER PICT TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarPict        EQU       vToks+4     ; 60h
;
;       USER GDB TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarGDB         EQU       vToks+5     ; 61h
tVarOut         EQU       vToks+6     ; 62h
tVarSys         EQU       vToks+7     ; 63h
;
;
;======================================================================
;            Mode Setting Commands
;======================================================================

;
MODESA          EQU       vToks+8     ; 64h
;
tRad            EQU       MODESA      ; 64h 'Radian'
tDeg            EQU       MODESA+1    ; 65h 'Degree'
tNormF          EQU       MODESA+2    ; 66h 'Normal'
tSci            EQU       MODESA+3    ; 67h 'Sci'
tEng            EQU       MODESA+4    ; 68h 'Eng'
tFloat          EQU       MODESA+5    ; 69h 'Float'
;
CMPS            EQU       6Ah
;
tEQ             EQU       CMPS        ; 6Ah '=='
tLT             EQU       CMPS+1      ; 6Bh '<'
tGT             EQU       CMPS+2      ; 6Ch '>'
tLE             EQU       CMPS+3      ; 6Dh LLE
tGE             EQU       CMPS+4      ; 6Eh LGE
tNE             EQU       CMPS+5      ; 6Fh LNE
;
;======================================================================
;            BINARY OP
;======================================================================
;
tAdd            EQU       70h         ; 70h '+'
tSub            EQU       71h         ; 71h '-'
tAns            EQU       72h         ; 72h
;
;======================================================================
;            Mode Setting Commands
;======================================================================
;
MODES           EQU       73h
;
tFix            EQU       MODES       ; 73h 'Fix_'
tSplitOn        EQU       MODES+1     ; 74h
tFullScreen     EQU       MODES+2     ; 75h
tStndrd         EQU       MODES+3     ; 76h 'Func'
tParam          EQU       MODES+4     ; 77h 'Param'
tPolar          EQU       MODES+5     ; 78h 'Pol'
tSeqG           EQU       MODES+6     ; 79h
tAFillOn        EQU       MODES+7     ; 7Ah 'AUTO FILL ON
tAFillOff       EQU       MODES+8     ; 7Bh
tACalcOn        EQU       MODES+9     ; 7Ch
tACalcOff       EQU       MODES+10    ; 7Dh 'AutoFill OFF
;
;       GRAPH FORMAT TOKENS ARE 2 BYTE TOKENS
;
tGFormat        EQU       MODES+11    ; 7Eh
;
tBoxIcon        EQU       7Fh
tCrossIcon      EQU       80h
tDotIcon        EQU       81h
;
;======================================================================
;            (More) BINARY OP
;======================================================================
;
tMul            EQU       82h         ; 82h  '*'
tDiv            EQU       83h         ; 83h  '/'
;
;======================================================================
;            SOME GRAPH COMMANDS
;======================================================================
;
GRCMDS          EQU       84h
;
tTrace          EQU       GRCMDS      ; 84h 'Trace'
tClDrw          EQU       GRCMDS+1    ; 85h 'ClDrw'
tZoomStd        EQU       GRCMDS+2    ; 86h 'ZStd'
tZoomtrg        EQU       GRCMDS+3    ; 87h 'Ztrg'
tZoomBox        EQU       GRCMDS+4    ; 88h 'ZBOX'
tZoomIn         EQU       GRCMDS+5    ; 89h 'ZIn'
tZoomOut        EQU       GRCMDS+6    ; 8Ah 'ZOut'
tZoomSqr        EQU       GRCMDS+7    ; 8Bh 'ZSqr'
tZoomInt        EQU       GRCMDS+8    ; 8Ch 'ZInt'
tZoomPrev       EQU       GRCMDS+9    ; 8Dh 'ZPrev'
tZoomDec        EQU       GRCMDS+10   ; 8Eh 'ZDecm'
tZoomStat       EQU       GRCMDS+11   ; 8Fh 'ZStat
tUsrZm          EQU       GRCMDS+12   ; 90h 'ZRcl'
tPrtScrn        EQU       GRCMDS+13   ; 91h 'PrtScrn'
tZoomSto        EQU       GRCMDS+14   ; 92h  ZOOM STORE
tText           EQU       GRCMDS+15   ; 93h
;
;======================================================================
;           BINARY OP  (Combination & Permutation)
;======================================================================
;
tnPr            EQU       GRCMDS+16   ; 94h '_nPr_'
tnCr            EQU       GRCMDS+17   ; 95h '_nCr_'
;
;======================================================================
;           MORE GRAPH COMMANDS
;======================================================================
;
tYOn            EQU       GRCMDS+18   ; 96h 'FnOn_'
tYOff           EQU       GRCMDS+19   ; 97h 'FnOff_'
tStPic          EQU       GRCMDS+20   ; 98h 'StPic_'
tRcPic          EQU       GRCMDS+21   ; 99h 'RcPic_'
tStoDB          EQU       GRCMDS+22   ; 9Ah 'StGDB_'
tRclDB          EQU       GRCMDS+23   ; 9Bh 'RcGDB_'
tLine           EQU       GRCMDS+24   ; 9Ch 'Line'
tVert           EQU       GRCMDS+25   ; 9Dh 'Vert_'
tPtOn           EQU       GRCMDS+26   ; 9Eh 'PtOn'
tPtOff          EQU       GRCMDS+27   ; 9Fh 'PtOff'
;
;======================================================================
;           TOKEN A0 CANNOT BE AN EOS FUNCTION
;           SINCE LOW MULT=A0 ALREADY
;======================================================================
;
tPtChg          EQU       GRCMDS+28   ; A0h 'PtChg'
tPXOn           EQU       GRCMDS+29   ; A1h
tPXOff          EQU       GRCMDS+30   ; A2h
tPXChg          EQU       GRCMDS+31   ; A3h
tShade          EQU       GRCMDS+32   ; A4h 'Shade'
tCircl          EQU       GRCMDS+33   ; A5h 'Circl'
tHorz           EQU       GRCMDS+34   ; A6h 'HORIZONTAL'
tTanLn          EQU       GRCMDS+35   ; A7h 'TanLn'
tDrInv          EQU       GRCMDS+36   ; A8h 'DrInv_'
tDrawF          EQU       GRCMDS+37   ; A9h 'DrawF_'
;
tVarStrng       EQU       0AAh
;
;======================================================================
;           Functions with No Arguments                                    
;======================================================================
;
NOARG   EQU     0ABh
;
tRand           EQU       NOARG       ; ABh 'rand'
tPi             EQU       NOARG+1     ; ACh  Lpi
tGetKey         EQU       NOARG+2     ; ADh 'getKy'
;
;
tAPost          EQU       tGetKey+1   ; APOSTROPHY
tQuest          EQU       tAPost+1    ; QUESTION MARK
;
;
UNARY   EQU     tQuest+1              ; B0h
;
tChs            EQU       UNARY       ; B0h
tInt            EQU       UNARY+1     ; B1h
tAbs            EQU       UNARY+2     ; B2h
tDet            EQU       UNARY+3     ; B3h
tIdent          EQU       UNARY+4     ; B4h
tDim            EQU       UNARY+5     ; B5h
tSum            EQU       UNARY+6     ; B6h
tProd           EQU       UNARY+7     ; B7h
tNot            EQU       UNARY+8     ; B8h
tIPart          EQU       UNARY+9     ; B9h
tFPart          EQU       UNARY+10    ; BAh
;
;
;======================================================================
;           NEW 2 BYTE TOKENS
;======================================================================
;
t2ByteTok       EQU       0BBh         ;
;
;
UNARYLR EQU     UNARY+12
;
tSqrt           EQU       UNARYLR     ; BCh
tCubRt          EQU       UNARYLR+1   ; BDh
tLn             EQU       UNARYLR+2   ; BEh
tExp            EQU       UNARYLR+3   ; BFh
tLog            EQU       UNARYLR+4   ; C0h
tALog           EQU       UNARYLR+5   ; C1h
tSin            EQU       UNARYLR+6   ; C2h
tASin           EQU       UNARYLR+7   ; C3h
tCos            EQU       UNARYLR+8   ; C4h
tACos           EQU       UNARYLR+9   ; C5h
tTan            EQU       UNARYLR+10  ; C6h
tATan           EQU       UNARYLR+11  ; C7h
tSinH           EQU       UNARYLR+12  ; C8h
tASinH          EQU       UNARYLR+13  ; C9h
tCosH           EQU       UNARYLR+14  ; CAh
tACosH          EQU       UNARYLR+15  ; CBh
tTanH           EQU       UNARYLR+16  ; CCh
tATanH          EQU       UNARYLR+17  ; CDh
; 
;======================================================================
;           SOME PROGRAMMING COMMANDS                                      
;======================================================================
;
PROGTOK EQU     UNARYLR+18
;
tIf             EQU       PROGTOK     ; CEh
tThen           EQU       PROGTOK+1   ; CFh
tElse           EQU       PROGTOK+2   ; D0h
tWhile          EQU       PROGTOK+3   ; D1h
tRepeat         EQU       PROGTOK+4   ; D2h
tFor            EQU       PROGTOK+5   ; D3h
tEnd            EQU       PROGTOK+6   ; D4h
tReturn         EQU       PROGTOK+7   ; D5h
tLbl            EQU       PROGTOK+8   ; D6h   'Lbl_'
tGoto           EQU       PROGTOK+9   ; D7h   'Goto_'
tPause          EQU       PROGTOK+10  ; D8h   'Pause_'
tStop           EQU       PROGTOK+11  ; D9h   'Stop'
tISG            EQU       PROGTOK+12  ; DAh   'IS>'
tDSL            EQU       PROGTOK+13  ; DBh   'DS<'
tInput          EQU       PROGTOK+14  ; DCh   'Input_'
tPrompt         EQU       PROGTOK+15  ; DDh   'Prompt_'
tDisp           EQU       PROGTOK+16  ; DEh   'Disp_'
tDispG          EQU       PROGTOK+17  ; DFh   'DispG'
;
tOutput         EQU       PROGTOK+18  ; E0h    'Outpt'
tClLCD          EQU       PROGTOK+19  ; E1h    'ClLCD'
tConst          EQU       PROGTOK+20  ; E2h    'Fill'
tSortA          EQU       PROGTOK+21  ; E3h    'sortA_'
tSortD          EQU       PROGTOK+22  ; E4h    'sortD_'
tDispTab        EQU       PROGTOK+23  ; E5h    'Disp Table
tMenu           EQU       PROGTOK+24  ; E6h    'Menu'
tSendMBL        EQU       PROGTOK+25  ; E7h    'SEND'
tGetMBL         EQU       PROGTOK+26  ; E8h    'GET'
;
;======================================================================
;           STAT PLOT COMMANDS                                             
;======================================================================
;
statPCmd        EQU       PROGTOK+27
;
tPlotOn         EQU       statPCmd    ; E9h     ' PLOTSON'
tPlotOff        EQU       statPCmd+1   ; EAh     ' PLOTSOFF
;
tListName       EQU       0EBh        ; LIST DESIGNATOR
;
tPlot1          EQU       0ECh
tPlot2          EQU       0EDh
tPlot3          EQU       0EEh
;
tUnused01       EQU       0EFh        ;        available?
;
tPower          EQU       0F0h        ;        '^'
tXRoot          EQU       0F1h        ;        LsupX,Lroot

STATCMD         EQU       0F2h
;
tOneVar         EQU       STATCMD     ; F2h    'OneVar_'
tTwoVar         EQU       STATCMD+1   ; F3h
tLR             EQU       STATCMD+2   ; F4h    'LinR(A+BX
tLRExp          EQU       STATCMD+3   ; F5h    'ExpR_'
tLRLn           EQU       STATCMD+4   ; F6h    'LnR_'
tLRPwr          EQU       STATCMD+5   ; F7h    'PwrR_'
tMedMed         EQU       STATCMD+6   ; F8h
tQuad           EQU       STATCMD+7   ; F9h
tClrLst         EQU       STATCMD+8   ; FAh    'CLEAR LIST
tClrTbl         EQU       STATCMD+9   ; FBh    CLEAR TABLE
tHist           EQU       STATCMD+10  ; FCh    'Hist_'
txyLine         EQU       STATCMD+11  ; FDh    'xyline_'
tScatter        EQU       STATCMD+12  ; FEh    'Scatter_'
tLR1            EQU       STATCMD+13  ; FFh    'LINR(AX+B

;       
;======================================================================
;           2ND HALF OF GRAPH FORMAT TOKENS
;======================================================================
;           Format settings commands
;
GFMT            EQU       0
;
tSeq            EQU       GFMT        ; 'SeqG'
tSimulG         EQU       GFMT+1      ; 'SimulG'
tPolarG         EQU       GFMT+2      ; 'PolarGC'
tRectG          EQU       GFMT+3      ; 'RectGC'
tCoordOn        EQU       GFMT+4      ; 'CoordOn'
tCoordOff       EQU       GFMT+5      ; 'CoordOff'
tDrawLine       EQU       GFMT+6      ; 'DrawLine'
tDrawDot        EQU       GFMT+7      ; 'DrawDot'
tAxisOn         EQU       GFMT+8      ; 'AxesOn'
tAxisOff        EQU       GFMT+9      ; 'AxesOff'
tGridOn         EQU       GFMT+10     ; 'GridOn'
tGridOff        EQU       GFMT+11     ; 'GridOff'
tLblOn          EQU       GFMT+12     ; 'LabelOn'
tLblOff         EQU       GFMT+13     ; 'LabelOff'
tWebOn          EQU       GFMT+14     ; 'WebOn'
tWebOff         EQU       GFMT+15     ; 'WebOFF'
tuv             EQU       GFMT+16     ; U vs V
tvw             EQU       GFMT+17     ; V vs W
tuw             EQU       GFMT+18     ; U vs W
;
;======================================================================
;           2ND HALF OF USER MATRIX TOKENS                                 
;======================================================================
;
tMatA           EQU       00h         ; MAT A
tMatB           EQU       01h         ; MAT B
tMatC           EQU       02h         ; MAT C
tMatD           EQU       03h         ; MAT D
tMatE           EQU       04h         ; MAT E
tMatF           EQU       05h         ; MAT F
tMatG           EQU       06h         ; MAT G
tMatH           EQU       07h         ; MAT H
tMatI           EQU       08h         ; MAT I
tMatJ           EQU       09h         ; MAT J
;      
;======================================================================
;           2ND HALF OF USER LIST TOKENS
;======================================================================
;
tL1             EQU       00h         ; LIST 1
tL2             EQU       01h         ; LIST 2
tL3             EQU       02h         ; LIST 3
tL4             EQU       03h         ; LIST 4
tL5             EQU       04h         ; LIST 5
tL6             EQU       05h         ; LIST 6

;
;======================================================================
;           2ND HALF OF USER EQUATION TOKENS
;======================================================================
;
;       "Y" EQUATIONS HAVE BIT 4 SET
;
tY1             EQU       10h         ; Y1
tY2             EQU       11h         ; Y2
tY3             EQU       12h         ; Y3
tY4             EQU       13h         ; Y4
tY5             EQU       14h         ; Y5
tY6             EQU       15h         ; Y6
tY7             EQU       16h         ; Y7
tY8             EQU       17h         ; Y8
tY9             EQU       18h         ; Y9
tY0             EQU       19h         ; Y0
;
;       PARAM EQUATIONS HAVE BIT 5 SET
;
tX1T            EQU       20h         ; X1t
tY1T            EQU       21h         ; Y1t
tX2T            EQU       22h         ; X2t
tY2T            EQU       23h         ; Y2t
tX3T            EQU       24h         ; X3t
tY3T            EQU       25h         ; Y3t
tX4T            EQU       26h         ; X4t
tY4T            EQU       27h         ; Y4t
tX5T            EQU       28h         ; X5t
tY5T            EQU       29h         ; Y5t
tX6T            EQU       2Ah         ; X6t
tY6T            EQU       2Bh         ; Y6t
;
;       POLAR EQUATIONS HAVE BIT 6 SET
;
tR1             EQU       40h         ; R1
tR2             EQU       41h         ; R2
tR3             EQU       42h         ; R3
tR4             EQU       43h         ; R4
tR5             EQU       44h         ; R5
tR6             EQU       45h         ; R6
;
;       RECURSION EQUATIONS HAVE BIT 7 SET
;
tun             EQU       80h         ; Un
tvn             EQU       81h         ; Vn
twn             EQU       82h         ; Wn
;
;======================================================================
;           2ND HALF OF USER PICTURE TOKENS                                
;======================================================================
;
tPic1           EQU       00h         ; PIC1
tPic2           EQU       01h         ; PIC2
tPic3           EQU       02h         ; PIC3
tPic4           EQU       03h         ; PIC4
tPic5           EQU       04h         ; PIC5
tPic6           EQU       05h         ; PIC6
tPic7           EQU       06h         ; PIC7
tPic8           EQU       07h         ; PIC8
tPic9           EQU       08h         ; PIC9
tPic0           EQU       09h         ; PIC0
; 
;======================================================================
;           2ND HALF OF USER GRAPH DATABASE TOKENS                         
;======================================================================
;
tGDB1           EQU       00h         ; GDB1
tGDB2           EQU       01h         ; GDB2
tGDB3           EQU       02h         ; GDB3
tGDB4           EQU       03h         ; GDB4
tGDB5           EQU       04h         ; GDB5
tGDB6           EQU       05h         ; GDB6
tGDB7           EQU       06h         ; GDB7
tGDB8           EQU       07h         ; GDB8
tGDB9           EQU       08h         ; GDB9
tGDB0           EQU       09h         ; GDB0
;  
;======================================================================
;           2ND HALF OF STRING VARS                                        
;======================================================================
;
tStr1           EQU       00h
tStr2           EQU       01h
tStr3           EQU       02h
tStr4           EQU       03h
tStr5           EQU       04h
tStr6           EQU       05h
tStr7           EQU       06h
tStr8           EQU       07h
tStr9           EQU       08h
tStr0           EQU       09h

; 
;======================================================================
;           2ND HALF OF SYSTEM OUTPUT ONLY VARIABLES                       
;======================================================================
;
;
;   OPEN        EQU       00h
;
tRegEq          EQU       01h         ; REGRESSION EQUATION
;
tStatN          EQU       02h         ; STATISTICS N
;
tXMean          EQU       03h         ; X MEAN
tSumX           EQU       04h         ; SUM(X)
tSumXSqr        EQU       05h         ; SUM(X^2)
tStdX           EQU       06h         ; STANDARD DEV X
tStdPX          EQU       07h         ; STANDARD DEV POP X
tMinX           EQU       08h         ; Min X VALUE
tMaxX           EQU       09h         ; Max X VALUE
;
tMinY           EQU       0Ah         ; Min Y VALUE
tMaxY           EQU       0Bh         ; Max Y VALUE
tYmean          EQU       0Ch         ; Y MEAN
tSumY           EQU       0Dh         ; SUM(Y)
tSumYSqr        EQU       0Eh         ; SUM(Y^2)
tStdY           EQU       0Fh         ; STANDARD DEV Y
tStdPY          EQU       10h         ; STANDARD DEV POP Y
;
tSumXY          EQU       11h         ; SUM(XY)
tCorr           EQU       12h         ; CORRELATION
tMedX           EQU       13h         ; MED(X)
tQ1             EQU       14h         ; 1ST QUADRANT OF X
tQ3             EQU       15h         ; 3RD QUADRANT OF X
tQuadA          EQU       16h         ; 1ST TERM OF QUAD POLY REG/ Y-INT
tQuadB          EQU       17h         ; 2ND TERM OF QUAD POLY REG/ SLOPE
tQuadC          EQU       18h         ; 3RD TERM OF QUAD POLY REG
tCubeD          EQU       19h         ; 4TH TERM OF CUBIC POLY REG
tQuartE         EQU       1Ah         ; 5TH TERM OF QUART POLY REG
tMedX1          EQU       1Bh         ; x1 FOR MED-MED
tMedX2          EQU       1Ch         ; x2 FOR MED-MED
tMedX3          EQU       1Dh         ; x3 FOR MED-MED
tMedY1          EQU       1Eh         ; y1 FOR MED-MED
tMedY2          EQU       1Fh         ; y2 FOR MED-MED
tMedY3          EQU       20h         ; y3 FOR MED-MED
;
tRecurn		EQU       21h		; RECURSION N
tStatP          EQU       22h
tStatZ          EQU       23h
tStatT          EQU       24h
tStatChi        EQU       25h
tStatF          EQU       26h
tStatDF         EQU       27h
tStatPhat       EQU       28h
tStatPhat1      EQU       29h
tStatPhat2      EQU       2Ah
tStatMeanX1     EQU       2Bh
tStatStdX1      EQU       2Ch
tStatN1         EQU       2Dh
tStatMeanX2     EQU       2Eh
tStatStdX2      EQU       2Fh
tStatN2         EQU       30h
tStatStdXP      EQU       31h
tStatLower      EQU       32h
tStatUpper      EQU       33h
tStat_s         EQU       34h
tLRSqr          EQU       35h         ; r^2
tBRSqr          EQU       36h         ; R^2
;
;======================================================================
;       These next tokens are only used to access the data
;       they are display only and the user cannot access them at all
;======================================================================
;
tF_DF    EQU     37h       ; ANOFAV FACTOR DF
tF_SS    EQU     38h       ; ANOFAV FACTOR SS
tF_MS    EQU     39h       ; ANOFAV FACTOR MS
tE_DF    EQU     3Ah       ; ANOFAV ERROR DF
tE_SS    EQU     3Bh       ; ANOFAV ERROR SS
tE_MS    EQU     3Ch       ; ANOFAV ERROR MS
;
;
;======================================================================
;           2ND HALF OF SYSTEM INPUT/OUTPUT VARIABLES
;======================================================================
;      SYSTEM VARIABLE EQUATES
;
tuXscl       EQU  0
tuYscl       EQU  1
tXscl        EQU  2
tYscl        EQU  3
tRecuru0     EQU  4        ; U 1ST INITIAL COND
tRecurv0     EQU  5        ; V 1ST INITIAL COND
tun1         EQU  6        ; U(N-1)    ; NOT USED
tvn1         EQU  7        ; V(N-1)    ; NOT USED
tuRecuru0    EQU  8        ;
tuRecurv0    EQU  9        ;
;
tXmin        EQU  0Ah
tXmax        EQU  0Bh
tYmin        EQU  0Ch
tYmax        EQU  0Dh
tTmin        EQU  0Eh
tTmax        EQU  0Fh
tThetaMin    EQU  10h
tThetaMax    EQU  11h
tuXmin       EQU  12h
tuXmax       EQU  13h
tuYmin       EQU  14h
tuYmax       EQU  15h
tuThetMin    EQU  16h
tuThetMax    EQU  17h
tuTmin       EQU  18h
tuTmax       EQU  19h
tTblMin      EQU  1Ah
tPlotStart   EQU  1Bh
tuPlotStart  EQU  1Ch
tnMax        EQU  1Dh
tunMax       EQU  1Eh
tnMin        EQU  1Fh
tunMin       EQU  20h
;
tTblStep	EQU  21h
tTStep		EQU  22h
tThetaStep	EQU  23h
tuTStep		EQU  24h
tuThetStep	EQU  25h
;
tDeltaX		EQU  26h
tDeltaY		EQU  27h
;
tXFact		EQU  28h
tYFact		EQU  29h
;
tTblInput       EQU       2Ah
;
tFinN           EQU       2Bh
tFinI           EQU       2Ch
tFinPV          EQU       2Dh
tFinPMT         EQU       2Eh
tFinFV          EQU       2Fh
tFinPY          EQU       30h
tFinCY          EQU       31h
;
tRecurw0        EQU       32h         ; w0(1)
tuRecurw0       EQU       33h
;
tPlotStep       EQU       34h
tuPlotStep      EQU       35h
;
tXres           EQU       36h
tuXres          EQU       37h
;
tRecuru02       EQU       38h         ; u0(2)
tuRecuru02      EQU       39h
tRecurv02       EQU       3Ch         ; v0(2)
tuRecurv02      EQU       3Dh
tRecurw02       EQU       3Eh         ; w0(2)
tuRecurw02      EQU       3Fh
;
;======================================================================
;           2nd byte of t2ByteTok tokens
;======================================================================
;
tFinNPV         EQU       00h         ;
tFinIRR         EQU       01h         ;
tFinBAL         EQU       02h         ;
tFinPRN         EQU       03h         ;
tFinINT         EQU       04h         ;
tFinToNom       EQU       05h         ;
tFinToEff       EQU       06h         ;
tFinDBD         EQU       07h         ;
tLCM            EQU       08h         ;
tGCD            EQU       09h         ;
tRandInt        EQU       0Ah         ;
tRandBin        EQU       0Bh         ;
tSubStrng       EQU       0Ch         ;
tStdDev         EQU       0Dh         ;
tVariance       EQU       0Eh         ;
tInStrng        EQU       0Fh         ;
tDNormal        EQU       10h         ;
tInvNorm        EQU       11h         ;
tDT             EQU       12h         ;
tChI            EQU       13h         ;
tDF             EQU       14h         ;
tBINPDF         EQU       15h         ;
tBINCDF         EQU       16h         ;
tPOIPDF         EQU       17h         ;
tPOICDF         EQU       18h         ;
tGEOPDF         EQU       19h         ;
tGEOCDF         EQU       1Ah         ;
tNormalPDF      EQU       1Bh         ;
tTPDF           EQU       1Ch         ;
tChiPDF         EQU       1Dh         ;
tFPDF           EQU       1Eh         ;
tRandNorm       EQU       1Fh         ;
tFinFPMT        EQU       20h         ;
tFinFI          EQU       21h         ;
tFinFPV         EQU       22h         ;
tFinFN          EQU       23h         ;
tFinFFV         EQU       24h         ;
tConj           EQU       25h         ;
tReal           EQU       26h         ;
tImag           EQU       27h         ;
tAngle          EQU       28h         ;
tCumSum         EQU       29h         ;
tExpr           EQU       2Ah         ;
tLength         EQU       2Bh         ;
tDeltaLst       EQU       2Ch         ;
tRef            EQU       2Dh         ;
tRRef           EQU       2Eh         ;
tToRect         EQU       2Fh         ;
tToPolar        EQU       30h         ;
tConste         EQU       31h         ;
tSinReg         EQU       32h         ;
tLogistic       EQU       33h         ;
tLinRegTTest    EQU       34h         ;
tShadeNorm      EQU       35h         ;
tShadeT         EQU       36h         ;
tShadeChi       EQU       37h         ;
tShadeF         EQU       38h         ;
tMatToLst       EQU       39h         ;
tLstToMat       EQU       3Ah         ;
tZTest          EQU       3Bh         ;
tTTest          EQU       3Ch         ;
t2SampZTest     EQU       3Dh         ;
t1PropZTest     EQU       3Eh         ;
t2PropZTest     EQU       3Fh         ;
tChiTest        EQU       40h         ;
tZIntVal        EQU       41h         ;
t2SampZInt      EQU       42h         ;
t1PropZInt      EQU       43h         ;
t2PropZInt      EQU       44h         ;
tGraphStyle     EQU       45h         ;
t2SampTTest     EQU       46h         ;
t2SampFTest     EQU       47h         ;
tTIntVal        EQU       48h         ;
t2SampTInt      EQU       49h         ;
tSetupLst       EQU       4Ah         ;
tFinPMTend      EQU       4Bh         ;
tFinPMTbeg      EQU       4Ch         ;
tRealM          EQU       4Dh         ;
tPolarM         EQU       4Eh         ;
tRectM          EQU       4Fh         ;
tExprOn         EQU       50h         ;
tExprOff        EQU       51h         ;
tClrAllLst      EQU       52h         ;
tGetCalc        EQU       53h         ;
tDelVar         EQU       54h         ;
tEquToStrng     EQU       55h         ;
tStrngToEqu     EQU       56h         ;
tDelLast        EQU       57h         ;
tSelect         EQU       58h         ;
tANOVA          EQU       59h         ;
tModBox         EQU       5Ah         ;
tNormProb       EQU       5Bh         ;
;
;
tMGT            EQU       64h         ; VERTICAL SPLIT
tZFit           EQU       65h         ; ZOOM FIT
tDiag_on        EQU       66h         ; DIANOSTIC DISPLAY ON
tDiag_off       EQU       67h         ; DIANOSTIC DISPLAY OFF
tOkEnd2v0       EQU       67h         ;end of 2byte tokens for version 0.
tArchive        EQU       68h         ;archive
tUnarchive      EQU       69h         ;unarchive
tasm		EQU	  6Ah
tasmComp	EQU	  6Bh	      ; asm compile
tasmPrgm	EQU	  6Ch	      ; signifies a program is asm
tasmCmp 	EQU	  6Dh	      ; asm program is compiled
;
tLcapAAcute     EQU       6Eh
tLcapAGrave     EQU       6Fh
tLcapACaret     EQU       70h
tLcapADier      EQU       71h
tLaAcute        EQU       72h
tLaGrave        EQU       73h
tLaCaret        EQU       74h
tLaDier         EQU       75h
tLcapEAcute     EQU       76h
tLcapEGrave     EQU       77h
tLcapECaret     EQU       78h
tLcapEDier      EQU       79h
tLeAcute        EQU       7Ah
tLeGrave        EQU       7Bh
tLeCaret        EQU       7Ch
tLeDier         EQU       7Dh
;
tLcapIGrave     EQU       7Fh
tLcapICaret     EQU       80h
tLcapIDier      EQU       81h
tLiAcute        EQU       82h
tLiGrave        EQU       83h
tLiCaret        EQU       84h
tLiDier         EQU       85h
tLcapOAcute     EQU       86h
tLcapOGrave     EQU       87h
tLcapOCaret     EQU       88h
tLcapODier      EQU       89h
tLoAcute        EQU       8Ah
tLoGrave        EQU       8Bh
tLoCaret        EQU       8Ch
tLoDier         EQU       8Dh
tLcapUAcute     EQU       8Eh
tLcapUGrave     EQU       8Fh
tLcapUCaret     EQU       90h
tLcapUDier      EQU       91h
tLuAcute        EQU       92h
tLuGrave        EQU       93h
tLuCaret        EQU       94h
tLuDier         EQU       95h
tLcapCCed       EQU       96h
tLcCed          EQU       97h
tLcapNTilde     EQU       98h
tLnTilde        EQU       99h
tLaccent        EQU       9Ah
tLgrave         EQU       9Bh
tLdieresis      EQU       9Ch
tLquesDown      EQU       9Dh
tLexclamDown    EQU       9Eh
tLalpha         EQU       9Fh
tLbeta          EQU      0a0h
tLgamma         EQU      0a1h
tLcapDelta      EQU      0a2h
tLdelta         EQU      0a3h
tLepsilon       EQU      0a4h
tLlambda        EQU      0a5h
tLmu            EQU      0a6h
tLpi            EQU      0a7h
tLrho           EQU      0a8h
tLcapSigma      EQU      0a9h
;
;
tLphi           EQU      0aBh
tLcapOmega      EQU      0aCh
tLphat          EQU      0aDh
tLchi           EQU      0aEh
tLstatF         EQU      0aFh
;

tLsigma         EQU      0cbh
tLtau           EQU      0cch
tLcapIAcute     EQU      0cdh
;
;
tGarbagec EQU    0Ceh
TokEnd2V1       EQU      0Ceh         ;tLAST TOKEN IN THIS VERSION...
; the following tokens were added in OS version 1.15
tTilde          	equ     0CFh            ; ~
tReserved     	equ      0D0h
tAtSign        	 equ     0D1h            ; @
tPound          	equ     0D2h            ; #
tDollar        	 equ     0D3h            ; $
tAmpersand     	 equ     0D4h            ; &
tBackQuote      	equ     0D5h            ; `
tSemicolon      	equ     0D6h            ; ;
tBackSlash      	equ     0D7h            ; \
tVertSlash      	equ     0D8h            ; |
tUnderscore     	equ     0D9h            ; _
tPercent        	equ     0DAh            ; %
LastToken      	 equ     0DAh            ;tLAST TOKEN IN THIS VERSION...
tInvalid   	  equ     0DBh
#ENDIF


;Extra and misnamed romcalls defined in ION.INC:
_memfree = _memchk
_homeup		=$4558
_cleargbuf = _GrBufClr
_indicatorOn = _RunIndicOn
_indicatorOff = _RunIndicOff
_copygbuf = _GrBufCpy
_clrlcdf = _clrlcdfull
_cleartextshad = _ClrTxtShd
_clrscr = _ClrLCD
_clrscrf = _ClrScrnFull
_getk		= 4744h
_chksysvar = _ChkFindSym
_deletemem = _delmem
_lcdbusy = _LCD_BUSY

;Saferams
saferam1	= savesscreen
saferam2	= appbackupscreen
saferam3	= textshadow
saferam4	= op1
saferamp	= appbackupscreen
saferamp2	= boottemp

;Alternate equate names
gbuf = plotsscreen
grbuf = plotsscreen
sram = saferam1
apdram = saferam1
statram = saferam2
cmdshad = cmdshadow
_IY_TABLE = flags

;CrASH names
GRAPH_MEM = plotsscreen
APD_BUF = savesscreen
TEXT_MEM = textshadow
TEXT_MEM2 = cmdshadow
CURSOR_ROW = currow
CURSOR_COL = curcol
GRAF_CURS = pencol
CURSOR_X = pencol
CURSOR_Y = penrow
VAT_START = symtable
FREE_A1 = plotsscreen
FREE_A2 = saferam3
FREE_A3 = OP1
FREE_B = saferam2
FREE_C = saferam1  
START_ADDR = progstart

;CrASH Romcalls
CR_KHAND = _getkey
LD_HL_MHL = _ldhlind
CP_HL_DE = _cphlde
UNPACK_HL = _divhlby10
DIV_HL_A = _divhlbya
GET_KEY = _getcsc
DISP_DELAY = _lcdbusy

;CrASH ROM_CALLs
TR_CHARPUT = _putc
TX_CHARPUT = _putmap
D_ZT_STR = _puts
D_HL_DECI = _disphl
NEXT_LINE = _newline

M_CHARPUT = _vputmap
D_ZM_STR = _vputs

CLEARLCD = _clrlcdfull
DISP_GRAPH = _grbufcpy

#ENDIF

#IFDEF TI82

;88888888888 8888888        .d8888b.   .d8888b.  
;    888       888         d88P  Y88b d88P  Y88b 
;    888       888         Y88b. d88P        888 
;    888       888  888888  "Y88888"       .d88P 
;    888       888  888888 .d8P""Y8b.  .od888P"  
;    888       888         888    888 d88P"      
;    888       888         Y88b  d88P 888"       
;    888     8888888        "Y8888P"  8888888888

ptemp = $8D18

ApdSubTimer = $8009
ApdTimer = $800A

;======================================================================
;           System and State Flags
;======================================================================
;

trigFlags	EQU  0		;Trigonometry mode settings
trigDeg 	EQU    2	; 1=degrees, 0=radians

kbdFlags	EQU  0		;Keyboard scan
kbdSCR		EQU    3	; 1=scan code ready
kbdKeyPress	EQU    4	; 1=key has been pressed

doneFlags	EQU  0		;display "Done"
donePrgm	EQU    5	; 1=display "Done" after prgm

ioDelFlag	EQU  0
inDelete	EQU    0	;1 = DELETE SCREEN 

;----------------------------------------------------------------------
editFlags	EQU  1
editOpen	EQU    2	; 1=edit buffer is open

monFlags	EQU  1		;monitor flags
monAbandon	EQU    4	; 1=don't start any long process
				; in put away (#715)
;----------------------------------------------------------------------
plotFlags	EQU  2		;plot generation flags
plotTrace	equ  0
plotLoc 	EQU    1	; 0=bkup & display, 1=display only
plotDisp	EQU    2	; 1=plot is in display, 0=text in display


grfModeFlags	EQU  2		;graph mode settings
grfFuncM	EQU    4	; 1=function graph
grfPolarM	EQU    5	; 1=polar graph
grfParamM	EQU    6	; 1=parametric graph
grfRecurM       EQU    7        ; 1=RECURSION graph

graphFlags	EQU  3
graphDraw	EQU    0	; 0=graph is valid, 1=redraw graph

grfDBFlags	EQU  4
grfDot		EQU    0	; 0=line, 1=dot
grfSimul	EQU    1	; 0=sequential, 1=simultaneous
grfGrid 	EQU    2	; 0=no grid, 1=grid
grfPolar	EQU    3	; 0=rectangular, 1=polar coordinates
grfNoCoord	EQU    4	; 0=display coordinates, 1=off
grfNoAxis	EQU    5	; 0=axis, 1=no axis
grfLabel	EQU    6	; 0=off, 1=axis label

textFlags	EQU  5		;Text output flags
textEraseBelow	EQU    1	; 1=erase line below small char
textScrolled	EQU    2	; 1=screen scrolled
textInverse	EQU    3	; 1=display inverse bit-map
textInsMode	EQU    4	; 0=overstrike, 1=insert mode

ParsFlag2	EQU  7		;PARSER flags
numOP1		EQU    0	; 1=RESULT IN OP1, 0=NO RESULT

newDispF        EQU  8		;Derivative mode flags
preClrForMode   EQU    0	; 1=HELP BLINK ON MODE SCREEN

apdFlags	EQU  8		;Automatic power-down
apdAble 	EQU    2	; 1=APD enabled
apdRunning	EQU    3	; 1=APD clock running


web_err_mask    EQU  60h

onFlags 	EQU  9		;on key flags
onRunning	EQU  3		; 1 = calculator is running
onInterrupt	EQU    4	; 1=on key interrupt request

statFlags	EQU  9		;statistics flags
statsValid	EQU    6	; 1=stats are valid
statANSDISP	EQU  7		; 1=display stat results


fmtFlags	EQU  10		;numeric format flags
fmtExponent	EQU    0	; 1=show exponent, 0=no exponent
fmtEng		EQU    1	; 1=engineering notion, 0=scientific
fmtHex		EQU    2	; 1=hexadecimal
fmtOct		EQU    3	; 1=octal
fmtBin		EQU    4	; 1=binary
;
numMode         EQU  10
fmtReal         EQU    5
fmtRect         EQU    6
fmtPolar        EQU    7

realMode        EQU    5
rectMode        EQU    6
polarMode       EQU    7
;					;   if Hex and Oct both = 1
;					;   then Bin=0 means >Frac
;					;	 Bin=1 means >DMS
fmtBaseMask     EQU  00011100B		; mask to base flags
fmtBaseShift    EQU  2			; offset to base flags
;
;       CHECK IF THESE ARE USED BY NUMFORM,
;
;               EQU  6
;               EQU  7

fmtOverride	EQU  11		;copy of fmtFlags with conversion override

fmtEditFlags	EQU  12		;numeric editing flags
fmtEdit 	EQU    0	; 1=format number for editing

curFlags	EQU  12		;Cursor
curAble 	EQU    2	; 1=cursor flash is enabled
curOn		EQU    3	; 1=cursor is showing
curLock 	EQU    4	; 1=cursor is locked off

appFlags	EQU  13		;application flags
appWantIntrpt	EQU    0		; 1=want ON key interrupts
appTextSave	EQU    1		; 1=save characters in textShadow
appAutoScroll	EQU    2		; 1=auto-scroll text on last line
appMenus	EQU    3		; 1=process keys that bring up menus
					; 0=check Lock menu flag
appLockMenus	EQU    4		; 1=ignore menu keys
					; 0=switch to home screen and bring up menu
appCurGraphic	EQU    5		; 1=graphic cursor
appCurWord	EQU    6		; 1=text cursor covers entire word
appExit 	EQU    7		; 1=application handles [EXIT] key itself

appWantIntrptF	EQU	1<<appWantIntrpt
appTextSaveF	EQU	1<<appTextSave
appAutoScrollF	EQU	1<<appAutoScroll
appMenusF	EQU	1<<appMenus
appLockMenusF	EQU	1<<appLockMenus
appCurGraphicF	EQU	1<<appCurGraphic
appCurWordF	EQU	1<<appCurWord
appExitF	EQU	1<<appExit


seqFlags        EQU  15              ; Sequential Graph flags
webMode         EQU    0             ; 0 = NORMAL SEQ MODE, 1 = WEB MODE
webVert         EQU    1             ;
sequv           EQU    2             ; U vs V
seqvw           EQU    3             ; V vs W
sequw           EQU    4             ; U vs W


promptFlags	EQU  17		;prompt line flags
promptEdit	EQU    0	; 1=editing in prompt buffer

indicFlags	EQU  18		;Indicator flags
indicRun	EQU    0	; 1=run indicator ON
indicInUse	EQU    1	; indicator save area in use=1, free=0
indicOnly	EQU    2	; interrupt handler only checks run indicator

shiftFlags	EQU  18		;[2nd] and [ALPHA] flags
shift2nd	EQU    3	; 1=[2nd] has been pressed
shiftAlpha	EQU    4	; 1=[ALPHA] has been pressed
shiftLwrAlph	EQU    5	; 1=lower case, 0=upper case
shiftALock	EQU    6	; 1=alpha lock has been pressed
shiftKeepAlph	EQU    7	; 1=cannot cancel alpha shift


tblFlags        EQU  19		;table flags.
autoFill        EQU    4	; 1=prompt, 0=fillAuto
autoCalc        EQU    5	; 1=prompt, 0=CalcAuto
reTable         EQU    6	; 0=table is ok, 1=must recompute table.

sGrFlags	EQU  20
grfSplit        EQU    0	; 1=Split Graph, 0=Normal
vertSplit       EQU    1	; 1=Vertical (left-right) Split
grfSChanged     EQU    2	; 1=Graph just changed Split <-> normal
grfSplitOverride EQU   3	; 1 = ignore graph split flag if set
write_on_graph  EQU    4	; 1 = TEXT OR EQU WRITING TO GRAPH SCREEN
g_style_active  EQU    5	; 1 = GRAPH STYLES ARE ENABLED, USE THEM
cmp_mod_box     EQU    6	; 1 = DOING MOD BOX PLOT COMPUTATION
textWrite       EQU    7	; 1 = Small font writes to buffer
							; 0 = Small font writes to display
;

newIndicFlags   EQU  21
extraIndic      EQU    0
saIndic         EQU    1

newFlags2	    EQU  22
noRestores	    EQU  5

smartFlags      EQU  23
;
;----------------------------------------------------------------------
; Note: Fix these Equates if smartFlags are moved                            
;----------------------------------------------------------------------
;
smarter_mask    EQU  3
smarter_test    EQU  1
smartGraph      EQU  0
smartGraph_inv  EQU  1                                                 

more_Flags	EQU 26

No_Del_Stat	EQU 2
;----------------------------------------------------------------------
;           Available for ASM programming
;----------------------------------------------------------------------

asm_Flag1       EQU  33          ; ASM CODING
asm_Flag2       EQU  34          ; ASM CODING
asm_Flag3       EQU  35          ; ASM CODING

;======================================================================
;           Data Type Equates                                                    
;======================================================================
;
RealObj         EQU       0
ListObj         EQU       1
MatObj          EQU       2
EquObj          EQU       3
StrngObj        EQU       4
ProgObj         EQU       5
ProtProgObj     EQU       6
PictObj         EQU       7
GDBObj          EQU       8
UnknownObj      EQU       9
UnknownEquObj   EQU       0Ah
NewEquObj       EQU       0Bh
CplxObj         EQU       0Ch
CListObj        EQU       0Dh
UndefObj        EQU       0Eh
WindowObj       EQU       0Fh
ZStoObj         EQU       10h
TblRngObj       EQU       11h
LCDObj          EQU       12h
BackupObj       EQU       13h
AppObj		EQU	  14h		;application, only used in menus/link
AppVarObj	EQU	  15h		;application variable
TempProgObj     EQU       16h           ;program, home deletes when finished
GroupObj        EQU       17h           ;group.



#IFDEF CRASH19006
;
; CRASH19006.INC for CrASH
;  made by Hideaki Omuro and Jeff Mears
; modifi le 22/04/2003 pour CrASH_19006 par Jumbo (www.PlaneteTI82.fr.st)
;
;

;
; RAM Locations
;
;Name         Address Len  Meaning
;   
KEY_0        = $8000 ;Byte Scancode of last key, 0 if gotten
KEY_1        = $8001 ;Byte Scancode of key down now
KEY_2        = $8002 ;Byte Same as $8001, $FF if more than one key is down
KEY_STAT     = $8004 ;Byte Bit 2 set = key down now
LAST_KEY     = $8006 ;Byte Last key pressed
CONTRAST     = $8008 ;Byte Contrast ($00 to $1F)
CURSOR_POS   = $800C ;Word Text position for word operations
CURSOR_ROW   = $800C ;Byte Text cursor row
CURSOR_COL   = $800D ;Byte Text cursor column
BUSY_COUNTER = $8026 ;Byte Counter for busy indicator
BUSY_BITMAP  = $8027 ;Byte Bitmap for busy indicator
OP1          = $8028 ;  11 OP1
OP2          = $8033 ;  11 OP2
OP3          = $803E ;  11 OP3
OP4          = $8049 ;  11 OP4
OP5          = $8054 ;  11 OP5
OP6          = $805F ;  11 OP6
TEXT_MEM     = $808F ; 128 text memory
GRAF_CURS    = $8215 ;Word Cursor position for graphics
CURSOR_X     = $8215 ;Byte X value of graphics cursor
CURSOR_Y     = $8216 ;Byte Y value of graphics cursor
APD_BUF      = $8228 ; 768 APD screen buffer
_IY_TABLE    = $8528 ; N/A Where IY usually points
GRAPH_MEM    = $88B8 ; 768 Graphics Buffer
TEXT_MEM2    = $8BDF ; 128 Secondary text memory
WIN_START    = $8C8F ;Byte Number of first row in window
WIN_END      = $8C90 ;Byte Number of last row in window
CRASH_VER    = $8D73 ;Byte Version number of CrASH
VAT_START    = $FE6E ; N/A Start of VAT

; The following will be used in programs using the APD buffer.  If something
; important (ie. interrupt) is going on inside APD_BUF, INT_STATE will not
; be 00. (Look in CrASHprg.TXT for details)
APD_FREE     = $8D70 ;Word Public Free Mem of APD_BUF ($000=none, other=using)
INT_STATE    = $8D72 ;Byte Public Stat of Interrupts (00=none, other=using)

;
; Variable Regions
;
; You can allocate memory freely in FREE_A regions.
; You must be careful to not use FREE_B if your program calls CR_KHAND

;Free Memory Regions Len Comments
;  
FREE_A1 = GRAPH_MEM ;768 Cleared when starting a program
FREE_A2 = TEXT_MEM  ;128 Cleared when starting a program
FREE_A3 = OP1       ; 66 Can be used if only using normal ROM_CALLs
FREE_B  = TEXT_MEM2 ;128 Cleared after calling CR_KHAND
FREE_C  = APD_BUF   ;768 If INT_STATE is not 0, check for available APD_FREE

;TI83 names
plotsscreen = GRAPH_MEM
savesscreen = APD_BUF
textshadow = TEXT_MEM
cmdshadow = TEXT_MEM2
currow = CURSOR_ROW
curcol = CURSOR_COL
pencol = CURSOR_X
penrow = CURSOR_Y
symtable = VAT_START
indicbusy = BUSY_BITMAP
statvars = cmdshadow

;Saferams
saferam1	= savesscreen
saferam2	= text_mem2
saferam3	= textshadow
saferam4	= op1

;Alternate equate names
gbuf = plotsscreen
grbuf = plotsscreen
sram = saferam1
apdram = saferam1
statram = saferam2
cmdshad = cmdshadow

;
; CrASH call Addresses (use CALL)
;
; This is a list of useful functions created by the authors of CrASH.  They
; are embedded into CrASH, and are very useful tools.

; Use CALL to call these functions.

EXIT_2_TIOS  = $8D88 ; Exits to TI-OS with a clear screen. (can use JP)
RAND         = $8D8B ; Returns a random number 0-127 in A.
CP_HL_BC     = $8D8E ; Compares HL and BC, like CP_HL_DE
CR_KHAND     = $8D91 ; Key_Hand; does not crash if turned off
CR_GRBCopy   = $8D94 ; Fast copy to LCD - Read below

ionFastCopy = CR_GRBCopy

; CR_GRBCopy is limited:
;  May not work correctly if LCD_Port is set to go auto right
;  May not work correctly if calc is turboed
;  Only copies from GRAPH_MEM
;  Still testing - please report bugs while using it.

;
; ROM Addresses (use CALL)
;
; This is a list of useful functions inside the TI-82's ROM.

; Use CALL to call these functions.

;AddressName  Address Registers Description
;   
LD_HL_MHL    = $0033 ;HLA       HL=(HL), A=L
CP_HL_DE     = $0095 ;F         CP  HL, DE
UNPACK_HL    = $00A1 ;HLA       HL=int(HL/10), A=HL MOD 10
DIV_HL_A     = $00A3 ;HLA       HL=int(HL/A), A=HL MOD A
KEY_READ     = $01D7 ;$01B8	;AB        Does raw keyboard read with mask A; returns A
STORE_KEY    = $01E6 ;$01C7	;?         Store keystroke from A
GET_KEY      = $01F3 ;$01D4	;HL?       Get immediate keystroke into A
DISP_DELAY   = $083A ;$07F3	;          Delay for display controller      (60 clocks)
FIND_PIXEL   = $4166 ;?         Find pixel (rom page must be 4) !? Somebody uses that ???

;TI83 Romcalls
_getkey = CR_KHAND
_ldhlind = LD_HL_MHL
_cphlde = CP_HL_DE
_divhlby10 = UNPACK_HL
_divhlbya = DIV_HL_A
_getcsc = GET_KEY
_lcdbusy = DISP_DELAY

;
; ROM Offset Addresses (use ROM_CALL)
;
; To use functions not included here just make a definition like the ones
; below in your program (not recommended).  If you are using the addresses
; in rom found by another source for rom version 19, remember to subtract $1A
; from that address.  (The ones listed are roms version 16-18)

; Use ROM_CALL to call these functions.

#DEFINE ROM_CALL(addr) CALL addr
#define bcall(xxxx)	CALL xxxx
#define bjp(xxxx) jp xxxx
#define icall(xxxx) CALL xxxx
#define ijp(xxxx) JP xxxx

TR_CHARPUT   = $387D	;$3738 ; Raw display of A on screen, normal text style
TX_CHARPUT   = $3B35	;$39D2 ; Xlated display of A on screen, normal text style
D_ZT_STR     = $3A2D	;$38FA ; Display zero-terminated normal text string
D_LT_STR     = $3883	;$373E ; Display length-byte normal text string
D_HL_DECI    = $39AF	;$387C ; Disp HL as 5-char, right just., blank-pad. integer
LAST_LINE    = $38B9	;$3774 ; Is it the last line?
NEXT_LINE    = $38BF	;$377A ; Goto next line (might scroll)
SCROLL_UP    = $38CB	;$3786 ; Scroll text screen up
UP_TEXT      = $392B	;$37F2 ; Update text mem from command shadow

M_CHARPUT    = $3907	;$37CE ; Display %A on screen, menu style
D_ZM_STR     = $390D	;$37D4 ; Display zero-terminated string, menu style
D_LM_STR     = $3913	;$37DA ; Display length indexed string, menu style

CLEARLCD     = $39CD	;$389A ; Clear LCD, but not text or graphics memory
CLEAR_LCD    = $39CD	;$389A ; Clear LCD, but not text or graphics memory
CLEARTEXT_W  = $3A45	;$37B0 ; Clear LCD and text memory, but only current window (affected by 1,(IY+13))
CLEARTEXT_F  = $38E9	;$37A4 ; Clear LCD and text memory (affected by 1,(IY+13))
CLEAR_DISP   = $39F1	;$38CA ; Clear display
BACKUP_DISP  = $3B29	;$39C6 ; Copy display to APD_BUF
RESTOR_DISP  = $3A09	;$38DC ; Copy APD_BUF to display

_DispHL = D_HL_DECI
_Puts = D_ZT_STR
_NewLine = NEXT_LINE
_PutC = TX_CHARPUT

_Vputs = D_ZM_STR
_Vputmap = M_CHARPUT

_ClrLCDFull = CLEARLCD
_ClrLCDF = CLEARLCD

; In OShell-82, two of these ROM functions are called by the TI-83 name which
; TI gave them.  To make easier for TI-83 programmers, the following two
; definitions are included, although NOT recommended for usage AT ALL.
;                                                           
; Also, there are alternate CrASH calls that will do the same thing, but
; enhanced.  (Look above)

DISP_GRAPH   = $39DF	;$38AC ; Display contents of GRAPH_MEM on LCD
KEY_HAND     = $3A87	;$3924 ; Get key using key handler (2nd, alpha, contrast)

_GRBUFCPY_V  = DISP_GRAPH ; Way too slow
;_GETKEY      = KEY_HAND   ; Crashes when you turn off calc. (not a joke)

;
; Ports
;
P_LINK       = $00   ; Link port                                     (TI-85)
P_KEYBOARD   = $01   ; Port used to access keys                      (TI-85)
P_ROM        = $02   ; Rom page switching and other things
P_STATUS     = $03   ; Used to get/set status of varius hardware
P_INT        = $04   ; Interrupt speed (don't change!!! the display needs it)
P_LCD_CON    = $10   ; Control port for display controller           (TI-83)
P_LCD_DATA   = $11   ; Data port for display controller              (TI-83)

START_ADDR = $9104
progstart = START_ADDR

#DEFINE CRASH

#ENDIF

#IFDEF CRASH
#IFNDEF CRASH19006
;
; CRASH82.INC for CrASH v1.1
;  made by Hideaki Omuro and Jeff Mears
;

;
; RAM Locations
;
;Name         Address Len  Meaning
;   
KEY_0        = $8000 ;Byte Scancode of last key, 0 if gotten
KEY_1        = $8001 ;Byte Scancode of key down now
KEY_2        = $8002 ;Byte Same as $8001, $FF if more than one key is down
KEY_STAT     = $8004 ;Byte Bit 2 set = key down now
LAST_KEY     = $8006 ;Byte Last key pressed
CONTRAST     = $8008 ;Byte Contrast ($00 to $1F)
CURSOR_POS   = $800C ;Word Text position for word operations
CURSOR_ROW   = $800C ;Byte Text cursor row
CURSOR_COL   = $800D ;Byte Text cursor column
BUSY_COUNTER = $8026 ;Byte Counter for busy indicator
BUSY_BITMAP  = $8027 ;Byte Bitmap for busy indicator
OP1          = $8028 ;  11 OP1
OP2          = $8033 ;  11 OP2
OP3          = $803E ;  11 OP3
OP4          = $8049 ;  11 OP4
OP5          = $8054 ;  11 OP5
OP6          = $805F ;  11 OP6
TEXT_MEM     = $808F ; 128 text memory
GRAF_CURS    = $8215 ;Word Cursor position for graphics
CURSOR_X     = $8215 ;Byte X value of graphics cursor
CURSOR_Y     = $8216 ;Byte Y value of graphics cursor
APD_BUF      = $8228 ; 768 APD screen buffer
_IY_TABLE    = $8528 ; N/A Where IY usually points
GRAPH_MEM    = $88B8 ; 768 Graphics Buffer
TEXT_MEM2    = $8BDF ; 128 Secondary text memory
WIN_START    = $8C8F ;Byte Number of first row in window
WIN_END      = $8C90 ;Byte Number of last row in window
CRASH_VER    = $8D73 ;Byte Version number of CrASH
VAT_START    = $FE6E ; N/A Start of VAT

; The following will be used in programs using the APD buffer.  If something
; important (ie. interrupt) is going on inside APD_BUF, INT_STATE will not
; be 00. (Look in CrASHprg.TXT for details)
APD_FREE     = $8D70 ;Word Public Free Mem of APD_BUF ($000=none, other=using)
INT_STATE    = $8D72 ;Byte Public Stat of Interrupts (00=none, other=using)

;
; Variable Regions
;
; You can allocate memory freely in FREE_A regions.
; You must be careful to not use FREE_B if your program calls CR_KHAND

;Free Memory Regions Len Comments
;  
FREE_A1 = GRAPH_MEM ;768 Cleared when starting a program
FREE_A2 = TEXT_MEM  ;128 Cleared when starting a program
FREE_A3 = OP1       ; 66 Can be used if only using normal ROM_CALLs
FREE_B  = TEXT_MEM2 ;128 Cleared after calling CR_KHAND
FREE_C  = APD_BUF   ;768 If INT_STATE is not 0, check for available APD_FREE

;TI83 names
plotsscreen = GRAPH_MEM
savesscreen = APD_BUF
textshadow = TEXT_MEM
cmdshadow = TEXT_MEM2
currow = CURSOR_ROW
curcol = CURSOR_COL
pencol = CURSOR_X
penrow = CURSOR_Y
symtable = VAT_START
indicbusy = BUSY_BITMAP
statvars = cmdshadow

;Saferams
saferam1	= savesscreen
saferam2	= text_mem2
saferam3	= textshadow
saferam4	= op1

;Alternate equate names
gbuf = plotsscreen
grbuf = plotsscreen
sram = saferam1
apdram = saferam1
statram = saferam2
cmdshad = cmdshadow

;
; CrASH call Addresses (use CALL)
;
; This is a list of useful functions created by the authors of CrASH.  They
; are embedded into CrASH, and are very useful tools.

; Use CALL to call these functions.

EXIT_2_TIOS  = $8D88 ; Exits to TI-OS with a clear screen. (can use JP)
RAND         = $8D8B ; Returns a random number 0-127 in A.
CP_HL_BC     = $8D8E ; Compares HL and BC, like CP_HL_DE
CR_KHAND     = $8D91 ; Key_Hand; does not crash if turned off
CR_GRBCopy   = $8D94 ; Fast copy to LCD - Read below

ionFastCopy = CR_GRBCopy


; CR_GRBCopy is limited:
;  May not work correctly if LCD_Port is set to go auto right
;  May not work correctly if calc is turboed
;  Only copies from GRAPH_MEM
;  Still testing - please report bugs while using it.

;
; ROM Addresses (use CALL)
;
; This is a list of useful functions inside the TI-82's ROM.

; Use CALL to call these functions.

;AddressName  Address Registers Description
;   
LD_HL_MHL    = $0033 ;HLA       HL=(HL), A=L
CP_HL_DE     = $0095 ;F         CP  HL, DE
UNPACK_HL    = $00A1 ;HLA       HL=int(HL/10), A=HL MOD 10
DIV_HL_A     = $00A3 ;HLA       HL=int(HL/A), A=HL MOD A
KEY_READ     = $01B8 ;AB        Does raw keyboard read with mask A; returns A
STORE_KEY    = $01C7 ;?         Store keystroke from A
GET_KEY      = $01D4 ;HL?       Get immediate keystroke into A
DISP_DELAY   = $07F3 ;          Delay for display controller      (60 clocks)
FIND_PIXEL   = $4166 ;?         Find pixel (rom page must be 4)

;TI83 Romcalls
_getkey = CR_KHAND
_ldhlind = LD_HL_MHL
_cphlde = CP_HL_DE
_divhlby10 = UNPACK_HL
_divhlbya = DIV_HL_A
_getcsc = GET_KEY
_lcdbusy = DISP_DELAY

;
; ROM Offset Addresses (use ROM_CALL)
;
; To use functions not included here just make a definition like the ones
; below in your program (not recommended).  If you are using the addresses
; in rom found by another source for rom version 19, remember to subtract $1A
; from that address.  (The ones listed are roms version 16-18)

; Use ROM_CALL to call these functions.

#DEFINE ROM_CALL(addr) CALL $8D74 \ .DW addr
#define bcall(xxxx)	CALL $8D74 \ .dw xxxx
#define bjp(xxxx) bcall(xxxx) \ ret
#define icall(xxxx) CALL xxxx
#define ijp(xxxx) JP xxxx


TR_CHARPUT   = $3738 ; Raw display of A on screen, normal text style
TX_CHARPUT   = $39D2 ; Xlated display of A on screen, normal text style
D_ZT_STR     = $38FA ; Display zero-terminated normal text string
D_LT_STR     = $373E ; Display length-byte normal text string
D_HL_DECI    = $387C ; Disp HL as 5-char, right just., blank-pad. integer
LAST_LINE    = $3774 ; Is it the last line?
NEXT_LINE    = $377A ; Goto next line (might scroll)
SCROLL_UP    = $3786 ; Scroll text screen up
UP_TEXT      = $37F2 ; Update text mem from command shadow

_DispHL = D_HL_DECI
_Puts = D_ZT_STR
_NewLine = NEXT_LINE
_PutC = TX_CHARPUT

; "Menu style" is actually just a bad way of saying "small font"
M_CHARPUT    = $37CE ; Display %A on screen, small font
D_ZM_STR     = $37D4 ; Display zero-terminated string, small font
D_LM_STR     = $37DA ; Display length indexed string, small font

_Vputs = D_ZM_STR
_Vputmap = M_CHARPUT

CLEARLCD     = $389A ; Clear LCD, but not text or graphics memory
CLEARTEXT_W  = $37B0 ; Clear LCD and text memory, but only current window (affected by 1,(IY+13))
CLEARTEXT_F  = $37A4 ; Clear LCD and text memory (affected by 1,(IY+13))
CLEAR_DISP   = $38CA ; Clear display
BACKUP_DISP  = $39C6 ; Copy display to APD_BUF
RESTOR_DISP  = $38DC ; Copy APD_BUF to display

_ClrLCDFull = CLEARLCD
_ClrLCDF = CLEARLCD

;_GrbufClr = CLEAR_DISP
;_cleargbuf = CLEAR_DISP

; In OShell-82, two of these ROM functions are called by the TI-83 name which
; TI gave them.  To make easier for TI-83 programmers, the following two
; definitions are included, although NOT recommended for usage AT ALL.
;                                                           
; Also, there are alternate CrASH calls that will do the same thing, but
; enhanced.  (Look above)

DISP_GRAPH   = $38AC ; Display contents of GRAPH_MEM on LCD
KEY_HAND     = $3924 ; Get key using key handler (2nd, alpha, contrast)

_GRBUFCPY    = DISP_GRAPH ; Way too slow
_GRBUFCPY_V  = DISP_GRAPH ; Way too slow
;_GETKEY      = KEY_HAND   ; Crashes when you turn off calc. (not a joke)

;
; Extra Romcalls
;


;_DispDone         = $3884-$1C
;_DispxxxFail      = $38A2-$1C
;_convop1          = $1A93
;_setxxxxop2       = $1A8E
;_setxxxxop12       = $1A8D
;_setxxxxop1       = $1A88
;
;_op1toop2 = $19BB
;_op1toop3 = $199B
;_op1toop4 = $1978
;_op1toop5 = $1A07
;_op1toop6 = $19FF
;_op2toop1 = $1A0F
;_op2toop3 = $1A6E
;_op2toop4 = $1981
;_op2toop5 = $19EF
;_op2toop6 = $19F7
;_op3toop1 = $19DA
;_op3toop2 = $1993
;_op3toop4 = $196F
;_op3toop5 = $19E7
;_op4toop1 = $19D0
;_op4toop2 = $198A
;_op4toop3 = $1A76
;_op4toop5 = $19DF
;_op4toop6 = $1A80
;_op5toop1 = $19D5
;_op5toop2 = $19A3
;_op5toop3 = $1A7B
;_op5toop4 = $19B3
;_op5toop6 = $19AB
;_op6toop1 = $19CB
;_op6toop2 = $19C3
;
;_cpop1op2 = $192C
;
;_zeroop1  = $1B16
;_op1set0  = $1B16
;_op1set1 = $1ABF
;_op1set2 = $1AD7
;_op1set3 = $1ACB
;_op1set4 = $1AC4
;
;_zeroop2 = $1B11
;_op2set0 = $1B11
;_op2set1 = $1AE7
;_op2set2 = $1ADC
;_op2set3 = $1AB7
;_op2set4 = $1AB0
;_op2set5 = $1AA9
;_op2set8 = $1AA2
;
;_zeroop3 = $1B0C
;_op3set0 = $1B0C
;_op3set1 = $1A9D
;_op3set2 = $1AD2
;_op4set0 = $1B07
;_op4set1 = $1A98
;
;_op1exop2 = $1D33
;_op1exop3 = $1D29
;_op1exop4 = $1D2E
;_op1exop5 = $1D12
;_op1exop6 = $1D17
;_op2exop4 = $1D1C
;_op2exop5 = $1D21
;_op2exop6 = $1D05
;_op5exop6 = $1D0A
;
;_popop1 = $26F4
;_popop2 = $26EF
;_popop3 = $26EA
;_popop4 = $26E5
;_popop5 = $26E0
;_popop6 = $26DB
;_pushop1 = $273E
;_pushop2 = $2739
;_pushop3 = $2734
;_pushop4 = $272F
;_pushop5 = $272A
;_pushop6 = $2725
;
;_fpinc    = $0842
;_fpadd    = $085A
;_fpsub    = $084A
;_fpmult   = $092C
;_fpdiv    = $0ACB
;_fprecip  = $0AD1
;_fpsqrt   = $0B9C
;_fpsquare = $0924
;_fpcube   = $0917
;_fpdouble = $083F
;_fphalf   = $091C
;_invop1s		= $0A5E
;
;_asin      = $15D6
;_acos      = $1567
;_atan      = $1578
;
;_sin      = $10E0
;_cos      = $10E4
;_tan      = $10E8
;
;_sinh      = $14D0
;_cosh      = $14CC
;_tanh      = $14C8
;
;_invop1


;
; Misc
;
#DEFINE TEXT_START RES 1,(IY+0Dh) \ RES 2,(IY+0Dh) \ ROM_CALL(CLEAR_DISP) \ LD HL,0000h \ LD (CURSOR_POS),HL
#DEFINE TEXT_END SET 2,(IY+0Dh) \ SET 1,(IY+0Dh)

START_ADDR = $9104
progstart = START_ADDR
#ENDIF
#ENDIF

#IFDEF ACE
;
; ACEBETA.INC for ACE BETA 3
;  made by Hideaki Omuro
;

;
; RAM Locations
;
;Name         Address Len  Meaning
;   
KEY_0        = $8000 ;Byte Scancode of last key, 0 if gotten
KEY_1        = $8001 ;Byte Scancode of key down now
KEY_2        = $8002 ;Byte Same as $8001, $FF if more than one key is down
KEY_STAT     = $8004 ;Byte Bit 2 set = key down now
LAST_KEY     = $8006 ;Byte Last key pressed
CONTRAST     = $8008 ;Byte Contrast ($00 to $1F)
CURSOR_POS   = $800C ;Word Text position for word operations
CURSOR_ROW   = $800C ;Byte Text cursor row
CURSOR_COL   = $800D ;Byte Text cursor column
BUSY_COUNTER = $8026 ;Byte Counter for busy indicator
BUSY_BITMAP  = $8027 ;Byte Bitmap for busy indicator
OP1          = $8028 ;  11 OP1
OP2          = $8033 ;  11 OP2
OP3          = $803E ;  11 OP3
OP4          = $8049 ;  11 OP4
OP5          = $8054 ;  11 OP5
OP6          = $805F ;  11 OP6
TEXT_MEM     = $808F ; 128 text memory
GRAF_CURS    = $8215 ;Word Cursor position for graphics
CURSOR_X     = $8215 ;Byte X value of graphics cursor
CURSOR_Y     = $8216 ;Byte Y value of graphics cursor
;APD_BUF      = $8228 ; 768 APD screen buffer ** DO NOT ACCESS **
APD_BUF      = $9000
_IY_TABLE    = $8528 ; N/A Where IY usually points
GRAPH_MEM    = $88B8 ; 768 Graphics Buffer
TEXT_MEM2    = $8BDF ; 128 Secondary text memory
WIN_START    = $8C8F ;Byte Number of first row in window
WIN_END      = $8C90 ;Byte Number of last row in window
VAT_START    = $FE6E ; N/A Start of VAT

saferam2 = APD_BUF+768
plotsscreen = graph_mem
gbuf = graph_mem
pencol = CURSOR_X
penrow = CURSOR_Y

currow = CURSOR_ROW
curcol = CURSOR_COL

savesscreen = APD_BUF

saferam1 = APD_BUF
statvars = saferam2
saferam3 = TEXT_MEM
saferam4 = OP1



;
; Variable Regions
;
SH_BUF       = $9000 ;  2k buffer

;
; ACE shell calls
;
; This is a list of functions within ACE.

FILL         = $8D50 ; Fill BC bytes with A, starting at HL
GET_RAND     = $8D53 ; Get random # 0-255
PAUSE        = $8D56 ; Wait for [ENTER]
HL_DECI      = $8D59 ; Convert decimal HL to a string, returns HL->string
GRB_COPY     = $8D5C ; Copy graphics buffer to LCD
GRB_FILL     = $8D5F ; Fill graphics buffer with A
DIRECT_KEY   = $8D62 ; put state of down,left,right,up,Y=,2nd,mode,del into A
DISP_DELAY   = $8D65 ; delay for LCD write

FIND_PIXEL   = $8D68 ; *** not implemented yet
SPRITE       = $8D6B ; *** not implemented yet
LINK_SEND    = $8D6E ; *** not implemented yet
LINK_UPDATE  = $8D71 ; *** not implemented yet
LINK_GET     = $8D74 ; *** not implemented yet

EXIT_TIOS    = $8D77 ; Exit to TI-OS
FIND_INIT    = $8D7A ; Initialize Variable Find (call before searching a var)
FIND_NEXT    = $8D7D ; Return HL->vat entry,DE->prog,OP1=name of next var
KHAND        = $8D80 ; key handler w/ 2nd/alpha/contrast/off

CR_GRBCopy   = GRB_COPY ; duplicate definition
CR_KHAND     = KHAND    ; duplicate definition

ionFastCopy = CR_GRBCopy
_getkey     = CR_KHAND
;----------------------------------------------------------------------------
;[ Macro Definitions ]
;----------------------------------------------------------------------------
#DEFINE LD_HL_MHL LD A,(HL)\ INC HL\ LD H,(HL)\ LD L,A
#DEFINE CP_HL_DE  PUSH HL\ OR A\ SBC HL,DE\ POP HL
#DEFINE CP_HL_BC  PUSH HL\ OR A\ SBC HL,BC\ POP HL

;----------------------------------------------------------------------------
;[ ROM call vectors ]
;----------------------------------------------------------------------------
; This is a list of functions inside the TI-82 ROM.

#define ROM_CALL(x) CALL x
#define bcall(x) CALL x
#define icall(x) CALL x
#define bjp(xxxx) JP xxxx
#define ijp(xxxx) JP xxxx


TX_CHARPUT  = TEXT_MEM2
M_CHARPUT   = TEXT_MEM2 + 3
D_ZT_STR    = TEXT_MEM2 + 6
D_ZM_STR    = TEXT_MEM2 + 9
CLEAR_TEXT  = TEXT_MEM2 + 12
CLEAR_LCD   = TEXT_MEM2 + 15
GET_KEY     = TEXT_MEM2 + 18
STORE_KEY   = TEXT_MEM2 + 21
FIND_VAT    = TEXT_MEM2 + 24
UNPACK_HL   = TEXT_MEM2 + 27
CREATE_PRGM = TEXT_MEM2 + 30
DEL_MEM     = TEXT_MEM2 + 33
INSERT_MEM  = TEXT_MEM2 + 36
MEM_FREE    = TEXT_MEM2 + 39
DEL_TMP     = TEXT_MEM2 + 42
CREATE_TMP  = TEXT_MEM2 + 45
KEY_HAND    = $8D4D

_ldhlind = $0033
_getcsc = GET_KEY
;_divhlbya = DIV_HL_A
_divhlby10 = UNPACK_HL
_lcdbusy = DISP_DELAY
_Puts = D_ZT_STR
_VPuts = D_ZM_STR
_Insertmem = INSERT_MEM
_delmem = DEL_MEM
_chkfindsym = FIND_VAT
_createprog = CREATE_PRGM
_memfree = MEM_FREE

;_NewLine = NEXT_LINE
_PutC = TX_CHARPUT
_Vputmap = M_CHARPUT
_ClrLCDFull = CLEAR_LCD
_ClrLCDF = CLEAR_LCD

;
; Misc
;
#DEFINE TEXT_START RES 1,(IY+0Dh) \ RES 2,(IY+0Dh) \ ROM_CALL(CLEAR_DISP) \ LD HL,0000h \ LD (CURSOR_POS),HL
#DEFINE TEXT_END SET 2,(IY+0Dh) \ SET 1,(IY+0Dh)


START_ADDR = $9880
progstart = START_ADDR
#ENDIF

#ENDIF

#IFDEF TI73

;88888888888 8888888      8888888888  .d8888b.  
;    888       888              d88P d88P  Y88b 
;    888       888             d88P       .d88P 
;    888       888  888888    d88P       8888"  
;    888       888  888888   d88P         "Y8b. 
;    888       888          d88P     888    888 
;    888       888         d88P      Y88b  d88P 
;    888     8888888      d88P        "Y8888P"  

progstart = $4000

#define bcall(xxxx)	rst 28h \ .dw xxxx
#define	bcallz(xxxx)	jr nz,$+5 \ rst 28h \ .dw xxxx
#define	bcallnz(xxxx)	jr	z,$+5 \ rst 28h \ .dw xxxx
#define	bcallc(xxxx)	jr	nc,$+5 \ rst 28h \ .dw xxxx
#define	bcallnc(xxxx)	jr	c,$+5	\ rst 28h \ .dw xxxx
#define bjump(xxxx)	call 50h \ .dw xxxx
#define bjp(xxxx)	bcall(xxxx) \ ret
#define icall(xxxx) bcall(xxxx)
#define ijp(xxxx) bjp(xxxx)
 .addinstr B_CALL *       EF   3 NOP 1

APP_PUSH_ERRORH .EQU	59h
APP_POP_ERRORH	.EQU	5Ch
#define AppOnErr(xxxx) ld hl,xxxx \ call APP_PUSH_ERRORH
#define AppOffErr call APP_POP_ERRORH
#define getcurrentpage in a, (mempageaport)

;=================================
;entry points
;=================================
_AbsO1O2Cp		equ 40f0h
_AbsO1PAbsO2		equ 405ah
_ACos		equ 40d2h
_ACosRad		equ 40c6h
_AdrLEle		equ 4ab6h
_AdrMRow		equ 4567h
_AdrMEle		equ 456ah
_AllEq		equ 4792h
_AllocFPS		equ 4369h
_AllocFPS1		equ 436ch
_AnsName		equ 4B67h
_ApdSetup		equ 4c06h
_AppGetCbl		equ 4b97h
_AppGetCalc		equ 4b9ah
_AppInit		equ 4048h
_ASin		equ 40d8h
_ASinRad		equ 40cfh
_ATanRad		equ 40c9h
_ATan2Rad		equ 40cch
_ATan		equ 40d5h
_ATan2		equ 40dbh

_BinOPExec		equ 459ah
_BufClear		equ 4840h

_CanAlphIns		equ 4ab0h
_ChkFindSym		equ 42d9h
_CircCmd		equ 46f6h
_CkInt		equ 423ah
_CkOdd		equ 423dh
_CkOP1C0		equ 422bh
_CkOP1FP0		equ 422eh
_CkOP1Pos		equ 4252h
_CkOP1Real		equ 40e7h
_CkOP2FP0		equ 4231h
_CkOP2Pos		equ 424fh
_CkOP2Real		equ 42cah
_CkPosInt		equ 4237h
_CkValidNum		equ 426ah
_CleanAll		equ 4927h
_ClearRect		equ 4cfch
_ClearRow		equ 4c8dh
_CLine		equ 46cch
_CLineS		equ 46cfh
_CloseEditBuf	equ 47E9h
_CloseEditBufNoR		equ 46a2h
_CloseEditEqu	equ 4867h
_CloseProg		equ 490ch
_ClrGraphRef		equ 490fh
_ClrLCD		equ 44aah
_ClrLCDFull		equ 44a7h
_ClrLp		equ 41E3h
_ClrOP1S		equ 4258h
_ClrOP2S		equ 4255h
_ClrScrn		equ 44b0h
_ClrScrnFull		equ 44adh
_ClrTxtShd		equ 44b3h
_CmpSyms		equ 4921h
_ConvDim		equ 4B58h
_ConvDim00		equ 4B5Bh
_ConvOP1		equ 49abh
_Cos		equ 40c0h
_CpHLDE		equ 4003h
_CPoint		equ 4d89h
_CPointS		equ 4717h
_CpOP1OP2		equ 40f3h
_CpOP4OP3		equ 40eah
_CpyO1toFPS1		equ 4411h
_CpyO1toFPS2		equ 4420h
_CpyO1toFPS3		equ 442ch
_CpyO1toFPS4		equ 443eh
_CpyO1toFPS5		equ 4438h
_CpyO1toFPS6		equ 4432h
_CpyO1toFPS7		equ 4435h
_CpyO1toFPST		equ 43ffh
_CpyO2toFPS1		equ 440eh
_CpyO2toFPS2		equ 4417h
_CpyO2toFPS3		equ 4429h
_CpyO2toFPS4		equ 443bh
_CpyO2toFPST		equ 43f9h
_CpyO3toFPS1		equ 4408h
_CpyO3toFPS2		equ 441ah
_CpyO3toFPST		equ 43f6h
_CpyO5toFPS1		equ 440bh
_CpyO5toFPS3		equ 4426h
_CpyO6toFPS2		equ 441dh
_CpyO6toFPST		equ 43fch
_CpyStack		equ 43deh
_Cpyto1FPS1		equ 43e7h
_Cpyto1FPS10		equ 43a8h
_Cpyto1FPS11		equ 438dh
_Cpyto1FPS2		equ 43f0h
_Cpyto1FPS3		equ 43bdh
_Cpyto1FPS4		equ 43c3h
_Cpyto1FPS5		equ 4393h
_Cpyto1FPS6		equ 4399h
_Cpyto1FPS7		equ 439fh
_Cpyto1FPS8		equ 43a2h
_Cpyto1FPS9		equ 43abh
_Cpyto1FPST		equ 43d8h
_CpyTo2FPS1		equ 43e4h
_CpyTo2FPS2		equ 43edh
_CpyTo2FPS3		equ 43b7h
_CpyTo2FPS4		equ 43aeh
_CpyTo2FPS5		equ 4390h
_CpyTo2FPS6		equ 4396h
_CpyTo2FPS7		equ 439ch
_CpyTo2FPS8		equ 43a5h
_CpyTo2FPST		equ 43D5h
_CpyTo3FPS1		equ 43e1h
_CpyTo3FPS2		equ 43c6h
_CpyTo3FPST		equ 43d2h
_CpyTo4FPST		equ 43cfh
_CpyTo5FPST		equ 43c9h
_CpyTo6FPST		equ 43cch
_CpyTo6FPS3		equ 43b1h
_CpyTo6FPS2		equ 43b4h
_CpyToFPS1		equ 4414h
_CpyToFPS2		equ 4423h
_CpyToFPS3		equ 442fh
_CpyToFPST		equ 4402h
_CpyToStack		equ 4405h
_Create0Equ		equ 4303h
_CreateAppVar		equ 4312h
_CreateEqu		equ 4309h
_CreatePict		equ 430ch
_CreateProg		equ 430fh
_CreateReal		equ 42f4h
_CreateRList	equ 42fah
_CreateStrng	equ 4300h
_Cube  		equ 407bh
_CursorOff		equ 4522h
_CursorOn		equ 4528h

_DarkLine		equ 46ffh
_DarkPnt		equ 4714h
_DataSize		equ 433ch
_DataSizeA		equ 4339h
_DeallocFPS		equ 4363h
_DeallocFPS1		equ 4366h
_DecO1Exp		equ 4261h
_DelListEl		equ 4906h
_DelMem		equ 4330h
_DelRes		equ 48fah
_DelVar		equ 432ah
_DisableApd		equ 4be8h
_DisableAPI		equ 4bdfh
_DispDone		equ 4519h
_DispEOL		equ 450ah
_DispHL		equ 446eh
_DisplayImage		equ 4d5ch
_DispOP1A		equ 4AFBh
_DivHLBy10		equ 4006h
_DivHLByA		equ 4009h
_DrawCirc2		equ 4aadh
_DrawCmd		equ 47ddh
_DrawRectBorder		equ 4d2ch
_DrawRectBorderClear		equ 4d3bh
_DToR		equ 4075h

_EditProg		equ 4909h
_EnableApd		equ 4bebh
_EnoughMem		equ 42e5h
_EOP1NotReal		equ 4273h
_Equ_or_NewEqu		equ 42afh
_EraseEOL		equ 44b9h
_EraseRectBorder		equ 4d35h
_ErrArgument		equ 49ffh
_ErrBadGuess		equ 4a2fh
_ErrBreak		equ 4a20h
_ErrD_OP1_0		equ 42beh
_ErrD_OP1_LE_0		equ 42bbh
_ErrD_OP1Not_R		equ 42b5h
_ErrD_OP1NotPos		equ 42b2h
_ErrD_OP1NotPosInt		equ 42b8h
_ErrDataType		equ 49f9h
_ErrDimMismatch		equ 4a02h
_ErrDimension		equ 4a05h
_ErrDivBy0		equ 49e4h
_ErrDomain		equ 49e7h
_ErrIncrement		equ 49eah
_ErrInvalid		equ 4a11h
_ErrIterations		equ 4a2ch
_ErrLinkXmit		equ 4a38h
_ErrMemory		equ 4a0bh
_ErrNon_Real		equ 49edh
_ErrNonReal		equ 495ah
_ErrNotEnoughMem		equ 4441h
_ErrOverflow		equ 49e1h
_ErrSignChange		equ 4a29h
_ErrStat		equ 4a23h
_ErrStatPlot		equ 4a35h
_ErrSyntax		equ 49f0h
_ErrTolTooSmall		equ 4a32h
_ErrUndefined		equ 4a08h
_EToX		equ 40b4h
_ExLp		equ 4228h
_ExpToHex		equ 4249h

_FillRect		equ 4d02h
_FillRectPattern		equ 4d38h
_Find_Parse_Formula		equ 49b1h
_FindAlphaDn		equ 491eh
_FindAlphaUp		equ 491bh
_FindSym		equ 42dch
_FiveExec		equ 45b5h
_FixTempCnt		equ 4912h
_FormEReal		equ 4885h
_FormReal		equ 488Bh
_FourExec		equ 45b2h
_FPAdd		equ 4072h
_FPDiv		equ 4099h
_FPMult		equ 4084h
_FPRecip		equ 4096h
_FPSquare		equ 4081h
_FPSub		equ 406fh
_Frac		equ 4093h

_Get_Tok_Strng		equ 44F8h
_GetBaseVer		equ 4b8eh
_GetCSC		equ 400fh
_GetKey		equ 486dh
_GetLToOP1		equ 4573h
_GetTokLen		equ 44f5h
_GrBufClr         equ 4AD4h
_GrBufCpy		equ 4789h
_GrphCirc		equ 46f9h

_HLTimes9		equ 40e4h
_HorizCmd		equ 47c5h
_HTimesL		equ 4270h

_IBounds		equ 4aa4h
_IBoundsFull		equ 4d4dh
_ILine		equ 4702h
_IncLstSize		equ 4900h
_InsertList		equ 4903h
_InsertMem		equ 42dfh
_Int		equ 40a5h
_Intgr		equ 405dh
_InvCmd		equ 47e3h
_InvertRect		equ 4cffh
_InvOP1S		equ 408dh
_InvOP1SC		equ 408ah
_InvOP2S		equ 4090h
_InvSub		equ 4063h
_IOffset		equ 4aa7h
_IPoint		equ 4705h
_IsA2ByteTok	equ 4291h
_IsEditEmpty		equ 483Ah

_JError		equ 4a4dh
_JErrorNo		equ 4c03h
_JForceCmdNoChar		equ 401eh

_KeyToString		equ 452eh

_LCD_BUSY		equ 4051h
_LCD_DRIVERON		equ 4873h
_LdHLind		equ 4000h
_LineCmd		equ 47cbh
_LnX		equ 40abh
_Load_SFont		equ 46b7h
_LoadPattern		equ 4c51h
_LogX		equ 40aeh

_Max		equ 4057h
_MemChk		equ 42cdh
_MemClear		equ 4a47h
_MemSet		equ 4a4ah
_Min		equ 4054h
_Minus1		equ 406ch
_Mov10B		equ 413eh
_Mov18B		equ 46fch
_Mov9B		equ 4141h
_Mov7B		equ 414ah
_Mov8B		equ 4147h
_Mov9ToOP1		equ 416bh
_Mov9OP1OP2		equ 416eh
_Mov9ToOP2		equ 4171h
_MovFrOP1  equ 4174h

_NewLine		equ 4495h

_OP1ExOP2		equ 4225h
_OP1ExOP3		equ 421fh
_OP1ExOP4		equ 4222h
_OP1ExOP5		equ 4219h
_OP1ExOP6		equ 421ch
_OP1ExpToDec		equ 424ch
_OP1Set0		equ 41d1h
_OP1Set1		equ 4192h
_OP1Set2		equ 419eh
_OP1Set3		equ 4198h
_OP1Set4		equ 4195h
_OP1ToOP2		equ 4111h
_OP1ToOP3		equ 4105h
_OP1ToOP4		equ 40f9h
_OP1ToOP5		equ 4135h
_OP1ToOP6		equ 4132h
_OP2Set0		equ 41ceh
_OP2Set1		equ 41aah
_OP2Set2		equ 41a1h
_OP2Set3		equ 418fh
_OP2Set4		equ 418ch
_OP2Set5		equ 4186h
_OP2Set60		equ 497bh
_OP2Set8		equ 4183h
_OP2SetA		equ 4189h
_OP2ToOP1		equ 4138h
_OP2ToOP3		equ 415fh
_OP2ToOP4		equ 40fch
_OP2ToOP5		equ 412ch
_OP2ToOP6		equ 412fh
_OP3Set0		equ 41cbh
_OP3Set1		equ 4180h
_OP3Set2		equ 419bh
_OP3ToOP1		equ 4120h
_OP3ToOP2		equ 4102h
_OP3ToOP4		equ 40f6h
_OP3ToOP5		equ 4129h
_OP4Set0		equ 41c8h
_OP4Set1		equ 417dh
_OP4ToOP1		equ 411ah
_OP4ToOP2		equ 40ffh
_OP4ToOP3		equ 4162h
_OP4ToOP5		equ 4126h
_OP4ToOP6		equ 4168h
_OP5Set0		equ 41c5h
_OP5ToOP1		equ 411dh
_OP5ToOP2		equ 4108h
_OP5ToOP3		equ 4165h
_OP5ToOP4		equ 410eh
_OP5ToOP6		equ 410bh
_OP6ToOP1		equ 4117h
_OP6ToOP2		equ 4114h
_OP6ToOP5		equ 4123h

_ParseInp		equ 4966h
_PDspGrph		equ 47c2h
_PixelTest		equ 47d4h
_Plus1		equ 4069h
_PointCmd		equ 47d1h
_PointOn		equ 4a56h
_PopOP1		equ 4345h
_PopOP3		equ 4342h
_PopOP5		equ 433fh
_PopReal		equ 435ah
_PopRealO1		equ 4357h
_PopRealO2		equ 4354h
_PopRealO3		equ 4351h
_PopRealO4		equ 434eh
_PopRealO5		equ 434bh
_PopRealO6		equ 4348h
_PosNo0Int		equ 4234h
_PToR		equ 40deh
_PushOP1		equ 438ah
_PushOP3		equ 4387h
_PushOP5		equ 4384h
_PushReal		equ 4381h
_PushRealO1		equ 437eh
_PushRealO2		equ 437bh
_PushRealO3		equ 4378h
_PushRealO4		equ 4375h
_PushRealO5		equ 4372h
_PushRealO6		equ 436fh
_PutAway		equ 4039h
_PutC		equ 446bh
_PutMap		equ 4468h
_PutPS		equ 4477h
_PutPSB		equ 4474h
_PutS		equ 4471h
_PutTokString		equ 485eh
_PutToL		equ 4582h

_RandInit		equ 4c21h
_Random		equ 4b9dh
_Rcl_StatVar		equ 42c7h
_RclAns		equ 4996h
_RclGDB2		equ 46f3h
_RclSysTok		equ 49a2h
_RclVarSym		equ 499fh
_RclX		equ 499ch
_RclY		equ 4999h
_RedimMat		equ 48fdh
_Regraph		equ 47aah
_ReleaseBuffer		equ 46A5h
_ReloadAppEntryVecs		equ 4a50h
_RestoreDisp		equ 478ch
_RndGuard		equ 409fh
_RnFx		equ 40a2h
_Round		equ 40a8h
_RToD		equ 4078h
_RToP		equ 40e1h
_RunIndicOff		equ 44d7h
_RunIndicOn		equ 44d4h

_SaveDisp		equ 4ba3h
_SendVarCmd		equ 48F1h
_SetFuncM		equ 4762h
_SetXXOP1		equ 46c0h
_SetXXOP2		equ 46c3h
_SetXXXXOP2		equ 46c6h
_SFont_Len		equ 46bah
_ShRAcc		equ 41E6h
_Sin		equ 40bdh
_SinCosRad		equ 40bah
_SqRoot		equ 409ch
_SrchVLstDn		equ 4c36h
_SrchVLstUp		equ 4c33h
_SStringLength		equ 4c54h
_StMatEl		equ 49a5h
_StoAns		equ 498ah
_StoGDB2		equ 46f0h
_StoOther		equ 4993h
_StoRand		equ 4e5eh
_StoSysTok		equ 4987h
_StoX		equ 4990h
_StoY		equ 498dh
_StrCopy		equ 444ah
_StrLength		equ 4a5ch

_Tan		equ 40c3h
_TenX		equ 40b7h
_ThreeExec		equ 45ach
_Times2		equ 4066h
_TimesPt5		equ 407eh
_ToFrac		equ 4594h
_Trunc		equ 4060h

_UCLines		equ 46c9h
_UnLineCmd		equ 47ceh
_UnOpExec		equ 45a9h

_VertCmd		equ 47c8h
_VPutMap		equ 44c5h
_VPutS		equ 44c8h
_VPutSN		equ 44cbh
_VtoWHLDE		equ 471dh

_XftoI		equ 4726h
_XitoF		equ 4720h
_XName		equ 4279h
_XRootY		equ 46d2h

_YftoI		equ 4723h
_YName		equ 427ch
_YtoX		equ 46d5h

_Zero16D		equ 41b9h
_ZeroOP		equ 41e0h
_ZeroOP1		equ 41d7h
_ZeroOP2		equ 41dah
_ZeroOP3		equ 41ddh
_ZmDecml		equ 476eh
_ZmFit		equ 477ah
_ZmInt		equ 476bh
_ZmPrev		equ 4771h
_ZmSquare		equ 477dh
_ZmStats		equ 46d8h
_ZmTrig		equ 4780h
_ZmUsr		equ 4774h
_ZooDefault		equ 4786h

;======================================================================
;           system-defined ram variable address equates
;======================================================================
;


ramStart             equ  8000h
appData              equ  8000h
ramCode              equ  8100h
ramCodeEnd           equ  822fh
baseAppBrTab         equ  8230h
bootTemp             equ  8241h
appSearchPage        equ  8293h
tempSwapArea         equ  8295h
appID                equ  837dh
ramReturnData        equ  8529h
appInfo              equ  8998h
appBank_jump         equ  85c9h
appPage              equ  85cbh
kbdScanCode          equ  83d8h
kbdKey               equ  83ddh
kbdGetKy             equ  83deh
keyExtend            equ  83dfh
contrast             equ  83e0h
apdSubTimer          equ  84e0h
apdTimer             equ  84e1h
curTime              equ  83e1h
curRow               equ  83e2h
curCol               equ  83e3h
curOffset            equ  83e4h
curUnder             equ  83e5h
curY                 equ  8498h
curType              equ  8499h
curXRow              equ  849ah
prevDData            equ  849bh
lFont_record         equ  84a3h
sFont_record         equ  84abh
tokVarPtr            equ  85a1h
tokLen               equ  85a3h
indicMem             equ  84d6h
indicCounter         equ  84deh
indicBusy            equ  84dfh
OP1                  equ  842bh
OP1M                 equ  842dh
OP2                  equ  8436h
OP2M                 equ  8485h
OP2EXT               equ  843fh
OP3                  equ  8441h
OP3M                 equ  8443h
OP4                  equ  844ch
OP4M                 equ  844eh
OP5                  equ  8457h
OP5M                 equ  8459h
OP6                  equ  8462h
OP6M                 equ  8464h
OP6EXT               equ  846bh
progToEdit           equ  8472h
nameBuff             equ  847ah
equ_edit_save        equ  8485h
iMathPtr1            equ  8486h
iMathPtr2            equ  8488h
iMathPtr3            equ  848ah
iMathPtr4            equ  848ch
iMathPtr5            equ  848eh
chkDelPtr1           equ  8490h
chkDelPtr2           equ  8492h
chkDelPtr3           equ  9baah
chkDelPtr4           equ  9bach
insDelPtr            equ  8494h
upDownPtr            equ  8496h
fOutDat              equ  85a5h
asm_data_ptr1        equ  85afh
asm_data_ptr2        equ  85b1h
asm_sym_ptr1         equ  85b3h
asm_sym_ptr2         equ  85b5h
asm_ind_call         equ  85b7h
textShadow           equ  8913h
textShadCur          equ  8993h
textShadTop          equ  8995h
textShadAlph         equ  8996h
textShadIns          equ  8997h
cxMain               equ  84e2h
cxPPutAway           equ  84e4h
cxPutAway            equ  84e6h
cxRedisp             equ  84e8h
cxErrorEP            equ  84eah
cxSizeWind           equ  84ech
cxPage               equ  84eeh
cxCurApp             equ  84efh
cxPrev               equ  84f0h
monQH                equ  850eh
monQT                equ  850fh
monQueue             equ  8510h
onSP                 equ  8520h
promptRow            equ  89a2h
promptCol            equ  89a3h
promptIns            equ  89a4h
promptShift          equ  89a5h
promptRet            equ  89a6h
promptValid          equ  89a8h
promptTop            equ  89aah
promptCursor         equ  89ach
promptTail           equ  89aeh
promptBtm            equ  89b0h
varType              equ  89b2h
varCurrent           equ  89b3h
varClass             equ  89bbh
menuActive           equ  84b3h
menuAppDepth         equ  84b4h
MenuCurrent          equ  84b5h
ProgCurrent          equ  84c1h
userMenuSA           equ  89e1h
ioPrompt             equ  8a41h
dImageWidth          equ  8a42h
RectFillPHeight		 equ  8A42h
RectFillPWidth		 equ  8a43h
RectFillPattern		 equ  8a44h
ioFlag               equ  83e6h
sndRecState          equ  83e8h
ioErrState           equ  83e9h
header               equ  83eah
ioData               equ  83f3h
bakHeader            equ  83ffh
penCol               equ  89dfh
penRow               equ  89e0h
rclQueue             equ  84d2h
rclQueueEnd          equ  84d4h
errNo                equ  854ah
errSP                equ  854bh
errOffset            equ  854dh
saveSScreen          equ  8a52h
bstCounter           equ  8559h
flags                equ  855bh
statVars             equ  8d58h
curGStyle            equ  8e94h
curGY                equ  8e95h
curGX                equ  8e96h
curGY2               equ  8e97h
curGX2               equ  8e98h
freeSaveY            equ  8e99h
freeSaveX            equ  8e9ah
XOffset              equ  8ec8h
YOffset              equ  8ec9h
lcdTallP             equ  8ecah
pixWideP             equ  8ecbh
pixWide_m_1          equ  8ecch
pixWide_m_2          equ  8ecdh
lastEntryPTR         equ  8eceh
lastEntryStk         equ  8ed0h
numLastEntries       equ  90d0h
currLastEntry        equ  90d1h
curInc               equ  92deh
uXmin                equ  92f5h
uXmax                equ  92feh
uXscl                equ  9307h
uYmin                equ  9310h
uYmax                equ  9319h
uYscl                equ  9322h
uThetMin             equ  932bh
uThetMax             equ  9334h
uThetStep            equ  933dh
uTmin                equ  9346h
uTmax                equ  934fh
uTStep               equ  9358h
uPlotStart           equ  9361h
unMax                equ  936ah
uu0                  equ  9373h
uv0                  equ  937ch
unMin                equ  9385h
uu02                 equ  938eh
uv02                 equ  9397h
uw0                  equ  93a0h
uPlotStep            equ  93a9h
uXres                equ  93b2h
uw02                 equ  93bbh
Xmin                 equ  93c7h
Xmax                 equ  93d0h
Xscl                 equ  93d9h
Ymin                 equ  93e2h
Ymax                 equ  93ebh
Yscl                 equ  93f4h
ThetaMin             equ  93fdh
ThetaMax             equ  9406h
ThetaStep            equ  940fh
TminPar              equ  9418h
TmaxPar              equ  9421h
Tstep                equ  942ah
PlotStart            equ  9433h
nMax                 equ  943ch
u0                   equ  9445h
v0                   equ  944eh
nMin                 equ  9457h
u02                  equ  9460h
v02                  equ  9469h
w0                   equ  9472h
PlotStep             equ  947bh
XresO                equ  9484h
w02                  equ  948dh
smallEditRAM         equ  949fh
XFact                equ  950bh
YFact                equ  9514h
Xres_int             equ  951dh
deltaX               equ  951eh
deltaY               equ  9527h
shortX               equ  9530h
shortY               equ  9539h
lower                equ  9542h
upper                equ  954bh
XOutSym              equ  9558h
XOutDat              equ  955ah
YOutSym              equ  955ch
YOutDat              equ  955eh
inputSym             equ  9560h
inputDat             equ  9562h
prevData             equ  9564h
TblMin               equ  9676h
TblStep              equ  967fh
P1Type		     EQU  968ch
SavX1List	     EQU  968dh
SavY1List	     EQU  9692h
SavF1List	     EQU  9697h
P1FrqOnOff	     EQU  96abh
P2Type		     EQU  96aeh
SavX2List	     EQU  96afh
SavY2List	     EQU  96b4h
SavF2List	     EQU  96b9h
P2FrqOnOff	     EQU  96cdh
P3Type		     EQU  96d0h	
SavX3List	     EQU  96d1h
SavY3List	     EQU  96d6h
SavF3List	     EQU  96d8h
P3FrqOnOff	     EQU  96efh
plotSScreen          equ  9737h
seed1                equ  9a37h
seed2                equ  9a40h
cmdCursor            equ  9a7eh
editTop              equ  9a82h
editCursor           equ  9a84h
editTail             equ  9a86h
editBtm              equ  9a88h
editSym              equ  9a94h
editDat              equ  9a96h
winTop               equ  9b33h
winBtm               equ  9b34h
winLeftEdge          equ  9b35h
winLeft              equ  9b36h
winAbove             equ  9b38h
winRow               equ  9b3ah
winCol               equ  9b3ch
fmtDigits            equ  9b3eh
fmtString            equ  9b3fh
fmtConv              equ  9b80h
fmtLeft              equ  9b92h
fmtIndex             equ  9b94h
fmtMatSym            equ  9b96h
fmtMatMem            equ  9b98h
EQS                  equ  9b9ah
tSymPtr1             equ  9ba6h
tSymPtr2             equ  9ba8h
tempMem              equ  9baeh
fpBase               equ  9bb0h
FPS                  equ  9bb2h
OPBase               equ  9bb4h
OPS                  equ  9bb6h
pTempCnt             equ  9bb8h
cleanTmp             equ  9bbah
pTemp                equ  9bbch
progPtr              equ  9bbeh
newDataPtr           equ  9bc0h
appErr1              equ  9bc2h
appErr2              equ  9bcfh
appRawKeyHandle      equ  85b8h
appBackUpScreen      equ  8613h
customHeight         equ  8612h
userMem              equ  9c37h
symTable             equ  0fe66h

vat = progptr
;====================================
; equates for fraction entry routines
;====================================
fieldMax    equ    OP5
fieldLen    equ    fieldMax+1
fieldCol    equ    fieldLen+1     ; pencol of start
fieldRow    equ    fieldCol+1     ; penrow of start  3 rows above char
fieldInput  equ    fieldRow+1     ; 9 bytes,
wholeMax    equ    tempSwapArea
wholeLen    equ    wholeMax+1
wholeCol    equ    wholeLen+1     ; pencol of start
wholeRow    equ    wholeCol+1     ; penrow of start  3 rows above char
wholeInput  equ    wholeRow+1     ; 9 bytes,
;
numerMax    equ    wholeInput+9
numerLen    equ    numerMax+1
numerCol    equ    numerLen+1     ; pencol of start
numerRow    equ    numerCol+1     ; penrow of start
numerInput  equ    numerRow+1     ; 9 bytes,
;
denomMax    equ    numerInput+9
denomLen    equ    denomMax+1
denomCol    equ    denomLen+1     ; pencol of start
denomRow    equ    denomCol+1     ; penrow of start
denomInput  equ    denomRow+1     ; 9 bytes,
;
fracLineLen equ   denomInput+9
fracLineCol equ   fracLineLen+1
;
fracRightEdge  equ fracLineCol+1
;
curPosition equ   fracRightEdge+1
;
numerSpaces equ   curPosition+1
denomSpaces equ   numerSpaces+1
haveFrac    equ   3
haveUnit    equ   4
incCurPos   equ   5

;======================================================================
;           system variable equates
;======================================================================
;       entry points : rclsystok, stosystok
;
;       vars with no restriction
;
XSCLt       equ  3           ; 'xscl'           4
YSCLt       equ  4           ; 'yscl'           4
;
;       vars rounded to 10 digits
;
XMINt       EQU  0AH         ; 'xMin'           4
XMAXt       EQU  0BH         ; 'xMax'           4
YMINt       EQU  0CH         ; 'yMin'           4
YMAXt       EQU  0DH         ; 'yMax'           4
TBLMINt     EQU  1AH         ; 'TBLMIN'
;
;       rouned to 10 digits, and positve integers
;
PLOTSTARTt   equ  1bh         ; 'nstart'         5
UPLOTSTARTt  equ  1ch         ; 'znstart'        6

;       vars cannot be 0
;
TBLSTEPt    EQU  21H         ; 'TBLSTEP'
TSTEPt      EQU  22H         ; 'tStep'          5
THETSTEPt   EQU  23H         ; Ltheta,'Step'    5
UTSTEPt     EQU  24H         ; 'ztStep'         6
UTHSTEPt    EQU  25H         ; 'z',Ltheta,'Step' 6
;
;       special limits on these
;
DELTAXt     EQU  26H         ; LcapDelta,'x'    2
DELTAYt     EQU  27H         ; LcapDelta,'y'    2
;
;       special limits on these
;
XFACTt      EQU  28H         ; 'xFact'          5
YFACTt      EQU  29H         ; 'yFact'          5
;
;       sys var list
;
TBLINPUTt       equ       2ah
;
;
;
PLOTSTEPt       equ       34h
UPLOTSTEPt      equ       35h
;
XRESt           equ       36h
UXRESt          equ       37h
;
; special token used by input/prompt for storing value to variable

INPUTt      equ  40h         ; set up for store for input/prompt

; special token used by graph math intersection routine to indicate
; list data

ISECTt      equ  41h         ; list data follows this
;
;========== automatic power down time out value (<= 7 minutes)
apdTimeOut	equ	5		;automatic power down time (minutes)

;========== interrupt equate
iAll 	equ 1011b

;========== i/o ports
intrptEnPort	equ 3 ; interrupt enable (in/out) 
memPageAPort	equ	6		; memory page a	(i/o)
LCDInstPort     equ     10h
LCDDataPort     equ     11h 


;========== common subroutine rst numbers
rOP1TOOP2	EQU  08h
rFINDSYM	EQU  10h
rPUSHREALO1	EQU  18h
rMOV9TOOP1	EQU  20h
rFPADD		EQU  30h

;========== run indicators

busyPause	equ	10101010b
busynNormal	equ	11110000b

;======================================================================
;           system and state flags
;======================================================================
;

trigFlags	equ  0		;trigonometry mode settings
trigDeg 	equ    2	; 1=degrees, 0=radians

kbdFlags	equ  0		;keyboard scan
kbdScr		equ    3	; 1=scan code ready
kbdKeyPress	equ    4	; 1=key has been pressed

doneFlags	equ  0		;display "done"
donePrgm	equ    5	; 1=display "done" after prgm

ioDelFlag	equ  0
inDelete	equ    0	;1 = delete screen 

;----------------------------------------------------------------------
editFlags	equ  1
editOpen	equ    2	; 1=edit buffer is open

monFlags	equ  1		;monitor flags
monAbandon	equ    4	; 1=don't start any long process
				; in put away (#715)
;----------------------------------------------------------------------
plotFlags	equ  2		;plot generation flags
plotTrace	equ  0
plotLoc 	equ    1	; 0=bkup & display, 1=display only
plotDisp	equ    2	; 1=plot is in display, 0=text in display


grfModeFlags	equ  2		;graph mode settings
grfFuncM	equ    4	; 1=function graph
grfPolarM	equ    5	; 1=polar graph
grfParamM	equ    6	; 1=parametric graph
grfRecurM       equ    7        ; 1=recursion graph

graphFlags	equ  3
graphProg	equ  1     ; 1=graph in progress
graphDraw	equ    0	; 0=graph is valid, 1=redraw graph

grfDBFlags	equ  4
grfDot		equ    0	; 0=line, 1=dot
grfSimul	equ    1	; 0=sequential, 1=simultaneous
grfGrid 	equ    2	; 0=no grid, 1=grid
grfPolar	equ    3	; 0=rectangular, 1=polar coordinates
grfNoCoord	equ    4	; 0=display coordinates, 1=off
grfNoAxis	equ    5	; 0=axis, 1=no axis
grfLabel	equ    6	; 0=off, 1=axis label

textFlags	equ  5		;text output flags
textEraseBelow	equ    1	; 1=erase line below small char
textScrolled	equ    2	; 1=screen scrolled
textInverse	equ    3	; 1=display inverse bit-map
textInsMode	equ    4	; 0=overstrike, 1=insert mode

parsFlag2	equ  7		;parser flags
numOP1		equ    0	; 1=result in op1, 0=no result

newDispF        equ  8		;derivative mode flags
preClrForMode   equ    0	; 1=help blink on mode screen

apdFlags	equ  8		;automatic power-down
apdAble 	equ    2	; 1=apd enabled
apdRunning	equ    3	; 1=apd clock running


web_err_mask    equ  60h

onFlags 	equ  9		;on key flags
onRunning	equ	 3
onInterrupt	equ    4	; 1=on key interrupt request

statFlags	equ  9		;statistics flags
statsValid	equ    6	; 1=stats are valid

fmtFlags	equ  10		;numeric format flags
fmtExponent	equ    0	; 1=show exponent, 0=no exponent
fmtEng		equ    1	; 1=engineering notion, 0=scientific
fmtHex		equ    2	; 1=hexadecimal
fmtOct		equ    3	; 1=octal
fmtBin		equ    4	; 1=binary
;
numMode         equ  10
fmtReal         equ    5
fmtRect         equ    6
fmtPolar        equ    7

realMode        equ    5
rectMode        equ    6
polarMmode       equ    7
;					;   if hex and oct both = 1
;					;   then bin=0 means >frac
;					;	 bin=1 means >dms
fmtBaseMask     equ  00011100b		; mask to base flags
fmtBaseShift    equ  2			; offset to base flags
;
;       check if these are used by numform,
;
;               equ  6
;               equ  7

fmtOverride	equ  11		;copy of fmtflags with conversion override

fmtEditFlags	equ  12		;numeric editing flags
fmtEdit 	equ    0	; 1=format number for editing

curFlags	equ  12		;cursor
curAble 	equ    2	; 1=cursor flash is enabled
curOn		equ    3	; 1=cursor is showing
curLock 	equ    4	; 1=cursor is locked off

appFlags	equ  13		;application flags
appWantIntrpt	equ    0		; 1=want on key interrupts
appTextSave	equ    1		; 1=save characters in textshadow
appAutoScroll	equ    2		; 1=auto-scroll text on last line
appMenus	equ    3		; 1=process keys that bring up menus
					; 0=check lock menu flag
appLockMenus	equ    4		; 1=ignore menu keys
					; 0=switch to home screen and bring up menu
appCurGraphic	equ    5		; 1=graphic cursor
appCurWord	equ    6		; 1=text cursor covers entire word
appExit 	equ    7		; 1=application handles [exit] key itself

appWantIntrptF	equ	1<<appWantIntrpt
appTextSaveF	equ	1<<appTextSave
appAutoScrollF	equ	1<<appAutoScroll
appMenusF	equ	1<<appMenus
appLockMenusF	equ	1<<appLockMenus
appCurGraphicF	equ	1<<appCurGraphic
appCurWordF	equ	1<<appCurWord
appExitF	equ	1<<appExit


seqFlags        equ  15              ; sequential graph flags
webMode         equ    0             ; 0 = normal seq mode, 1 = web mode
webVert         equ    1             ;
sequv           equ    2             ; u vs v
seqvw           equ    3             ; v vs w
sequw           equ    4             ; u vs w


promptFlags	equ  17		;prompt line flags
promptEdit	equ    0	; 1=editing in prompt buffer

indicFlags	equ  18		;indicator flags
indicRun	equ    0	; 1=run indicator on
indicInUse	equ    1	; indicator save area in use=1, free=0
indicOnly	equ    2	; interrupt handler only checks run indicator

shiftFlags	equ  18		;[2nd] and [alpha] flags
shift2nd	equ    3	; 1=[2nd] has been pressed
shiftAlpha	equ    4	; 1=[alpha] has been pressed
shiftLwrAlph	equ    5	; 1=lower case, 0=upper case
shiftALock	equ    6	; 1=alpha lock has been pressed
shiftKeepAlph	equ    7	; 1=cannot cancel alpha shift


tblFlags        equ  19		;table flags.
autoFill        equ    4	; 1=prompt, 0=fillauto
autoCalc        equ    5	; 1=prompt, 0=calcauto
reTable         equ    6	; 0=table is ok, 1=must recompute table.


fracMode        equ      20             ;FRAC FLAGS
ABCF            equ       0             ; a b/c if 1, b/c if 0.
AutoSimpF       equ       1             ; autosimp if 0, mansimp if 1.
ManSimpF        equ       1             ; autosimp if 0, mansimp if 1.

ConstMode       equ     20
Hide            equ       2            ;0 if show, 1 if hide.
Multi           equ       3            ;0 if single, 1 if multiple.

sGrFlags	equ  20
write_on_graph  equ    4	; 1 = text or equ writing to graph screen
g_style_active  equ    5	; 1 = graph styles are enabled, use them
cmp_mod_box     equ    6	; 1 = doing mod box plot computation
textWrite       equ    7
;

newIndicFlags   equ  21
extraIndic      equ    0
saIndic         equ    1

smartFlags      equ  23
;
;----------------------------------------------------------------------
; note: fix these equates if smartflags are moved                            
;----------------------------------------------------------------------
;
smarter_mask    equ  3
smarter_test    equ  1
smartGraph      equ  0
smartGraph_inv  equ  1                                                 

;----------------------------------------------------------------------
;           available for asm programming
;----------------------------------------------------------------------

asm_Flag1       equ  33          ; asm coding
asm_Flag2       equ  34          ; asm coding
asm_Flag3       equ  35          ; asm coding

;----------------------------------------------------------------------

fracFlags	equ	  36		; fraction display/edit
fracStopEOS	equ	   0
fracInside	equ	   1
fracDontDraw	equ	   2
fracEditMode	equ	   3
fracInNum	equ	   4
fracScrScrolled equ	   5
fracFlag1	equ	   6
fracClearEOS	equ	   7	   ; tell drawExpr code to clear to end of screen when done
;
fontFlags	equ	   37
fracDrawNum	equ	   0
fracDrawDenom	equ	   1
fracDrawLFont	equ	   2
fracTallLFont	equ	   3
fracLastChar	equ	   4
fracCursor	equ	   5
DrawLfont       equ       6
CustomFont      equ       7
;
;flags for fraction math routines:
FracFlags2      equ     38
DtoF10F         equ      0      ;10digit flag for Dec ->Frac
SimpKeyF        equ      1      ;Used for Simp K.
RatD1K          equ      1      ;also used to show denominator=1000
FinalType       equ      2      ;1=final type is improper 0=type is simple
FinalFrac       equ      3      ;set if final fraction result expected.
ManDtoF         equ      4      ;manual simp=1, Dec->frac bit.
ManForceS       equ      5      ;manual simp=1, Abc=0; force simplify anyhow.
ManAdjW         equ      6      ;manual simp=1, Abc=0; Adjust whole flag.
ManBin          equ      7      ;manual simp=1, Binop set.


;flags for fraction math routines:
FracFlags3      equ     39
FNormOver       equ      0      ;Fraction Normalization Override if set.
DivSmallNum     equ      1      ;set if Frac Numerator < denominator.
IntDivDisp      equ      2      ;integer divide display flag.
StrictInt       equ      3      ;strict integer check by intdiv if set.
yxRatF          equ      4      ;y^x rational approximation flag.
nid             equ      5      ;1 if non integer decimal
SimpDisp        equ      6      ;1 if need to display frac simplify factor.
HadTrueMixed    equ      7      ;at least one true fraction in an expression.
;(HadTrueMixed is needed by >Ab/c<>d/e so that if any mixed fraction, then
; if convert, will stay b/c if b/c mode & not convert back to mixed.)

;
getSendFlg	equ  47
comFailed	equ  3	      ; 1 = get/send communication failed
;
;
apiFlg3		equ  52 
;
apiFlg4		equ  53
fullScrnDraw	equ    2	; draw into last row/col of screen

xapFlag0        equ  43      ; external app flags
xapFlag1        equ  44
xapFlag2        equ  45
xapFlag3        equ  46

plotFlag3	equ  47  
bufferOnly	equ    6
useFastCirc	equ    4

;
varTypeMask	equ  1fh             
varGraphRef	equ    6

#IFNDEF NOTOKENS
;==========================================
;	tokens
;==========================================

EOSSTART        EQU       0
;
;
;   DISPLAY CONVERSIONS COME IMMEDIATELY BEFORE 'TSTORE'
;
DCONV   EQU     01h
;
tToDMS          EQU       DCONV       ; 01h
tToDEC          EQU       DCONV+1     ; 02h
tToAbc          EQU       DCONV+2     ; 03h    > A b/c
tUnredF         equ     03h     ;down arrow for unreduced fraction token

tStore          EQU       DCONV+3     ; 04h Lstore                01
;
tBoxPlot        EQU       05h
;
BRACKS          EQU       06h
;
tLBrack         EQU       BRACKS      ; 06h '['
tRBrack         EQU       BRACKS+1    ; 07h ']'
tLBrace         EQU       BRACKS+2    ; 08h '{'
tRBrace         EQU       BRACKS+3    ; 09h '}'

tPOST1          EQU       BRACKS+4
;
tFromRad        EQU       tPOST1       ; 0Ah Lradian
tFromDeg        EQU       tPOST1+1     ; 0Bh Ldegree
tRecip          EQU       tPOST1+2     ; 0Ch Linverse
tSqr            EQU       tPOST1+3     ; 0Dh Lsquare
tTrnspos        EQU       tPOST1+4     ; 0Eh Ltranspose
tCube           EQU       tPOST1+5     ; 0Fh '^3'
;
tLParen         EQU       10h         ; 10h '('
tRParen         EQU       11h         ; 11h ')'

;   immediate unary tokens go from here to 'eosel-1'
;
IMUN            EQU       12h
;
tRound          EQU       IMUN        ; 12h 'round'
tPxTst          EQU       IMUN+1      ; 13h 'PXL-TEST'
tAug            EQU       IMUN+2      ; 14h 'aug'
tRowSwap        EQU       IMUN+3      ; 15h 'rSwap'
tRowPlus        EQU       IMUN+4      ; 16h 'rAdd'
tmRow           EQU       IMUN+5      ; 17h 'multR'
tmRowPlus       EQU       IMUN+6      ; 18h 'mRAdd'
tMax            EQU       IMUN+7      ; 19h 'max'
tMin            EQU       IMUN+8      ; 1Ah 'min'
tRToPr          EQU       IMUN+9      ; 1Bh 'R>Pr
tRToPo          EQU       IMUN+10     ; 1Ch 'R>Po
tPToRx          EQU       IMUN+11     ; 1Dh 'P>Rx
tPToRy          EQU       IMUN+12     ; 1Eh 'P>Ry
tMedian         EQU       IMUN+13     ; 1Fh 'MEDIAN
tRandM          EQU       IMUN+14     ; 20h 'randM'
tMean           EQU       IMUN+15     ; 21h
tLstMode        equ       IMUN+16     ; 22h 'mode('
;the below tokens are immediate, but require an expression as 1st arg & so
;are grouped together...
tRoot           equ       IMUN+17     ; 23h 'solve('
tSeries         equ       IMUN+18     ; 24h 'seq'
tEvalF          equ       IMUN+20     ; 26h             ;keep until sure.
IMUNEND         equ     IMUN+20         ;end of immediate unaries.

tSolveEqual     equ     IMUN+21         ;teq ('=') in solver -> tsolveequal.
tEOFrac         equ     IMUN+22         ; 28h end of fraction in home screen.
;
;
;  token 29h cannot be an eos function since el(=29h already
;
tEOSEL          EQU       IMUN+23
;
tSpace          EQU       tEOSEL       ; 29h   ' '
tString         EQU       tEOSEL+1     ; 2Ah  '"'
tComma          EQU       tEOSEL+2     ; 2Bh  ','
tRemain         equ     2ch     ;'r' for remainder display.
; tremain is needed in home screen where turn display chars into tokens.
; (there is no token for small 'r').
; ===== postfix functions
;
tPost           equ       2eh
;
tFact           equ       tPost       ; 2eh '!'
tPercent        equ     tPost+1       ; 2fh '%'                 new

;=============number tokens

NUMS            EQU       30h
;
t0              EQU       NUMS        ; 30h
t1              EQU       NUMS+1      ; 31h
t2              EQU       NUMS+2      ; 32h
t3              EQU       NUMS+3      ; 33h
t4              EQU       NUMS+4      ; 34h
t5              EQU       NUMS+5      ; 35h
t6              EQU       NUMS+6      ; 36h
t7              EQU       NUMS+7      ; 37h
t8              EQU       NUMS+8      ; 38h
t9              EQU       NUMS+9      ; 39h
tDecPt          EQU       NUMS+10     ; 3Ah
tee             EQU       NUMS+11     ; 3Bh

;=======================
;   binary op
;=======================
tOr             EQU       3Ch         ; 3Ch  '_or_'
tXor            EQU       3Dh         ; 3Dh
;
tColon          EQU       3Eh         ; 3Eh ':'
tEnter          EQU       3Fh         ; 3Fh  Lenter
;
tAnd            EQU       40h         ; 40h  '_and_'       

;========================
;       letter tokens
;========================
LET             EQU       41h
;
tA              EQU       LET         ; 41h
tB              EQU       LET+1       ; 42h
tC              EQU       LET+2       ; 43h
tD              EQU       LET+3       ; 44h
tE              EQU       LET+4       ; 45h
tF              EQU       LET+5       ; 46h
tG              EQU       LET+6       ; 47h
tH              EQU       LET+7       ; 48h
tI              EQU       LET+8       ; 49h
tJ              EQU       LET+9       ; 4Ah
tK              EQU       LET+10      ; 4Bh
tL              EQU       LET+11      ; 4Ch
tM              EQU       LET+12      ; 4Dh
tN              EQU       LET+13      ; 4Eh
tO              EQU       LET+14      ; 4Fh
tP              EQU       LET+15      ; 50h
tQ              EQU       LET+16      ; 51h
tR              EQU       LET+17      ; 52h
tS              EQU       LET+18      ; 53h
tT              EQU       LET+19      ; 54h
tU              EQU       LET+20      ; 55h
tV              EQU       LET+21      ; 56h
tW              EQU       LET+22      ; 57h
tX              EQU       LET+23      ; 58h
tY              EQU       LET+24      ; 59h
tZ              EQU       LET+25      ; 5Ah
tEOF            equ       LET+26      ; 5bh
;
;===============================================
;   these var tokens are 1st of a double token
;===============================================
;
;======================================================================
;   THESE VAR TOKENS ARE 1ST OF A DOUBLE TOKEN 
;======================================================================

;
vToks   EQU     LET+27    ;
;
;       USER MATRIX TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarMat         EQU       vToks       ; 5Ch
;
;       USER LIST TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarLst         EQU       vToks+1     ; 5Dh
;
;       USER EQUATION TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarEqu         EQU       vToks+2     ; 5Eh
tProg           EQU       vToks+3     ; 5Fh
;
;       USER PICT TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarPict        EQU       vToks+4     ; 60h
;
;       USER GDB TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarGDB         EQU       vToks+5     ; 61h
tVarOut         EQU       vToks+6     ; 62h
tVarSys         EQU       vToks+7     ; 63h

;================================
; ===== mode setting commands
;================================
MODESA          EQU       vToks+8     ; 64h
;
tRad            EQU       MODESA      ; 64h 'Radian'
tDeg            EQU       MODESA+1    ; 65h 'Degree'
tNormF          EQU       MODESA+2    ; 66h 'Normal'
tSci            EQU       MODESA+3    ; 67h 'Sci'
tEng            EQU       MODESA+4    ; 68h 'Eng'
tFloat          EQU       MODESA+5    ; 69h 'Float'
;
CMPS            EQU       6Ah
;
tEQ             EQU       CMPS        ; 6Ah '=='
tLT             EQU       CMPS+1      ; 6Bh '<'
tGT             EQU       CMPS+2      ; 6Ch '>'
tLE             EQU       CMPS+3      ; 6Dh LLE
tGE             EQU       CMPS+4      ; 6Eh LGE
tNE             EQU       CMPS+5      ; 6Fh LNE

;=======================
;   binary op
;=======================
tAdd            equ       70h         ; 70h '+'
tSub            equ       71h         ; 71h '-'
;
tAns            equ       72h         ; 72h
;
;============================
;===== mode setting commands
;============================
;       graph format tokens are 2 byte tokens
;
tGFormat        equ     73h
;
;
tBoyIcon        equ     78h           ; for stat plots in programs
tTreeIcon       equ     79h           ; for stat plots in programs
tDollarIcon     equ     7ah           ; for stat plots in programs
tFaceIcon       equ     7bh           ; for stat plots in programs
tCircleIcon     equ     7ch           ; for stat plots in programs
tDiamondIcon    equ     7dh           ; for stat plots in programs
tStarIcon       equ     7eh           ; for stat plots in programs
tBoxIcon        equ       7fh
tCrossIcon      equ       80h
tDotIcon        equ       81h
;
;===========================
;       binary op cont.
;===========================
tMul            equ       82h         ; 82h  '*'
tDiv            equ       83h         ; 83h  '/'
tIDiv           equ       84h         ; 84h  'int/'
;
;============================
;       some graph commands
;============================
;
GRCMDS          equ       85h
;
tTrace          EQU       GRCMDS      ; 84h 'Trace'
tClDrw          EQU       GRCMDS+1    ; 85h 'ClDrw'
tZoomStd        EQU       GRCMDS+2    ; 86h 'ZStd'
tZoomtrg        EQU       GRCMDS+3    ; 87h 'Ztrg'
tZoomBox        EQU       GRCMDS+4    ; 88h 'ZBOX'
tZoomIn         EQU       GRCMDS+5    ; 89h 'ZIn'
tZoomOut        EQU       GRCMDS+6    ; 8Ah 'ZOut'
tZoomSqr        EQU       GRCMDS+7    ; 8Bh 'ZSqr'
tZoomInt        EQU       GRCMDS+8    ; 8Ch 'ZInt'
tZoomPrev       EQU       GRCMDS+9    ; 8Dh 'ZPrev'
tZoomDec        EQU       GRCMDS+10   ; 8Eh 'ZDecm'
tZoomStat       EQU       GRCMDS+11   ; 8Fh 'ZStat
tZoomQuad1      equ       GRCMDS+12   ; 91h 'quadrant 1'
tZoomSto        equ       GRCMDS+14   ; 93h  zoom store
tText           equ       GRCMDS+15   ; 94h
;
;=======================
;       binary op
;=======================
tnPr            EQU       GRCMDS+16   ; 94h '_nPr_'
tnCr            EQU       GRCMDS+17   ; 95h '_nCr_'

;=========================================================================
;       more graph commands (start of graph commands requiring arguments)
;=========================================================================

tYOn            EQU       GRCMDS+18   ; 96h 'FnOn_'
tYOff           EQU       GRCMDS+19   ; 97h 'FnOff_'
tStPic          EQU       GRCMDS+20   ; 98h 'StPic_'
tRcPic          EQU       GRCMDS+21   ; 99h 'RcPic_'
tLine           EQU       GRCMDS+22   ; 9bh 'line'
tVert           EQU       GRCMDS+23   ; 9ch 'vert_'
tPtOn           EQU       GRCMDS+24   ; 9dh 'pton'
tPtOff          EQU       GRCMDS+25   ; 9eh 'ptoff'
;
;===============================================================
;  token a0 cannot be an eos function since low mult=a0 already
;===============================================================
tPtChg          EQU       GRCMDS+26   ; 9fh 'ptchg'
tPXOn           EQU       GRCMDS+27   ; a0h
tPXOff          EQU       GRCMDS+28   ; a1h
tPXChg          EQU       GRCMDS+29   ; a2h
tShade          EQU       GRCMDS+30   ; a3h 'shade'
tCircl          EQU       GRCMDS+31   ; a4h 'circl'
tHorz           EQU       GRCMDS+32   ; a5h 'horizontal'
tTanLn          EQU       GRCMDS+33   ; a6h 'tanln'
;tManFit         equ       GRCMDS+34   ; a7h 'manual fit'

;
;available      equ       GRCMDS+35   ; a8h
;
tDrInv          equ       GRCMDS+36   ; a9h 'drinv_'
tDrawF          equ       GRCMDS+36   ; a9h 'drawf_'
;
tVarStrng       equ       0aah
;
;===================================
; ===== functions with no arguments
;===================================
;
NOARG   EQU     0ABh
;
tRand           EQU       NOARG       ; ABh 'rand'
tPi             EQU       NOARG+1     ; ACh  Lpi
tGetKey         EQU       NOARG+2     ; ADh 'getKy'
;
;
tAPost          EQU       tGetKey+1   ; APOSTROPHY
tQuest          EQU       tAPost+1    ; QUESTION MARK
;
;
UNARY   EQU     tQuest+1              ; B0h
;
tChs            EQU       UNARY       ; B0h
tInt            EQU       UNARY+1     ; B1h
tAbs            EQU       UNARY+2     ; B2h
tDet            EQU       UNARY+3     ; B3h
tIdent          EQU       UNARY+4     ; B4h
tDim            EQU       UNARY+5     ; B5h
tSum            EQU       UNARY+6     ; B6h
tProd           EQU       UNARY+7     ; B7h
;tUnit            EQU       UNARY+8     ; B8h
tIPart          EQU       UNARY+9     ; B9h
tFPart          EQU       UNARY+10    ; BAh
;
;================================
;       new 2 byte tokens
;================================
t2ByteTok       equ       0bbh         ;
;
;
UNARYLR EQU     UNARY+12
;
tSqrt           EQU       UNARYLR     ; BCh
tCubRt          EQU       UNARYLR+1   ; BDh
tLn             EQU       UNARYLR+2   ; BEh
tExp            EQU       UNARYLR+3   ; BFh
tLog            EQU       UNARYLR+4   ; C0h
tALog           EQU       UNARYLR+5   ; C1h
tSin            EQU       UNARYLR+6   ; C2h
tASin           EQU       UNARYLR+7   ; C3h
tCos            EQU       UNARYLR+8   ; C4h
tACos           EQU       UNARYLR+9   ; C5h
tTan            EQU       UNARYLR+10  ; C6h
tATan           EQU       UNARYLR+11  ; C7h
LASTUNARY2       equ       UNARYLR+11  ; c7h
tMixSimp        equ     UNARYLR+12 ;c8convert between mixed(ab/c) & simple(b/c)
tFracDec        equ     UNARYLR+13 ;c9h convert between fraction & decimal.
tSimp           equ     UNARYLR+14 ;cah simplify a fraction.

;=================
;  binary op
;=================
tPower          equ       0cbh        ; cbh    '^'
tXRoot          equ       0cch        ; cch    lsupx,lroot

;===================================
;       some programming commands
;===================================
;
PROGTOK equ     UNARYLR+17
;
tSetMenu        equ       PROGTOK     ; cdh
tIf             equ       PROGTOK+1   ; ceh
tThen           equ       PROGTOK+2   ; cfh
tElse           equ       PROGTOK+3   ; d0h
tWhile          equ       PROGTOK+4   ; d1h
tRepeat         equ       PROGTOK+5   ; d2h
tFor            equ       PROGTOK+6   ; d3h
tEnd            equ       PROGTOK+7   ; d4h
tReturn         equ       PROGTOK+8   ; d5h
tLbl            equ       PROGTOK+9   ; d6h   'lbl_'
tGoto           equ       PROGTOK+10  ; d7h   'goto_'
tPause          equ       PROGTOK+11  ; d8h   'pause_'
tStop           equ       PROGTOK+12  ; d9h   'stop'
tISG            equ       PROGTOK+13  ; dah   'is>'
tDSL            equ       PROGTOK+14  ; dbh   'ds<'
tInput          equ       PROGTOK+15  ; dch   'input_'
tPrompt         equ       PROGTOK+16  ; ddh   'prompt_'
tDisp           equ       PROGTOK+17  ; deh   'disp_'
tDispG          equ       PROGTOK+18  ; dfh   'dispg'
;
tOutput         equ       PROGTOK+19  ; e0h    'outpt'
tClLCD          equ       PROGTOK+20  ; e1h    'cllcd'
tFill           equ       PROGTOK+21  ; e2h    'fill'
tSortA          equ       PROGTOK+22  ; e3h    'sorta_'
tSortD          equ       PROGTOK+23  ; e4h    'sortd_'
tDispTab        equ       PROGTOK+24  ; e5h    'disp table
tMenu           equ       PROGTOK+25  ; e6h    'menu'
tSendMBL        equ       PROGTOK+26  ; e7h    'send'
tGetMBL         equ       PROGTOK+27  ; e8h    'get'
;
;===================================
;       stat plot commands
;===================================
statPCmd        equ       PROGTOK+28
;
tPlotOn         equ       statPCmd    ; e9h     ' plotson'
tPlotOff        equ       statPCmd+1   ; eah     ' plotsoff
CmdLen0         equ     tPlotOff - PROGTOK+ 1
CmdOff1         equ     CmdLen0 - 1
;
tListName       equ       0ebh        ; list designator
;
tPlot1          equ       0ech
tPlot2          equ       0edh
tPlot3          equ       0eeh

tUnit           equ       0efh        ; efh unit for mixed fractions (a u b/c)
tFracSlash      equ       0f0h         ; f0h '/' (bc symbol)

tStatCmd         equ       0f2h
;
;==================================
; 2nd half of graph format tokens
;
; ===== format settings commands
;===================================

GFMT            equ       1
;
tSeq            equ       GFMT        ; 01  'seqg'
tSimulG         equ       GFMT+1      ; 02  'simulg'
tCoordOn        equ       GFMT+2      ; 03  'coordon'
tCoordOff       equ       GFMT+3      ; 04  'coordoff'
tDrawLine       equ       GFMT+4      ; 05  'drawline'
tDrawDot        equ       GFMT+5      ; 06  'drawdot'
tAxisOn         equ       GFMT+6      ; 07  'axeson'
tAxisOff        equ       GFMT+7      ; 08  'axesoff'
tGridOn         equ       GFMT+8      ; 09  'gridon'
tGridOff        equ       GFMT+9      ; 0a  'gridoff'
tLblOn          equ       GFMT+10     ; 0b  'labelon'
tLblOff         equ       GFMT+11     ; 0c  'labeloff'
tPolarG         equ       GFMT+12     ; 0d  'polargc'
tRectG          equ       GFMT+13     ; 0e  'rectgc'
;
;=====================================
; 2nd half of mode setting tokens
;=====================================
MODES2          equ       GFMT+14
;
tStndrd         equ       MODES2      ; 0fh 'func'
tParam          equ       MODES2+1    ; 10h 'param'
tPolar          equ       MODES2+2    ; 11h 'pol'
tAFillOn        equ       MODES2+3    ; 12h 'auto fill on
tAFillOff       equ       MODES2+4    ; 13h
tACalcOn        equ       MODES2+5    ; 14h
tACalcOff       equ       MODES2+6    ; 15h 'autofill off
;
tMABC           equ       MODES2+7    ; 16h 'aub/c mode'
tMBC            equ       MODES2+8    ; 17h 'b/c mode'
tMAutoSimp      equ       MODES2+9    ; 18h 'b/c mode'
tMManSimp       equ       MODES2+10   ; 19h 'b/c mode'
tMSingleC       equ       MODES2+11   ; 1ah single constant
tMMultiC        equ       MODES2+12   ; 1bh multi constant


STATREGS        equ     1
tOneVar         equ     STATREGS      ; 01h    'onevar_'
tTwoVar         equ     STATREGS+1    ; 02h
tLR1            equ     STATREGS+2    ; 03h    'linr(ax+b
tManFit         equ     STATREGS+5   ;07h     'manual fit'
tMedMed         equ     STATREGS+6    ; 08h
tQuad           equ     STATREGS+7    ; 09h
tClrLst         equ     STATREGS+8    ; 0ah    'clear list
tClrTbl         equ     STATREGS+9    ; 0bh    clear table
tHist           equ     STATREGS+10   ; 0ch    'hist_'
tXYLine         equ     STATREGS+11   ; 0dh    'xyline_'
tScatter        equ     STATREGS+12   ; 0eh    'scatter_'
tLR             equ     STATREGS+13   ; 0fh    'linr(a+bx

;======================================
;       2nd half of user list tokens
;======================================
tL1             equ       01h         ; list 1
tL2             equ       02h         ; list 2
tL3             equ       03h         ; list 3
tL4             equ       04h         ; list 4
tL5             equ       05h         ; list 5
tL6             equ       06h         ; list 6

;===========================================
;       2nd half of user equation tokens
;===========================================
;
tY1             equ       10h         ; y1
tY2             equ       11h         ; y2
tY3             equ       12h         ; y3
tY4             equ       13h         ; y4
;
;       constant equations have bit 7 set
tC1             equ       80h         ; const1
tC2             equ       81h         ; const2
tC3             equ       82h         ; const3
tC4             equ       83h         ; const4
;
;===========================================
;       2nd half of user picture tokens
;===========================================
tPic1           equ       01h         ; pic1
tPic2           equ       02h         ; pic2
tPic3           equ       03h         ; pic3

;==============================================
;       2nd half of user graph database tokens
;==============================================
tGDB1           equ       01h         ; gdb1
tGDB2           equ       02h         ; gdb2
tGDB3           equ       03h         ; gdb3
tGDB4           equ       04h         ; gdb4
tGDB5           equ       05h         ; gdb5
tGDB6           equ       06h         ; gdb6
tGDB7           equ       07h         ; gdb7
tGDB8           equ       08h         ; gdb8
tGDB9           equ       09h         ; gdb9
tGDB0           equ       0ah         ; gdb0
;
;====================================
;       2nd half of string vars
;====================================
tStr1           equ       01h
tStr2           equ       02h
tStr3           equ       03h
tStr4           equ       04h
tStr5           equ       05h
tStr6           equ       06h
tStr7           equ       07h
tStr8           equ       08h
tStr9           equ       09h
tStr0           equ       0ah

;==================================================
;       2nd half of system output only variables
;==================================================
;
;   OPEN        EQU       00h
;
tRegEq          EQU       01h         ; REGRESSION EQUATION
;
tStatN          EQU       02h         ; STATISTICS N
;
tXMean          EQU       03h         ; X MEAN
tSumX           EQU       04h         ; SUM(X)
tSumXSqr        EQU       05h         ; SUM(X^2)
tStdX           EQU       06h         ; STANDARD DEV X
tStdPX          EQU       07h         ; STANDARD DEV POP X
tMinX           EQU       08h         ; Min X VALUE
tMaxX           EQU       09h         ; Max X VALUE
;
tMinY           EQU       0Ah         ; Min Y VALUE
tMaxY           EQU       0Bh         ; Max Y VALUE
tYmean          EQU       0Ch         ; Y MEAN
tSumY           EQU       0Dh         ; SUM(Y)
tSumYSqr        EQU       0Eh         ; SUM(Y^2)
tStdY           EQU       0Fh         ; STANDARD DEV Y
tStdPY          EQU       10h         ; STANDARD DEV POP Y
;
tSumXY          EQU       11h         ; SUM(XY)
tCorr           EQU       12h         ; CORRELATION
tMedX           EQU       13h         ; MED(X)
tQ1             EQU       14h         ; 1ST QUADRANT OF X
tQ3             EQU       15h         ; 3RD QUADRANT OF X
tQuadA          EQU       16h         ; 1ST TERM OF QUAD POLY REG/ Y-INT
tQuadB          EQU       17h         ; 2ND TERM OF QUAD POLY REG/ SLOPE
tQuadC          EQU       18h         ; 3RD TERM OF QUAD POLY REG
tCubeD          EQU       19h         ; 4TH TERM OF CUBIC POLY REG
tQuartE         EQU       1Ah         ; 5TH TERM OF QUART POLY REG
tMedX1          EQU       1Bh         ; x1 FOR MED-MED
tMedX2          EQU       1Ch         ; x2 FOR MED-MED
tMedX3          EQU       1Dh         ; x3 FOR MED-MED
tMedY1          EQU       1Eh         ; y1 FOR MED-MED
tMedY2          EQU       1Fh         ; y2 FOR MED-MED
tMedY3          EQU       20h         ; y3 FOR MED-MED
;
tLRSqr          equ       21h         ; r^2
tBRSqr          equ       22h         ; r^2
;
tError    equ     23h       ; token in case of error in entry (ans only)
tDone     equ     24h
;
;====================================
; system variable equates
;       vars with no restriction
;====================================
tuXscl       equ  1
tuYscl       equ  2
tXscl        equ  3
tYscl        equ  4

tXmin        EQU  0Ah
tXmax        EQU  0Bh
tYmin        EQU  0Ch
tYmax        EQU  0Dh
tTmin        EQU  0Eh
tTmax        EQU  0Fh
tThetaMin    EQU  10h
tThetaMax    EQU  11h
tuXmin       EQU  12h
tuXmax       EQU  13h
tuYmin       EQU  14h
tuYmax       EQU  15h
tuThetMin    EQU  16h
tuThetMax    EQU  17h
tuTmin       EQU  18h
tuTmax       EQU  19h
tTblMin      EQU  1Ah
tPlotStart   EQU  1Bh
tuPlotStart  EQU  1Ch
tnMax        EQU  1Dh
tunMax       EQU  1Eh
tnMin        EQU  1Fh
tunMin       EQU  20h

;
;       vars cannot be 0
;
tTblStep     equ  21h
tTStep       equ  22h
tThetaStep    equ  23h
tuTStep      equ  24h
tuThetStep     equ  25h
;
;       special limits on these
;
tDeltaX      equ  26h
tDeltaY      equ  27h
;
;       special limits on these
;
tXFact       equ  28h
tYFact       equ  29h
;
tTblInput       equ       2ah
;
;       no restrictions
tSimpFact       equ     2bh     ;simplification factor.
;
tPlotStep       equ       34h
tuPlotStep      equ       35h
;
tXres           equ       36h
tuXres          equ       37h
;

;======================================
;2nd byte of 2byte tokens
;======================================

tLCM            equ       01h         ; immediate unary
tGCD            equ       02h         ; immediate unary
tRandInt        equ       03h         ; immediate unary
tSubStrng       equ       04h         ; immediate unary
tStdDev         equ       05h         ; immediate unary
tVariance       equ       06h         ; immediate unary
tInStrng        equ       07h         ; immediate unary
tCoin           equ       08h         ; immediate unary
tDice           equ       09h         ; immediate unary
tRemainder      equ       0ah         ; immediate unary
lastImUnary      equ       0ah         ; last immediate unary token.
firstUnary      equ       0bh         ; first unary operator.
tCumSum         equ       0bh         ; unary
tExpr           equ       0ch         ; unary                        do autop
tLength         equ       0dh         ; unary                        do autop
tDeltaLst       equ       0eh         ; unary                        do autop
tRef            equ       0fh         ; unary                        no autop
autoP10         equ       10h
tRRef           equ       10h         ; unary                        no autop
newTok12         equ    12h

autoP11         equ       12h
tMatToLst       equ       12h         ; command pushed               do autop
tLstToMat       equ       13h         ; command pushed               do autop
tSetConst       equ       14h         ;                                 autop
tGraphStyle     equ       15h         ; command pushed                  autop
autoP12          equ       15h
tSetUpLst       equ       16h         ; command opt pushed           no autop
tExprOn         equ       1ah         ; mode command
tExprOff        equ       1bh         ; mode command
tClrSllLst      equ       1ch         ; command not pushed
tGetCalc        equ       1dh         ; command not pushed
tDelVar         equ       1eh         ; command not pushed
tEquToStrng     equ       1fh         ; command not pushed
tStrngToEqu     equ       20h         ; command not pushed
tDelLast        equ       21h         ; command not pushed
tSelect        equ       22h         ; command not pushed
tModBox         equ       23h         ; icon
tNormProb       equ       24h         ; icon
tPictOlot       equ       25h
tCirclePlot     equ       26h
tStemPlot       equ       27h
tBarPlot        equ       28h
tZFit           equ       29h         ; zoom fit
tDiag_On        equ       2ah         ; dianostic display on
tDiag_Off       equ       2bh         ; dianostic display off
#ENDIF

;==========================================
;	data type equates
;===========================================
RealObj         equ       0
ListObj         equ       1
MatObj          equ       2
EquObj          equ       3
StrngObj        equ       4
ProgObj         equ       5
ProtProgObj     equ       6
PictObj         equ       7
GDBObj          equ       8
UnknownObj      equ       9
UnknownEquObj   equ       0ah
NewEquObj       equ       0bh
CplxObj         equ       0ch
ClistObj        equ       0dh
SlistObj        equ       0dh   ;string list
UndefObj        equ       0eh
WindowObj       equ       0fh
ZStoObj         equ       10h
TblrngObj       equ       11h
LCDObj          equ       12h
BackupObj       equ       13h
FracObj         equ       14h
FracRSObj       equ       14h           ;reduced   simple fraction.
FracRMObj       equ       15h           ;reduced   mixed  fraction.
FracUSObj       equ       16h           ;unreduced simple fraction.
FracUMObj       equ       17h           ;unreduced mixed fraction.
NameObj         equ       18h           ;name string (will fit in fplen).
AppObj          equ       19h           ;application, only used in menus/link
AppVarObj       equ       1ah           ;application variable
TmpConstObj     equ       1bh           ;temporary type of constant.

;======================================================================
;           i/o equates                                                          
;======================================================================
;
D0D1_bits	EQU  03h
D0LD1L		EQU  03h
D0LD1H		EQU  01h
D0HD1L		EQU  02h
D0HD1H		EQU  00h
bport		EQU  0			; 4-BIT LINK PORT (I/O) 
;
;======================================================================
;           device codes                                                       
;======================================================================
;
;
TI82DEV		EQU  82h
PC82DEV		EQU  02h
MAC82DEV	EQU  12h
;
TI83FDEV	EQU  73h
LINK83FDEV	EQU  23h
;
TI83DEV		EQU  83h
PC83DEV		EQU  03h
MAC83DEV	EQU  13h
;
TI85DEV		EQU  95h      ; DIFF THEN REAL 85 SO ME TALK
PC85DEV		EQU  05h
MAC85DEV	EQU  15h
;
TI73DEV		EQU  74h      ; Device x3 is always an 83
PC73DEV		EQU  07h
MAC73DEV	EQU  17h
;
LINK73FDEV	EQU  23h
PC83FDEV	EQU  23h

;=========================================
;	system error codes
;=========================================

E_EDITF		EQU	7		;allow re-entering application
E_EDIT		EQU	1<<E_EDITF
E_Mask		EQU	7Fh

E_Overflow	EQU	1+E_EDIT
E_DivBy0	EQU	2+E_EDIT
E_SingularMat	EQU	3+E_EDIT
E_Domain	EQU	4+E_EDIT
E_Increment	EQU	5+E_EDIT
E_Break		EQU	6+E_EDIT
E_NonReal       EQU     7+E_EDIT
E_Syntax        EQU     8+E_EDIT
E_DataType      EQU     9+E_EDIT
E_Argument      EQU     10+E_EDIT
E_DimMismatch   EQU     11+E_EDIT
E_Dimension     EQU     12+E_EDIT
E_Undefined     EQU     13+E_EDIT
E_Memory        EQU     14+E_EDIT
E_Invalid       EQU     15+E_EDIT
E_IllegalNest   EQU     16+E_EDIT
E_Bound         EQU     17+E_EDIT
E_GraphRange    EQU     18+E_EDIT
E_Zoom          EQU     19+E_EDIT
E_Label         EQU     20
E_Stat          EQU     21
E_Solver        EQU     22+E_EDIT
E_Singularity   EQU     23+E_EDIT
E_SignChange    EQU     24+E_EDIT
E_Iterations    EQU     25+E_EDIT
E_BadGuess      EQU     26+E_EDIT
E_StatPlot      EQU     27
E_TolTooSmall   EQU     28+E_EDIT
E_Reserved      EQU     29+E_EDIT
E_Mode          EQU     30+E_EDIT
;
E_Scale         equ     31+E_EDIT
;
E_LnkErr        equ     32+E_EDIT
E_LnkMemErr     equ     33+E_EDIT
E_LnkTransErr   equ     34+E_EDIT
E_LnkDupErr     equ     35+E_EDIT
E_LnkMemFull    equ     36+E_EDIT
E_Unknown       equ     37+E_EDIT
E_IdNotFound    equ     38+E_EDIT
E_NoMode        equ     39+E_EDIT
E_Validation    equ     40+E_EDIT
E_Length        equ     41+E_EDIT
E_Application   equ     42+E_EDIT
E_AppErr1       equ     43+E_EDIT
E_AppErr2       equ     44+E_EDIT
E_ExpiredApp	equ	45+E_EDIT
E_BadAddr	equ	46+E_EDIT
HigErrNum	equ	46+E_EDIT

; obsolete error numbers34              ;first link error
E_LinkIOChkSum	equ	34
E_LinkIOTimeOut	equ	35
E_LinkIOBusy	equ	36
E_LinkIOVer	equ	37

;========= floating point

FPLEN           equ     9               ;length of a floating-point number

;========= constant table equates

drConst 	equ	0
pi2Const	equ	1
pi4Const	equ	2
logeConst	equ	3
piConst 	equ	4
DtoRConst	equ	5
ln10		equ	6

;========== text screen
textRows	equ	8
textCols        equ     16

;========== alphatext screen
alphabuffersize equ	16
;
;========== msc fraction equates
mantSignBit     equ  7       ;msbit of data type byte is set if negative.
redF            equ  1       ;reduced if 0, unreduced if 1.
unredF          equ  1       ;reduced if 0, unreduced if 1.
simpF           equ  0       ;simple if 0, mixed if 1.
mixedF          equ  0       ;simple if 0, mixed if 1.
;

;==============================================
;       equates to ram locations for stat vars
;==============================================
;
StatN   EQU     statVars
XMean   EQU     StatN      + FPLEN
SumX    EQU     XMean      + FPLEN
SumXSqr EQU     SumX       + FPLEN
StdX    EQU     SumXSqr    + FPLEN
StdPX   EQU     StdX       + FPLEN
MinX    EQU     StdPX      + FPLEN
MaxX    EQU     MinX       + FPLEN
MinY    EQU     MaxX       + FPLEN
MaxY    EQU     MinY       + FPLEN
YMean   EQU     MaxY       + FPLEN
SumY    EQU     YMean      + FPLEN
SumYSqr EQU     SumY       + FPLEN
StdY    EQU     SumYSqr    + FPLEN
StdPY   EQU     StdY       + FPLEN
SumXY   EQU     StdPY      + FPLEN
Corr    EQU     SumXY      + FPLEN
MedX    EQU     Corr       + FPLEN
Q1      EQU     MedX       + FPLEN
Q3      EQU     Q1         + FPLEN
QuadA   EQU     Q3         + FPLEN
QuadB   EQU     QuadA      + FPLEN
QuadC   EQU     QuadB      + FPLEN
CubeD   EQU     QuadC      + FPLEN
QuartE  EQU     CubeD      + FPLEN
MedX1   EQU     QuartE     + FPLEN
MedX2   EQU     MedX1      + FPLEN
MedX3   EQU     MedX2      + FPLEN
MedY1   EQU     MedX3      + FPLEN
MedY2   EQU     MedY1      + FPLEN
MedY3   EQU     MedY2      + FPLEN
SLower  EQU     MedY3      + 2*FPLEN
SUpper  EQU     MedY3      + FPLEN

L3rd        equ              OP6+4
R3rd        equ              OP6+6
RidX        equ              OP6
LidX        equ              OP6+2

;Extra and misnamed romcalls defined in ION.INC:
_memfree = _memchk
_cleargbuf = _GrBufClr
_indicatorOn = _RunIndicOn
_indicatorOff = _RunIndicOff
_copygbuf = _GrBufCpy
_clrlcdf = _clrlcdfull
_cleartextshad = _ClrTxtShd
_clrscr = _ClrLCD
_clrscrf = _ClrScrnFull
_chksysvar = _ChkFindSym
_deletemem = _delmem
_lcdbusy = _LCD_BUSY

;Saferams
saferam1	= savesscreen
saferam2	= appbackupscreen
saferam3	= textshadow
saferam4	= op1
saferamp	= appbackupscreen
saferamp2	= boottemp

;Alternate equate names
gbuf = plotsscreen
grbuf = plotsscreen
sram = saferam1
apdram = saferam1
statram = saferam2

;CrASH names
GRAPH_MEM = plotsscreen
APD_BUF = savesscreen
TEXT_MEM = textshadow
CURSOR_ROW = currow
CURSOR_COL = curcol
GRAF_CURS = pencol
CURSOR_X = pencol
CURSOR_Y = penrow
VAT_START = symtable
FREE_A1 = plotsscreen
FREE_A2 = saferam3
FREE_A3 = OP1
FREE_B = saferam2
FREE_C = saferam1  
START_ADDR = progstart

;CrASH Romcalls
CR_KHAND = _getkey
LD_HL_MHL = _ldhlind
CP_HL_DE = _cphlde
UNPACK_HL = _divhlby10
DIV_HL_A = _divhlbya
GET_KEY = _getcsc
DISP_DELAY = _lcdbusy

;CrASH ROM_CALLs
TR_CHARPUT = _putc
TX_CHARPUT = _putmap
D_ZT_STR = _puts
D_HL_DECI = _disphl
NEXT_LINE = _newline

M_CHARPUT = _vputmap
D_ZM_STR = _vputs

CLEARLCD = _clrlcdfull
DISP_GRAPH = _grbufcpy

#endif



#ifdef ION
#ifdef TI83
;-----> Ion Functions
lVectors	=cmdshad+80
ionVersion	=lVectors
ionRandom	=lVectors+3
ionPutSprite	=lVectors+6
ionLargeSprite	=lVectors+9
ionGetPixel	=lVectors+12
ionFastCopy	=lVectors+15
ionDetect	=lVectors+18
ionDecompress	=lVectors+21

iversion              = ionversion
irandom               = ionrandom
isprite               = ionputsprite
ilsprite              = ionlargesprite
igetpix               = iongetpixel
ifastcopy             = ionfastcopy
idetect               = iondetect
idecomp               = iondecompress
#endif
#ifdef TI83P
#IFDEF ION
#ifndef NOIONLIB
;-----> Ion Functions
lVectors	=cmdshad+80
ionVersion	=lVectors
ionRandom	=lVectors+3
ionPutSprite	=lVectors+6
ionLargeSprite	=lVectors+9
ionGetPixel	=lVectors+12
ionFastCopy	=lVectors+15
ionDetect	=lVectors+18
ionDecompress	=lVectors+21

iversion              = ionversion
irandom               = ionrandom
isprite               = ionputsprite
ilsprite              = ionlargesprite
igetpix               = iongetpixel
ifastcopy             = ionfastcopy
idetect               = iondetect
idecomp               = iondecompress
#endif
#endif
#endif
#endif

#ifdef MIRAGEOS
;=================================;
;======== Ion Subroutines ========;
;=================================;
#ifndef NOIONLIB
iversion              = $4083
irandom               = $4086
isprite               = $4089
ilsprite              = $408C
igetpix               = $408F
ifastcopy             = $4092
idetect               = $4095
idecomp               = $4098

ionversion              = $4083
ionrandom               = $4086
ionputsprite            = $4089
ionlargesprite        = $408C
iongetpixel           = $408F
ionfastcopy           = $4092
iondetect             = $4095
iondecompress         = $4098
#ENDIF
;=================================;
;====== MirageOS Subroutines =====;
;=================================;

directin              = $409B
sendbytetios          = $409E
getbytetios           = $40A1
version               = $40A4
setvputs              = $40A7
setpixel              = $40AA
fastcopys             = $40AD
delayb                = $40B0
multhe                = $40B3
multhl                = $40B6
quittoshell           = $40B9
fastline              = $40BC
pixelonhl             = $40BF
pixeloff              = $40C2
pixelxor              = $40C5
pixeltest             = $40C8
pixeloffhl            = $40CB
pixelxorhl            = $40DE
pixeltesthl           = $40D1
fastlineb             = $40D4
fastlinew             = $40D7
fastlinex             = $40DA
pointonc              = $40DD
pointoffc             = $40E0
pointxorc             = $40E3
centertext            = $40E6
cphlbc                = $40E9
putsprite8            = $40EC
fastcopyb             = $40EF
vputsc                = $40F2
scrolld7              = $40F5
vnewline              = $40F8
rand127               = $40FB
disprle               = $40FE
cphlde                = $4101
screentopic           = $4104
fastlined             = $4107
disprlel              = $410A
getnextgoodprog       = $410D
getprevgoodprog       = $4110
getnext               = $4113
getprev               = $4116
compstrs              = $4119
nextstr               = $411C
getinfo               = $411F
fastrectangle         = $4122
gettext               = $4125
gettextv              = $4128
FastRectangle_Save    = $412B
vputa                 = $412E
runprog               = $4131
isgoodprog            = $4134
existfold             = $4137
delprog               = $413A
filledrectangle       = $413D
nextfolder            = $4140
delfolder             = $4143
moveall               = $4149
curfoldname           = $4149
curfoldnamea          = $414C
createfolder          = $414F
compstrsn             = $4152
folder_menu_start     = $4155
options_screen        = $4158
put_folder_name_top   = $415B
general_key_routine   = $415E
find_num_good_progs   = $4161
put_scrollbar         = $4164
invert_lines          = $4167
invert_1_line         = $416A
right_align_value     = $416D
put_mirageos_header   = $4170
put_size_graphic      = $4173
sendprog              = $4176
hideprog              = $4179
arcprog               = $417C
filledrectangle_save  = $417F
getbytetiosw          = $4182
vatswap               = $4185
renameprog            = $4188
renamefolder          = $418B
sysmain               = $418E
setupint              = $4191
move_gui_prog         = $4194
largespritehl         = $4197
Update_Scrollbar      = $419A
Initial_Scrollbar     = $419D
sortallfolds          = $41A0
dofoldsort            = $41A3
getfoldsort           = $41A6
setfoldsort           = $41A9
Increase_Cur_Element  = $41AC
Decrease_Cur_Element  = $41AF
Increase_Max_Elements = $41B2
Decrease_Max_Elements = $41B5
Add_A_To_Cur_Element  = $41B8
Sub_A_From_Cur_Element = $41BB
Add_A_To_Max_Elements = $41BE
Sub_A_From_Max_Elements = $41C1
Skip_Forward_B_From_Top = $41C4
Get_Curgoodprog_Ptr   = $41C7
getchecksum           = $41CA
freearc               = $41CD
swapram               = $41D0
hideall               = $41D3

;=================================;
;====== MirageOS Ram Equates =====;
;=================================;

mlinebitmap           = $FFFF-399
timer1                = $8a3a
timer1max             = $8a3b
timer2                = $8a3c
timer2max             = $8a3d
timer3                = $8a3e
custintaddr           = $966F

mirflags              = $21
rightalign            = $07
lcdinstport           = $10
lcddataport           = $11

numscrollpixels       = CmdShadow+104
scrollbarcoords       = CmdShadow+105
old_element           = CmdShadow+107
cur_element           = CmdShadow+109
old_max_elem          = CmdShadow+111
max_elements          = CmdShadow+113

#ENDIF

#ifdef SOS
; Precalculated values for use with SOS libs and modules

;--> These are the module variables
mvars		=$9157
mKeypress	=mvars		; _getkey keypress value
mpName		=mvars+2	; Program name (10 bytes)
mpPointer	=mvars+12	; Program pointer (to start of data)
mpSize		=mvars+14	; Program Size
mpVat		=mvars+16	; End of VAT entry (1st byte of next)

;--> These are to be called
vector0		=$9157+40
vector1		=vector0+3
vector2		=vector0+6
vector3		=vector0+9
vector4		=vector0+12
vector5		=vector0+15
vector6		=vector0+18
vector7		=vector0+21
vector8		=vector0+24
vector9		=vector0+27
vectorA		=vector0+30
vectorB		=vector0+33
vectorC		=vector0+36
vectorD		=vector0+39
vectorE		=vector0+42
vectorF		=vector0+45

;--> These are library routine pointer offsets
lib0		=0
lib1		=2
lib2		=4
lib3		=6
lib4		=8
lib5		=10
lib6		=12
lib7		=14
lib8		=16
lib9		=18
libA		=20
libB		=22
libC		=24
libD		=26
libE		=28
libF		=30

;-->These are vector offsets
vec0		=0
vec1		=3
vec2		=6
vec3		=9
vec4		=12
vec5		=15
vec6		=18
vec7		=21
vec8		=24
vec9		=27
vecA		=30
vecB		=33
vecC		=36
vecD		=39
vecE		=42
vecF		=45

;-->Two bytes of data used in some library routines
data		=$9157+38
#endif


;======================================================================
;           Scan Code Equates
;======================================================================;
skDown          EQU	01h
skLeft          EQU	02h
skRight         EQU	03h
skUp            EQU	04h
skEnter         EQU	09h
skAdd           EQU	0Ah
skSub           EQU	0Bh
skMul           EQU	0Ch
skDiv           EQU	0Dh
skPower         EQU	0Eh
skClear         EQU	0Fh
skChs           EQU	11h
sk3             EQU	12h
sk6             EQU	13h
sk9             EQU	14h
skRParen        EQU	15h
skTan           EQU	16h
skVars          EQU	17h
skDecPnt        EQU	19h
sk2             EQU	1Ah
sk5             EQU	1Bh
sk8             EQU	1Ch
skLParen        EQU	1Dh
skCos           EQU	1Eh
skPrgm          EQU	1Fh
skStat          EQU	20h
sk0             EQU	21h
sk1             EQU	22h
sk4             EQU	23h
sk7             EQU	24h
skComma         EQU	25h
skSin           EQU	26h
skMatrix        EQU	27h
skGraphvar      EQU	28h
skStore         EQU	2Ah
skLn            EQU	2Bh
skLog           EQU	2Ch
skSquare        EQU	2Dh
skRecip         EQU	2Eh
skMath          EQU	2Fh
skAlpha         EQU	30h
skGraph         EQU	31h
skTrace         EQU	32h
skZoom          EQU	33h
skWindow        EQU	34h
skYEqu          EQU	35h
sk2nd           EQU	36h
skMode          EQU	37h
skDel           EQU	38h

gDown          EQU	01h
gLeft          EQU	02h
gRight         EQU	03h
gUp            EQU	04h
gEnter         EQU	09h
gAdd           EQU	0Ah
gSub           EQU	0Bh
gMul           EQU	0Ch
gDiv           EQU	0Dh
gPower         EQU	0Eh
gClear         EQU	0Fh
gChs           EQU	11h
g3             EQU	12h
g6             EQU	13h
g9             EQU	14h
gRParen        EQU	15h
gTan           EQU	16h
gVars          EQU	17h
gDecPnt        EQU	19h
g2             EQU	1Ah
g5             EQU	1Bh
g8             EQU	1Ch
gLParen        EQU	1Dh
gCos           EQU	1Eh
gPrgm          EQU	1Fh
gStat          EQU	20h
g0             EQU	21h
g1             EQU	22h
g4             EQU	23h
g7             EQU	24h
gComma         EQU	25h
gSin           EQU	26h
gMatrix        EQU	27h
gGraphvar      EQU	28h
gStore         EQU	2Ah
gLn            EQU	2Bh
gLog           EQU	2Ch
gSquare        EQU	2Dh
gRecip         EQU	2Eh
gMath          EQU	2Fh
gAlpha         EQU	30h
gGraph         EQU	31h
gTrace         EQU	32h
gZoom          EQU	33h
gWindow        EQU	34h
gYEqu          EQU	35h
g2nd           EQU	36h
gMode          EQU	37h
gDel           EQU	38h

gYequals	.equ 35h
gYequhack				.equ 35h

GF1		.equ 35h
GF2		.equ 34h
GF3		.equ 33h
GF4		.equ 32h
GF5		.equ 31h

GXT0		.equ 28h
GXTN		.equ 28h
GApps		.equ 27h
GMatrx		.equ 27h
GLeftPara	.equ 1Dh
GRightPara	.equ 15h

GDivide		.equ 0Dh
GMultiply	.equ 0Ch
GSubtract	.equ 0Bh
GSto		.equ 2Ah

GPeriod 	.equ 8Dh
GNegative	.equ 8Ch

;#######################
;## Direct Input Keys ##
;#######################

;Group 1
KeyRow_Pad	.equ 0feh
KDown   .equ 254
KLeft   .equ 253
KRight  .equ 251
KUp     .equ 247
KIDown  .equ 1
KILeft  .equ 2
KIRight .equ 4
KIUp    .equ 8



;Group 2
KeyRow_5 	.equ 0fdh
KEnter		.equ 254
KIEnter		.equ 1
KPlus		.equ 253
KMinus		.equ 251
KMul		.equ 247
KDiv		.equ 239
KPower		.equ 223
KClear		.equ 191

;Group 3
KeyRow_4 	.equ 0fbh
KMinus2		.equ 254
KThree		.equ 253
KSix		.equ 251
KNine		.equ 247
KRbracket	.equ 239
KTan		.equ 223
KVars		.equ 191

;Group 4
KeyRow_3	.equ 0f7h
KPoint		.equ 254
KTwo		.equ 253
KFive		.equ 251
KEight		.equ 247
KLbracket	.equ 239
KCos		.equ 223
KPrgm		.equ 191
KStat		.equ 127

;Group 5
KeyRow_2 	.equ 0efh
KZero		.equ 254
KOne		.equ 253
KFour		.equ 251
KSeven		.equ 247
KComma		.equ 239
KSin		.equ 223
KMatrx		.equ 191
KApps		.equ 191
KX		.equ 127

;Group 6
KeyRow_1 	.equ 0dfh
KOn         = 254
KSto		.equ 253
KLn		.equ 251
KLog		.equ 247
kX2		.equ 239
kX1		.equ 223
kMath		.equ 191
kAlpha		.equ 127
kIOn    = 1
kIMath  = 64
kIAlpha = 128


;Group 7
KeyRow_Top 	.equ 0bfh
KGraph		.equ 254
KTrace		.equ 253
KZoom		.equ 251
KWindow		.equ 247
KY		.equ 239
k2nd		.equ 223
kMode		.equ 191
kDel		.equ 127

kI2nd = 32
kIMode = 64
kIDel = 128




.org progstart


#ifdef TI83P
#IFNDEF APP
#IFNDEF CRUNCHY
 .org progstart-2
 .db     $BB,$6D  ;squished asmprgm token
#endif
#ENDIF
#ENDIF

#ifdef ZES
	.org 931Eh
	.db $E7,$39,$5F,$5A,$45,$53,$3F,$D5,$3F
	ccf
	jr lblStart
	.dw libs-progstart
	.dw tTitle
#endif

#ifdef MIRAGEOS
	ret
	.db 1
#endif

#ifdef ION
#ifndef CRUNCHY
  ret
  jr nc,lblStart
#endif
#endif

#ifndef ZES
#ifdef SOS
  ccf
#ifndef LIB
	jr lblStart
	.dw 0
ionfastcopy = _grbufcpy
#else
  jr z,lblStart
	.dw	libs-progstart
#endif
	.dw tTitle
#endif
#endif

#ifdef VENUS
.org $9329
	.db	$e7,"9_[V?",0
	jr nc,lblStart
vFastCopy	=$FE75
ionfastcopy = vFastCopy

#Endif

#IFDEF CRUNCHY
	.org progstart+2
#ENDIF



#IFDEF APP
.org $4000

#IFNDEF NOAPPHEADER
;This is the application header definition area required for all apps.
 .db 080h,0Fh    ;Field: Program length
 .db   00h,00h,00h,00h ;Length=0 (N/A for unsigned apps)
 .db 080h,012h    ;Field: Program type
 #ifdef TI73
 	.db 01h, 02h  ;type=Shareware, TI-73
 #else
  .db   01h,04h  ;Type= Shareware, TI-83Plus
 #endif
 .db 080h,021h    ;Field: App ID
 .db   01h       ;Id = 1
 .db 080h,031h    ;Field: App Build
 .db   01h       ;Build = 1
 .db 080h,048h    ;Field: App Name
 .db APPNAME
 .db 080h,081h    ;Field: App Pages
 .db APPPAGES
 .db 080h,090h    ;No default splash screen
 .db 03h,026h ,09h,04h, 04h,06fh,01bh,80h     ;Field: Date stamp- 5/12/1999
 .db 02h,0dh,040h                             ;Dummy encrypted TI date stamp signature
 .db 0a1h ,06bh ,099h ,0f6h ,059h ,0bch ,067h 
 .db 0f5h ,085h ,09ch ,09h ,06ch ,0fh ,0b4h ,03h ,09bh ,0c9h 
 .db 03h ,032h ,02ch ,0e0h ,03h ,020h ,0e3h ,02ch ,0f4h ,02dh 
 .db 073h ,0b4h ,027h ,0c4h ,0a0h ,072h ,054h ,0b9h ,0eah ,07ch 
 .db 03bh ,0aah ,016h ,0f6h ,077h ,083h ,07ah ,0eeh ,01ah ,0d4h 
 .db 042h ,04ch ,06bh ,08bh ,013h ,01fh ,0bbh ,093h ,08bh ,0fch 
 .db 019h ,01ch ,03ch ,0ech ,04dh ,0e5h ,075h 
 .db 80h,7Fh      ;Field: Program Image length
 .db   0,0,0,0    ;Length=0, N/A
 .db   0,0,0,0    ;Reserved
 .db   0,0,0,0    ;Reserved
 .db   0,0,0,0    ;Reserved
 .db   0,0,0,0    ;Reserved
#ENDIF
#ENDIF


;END
;.END