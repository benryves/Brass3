;======================================================================
; Include File for the TI-73
; Last Updated 8/14/2002 
;
; Copyright (c) 2002 Texas Instruments: The Licensed Materials are
; copyrighted by TI. LICENSEE agrees that it will 
; not delete the copyright notice, trademarks or
; protective notices from any copy made by LICENSEE.
;
; Warranty: TI does not warrant that the Licensed Materials will
; be free from errors or will meet your specific requirements.
; The Licensed Materials are made available "AS IS" to LICENSEE.
;
; Limitations: TI MAKES NO WARRANTY OR CONDITION, EITHER EXPRESS
; OR IMPLIED, INCLUDING BUT NOT LIMITED TO ANY IMPLIED
; WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE,
; REGARDING THE LICENSED MATERIALS.  IN NO EVENT SHALL
; TI OR ITS SUPPLIERS BE LIABLE FOR ANY INDIRECT, INCIDENTAL
; OR CONSEQUENTIAL DAMAGES, LOSS OF PROFITS, LOSS OF USE OR DATA, 
; OR INTERRUPTION OF BUSINESS, WHETHER THE ALLEGED DAMAGES ARE
; LABELED IN TORT, CONTRACT OR INDEMNITY.
;
;========================================================================


;SYMBOLIC_DEBUG_START
;=================================
;entry points
;=================================
_AbsO1O2Cp		= 40f0h
_AbsO1PAbsO2		= 405ah
_ACos		= 40d2h
_ACosRad		= 40c6h
_AdrLEle		= 4ab6h
_AdrMRow		= 4567h
_AdrMEle		= 456ah
_AllEq		= 4792h
_AllocFPS		= 4369h
_AllocFPS1		= 436ch
_AnsName		= 4B67h
_ApdSetup		= 4c06h
_AppGetCbl		= 4b97h
_AppGetCalc		= 4b9ah
_AppInit		= 4048h
_ASin		= 40d8h
_ASinRad		= 40cfh
_ATanRad		= 40c9h
_ATan2Rad		= 40cch
_ATan		= 40d5h
_ATan2		= 40dbh

_BinOPExec		= 459ah
_BufClear		= 4840h

_CanAlphIns		= 4ab0h
_ChkFindSym		= 42d9h
_CircCmd		= 46f6h
_CkInt		= 423ah
_CkOdd		= 423dh
_CkOP1C0		= 422bh
_CkOP1FP0		= 422eh
_CkOP1Pos		= 4252h
_CkOP1Real		= 40e7h
_CkOP2FP0		= 4231h
_CkOP2Pos		= 424fh
_CkOP2Real		= 42cah
_CkPosInt		= 4237h
_CkValidNum		= 426ah
_CleanAll		= 4927h
_ClearRect		= 4cfch
_ClearRow		= 4c8dh
_CLine		= 46cch
_CLineS		= 46cfh
_CloseEditBuf	= 47E9h
_CloseEditBufNoR		= 46a2h
_CloseEditEqu	= 4867h
_CloseProg		= 490ch
_ClrGraphRef		= 490fh
_ClrLCD		= 44aah
_ClrLCDFull		= 44a7h
_ClrLp		= 41E3h
_ClrOP1S		= 4258h
_ClrOP2S		= 4255h
_ClrScrn		= 44b0h
_ClrScrnFull		= 44adh
_ClrTxtShd		= 44b3h
_CmpSyms		= 4921h
_ConvDim		= 4B58h
_ConvDim00		= 4B5Bh
_ConvOP1		= 49abh
_Cos		= 40c0h
_CpHLDE		= 4003h
_CPoint		= 4d89h
_CPointS		= 4717h
_CpOP1OP2		= 40f3h
_CpOP4OP3		= 40eah
_CpyO1toFPS1		= 4411h
_CpyO1toFPS2		= 4420h
_CpyO1toFPS3		= 442ch
_CpyO1toFPS4		= 443eh
_CpyO1toFPS5		= 4438h
_CpyO1toFPS6		= 4432h
_CpyO1toFPS7		= 4435h
_CpyO1toFPST		= 43ffh
_CpyO2toFPS1		= 440eh
_CpyO2toFPS2		= 4417h
_CpyO2toFPS3		= 4429h
_CpyO2toFPS4		= 443bh
_CpyO2toFPST		= 43f9h
_CpyO3toFPS1		= 4408h
_CpyO3toFPS2		= 441ah
_CpyO3toFPST		= 43f6h
_CpyO5toFPS1		= 440bh
_CpyO5toFPS3		= 4426h
_CpyO6toFPS2		= 441dh
_CpyO6toFPST		= 43fch
_CpyStack		= 43deh
_Cpyto1FPS1		= 43e7h
_Cpyto1FPS10		= 43a8h
_Cpyto1FPS11		= 438dh
_Cpyto1FPS2		= 43f0h
_Cpyto1FPS3		= 43bdh
_Cpyto1FPS4		= 43c3h
_Cpyto1FPS5		= 4393h
_Cpyto1FPS6		= 4399h
_Cpyto1FPS7		= 439fh
_Cpyto1FPS8		= 43a2h
_Cpyto1FPS9		= 43abh
_Cpyto1FPST		= 43d8h
_CpyTo2FPS1		= 43e4h
_CpyTo2FPS2		= 43edh
_CpyTo2FPS3		= 43b7h
_CpyTo2FPS4		= 43aeh
_CpyTo2FPS5		= 4390h
_CpyTo2FPS6		= 4396h
_CpyTo2FPS7		= 439ch
_CpyTo2FPS8		= 43a5h
_CpyTo2FPST		= 43D5h
_CpyTo3FPS1		= 43e1h
_CpyTo3FPS2		= 43c6h
_CpyTo3FPST		= 43d2h
_CpyTo4FPST		= 43cfh
_CpyTo5FPST		= 43c9h
_CpyTo6FPST		= 43cch
_CpyTo6FPS3		= 43b1h
_CpyTo6FPS2		= 43b4h
_CpyToFPS1		= 4414h
_CpyToFPS2		= 4423h
_CpyToFPS3		= 442fh
_CpyToFPST		= 4402h
_CpyToStack		= 4405h
_Create0Equ		= 4303h
_CreateAppVar		= 4312h
_CreateEqu		= 4309h
_CreatePict		= 430ch
_CreateProg		= 430fh
_CreateReal		= 42f4h
_CreateRList	= 42fah
_CreateStrng	= 4300h
_Cube  		= 407bh
_CursorOff		= 4522h
_CursorOn		= 4528h

_DarkLine		= 46ffh
_DarkPnt		= 4714h
_DataSize		= 433ch
_DataSizeA		= 4339h
_DeallocFPS		= 4363h
_DeallocFPS1		= 4366h
_DecO1Exp		= 4261h
_DelListEl		= 4906h
_DelMem		= 4330h
_DelRes		= 48fah
_DelVar		= 432ah
_DisableApd		= 4be8h
_DisableAPI		= 4bdfh
_DispDone		= 4519h
_DispEOL		= 450ah
_DispHL		= 446eh
_DisplayImage		= 4d5ch
_DispOP1A		= 4AFBh
_DivHLBy10		= 4006h
_DivHLByA		= 4009h
_DrawCirc2		= 4aadh
_DrawCmd		= 47ddh
_DrawRectBorder		= 4d2ch
_DrawRectBorderClear		= 4d3bh
_DToR		= 4075h

_EditProg		= 4909h
_EnableApd		= 4bebh
_EnoughMem		= 42e5h
_EOP1NotReal		= 4273h
_Equ_or_NewEqu		= 42afh
_EraseEOL		= 44b9h
_EraseRectBorder		= 4d35h
_ErrArgument		= 49ffh
_ErrBadGuess		= 4a2fh
_ErrBreak		= 4a20h
_ErrD_OP1_0		= 42beh
_ErrD_OP1_LE_0		= 42bbh
_ErrD_OP1Not_R		= 42b5h
_ErrD_OP1NotPos		= 42b2h
_ErrD_OP1NotPosInt		= 42b8h
_ErrDataType		= 49f9h
_ErrDimMismatch		= 4a02h
_ErrDimension		= 4a05h
_ErrDivBy0		= 49e4h
_ErrDomain		= 49e7h
_ErrIncrement		= 49eah
_ErrInvalid		= 4a11h
_ErrIterations		= 4a2ch
_ErrLinkXmit		= 4a38h
_ErrMemory		= 4a0bh
_ErrNon_Real		= 49edh
_ErrNonReal		= 495ah
_ErrNotEnoughMem		= 4441h
_ErrOverflow		= 49e1h
_ErrSignChange		= 4a29h
_ErrStat		= 4a23h
_ErrStatPlot		= 4a35h
_ErrSyntax		= 49f0h
_ErrTolTooSmall		= 4a32h
_ErrUndefined		= 4a08h
_EToX		= 40b4h
_ExLp		= 4228h
_ExpToHex		= 4249h

_FillRect		= 4d02h
_FillRectPattern		= 4d38h
_Find_Parse_Formula		= 49b1h
_FindAlphaDn		= 491eh
_FindAlphaUp		= 491bh
_FindSym		= 42dch
_FiveExec		= 45b5h
_FixTempCnt		= 4912h
_FormEReal		= 4885h
_FormReal		= 488Bh
_FourExec		= 45b2h
_FPAdd		= 4072h
_FPDiv		= 4099h
_FPMult		= 4084h
_FPRecip		= 4096h
_FPSquare		= 4081h
_FPSub		= 406fh
_Frac		= 4093h

_Get_Tok_Strng		= 44F8h
_GetBaseVer		= 4b8eh
_GetCSC		= 400fh
_GetKey		= 486dh
_GetLToOP1		= 4573h
_GetTokLen		= 44f5h
_GrBufClr         = 4AD4h
_GrBufCpy		= 4789h
_GrphCirc		= 46f9h

_HLTimes9		= 40e4h
_HorizCmd		= 47c5h
_HTimesL		= 4270h

_IBounds		= 4aa4h
_IBoundsFull		= 4d4dh
_ILine		= 4702h
_IncLstSize		= 4900h
_InsertList		= 4903h
_InsertMem		= 42dfh
_Int		= 40a5h
_Intgr		= 405dh
_InvCmd		= 47e3h
_InvertRect		= 4cffh
_InvOP1S		= 408dh
_InvOP1SC		= 408ah
_InvOP2S		= 4090h
_InvSub		= 4063h
_IOffset		= 4aa7h
_IPoint		= 4705h
_IsA2ByteTok	= 4291h
_IsEditEmpty		= 483Ah

_JError		= 4a4dh
_JErrorNo		= 4c03h
_JForceCmdNoChar		= 401eh

_KeyToString		= 452eh

_LCD_BUSY		= 4051h
_LCD_DRIVERON		= 4873h
_LdHLind		= 4000h
_LineCmd		= 47cbh
_LnX		= 40abh
_Load_SFont		= 46b7h
_LoadPattern		= 4c51h
_LogX		= 40aeh

_Max		= 4057h
_MemChk		= 42cdh
_MemClear		= 4a47h
_MemSet		= 4a4ah
_Min		= 4054h
_Minus1		= 406ch
_Mov10B		= 413eh
_Mov18B		= 46fch
_Mov9B		= 4141h
_Mov7B		= 414ah
_Mov8B		= 4147h
_Mov9ToOP1		= 416bh
_Mov9OP1OP2		= 416eh
_Mov9ToOP2		= 4171h
_MovFrOP1  = 4174h

_NewLine		= 4495h

_OP1ExOP2		= 4225h
_OP1ExOP3		= 421fh
_OP1ExOP4		= 4222h
_OP1ExOP5		= 4219h
_OP1ExOP6		= 421ch
_OP1ExpToDec		= 424ch
_OP1Set0		= 41d1h
_OP1Set1		= 4192h
_OP1Set2		= 419eh
_OP1Set3		= 4198h
_OP1Set4		= 4195h
_OP1ToOP2		= 4111h
_OP1ToOP3		= 4105h
_OP1ToOP4		= 40f9h
_OP1ToOP5		= 4135h
_OP1ToOP6		= 4132h
_OP2Set0		= 41ceh
_OP2Set1		= 41aah
_OP2Set2		= 41a1h
_OP2Set3		= 418fh
_OP2Set4		= 418ch
_OP2Set5		= 4186h
_OP2Set60		= 497bh
_OP2Set8		= 4183h
_OP2SetA		= 4189h
_OP2ToOP1		= 4138h
_OP2ToOP3		= 415fh
_OP2ToOP4		= 40fch
_OP2ToOP5		= 412ch
_OP2ToOP6		= 412fh
_OP3Set0		= 41cbh
_OP3Set1		= 4180h
_OP3Set2		= 419bh
_OP3ToOP1		= 4120h
_OP3ToOP2		= 4102h
_OP3ToOP4		= 40f6h
_OP3ToOP5		= 4129h
_OP4Set0		= 41c8h
_OP4Set1		= 417dh
_OP4ToOP1		= 411ah
_OP4ToOP2		= 40ffh
_OP4ToOP3		= 4162h
_OP4ToOP5		= 4126h
_OP4ToOP6		= 4168h
_OP5Set0		= 41c5h
_OP5ToOP1		= 411dh
_OP5ToOP2		= 4108h
_OP5ToOP3		= 4165h
_OP5ToOP4		= 410eh
_OP5ToOP6		= 410bh
_OP6ToOP1		= 4117h
_OP6ToOP2		= 4114h
_OP6ToOP5		= 4123h

_ParseInp		= 4966h
_PDspGrph		= 47c2h
_PixelTest		= 47d4h
_Plus1		= 4069h
_PointCmd		= 47d1h
_PointOn		= 4a56h
_PopOP1		= 4345h
_PopOP3		= 4342h
_PopOP5		= 433fh
_PopReal		= 435ah
_PopRealO1		= 4357h
_PopRealO2		= 4354h
_PopRealO3		= 4351h
_PopRealO4		= 434eh
_PopRealO5		= 434bh
_PopRealO6		= 4348h
_PosNo0Int		= 4234h
_PToR		= 40deh
_PushOP1		= 438ah
_PushOP3		= 4387h
_PushOP5		= 4384h
_PushReal		= 4381h
_PushRealO1		= 437eh
_PushRealO2		= 437bh
_PushRealO3		= 4378h
_PushRealO4		= 4375h
_PushRealO5		= 4372h
_PushRealO6		= 436fh
_PutAway		= 4039h
_PutC		= 446bh
_PutMap		= 4468h
_PutPS		= 4477h
_PutPSB		= 4474h
_PutS		= 4471h
_PutTokString		= 485eh
_PutToL		= 4582h

_RandInit		= 4c21h
_Random		= 4b9dh
_Rcl_StatVar		= 42c7h
_RclAns		= 4996h
_RclGDB2		= 46f3h
_RclSysTok		= 49a2h
_RclVarSym		= 499fh
_RclX		= 499ch
_RclY		= 4999h
_RedimMat		= 48fdh
_Regraph		= 47aah
_ReleaseBuffer		= 46A5h
_ReloadAppEntryVecs		= 4a50h
_RestoreDisp		= 478ch
_RndGuard		= 409fh
_RnFx		= 40a2h
_Round		= 40a8h
_RToD		= 4078h
_RToP		= 40e1h
_RunIndicOff		= 44d7h
_RunIndicOn		= 44d4h

_SaveDisp		= 4ba3h
_SendVarCmd		= 48F1h
_SetFuncM		= 4762h
_SetXXOP1		= 46c0h
_SetXXOP2		= 46c3h
_SetXXXXOP2		= 46c6h
_SFont_Len		= 46bah
_ShRAcc		= 41E6h
_Sin		= 40bdh
_SinCosRad		= 40bah
_SqRoot		= 409ch
_SrchVLstDn		= 4c36h
_SrchVLstUp		= 4c33h
_SStringLength		= 4c54h
_StMatEl		= 49a5h
_StoAns		= 498ah
_StoGDB2		= 46f0h
_StoOther		= 4993h
_StoRand		= 4e5eh
_StoSysTok		= 4987h
_StoX		= 4990h
_StoY		= 498dh
_StrCopy		= 444ah
_StrLength		= 4a5ch

_Tan		= 40c3h
_TenX		= 40b7h
_ThreeExec		= 45ach
_Times2		= 4066h
_TimesPt5		= 407eh
_ToFrac		= 4594h
_Trunc		= 4060h

_UCLines		= 46c9h
_UnLineCmd		= 47ceh
_UnOpExec		= 45a9h

_VertCmd		= 47c8h
_VPutMap		= 44c5h
_VPutS		= 44c8h
_VPutSN		= 44cbh
_VtoWHLDE		= 471dh

_XftoI		= 4726h
_XitoF		= 4720h
_XName		= 4279h
_XRootY		= 46d2h

_YftoI		= 4723h
_YName		= 427ch
_YtoX		= 46d5h

_Zero16D		= 41b9h
_ZeroOP		= 41e0h
_ZeroOP1		= 41d7h
_ZeroOP2		= 41dah
_ZeroOP3		= 41ddh
_ZmDecml		= 476eh
_ZmFit		= 477ah
_ZmInt		= 476bh
_ZmPrev		= 4771h
_ZmSquare		= 477dh
_ZmStats		= 46d8h
_ZmTrig		= 4780h
_ZmUsr		= 4774h
_ZooDefault		= 4786h

;======================================================================

;           system-defined ram variable address equates

;======================================================================

;


ramStart             =  8000h
appData              =  8000h
ramCode              =  8100h
ramCodeEnd           =  822fh
baseAppBrTab         =  8230h
bootTemp             =  8241h
appSearchPage        =  8293h
tempSwapArea         =  8295h
appID                =  837dh
ramReturnData        =  8529h
appInfo              =  8998h
appBank_jump         =  85c9h
appPage              =  85cbh
kbdScanCode          =  83d8h
kbdKey               =  83ddh
kbdGetKy             =  83deh
keyExtend            =  83dfh
contrast             =  83e0h
apdSubTimer          =  84e0h
apdTimer             =  84e1h
curTime              =  83e1h
curRow               =  83e2h
curCol               =  83e3h
curOffset            =  83e4h
curUnder             =  83e5h
curY                 =  8498h
curType              =  8499h
curXRow              =  849ah
prevDData            =  849bh
lFont_record         =  84a3h
sFont_record         =  84abh
tokVarPtr            =  85a1h
tokLen               =  85a3h
indicMem             =  84d6h
indicCounter         =  84deh
indicBusy            =  84dfh
OP1                  =  842bh
OP1M                 =  842dh
OP2                  =  8436h
OP2M                 =  8485h
OP2EXT               =  843fh
OP3                  =  8441h
OP3M                 =  8443h
OP4                  =  844ch
OP4M                 =  844eh
OP5                  =  8457h
OP5M                 =  8459h
OP6                  =  8462h
OP6M                 =  8464h
OP6EXT               =  846bh
progToEdit           =  8472h
nameBuff             =  847ah
equ_edit_save        =  8485h
iMathPtr1            =  8486h
iMathPtr2            =  8488h
iMathPtr3            =  848ah
iMathPtr4            =  848ch
iMathPtr5            =  848eh
chkDelPtr1           =  8490h
chkDelPtr2           =  8492h
chkDelPtr3           =  9baah
chkDelPtr4           =  9bach
insDelPtr            =  8494h
upDownPtr            =  8496h
fOutDat              =  85a5h
asm_data_ptr1        =  85afh
asm_data_ptr2        =  85b1h
asm_sym_ptr1         =  85b3h
asm_sym_ptr2         =  85b5h
asm_ind_call         =  85b7h
textShadow           =  8913h
textShadCur          =  8993h
textShadTop          =  8995h
textShadAlph         =  8996h
textShadIns          =  8997h
cxMain               =  84e2h
cxPPutAway           =  84e4h
cxPutAway            =  84e6h
cxRedisp             =  84e8h
cxErrorEP            =  84eah
cxSizeWind           =  84ech
cxPage               =  84eeh
cxCurApp             =  84efh
cxPrev               =  84f0h
monQH                =  850eh
monQT                =  850fh
monQueue             =  8510h
onSP                 =  8520h
promptRow            =  89a2h
promptCol            =  89a3h
promptIns            =  89a4h
promptShift          =  89a5h
promptRet            =  89a6h
promptValid          =  89a8h
promptTop            =  89aah
promptCursor         =  89ach
promptTail           =  89aeh
promptBtm            =  89b0h
varType              =  89b2h
varCurrent           =  89b3h
varClass             =  89bbh
menuActive           =  84b3h
menuAppDepth         =  84b4h
MenuCurrent          =  84b5h
ProgCurrent          =  84c1h
userMenuSA           =  89e1h
ioPrompt             =  8a41h
dImageWidth          =  8a42h
RectFillPHeight		 =  8A42h
RectFillPWidth		 =  8a43h
RectFillPattern		 =  8a44h
ioFlag               =  83e6h
sndRecState          =  83e8h
ioErrState           =  83e9h
header               =  83eah
ioData               =  83f3h
bakHeader            =  83ffh
penCol               =  89dfh
penRow               =  89e0h
rclQueue             =  84d2h
rclQueueEnd          =  84d4h
errNo                =  854ah
errSP                =  854bh
errOffset            =  854dh
saveSScreen          =  8a52h
bstCounter           =  8559h
flags                =  855bh
statVars             =  8d58h
curGStyle            =  8e94h
curGY                =  8e95h
curGX                =  8e96h
curGY2               =  8e97h
curGX2               =  8e98h
freeSaveY            =  8e99h
freeSaveX            =  8e9ah
XOffset              =  8ec8h
YOffset              =  8ec9h
lcdTallP             =  8ecah
pixWideP             =  8ecbh
pixWide_m_1          =  8ecch
pixWide_m_2          =  8ecdh
lastEntryPTR         =  8eceh
lastEntryStk         =  8ed0h
numLastEntries       =  90d0h
currLastEntry        =  90d1h
curInc               =  92deh
uXmin                =  92f5h
uXmax                =  92feh
uXscl                =  9307h
uYmin                =  9310h
uYmax                =  9319h
uYscl                =  9322h
uThetMin             =  932bh
uThetMax             =  9334h
uThetStep            =  933dh
uTmin                =  9346h
uTmax                =  934fh
uTStep               =  9358h
uPlotStart           =  9361h
unMax                =  936ah
uu0                  =  9373h
uv0                  =  937ch
unMin                =  9385h
uu02                 =  938eh
uv02                 =  9397h
uw0                  =  93a0h
uPlotStep            =  93a9h
uXres                =  93b2h
uw02                 =  93bbh
Xmin                 =  93c7h
Xmax                 =  93d0h
Xscl                 =  93d9h
Ymin                 =  93e2h
Ymax                 =  93ebh
Yscl                 =  93f4h
ThetaMin             =  93fdh
ThetaMax             =  9406h
ThetaStep            =  940fh
TminPar              =  9418h
TmaxPar              =  9421h
Tstep                =  942ah
PlotStart            =  9433h
nMax                 =  943ch
u0                   =  9445h
v0                   =  944eh
nMin                 =  9457h
u02                  =  9460h
v02                  =  9469h
w0                   =  9472h
PlotStep             =  947bh
XresO                =  9484h
w02                  =  948dh
smallEditRAM         =  949fh
XFact                =  950bh
YFact                =  9514h
Xres_int             =  951dh
deltaX               =  951eh
deltaY               =  9527h
shortX               =  9530h
shortY               =  9539h
lower                =  9542h
upper                =  954bh
XOutSym              =  9558h
XOutDat              =  955ah
YOutSym              =  955ch
YOutDat              =  955eh
inputSym             =  9560h
inputDat             =  9562h
prevData             =  9564h
TblMin               =  9676h
TblStep              =  967fh
P1Type		     =  968ch
SavX1List	     =  968dh
SavY1List	     =  9692h
SavF1List	     =  9697h
P1FrqOnOff	     =  96abh
P2Type		     =  96aeh
SavX2List	     =  96afh
SavY2List	     =  96b4h
SavF2List	     =  96b9h
P2FrqOnOff	     =  96cdh
P3Type		     =  96d0h	
SavX3List	     =  96d1h
SavY3List	     =  96d6h
SavF3List	     =  96d8h
P3FrqOnOff	     =  96efh
plotSScreen          =  9737h
seed1                =  9a37h
seed2                =  9a40h
cmdCursor            =  9a7eh
editTop              =  9a82h
editCursor           =  9a84h
editTail             =  9a86h
editBtm              =  9a88h
editSym              =  9a94h
editDat              =  9a96h
winTop               =  9b33h
winBtm               =  9b34h
winLeftEdge          =  9b35h
winLeft              =  9b36h
winAbove             =  9b38h
winRow               =  9b3ah
winCol               =  9b3ch
fmtDigits            =  9b3eh
fmtString            =  9b3fh
fmtConv              =  9b80h
fmtLeft              =  9b92h
fmtIndex             =  9b94h
fmtMatSym            =  9b96h
fmtMatMem            =  9b98h
EQS                  =  9b9ah
tSymPtr1             =  9ba6h
tSymPtr2             =  9ba8h
chkDelPtr3           =  9baah
chkDelPtr4           =  9bach
tempMem              =  9baeh
fpBase               =  9bb0h
FPS                  =  9bb2h
OPBase               =  9bb4h
OPS                  =  9bb6h
pTempCnt             =  9bb8h
cleanTmp             =  9bbah
pTemp                =  9bbch
progPtr              =  9bbeh
newDataPtr           =  9bc0h
appErr1              =  9bc2h
appErr2              =  9bcfh
appRawKeyHandle      =  85b8h
appBackUpScreen      =  8613h
customHeight         =  8612h
userMem              =  9c37h
symTable             =  0fe66h

;SYMBOLIC_DEBUG_END
;====================================
; equates for fraction entry routines
;====================================
fieldMax    =    OP5
fieldLen    =    fieldMax+1
fieldCol    =    fieldLen+1     ; pencol of start
fieldRow    =    fieldCol+1     ; penrow of start  3 rows above char
fieldInput  =    fieldRow+1     ; 9 bytes,
wholeMax    =    tempSwapArea
wholeLen    =    wholeMax+1
wholeCol    =    wholeLen+1     ; pencol of start
wholeRow    =    wholeCol+1     ; penrow of start  3 rows above char
wholeInput  =    wholeRow+1     ; 9 bytes,
;
numerMax    =    wholeInput+9
numerLen    =    numerMax+1
numerCol    =    numerLen+1     ; pencol of start
numerRow    =    numerCol+1     ; penrow of start
numerInput  =    numerRow+1     ; 9 bytes,
;
denomMax    =    numerInput+9
denomLen    =    denomMax+1
denomCol    =    denomLen+1     ; pencol of start
denomRow    =    denomCol+1     ; penrow of start
denomInput  =    denomRow+1     ; 9 bytes,
;
fracLineLen =   denomInput+9
fracLineCol =   fracLineLen+1
;
fracRightEdge  = fracLineCol+1
;
curPosition =   fracRightEdge+1
;
numerSpaces =   curPosition+1
denomSpaces =   numerSpaces+1
haveFrac    =   3
haveUnit    =   4
incCurPos   =   5




;====================================================================
; macro to call base code from an app
;====================================================================
rBR_CALL	=	28h		; needed for apps
BRT_JUMP0	=	50h
;
;
;======================================================================
;           system variable equates
;======================================================================
;       entry points : rclsystok, stosystok
;
;       vars with no restriction
;
XSCLt       =  3           ; 'xscl'           4
YSCLt       =  4           ; 'yscl'           4
;
;       vars rounded to 10 digits
;
XMINt       =  0AH         ; 'xMin'           4
XMAXt       =  0BH         ; 'xMax'           4
YMINt       =  0CH         ; 'yMin'           4
YMAXt       =  0DH         ; 'yMax'           4
TBLMINt     =  1AH         ; 'TBLMIN'
;
;       rouned to 10 digits, and positve integers
;
PLOTSTARTt   =  1bh         ; 'nstart'         5
UPLOTSTARTt  =  1ch         ; 'znstart'        6

;       vars cannot be 0
;
TBLSTEPt    =  21H         ; 'TBLSTEP'
TSTEPt      =  22H         ; 'tStep'          5
THETSTEPt   =  23H         ; Ltheta,'Step'    5
UTSTEPt     =  24H         ; 'ztStep'         6
UTHSTEPt    =  25H         ; 'z',Ltheta,'Step' 6
;
;       special limits on these
;
DELTAXt     =  26H         ; LcapDelta,'x'    2
DELTAYt     =  27H         ; LcapDelta,'y'    2
;
;       special limits on these
;
XFACTt      =  28H         ; 'xFact'          5
YFACTt      =  29H         ; 'yFact'          5
;
;       sys var list
;
TBLINPUTt       =       2ah
;
;
;
PLOTSTEPt       =       34h
UPLOTSTEPt      =       35h
;
XRESt           =       36h
UXRESt          =       37h
;
; special token used by input/prompt for storing value to variable

INPUTt      =  40h         ; set up for store for input/prompt

; special token used by graph math intersection routine to indicate
; list data

ISECTt      =  41h         ; list data follows this
;
;========== automatic power down time out value (<= 7 minutes)
apdTimeOut	=	5		;automatic power down time (minutes)

;========== interrupt equate
iAll 	= 1011b

;========== i/o ports
intrptEnPort	= 3 ; interrupt enable (in/out) 
memPageAPort	=	6		; memory page a	(i/o)
LCDInstPort     =     10h
LCDDataPort     =     11h 


;========== common subroutine rst numbers
rOP1TOOP2	=  08h
rFINDSYM	=  10h
rPUSHREALO1	=  18h
rMOV9TOOP1	=  20h
rFPADD		=  30h

;============error handler equates/macros
APP_PUSH_ERRORH =	59h
APP_POP_ERRORH	=	5Ch
;
;========== run indicators

busyPause	=	10101010b
busynNormal	=	11110000b

;======================================================================
;           system and state flags
;======================================================================
;

trigFlags	=  0		;trigonometry mode settings
trigDeg 	=    2	; 1=degrees, 0=radians

kbdFlags	=  0		;keyboard scan
kbdScr		=    3	; 1=scan code ready
kbdKeyPress	=    4	; 1=key has been pressed

doneFlags	=  0		;display "done"
donePrgm	=    5	; 1=display "done" after prgm

ioDelFlag	=  0
inDelete	=    0	;1 = delete screen 

;----------------------------------------------------------------------
editFlags	=  1
editOpen	=    2	; 1=edit buffer is open

monFlags	=  1		;monitor flags
monAbandon	=    4	; 1=don't start any long process
				; in put away (#715)
;----------------------------------------------------------------------
plotFlags	=  2		;plot generation flags
plotTrace	=  0
plotLoc 	=    1	; 0=bkup & display, 1=display only
plotDisp	=    2	; 1=plot is in display, 0=text in display


grfModeFlags	=  2		;graph mode settings
grfFuncM	=    4	; 1=function graph
grfPolarM	=    5	; 1=polar graph
grfParamM	=    6	; 1=parametric graph
grfRecurM       =    7        ; 1=recursion graph

graphFlags	=  3
graphProg	=  1     ; 1=graph in progress
graphDraw	=    0	; 0=graph is valid, 1=redraw graph

grfDBFlags	=  4
grfDot		=    0	; 0=line, 1=dot
grfSimul	=    1	; 0=sequential, 1=simultaneous
grfGrid 	=    2	; 0=no grid, 1=grid
grfPolar	=    3	; 0=rectangular, 1=polar coordinates
grfNoCoord	=    4	; 0=display coordinates, 1=off
grfNoAxis	=    5	; 0=axis, 1=no axis
grfLabel	=    6	; 0=off, 1=axis label

textFlags	=  5		;text output flags
textEraseBelow	=    1	; 1=erase line below small char
textScrolled	=    2	; 1=screen scrolled
textInverse	=    3	; 1=display inverse bit-map
textInsMode	=    4	; 0=overstrike, 1=insert mode

parsFlag2	=  7		;parser flags
numOP1		=    0	; 1=result in op1, 0=no result

newDispF        =  8		;derivative mode flags
preClrForMode   =    0	; 1=help blink on mode screen

apdFlags	=  8		;automatic power-down
apdAble 	=    2	; 1=apd enabled
apdRunning	=    3	; 1=apd clock running


web_err_mask    =  60h

onFlags 	=  9		;on key flags
onRunning	=	 3
onInterrupt	=    4	; 1=on key interrupt request

statFlags	=  9		;statistics flags
statsValid	=    6	; 1=stats are valid

fmtFlags	=  10		;numeric format flags
fmtExponent	=    0	; 1=show exponent, 0=no exponent
fmtEng		=    1	; 1=engineering notion, 0=scientific
fmtHex		=    2	; 1=hexadecimal
fmtOct		=    3	; 1=octal
fmtBin		=    4	; 1=binary
;
numMode         =  10
fmtReal         =    5
fmtRect         =    6
fmtPolar        =    7

realMode        =    5
rectMode        =    6
polarMmode       =    7
;					;   if hex and oct both = 1
;					;   then bin=0 means >frac
;					;	 bin=1 means >dms
fmtBaseMask     =  00011100b		; mask to base flags
fmtBaseShift    =  2			; offset to base flags
;
;       check if these are used by numform,
;
;               =  6
;               =  7

fmtOverride	=  11		;copy of fmtflags with conversion override

fmtEditFlags	=  12		;numeric editing flags
fmtEdit 	=    0	; 1=format number for editing

curFlags	=  12		;cursor
curAble 	=    2	; 1=cursor flash is enabled
curOn		=    3	; 1=cursor is showing
curLock 	=    4	; 1=cursor is locked off

appFlags	=  13		;application flags
appWantIntrpt	=    0		; 1=want on key interrupts
appTextSave	=    1		; 1=save characters in textshadow
appAutoScroll	=    2		; 1=auto-scroll text on last line
appMenus	=    3		; 1=process keys that bring up menus
					; 0=check lock menu flag
appLockMenus	=    4		; 1=ignore menu keys
					; 0=switch to home screen and bring up menu
appCurGraphic	=    5		; 1=graphic cursor
appCurWord	=    6		; 1=text cursor covers entire word
appExit 	=    7		; 1=application handles [exit] key itself

appWantIntrptF	=	1<<appWantIntrpt
appTextSaveF	=	1<<appTextSave
appAutoScrollF	=	1<<appAutoScroll
appMenusF	=	1<<appMenus
appLockMenusF	=	1<<appLockMenus
appCurGraphicF	=	1<<appCurGraphic
appCurWordF	=	1<<appCurWord
appExitF	=	1<<appExit


seqFlags        =  15              ; sequential graph flags
webMode         =    0             ; 0 = normal seq mode, 1 = web mode
webVert         =    1             ;
sequv           =    2             ; u vs v
seqvw           =    3             ; v vs w
sequw           =    4             ; u vs w


promptFlags	=  17		;prompt line flags
promptEdit	=    0	; 1=editing in prompt buffer

indicFlags	=  18		;indicator flags
indicRun	=    0	; 1=run indicator on
indicInUse	=    1	; indicator save area in use=1, free=0
indicOnly	=    2	; interrupt handler only checks run indicator

shiftFlags	=  18		;[2nd] and [alpha] flags
shift2nd	=    3	; 1=[2nd] has been pressed
shiftAlpha	=    4	; 1=[alpha] has been pressed
shiftLwrAlph	=    5	; 1=lower case, 0=upper case
shiftALock	=    6	; 1=alpha lock has been pressed
shiftKeepAlph	=    7	; 1=cannot cancel alpha shift


tblFlags        =  19		;table flags.
autoFill        =    4	; 1=prompt, 0=fillauto
autoCalc        =    5	; 1=prompt, 0=calcauto
reTable         =    6	; 0=table is ok, 1=must recompute table.


fracMode        =      20             ;FRAC FLAGS
ABCF            =       0             ; a b/c if 1, b/c if 0.
AutoSimpF       =       1             ; autosimp if 0, mansimp if 1.
ManSimpF        =       1             ; autosimp if 0, mansimp if 1.

ConstMode       =     20
Hide            =       2            ;0 if show, 1 if hide.
Multi           =       3            ;0 if single, 1 if multiple.

sGrFlags	=  20
write_on_graph  =    4	; 1 = text or = writing to graph screen
g_style_active  =    5	; 1 = graph styles are enabled, use them
cmp_mod_box     =    6	; 1 = doing mod box plot computation
textWrite       =    7
;

newIndicFlags   =  21
extraIndic      =    0
saIndic         =    1

smartFlags      =  23
;
;----------------------------------------------------------------------
; note: fix these equates if smartflags are moved                            
;----------------------------------------------------------------------
;
smarter_mask    =  3
smarter_test    =  1
smartGraph      =  0
smartGraph_inv  =  1                                                 

;----------------------------------------------------------------------
;           available for asm programming
;----------------------------------------------------------------------

asm_Flag1       =  33          ; asm coding
asm_Flag2       =  34          ; asm coding
asm_Flag3       =  35          ; asm coding

;----------------------------------------------------------------------

fracFlags	=	  36		; fraction display/edit
fracStopEOS	=	   0
fracInside	=	   1
fracDontDraw	=	   2
fracEditMode	=	   3
fracInNum	=	   4
fracScrScrolled =	   5
fracFlag1	=	   6
fracClearEOS	=	   7	   ; tell drawExpr code to clear to end of screen when done
;
fontFlags	=	   37
fracDrawNum	=	   0
fracDrawDenom	=	   1
fracDrawLFont	=	   2
fracTallLFont	=	   3
fracLastChar	=	   4
fracCursor	=	   5
DrawLfont       =       6
CustomFont      =       7
;
;flags for fraction math routines:
FracFlags2      =     38
DtoF10F         =      0      ;10digit flag for Dec ->Frac
SimpKeyF        =      1      ;Used for Simp K.
RatD1K          =      1      ;also used to show denominator=1000
FinalType       =      2      ;1=final type is improper 0=type is simple
FinalFrac       =      3      ;set if final fraction result expected.
ManDtoF         =      4      ;manual simp=1, Dec->frac bit.
ManForceS       =      5      ;manual simp=1, Abc=0; force simplify anyhow.
ManAdjW         =      6      ;manual simp=1, Abc=0; Adjust whole flag.
ManBin          =      7      ;manual simp=1, Binop set.


;flags for fraction math routines:
FracFlags3      =     39
FNormOver       =      0      ;Fraction Normalization Override if set.
DivSmallNum     =      1      ;set if Frac Numerator < denominator.
IntDivDisp      =      2      ;integer divide display flag.
StrictInt       =      3      ;strict integer check by intdiv if set.
yxRatF          =      4      ;y^x rational approximation flag.
nid             =      5      ;1 if non integer decimal
SimpDisp        =      6      ;1 if need to display frac simplify factor.
HadTrueMixed    =      7      ;at least one true fraction in an expression.
;(HadTrueMixed is needed by >Ab/c<>d/e so that if any mixed fraction, then
; if convert, will stay b/c if b/c mode & not convert back to mixed.)

;
getSendFlg	=  47
comFailed	=  3	      ; 1 = get/send communication failed
;
;
apiFlg3		=  52 
;
apiFlg4		=  53
fullScrnDraw	=    2	; draw into last row/col of screen

xapFlag0        =  43      ; external app flags
xapFlag1        =  44
xapFlag2        =  45
xapFlag3        =  46

plotFlag3	=  47  
bufferOnly	=    6
useFastCirc	=    4

;
varTypeMask	=  1fh             
varGraphRef	=    6

;======================================================================
;           character font equates  
;======================================================================
;
LrecurN         =     001h
LrecurU 	=	002h
LrecurV 	=	003h
LrecurW		=	004h
Lconvert	=	005h
LsqUp		=	006h
LsqDown		=	007h
Lintegral	=	008h
Lcross		=	009h
LboxIcon 	=     00ah
LcrossIcon 	=     00bh
LdotIcon 	=     00ch
LsubT   	=     00dh		;small capital t for parametric mode.
LcubeR  	=     00eh		;slightly different 3 for cubed root.
LhexF		=	00fh
Lroot		=	010h
Linverse	=	011h
Lsquare		=	012h
Langle		=	013h
Ldegree		=	014h
Lradian		=	015h
Ltranspose	=	016h
LLE		=	017h
LNE		=	018h
LGE		=	019h
Lneg		=	01ah
Lexponent	=	01bh
Lstore		=	01ch
Lten		=	01dh
LupArrow	=	01eh
LdownArrow	=	01fh
Lspace		=	020h
Lexclam		=	021h
Lquote		=	022h
Lpound		=	023h
Lfourth		=     024h
Lpercent	=	025h
Lampersand	=	026h
Lapostrophe	=	027h
LlParen		=	028h
LrParen		=	029h
Lasterisk	=	02ah
LplusSign	=	02bh
Lcomma		=	02ch
Ldash		=	02dh
Lperiod		=	02eh
Lslash		=	02fh
L0		=	030h
L1		=	031h
L2		=	032h
L3		=	033h
L4		=	034h
L5		=	035h
L6		=	036h
L7		=	037h
L8		=	038h
L9		=	039h
Lcolon		=	03ah
Lsemicolon	=	03bh
LLT		=	03ch
LEQ		=	03dh
LGT		=	03eh
Lquestion	=	03fh
LatSign		=	040h
LcapA	=	041h
LcapB	=	042h
LcapC	=	043h
LcapD	=	044h
LcapE	=	045h
LcapF	=	046h
LcapG	=	047h
LcapH	=	048h
LcapI	=	049h
LcapJ	=	04ah
LcapK	=	04bh
LcapL	=	04ch
LcapM	=	04dh
LcapN	=	04eh
LcapO	=	04fh
LcapP	=	050h
LcapQ	=	051h
LcapR	=	052h
LcapS	=	053h
LcapT	=	054h
LcapU	=	055h
LcapV	=	056h
LcapW	=	057h
LcapX	=	058h
LcapY	=	059h
LcapZ	=	05ah
Ltheta  =     05bh
Lbackslash	=	05ch
LrBrack		=	05dh
Lcaret		=	05eh
Lunderscore	=	05fh
Lbackquote	=	060h
La	=	061h
Lb	=	062h
Lc	=	063h
Ld	=	064h
Le	=	065h
Lf	=	066h
Lg	=	067h
Lh	=	068h
Li	=	069h
Lj	=	06ah
Lk	=	06bh
Ll	=	06ch
Lm	=	06dh
Ln	=	06eh
Lo	=	06fh
Lp	=	070h
Lq	=	071h
Lr	=	072h
Ls	=	073h
Lt	=	074h
Lu	=	075h
Lv	=	076h
Lw	=	077h
Lx	=	078h
Ly	=	079h
Lz	=	07ah
LlBrace	=	07bh
Lbar	=	07ch
LrBrace	=	07dh
Ltilde	=	07eh
LinvEQ	=	07fh
Lsub0	=	080h
Lsub1	=	081h
Lsub2	=	082h
Lsub3	=	083h
Lsub4	=	084h
Lsub5	=	085h
Lsub6	=	086h
Lsub7	=	087h
Lsub8	=	088h
Lsub9	=	089h
LcapAAcute	=	08ah
LcapAGrave	=	08bh
LcapACaret	=	08ch
LcapADier	=	08dh
LaAcute		=	08eh
LaGrave		=	08fh
LaCaret		=	090h
LaDier		=	091h
LcapEAcute	=	092h
LcapEGrave	=	093h
LcapECaret	=	094h
LcapEDier	=	095h
LeAcute		=	096h
LeGrave		=	097h
LeCaret		=	098h
LeDier		=	099h
LcapIAcute	=	09ah
LcapIGrave	=	09bh
LcapICaret	=	09ch
LcapIDier	=	09dh
LiAcute		=	09eh
LiGrave		=	09fh
LiCaret		=	0a0h
LiDier		=	0a1h
LcapOAcute	=	0a2h
LcapOGrave	=	0a3h
LcapOCaret	=	0a4h
LcapODier	=	0a5h
LoAcute		=	0a6h
LoGrave		=	0a7h
LoCaret		=	0a8h
LoDier		=	0a9h
LcapUAcute	=	0aah
LcapUGrave	=	0abh
LcapUCaret	=	0ach
LcapUDier	=	0adh
LuAcute		=	0aeh
LuGrave		=	0afh
LuCaret		=	0b0h
LuDier		=	0b1h
LcapCCed	=	0b2h
LcCed		=	0b3h
LcapNTilde	=	0b4h
LnTilde		=	0b5h
Laccent		=	0b6h
Lgrave		=	0b7h
Ldieresis	=	0b8h
LquesDown	=	0b9h
LexclamDown	=	0bah
Lalpha		=	0bbh
Lbeta		=	0bch
Lgamma		=	0bdh
LcapDelta	=	0beh
Ldelta		=	0bfh
Lepsilon	=	0c0h
LlBrack 	=     0c1h
Llambda		=	0c2h
Lmu		=	0c3h
Lpi		=	0c4h
Lrho		=	0c5h
LcapSigma	=	0c6h
Lsigma		=	0c7h
Ltau		=	0c8h
Lphi		=	0c9h
LcapOmega	=	0cah
LxMean		=	0cbh
LyMean		=	0cch
LsupX		=	0cdh
Lellipsis	=	0ceh
Lleft		=	0cfh
Lblock		=	0d0h
Lper		=	0d1h
Lhyphen		=	0d2h
Larea		=	0d3h
Ltemp		=	0d4h
Lcube		=	0d5h
Lenter		=	0d6h
LimagI		=	0d7h
Lphat		=	0d8h
Lchi		=	0d9h
LstatF		=     0dah
Llne		=	0dbh
LlistL		=	0dch
LfinanN 	=	0ddh
L2_r_paren 	=	0deh
LblockArrow     =     0dfh
LcurO   	=     0e0h
LcurO2  	=     0e1h
LcurOcapA       =     0e2h
LcurOa  	=     0e3h
LcurI   	=     0e4h
LcurI2  	=     0e5h
LcurIcapA       =     0e6h
LcurIa  	=     0e7h
LGline          =     0e8h    ; = 0
LGthick         =     0e9h    ; = 1
LGabove         =     0eah    ; = 2
LGbelow         =     0ebh    ; = 3
LGpath          =     0ech    ; = 4
LGanimate       =     0edh    ; = 5
LGdot           =     0eeh    ; = 6
LGpBlk          =     0efh    ;up arrow and block in solver
LDnBlk          =     0f0h    ;down arrow and block in solver
LcurFull        =     0f1h    ;note: must be last char (putmap checks)

;========= floating point

FPLEN           =     9               ;length of a floating-point number

;========= constant table equates

drConst 	=	0
pi2Const	=	1
pi4Const	=	2
logeConst	=	3
piConst 	=	4
DtoRConst	=	5
ln10		=	6

;========== text screen
textRows	=	8
textCols        =     16

;========== alphatext screen
alphabuffersize =	16
;
;========== msc fraction equates
mantSignBit     =  7       ;msbit of data type byte is set if negative.
redF            =  1       ;reduced if 0, unreduced if 1.
unredF          =  1       ;reduced if 0, unreduced if 1.
simpF           =  0       ;simple if 0, mixed if 1.
mixedF          =  0       ;simple if 0, mixed if 1.
;

;
;

;====== keyboard key names
;
;       key presses that do not echo into the display
;       and are not application or menu loading key presses
;
kRight          =       001h
kLeft           =       002h
kUp             =       003h
kDown           =       004h
kEnter          =       005h
kAlphaEnter     =       006h
kAlphaUp        =       007h
kAlphaDown      =       008h
kClear          =       009h
kDel            =       00Ah
kIns            =       00Bh
kRecall         =       00Ch
kLastEnt        =       00Dh
kBOL            =       00Eh
kEOL            =       00Fh
;
kSelAll         =       010h
kUnselAll       =       011h
kLtoTI82        =       012h
kBackup         =       013h
kRecieve        =       014h
kLnkQuit        =       015h
kTrans          =       016h
kRename         =       017h
kOverw          =       018h
kOmit           =       019h
kCont           =       01Ah
kLtoTI83        =       01Bh
kSendApp        =       01Ch
kvApps          =       01Ch
kSendId         =       01Dh
kSendSW         =       01Eh
kvALL           =       01Fh
kvSendType      =       020h  ;send a particular data type (in kextend)
kNoWay          =       021h
kYes            =       022h
kAbout          =       023h  ;about screen
kApp            =       024h
;
kProgExec       =       025h
kListflag       =       026h
;
;open for expansion       028h..02bh
;

menustart       =       02ch
;
;       key presses that do not echo into the display
;       and are menu switching key presses
;
kAreYouSure     =       02Ch
kPrgm           =       02Dh
kZoom           =       02Eh        ; PULL DOWN
kDraw           =       02Fh
kSPlot          =       030h
kStat           =       031h
kMath           =       032h
kTest           =       033h
kAppsMenu       =       034h
kVars           =       035h
kMem            =       036h
kConstSetmenu   =       037h
kConvMenu       =       038h
kTrigmenu       =       039h
kLIST           =       03Ah
kConstMenu      =       03Bh
;
menuend         =       kConstMenu
;
kConst          =       03Ch
;
;
kAlpha          =       03dh
kCatalog        =       03eh        ; catalog
kInputDone      =       3fh

;
;       key presses that do not echo into the display
;       and are context switching key presses
;
kQuit           =       040h
kLinkIO         =       041h
kChkApps        =       042h        ; Check Apps
kStatEd         =       043h
kGraph          =       044h
kMode           =       045h
kPrgmEd         =       046h        ; PROGRAM EDIT
kPrgmCr         =       047h        ; PROGRAM CREATE
kWindow         =       048h        ; RANGE EDITOR
kYequ           =       049h        ; EQUATION EDITOR
kTable          =       04Ah        ; TABLE EDITOR
kTblSet         =       04Bh        ; TABLE SET
kChkRAM         =       04CH        ; CHECK RAM
kDelMem         =       04DH        ; DELETE MEM
kResetMem       =       04EH        ; RESET MEM
kResetDef	=	  04FH        ; RESET DEFAULT
kPrgmInput      =       050H        ; PROGRAM INPUT
kZFactEd        =       051H        ; ZOOM FACTOR EDITOR
kError          =       052H        ; ERROR
kExtApps        =       053h        ; Extra Applications.     NEW
kSolveRoot	=	  054h        ; SOLVE EDITOR
kStatP          =       055h        ; stat plot
kConstEd        =       056h        ; Constant = Editor.
kPrgmSetVar     =       057h        ; SetMenu in a Program.
kFormat         =       058h        ; FORMAT
;
;
append          =       kFormat
;
;       58h - 59h are empty for growth
;
echoStart1      =       05ah
;
;       conditional keys that if in prog edit will echo
;       or if not in prog edit go to the graph screen
;       'trace' , 'zooms'
;
;
kTrace          =       05ah
kZFit           =       05bh
kZIn            =       05ch
kZOut           =       05dh
kZPrev          =       05eh
kZBox           =       05fh
kZDecml         =       060h
kSetZm          =       061h
kZSquar         =       062h
kZStd           =       063h
kZTrig          =       064h
kZQuad1         =       065h
;kusrzm          =       065h
;kzsto           =       066h
kZInt           =       067h
kZStat          =       068h
;
;       conditional keys that if in prog edit or if the
;       graph screen is not displayed will echo, else will
;       go to the graph screen. 'draw' menu, eval
echoStart2      =       069h
;
;
kSelect         =       069h
kCircl          =       06ah
kClDrw          =       06bh
kLine           =       06ch
kPen            =       06dh
kPtChg          =       06eh
kPtOff          =       06fh
kPtOn           =       070h
kVert           =       071h
kHoriz          =       072h
kText           =       073h
kTanLn          =       074h
kManFit         =       075h
;
;   these keys will always force to graph mode
;
kEval           =       076h
;
echoStart       =       07dh
;
;       key presses that echo into a buffer
;
kRemain         =       07dh          ;only needed to display 'r'
kPlot3          =       07eh
kListName       =       07fh
kAdd            =       080h
kSub            =       081h
kMul            =       082h
kDiv            =       083h
kExpon          =       084h
kLParen         =       085h
kRParen         =       086h
kLBrack         =       087h
kRBrack         =       088h
kShade          =       089h
kStore          =       08ah
kComma          =       08bh
kChs            =       08ch
kDecPnt         =       08dh
k0              =       08eh
k1              =       08fh
k2              =       090h
k3              =       091h
k4              =       092h
k5              =       093h
k6              =       094h
k7              =       095h
k8              =       096h
k9              =       097h
kEE             =       098h
kSpace          =       099h
kCapA           =       09Ah
kCapB           =       09Bh
kCapC           =       09Ch
kCapD           =       09Dh
kCapE           =       09Eh
kCapF           =       09Fh
kCapG           =       0A0h
kCapH           =       0A1h
kCapI           =       0A2h
kCapJ           =       0A3h
kCapK           =       0A4h
kCapL           =       0A5h
kCapM           =       0A6h
kCapN           =       0A7h
kCapO           =       0A8h
kCapP           =       0A9h
kCapQ           =       0AAh
kCapR           =       0ABh
kCapS           =       0ACh
kCapT           =       0ADh
kCapU           =       0AEh
kCapV           =       0AFh
kCapW           =       0B0h
kCapX           =       0B1h
kCapY           =       0B2h
kCapZ           =       0B3h
kVarx           =       0B4h
kPi             =       0B5h
kInv            =       0B6h
kSin            =       0B7h
kASin           =       0B8h
kCos            =       0B9h
kACos           =       0BAh
kTan            =       0BBh
kATan           =       0BCh
kSquare         =       0BDh
kSqrt           =       0BEh
kLn             =       0BFh
kExp            =       0C0h
kLog            =       0C1h
kALog           =       0C2h
kToABC          =       0C3h
;
;kclrtbl         =       0c4h
;
kAns            =       0c5h
kColon          =       0c6h
;

kUnit           =       0c7h          ;                       new
kFracSlash      =       0c8h          ;                       new
kRoot           =       0c9h
;
kQuest          =       0cah        ; question mark
kQuote          =       0cbh        ; "
kTheta          =       0cch
kIf             =       0cdh
kThen           =       0ceh
kElse           =       0cfh
kFor            =       0d0h
kWhile          =       0d1h
kRepeat         =       0d2h
kEnd            =       0d3h
kPause          =       0d4h
kLbl            =       0d5h
kGoto           =       0d6h
kISG            =       0d7h
kDSL            =       0d8h
kMenu           =       0d9h
kExec           =       0dah
kReturn         =       0dbh
kStop           =       0dch
kInput          =       0ddh
kPrompt         =       0deh
kDisp           =       0dfh
kDispG          =       0e0h
kDispT          =       0e1h
kOutput         =       0e2h
kGetKey         =       0e3h
kClrHome        =       0e4h
;
;kprtscr         =       0e5h
;
kPercent        =        0e6h         ;%                      new
kMixSimp        =        0e7h         ;                       new
kFracDec        =        0e8h         ;                       new
kSimp           =        0e9h         ;                       new
kUnredF         =        0eah         ;                       new
kIDiv           =        0ebh         ;                       new
kConvert        =       0ech          ;                       new
kLBrace         =       0edh
kRBrace         =       0eeh
kL1A            =       0efh
kL2A            =       0f0h
kL3A            =       0f1h
kL4A            =       0f2h
kL5A            =       0f3h
kL6A            =       0f4h
kConvLength     =       0f5h        ;1st byte of conversion key.
kConvArea       =       0f6h        ;1st byte of conversion key.
kConvVolume     =       0f7h        ;1st byte of conversion key.
kConvTime       =       0f8h        ;1st byte of conversion key.
kConvTemp       =       0f9h        ;1st byte of conversion key.
kConvMass       =       0fah        ;1st byte of conversion key.
kConvSpeed      =       0fbh        ;1st byte of conversion key.
;
;       this key means that it is a 2 byte keycode
;       there are 2 of these keys be careful
;
;
kExtendEcho2    =       0fch
;
;
;       this key means that the key press is one that echos
;       into a buffer, and it is a 2 byte key code, go look at
;       (extecho) for the key value
;
kExtendEcho     =       0feh
;
kE1BT           =       0

kDrawInv        =       kE1BT
kDrawF          =       kE1BT+1
kPixelOn        =       kE1BT+2
kPixelOff       =       kE1BT+3
kPxlTest        =       kE1BT+4
kRCGDB          =       kE1BT+5
kRCPic          =       kE1BT+6
kSTGDB          =       kE1BT+7
kSTPic          =       kE1BT+8
kAbs            =       kE1BT+9
kTEqu           =       kE1BT+10    ; ==
kTNoteQ         =       kE1BT+11    ; <>
kTGT            =       kE1BT+12    ; >
kTGTE           =       kE1BT+13    ; > =
kTLT            =       kE1BT+14    ; <
kTLTE           =       kE1BT+15    ; < =

kAnd            =       kE1BT+16
kOr             =       kE1BT+17
kXor            =       kE1BT+18
kNot            =       kE1BT+19

;klr1            =       kE1BT+20

kXRoot          =       kE1BT+21
kCube           =       kE1BT+22
kCbRt           =       kE1BT+23    ; cube root
kToDec          =       kE1BT+24
;
kCubicR         =       kE1BT+25
kQuartR         =       kE1BT+26
;
kPlot1          =       kE1BT+27
kPlot2          =       kE1BT+28
;

kRound          =       kE1BT+29
kIPart          =       kE1BT+30
kFPart          =       kE1BT+31
kInt            =       kE1BT+32

kRand           =       kE1BT+33
kNPR            =       kE1BT+34
kNCR            =       kE1BT+35
kXFactorial     =       kE1BT+36

kRad            =       kE1BT+37
kDegr           =       kE1BT+38    ; degrees conv
kApost          =       kE1BT+39
kToDMS          =       kE1BT+40
kRtoPo          =       kE1BT+41    ; r
kRtoPr          =       kE1BT+42
kPtoRx          =       kE1BT+43
kPtoRy          =       kE1BT+44

kRowSwap        =       kE1BT+45
kRowPlus        =       kE1BT+46
kTimRow         =       kE1BT+47
kTRowP          =       kE1BT+48

kSortA          =       kE1BT+49
kSortD          =       kE1BT+50
kSeq            =       kE1BT+51

kMin            =       kE1BT+52
kMax            =       kE1BT+53
kMean           =       kE1BT+54
kMedian         =       kE1BT+55
kSum            =       kE1BT+56
kProd           =       kE1BT+57

kDet            =       kE1BT+58
kTransp         =       kE1BT+59
kDim            =       kE1BT+60    ; same as one in list
kFill           =       kE1BT+61    ; same as one in list
kIdent          =       kE1BT+62
kRandm          =       kE1BT+63
kAug            =       kE1BT+64

kLstMode        =       kE1BT+65
;available      =       kE1BT+66

kmRad           =       kE1BT+67
kmDeg           =       kE1BT+68
kmNormF         =       kE1BT+69
kmSci           =       kE1BT+70
kFix            =       kE1BT+71
kmFloat         =       kE1BT+72
kSetMenu        =       kE1BT+73
kEOFrac         =       kE1BT+74

kBoyIcon        =     kE1BT+75
kTreeIcon       =     kE1BT+76
kFaceIcon       =     kE1BT+77
kCircleIcon     =     kE1BT+78
kDiamondIcon    =     kE1BT+79
kStarIcon       =     kE1BT+80
kDollarIcon      =    kE1BT+81
;available      =     kE1BT+82
;available      =     kE1BT+83

kFNOn           =       kE1BT+84
kFNOff          =       kE1BT+85

kPlotsOn        =       kE1BT+86
kPlotsPff       =       kE1BT+87

kPixelChg       =       kE1BT+88

kSendMBL        =       kE1BT+89
kRecvMBL        =       kE1BT+90

kBoxPlot        =       kE1BT+ 91
kBoxIcon        =       kE1BT+ 92

kCrossIcon      =       kE1BT+ 93
kDotIcon        =       kE1BT+ 94

kE2BT           =       kE1BT+95

kSeqential      =       kE2BT         ;105
kSimulG         =       kE2BT+1       ;106
kCoordOn        =       kE2BT+2       ;107
kCoordOff       =       kE2BT+3       ;108
kDrawLine       =       kE2BT+4       ;109
kDrawDot        =       kE2BT+5       ;110
kAxisOn         =       kE2BT+6       ;111
kAxisOff        =       kE2BT+7       ;112
kGridOn         =       kE2BT+8       ;113
kGridOff        =       kE2BT+9       ;114
kLblOn          =       kE2BT+10      ;115
kOblOff         =       kE2BT+11      ;116
kPolarG         =       kE2BT+12      ;117
kRectG          =       kE2BT+13      ;118

kL1             =       kE2BT+14      ;119 = 77h
kL2             =       kE2BT+15
kL3             =       kE2BT+16
kL4             =       kE2BT+17
kL5             =       kE2BT+18
kL6             =       kE2BT+19

kMatA          =       kE2BT+20
kMatB           =       kE2BT+21
kMatC           =       kE2BT+22
kMatD           =       kE2BT+23
kMatE           =       kE2BT+24

kXmin           =       kE2BT+25
kXmax           =       kE2BT+26
kXscl           =       kE2BT+27
kYmin           =       kE2BT+28
kYmax           =       kE2BT+29
kYscl           =       kE2BT+30
kTmin           =       kE2BT+31
kTmax           =       kE2BT+32
kTStep          =       kE2BT+33
kOmin           =       kE2BT+34
kOmax           =       kE2BT+35
kOStep          =       kE2BT+36
ku0             =       kE2BT+37
kv0             =       kE2BT+38
knMin           =       kE2BT+39
knMax           =       kE2BT+40
kDeltaY         =       kE2BT+41
kDeltaX         =       kE2BT+42

kZXmin          =       kE2BT+43
kZXmax          =       kE2BT+44
kZXscl          =       kE2BT+45
kZYmin          =       kE2BT+46
kZYmax          =       kE2BT+47
kZYscl          =       kE2BT+48
kZTmin          =       kE2BT+49
kZTmax          =       kE2BT+50
kZTStep         =       kE2BT+51
kZOmin          =       kE2BT+52
kZOmax          =       kE2BT+53
kZOStep         =       kE2BT+54
;
kABC         =       kE2BT+55
kBC           =       kE2BT+56
kAutoSimp       =       kE2BT+57
kManSimp        =       kE2BT+58

kDelLast        =       kE2BT+59
ksingleC        =       kE2BT+60
kMultiC         =       kE2BT+61

kPic1           =       kE2BT+62
kPic2           =       kE2BT+63
kPic3           =       kE2BT+64

kDelVar         =       kE2BT+65
kGetCalc        =       kE2BT+66

kStndrd         =       kE2BT+67
kParam          =       kE2BT+68
kPolar          =       kE2BT+69
kAFillOn        =       kE2BT+70
kAFillOff       =       kE2BT+71
kACalcOn        =       kE2BT+72
kACalcOff       =       kE2BT+73

kSetConst       =       kE2BT+74
;
kGraphStyle     =       kE2BT+75
;
kExprOn         =       kE2BT+76
kExprOff        =       kE2BT+77
kStatA          =       kE2BT+78
kStatB          =       kE2BT+79
kStatC          =       kE2BT+80
kCorr           =       kE2BT+81
kStatD          =       kE2BT+82
kStatE          =       kE2BT+83
kRegEq          =       kE2BT+84
kMinX           =       kE2BT+85
kQ1             =       kE2BT+86
kMD             =       kE2BT+87
kQ3             =       kE2BT+88
kMaxX           =       kE2BT+89
kStatX1         =       kE2BT+90
kStatY1         =       kE2BT+91
kStatX2         =       kE2BT+92
kStatY2         =       kE2BT+93
kStatX3         =       kE2BT+94
kStatY3         =       kE2BT+95
kTblMin         =       kE2BT+96
kTblStep        =       kE2BT+97
kSetupLst       =       kE2BT+98
kClrAllLst      =       kE2BT+99
kLogistic       =       kE2BT+100
kc1             =       kE2BT+101   ;constant c1
kc2             =       kE2BT+102   ;constant c2
kc3             =       kE2BT+103   ;constant c3
kc4             =       kE2BT+104   ;constant c4
kPlotStart      =       kE2BT+105   ; plotstart
kZPlotStart     =       kE2BT+106   ; zplotstart
kXFact          =       kE2BT+107   ; xfact
kYFact          =       kE2BT+108   ; yfact
kMaxY           =       kE2BT+109   ; maxy
kTblInput       =       kE2BT+110   ; web off
kPlotStep       =       kE2BT+111
kZPlotStep      =       kE2BT+112
kSelectA        =       kE2BT+113
kZFitA          =       kE2BT+114
kOneVar         =       kE2BT+115
kTwoVar         =       kE2BT+116
kLR             =       kE2BT+117
kLRExp          =       kE2BT+118
kLRLn           =       kE2BT+119
;klrpwr          =       kE2BT+
;kmanfit         =       kE2BT+
kMedMed         =       kE2BT+120
kQuad           =       kE2BT+121
kClrLst         =       kE2BT+122
kHist           =       kE2BT+123
kXYLine         =       kE2BT+124
kScatter        =       kE2BT+125
kLR1            =       kE2BT+126
kClrTbl         =       kE2BT+127
kE2BT_end       =       kClrTbl
;
;

; new 2 byte keys

kE2BT2		=       0

kGDB1		=       kE2BT2
kGDB2		=       kE2BT2+1
kGDB3		=       kE2BT2+2
kY1		=       kE2BT2+3
kY2		=       kE2BT2+4
kY3		=       kE2BT2+5
kY4		=       kE2BT2+6
kY5		=       kE2BT2+7
kY6		=       kE2BT2+8
kY7		=       kE2BT2+9
kY8		=       kE2BT2+10
kY9		=       kE2BT2+11
kY0		=       kE2BT2+12
kX1T		=       kE2BT2+13
kY1T		=       kE2BT2+14
kX2T		=       kE2BT2+15
kY2T		=       kE2BT2+16
kX3T		=       kE2BT2+17
kY3T		=       kE2BT2+18
kX4T		=       kE2BT2+19
kY4T		=       kE2BT2+20
kX5T		=       kE2BT2+21
kY5T		=       kE2BT2+22
kX6T		=       kE2BT2+23
kY6T		=       kE2BT2+24
kR1		=       kE2BT2+25
kR2		=       kE2BT2+26
kR3		=       kE2BT2+27
kR4		=       kE2BT2+28
kR5		=       kE2BT2+29
kR6		=       kE2BT2+30
kGDB4		=       kE2BT2+31
kGDB5		=       kE2BT2+32
kGDB6		=       kE2BT2+33
kPic4		=       kE2BT2+34
kPic5		=       kE2BT2+35
kPic6		=       kE2BT2+36
kGDB7		=       kE2BT2+37
kGDB8		=       kE2BT2+38
kGDB9		=       kE2BT2+39
kGDB0		=       kE2BT2+40
kPic7		=       kE2BT2+41
kPic8		=       kE2BT2+42
kPic9		=       kE2BT2+43
kPic0		=       kE2BT2+44
kStatN		=       kE2BT2+45
kXMean		=       kE2BT2+46
;kconj           =       kE2BT2+47
;kreal           =       kE2BT2+48
;kfangle         =       kE2BT2+49
kLCM            =       kE2BT2+50
kGCD            =       kE2BT2+51
kRandInt        =       kE2BT2+52
kCoin           =       kE2BT2+53
kDice           =       kE2BT2+54
kRemainder      =       kE2BT2+55
kYMean          =       kE2BT2+56
kStdX           =       kE2BT2+57
kStdX1          =       kE2BT2+58
kw0             =       kE2BT2+59
kMatf           =       kE2BT2+60
kMatg           =       kE2BT2+61
kMatrh          =       kE2BT2+62
kMati           =       kE2BT2+63
kMatj           =       kE2BT2+64
kYMean1         =       kE2BT2+65
kStdY           =       kE2BT2+66
kStdY1          =       kE2BT2+67
kMaTtoLst       =       kE2BT2+68
kLstToMat       =       kE2BT2+69
kCumSum         =       kE2BT2+70
kDeltaLst       =       kE2BT2+71
kStdDev         =       kE2BT2+72
kVariance       =       kE2BT2+73
kLength         =       kE2BT2+74
kEquToStrng     =       kE2BT2+75
kStrngToEqu     =       kE2BT2+76
kExpr           =       kE2BT2+77
kSubStrng       =       kE2BT2+78
kInStrng        =       kE2BT2+79
kStr1           =       kE2BT2+80
kStr2           =       kE2BT2+81
kStr3           =       kE2BT2+82
kStr4           =       kE2BT2+83
kStr5           =       kE2BT2+84
kStr6           =       kE2BT2+85
kStr7           =       kE2BT2+86
kStr8           =       kE2BT2+87
kStr9           =       kE2BT2+88
kStr0           =       kE2BT2+89
kSimpFact       =       kE2BT2+90
kPictPlot       =     kE2BT2+91
kCirclePlot     =     kE2BT2+92
kStemPlot       =     kE2BT2+93
kBarPlot        =     kE2BT2+94
kSumX           =       kE2BT2+102
kSumX2          =       kE2BT2+103

kSumY           =       kE2BT2+104
kXRes           =       kE2BT2+105

kSumY2          =       kE2BT2+106
kSumXY          =       kE2BT2+107
kuXRes          =       kE2BT2+108
kModBox         =       kE2BT2+109
kNormProb       =       kE2BT2+110

kMinY           =       kE2BT2+111   ; miny
kRef            =       kE2BT2+112
kRRef           =       kE2BT2+113
kLRSqr          =       kE2BT2+114
kBRSqr          =       kE2BT2+115
kDiagOn         =       kE2BT2+116
kDiagOff        =       kE2BT2+117
;kun1            =       kE2BT2+152   ; for rcl use when gotten from 82
;kvn1            =       kE2BT2+153   ; for rcl use when gotten from 82
kE2BT2_end      =       kDiagOff

;=================================================
;    context equates
;=================================================
cxCmd           =     kQuit                   ;home screen
cxAppsApp       =     kChkApps                ;apps usage screen
cxPrgmEdit      =     kPrgmEd                 ;program editor
cxEquEdit       =     kYequ                   ;equation editor
cxGrRange       =     kWindow                 ;graph range editor
cxGrZfact       =     kZFactEd                ;graph zoom factors editor
cxGraph         =     kGraph                  ;graph mode
cxStatEdit      =     kStatEd                 ;statistics list editor
cxPrgmInput     =     kPrgmInput              ;programmed input
cxError         =     kError                  ;error handler
cxLinkIO        =     kLinkIO                 ;link i/o interface
cxMem           =     kResetMem               ;reset memory
cxDefMem        =     kResetDef               ;reset default
cxRAMApp        =     kChkRAM                 ;ram usage screen
cxMode          =     kMode                   ;mode settings screen
cxErase         =     kDelMem                 ;memory erase
cxPrgmCreate    =     kPrgmCr                 ;program create
cxTableEditor   =     kTable                  ;table editor
cxTableSet      =     kTblSet                 ;table set up
cxStatPlot      =     kStatP                  ;stat plots
cxFormat        =     kFormat                 ;format context
cxSolveRoot     =     kSolveRoot              ;solve root
cxextapps       =     kExtApps        ;053h   ; extra applications.     new
cxConstEd       =     kConstEd        ;056h   ; constant = editor.
cxPrgmSetVar    =     kPrgmSetVar     ;057h   ; setmenu in a program.

;============================================
;	scan code equates
;============================================

skDown          =	01h
skLeft          =	02h
skRight         =	03h
skUp            =	04h
skEnter         =	09h
skAdd           =	0Ah
skSub           =	0Bh
skMul           =	0Ch
skDiv           =	0Dh
skConst         =              0eh
skClear         =              0fh
skChs           =              11h
sk3             =              12h
sk6             =              13h
sk9             =              14h
skRParen        =              15h
skMixSimp       =              16h
skAppsMenu      =              17h
skDecPnt        =              19h
sk2             =              1ah
sk5             =              1bh
sk8             =              1ch
skLParen        =              1dh
skFracDec       =              1eh
skPrgm          =              1fh
skStatEd        =              20h
sk0             =              21h
sk1             =              22h
sk4             =              23h
sk7             =              24h
skPercent       =              25h
skFracSlash     =              26h
skExpon         =              27h
skDraw          =              28h
skStore         =              2ah
skComma         =              2bh
skVarX          =              2ch
skSimp          =              2dh
skUnit          =              2eh
skSquare        =              2fh
skMath          =              30h
skGraph         =              31h
skTrace         =              32h
skZoom          =              33h
skWindow        =              34h
skYEqu          =              35h
sk2nd           =              36h
skMode          =              37h
skDel           =              38h

;==========================================
;	tokens
;==========================================

EOSSTART        =       0
;
;
;   DISPLAY CONVERSIONS COME IMMEDIATELY BEFORE 'TSTORE'
;
DCONV   =     01h
;
tToDMS          =       DCONV       ; 01h
tToDEC          =       DCONV+1     ; 02h
tToAbc          =       DCONV+2     ; 03h    > A b/c
tUnredF         =     03h     ;down arrow for unreduced fraction token

tStore          =       DCONV+3     ; 04h Lstore                01
;
tBoxPlot        =       05h
;
BRACKS          =       06h
;
tLBrack         =       BRACKS      ; 06h '['
tRBrack         =       BRACKS+1    ; 07h ']'
tLBrace         =       BRACKS+2    ; 08h '{'
tRBrace         =       BRACKS+3    ; 09h '}'

tPOST1          =       BRACKS+4
;
tFromRad        =       tPOST1       ; 0Ah Lradian
tFromDeg        =       tPOST1+1     ; 0Bh Ldegree
tRecip          =       tPOST1+2     ; 0Ch Linverse
tSqr            =       tPOST1+3     ; 0Dh Lsquare
tTrnspos        =       tPOST1+4     ; 0Eh Ltranspose
tCube           =       tPOST1+5     ; 0Fh '^3'
;
tLParen         =       10h         ; 10h '('
tRParen         =       11h         ; 11h ')'

;   immediate unary tokens go from here to 'eosel-1'
;
IMUN            =       12h
;
tRound          =       IMUN        ; 12h 'round'
tPxTst          =       IMUN+1      ; 13h 'PXL-TEST'
tAug            =       IMUN+2      ; 14h 'aug'
tRowSwap        =       IMUN+3      ; 15h 'rSwap'
tRowPlus        =       IMUN+4      ; 16h 'rAdd'
tmRow           =       IMUN+5      ; 17h 'multR'
tmRowPlus       =       IMUN+6      ; 18h 'mRAdd'
tMax            =       IMUN+7      ; 19h 'max'
tMin            =       IMUN+8      ; 1Ah 'min'
tRToPr          =       IMUN+9      ; 1Bh 'R>Pr
tRToPo          =       IMUN+10     ; 1Ch 'R>Po
tPToRx          =       IMUN+11     ; 1Dh 'P>Rx
tPToRy          =       IMUN+12     ; 1Eh 'P>Ry
tMedian         =       IMUN+13     ; 1Fh 'MEDIAN
tRandM          =       IMUN+14     ; 20h 'randM'
tMean           =       IMUN+15     ; 21h
tLstMode        =       IMUN+16     ; 22h 'mode('
;the below tokens are immediate, but require an expression as 1st arg & so
;are grouped together...
tRoot           =       IMUN+17     ; 23h 'solve('
tSeries         =       IMUN+18     ; 24h 'seq'
tEvalF          =       IMUN+20     ; 26h             ;keep until sure.
IMUNEND         =     IMUN+20         ;end of immediate unaries.

tSolveEqual     =     IMUN+21         ;teq ('=') in solver -> tsolveequal.
tEOFrac         =     IMUN+22         ; 28h end of fraction in home screen.
;
;
;  token 29h cannot be an eos function since el(=29h already
;
tEOSEL          =       IMUN+23
;
tSpace          =       tEOSEL       ; 29h   ' '
tString         =       tEOSEL+1     ; 2Ah  '"'
tComma          =       tEOSEL+2     ; 2Bh  ','
tRemain         =     2ch     ;'r' for remainder display.
; tremain is needed in home screen where turn display chars into tokens.
; (there is no token for small 'r').
; ===== postfix functions
;
tPost           =       2eh
;
tFact           =       tPost       ; 2eh '!'
tPercent        =     tPost+1       ; 2fh '%'                 new

;=============number tokens

NUMS            =       30h
;
t0              =       NUMS        ; 30h
t1              =       NUMS+1      ; 31h
t2              =       NUMS+2      ; 32h
t3              =       NUMS+3      ; 33h
t4              =       NUMS+4      ; 34h
t5              =       NUMS+5      ; 35h
t6              =       NUMS+6      ; 36h
t7              =       NUMS+7      ; 37h
t8              =       NUMS+8      ; 38h
t9              =       NUMS+9      ; 39h
tDecPt          =       NUMS+10     ; 3Ah
tee             =       NUMS+11     ; 3Bh

;=======================
;   binary op
;=======================
tOr             =       3Ch         ; 3Ch  '_or_'
tXor            =       3Dh         ; 3Dh
;
tColon          =       3Eh         ; 3Eh ':'
tEnter          =       3Fh         ; 3Fh  Lenter
;
tAnd            =       40h         ; 40h  '_and_'       

;========================
;       letter tokens
;========================
LET             =       41h
;
tA              =       LET         ; 41h
tB              =       LET+1       ; 42h
tC              =       LET+2       ; 43h
tD              =       LET+3       ; 44h
tE              =       LET+4       ; 45h
tF              =       LET+5       ; 46h
tG              =       LET+6       ; 47h
tH              =       LET+7       ; 48h
tI              =       LET+8       ; 49h
tJ              =       LET+9       ; 4Ah
tK              =       LET+10      ; 4Bh
tL              =       LET+11      ; 4Ch
tM              =       LET+12      ; 4Dh
tN              =       LET+13      ; 4Eh
tO              =       LET+14      ; 4Fh
tP              =       LET+15      ; 50h
tQ              =       LET+16      ; 51h
tR              =       LET+17      ; 52h
tS              =       LET+18      ; 53h
tT              =       LET+19      ; 54h
tU              =       LET+20      ; 55h
tV              =       LET+21      ; 56h
tW              =       LET+22      ; 57h
tX              =       LET+23      ; 58h
tY              =       LET+24      ; 59h
tZ              =       LET+25      ; 5Ah
tEOF            =       LET+26      ; 5bh
;
;===============================================
;   these var tokens are 1st of a double token
;===============================================
;
;======================================================================
;   THESE VAR TOKENS ARE 1ST OF A DOUBLE TOKEN 
;======================================================================

;
vToks   =     LET+27    ;
;
;       USER MATRIX TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarMat         =       vToks       ; 5Ch
;
;       USER LIST TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarLst         =       vToks+1     ; 5Dh
;
;       USER EQUATION TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarEqu         =       vToks+2     ; 5Eh
tProg           =       vToks+3     ; 5Fh
;
;       USER PICT TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarPict        =       vToks+4     ; 60h
;
;       USER GDB TOKEN, 2ND TOKEN NEEDED FOR NAME
;
tVarGDB         =       vToks+5     ; 61h
tVarOut         =       vToks+6     ; 62h
tVarSys         =       vToks+7     ; 63h

;================================
; ===== mode setting commands
;================================
MODESA          =       vToks+8     ; 64h
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

;=======================
;   binary op
;=======================
tAdd            =       70h         ; 70h '+'
tSub            =       71h         ; 71h '-'
;
tAns            =       72h         ; 72h
;
;============================
;===== mode setting commands
;============================
;       graph format tokens are 2 byte tokens
;
tGFormat        =     73h
;
;
tBoyIcon        =     78h           ; for stat plots in programs
tTreeIcon       =     79h           ; for stat plots in programs
tDollarIcon     =     7ah           ; for stat plots in programs
tFaceIcon       =     7bh           ; for stat plots in programs
tCircleIcon     =     7ch           ; for stat plots in programs
tDiamondIcon    =     7dh           ; for stat plots in programs
tStarIcon       =     7eh           ; for stat plots in programs
tBoxIcon        =       7fh
tCrossIcon      =       80h
tDotIcon        =       81h
;
;===========================
;       binary op cont.
;===========================
tMul            =       82h         ; 82h  '*'
tDiv            =       83h         ; 83h  '/'
tIDiv           =       84h         ; 84h  'int/'
;
;============================
;       some graph commands
;============================
;
GRCMDS          =       85h
;
tTrace          =       GRCMDS      ; 84h 'Trace'
tClDrw          =       GRCMDS+1    ; 85h 'ClDrw'
tZoomStd        =       GRCMDS+2    ; 86h 'ZStd'
tZoomtrg        =       GRCMDS+3    ; 87h 'Ztrg'
tZoomBox        =       GRCMDS+4    ; 88h 'ZBOX'
tZoomIn         =       GRCMDS+5    ; 89h 'ZIn'
tZoomOut        =       GRCMDS+6    ; 8Ah 'ZOut'
tZoomSqr        =       GRCMDS+7    ; 8Bh 'ZSqr'
tZoomInt        =       GRCMDS+8    ; 8Ch 'ZInt'
tZoomPrev       =       GRCMDS+9    ; 8Dh 'ZPrev'
tZoomDec        =       GRCMDS+10   ; 8Eh 'ZDecm'
tZoomStat       =       GRCMDS+11   ; 8Fh 'ZStat
tZoomQuad1      =       GRCMDS+12   ; 91h 'quadrant 1'
tZoomSto        =       GRCMDS+14   ; 93h  zoom store
tText           =       GRCMDS+15   ; 94h
;
;=======================
;       binary op
;=======================
tnPr            =       GRCMDS+16   ; 94h '_nPr_'
tnCr            =       GRCMDS+17   ; 95h '_nCr_'

;=========================================================================
;       more graph commands (start of graph commands requiring arguments)
;=========================================================================

tYOn            =       GRCMDS+18   ; 96h 'FnOn_'
tYOff           =       GRCMDS+19   ; 97h 'FnOff_'
tStPic          =       GRCMDS+20   ; 98h 'StPic_'
tRcPic          =       GRCMDS+21   ; 99h 'RcPic_'
tLine           =       GRCMDS+22   ; 9bh 'line'
tVert           =       GRCMDS+23   ; 9ch 'vert_'
tPtOn           =       GRCMDS+24   ; 9dh 'pton'
tPtOff          =       GRCMDS+25   ; 9eh 'ptoff'
;
;===============================================================
;  token a0 cannot be an eos function since low mult=a0 already
;===============================================================
tPtChg          =       GRCMDS+26   ; 9fh 'ptchg'
tPXOn           =       GRCMDS+27   ; a0h
tPXOff          =       GRCMDS+28   ; a1h
tPXChg          =       GRCMDS+29   ; a2h
tShade          =       GRCMDS+30   ; a3h 'shade'
tCircl          =       GRCMDS+31   ; a4h 'circl'
tHorz           =       GRCMDS+32   ; a5h 'horizontal'
tTanLn          =       GRCMDS+33   ; a6h 'tanln'
;tManFit         =       GRCMDS+34   ; a7h 'manual fit'

;
;available      =       GRCMDS+35   ; a8h
;
tDrInv          =       GRCMDS+36   ; a9h 'drinv_'
tDrawF          =       GRCMDS+36   ; a9h 'drawf_'
;
tVarStrng       =       0aah
;
;===================================
; ===== functions with no arguments
;===================================
;
NOARG   =     0ABh
;
tRand           =       NOARG       ; ABh 'rand'
tPi             =       NOARG+1     ; ACh  Lpi
tGetKey         =       NOARG+2     ; ADh 'getKy'
;
;
tAPost          =       tGetKey+1   ; APOSTROPHY
tQuest          =       tAPost+1    ; QUESTION MARK
;
;
UNARY   =     tQuest+1              ; B0h
;
tChs            =       UNARY       ; B0h
tInt            =       UNARY+1     ; B1h
tAbs            =       UNARY+2     ; B2h
tDet            =       UNARY+3     ; B3h
tIdent          =       UNARY+4     ; B4h
tDim            =       UNARY+5     ; B5h
tSum            =       UNARY+6     ; B6h
tProd           =       UNARY+7     ; B7h
;tUnit            =       UNARY+8     ; B8h
tIPart          =       UNARY+9     ; B9h
tFPart          =       UNARY+10    ; BAh
;
;================================
;       new 2 byte tokens
;================================
t2ByteTok       =       0bbh         ;
;
;
UNARYLR =     UNARY+12
;
tSqrt           =       UNARYLR     ; BCh
tCubRt          =       UNARYLR+1   ; BDh
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
LASTUNARY2       =       UNARYLR+11  ; c7h
tMixSimp        =     UNARYLR+12 ;c8convert between mixed(ab/c) & simple(b/c)
tFracDec        =     UNARYLR+13 ;c9h convert between fraction & decimal.
tSimp           =     UNARYLR+14 ;cah simplify a fraction.

;=================
;  binary op
;=================
tPower          =       0cbh        ; cbh    '^'
tXRoot          =       0cch        ; cch    lsupx,lroot

;===================================
;       some programming commands
;===================================
;
PROGTOK =     UNARYLR+17
;
tSetMenu        =       PROGTOK     ; cdh
tIf             =       PROGTOK+1   ; ceh
tThen           =       PROGTOK+2   ; cfh
tElse           =       PROGTOK+3   ; d0h
tWhile          =       PROGTOK+4   ; d1h
tRepeat         =       PROGTOK+5   ; d2h
tFor            =       PROGTOK+6   ; d3h
tEnd            =       PROGTOK+7   ; d4h
tReturn         =       PROGTOK+8   ; d5h
tLbl            =       PROGTOK+9   ; d6h   'lbl_'
tGoto           =       PROGTOK+10  ; d7h   'goto_'
tPause          =       PROGTOK+11  ; d8h   'pause_'
tStop           =       PROGTOK+12  ; d9h   'stop'
tISG            =       PROGTOK+13  ; dah   'is>'
tDSL            =       PROGTOK+14  ; dbh   'ds<'
tInput          =       PROGTOK+15  ; dch   'input_'
tPrompt         =       PROGTOK+16  ; ddh   'prompt_'
tDisp           =       PROGTOK+17  ; deh   'disp_'
tDispG          =       PROGTOK+18  ; dfh   'dispg'
;
tOutput         =       PROGTOK+19  ; e0h    'outpt'
tClLCD          =       PROGTOK+20  ; e1h    'cllcd'
tFill           =       PROGTOK+21  ; e2h    'fill'
tSortA          =       PROGTOK+22  ; e3h    'sorta_'
tSortD          =       PROGTOK+23  ; e4h    'sortd_'
tDispTab        =       PROGTOK+24  ; e5h    'disp table
tMenu           =       PROGTOK+25  ; e6h    'menu'
tSendMBL        =       PROGTOK+26  ; e7h    'send'
tGetMBL         =       PROGTOK+27  ; e8h    'get'
;
;===================================
;       stat plot commands
;===================================
statPCmd        =       PROGTOK+28
;
tPlotOn         =       statPCmd    ; e9h     ' plotson'
tPlotOff        =       statPCmd+1   ; eah     ' plotsoff
CmdLen0         =     tPlotOff - PROGTOK+ 1
CmdOff1         =     CmdLen0 - 1
;
tListName       =       0ebh        ; list designator
;
tPlot1          =       0ech
tPlot2          =       0edh
tPlot3          =       0eeh

tUnit           =       0efh        ; efh unit for mixed fractions (a u b/c)
tFracSlash      =       0f0h         ; f0h '/' (bc symbol)

tStatCmd         =       0f2h
;
;==================================
; 2nd half of graph format tokens
;
; ===== format settings commands
;===================================

GFMT            =       1
;
tSeq            =       GFMT        ; 01  'seqg'
tSimulG         =       GFMT+1      ; 02  'simulg'
tCoordOn        =       GFMT+2      ; 03  'coordon'
tCoordOff       =       GFMT+3      ; 04  'coordoff'
tDrawLine       =       GFMT+4      ; 05  'drawline'
tDrawDot        =       GFMT+5      ; 06  'drawdot'
tAxisOn         =       GFMT+6      ; 07  'axeson'
tAxisOff        =       GFMT+7      ; 08  'axesoff'
tGridOn         =       GFMT+8      ; 09  'gridon'
tGridOff        =       GFMT+9      ; 0a  'gridoff'
tLblOn          =       GFMT+10     ; 0b  'labelon'
tLblOff         =       GFMT+11     ; 0c  'labeloff'
tPolarG         =       GFMT+12     ; 0d  'polargc'
tRectG          =       GFMT+13     ; 0e  'rectgc'
;
;=====================================
; 2nd half of mode setting tokens
;=====================================
MODES2          =       GFMT+14
;
tStndrd         =       MODES2      ; 0fh 'func'
tParam          =       MODES2+1    ; 10h 'param'
tPolar          =       MODES2+2    ; 11h 'pol'
tAFillOn        =       MODES2+3    ; 12h 'auto fill on
tAFillOff       =       MODES2+4    ; 13h
tACalcOn        =       MODES2+5    ; 14h
tACalcOff       =       MODES2+6    ; 15h 'autofill off
;
tMABC           =       MODES2+7    ; 16h 'aub/c mode'
tMBC            =       MODES2+8    ; 17h 'b/c mode'
tMAutoSimp      =       MODES2+9    ; 18h 'b/c mode'
tMManSimp       =       MODES2+10   ; 19h 'b/c mode'
tMSingleC       =       MODES2+11   ; 1ah single constant
tMMultiC        =       MODES2+12   ; 1bh multi constant


STATREGS        =     1
tOneVar         =     STATREGS      ; 01h    'onevar_'
tTwoVar         =     STATREGS+1    ; 02h
tLR1            =     STATREGS+2    ; 03h    'linr(ax+b
tManFit         =     STATREGS+5   ;07h     'manual fit'
tMedMed         =     STATREGS+6    ; 08h
tQuad           =     STATREGS+7    ; 09h
tClrLst         =     STATREGS+8    ; 0ah    'clear list
tClrTbl         =     STATREGS+9    ; 0bh    clear table
tHist           =     STATREGS+10   ; 0ch    'hist_'
tXYLine         =     STATREGS+11   ; 0dh    'xyline_'
tScatter        =     STATREGS+12   ; 0eh    'scatter_'
tLR             =     STATREGS+13   ; 0fh    'linr(a+bx

;======================================
;       2nd half of user list tokens
;======================================
tL1             =       01h         ; list 1
tL2             =       02h         ; list 2
tL3             =       03h         ; list 3
tL4             =       04h         ; list 4
tL5             =       05h         ; list 5
tL6             =       06h         ; list 6

;===========================================
;       2nd half of user equation tokens
;===========================================
;
tY1             =       10h         ; y1
tY2             =       11h         ; y2
tY3             =       12h         ; y3
tY4             =       13h         ; y4
;
;       constant equations have bit 7 set
tC1             =       80h         ; const1
tC2             =       81h         ; const2
tC3             =       82h         ; const3
tC4             =       83h         ; const4
;
;===========================================
;       2nd half of user picture tokens
;===========================================
tPic1           =       01h         ; pic1
tPic2           =       02h         ; pic2
tPic3           =       03h         ; pic3

;==============================================
;       2nd half of user graph database tokens
;==============================================
tGDB1           =       01h         ; gdb1
tGDB2           =       02h         ; gdb2
tGDB3           =       03h         ; gdb3
tGDB4           =       04h         ; gdb4
tGDB5           =       05h         ; gdb5
tGDB6           =       06h         ; gdb6
tGDB7           =       07h         ; gdb7
tGDB8           =       08h         ; gdb8
tGDB9           =       09h         ; gdb9
tGDB0           =       0ah         ; gdb0
;
;====================================
;       2nd half of string vars
;====================================
tStr1           =       01h
tStr2           =       02h
tStr3           =       03h
tStr4           =       04h
tStr5           =       05h
tStr6           =       06h
tStr7           =       07h
tStr8           =       08h
tStr9           =       09h
tStr0           =       0ah

;==================================================
;       2nd half of system output only variables
;==================================================
;
;   OPEN        =       00h
;
tRegEq          =       01h         ; REGRESSION EQUATION
;
tStatN          =       02h         ; STATISTICS N
;
tXMean          =       03h         ; X MEAN
tSumX           =       04h         ; SUM(X)
tSumXSqr        =       05h         ; SUM(X^2)
tStdX           =       06h         ; STANDARD DEV X
tStdPX          =       07h         ; STANDARD DEV POP X
tMinX           =       08h         ; Min X VALUE
tMaxX           =       09h         ; Max X VALUE
;
tMinY           =       0Ah         ; Min Y VALUE
tMaxY           =       0Bh         ; Max Y VALUE
tYmean          =       0Ch         ; Y MEAN
tSumY           =       0Dh         ; SUM(Y)
tSumYSqr        =       0Eh         ; SUM(Y^2)
tStdY           =       0Fh         ; STANDARD DEV Y
tStdPY          =       10h         ; STANDARD DEV POP Y
;
tSumXY          =       11h         ; SUM(XY)
tCorr           =       12h         ; CORRELATION
tMedX           =       13h         ; MED(X)
tQ1             =       14h         ; 1ST QUADRANT OF X
tQ3             =       15h         ; 3RD QUADRANT OF X
tQuadA          =       16h         ; 1ST TERM OF QUAD POLY REG/ Y-INT
tQuadB          =       17h         ; 2ND TERM OF QUAD POLY REG/ SLOPE
tQuadC          =       18h         ; 3RD TERM OF QUAD POLY REG
tCubeD          =       19h         ; 4TH TERM OF CUBIC POLY REG
tQuartE         =       1Ah         ; 5TH TERM OF QUART POLY REG
tMedX1          =       1Bh         ; x1 FOR MED-MED
tMedX2          =       1Ch         ; x2 FOR MED-MED
tMedX3          =       1Dh         ; x3 FOR MED-MED
tMedY1          =       1Eh         ; y1 FOR MED-MED
tMedY2          =       1Fh         ; y2 FOR MED-MED
tMedY3          =       20h         ; y3 FOR MED-MED
;
tLRSqr          =       21h         ; r^2
tBRSqr          =       22h         ; r^2
;
tError    =     23h       ; token in case of error in entry (ans only)
tDone     =     24h
;
;====================================
; system variable equates
;       vars with no restriction
;====================================
tuXscl       =  1
tuYscl       =  2
tXscl        =  3
tYscl        =  4

tXmin        =  0Ah
tXmax        =  0Bh
tYmin        =  0Ch
tYmax        =  0Dh
tTmin        =  0Eh
tTmax        =  0Fh
tThetaMin    =  10h
tThetaMax    =  11h
tuXmin       =  12h
tuXmax       =  13h
tuYmin       =  14h
tuYmax       =  15h
tuThetMin    =  16h
tuThetMax    =  17h
tuTmin       =  18h
tuTmax       =  19h
tTblMin      =  1Ah
tPlotStart   =  1Bh
tuPlotStart  =  1Ch
tnMax        =  1Dh
tunMax       =  1Eh
tnMin        =  1Fh
tunMin       =  20h

;
;       vars cannot be 0
;
tTblStep     =  21h
tTStep       =  22h
tThetaStep    =  23h
tuTStep      =  24h
tuThetStep     =  25h
;
;       special limits on these
;
tDeltaX      =  26h
tDeltaY      =  27h
;
;       special limits on these
;
tXFact       =  28h
tYFact       =  29h
;
tTblInput       =       2ah
;
;       no restrictions
tSimpFact       =     2bh     ;simplification factor.
;
tPlotStep       =       34h
tuPlotStep      =       35h
;
tXres           =       36h
tuXres          =       37h
;

;======================================
;2nd byte of 2byte tokens
;======================================

tLCM            =       01h         ; immediate unary
tGCD            =       02h         ; immediate unary
tRandInt        =       03h         ; immediate unary
tSubStrng       =       04h         ; immediate unary
tStdDev         =       05h         ; immediate unary
tVariance       =       06h         ; immediate unary
tInStrng        =       07h         ; immediate unary
tCoin           =       08h         ; immediate unary
tDice           =       09h         ; immediate unary
tRemainder      =       0ah         ; immediate unary
lastImUnary      =       0ah         ; last immediate unary token.
firstUnary      =       0bh         ; first unary operator.
tCumSum         =       0bh         ; unary
tExpr           =       0ch         ; unary                        do autop
tLength         =       0dh         ; unary                        do autop
tDeltaLst       =       0eh         ; unary                        do autop
tRef            =       0fh         ; unary                        no autop
autoP10         =       10h
tRRef           =       10h         ; unary                        no autop
newTok12         =    12h

autoP11         =       12h
tMatToLst       =       12h         ; command pushed               do autop
tLstToMat       =       13h         ; command pushed               do autop
tSetConst       =       14h         ;                                 autop
tGraphStyle     =       15h         ; command pushed                  autop
autoP12          =       15h
tSetUpLst       =       16h         ; command opt pushed           no autop
tExprOn         =       1ah         ; mode command
tExprOff        =       1bh         ; mode command
tClrSllLst      =       1ch         ; command not pushed
tGetCalc        =       1dh         ; command not pushed
tDelVar         =       1eh         ; command not pushed
tEquToStrng     =       1fh         ; command not pushed
tStrngToEqu     =       20h         ; command not pushed
tDelLast        =       21h         ; command not pushed
tSelect        =       22h         ; command not pushed
tModBox         =       23h         ; icon
tNormProb       =       24h         ; icon
tPictOlot       =       25h
tCirclePlot     =       26h
tStemPlot       =       27h
tBarPlot        =       28h
tZFit           =       29h         ; zoom fit
tDiag_On        =       2ah         ; dianostic display on
tDiag_Off       =       2bh         ; dianostic display off

;==========================================
;	data type equates
;===========================================
RealObj         =       0
ListObj         =       1
MatObj          =       2
EquObj          =       3
StrngObj        =       4
ProgObj         =       5
ProtProgObj     =       6
PictObj         =       7
GDBObj          =       8
UnknownObj      =       9
UnknownEquObj   =       0ah
NewEquObj       =       0bh
CplxObj         =       0ch
ClistObj        =       0dh
SlistObj        =       0dh   ;string list
UndefObj        =       0eh
WindowObj       =       0fh
ZStoObj         =       10h
TblrngObj       =       11h
LCDObj          =       12h
BackupObj       =       13h
FracObj         =       14h
FracRSObj       =       14h           ;reduced   simple fraction.
FracRMObj       =       15h           ;reduced   mixed  fraction.
FracUSObj       =       16h           ;unreduced simple fraction.
FracUMObj       =       17h           ;unreduced mixed fraction.
NameObj         =       18h           ;name string (will fit in fplen).
AppObj          =       19h           ;application, only used in menus/link
AppVarObj       =       1ah           ;application variable
TmpConstObj     =       1bh           ;temporary type of constant.

;======================================================================
;           i/o equates                                                          
;======================================================================
;
D0D1_bits	=  03h
D0LD1L		=  03h
D0LD1H		=  01h
D0HD1L		=  02h
D0HD1H		=  00h
bport		=  0			; 4-BIT LINK PORT (I/O) 
;
;======================================================================
;           device codes                                                       
;======================================================================
;
;
TI82DEV		=  82h
PC82DEV		=  02h
MAC82DEV	=  12h
;
TI83FDEV	=  73h
LINK83FDEV	=  23h
;
TI83DEV		=  83h
PC83DEV		=  03h
MAC83DEV	=  13h
;
TI85DEV		=  95h      ; DIFF THEN REAL 85 SO ME TALK
PC85DEV		=  05h
MAC85DEV	=  15h
;
TI73DEV		=  74h      ; Device x3 is always an 83
PC73DEV		=  07h
MAC73DEV	=  17h
;
TI83FDEV	=  73h
LINK73FDEV	=  23h
PC83FDEV	=  23h

;=========================================
;	system error codes
;=========================================

E_EDITF		=	7		;allow re-entering application
E_EDIT		=	1<<E_EDITF
E_Mask		=	7Fh

E_Overflow	=	1+E_EDIT
E_DivBy0	=	2+E_EDIT
E_SingularMat	=	3+E_EDIT
E_Domain	=	4+E_EDIT
E_Increment	=	5+E_EDIT
E_Break		=	6+E_EDIT
E_NonReal       =     7+E_EDIT
E_Syntax        =     8+E_EDIT
E_DataType      =     9+E_EDIT
E_Argument      =     10+E_EDIT
E_DimMismatch   =     11+E_EDIT
E_Dimension     =     12+E_EDIT
E_Undefined     =     13+E_EDIT
E_Memory        =     14+E_EDIT
E_Invalid       =     15+E_EDIT
E_IllegalNest   =     16+E_EDIT
E_Bound         =     17+E_EDIT
E_GraphRange    =     18+E_EDIT
E_Zoom          =     19+E_EDIT
E_Label         =     20
E_Stat          =     21
E_Solver        =     22+E_EDIT
E_Singularity   =     23+E_EDIT
E_SignChange    =     24+E_EDIT
E_Iterations    =     25+E_EDIT
E_BadGuess      =     26+E_EDIT
E_StatPlot      =     27
E_TolTooSmall   =     28+E_EDIT
E_Reserved      =     29+E_EDIT
E_Mode          =     30+E_EDIT
;
E_Scale         =     31+E_EDIT
;
E_LnkErr        =     32+E_EDIT
E_LnkMemErr     =     33+E_EDIT
E_LnkTransErr   =     34+E_EDIT
E_LnkDupErr     =     35+E_EDIT
E_LnkMemFull    =     36+E_EDIT
E_Unknown       =     37+E_EDIT
E_IdNotFound    =     38+E_EDIT
E_NoMode        =     39+E_EDIT
E_Validation    =     40+E_EDIT
E_Length        =     41+E_EDIT
E_Application   =     42+E_EDIT
E_AppErr1       =     43+E_EDIT
E_AppErr2       =     44+E_EDIT
E_ExpiredApp	=	45+E_EDIT
E_BadAddr	=	46+E_EDIT
HigErrNum	=	46+E_EDIT

; obsolete error numbers34              ;first link error
E_LinkIOChkSum	=	34
E_LinkIOTimeOut	=	35
E_LinkIOBusy	=	36
E_LinkIOVer	=	37

;==============================================
;       equates to ram locations for stat vars
;==============================================
;
StatN   =     statVars
XMean   =     StatN      + FPLEN
SumX    =     XMean      + FPLEN
SumXSqr =     SumX       + FPLEN
StdX    =     SumXSqr    + FPLEN
StdPX   =     StdX       + FPLEN
MinX    =     StdPX      + FPLEN
MaxX    =     MinX       + FPLEN
MinY    =     MaxX       + FPLEN
MaxY    =     MinY       + FPLEN
YMean   =     MaxY       + FPLEN
SumY    =     YMean      + FPLEN
SumYSqr =     SumY       + FPLEN
StdY    =     SumYSqr    + FPLEN
StdPY   =     StdY       + FPLEN
SumXY   =     StdPY      + FPLEN
Corr    =     SumXY      + FPLEN
MedX    =     Corr       + FPLEN
Q1      =     MedX       + FPLEN
Q3      =     Q1         + FPLEN
QuadA   =     Q3         + FPLEN
QuadB   =     QuadA      + FPLEN
QuadC   =     QuadB      + FPLEN
CubeD   =     QuadC      + FPLEN
QuartE  =     CubeD      + FPLEN
MedX1   =     QuartE     + FPLEN
MedX2   =     MedX1      + FPLEN
MedX3   =     MedX2      + FPLEN
MedY1   =     MedX3      + FPLEN
MedY2   =     MedY1      + FPLEN
MedY3   =     MedY2      + FPLEN
SLower  =     MedY3      + 2*FPLEN
SUpper  =     MedY3      + FPLEN

L3rd        =              OP6+4
R3rd        =              OP6+6
RidX        =              OP6
LidX        =              OP6+2