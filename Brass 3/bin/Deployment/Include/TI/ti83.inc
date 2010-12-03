;-----------------------------------------------------------------------------
;TI-83 Include File
;-----------------------------------------------------------------------------
; 06/27/05
;-----------------------------------------------------------------------------
;Brandon Wilson
;-----------------------------------------------------------------------------
__bank_call             =     0000h
__bank_entry            =     0000h
__bank_ret              =     0000h
__bank_jump             =     0000h

;ROM Calls
;------------------------------------
_ldHLind                =     4000h
_cphlde                 =     4004h
_divHLby10              =     4008h
_divHLbyA               =     400Ch
_kbdScan                =     4010h
_getcsc                 =     4014h
_coorMon                =     4018h
_mon                    =     401Bh
_monForceKey            =     401Eh
_sendKPress             =     4021h
_JforceCmdNoChar        =     4024h
_JforceCmd              =     4027h
_sysErrHandler          =     402Ah
_newContext             =     402Eh
_newContext0            =     4032h
_A2POINTHLIND           =     4036h
_POINTHLIND             =     403Ah
_PPutAwayPrompt         =     403Eh
_PPutAway               =     4042h
_PutAway                =     4046h
_SizeWind               =     404Ah
_ErrorEP                =     404Eh
_callMain               =     4052h
_monErrHand             =     4056h
_appInit                =     405Ah
_appInitIfDec           =     405Eh
_initialize             =     4062h
_lcd_busy               =     4066h
_MIN                    =     406Ah
_MAX                    =     406Eh
_ABSO1PABSO2            =     4072h
_INTGR                  =     4076h
_TRUNC                  =     407Ah
_Invsub                 =     407Eh
_Times2                 =     4082h
_Plus1                  =     4086h
_Minus1                 =     408Ah
_FPsub                  =     408Eh
_FPadd                  =     4092h
_Dtor                   =     4096h
_RTOD                   =     409Ah
_Cube                   =     409Eh
_Timespt5               =     40A2h
_FPsquare               =     40A6h
_FPmult                 =     40AAh
_LJRND                  =     40AEh
_Invop1sc               =     40B2h
_Invop1s                =     40B6h
_Invop2s                =     40BAh
_FRAC                   =     40BEh
_FPrecip                =     40C2h
_FPDIV                  =     40C6h
_SQROOT                 =     40CAh
_RNDGUARD               =     40CEh
_RNFX                   =     40D2h
_Int                    =     40D6h
_ROUND                  =     40DAh
_LNX                    =     40DEh
_Logx                   =     40E2h
_LJNORND                =     40E6h
_ETOX                   =     40EAh
_TENX                   =     40EEh
_SINCOSRAD              =     40F2h
_SIN                    =     40F6h
_Cos                    =     40FAh
_Tan                    =     40FEh
_SINHCOSH               =     4102h
_Tanh                   =     4106h
_Cosh                   =     410Ah
_Sinh                   =     410Eh
_ACOSRAD                =     4112h
_ATANRAD                =     4116h
_ATAN2RAD               =     411Ah
_ASINRAD                =     411Eh
_ACOS                   =     4122h
_ATAN                   =     4126h
_ASIN                   =     412Ah
_ATAN2                  =     412Eh
_Atanh                  =     4132h
_ASINH                  =     4136h
_Acosh                  =     413Ah
_Ptor                   =     413Eh
_Rtop                   =     4142h
_HLTIMES9               =     4146h
_CKOP1CPLX              =     414Ah
_CKOP1REAL              =     414Eh
_Angle                  =     4152h
_COP1SET0               =     4156h
_CPOP4OP3               =     415Ah
_MOV9OP2CP              =     415Eh
_ABSO1O2CP              =     4162h
_Cpop1op2               =     4166h
_Op3tooP4               =     416Ah
_Op1tooP4               =     416Eh
_Op2tooP4               =     4172h
_OP4TOOP2               =     4176h
_OP3TOOP2               =     417Ah
_OP1TOOP3               =     417Eh
_Op5tooP2               =     4182h
_OP5TOOP6               =     4186h
_OP5TOOP4               =     418Ah
_Op1tooP2               =     418Eh
_OP6TOOP2               =     4192h
_OP6TOOP1               =     4196h
_Op4tooP1               =     419Ah
_OP5TOOP1               =     419Eh
_Op3tooP1               =     41A2h
_OP6TOOP5               =     41A6h
_Op4tooP5               =     41AAh
_Op3tooP5               =     41AEh
_OP2TOOP5               =     41B2h
_Op2tooP6               =     41B6h
_OP1TOOP6               =     41BAh
_OP1TOOP5               =     41BEh
_OP2TOOP1               =     41C2h
_MOVREG                 =     41C6h
_MOV10B                 =     41CAh
_MOV9B                  =     41CEh
_MOV18                  =     41D2h
_MOV8B                  =     41D6h
_MOV7B                  =     41DAh
_MOV14                  =     41DEh
_MOV6B                  =     41E2h
_MOV5B                  =     41E6h
_MOV4B                  =     41EAh
_MOV3B                  =     41EEh
_MOV2B                  =     41F2h
_Op2tooP3               =     41F6h
_OP4TOOP3               =     41FAh
_OP5TOOP3               =     41FEh
_OP4TOOP6               =     4202h
_MOV9TOOP1              =     4206h
_MOV9OP1OP2             =     420Ah
_MOV9TOOP2              =     420Eh
_MOVFROP1               =     4212h
_Op4set1                =     4216h
_OP3SET1                =     421Ah
_OP2SET8                =     421Eh
_Op2set5                =     4222h
_OP2SETA                =     4226h
_Op2set4                =     422Ah
_Op2set3                =     422Eh
_OP1SET1                =     4232h
_Op1set4                =     4236h
_Op1set3                =     423Ah
_Op3set2                =     423Eh
_Op1set2                =     4242h
_Op2set2                =     4246h
_SETNUM2                =     424Ah
_SETMANT1               =     424Eh
_Op2set1                =     4252h
_SETNUM1                =     4256h
_SETNUM                 =     425Ah
_SETNUMA                =     425Eh
_SETMANT                =     4262h
_ZERO16D                =     4266h
_SET16A                 =     426Ah
_SET14A                 =     426Eh
_SET14D                 =     4272h
_OP5SET0                =     4276h
_OP4SET0                =     427Ah
_OP3SET0                =     427Eh
_Op2set0                =     4282h
_Op1set0                =     4286h
_SETNUM0                =     428Ah
_zeroop1                =     428Eh
_ZEROOP2                =     4292h
_ZEROOP3                =     4296h
_ZEROOP                 =     429Ah
_CLRLP                  =     429Eh
_SHRACC                 =     42A2h
_SHLACC                 =     42A6h
_shlacc                 =     42A6h
_SHR18                  =     42AAh
_SHR18A                 =     42AEh
_SHR16                  =     42B2h
_SHR14                  =     42B6h
_SHL16                  =     42BAh
_SHL14                  =     42BEh
_SRDO1                  =     42C2h
_SHRDRND                =     42C6h
_MANTPA                 =     42CAh
_ADDPROP                =     42CEh
_ADDPROPLP              =     42D2h
_ADD16D                 =     42D6h
_ADD14D                 =     42DAh
_SUB16D                 =     42DEh
_SUB14D                 =     42E2h
_Op2exOp6               =     42E6h
_Op5exOp6               =     42EAh
_Op1exop5               =     42EEh
_Op1exop6               =     42F2h
_Op2exOp4               =     42F6h
_OP2EXOP5               =     42FAh
_OP1EXOP3               =     42FEh
_Op1exop4               =     4302h
_OP1EXOP2               =     4306h
_EXLP                   =     430Ah
_CKOP1C0                =     430Eh
_CKOP1FP0               =     4312h
_CKOP2FP0               =     4316h
_POSNO0INT              =     431Ah
_CKPOSINT               =     431Eh
_Ckint                  =     4322h
_CKODD                  =     4326h
_CKOP1M                 =     432Ah
_GETCON1                =     432Eh
_GETCON                 =     4332h
_PIDIV2                 =     4336h
_PIDIV4                 =     433Ah
_TWOPI                  =     433Eh
_PICON                  =     4342h
_PIDIV4A                =     4346h
_PIDIV2A                =     434Ah
_EXPTOHEX               =     434Eh
_OP1EXPTODEC            =     4352h
_CKOP2POS               =     4356h
_Ckop1pos               =     435Ah
_CLROP2S                =     435Eh
_CLROP1S                =     4362h
_FDIV100                =     4366h
_FDIV10                 =     436Ah
_DECO1EXP               =     436Eh
_INCO1EXP               =     4372h
_INCEXP                 =     4376h
_CKVALIDNUM             =     437Ah
_GETEXP                 =     437Eh
_HtimesL                =     4382h
_EOP1NOTREAL            =     4386h
_THETANAME              =     438Ah
_RNAME                  =     438Eh
_REGEQNAME              =     4392h
_RECURNNAME             =     4396h
_XNAME                  =     439Ah
_YNAME                  =     439Eh
_TNAME                  =     43A2h
_REALNAME               =     43A6h
_SETesTOfps             =     43AAh
_CHKTEMPDIRT            =     43AEh
_OP1MOP2EXP             =     43B2h
_OP1EXPMDE              =     43B6h
_CHKERRBREAK            =     43BAh
_ISA2BYTETOK            =     43BEh
_GETLASTENTRY           =     43C2h
_GETLASTENTRYPTR        =     43C6h
_REGRCLRCHNG            =     43CAh
_RESETWINTOP            =     43CEh
_SETYUP                 =     43D2h
_SETXUP                 =     43D6h
_ISO1NONTLSTorPROG      =     43DAh
_ISO1NONTEMPLST         =     43DEh
_IS_A_LSTorCLST         =     43E2h
_CHK_HL_999             =     43E6h
_EQU_or_NEWEQU          =     43EAh
_GET_PLUS1_SAVE         =     43EEh
_ERRD_OP1NOTPOS         =     43F2h
_ERRD_OP1NOT_R          =     43F6h
_ERRD_OP1NOTPOSINT      =     43FAh
_ERRD_OP1_LE_0          =     43FEh
_ERRD_OP1_0             =     4402h
_FINDSYM_GET_SIZE       =     4406h
_STO_STATVAR            =     440Ah
_RCL_STATVAR            =     440Eh
_Ckop2real              =     4412h
_GET_X_INDIRECT         =     4416h
_MEMCHK                 =     441Ah
_CMPPRGNAMLEN1          =     441Eh
_CMPPRGNAMLEN           =     4422h
_FINDPROGSYM            =     4426h
_Chkfindsym             =     442Ah
_FINDSYM                =     442Eh
_INSERTMEM              =     4432h
_INSERTMEMA             =     4436h
_ENOUGHMEM              =     443Ah
_CMPMEMNEED             =     443Eh
_CREATEPVAR4            =     4442h
_CREATEPVAR3            =     4446h
_CREATEVAR3             =     444Ah
_CREATECPLX             =     444Eh
_CREATEREAL             =     4452h
_CREATETRLIST           =     4456h
_CREATERLIST            =     445Ah
_CREATETCLIST           =     445Eh
_CREATECLIST            =     4462h
_CREATETRMAT            =     4466h
_CREATERMAT             =     446Ah
_CREATETSTRNG           =     446Eh
_CREATESTRNG            =     4472h
_CREATE0EQU             =     4476h
_CREATETEQU             =     447Ah
_CREATEEQU              =     447Eh
_CREATEPICT             =     4482h
_CREATEGDB              =     4486h
_CREATEPROG             =     448Ah
_CHKDEL                 =     448Eh
_CHKDELA                =     4492h
_ADJPARSER              =     4496h
_ADJMATH                =     449Ah
_ADJM7                  =     449Eh
_DELMEMA                =     44A2h
_GET_FORM_NUM           =     44A6h
_DELVAR                 =     44AAh
_DELVARIO               =     44AEh
_DELMEM                 =     44B2h
_DELVAR3D               =     44B6h
_DELVAR3C               =     44BAh
_DELVAR3DC              =     44BEh
_SYM_PROG_NON_T_LST     =     44C2h
_ADJSYMPTRS             =     44C6h
_DATASIZEA              =     44CAh
_DATASIZE               =     44CEh
_POPMCPLXO1             =     44D2h
_POPMCPLX               =     44D6h
_MOVCPLX                =     44DAh
_POPOP5                 =     44DEh
_POPOP3                 =     44E2h
_POPOP1                 =     44E6h
_POPREALO6              =     44EAh
_POPREALO5              =     44EEh
_POPREALO4              =     44F2h
_POPREALO3              =     44F6h
_POPREALO2              =     44FAh
_POPREALO1              =     44FEh
_POPREAL                =     4502h
_FPOPCPLX               =     4506h
_FPOPREAL               =     450Ah
_FPOPFPS                =     450Eh
_DEALLOCFPS             =     4512h
_DEALLOCFPS1            =     4516h
_ALLOCFPS               =     451Ah
_ALLOCFPS1              =     451Eh
_PUSHREALO6             =     4522h
_PUSHREALO5             =     4526h
_PUSHREALO4             =     452Ah
_PUSHREALO3             =     452Eh
_PUSHREALO2             =     4532h
_PUSHREALO1             =     4536h
_PUSHREAL               =     453Ah
_PUSHOP5                =     453Eh
_PUSHOP3                =     4542h
_PUSHMCPLXO3            =     4546h
_PUSHOP1                =     454Ah
_PUSHMCPLXO1            =     454Eh
_PUSHMCPLX              =     4552h
_EXMCPLXO1              =     4556h
_EXCH9                  =     455Ah
_CPYTO1FPS11            =     455Eh
_CPYTO2FPS5             =     4562h
_CPYTO1FPS5             =     4566h
_CPYTO2FPS6             =     456Ah
_CPYTO1FPS6             =     456Eh
_CPYTO2FPS7             =     4572h
_CPYTO1FPS7             =     4576h
_CPYTO1FPS8             =     457Ah
_CPYTO2FPS8             =     457Eh
_CPYTO1FPS10            =     4582h
_CPYTO1FPS9             =     4586h
_CPYTO2FPS4             =     458Ah
_CPYTO6FPS3             =     458Eh
_CPYTO6FPS2             =     4592h
_CPYTO2FPS3             =     4596h
_CPYCTO1FPS3            =     459Ah
_CPYTO1FPS3             =     459Eh
_CPYFPS3                =     45A2h
_CPYTO1FPS4             =     45A6h
_CPYTO3FPS2             =     45AAh
_CPYTO5FPST             =     45AEh
_CPYTO6FPST             =     45B2h
_CPYTO4FPST             =     45B6h
_CPYTO3FPST             =     45BAh
_CPYTO2FPST             =     45BEh
_CPYTO1FPST             =     45C2h
_CPYFPST                =     45C6h
_CPYSTACK               =     45CAh
_CPYTO3FPS1             =     45CEh
_CPYTO2FPS1             =     45D2h
_CPYTO1FPS1             =     45D6h
_CPYFPS1                =     45DAh
_CPYTO2FPS2             =     45DEh
_CPYTO1FPS2             =     45E2h
_CPYFPS2                =     45E6h
_CPYO3TOFPST            =     45EAh
_CPYO2TOFPST            =     45EEh
_CPYO6TOFPST            =     45F2h
_CPYO1TOFPST            =     45F6h
_CPYTOFPST              =     45FAh
_CPYTOSTACK             =     45FEh
_CPYO3TOFPS1            =     4602h
_CPYO5TOFPS1            =     4606h
_CPYO2TOFPS1            =     460Ah
_CPYO1TOFPS1            =     460Eh
_CPYTOFPS1              =     4612h
_CPYO2TOFPS2            =     4616h
_CPYO3TOFPS2            =     461Ah
_CPYO6TOFPS2            =     461Eh
_CPYO1TOFPS2            =     4622h
_CPYTOFPS2              =     4626h
_CPYO5TOFPS3            =     462Ah
_CPYO2TOFPS3            =     462Eh
_CPYO1TOFPS3            =     4632h
_CPYTOFPS3              =     4636h
_CPYO1TOFPS6            =     463Ah
_CPYO1TOFPS7            =     463Eh
_CPYO1TOFPS5            =     4642h
_CPYO2TOFPS4            =     4646h
_CPYO1TOFPS4            =     464Ah
_ERRNOTENOUGHMEM        =     464Eh
_FPSMINUS9              =     4652h
_HLMINUS9               =     4656h
_ERROVERFLOW            =     465Ah
_ERRDIVBY0              =     465Dh
_errSingularMat         =     4660h
_ERRDOMAIN              =     4663h
_errIncrement           =     4666h
_ERRNON_REAL            =     4669h
_errSyntax              =     466Ch
_ERRDATATYPE            =     466Fh
_errArgument            =     4672h
_errDimMismatch         =     4675h
_ERRDIMENSION           =     4678h
_ERRUNDEFINED           =     467Bh
_ERRMEMORY              =     467Eh
_ERRINVALID             =     4681h
_ERRBREAK               =     4684h
_errStat                =     4687h
_errSignChange          =     468Ah
_errIterations          =     468Dh
_errBadGuess            =     4690h
_errTolTooSmall         =     4693h
_ERRSTATPLOT            =     4696h
_errLinkXmit            =     4699h
_JError                 =     469Ch
_JErrorNo               =     469Fh
_noErrorEntry           =     46A2h
_pushErrorHandlER       =     46A5h
_popErrorHandleR        =     46A9h
_strCopy                =     46ADh
_strCat                 =     46B1h
_isInSet                =     46B5h
_sDone                  =     46B9h
_serrort                =     46BDh
_sNameEq                =     46C1h
_sUnderScr              =     46C5h
_sFAIL                  =     46C9h
_sName                  =     46CDh
_sOK                    =     46D1h
_seqn                   =     46D5h
_Sselect                =     46D9h
_STransmit              =     46DDh
_SRECURN                =     46E1h
_GEQNAMEA               =     46E5h
_RECNAME                =     46E9h
___bank_call            =     46EDh
___bank_ret             =     46F1h
___bank_jump            =     46F5h
___bank_entry           =     46F9h
_READDISP2              =     46FDh
_putmap                 =     4701h
_putc                   =     4705h
_dispHL                 =     4709h
_puts                   =     470Dh
_putpsb                 =     4711h
_putps                  =     4715h
_wputps                 =     4719h
_putbuf                 =     471Dh
_putbuf1                =     4721h
_wputc                  =     4725h
_wputs                  =     4729h
_wputsEOL               =     472Dh
_wdispEOL               =     4731h
_whomeUp                =     4735h
_setNumWindow           =     4739h
_newline                =     473Dh
_moveDown               =     4741h
_scrollUp               =     4745h
_shrinkWindow           =     4749h
_moveUp                 =     474Dh
_scrollDown             =     4751h
_clrlcdfull             =     4755h
_clrLCD                 =     4759h
_clrscrnfull            =     475Dh
_clrScrn                =     4761h
_clrtxtshd              =     4765h
_clrWindow              =     4769h
_eraseeol               =     476Dh
_eraseEOW               =     4771h
_homeUp                 =     4775h
_getcurloc              =     4779h
_vputmap                =     477Dh
_vputs                  =     4781h
_vputsn                 =     4785h
_vputsnG                =     4789h
_vputsnT                =     478Dh
_runIndicOn             =     4791h
_runIndicOff            =     4795h
_saveCmdShadow          =     4799h
_saveShadow             =     479Dh
_rstrShadow             =     47A1h
_rstrpartial            =     47A5h
_rstrCurRow             =     47A9h
_rstrUnderMenu          =     47ADh
_rstrbotrow             =     47B1h
_saveTR                 =     47B5h
_restoreTR              =     47B9h
_GETKEYPRESS            =     47BDh
_GETTOKLEN              =     47C1h
_GET_TOK_STRNG          =     47C5h
_GETTOKSTRING           =     47C9h
_PUTBPATBUF2            =     47CDh
_PUTBPATBUF             =     47D1h
_putbPAT                =     47D5h
_putcCheckScrolL        =     47D9h
_dispEOL                =     47DDh
_fdispEOL               =     47E1h
_MAKEROWCMD             =     47E5h
_TOTOSTRP               =     47E9h
_SETVARNAME             =     47EDh
_dispDone               =     47F1h
_dispDone               =     47F1h
_finishoutput           =     47F5h
_curBlink               =     47F9h
_CURSOROFF              =     47FDh
_hideCursor             =     4801h
_cursorOn               =     4805h
_showCursor             =     4809h
_KEYTOSTRING            =     480Dh
_PULLDOWNCHK            =     4811h
_MenuCatCommon          =     4815h
_ZIfCatalog             =     4819h
_ZIfMatrixMenu          =     481Dh
_loadCurCat             =     4821h
_NCifprgmedmode         =     4825h
_LoadMenuNum            =     4829h
_LoadMenuNumL           =     482Dh
_MenuEdKey              =     4831h
_MenCatRet              =     4835h
_notalphnum             =     4839h
_SaveSavedFlags         =     483Dh
_SetMenuFlags           =     4841h
_RstrSomeFlags          =     4845h
_RstrSmallText          =     4849h
_dispListName           =     484Dh
_SeeIfErrorCx           =     4851h
_RstrOScreen            =     4855h
_SaveOscreen            =     4859h
_DispLAlphaName         =     485Dh
_AbortPrgmode           =     4861h
_IS_FULLCNTX            =     4865h
_ADRMROW                =     4869h
_ADRMELE                =     486Dh
_GETMATOP1A             =     4871h
_GETM1TOOP1             =     4875h
_GETM1TOP1A             =     4879h
_GETMTOOP1              =     487Dh
_PUTTOM1A               =     4881h
_PUTTOMA1               =     4885h
_PUTTOMAT               =     4889h
_MAT_EL_DIV             =     488Dh
_CMATFUN                =     4891h
_ROWECH_POLY            =     4895h
_ROWECHELON             =     4899h
_ADRLELE                =     489Dh
_GETL1TOOP1             =     48A1h
_GETL1TOP1A             =     48A5h
_Getltoop1              =     48A9h
_GETL1TOOP2             =     48ADh
_GETL1TOP2A             =     48B1h
_GETL2TOP1A             =     48B5h
_PUTTOLA1               =     48B9h
_PUTTOL                 =     48BDh
_MAXMINLST              =     48C1h
_LLOW                   =     48C5h
_LHIGH                  =     48C9h
_LSUM                   =     48CDh
_CUMSUM                 =     48D1h
_TOFRAC                 =     48D5h
_SEQSET                 =     48D9h
_SEQSOLVE               =     48DDh
_CMP_NUM_INIT           =     48E1h
_BINOPEXEC              =     48E5h
_EXMEAN1                =     48E9h
_SET2MVLPTRS            =     48EDh
_SETMAT1                =     48F1h
_CREATETLIST            =     48F5h
_UNOPEXEC               =     48F9h
_THREEEXEC              =     48FDh
_RESTOREERRNO           =     4901h
_FOUREXEC               =     4904h
_FIVEEXEC               =     4908h
_CPYTO2ES1              =     490Ch
_CPYTO6ES1              =     4910h
_CPYTO1ES1              =     4914h
_CPYTO3ES1              =     4918h
_CPYTO3ES2              =     491Ch
_CPYTO2ES2              =     4920h
_CPYTO1ES2              =     4924h
_CPYTO2ES3              =     4928h
_CPYTO1ES3              =     492Ch
_CPYTO3ES4              =     4930h
_CPYTO6ES3              =     4934h
_CPYTO2ES4              =     4938h
_CPYTO1ES4              =     493Ch
_CPYTO2ES5              =     4940h
_CPYTO1ES5              =     4944h
_CPYTO4EST              =     4948h
_CPYTO2EST              =     494Ch
_CPYTO1EST              =     4950h
_CPYTO2ES6              =     4954h
_CPYTO1ES6              =     4958h
_CPYTO2ES7              =     495Ch
_CPYTO1ES7              =     4960h
_CPYTO2ES8              =     4964h
_CPYTO1ES8              =     4968h
_CPYTO1ES9              =     496Ch
_CPYTO2ES9              =     4970h
_CPYTO2ES10             =     4974h
_CPYTO1ES10             =     4978h
_CPYTO2ES11             =     497Ch
_CPYTO1ES11             =     4980h
_CPYTO2ES12             =     4984h
_CPYTO1ES12             =     4988h
_CPYTO2ES13             =     498Ch
_CPYTO1ES13             =     4990h
_CPYTO1ES14             =     4994h
_CPYTO1ES16             =     4998h
_CPYTO1ES17             =     499Ch
_CPYTO1ES18             =     49A0h
_CPYTO1ES15             =     49A4h
_CPYTO2ES15             =     49A8h
_CPYO1TOEST             =     49ACh
_CPYO1TOES1             =     49B0h
_CPYO6TOES1             =     49B4h
_CPYO6TOES3             =     49B8h
_CPYO1TOES2             =     49BCh
_CPYO2TOES2             =     49C0h
_CPYO1TOES3             =     49C4h
_CPYO1TOES4             =     49C8h
_CPYO1TOES5             =     49CCh
_CPYO1TOES6             =     49D0h
_CPYO1TOES7             =     49D4h
_CPYO2TOES4             =     49D8h
_CPYO2TOES5             =     49DCh
_CPYO2TOES6             =     49E0h
_CPYO2TOES7             =     49E4h
_CPYO2TOES8             =     49E8h
_CPYO2TOES9             =     49ECh
_CPYO1TOES8             =     49F0h
_CPYO1TOES9             =     49F4h
_CPYO1TOES10            =     49F8h
_CPYO1TOES11            =     49FCh
_CPYO1TOES12            =     4A00h
_CPYO1TOES13            =     4A04h
_CPYO1TOES14            =     4A08h
_CPYO1TOES15            =     4A0Ch
_EVALF3A                =     4A10h
COMPLEX_EXEC            =     4A14h
_GetK                   =     4A18h
_setTitlE               =     4A1Ch
_dispVarVal             =     4A20h
_RecallEd               =     4A24h
_setupBuffer            =     4A28h
_createNumEditBUF       =     4A2Ch
_CallCommon             =     4A30h
_CommonKeys             =     4A34h
_Leftmore               =     4A37h
_fDel                   =     4A3Ah
_fClear                 =     4A3Eh
_finsDisp               =     4A42h
_FinsDisp02             =     4A45h
_setIndicator           =     4A49h
_CloseEditBufNo         =     4A4Dh
_releaseBuffer          =     4A51h
_varnameToOP1hl         =     4A55h
_nameToOP1              =     4A59h
_numPPutAway            =     4A5Dh
_numRedisp              =     4A61h
_numError02             =     4A65h
_LOAD_SFONT             =     4A68h
_SFONT_LEN              =     4A6Ch
_InitNumVec             =     4A70h
_SETXXOP1               =     4A74h
_SETXXOP2               =     4A78h
_setXXXXOp2             =     4A7Ch
_UCLINES                =     4A80h
_CLine                  =     4A84h
_CLineS                 =     4A88h
_XROOTY                 =     4A8Ch
_YTOX                   =     4A90h
_ZMSTATS                =     4A94h
_POINT_STAT_HLP         =     4A98h
_DRAWSPLOT              =     4A9Ch
_INITNEWTRACEP          =     4AA0h
_SPLOTCOORD             =     4AA4h
_SPLOTRIGHT             =     4AA8h
_SPLOTLEFT              =     4AACh
_CMPBOXINFO             =     4AB0h
_NEXTPLOT               =     4AB4h
_PREVPLOT               =     4AB8h
_CLRPREVPLOT            =     4ABCh
_PUT_INDEX_LST          =     4AC0h
_GET_INDEX_LST          =     4AC4h
_HEAP_SORT              =     4AC8h
_STOGDB2                =     4ACCh
_RCLGDB2                =     4AD0h
_CIRCCMD                =     4AD4h
_GRPHCIRC               =     4AD8h
_MOV18B                 =     4ADCh
_DARKLINE               =     4AE0h
_ILINE                  =     4AE4h
_IPOINT                 =     4AE8h
_XYRNDBOTH              =     4AECh
_XYRND                  =     4AF0h
_CheckTOP               =     4AF4h
_CheckXY                =     4AF8h
_DARKPNT                =     4AFCh
_CPointS                =     4B00h
_WTOV                   =     4B04h
_VtoWHLDE               =     4B08h
_XItoF                  =     4B0Ch
_YFTOI                  =     4B10h
_XFTOI                  =     4B14h
_TraceOff               =     4B18h
_GrRedisp               =     4B1Ch
_gdisptoken             =     4B20h
_GRDECODA               =     4B24h
_LABCOOR                =     4B28h
_COORDISP               =     4B2Ch
_TMP=NOSRC            =     4B30h
_GRLABELS               =     4B34h
_YPIXSET                =     4B38h
_XPIXSET                =     4B3Ch
_COPYRNG                =     4B40h
_VALCUR                 =     4B44h
_GRPUTAWAY              =     4B48h
_RSTGFLAGS              =     4B4Ch
_GRReset                =     4B50h
_XYCENT                 =     4B54h
_ZOOMXYCMD              =     4B58h
_CPTDELY                =     4B5Ch
_CPTDELX                =     4B60h
_SETFUNCM               =     4B64h
_SETSEQM                =     4B68h
_SETPOLM                =     4B6Ch
_SETPARM                =     4B70h
_ZMINT                  =     4B74h
_ZMDECML                =     4B78h
_ZMPREV                 =     4B7Ch
_ZMUSR                  =     4B80h
_SETUZM                 =     4B84h
_ZMFIT                  =     4B88h
_ZMSQUARE               =     4B8Ch
_ZMTRIG                 =     4B90h
_SetXMinMax             =     4B94h
_ZooDefault             =     4B98h
_GRBUFCPY               =     4B9Ch
_DRAWSPLITLINE          =     4BA0h
_RestoreDisp            =     4BA4h
_FNDDB                  =     4BA8h
_ALLEQ                  =     4BACh
_fndallseleq            =     4BB0h
_NEXTEQ                 =     4BB4h
_PREVEQ                 =     4BB8h
_BLINKGCUR              =     4BBCh
_NBCURSOR               =     4BC0h
_STATMARK               =     4BC4h
_CHKTEXTCURS            =     4BC8h
_Regraph                =     4BCCh
_DOREFFLAGS02           =     4BD0h
_INITNSEQ               =     4BD4h
_YRES                   =     4BD8h
_PLOTPTXY2              =     4BDCh
_Ceiling                =     4BE0h
_PutXY                  =     4BE4h
_PUT=NO               =     4BE7h
_PDspGrph               =     4BEBh
_PDspGrph               =     4BEBh
_Horizcmd               =     4BEFh
_VERTCMD                =     4BF3h
_LINECMD                =     4BF7h
_UNLINECMD              =     4BFBh
_POINTCMD               =     4BFFh
_PIXELTEST              =     4C03h
_PIXELCMD               =     4C07h
_TANLNF                 =     4C0Bh
_DRAWCMD_INIT           =     4C0Fh
_DRAWCMD                =     4C13h
_SHADECMD               =     4C17h
_INVCMD                 =     4C1Bh
_STATSHADE              =     4C1Fh
_dspmattable            =     4C23h
_dsplsts                =     4C27h
_closeEditBuf           =     4C2Bh
_parseEditBuf           =     4C2Fh
_putsm                  =     4C33h
_DspCurTbl              =     4C37h
_DSPGRTBL               =     4C3Bh
_ZeroTemplate           =     4C3Fh
_settblrefs             =     4C43h
_dispTblBot             =     4C47h
_DispTblTop             =     4C4Bh
_dispTblbody            =     4C4Fh
_vputblank              =     4C53h
_VPUTBLANK              =     4C53h
_TBLTRACE               =     4C57h
_dispListNameY          =     4C5Bh
_CurNameLength          =     4C5Fh
_NameToBuf              =     4C63h
_jpromptcursor          =     4C67h
_bufLeft                =     4C6Ah
_BUFRIGHT               =     4C6Eh
_bufInsert              =     4C72h
_bufQueueChar           =     4C76h
_bufReplace             =     4C7Ah
_bufDelete              =     4C7Eh
_BUFPEEK                =     4C82h
_BUFPEEK1               =     4C86h
_BUFPEEK2               =     4C8Ah
_BUFPEEK3               =     4C8Eh
_BufToBtm               =     4C92h
_setupEditEqu           =     4C96h
_bufToTop               =     4C9Ah
_isEditFull             =     4C9Eh
_isEditEmpty            =     4CA2h
_ISATTOP                =     4CA6h
_isAtBtm                =     4CAAh
_bufClear               =     4CAEh
_JcursorFirst           =     4CB2h
_JcursorLast            =     4CB5h
_cursorLeft             =     4CB8h
_JcursorRight           =     4CBCh
_JcursorUp              =     4CBFh
_cursorDown             =     4CC2h
_cursorToOffset         =     4CC6h
_insDisp                =     4CCAh
_FDISPBOL1              =     4CCEh
_FDISPBOL               =     4CD2h
_dispEOW                =     4CD6h
_dispHead               =     4CDAh
_dispTail               =     4CDEh
_PutTokString           =     4CE2h
_setupEditCmd           =     4CE6h
_setEmptyEditEqu        =     4CEAh
_setEmptyEditPtR        =     4CEEh
_closeEditEqu           =     4CF2h
_toggleIns              =     4CF6h
_GETPREVTOK             =     4CFAh
_getkey                 =     4CFEh
_getkey                 =     4CFEh
_canIndic               =     4D02h
_LCD_DRIVERON           =     4D06h
_DFMIN2                 =     4D0Ah
_formDisp               =     4D0Eh
_formMatrix             =     4D12h
_wscrollLeft            =     4D16h
_wscrollUp              =     4D1Ah
_wscrollDown            =     4D1Eh
_wscrollRight           =     4D22h
_formEReal              =     4D26h
_formERealTOK           =     4D2Ah
_formdcplx              =     4D2Eh
_formReal               =     4D32h
_formScrollUp           =     4D36h
_setwinabove            =     4D3Ah
_disarmScroll           =     4D3Eh
_OP1toEdit              =     4D42h
_MinToEdit              =     4D46h
_rclVarToEdit           =     4D4Ah
_rclVarToEditPtR        =     4D4Eh
_RCLENTRYTOEDIT         =     4D52h
_rclToQueue             =     4D56h
_FORMTOTOK              =     4D5Ah
_DISP_INTERVAL          =     4D5Eh
_DisplstName            =     4D62h
_dispSLstNameHL         =     4D66h
_EditEqu                =     4D6Ah
_closeEquField          =     4D6Eh
_AutoSelect             =     4D72h
_DISPYEOS               =     4D76h
_dispNumEOS             =     4D7Ah
_setupdispeq            =     4D7Eh
_dispForward            =     4D82h
_DispYPrompt2           =     4D86h
_stringwidth            =     4D8Ah
_dispErrorScreeN        =     4D8Eh
_POPCX                  =     4D92h
_loadnoeentry           =     4D96h
_SaveScreen             =     4D9Ah
_RETSCREEN              =     4D9Eh
_RetScreenErr           =     4DA2h
_SplitChange            =     4DA6h
_SolveRedisp            =     4DAAh
_SolveDisp              =     4DAEh
_itemName               =     4DB2h
_SETNORM_VALS           =     4DB6h
_SETVERT_VALS           =     4DBAh
_CONVKEYTOTOK           =     4DBEh
_KEYTOTOKNEW2B          =     4DC2h
_KEYTOTOK2BYTE          =     4DC6h
_TOKTOKEY               =     4DCAh
_LOAD_LFONT             =     4DCEh
_SEND1BERR              =     4DD2h
_GETVARCMD              =     4DD6h
_SENDVARCMD             =     4DDAh
_PRINTSCREEN            =     4DDEh
_KEYSCNLNK              =     4DE2h
_IORESETALL             =     4DE6h
_DELRES                 =     4DEAh
_CONVLCTOLR             =     4DEEh
_REDIMMAT               =     4DF2h
_INCLSTSIZE             =     4DF6h
_insertlist             =     4DFAh
_dellistel              =     4DFEh
_EDITPROG               =     4E02h
_CLOSEPROG              =     4E06h
_CLRGRAPHREF            =     4E0Ah
_FIXTEMPCNT             =     4E0Eh
_SAVEDATA               =     4E12h
_RESTOREDATA            =     4E16h
_FindAlphaup            =     4E1Ah
_FindAlphadn            =     4E1Eh
_CMPSYMS                =     4E22h
_CREATETEMP             =     4E26h
_CLEANALL               =     4E2Ah
_MOVETONEXTSYM          =     4E2Eh
_CONVLRTOLC             =     4E32h
_TblScreenDn            =     4E36h
_TblScreenUp            =     4E3Ah
_SCREENUP               =     4E3Eh
_SCREENUPDOWN           =     4E42h
_ZifRclHandler          =     4E46h
_zifrclkapp             =     4E4Ah
_rclkeyRtn              =     4E4Eh
_RCLKEY                 =     4E52h
_RCLREGEQ_CALL          =     4E55h
_RCLREGEQ               =     4E59h
_initNamePrompt         =     4E5Ch
_NamePrompt2            =     4E60h
_CATALOGCHK             =     4E64h
_clrTR                  =     4E68h
_QUAD                   =     4E6Ch
_GRAPHQUAD              =     4E70h
_BC2NONREAL             =     4E74h
_ERRNONREAL             =     4E78h
_WRITE_TEXT             =     4E7Ch
_FORSEQINIT             =     4E80h
_GRPHPARS               =     4E84h
_PLOTPARS               =     4E88h
_PARSEinp               =     4E8Ch
_PARSEOFF               =     4E90h
_PARSESCAN              =     4E94h
_GETPARSE               =     4E98h
_SAVEPARSE              =     4E9Ch
_InitPFlgs              =     4EA0h
_CKENDLINERR            =     4EA4h
_Op2set60               =     4EA8h
_GETSTATPTR             =     4EACh
_CMP_STATPTR            =     4EB0h
_VARSYSADR              =     4EB4h
_Stosystok              =     4EB8h
_STOANS                 =     4EBCh
_STOTHETA               =     4EC0h
_STOR                   =     4EC4h
_Stoy                   =     4EC8h
_STON                   =     4ECCh
_STOT                   =     4ED0h
_Stox                   =     4ED4h
_Stoother               =     4ED8h
_RCLANS                 =     4EDCh
_Rcly                   =     4EE0h
_RCLN                   =     4EE4h
_Rclx                   =     4EE8h
_Rclvarsym              =     4EECh
_Rclsystok              =     4EF0h
_StMatEl                =     4EF4h
_STLSTVECEL             =     4EF8h
_Convop1                =     4EFCh
_isaletter              =     4F00h
_Find_Parse_ForMULA     =     4F04h
_PARSE_FORMULA          =     4F08h
_STRNGENT1              =     4F0Ch
_GETNUMLET              =     4F15h
_INIT_PROG_LIST         =     4F1Eh
_PRGRDLP                =     4F27h
_VARENT                 =     4F30h
_RCLSTAT                =     4F39h
_PARSELOOP              =     4F42h
_PARSEONC               =     4F4Bh
_PARSEON                =     4F52h
_PARSECMD               =     4F5Ah
_STOTYPE                =     4F61h
_CREATEPAIR             =     4F6Ah
_PUSHNUM                =     4F73h
_INCCURPCERREND         =     4F7Ch
_ERREND                 =     4F85h
_COMMAERRF              =     4F8Eh
_COMMAERR               =     4F97h
_STEQARG2               =     4FA0h
_STEQARG                =     4FA9h
_INPARG                 =     4FB2h
_STEQARG3               =     4FBBh
_NXTFETCH               =     4FC4h
_CKFETCHVAR             =     4FCDh
_FETCHVARA              =     4FD6h
PARSER_EXEC             =     4FDFh
_FETCHVAR               =     4FE3h
_CKENDLIN               =     4FECh
_CKENDEXP               =     4FF5h
_CKPARSEND              =     4FFEh
_STOTYPEARG             =     5007h
_CONVDIM                =     5010h
_CONVDIM00              =     5019h
_AHEADEQUAL             =     5022h
_PARSAHEADS             =     502Bh
_PARSAHEAD              =     5034h
_ANSNAME                =     503Dh
_STOCMPREALS            =     5046h
_GETDEPTR               =     504Fh
_PUSH2BOPER             =     5058h
_POP2BOPER              =     5061h
_PUSHOPER               =     506Ah
_POPOPER                =     5073h
_FIND_E_UNDEF           =     507Ch
_STTMPEQ                =     5085h
_FINDEOL                =     508Eh
_BRKINC                 =     5097h
_INCFETCH               =     50A0h
_CURFETCH               =     50A9h
PGMIO_EXEC              =     50B2h
_Random                 =     50B6h
_Storand                =     50BAh
_RandInit               =     50BEh
_resetStacks            =     50C2h
_FACTORIAL              =     50C6h
_YONOFF                 =     50CAh
_EQSELUNSEL             =     50CEh
_ITSOLVER               =     50D2h
_GRITSOLVER             =     50D6h
_ITSOLVERB              =     50DAh
_ITSOLVERNB             =     50DEh
_ExTest_INT             =     50E2h
_DIST_FUN               =     50E6h
_LOGGAMMA               =     50EAh
_ONEVAR                 =     50EEh
_ONEVARS_0              =     50F2h
_ORDSTAT                =     50F6h
_INITSTATANS2           =     50FAh
_ANOVA_SPEC             =     50FEh
_EXEC_ASSEMBLY          =     5102h
_outputexpr             =     5106h
_CentCursor             =     510Ah
_TEXT                   =     5113h
_FINISHSPEC             =     511Ch
_TRCYFUNC               =     5125h
_RCL_SEQ_X              =     512Eh
_RCLSEQ2                =     5137h
_GRPPutAway             =     5140h
_CKVALDELX              =     5149h
_CKVALDELTA             =     5152h
_Grbufclr               =     515Bh
_Grbufcpy_v             =     5164h
_FNDSELEQ               =     516Dh
_CLRGRAPHXY             =     5176h
_NEXT_Y_STYLE           =     517Fh
GRAPH_EXEC              =     5188h
_PLOTPT                 =     518Ch
_NEWINDEP               =     5195h
_Axes                   =     519Eh
_setPenX                =     51A7h
_setPenY                =     51B0h
_setPenT                =     51B9h
_TAN_EQU_DISP           =     51C2h
_putAns                 =     51CBh
_DISPOP1A               =     51D4h
_MATHTANLN              =     51DDh
_ENDDRAW                =     51E6h
IO_EXEC                 =     51EFh
EXECUTE_Z80             =     52E5h

;RAM =ates
;------------------------------------
curRow                  =     800Ch
curCol                  =     800Dh
OP1                     =     8039h
op1+1                   =     803Ah
op1M                    =     803Bh
OP2                     =     8044h
op2exp                  =     8045h
op2m                    =     8046H
op3                     =     804Fh
op3exP                  =     8050h
OP3M                    =     8051h
OP3EXT                  =     8058h
OP4                     =     805Ah
OP4EXP                  =     805Bh
OP4M                    =     805Ch
OP4EXT                  =     8063h
op5                     =     8065h
OP5EXP                  =     8066h
op5M                    =     8067h
OP5EXT                  =     806Eh
OP6                     =     8070h
op6exP                  =     8071h
OP6M                    =     8072h
op6exT                  =     8079h
Imathptr1               =     8094h
Imathptr2               =     8096h
IMATHPTR3               =     8098h
Imathptr4               =     809Ah
IMATHPTR5               =     809Ch
ASM_IND_CALL            =     80C8h
asm_ind_call            =     80C8h
TEXTSHADOW              =     80C9h
PENCOL                  =     8252h
PENROW                  =     8253h
savesscreen             =     8265h
usermem_offset          =     8565h
statvars                =     858Fh
CURGY                   =     886Dh
CURGX                   =     886Eh
PLOTSSCREEN             =     8E29h
cmdshadow               =     9157h
progPtr                 =     9319h
symTable                =     FE6Eh

;Address of LCD Driver Instruction Port
;--------------------------------------
LCDINSTPORT		=	10h

;Screen Shadows
;------------------------------------
TEXTSHADOW		=	80C9h
CMDSHADOW		=	9157h

;Graph Backup Buffer
;------------------------------------
PLOTSSCREEN		=	8E29h

;Safe RAM Locations
;------------------------------------
SAVESSCREEN		=	8265h
STATVARS		=	858Fh

;Indirect Call Byte
;------------------------------------
ASM_IND_CALL		=	80C8h

;Link Port Write equates
;------------------------------------
D0LD1L			=	0C3h
D0LD1H			=	0C1h
D0HD1L			=	0C2h
D0HD1H			=	0C0h
BPORT			=	0

;System Flag =ates
;------------------------------------
trigflags		=	0
trigdeg			=	2
;
plotflags		=	2
plotloc			=	1
plotdisp		=	2
;
grfmodeflags		=	2
grffuncm		=	4
grfpolarm		=	5
grfparamm		=	6
grfrecurm		=	7
;
graphflags		=	3
graphdraw		=	0
graphcursor		=	2
;
grfdbflags		=	4
grfdot			=	0
grfsimul		=	1
grfgrid			=	2
grfpolar		=	3
grfnocoord		=	4
grfnoaxis		=	5
grflabel		=	6
;
textflags		=	5
textEraseBelow		=	1
textScrolled		=	2
textInverse		=	3
;
onflags			=	9
onRunning		=	3
onInterrupt		=	4
;
statflags		=	9
statsvalid		=	6
;
fmtflags		=	10
fmtExponent		=	0
fmtEng			=	1
;
nummode			=	10
FMTREAL			=	5
FMTRECT			=	6
FMTPOLAR		=	7
;
curflags		=	12
curAble			=	2
curOn			=	3
curLock			=	4
;
appflags		=	13
appTextSave		=	1
appAutoScroll		=	2
;
PLOTFLAG2		=	17
EXPR_PARAM		=	3
EXPR_WRITING		=	4
;
indicflags		=	18
indicRun		=	0
indicOnly		=	2
;
shiftflags		=	18
shift2nd		=	3
shiftAlpha		=	4
shiftALock		=	6
;
tblflags		=	19
AutoFill		=	4
AutoCalc		=	5
;
sgrflags		=	20
grfSplit		=	0
VertSplit		=	1
WRITE_ON_GRAPH		=	4
textwrite		=	7
;
asm_flag1		=	33
asm_flag1_0		=	0
asm_flag1_1		=	1
asm_flag1_2		=	2
asm_flag1_3		=	3
asm_flag1_4		=	4
asm_flag1_5		=	5
asm_flag1_6		=	6
asm_flag1_7		=	7
;
asm_flag2		=	34
asm_flag2_0		=	0
asm_flag2_1		=	1
asm_flag2_2		=	2
asm_flag2_3		=	3
asm_flag2_4		=	4
asm_flag2_5		=	5
asm_flag2_6		=	6
asm_flag2_7		=	7
;
asm_flag3		=	35
asm_flag3_0		=	0
asm_flag3_1		=	1
asm_flag3_2		=	2
asm_flag3_3		=	3
asm_flag3_4		=	4
asm_flag3_5		=	5
asm_flag3_6		=	6
asm_flag3_7		=	7

;System Var =ates, For Storing and Recalling Them
;--------------------------------------------------
XSCLt			=	2 ;XSCALE
YSCLt			=	3 ;YSCALE
XMINt			=	0Ah ;XMIN
XMAXt			=	0Bh ;XMAX
YMINt			=	0Ch ;YMIN
YMAXt			=	0Dh ;XMAX
TMINt			=	0Eh ;TMIN
TMAXt			=	0Fh ;TMAX
THETMINt		=	10h ;THETA MIN
THETMAXt		=	11h ;THETA MAX
TBLMINt			=	1Ah ;TABLE MIN
PLOTSTARTt		=	1Bh ;PLOT START
NMAXt			=	1Dh ;NMAX
nmint			=	1Fh ;NMIN
TBLSTEPt		=	21h ;TABLE STEP
TSTEPt			=	22h ;T STEP
THETSTEPt		=	23h ;THETA STEP
DELTAXt			=	26h ;DELTA X
DELTAYt			=	27h ;DELTA Y
XFACTt			=	28h ;X ZOOM FACTOR
YFACTt			=	29h ;Y ZOOM FACTOR
FINnT			=	2Bh ;TVM n
FINiT			=	2Ch ;TVM i
FINpvT			=	2Dh ;TVM pv
FINpmtT			=	2Eh ;TVM pmt
FINfvT			=	2Fh ;TVM fv
FINpyT			=	30h ;TVM p/y
FINcyT			=	31h ;TVM c/y
PLOTSTEPT		=	34h ;PLOT STEP
XREST			=	36h ;X RES

;Character Font =ates
;-----------------------------------
LSEQ_N			=	001h
LSEQ_U			=	002h
LSEQ_V			=	003h
LSEQ_W			=	004h
Lconvert		=	005h
LsqUp			=	006h
LsqDown			=	007h
Lintegral		=	008h
Lcross			=	009h
LBOXICON		=	00Ah
LCROSSICON		=	00Bh
LDOTICON		=	00Ch
LsubT			=	00Dh ;small capital T for parametric mode.
LcubeR			=	00Eh ;slightly different 3 for cubed root.
LhexF			=	00Fh
Lroot			=	010h
Linverse		=	011h
Lsquare			=	012h
Langle			=	013h
Ldegree			=	014h
Lradian			=	015h
Ltranspose		=	016h
LLE			=	017h
LNE                     =     018h
LGE                     =     019h
Lneg                    =     01Ah
Lexponent               =     01Bh
Lstore                  =     01Ch
Lten                    =     01Dh
LupArrow                =     01Eh
LdownArrow              =     01Fh
Lspace                  =     020h
Lexclam                 =     021h
Lquote                  =     022h
Lpound                  =     023h
LFOURTH                 =     024h
Lpercent                =     025h
Lampersand              =     026h
Lapostrophe             =     027h
LlParen                 =     028h
LrParen                 =     029h
Lasterisk               =     02Ah
LplusSign               =     02Bh
Lcomma                  =     02Ch
Ldash                   =     02Dh
Lperiod                 =     02Eh
Lslash                  =     02Fh
L0                      =     030h
L1                      =     031h
L2                      =     032h
L3                      =     033h
L4                      =     034h
L5                      =     035h
L6                      =     036h
L7                      =     037h
L8                      =     038h
L9                      =     039h
Lcolon                  =     03Ah
Lsemicolon              =     03Bh
LLT                     =     03Ch
LEQ                     =     03Dh
LGT                     =     03Eh
Lquestion               =     03Fh
LatSign                 =     040h
LcapA                   =     041h
LcapB                   =     042h
LcapC                   =     043h
LcapD                   =     044h
LcapE                   =     045h
LcapF 	        	=     046h
LcapG   	        =     047h
LcapH			=     048h
LcapI			=     049h
LcapJ			=     04Ah
LcapK			=     04Bh
LcapL			=     04Ch
LcapM			=     04Dh
LcapN			=     04Eh
LcapO			=     04Fh
LcapP			=     050h
LcapQ			=     051h
LcapR			=     052h
LcapS			=     053h
LcapT			=     054h
LcapU			=     055h
LcapV			=     056h
LcapW			=     057h
LcapX			=     058h
LcapY			=     059h
LcapZ			=     05Ah
Ltheta			=     05Bh
Lbackslash		=     05Ch
LrBrack			=     05Dh
Lcaret			=     05Eh
Lunderscore		=     05Fh
Lbackquote		=     060h
La			=     061h
Lb			=     062h
Lc			=     063h
Ld			=     064h
Le			=     065h
Lf			=     066h
Lg			=     067h
Lh			=     068h
Li			=     069h
Lj			=     06Ah
Lk			=     06Bh
Ll			=     06Ch
Lm			=     06Dh
Ln			=     06Eh
Lo			=     06Fh
Lp			=     070h
Lq			=     071h
Lr			=     072h
Ls			=     073h
Lt			=     074h
Lu			=     075h
Lv			=     076h
Lw			=     077h
Lx			=     078h
Ly			=     079h
Lz			=     07Ah
LlBrace			=     07Bh
Lbar			=     07Ch
LrBrace			=     07Dh
Ltilde			=     07Eh
LinvEQ			=     07Fh
Lsub0			=     080h
Lsub1			=     081h
Lsub2			=     082h
Lsub3			=     083h
Lsub4			=     084h
Lsub5			=     085h
Lsub6			=     086h
Lsub7			=     087h
Lsub8			=     088h
Lsub9			=     089h
LcapAAcute		=     08Ah
LcapAGrave		=     08Bh
LcapACaret		=     08Ch
LcapADier		=     08Dh
LaAcute			=     08Eh
LaGrave			=     08Fh
LaCaret			=     090h
LaDier			=     091h
LcapEAcute		=     092h
LcapEGrave		=     093h
LcapECaret		=     094h
LcapEDier		=     095h
LeAcute			=     096h
LeGrave			=     097h
LeCaret			=     098h
LeDier			=     099h
LcapIAcute		=     09Ah
LcapIGrave		=     09Bh
LcapICaret		=     09Ch
LcapIDier		=     09Dh
LiAcute			=     09Eh
LiGrave			=     09Fh
LiCaret			=     0A0h
LiDier			=     0A1h
LcapOAcute		=     0A2h
LcapOGrave		=     0A3h
LcapOCaret		=     0A4h
LcapODier		=     0A5h
LoAcute			=     0A6h
LoGrave			=     0A7h
LoCaret			=	0A8h
LoDier			=     0A9h
LcapUAcute		=     0AAh
LcapUGrave		=     0ABh
LcapUCaret		=     0ACh
LcapUDier		=     0ADh
LuAcute			=     0AEh
LuGrave			=     0AFh
LuCaret			=     0B0h
LuDier			=     0B1h
LcapCCed		=     0B2h
LcCed			=     0B3h
LcapNTilde		=     0B4h
LnTilde			=     0B5h
Laccent			=     0B6h
Lgrave			=     0B7h
Ldieresis		=     0B8h
LquesDown		=     0B9h
LexclamDown		=     0BAh
Lalpha			=     0BBh
Lbeta			=     0BCh
Lgamma			=     0BDh
LcapDelta		=     0BEh
Ldelta			=     0BFh
Lepsilon		=     0C0h
LlBrack			=     0C1h
Llambda			=     0C2h
Lmu			=     0C3h
Lpi			=     0C4h
Lrho			=     0C5h
LcapSigma		=     0C6h
Lsigma			=     0C7h
Ltau			=     0C8h
Lphi			=     0C9h
LcapOmega		=     0CAh
LxMean			=     0CBh
LyMean			=     0CCh
LsupX			=     0CDh
Lellipsis		=     0CEh
Lleft			=     0CFh
Lblock			=     0D0h
Lper			=     0D1h
Lhyphen			=     0D2h
Larea			=     0D3h
Ltemp			=     0D4h
Lcube			=     0D5h
Lenter			=     0D6h
LimagI			=     0D7h
Lphat			=     0D8h
Lchi			=     0D9h
LstatF			=     0DAh
Llne			=     0DBh
LlistL			=     0DCh
LfinanN			=     0DDh

;# Tokens in Use = 249
;--------------------------------
;length (+80h for leading blank)
;(+40h for trailing blank)
EOSSTART		=	0

;Display Conversions Come Immediately Before 'tstore'
;----------------------------------------------------
DCONV			=	01h
;
TTODMS			=	DCONV ;01h
TTODEC			=	DCONV+1 ;02h
TTOAbc			=	DCONV+2 ;03h > A b/c
;
TSTORE			=	DCONV+3 ;04h Lstore 01
;
TBOXPLOT		=	05h
;
BRACKS			=	06h
;
tLBrack			=	BRACKS ;06h '['
tRBrack			=	BRACKS+1 ;07h ']'
tLBrace			=	BRACKS+2 ;08h '{'
tRBrace			=	BRACKS+3 ; 09h '}'
;
TPOST1			=	BRACKS+4
;
tFromRad		=	TPOST1 ;0Ah Lradian
tFromDeg		=	TPOST1+1 ;0Bh Ldegree
tRecip			=	TPOST1+2 ;0Ch Linverse
tSqr			=	TPOST1+3 ;0Dh Lsquare
tTrnspos		=	TPOST1+4 ;0Eh Ltranspose
TCUBE			=	TPOST1+5 ;0Fh '^3'
;
tLParen			=	10h ;10h '('
tRParen			=	11h ;11h ')'

;Immediate Unary Tokens Go From Here To 'EOSEL-1'
;--------------------------------------------------------
IMUN			=	12h
;
tRound			=	IMUN ;12h 'round'
TPXTST			=	IMUN+1 ;13h 'PXL-TEST'
tAug			=	IMUN+2 ;14h 'aug'
tRowSwap		=	IMUN+3 ;15h 'rSwap'
tRowPlus		=	IMUN+4 ;16h 'rAdd'
tmRow			=	IMUN+5 ;17h 'multR'
tmRowPlus		=	IMUN+6 ;18h 'mRAdd'
tMax			=	IMUN+7 ;19h 'max'
tMin			=	IMUN+8 ;1Ah 'min'
TRTOPr			=	IMUN+9 ;1Bh 'R>Pr
TRTOPo			=	IMUN+10 ;1Ch 'R>Po
TPTORx			=	IMUN+11 ;1Dh 'P>Rx
TPTORy			=	IMUN+12 ;1Eh 'P>Ry
tMEDIAN			=	IMUN+13 ;1Fh 'MEDIAN
tRandM			=	IMUN+14 ;20h 'randM'
TMEAN			=	IMUN+15 ;21h
TROOT			=	IMUN+16 ;22h 'ROOT'
tSeries			=	IMUN+17 ;23h 'seq'
tFnInt			=	IMUN+18 ;24h 'fnInt'
tNDeriv			=	IMUN+19 ;25h 'fnIr'
TEVALF			=	IMUN+20 ;26h
TFMIN			=	IMUN+21 ;27h
TFMAX			=	IMUN+22 ;28h

;Token 29h Cannot Be An EOS Function Since EL(=29h Already
;---------------------------------------------------------
tEOSEL			=	IMUN+23
;
TSPACE			=	tEOSEL ;29h ' '
tString			=	tEOSEL+1 ;2Ah '"'
tComma			=	tEOSEL+2 ;2Bh ','

Tii			=	2Ch ;i

;Postfix Functions
;---------------------------------------------
TPOST			=	2Dh
;
tFact			=	TPOST ;2Dh '!'

TCUBICR			=	2Eh
TQUARTR			=	2Fh

;Number Tokens
;--------------------------------------------
NUMS			=	30h
;
T0			=	NUMS ;30h
T1			=	NUMS+1 ;31h
T2			=	NUMS+2 ;32h
T3			=	NUMS+3 ;33h
T4			=	NUMS+4 ;34h
T5			=	NUMS+5 ;35h
T6			=	NUMS+6 ;36h
T7			=	NUMS+7 ;37h
T8			=	NUMS+8 ;38h
T9			=	NUMS+9 ;39h
TDECPT			=	NUMS+10 ;3Ah
Tee			=	NUMS+11 ;3Bh

;Binary OP
;----------------------------------------------
tOr			=	3Ch ;3Ch '_or_'
TXOR			=	3Dh ;3Dh
;
tColon			=	3Eh ;3Eh ':'
tEnter			=	3Fh ;3Fh Lenter

;Binary OP
;--------------------------------------------------
tAnd			=	40h ;40h '_and_' 05

;Letter Tokens
;------------------------------------------
LET			=	41h
;
TA			=	LET ;41h
TB			=	LET+1 ;42h
TC			=	LET+2 ;43h
TD			=	LET+3 ;44h
TE			=	LET+4 ;45h
TF			=	LET+5 ;46h
TG			=	LET+6 ;47h
TH			=	LET+7 ;48h
TI			=	LET+8 ;49h
TJ			=	LET+9 ;4Ah
TK			=	LET+10 ;4Bh
TL			=	LET+11 ;4Ch
TM			=	LET+12 ;4Dh
TN			=	LET+13 ;4Eh
TO			=	LET+14 ;4Fh
TP			=	LET+15 ;50h
TQ			=	LET+16 ;51h
TR			=	LET+17 ;52h
TS			=	LET+18 ;53h
TT			=	LET+19 ;54h
TU			=	LET+20 ;55h
TV			=	LET+21 ;56h
TW			=	LET+22 ;57h
TX			=	LET+23 ;58h
TY			=	LET+24 ;59h
TZ			=	LET+25 ;5Ah
TTHETA			=	LET+26 ;5Bh

;These Var Tokens Are 1st Of A Double Token
; See 'vars.doc' For More Info On Vars
;------------------------------------------
VTOKS			=	LET+27

;User Matrix Token, 2nd Token Needed For Name
;--------------------------------------------
TVARMAT			=	VTOKS ;5Ch

;User List Token, 2nd Token Needed For Name
;-------------------------------------------
TVARLST			=	VTOKS+1 ;5Dh

;User =ation Token, 2nd Token Needed For Name
;----------------------------------------------
TVAREQU			=	VTOKS+2 ;5Eh

;This Is A Single Token, I Will Have To Read The Name In
;Every Single Time I Execute
;-------------------------------------------
TPROG			=	VTOKS+3 ;5Fh

;User Pict Token, 2nd Token Needed For Name
;-------------------------------------------
TVARPICT		=	VTOKS+4 ;60h

;User GDB Token, 2nd Token Needed For Name
;-------------------------------------------
TVARGDB			=	VTOKS+5 ;61h

;System Vars That Are Used For Output Only
;Cannot Be Stored To By The User
;-------------------------------------------
tVarOut			=	VTOKS+6 ;62h

;System Vars That Are Not In The Symbol Table
;But The User Can Store To Them
;--------------------------------------------
tVarSys			=	VTOKS+7 ;63h


;Mode Setting Commands
;------------------------------------------------------
MODESA			=	VTOKS+8 ;64h
;
tRad			=	MODESA ;64h 'Radian'
tDeg			=	MODESA+1 ; 65h 'Degree'
tNormF			=	MODESA+2 ;66h 'Normal'
tSci			=	MODESA+3 ;67h 'Sci'
tEng			=	MODESA+4 ;68h 'Eng'
tFloat			=	MODESA+5 ;69h 'Float'
;
CMPS			=	6Ah
;
tEQ			=	CMPS ;6Ah '=='
tLT			=	CMPS+1 ;6Bh '<'
tGT			=	CMPS+2 ;6Ch '>'
tLE			=	CMPS+3 ;6Dh LLE
tGE			=	CMPS+4 ;6Eh LGE
tNE			=	CMPS+5 ;6Fh LNE

;Binary OP
;-------------------------------------------
tAdd			=	70h ;70h '+'
tSub			=	71h ;71h '-'
;
TANS			=	72h ;72h

;Mode Setting Commands
;----------------------------------
MODES			=	73h
;
tFix			=	MODES ;73h 'Fix_'
TSPLITON		=	MODES+1 ;74h
TFULLSCREEN		=	MODES+2 ;75h
tStndrd			=	MODES+3 ;76h 'Func'
tParam			=	MODES+4 ;77h 'Param'
tPolar			=	MODES+5 ;78h 'Pol'
TSEQG			=	MODES+6 ;79h
TAFILLON		=	MODES+7 ;7Ah 'AUTO FILL ON
TAFILLOFF		=	MODES+8 ;7Bh
TACALCON		=	MODES+9 ;7Ch
TACALCOFF		=	MODES+10 ;7Dh 'AUTOFILL OFF

;Graph Format Tokens Are 2 Byte Tokens
;--------------------------------------------
TGFORMAT		=	MODES+11 ;7Eh
;
TBOXICON		=	7Fh
TCROSSICON		=	80h
TDOTICON		=	81h

;Binary OP
;---------------------------
tMul			=	82h ;82h '*'
tDiv			=	83h ;83h '/'

;Some Graph Commands
;-------------------------
GRCMDS			=	84h
;
tTrace			=	GRCMDS ;84h 'Trace'
tClDrw			=	GRCMDS+1 ;85h 'ClDrw'
tZoomStd		=	GRCMDS+2 ;86h 'ZStd'
tZoomtrg		=	GRCMDS+3 ;87h 'Ztrg'
tZOOMBOX		=	GRCMDS+4 ;88h 'ZBOX'
tZoomIn			=	GRCMDS+5 ;89h 'ZIn'
tZoomOut		=	GRCMDS+6 ;8Ah 'ZOut'
tZoomSqr		=	GRCMDS+7 ;8Bh 'ZSqr'
tZoomInt		=	GRCMDS+8 ;8Ch 'ZInt'
tZoomPrev		=	GRCMDS+9 ;8Dh 'ZPrev'
tZoomDec		=	GRCMDS+10 ;8Eh 'ZDecm'
TZOOMSTAT		=	GRCMDS+11 ;8Fh 'ZSTAT
tUsrZm			=	GRCMDS+12 ;90h 'ZRcl'
tPrtScrn		=	GRCMDS+13 ;91h 'PrtScrn'
tZOOMSTO		=	GRCMDS+14 ;92h  ZOOM STORE
TTEXT			=	GRCMDS+15 ;93h

;Binary OP
;----------------------------
tnPr			=	GRCMDS+16 ;94h '_nPr_'
tnCr			=	GRCMDS+17 ;95h '_nCr_'

;More Graph Commands
;--------------------------------
tYOn			=	GRCMDS+18 ;96h 'FnOn_'
tYOff			=	GRCMDS+19 ;97h 'FnOff_'
tStPic			=	GRCMDS+20 ;98h 'StPic_'
tRcPic			=	GRCMDS+21 ;99h 'RcPic_'
tStoDB			=	GRCMDS+22 ;9Ah 'StGDB_'
tRclDB			=	GRCMDS+23 ;9Bh 'RcGDB_'
tLine			=	GRCMDS+24 ;9Ch 'Line'
tVert			=	GRCMDS+25 ;9Dh 'Vert_'
tPtOn			=	GRCMDS+26 ;9Eh 'PtOn'
tPtOff			=	GRCMDS+27 ;9Fh 'PtOff'

;Token A0 Cannot Be An EOS Function Since Low MULT=A0 Already
;------------------------------------------------------------
tPtChg			=	GRCMDS+28 ;A0h 'PtChg'
TPXON			=	GRCMDS+29 ;A1h
TPXOFF			=	GRCMDS+30 ;A2h
TPXCHG			=	GRCMDS+31 ;A3h
tShade			=	GRCMDS+32 ;A4h 'Shade'
tCircl			=	GRCMDS+33 ;A5h 'Circl'
tHORZ			=	GRCMDS+34 ;A6h 'HORIZONTAL'
tTanLn			=	GRCMDS+35 ;A7h 'TanLn'
tDrInv			=	GRCMDS+36 ;A8h 'DrInv_'
tDrawF			=	GRCMDS+37 ;A9h 'DrawF_'
;
TVARSTRNG		=	0AAh

;Functions With No Arguments
;------------------------------------------------------
NOARG			=	0ABh
;
tRand			=	NOARG ;ABh 'rand'
tPi			=	NOARG+1 ;ACh  Lpi
tGetKey			=	NOARG+2 ;ADh 'getKy'
;
TAPOST			=	TGETKEY+1 ;Apostrophe
TQUEST			=	TAPOST+1 ;Question Mark

;Unary Functions
;---------------------------------------------------
;Unary: These unary functions take a copmbination of
;        arguments, real, list, matrix
UNARY			=	TQUEST+1 ;B0h
;
tChs			=	UNARY ;B0h
tInt			=	UNARY+1 ;B1h
TABS			=	UNARY+2 ;B2h
tDet			=	UNARY+3 ;B3h
tIdent			=	UNARY+4 ;B4h
tDim			=	UNARY+5 ;B5h
tSum			=	UNARY+6 ;B6h
tProd			=	UNARY+7 ;B7h
tNot			=	UNARY+8 ;B8h
tIPart			=	UNARY+9 ;B9h
tFPart			=	UNARY+10 ;BAh

;New 2 Byte Tokens
;-----------------------------------
T2BYTETOK		=	0BBh

;UNARYLR: Unary functions that only work with
;          real numbers and lists
;----------------------------------------------
UNARYLR =     UNARY+12
;
tSqrt			=	UNARYLR ;BCh
TCUBRT			=	UNARYLR+1 ;BDh
tLn			=	UNARYLR+2 ;BEh
tExp			=	UNARYLR+3 ;BFh
tLog			=	UNARYLR+4 ;C0h
tALog			=	UNARYLR+5 ;C1h
tSin			=	UNARYLR+6 ;C2h
tASin			=	UNARYLR+7 ;C3h
tCos			=	UNARYLR+8 ;C4h
tACos			=	UNARYLR+9 ;C5h
tTan			=	UNARYLR+10 ;C6h
tATan			=	UNARYLR+11 ;C7h
tSinh			=	UNARYLR+12 ;C8h
tASinh			=	UNARYLR+13 ;C9h
tCosh			=	UNARYLR+14 ;CAh
tACosh			=	UNARYLR+15 ;CBh
tTanh			=	UNARYLR+16 ;CCh
tATanh			=	UNARYLR+17 ;CDh

;Some Programming Commands
;--------------------------------------------------------
PROGTOK			=	UNARYLR+18
;
tIf			=	PROGTOK ;CEh
tThen			=	PROGTOK+1 ;CFh
tElse			=	PROGTOK+2 ;D0h
tWhile			=	PROGTOK+3 ;D1h
tRepeat			=	PROGTOK+4 ;D2h
tFor			=	PROGTOK+5 ;D3h
tEnd			=	PROGTOK+6 ;D4h
tReturn			=	PROGTOK+7 ;D5h
tLbl			=	PROGTOK+8 ;D6h 'Lbl_'
tGoto			=	PROGTOK+9 ;D7h 'Goto_'
tPause			=	PROGTOK+10 ;D8h 'Pause_'
tStop			=	PROGTOK+11 ;D9h 'Stop'
tISG			=	PROGTOK+12 ;DAh 'IS>'
tDSL			=	PROGTOK+13 ;DBh 'DS<'
tInput			=	PROGTOK+14 ;DCh 'Input_'
tPrompt			=	PROGTOK+15 ;DDh 'Prompt_'
tDisp			=	PROGTOK+16 ;DEh 'Disp_'
tDispG			=	PROGTOK+17 ;DFh 'DispG'

;Token E0 Cannot Be An EOS Function Since HI MU
;---------------------------------------------
tOutput			=	PROGTOK+18 ;E0h 'Outpt'
tClLCD			=	PROGTOK+19 ;E1h 'ClLCD'
tConst			=	PROGTOK+20 ;E2h 'Fill'
tSortA			=	PROGTOK+21 ;E3h 'sortA_'
tSortD			=	PROGTOK+22 ;E4h 'sortD_'
TDISPTAB		=	PROGTOK+23 ;E5h 'DISP TABLE
tMenu			=	PROGTOK+24 ;E6h 'Menu'
tSENDMBL		=	PROGTOK+25 ;E7h 'SEND'
tGETMBL			=	PROGTOK+26 ;E8h 'GET'

;Stat Plot Commands
;-------------------------
STATPCMD		=	PROGTOK+27
;
TPLOTON			=	STATPCMD ;E9h ' PLOTSON'
TPLOTOFF		=	STATPCMD+1 ;EAh ' PLOTSOFF
;
TLISTNAME		=	0EBh ;list designator

TPLOT1			=	0ECh
TPLOT2			=	0EDh
TPLOT3			=	0EEh

tPower			=	0F0h ;'^'
tXRoot			=	0F1h ;LsupX,Lroot

STATCMD			=	0F2h
;
tOneVar			=	STATCMD ;F2h 'OneVar_'
TTWOVAR			=	STATCMD+1 ;F3h
tLR			=	STATCMD+2 ;F4h 'LinR(A+BX
tLRExp			=	STATCMD+3 ;F5h 'ExpR_'
tLRLn			=	STATCMD+4 ;F6h 'LnR_'
tLRPwr			=	STATCMD+5 ;F7h 'PwrR_'
TMEDMED			=	STATCMD+6 ;F8h
TQUAD			=	STATCMD+7 ;F9h
tCLRLST			=	STATCMD+8 ;FAh 'CLEAR LIST
TCLRTBL			=	STATCMD+9 ;FBH 'CLEAR TABLE
tHist			=	STATCMD+10 ;FCh 'Hist_'
txyLine			=	STATCMD+11 ;FDH 'xyline_'
tScatter		=	STATCMD+12 ;FEh 'Scatter_'
tLR1			=	STATCMD+13 ;FFh 'LINR(AX+B

;2nd Half Of Graph Format Tokens
;--------------------------------------------------
;Format Settings Commands
GFMT			=	0
;
tSeq			=	GFMT ;'SeqG'
tSimulG			=	GFMT+1 ;'SimulG'
tPolarG			=	GFMT+2 ;'PolarGC'
tRectG			=	GFMT+3 ;'RectGC'
tCoordOn		=	GFMT+4 ;'CoordOn'
tCoordOff		=	GFMT+5 ;'CoordOff'
tDrawLine		=	GFMT+6 ;'DrawLine'
tDrawDot		=	GFMT+7 ;'DrawDot'
tAxisOn			=	GFMT+8 ;'AxesOn'
tAxisOff		=	GFMT+9 ;'AxesOff'
tGridOn			=	GFMT+10 ;'GridOn'
tGridOff		=	GFMT+11 ;'GridOff'
tLblOn			=	GFMT+12 ;'LabelOn'
tLblOff			=	GFMT+13 ;'LabelOff'
tWebOn			=	GFMT+14 ;'WebOn'
tWebOff			=	GFMT+15 ;'WebOFF'
TUV			=	GFMT+16 ;U vs V
TVW			=	GFMT+17 ;V vs W
TUW			=	GFMT+18 ;U vs W

;2nd Half Of User Matrix Tokens
;-----------------------------------------
TMATA			=	00h ;MAT A
TMATB			=	01h ;MAT B
TMATC			=	02h ;MAT C
TMATD			=	03h ;MAT D
TMATE			=	04h ;MAT E
TMATF			=	05h ;MAT F
TMATG			=	06h ;MAT G
TMATH			=	07h ;MAT H
TMATI			=	08h ;MAT I
TMATJ			=	09h ;MAT J

;2nd Half Of User List Tokens
;------------------------------------------
TL1			=	00h ;List 1
TL2			=	01h ;List 2
TL3			=	02h ;List 3
TL4			=	03h ;List 4
TL5			=	04h ;List 5
TL6			=	05h ;List 6

;2nd Half Of User =ation Tokens
;--------------------------------------
;"Y" =ations Have Bit 4 Set
TY1			=	10h ;Y1
TY2			=	11h ;Y2
TY3			=	12h ;Y3
TY4			=	13h ;Y4
TY5			=	14h ;Y5
TY6			=	15h ;Y6
TY7			=	16h ;Y7
TY8			=	17h ;Y8
TY9			=	18h ;Y9
TY0			=	19h ;Y0

;Param =ations Have Bit 5 Set
;---------------------------------------
TX1T			=	20h ;X1t
TY1T			=	21h ;Y1t
TX2T			=	22h ;X2t
TY2T			=	23h ;Y2t
TX3T			=	24h ;X3t
TY3T			=	25h ;Y3t
TX4T			=	26h ;X4t
TY4T			=	27h ;Y4t
TX5T			=	28h ;X5t
TY5T			=	29h ;Y5t
TX6T			=	2Ah ;X6t
TY6T			=	2Bh ;Y6t

;Polar =ations Have Bit 6 Set
;--------------------------------------
TR1			=	40h ;R1
TR2			=	41h ;R2
TR3			=	42h ;R3
TR4			=	43h ;R4
TR5			=	44h ;R5
TR6			=	45h ;R6

;Recursion =ations Have Bit 7 Set
;--------------------------------------
TUN			=	80h ;Un
TVN			=	81h ;Vn
TWN			=	82h ;Wn

;2nd Half Of User Picture Tokens
;----------------------------------------
TPIC1			=	00h ;PIC1
TPIC2			=	01h ;PIC2
TPIC3			=	02h ;PIC3
TPIC4			=	03h ;PIC4
TPIC5			=	04h ;PIC5
TPIC6			=	05h ;PIC6
TPIC7			=	06h ;PIC7
TPIC8			=	07h ;PIC8
TPIC9			=	08h ;PIC9
TPIC0			=	09h ;PIC0

;2nd Half Of User Graph Database Tokens
;----------------------------------------
TGDB1			=	00h ;GDB1
TGDB2			=	01h ;GDB2
TGDB3			=	02h ;GDB3
TGDB4			=	03h ;GDB4
TGDB5			=	04h ;GDB5
TGDB6			=	05h ;GDB6
TGDB7			=	06h ;GDB7
TGDB8			=	07h ;GDB8
TGDB9			=	08h ;GDB9
TGDB0			=	09h ;GDB0

;2nd Half Of String Vars
;----------------------------------
TSTR1			=	00h
TSTR2			=	01h
TSTR3			=	02h
TSTR4			=	03h
TSTR5			=	04h
TSTR6			=	05h
TSTR7			=	06h
TSTR8			=	07h
TSTR9			=	08h
TSTR0			=	09h

;2nd Half Of System Output Only Variables
;--------------------------------------------------------------------
;OPEN			=	00h
;
TREGEQ			=	01h ;REGRESSION =ATION
;
TSTATN			=	02h ;STATISTICS N
;
TXMEAN			=	03h ;X MEAN
TSUMX			=	04h ;SUM(X)
TSUMXSQR		=	05h ;SUM(X^2)
TSTDX			=	06h ;STANDARD DEV X
TSTDPX			=	07h ;STANDARD DEV POP X
TMINX			=	08h ;MIN X VALUE
TMAXX			=	09h ;MAX X VALUE
;
TMINY			=	0Ah ;MIN Y VALUE
TMAXY			=	0Bh ;MAX Y VALUE
TYMEAN			=	0Ch ;Y MEAN
TSUMY			=	0Dh ;SUM(Y)
TSUMYSQR		=	0Eh ;SUM(Y^2)
TSTDY			=	0Fh ;STANDARD DEV Y
TSTDPY			=	10h ;STANDARD DEV POP Y
;
TSUMXY			=	11h ;SUM(XY)
TCORR			=	12h ;CORRELATION
TMEDX			=	13h ;MED(X)
TQ1			=	14h ;1ST QUADRANT OF X
TQ3			=	15h ;3RD QUADRANT OF X
TQUADa			=	16h ;1ST TERM OF QUAD POLY REG/ Y-INT
TQUADb			=	17h ;2ND TERM OF QUAD POLY REG/ SLOPE
TQUADc			=	18h ;3RD TERM OF QUAD POLY REG
TCUBEd			=	19h ;4TH TERM OF CUBIC POLY REG
TQUARTe			=	1Ah ;5TH TERM OF QUART POLY REG
TMEDX1			=	1Bh ;x1 FOR MED-MED
TMEDX2			=	1Ch ;x2 FOR MED-MED
TMEDX3			=	1Dh ;x3 FOR MED-MED
TMEDY1			=	1Eh ;y1 FOR MED-MED
TMEDY2			=	1Fh ;y2 FOR MED-MED
TMEDY3			=	20h ;y3 FOR MED-MED
;
TRECURN			=	21h ;RECURSION N
TSTATP			=	22h
TSTATZ			=	23h
TSTATT			=	24h
TSTATCHI		=	25h
TSTATF			=	26h
TSTATDF			=	27h
TSTATPHAT		=	28h
TSTATPHAT1		=	29h
TSTATPHAT2		=	2Ah
TSTATMEANX1		=	2Bh
TSTATSTDX1		=	2Ch
TSTATN1			=	2Dh
TSTATMEANX2		=	2Eh
TSTATSTDX2		=	2Fh
TSTATN2			=	30h
TSTATSTDXP		=	31h
TSTATLOWER		=	32h
TSTATUPPER		=	33h
TSTAT_S			=	34h
TLRSQR			=	35h ;r^2
TBRSQR			=	36h ;R^2

;These next tokens are only used to acess the data.
;They are display only and the user cannot access them at all.
;-------------------------------------------------------------
TF_DF			=	37h ;ANOFAV FACTOR DF
TF_SS			=	38h ;ANOFAV FACTOR SS
TF_MS			=	39h ;ANOFAV FACTOR MS
TE_DF			=	3Ah ;ANOFAV ERROR DF
TE_SS			=	3Bh ;ANOFAV ERROR SS
TE_MS			=	3Ch ;ANOFAV ERROR MS

;2nd Half Of System Input/Output Variables
;----------------------------------------------------
;System Variable =ates
;
;Vars With No Restriction
TUXSCL			=	0
TUYSCL			=	1
TXSCL			=	2
TYSCL			=	3
TRECURU0		=	4 ;U 1ST INITIAL COND
TRECURV0		=	5 ;V 1ST INITIAL COND
TUN1			=	6 ;U(N-1) ;NOT USED
TVN1			=	7 ;V(N-1) ;NOT USED
TURECURU0		=	8
TURECURV0		=	9

;Vars Rounded To 10 Digits
;----------------------------------
TXMIN			=	0Ah
TXMAX			=	0Bh
TYMIN			=	0Ch
TYMAX			=	0Dh
TTMIN			=	0Eh
TTMAX			=	0Fh
TTHETAMIN		=	10h
TTHETAMAX		=	11h
TUXMIN			=	12h
TUXMAX			=	13h
TUYMIN			=	14h
TUYMAX			=	15h
TUTHETMIN		=	16h
TUTHETMAX		=	17h
TUTMIN			=	18h
TUTMAX			=	19h
TTBLMIN			=	1Ah
TPLOTSTART		=	1Bh
TUPLOTSTART		=	1Ch
TNMAX			=	1Dh
TUNMAX			=	1Eh
TNMIN			=	1Fh
TUNMIN			=	20h

;Vars Cannot Be 0
;----------------------------------
TTBLSTEP		=	21h
TTSTEP			=	22h
TTHETASTEP		=	23h
TUTSTEP			=	24h
TUTHETSTEP		=	25h

;Special Limits On These
;----------------------------------
TDELTAX			=	26h
TDELTAY			=	27h

;Special Limits On These
;----------------------------------
TXFACT			=	28h
TYFACT			=	29h

TTBLINPUT		=	2Ah

;Any Restrictions?
;-----------------------------------------
TFINn			=	2Bh
TFINi			=	2Ch
TFINpv			=	2Dh
TFINpmt			=	2Eh
TFINfv			=	2Fh
TFINpy			=	30h
TFINcy			=	31h
;
TRECURW0		=	32h ;W0(1)
TURECURW0		=	33h
;
TPLOTSTEP		=	34h
TUPLOTSTEP		=	35h
;
TXRES			=	36h
TUXRES			=	37h

;All The Tokens Below Do Not Have Strings Associated.
;They Are Here Just For Storing And Recalling.
;----------------------------------------------------
TRECURU02		=	38h ;U0(2)
TURECURU02		=	39h
TRECURV02		=	3Ch ;V0(2)
TURECURV02		=	3Dh
TRECURW02		=	3Eh ;W0(2)
TURECURW02		=	3Fh

;No Restrictions
;------------------------------------------
;TWN1			=	40h ;W(N-1)
;UN2			=	41h ;U(N-2)
;VN2			=	42h ;V(N-2)
;WN2			=	43h ;W(N-2)

;Don't Know If Need These Yet?
;-----------------------------
;TUI12
;TUI22
;TUUI12
;TUUI22

TFINnpv			=	00h ;immediate unary
TFINirr			=	01h ;immediate unary
TFINbal			=	02h ;immediate unary
TFINprn			=	03h ;immediate unary
TFINint			=	04h ;immediate unary
TFINtonom		=	05h ;immediate unary
TFINtoeff		=	06h ;immediate unary
TFINdbd			=	07h ;immediate unary
TLCM			=	08h ;immediate unary
TGCD			=	09h ;immediate unary
TRANDINT		=	0Ah ;immediate unary
TRANDBIN		=	0Bh ;immediate unary
TSUBSTRNG		=	0Ch ;immediate unary
TSTDDEV			=	0Dh ;immediate unary
TVARIANCE		=	0Eh ;immediate unary
TINSTRNG		=	0Fh ;immediate unary
TDNORMAL		=	10h ;immediate unary
TINVNORM		=	11h ;immediate unary
TDT			=	12h ;immediate unary
TCHI			=	13h ;immediate unary
TDF			=	14h ;immediate unary
TBINPDF			=	15h ;immediate unary
TBINCDF			=	16h ;immediate unary
TPOIPDF			=	17h ;immediate unary
TPOICDF			=	18h ;immediate unary
TGEOPDF			=	19h ;immediate unary
TGEOCDF			=	1Ah ;immediate unary
TNORMALPDF		=	1Bh ;immediate unary
TTPDF			=	1Ch ;immediate unary
TCHIPDF			=	1Dh ;immediate unary
TFPDF			=	1Eh ;immediate unary
;TRANDBIN		=	1Fh ;immediate unary
TRANDNORM		=	1Fh ;immediate unary
TFINFpmt		=	20h ;0 or multy arg function
TFINFi			=	21h ;0 or multy arg function
TFINFpv			=	22h ;0 or multy arg function
TFINFn			=	23h ;0 or multy arg function
TFINFfv			=	24h ;0 or multy arg function
TCONJ			=	25h ;unary
TREAL			=	26h ;unary
TIMAG			=	27h ;unary
TANGLE			=	28h ;unary
TCUMSUM			=	29h ;unary
TEXPR			=	2Ah ;unary
TLENGTH			=	2Bh ;unary
TDELTALST		=	2Ch ;unary
TREF			=	2Dh ;unary
TRREF			=	2Eh ;unary
TTORECT			=	2Fh ;display conversion
TTOPOLAR		=	30h ;display conversion
TCONSTE			=	31h ;function no arg
TSINREG			=	32h ;command opt push
TLOGISTIC		=	33h ;command opt push
TLINREGTTEST		=	34h ;command opt push
TSHADENORM		=	35h ;command pushed
TSHADET			=	36h ;command pushed
TSHADECHI		=	37h ;command pushed
TSHADEF			=	38h ;command pushed
TMATTOLST		=	39h ;command pushed
TLSTTOMAT		=	3Ah ;command pushed
TZTEST			=	3Bh ;command pushed
TTTEST			=	3Ch ;command pushed
T2SAMPZTEST		=	3Dh ;command pushed
T1PROPZTEST		=	3Eh ;command pushed
T2PROPZTEST		=	3Fh ;command pushed
TCHITEST		=	40h ;command pushed
TZINTVAL		=	41h ;command pushed
T2SAMPZINT		=	42h ;command pushed
T1PROPZINT		=	43h ;command pushed
T2PROPZINT		=	44h ;command pushed
TGRAPHSTYLE		=	45h ;command pushed
T2SAMPTTEST		=	46h ;command opt pushed
T2SAMPFTEST		=	47h ;command opt pushed
TTINTVAL		=	48h ;command opt pushed
T2SAMPTINT		=	49h ;command opt pushed
TSETUPLST		=	4Ah ;command opt pushed
TFINpmtend		=	4Bh ;mode command
TFINpmtbeg		=	4Ch ;mode command
TREALM			=	4Dh ;mode command
TPOLARM			=	4Eh ;mode command
TRECTM			=	4Fh ;mode command
TEXPRON			=	50h ;mode command
TEXPROFF		=	51h ;mode command
TCLRALLLST		=	52h ;command not pushed
TGETCALC		=	53h ;command not pushed
TDELVAR			=	54h ;command not pushed
TEQUTOSTRNG		=	55h ;command not pushed
TSTRNGTOEQU		=	56h ;command not pushed
tdellast		=	57h ;command not pushed
TSELECT			=	58h ;command not pushed
TANOVA			=	59h ;command not pushed
TMODBOX			=	5Ah ;icon
TNORMPROB		=	5Bh ;icon

;Tokens 5Ch to 63h cannot be the 2nd Byte of
; two byte tokens. These are already the 1st byte value
; of two byte tokens. Reading tokens backwards Will not
; work if this restriction is not in place.
;------------------------------------------------------
;NO USE			=	5Ch
;NO USE			=	5Dh
;NO USE			=	5Eh
;NO USE			=	5Fh
;NO USE			=	60h
;NO USE			=	61h
;NO USE			=	62h
;NO USE			=	63h

TMGT			=	64h ;VERTICAL SPLIT
TZFIT			=	65h ;ZOOM FIT
TDIAG_ON		=	66h ;DIANOSTIC DISPLAY ON
TDIAG_OFF		=	67h ;DIANOSTIC DISPLAY OFF

;
;======================================================================
;           Scan Code Equates
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
skYEqu          =	35h
sk2nd           =	36h
skMode          =	37h
skDel           =	38h