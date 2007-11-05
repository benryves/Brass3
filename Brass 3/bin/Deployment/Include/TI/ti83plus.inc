;======================================================================
; Include File for the TI-83 Plus
; Last Updated 9/17/2002 
;
; Copyright (c) 1999, 2001, 2002 Texas Instruments: The Licensed Materials are
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
;
;======================================================================
;           System Variable Equates
;======================================================================
;       Entry Points : RclSysTok, StoSysTok
;
XSCLt		=  2
YSCLt		=  3
XMINt		=  0Ah
XMAXt		=  0Bh
YMINt		=  0Ch
YMAXt		=  0Dh
TMINt		=  0Eh
TMAXt		=  0Fh
THETMINt	=  10h
THETMAXt	=  11h
TBLMINt		=  1Ah
PLOTSTARTt	=  1Bh
NMAXt		=  1Dh
NMINt		=  1Fh
TBLSTEPt	=  21h
TSTEPt		=  22h
THETSTEPt	=  23h
DELTAXt		=  26h
DELTAYt		=  27h
XFACTt		=  28h
YFACTt		=  29h
FINNt		=  2Bh
FINIt		=  2Ch
FINPVt		=  2Dh
FINPMTt		=  2Eh
FINFVt		=  2Fh
FINPYt		=  30h
FINCYt		=  31h
PLOTSTEPt	=  34h
XRESt		=  36h
;

;======================================================================
;           Run indicators
;======================================================================
;
busyPause	=	10101010b
busyNormal	=	11110000b
;
;======================================================================
; Macro to call base code from an app,
;======================================================================
;
rBR_CALL	=	28h		; NEEDED FOR APPS
BRT_JUMP0	=	50h
;
;======================================================================
;           Common subroutine RST numbers  
;======================================================================
;
rOP1TOOP2	=  08h
rFINDSYM	=  10h
rPUSHREALO1	=  18h
rMOV9TOOP1	=  20h
rFPADD		=  30h
;

;SYMBOLIC_DEBUG_START
;======================================================================
;           Entry points  
;======================================================================
;
_AbsO1O2Cp	= 410Eh
_AbsO1PAbsO2	= 405Ah
_ACos		= 40DEh
_ACosH		= 40F0h
_ACosRad	= 40D2h
_AdrLEle	= 462Dh
_AdrMEle	= 4609h
_AdrMRow	= 4606h
_AllEq		= 4876h
_AllocFPS	= 43A5h
_AllocFPS1	= 43A8h
_Angle		= 4102h
_AnsName	= 4B52h
_ApdSetup	= 4C93h
_AppGetCalc	= 4C78h
_AppGetCbl	= 4C75h
_AppInit	= 404Bh  ;***
_Arc_Unarc	= 4FD8h
_ASin		= 40E4h
_ASinH		= 40EDh
_ASinRad	= 40DBh
_ATan		= 40E1h
_ATan2		= 40E7h
_ATan2Rad	= 40D8h
_ATanH		= 40EAh
_ATanRad	= 40D5h

_BinOPExec	= 4663h
_Bit_VertSplit	= 4FA8h
_BufClr		= 5074h
_BufCpy		= 5071h

_CAbs		= 4E97h
_CAdd		= 4E88h
_CanAlphIns	= 4C69h
_CDiv		= 4E94h
_CDivByReal	= 4EBBh
_CEtoX		= 4EA9h
_CFrac		= 4EC1h
_CheckSplitFlag	= 49F0h
_Chk_Batt_Low	= 50B3h  ;***
_ChkFindSym	= 42F1h
_CIntgr		= 4EC4h
_CircCmd	= 47D4h
_CkInt		= 4234h
_CkOdd		= 4237h
_CkOP1C0	= 4225h
_CkOP1Cplx	= 40FCh
_CkOP1FP0	= 4228h
_CkOP1Pos	= 4258h
_CkOP1Real	= 40FFh
_CkOP2FP0	= 422Bh
_CkOP2Pos	= 4255h
_CkOP2Real	= 42DFh
_CkPosInt	= 4231h
_CkValidNum	= 4270h
_CleanAll	= 4A50h
_ClearRect	= 4D5Ch
_ClearRow	= 4CEDh
_CLine		= 4798h
_CLineS		= 479Bh
_CLN		= 4EA0h
_CLog		= 4EA3h
_CloseEditBuf	= 48D3h  ;***
_CloseEditBufNoR	= 476Eh
_CloseEditEqu	= 496Ch  ;***
_CloseProg	= 4A35h
_ClrGraphRef	= 4A38h
_ClrLCD		= 4543h
_ClrLCDFull	= 4540h
_ClrLp		= 41D1h
_ClrOP1S	= 425Eh
_ClrOP2S	= 425Bh
_ClrScrn	= 4549h
_ClrScrnFull	= 4546h
_ClrTxtShd	= 454Ch
_CMltByReal	= 4EB8h
_CmpSyms	= 4A4Ah
_CMult		= 4E8Eh
_Conj		= 4EB5h
_ConvDim	= 4B43h
_ConvDim00	= 4B46h
_ConvLcToLr	= 4A23h
_ConvLrToLc	= 4A56h
_ConvOP1	= 4AEFh
_COP1Set0	= 4105h
_Cos		= 40C0h
_CosH		= 40CCh
_CpHLDE		= 400Ch
_CPoint		= 4DC8h
_CPointS	= 47F5h
_CpOP1OP2	= 4111h
_CpOP4OP3	= 4108h
_CpyO1ToFPS1	= 445Ch
_CpyO1ToFPS2	= 446Bh
_CpyO1ToFPS3	= 4477h
_CpyO1ToFPS4	= 4489h
_CpyO1ToFPS5	= 4483h
_CpyO1ToFPS6	= 447Dh
_CpyO1ToFPS7	= 4480h
_CpyO1ToFPST	= 444Ah
_CpyO2ToFPS1	= 4459h
_CpyO2ToFPS2	= 4462h
_CpyO2ToFPS3	= 4474h
_CpyO2ToFPS4	= 4486h
_CpyO2ToFPST	= 4444h
_CpyO3ToFPS1	= 4453h
_CpyO3ToFPS2	= 4465h
_CpyO3ToFPST	= 4441h
_CpyO5ToFPS1	= 4456h
_CpyO5ToFPS3	= 4471h
_CpyO6ToFPS2	= 4468h
_CpyO6ToFPST	= 4447h
_CpyStack	= 4429h
_CpyTo1FPS1	= 4432h
_CpyTo1FPS10	= 43F3h
_CpyTo1FPS11	= 43D8h
_CpyTo1FPS2	= 443Bh
_CpyTo1FPS3	= 4408h
_CpyTo1FPS4	= 440Eh
_CpyTo1FPS5	= 43DEh
_CpyTo1FPS6	= 43E4h
_CpyTo1FPS7	= 43EAh
_CpyTo1FPS8	= 43EDh
_CpyTo1FPS9	= 43F6h
_CpyTo1FPST	= 4423h
_CpyTo2FPS1	= 442Fh
_CpyTo2FPS2	= 4438h
_CpyTo2FPS3	= 4402h
_CpyTo2FPS4	= 43F9h
_CpyTo2FPS5	= 43DBh
_CpyTo2FPS6	= 43E1h
_CpyTo2FPS7	= 43E7h
_CpyTo2FPS8	= 43F0h
_CpyTo2FPST	= 4420h
_CpyTo3FPS1	= 442Ch
_CpyTo3FPS2	= 4411h
_CpyTo3FPST	= 441Dh
_CpyTo4FPST	= 441Ah
_CpyTo5FPST	= 4414h
_CpyTo6FPS2	= 43FFh
_CpyTo6FPS3	= 43FCh
_CpyTo6FPST	= 4417h
_CpyToFPS1	= 445Fh
_CpyToFPS2	= 446Eh
_CpyToFPS3	= 447Ah
_CpyToFPST	= 444Dh
_CpyToStack	= 4450h
_Create0Equ	= 432Ah
_CreateAppVar	= 4E6Ah
_CreateCList	= 431Bh
_CreateCplx	= 430Ch
_CreateEqu	= 4330h
_CreatePair	= 4B0Dh
_CreatePict	= 4333h
_CreateProg	= 4339h
_CreateProtProg	= 4E6Dh
_CreateReal	= 430Fh
_CreateRList	= 4315h
_CreateRMat	= 4321h
_CreateStrng	= 4327h
_CRecip		= 4E91h
_CSqRoot	= 4E9Dh
_CSquare	= 4E8Bh
_CSub		= 4E85h
_CTenX		= 4EA6h
_CTrunc		= 4EBEh
_Cube		= 407Bh
_CursorOff	= 45BEh
_CursorOn	= 45C4h
_CXrootY	= 4EACh
_CYtoX		= 4EB2h

_DarkLine	= 47DDh
_DarkPnt	= 47F2h
_DataSize	= 436Ch
_DataSizeA	= 4369h
_DeallocFPS	= 439Fh
_DeallocFPS1	= 43A2h
_DecO1Exp	= 4267h
_DelListEl	= 4A2Fh
_DelMem		= 4357h
_DelRes		= 4A20h
_DelVar		= 4351h
_DelVarArc	= 4FC6h
_DelVarNoArc	= 4FC9h
_DisableApd	= 4C84h
_Disp		= 4F45h
_DispDone	= 45B5h
_DispEOL	= 45A6h
_DispHL		= 4507h
_DisplayImage	= 4D9Bh
_DispMenuTitle	= 5065h
_DispOP1A	= 4BF7h
_DivHLBy10	= 400Fh
_DivHLByA	= 4012h
_DrawCirc2	= 4C66h
_DrawCmd	= 48C1h
_DrawRectBorder	= 4D7Dh
_DrawRectBorderClear	= 4D8Ch
_DToR		= 4075h

_EditProg	= 4A32h
_EnableApd	= 4C87h
_EnoughMem	= 42FDh
_EOP1NotReal	= 4279h
_Equ_or_NewEqu	= 42C4h
_EraseEOL	= 4552h
_EraseRectBorder	= 4D86h
_ErrArgument	= 44ADh
_ErrBadGuess	= 44CBh
_ErrBreak	= 44BFh
_ErrD_OP1_0	= 42D3h
_ErrD_OP1_LE_0	= 42D0h
_ErrD_OP1Not_R	= 42CAh
_ErrD_OP1NotPos	= 42C7h
_ErrD_OP1NotPosInt	= 42CDh
_ErrDataType	= 44AAh
_ErrDimension	= 44B3h
_ErrDimMismatch	= 44B0h
_ErrDivBy0	= 4498h
_ErrDomain	= 449Eh
_ErrIncrement	= 44A1h
_ErrInvalid	= 44BCh
_ErrIterations	= 44C8h
_ErrLinkXmit	= 44D4h
_ErrMemory	= 44B9h
_ErrNon_Real	= 44A4h
_ErrNonReal	= 4A8Ch
_ErrNotEnoughMem	= 448Ch
_ErrOverflow	= 4495h
_ErrSignChange	= 44C5h
_ErrSingularMat	= 449Bh
_ErrStat	= 44C2h
_ErrStatPlot	= 44D1h
_ErrSyntax	= 44A7h
_ErrTolTooSmall	= 44CEh
_ErrUndefined	= 44B6h
_EToX		= 40B4h
_Exch9		= 43D5h
_ExLp		= 4222h
_ExpToHex	= 424Fh

_Factorial	= 4B85h
_FillRect	= 4D62h
_FillRectPattern	= 4D89h
_Find_Parse_Formula	= 4AF2h
_FindAlphaDn	= 4A47h
_FindAlphaUp	= 4A44h
_FindApp		= 4C4Eh
_FindAppDn		= 4C4Bh
_FindAppNumPages	= 509Bh
_FindAppUp		= 4C48h
_FindSym	= 42F4h
_FiveExec	= 467Eh
_FixTempCnt	= 4A3Bh
_FlashToRam	= 5017h
_ForceFullScreen	= 508Fh
_FormBase	= 50AAh
_FormDCplx	= 4996h
_FormEReal	= 4990h
_FormReal	= 4999h
_FourExec	= 467Bh
_FPAdd		= 4072h
_FPDiv		= 4099h
_FPMult		= 4084h
_FPRecip	= 4096h
_FPSquare	= 4081h
_FPSub		= 406Fh
_Frac		= 4093h

_Get_Tok_Strng	= 4594h  ;***
_GetBaseVer	= 4C6Fh
_GetCSC		= 4018h
_GetKey		= 4972h
_GetLToOP1	= 4636h
_GetMToOP1	= 4615h
_GetSysInfo 	= 50DDh  ; v. 1.13+ only
_GetTokLen	= 4591h  ;***
_GrBufClr	= 4BD0h
_GrBufCpy	= 486Ah
_GrphCirc	= 47D7h

_HLTimes9	= 40F9h
_HorizCmd	= 48A6h
_HTimesL	= 4276h

_IBounds	= 4C60h
_IBoundsFull	= 4D98h
_ILine		= 47E0h
_IncLstSize	= 4A29h
_InsertList	= 4A2Ch
_InsertMem	= 42F7h
_Int		= 40A5h
_Intgr		= 405Dh
_InvCmd		= 48C7h
_InvertRect	= 4D5Fh
_InvOP1S	= 408Dh
_InvOP1SC	= 408Ah
_InvOP2S	= 4090h
_InvSub		= 4063h
_IOffset	= 4C63h
_IPoint		= 47E3h
_IsA2ByteTok	= 42A3h  ;***
_IsEditEmpty	= 492Dh  ;***

_JError		= 44D7h
_JErrorNo	= 4000h
_JForceCmdNoChar	= 4027h
_JForceGraphKey	= 5005h
_JForceGraphNoKey	= 5002h

_KeyToString	= 45CAh

_LCD_BUSY	= 4051h  ;***
_LCD_DRIVERON	= 4978h  ;***
_LdHLind	= 4009h  ;***
_LineCmd	= 48ACh
_LnX		= 40ABh
_Load_SFont	= 4783h
_LoadCIndPaged	= 501Dh
_LoadDEIndPaged	= 501Ah
_LoadPattern	= 4CB1h
_LogX		= 40AEh

_Max		= 4057h
_MemChk		= 42E5h
_MemClear	= 4C30h
_MemSet		= 4C33h
_Min		= 4054h
_Minus1		= 406Ch
_Mov10B		= 415Ch
_Mov18B		= 47DAh
_Mov7B		= 4168h
_Mov8B		= 4165h
_Mov9B		= 415Fh
_Mov9OP1OP2	= 417Dh
_Mov9OP2Cp	= 410Bh
_Mov9ToOP1	= 417Ah
_Mov9ToOP2	= 4180h
_MovFrOP1	= 4183h

_NewLine	= 452Eh  ;***
_NZIf83Plus	= 50E0h  ; v. 1.13+ only

_OneVar		= 4BA3h
_OP1ExOP2	= 421Fh
_OP1ExOP3	= 4219h
_OP1ExOP4	= 421Ch
_OP1ExOP5	= 420Dh
_OP1ExOP6	= 4210h
_OP1ExpToDec	= 4252h
_OP1Set0	= 41BFh
_OP1Set1	= 419Bh
_OP1Set2	= 41A7h
_OP1Set3	= 41A1h
_OP1Set4	= 419Eh
_OP1ToOP2	= 412Fh
_OP1ToOP3	= 4123h
_OP1ToOP4	= 4117h
_OP1ToOP5	= 4153h
_OP1ToOP6	= 4150h
_OP2ExOP4	= 4213h
_OP2ExOP5	= 4216h
_OP2ExOP6	= 4207h
_OP2Set0	= 41BCh
_OP2Set1	= 41ADh
_OP2Set2	= 41AAh
_OP2Set3	= 4198h
_OP2Set4	= 4195h
_OP2Set5	= 418Fh
_OP2Set60	= 4AB0h
_OP2Set8	= 418Ch
_OP2SetA	= 4192h
_OP2ToOP1	= 4156h
_OP2ToOP3	= 416Eh
_OP2ToOP4	= 411Ah
_OP2ToOP5	= 414Ah
_OP2ToOP6	= 414Dh
_OP3Set0	= 41B9h
_OP3Set1	= 4189h
_OP3Set2	= 41A4h
_OP3ToOP1	= 413Eh
_OP3ToOP2	= 4120h
_OP3ToOP4	= 4114h
_OP3ToOP5	= 4147h
_OP4Set0	= 41B6h
_OP4Set1	= 4186h
_OP4ToOP1	= 4138h
_OP4ToOP2	= 411Dh
_OP4ToOP3	= 4171h
_OP4ToOP5	= 4144h
_OP4ToOP6	= 4177h
_OP5ExOP6	= 420Ah
_OP5Set0	= 41B3h
_OP5ToOP1	= 413Bh
_OP5ToOP2	= 4126h
_OP5ToOP3	= 4174h
_OP5ToOP4	= 412Ch
_OP5ToOP6	= 4129h
_OP6ToOP1	= 4135h
_OP6ToOP2	= 4132h
_OP6ToOP5	= 4141h
_OutputExpr	= 4BB2h

_PagedGet	= 5023h
_ParseInp	= 4A9Bh
_PDspGrph	= 48A3h
_PixelTest	= 48B5h
_Plus1		= 4069h
_PointCmd	= 48B2h
_PointOn	= 4C39h
_PopMCplxO1	= 436Fh  ;***
_PopOP1		= 437Eh
_PopOP3		= 437Bh
_PopOP5		= 4378h
_PopReal	= 4393h
_PopRealO1	= 4390h
_PopRealO2	= 438Dh
_PopRealO3	= 438Ah
_PopRealO4	= 4387h
_PopRealO5	= 4384h
_PopRealO6	= 4381h
_PosNo0Int	= 422Eh
_PToR		= 40F3h
_PushMCplxO1	= 43CFh  ;***
_PushMCplxO3	= 43C6h  ;***
_PushOP1	= 43C9h
_PushOP3	= 43C3h
_PushOP5	= 43C0h
_PushReal	= 43BDh
_PushRealO1	= 43BAh
_PushRealO2	= 43B7h
_PushRealO3	= 43B4h
_PushRealO4	= 43B1h
_PushRealO5	= 43AEh
_PushRealO6	= 43ABh
_PutAway	= 403ch  ;***
_PutC		= 4504h
_PutMap		= 4501h
_PutPS		= 4510h
_PutPSB		= 450dh  ;***
_PutS		= 450Ah
_PutTokString	= 4960h
_PutToL		= 4645h
_PutToMat	= 461Eh

_RandInit	= 4B7Fh
_Random		= 4B79h
_Rcl_StatVar	= 42DCh
_RclAns		= 4AD7h
_RclGDB2	= 47D1h
_RclN		= 4ADDh
_RclSysTok	= 4AE6h
_RclVarSym	= 4AE3h
_RclX		= 4AE0h
_RclY		= 4ADAh
_Rec1stByte	= 4EFAh
_Rec1stByteNC	= 4EFDh
_RecAByteIO	= 4F03h
_RedimMat	= 4A26h
_Regraph	= 488Eh
_ReleaseBuffer	= 4771h  ;***
_ReloadAppEntryVecs	= 4C36h
_RestoreDisp	= 4870h
_RName		= 427Fh
_RndGuard	= 409Fh
_RnFx		= 40A2h
_Round		= 40A8h
_RToD		= 4078h
_RToP		= 40F6h
_RunIndicOff	= 4570h
_RunIndicOn	= 456Dh

_SaveDisp	= 4C7Bh
_SendAByte	= 4EE5h
_SendVarCmd	= 4A14h ;***
_SetAllPlots	= 4FCCh
_SetExSpeed 	= 50BFh ; v. 1.13+ only
_SetFuncM	= 4840h
_SetNorm_Vals	= 49FCh
_SetParM	= 4849h
_SetPolM	= 4846h
_SetSeqM	= 4843h
_SetTblGraphDraw	= 4C00h
_SetupPagedPtr	= 5020h
_SetXXOP1	= 478Ch
_SetXXOP2	= 478Fh
_SetXXXXOP2	= 4792h
_SFont_Len	= 4786h
_ShRAcc		= 41D4h
_Sin		= 40BDh
_SinCosRad	= 40BAh
_SinH		= 40CFh
_SinHCosH	= 40C6h
_SqRoot		= 409Ch
_SrchVLstDn	= 4F12h
_SrchVLstUp	= 4F0Fh
_SStringLength	= 4CB4h
_StMatEl	= 4AE9h
_StoAns		= 4ABFh
_StoGDB2	= 47CEh
_StoN		= 4ACBh
_StoOther	= 4AD4h
_StoR		= 4AC5h
_StoRand	= 4B7Ch
_StoSysTok	= 4ABCh
_StoT		= 4ACEh
_StoTheta	= 4AC2h
_StoX		= 4AD1h
_StoY		= 4AC8h
_StrCopy	= 44E3h
_StrLength	= 4C3Fh   

_Tan		= 40C3h
_TanH		= 40C9h
_TanLnF		= 48BBh
_TenX		= 40B7h
_ThetaName	= 427Ch
_ThreeExec	= 4675h
_Times2		= 4066h
_TimesPt5	= 407Eh
_TName		= 428Eh
_ToFrac		= 4657h
_Trunc		= 4060h
_TwoVarSet	= 50D4h  ; v. 1.13+ only

_UCLineS	= 4795h
_UnLineCmd	= 48AFh
_UnOPExec	= 4672h

_VertCmd	= 48A9h
_VPutMap	= 455Eh
_VPutS		= 4561h
_VPutSN		= 4564h
_VtoWHLDE	= 47FBh


_XftoI		= 4804h
_Xitof		= 47FEh
_XName		= 4288h
_XRootY		= 479Eh

_YftoI		= 4801h
_YName		= 428Bh
_YToX		= 47A1h

_Zero16D	= 41B0h
_ZeroOP		= 41CEh
_ZeroOP1	= 41C5h
_ZeroOP2	= 41C8h
_ZeroOP3	= 41CBh
_ZmDecml	= 484Fh
_ZmFit		= 485Bh
_ZmInt		= 484Ch
_ZmPrev		= 4852h
_ZmSquare	= 485Eh
_ZmStats	= 47A4h
_ZmTrig		= 4861h
_ZmUsr		= 4855h
_ZooDefault	= 4867h
;
;======================================================================
;           System-defined RAM Variable Address Equates
;======================================================================
;

ramStart             =  8000h
appData              =  8000h
ramCode              =  8100h
ramCodeEnd           =  822Fh
baseAppBrTab         =  8230h
bootTemp             =  8251h
appSearchPage        =  82A3h
tempSwapArea         =  82A5h
appID                =  838Dh
ramReturnData        =  83EDh
arcInfo              =  83EEh
savedArcInfo         =  8406h
appInfo              =  8432h
appBank_jump         =  843Ch
appPage              =  843Eh
kbdScanCode          =  843Fh
kbdKey               =  8444h
kbdGetKy             =  8445h
keyExtend            =  8446h
contrast             =  8447h
apdSubTimer          =  8448h
apdTimer             =  8449h
curTime              =  844Ah
curRow               =  844Bh
curCol               =  844Ch
curOffset            =  844Dh
curUnder             =  844Eh
curY                 =  844Fh
curType              =  8450h
curXRow              =  8451h
prevDData            =  8452h
lFont_record         =  845Ah
sFont_record         =  8462h
tokVarPtr            =  846Ah
tokLen               =  846Ch
indicMem             =  846Eh
indicCounter         =  8476h
indicBusy            =  8477h
OP1                  =  8478h
OP1M                 =  847Ah
OP2                  =  8483h
OP2M                 =  8485h
OP2EXT               =  848Ch
OP3                  =  848Eh
OP3M                 =  8490h
OP4                  =  8499h
OP4M                 =  849Bh
OP5                  =  84A4h
OP5M                 =  84A6h
OP6                  =  84AFh
OP6M                 =  84B1h
OP6EXT               =  84B8h
progToEdit           =  84BFh
nameBuff             =  84C7h
equ_edit_save        =  84D2h
iMathPtr1            =  84D3h
iMathPtr2            =  84D5h
iMathPtr3            =  84D7h
iMathPtr4            =  84D9h
iMathPtr5            =  84DBh
chkDelPtr1           =  84DDh
chkDelPtr2           =  84DFh
insDelPtr            =  84E1h
upDownPtr            =  84E3h
fOutDat              =  84E5h
asm_data_ptr1        =  84EBh
asm_data_ptr2        =  84EDh
asm_sym_ptr1         =  84EFh
asm_sym_ptr2         =  84F1h
asm_ram              =  84F3h
asm_ind_call         =  8507h
textShadow           =  8508h
textShadCur          =  8588h
textShadTop          =  858Ah
textShadAlph         =  858Bh
textShadIns          =  858Ch
cxMain               =  858Dh
cxPPutAway           =  858Fh
cxPutAway            =  8591h
cxRedisp             =  8593h
cxErrorEP            =  8595h
cxSizeWind           =  8597h
cxPage               =  8599h
cxCurApp             =  859Ah
cxPrev               =  859Bh
monQH                =  85AAh
monQT                =  85ABh
monQueue             =  85ACh
onSP                 =  85BCh
promptRow            =  85C0h
promptCol            =  85C1h
promptIns            =  85C2h
promptShift          =  85C3h
promptRet            =  85C4h
promptValid          =  85C6h
promptTop            =  85C8h
promptCursor         =  85CAh
promptTail           =  85CCh
promptBtm            =  85CEh
varType              =  85D0h
varCurrent           =  85D1h
varClass             =  85D9h
menuActive           =  85DCh
menuAppDepth         =  85DDh
MenuCurrent          =  85DEh
ProgCurrent          =  85E8h
userMenuSA           =  85FEh
ioPrompt             =  865Fh
dImageWidth          =  8660h
RectFillPHeight      =  8660h
RectFillPWidth       =  8661h
RectFillPattern      =  8662h
ioFlag               =  8670h
sndRecState          =  8672h
ioErrState           =  8673h
header               =  8674h
ioData               =  867Dh
ioNewData            =  8689h
bakHeader            =  868Bh
penCol               =  86D7h
penRow               =  86D8h
rclQueue             =  86D9h
rclQueueEnd          =  86DBh
errNo                =  86DDh
errSP                =  86DEh
errOffset            =  86E0h
saveSScreen          =  86ECh
bstCounter           =  89EEh
flags                =  89F0h
statVars             =  8A3Ah
anovaf_vars          =  8C17h
infVars              =  8C4Dh
curGStyle            =  8D17h
curGY                =  8D18h
curGX                =  8D19h
curGY2               =  8D1Ah
curGX2               =  8D1Bh
freeSaveY            =  8D1Ch
freeSaveX            =  8D1Dh
XOffset              =  8DA1h
YOffset              =  8DA2h
lcdTallP             =  8DA3h
pixWideP             =  8DA4h
pixWide_m_1          =  8DA5h
pixWide_m_2          =  8DA6h
lastEntryPTR         =  8DA7h
lastEntryStk         =  8DA9h
numLastEntries       =  8E29h
currLastEntry        =  8E2Ah
curInc               =  8E67h
uXmin                =  8E7Eh
uXmax                =  8E87h
uXscl                =  8E90h
uYmin                =  8E99h
uYmax                =  8EA2h
uYscl                =  8EABh
uThetMin             =  8EB4h
uThetMax             =  8EBDh
uThetStep            =  8EC6h
uTmin                =  8ECFh
uTmax                =  8ED8h
uTStep               =  8EE1h
uPlotStart           =  8EEAh
unMax                =  8EF3h
uu0                  =  8EFCh
uv0                  =  8F05h
unMin                =  8F0Eh
uu02                 =  8F17h
uv02                 =  8F20h
uw0                  =  8F29h
uPlotStep            =  8F32h
uXres                =  8F3Bh
uw02                 =  8F44h
Xmin                 =  8F50h
Xmax                 =  8F59h
Xscl                 =  8F62h
Ymin                 =  8F6Bh
Ymax                 =  8F74h
Yscl                 =  8F7Dh
ThetaMin             =  8F86h
ThetaMax             =  8F8Fh
ThetaStep            =  8F98h
TminPar              =  8FA1h
TmaxPar              =  8FAAh
Tstep                =  8FB3h
PlotStart            =  8FBCh
nMax                 =  8FC5h
u0                   =  8FCEh
v0                   =  8FD7h
nMin                 =  8FE0h
u02                  =  8FE9h
v02                  =  8FF2h
w0                   =  8FFBh
PlotStep             =  9004h
XresO                =  900Dh
w02                  =  9016h
un1                  =  901Fh
un2                  =  9028h
vn1                  =  9031h
vn2                  =  903Ah
wn1                  =  9043h
wn2                  =  904Ch
fin_N                =  9055h
fin_I                =  905Eh
fin_PV               =  9067h
fin_PMT              =  9070h
fin_FV               =  9079h
fin_PY               =  9082h
fin_CY               =  908Bh
cal_N                =  9094h
cal_I                =  909Dh
cal_PV               =  90A6h
cal_PMT              =  90AFh
cal_FV               =  90B8h
cal_PY               =  90C1h
smallEditRAM         =  90D3h
XFact                =  913Fh
YFact                =  9148h
Xres_int             =  9151h
deltaX               =  9152h
deltaY               =  915Bh
shortX               =  9164h
shortY               =  916Dh
lower                =  9176h
upper                =  917Fh
XOutSym              =  918Ch
XOutDat              =  918Eh
YOutSym              =  9190h
YOutDat              =  9192h
inputSym             =  9194h
inputDat             =  9196h
prevData             =  9198h
TblMin               =  92B3h
TblStep              =  92BCh
P1Type		     =  92C9h
SavX1List	     =  92CAh
SavY1List	     =  92CFh
SavF1List	     =  92D4h
P1FrqOnOff	     =  92D9h
P2Type		     =  92DAh
SavX2List	     =  92DBh
SavY2List	     =  92E0h
SavF2List	     =  92E5h
P2FrqOnOff	     =  92EAh
P3Type		     =  92EBh	
SavX3List	     =  92ECh
SavY3List	     =  92F1h
SavF3List	     =  92F6h
P3FrqOnOff	     =  92FBh
plotSScreen          =  9340h
seed1                =  9640h
seed2                =  9649h
cmdShadow            =  966Eh
cmdShadCur           =  96EEh
cmdShadAlph          =  96F0h
cmdShadIns           =  96F1h
cmdCursor            =  96F2h
editTop              =  96F4h
editCursor           =  96F6h
editTail             =  96F8h
editBtm              =  96FAh
editSym              =  9706h
editDat              =  9708h
GY0		= 977Fh
GY1		= 9776h
GY2		= 9777h
GY3		= 9778h
GY4		= 9779h
GY5		= 977Ah
GY6		= 977Bh
GY7		= 977Ch
GY8		= 977Dh
GY9		= 977Eh
GX1		= 9780h
GX2		= 9781h
GX3		= 9782h
GX4		= 9783h
GX5		= 9784h
GX6		= 9785h
GU		= 978Ch
GV		= 978Dh
GW		= 978Eh
GR1		= 9786h
GR2		= 9787h
GR3		= 9788h
GR4		= 9789h
GR5		= 978Ah
GR6		= 978Bh
winTop               =  97A5h
winBtm               =  97A6h
winLeftEdge          =  97A7h
winLeft              =  97A8h
winAbove             =  97AAh
winRow               =  97ACh
winCol               =  97AEh
fmtDigits            =  97B0h
fmtString            =  97B1h
fmtConv              =  97F2h
fmtLeft              =  9804h
fmtIndex             =  9806h
fmtMatSym            =  9808h
fmtMatMem            =  980Ah
EQS                  =  980Ch
tSymPtr1             =  9818h
tSymPtr2             =  981Ah
chkDelPtr3           =  981Ch
chkDelPtr4           =  981Eh
tempMem              =  9820h
fpBase               =  9822h
FPS                  =  9824h
OPBase               =  9826h
OPS                  =  9828h
pTempCnt             =  982Ah
cleanTmp             =  982Ch
pTemp                =  982Eh
progPtr              =  9830h
newDataPtr           =  9832h
pagedCount           =  9834h
pagedPN              =  9835h
pagedGetPtr          =  9836h
pagedPutPtr          =  9838h
pagedBuf             =  983Ah
appErr1              =  984Dh
appErr2              =  985Ah
flashByte1           =  9867h
flashByte2           =  9868h
freeArcBlock         =  9869h
arcPage              =  986Bh
arcPtr               =  986Ch
appRawKeyHandle      =  9870h
appBackUpScreen      =  9872h
customHeight         =  9B72h
localLanguage        =  9B73h
cursorHookPtr        =  9B7Ch
rawKeyHookPtr        =  9B84h
getKeyHookPtr        =  9B88h
fontHookPtr          =  9B9Ch
restartClr           =  9BD0h
localTokStr          =  9D65h
keyForStr            =  9D76h
keyToStrRam          =  9D77h
sedMonSp             =  9D88h
bpSave               =  9D8Ah
userMem              =  9D95h
symTable             =  0FE66h

;SYMBOLIC_DEBUG_END
;
;======================================================================
;		Language localization equates
;======================================================================
LANG_NEUTRAL                       =   00h
LANG_DANISH                        =   06h
LANG_DUTCH                         =   13h
LANG_ENGLISH                       =   09h
LANG_FINNISH                       =   0bh
LANG_FRENCH                        =   0ch
LANG_GERMAN                        =   07h
LANG_HUNGARIAN                     =   0eh
LANG_ITALIAN                       =   10h
LANG_NORWEGIAN                     =   14h
LANG_POLISH                        =   15h
LANG_PORTUGUESE                    =   16h
LANG_SPANISH                       =   0ah
LANG_SWEDISH                       =   1dh

SUBLANG_NEUTRAL                    =   00h   ; language neutral
SUBLANG_DUTCH                    =   01h   ; Dutch
SUBLANG_ENGLISH_US               =   01h   ; English (USA)
SUBLANG_FRENCH                   =   01h   ; French
SUBLANG_GERMAN                   =   01h   ; German
SUBLANG_ITALIAN                  =   01h   ; Italian
SUBLANG_NORWEGIAN_BOKMAL         =   01h   ; Norwegian (Bokmal)
SUBLANG_PORTUGUESE               =   02h   ; Portuguese
SUBLANG_SPANISH                  =   01h   ; Spanish (Castilian)


;======================================================================
;           Error handler equates/macros 
;======================================================================
;
APP_PUSH_ERRORH =	59h
APP_POP_ERRORH	=	5Ch
;
;
;======================================================================
;           System and State Flags
;======================================================================
;

trigFlags	=  0		;Trigonometry mode settings
trigDeg 	=    2	; 1=degrees, 0=radians

kbdFlags	=  0		;Keyboard scan
kbdSCR		=    3	; 1=scan code ready
kbdKeyPress	=    4	; 1=key has been pressed

doneFlags	=  0		;display "Done"
donePrgm	=    5	; 1=display "Done" after prgm

ioDelFlag	=  0
inDelete	=    0	;1 = DELETE SCREEN 

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
grfRecurM       =    7        ; 1=RECURSION graph

graphFlags	=  3
graphDraw	=    0	; 0=graph is valid, 1=redraw graph

grfDBFlags	=  4
grfDot		=    0	; 0=line, 1=dot
grfSimul	=    1	; 0=sequential, 1=simultaneous
grfGrid 	=    2	; 0=no grid, 1=grid
grfPolar	=    3	; 0=rectangular, 1=polar coordinates
grfNoCoord	=    4	; 0=display coordinates, 1=off
grfNoAxis	=    5	; 0=axis, 1=no axis
grfLabel	=    6	; 0=off, 1=axis label

textFlags	=  5		;Text output flags
textEraseBelow	=    1	; 1=erase line below small char
textScrolled	=    2	; 1=screen scrolled
textInverse	=    3	; 1=display inverse bit-map
textInsMode	=    4	; 0=overstrike, 1=insert mode

ParsFlag2	=  7		;PARSER flags
numOP1		=    0	; 1=RESULT IN OP1, 0=NO RESULT

newDispF        =  8		;Derivative mode flags
preClrForMode   =    0	; 1=HELP BLINK ON MODE SCREEN

apdFlags	=  8		;Automatic power-down
apdAble 	=    2	; 1=APD enabled
apdRunning	=    3	; 1=APD clock running


web_err_mask    =  60h

onFlags 	=  9		;on key flags
onRunning	=  3		; 1 = calculator is running
onInterrupt	=    4	; 1=on key interrupt request

statFlags	=  9		;statistics flags
statsValid	=    6	; 1=stats are valid
statANSDISP	=  7		; 1=display stat results


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
polarMode       =    7
;					;   if Hex and Oct both = 1
;					;   then Bin=0 means >Frac
;					;	 Bin=1 means >DMS
fmtBaseMask     =  00011100B		; mask to base flags
fmtBaseShift    =  2			; offset to base flags
;
;       CHECK IF THESE ARE USED BY NUMFORM,
;
;               =  6
;               =  7

fmtOverride	=  11		;copy of fmtFlags with conversion override

fmtEditFlags	=  12		;numeric editing flags
fmtEdit 	=    0	; 1=format number for editing

curFlags	=  12		;Cursor
curAble 	=    2	; 1=cursor flash is enabled
curOn		=    3	; 1=cursor is showing
curLock 	=    4	; 1=cursor is locked off

appFlags	=  13		;application flags
appWantIntrpt	=    0		; 1=want ON key interrupts
appTextSave	=    1		; 1=save characters in textShadow
appAutoScroll	=    2		; 1=auto-scroll text on last line
appMenus	=    3		; 1=process keys that bring up menus
					; 0=check Lock menu flag
appLockMenus	=    4		; 1=ignore menu keys
					; 0=switch to home screen and bring up menu
appCurGraphic	=    5		; 1=graphic cursor
appCurWord	=    6		; 1=text cursor covers entire word
appExit 	=    7		; 1=application handles [EXIT] key itself

appWantIntrptF	=	1<<appWantIntrpt
appTextSaveF	=	1<<appTextSave
appAutoScrollF	=	1<<appAutoScroll
appMenusF	=	1<<appMenus
appLockMenusF	=	1<<appLockMenus
appCurGraphicF	=	1<<appCurGraphic
appCurWordF	=	1<<appCurWord
appExitF	=	1<<appExit


seqFlags        =  15              ; Sequential Graph flags
webMode         =    0             ; 0 = NORMAL SEQ MODE, 1 = WEB MODE
webVert         =    1             ;
sequv           =    2             ; U vs V
seqvw           =    3             ; V vs W
sequw           =    4             ; U vs W


promptFlags	=  17		;prompt line flags
promptEdit	=    0	; 1=editing in prompt buffer

indicFlags	=  18		;Indicator flags
indicRun	=    0	; 1=run indicator ON
indicInUse	=    1	; indicator save area in use=1, free=0
indicOnly	=    2	; interrupt handler only checks run indicator

shiftFlags	=  18		;[2nd] and [ALPHA] flags
shift2nd	=    3	; 1=[2nd] has been pressed
shiftAlpha	=    4	; 1=[ALPHA] has been pressed
shiftLwrAlph	=    5	; 1=lower case, 0=upper case
shiftALock	=    6	; 1=alpha lock has been pressed
shiftKeepAlph	=    7	; 1=cannot cancel alpha shift


tblFlags        =  19		;table flags.
autoFill        =    4	; 1=prompt, 0=fillAuto
autoCalc        =    5	; 1=prompt, 0=CalcAuto
reTable         =    6	; 0=table is ok, 1=must recompute table.

sGrFlags	=  20
grfSplit        =    0	; 1=Split Graph, 0=Normal
vertSplit       =    1	; 1=Vertical (left-right) Split
grfSChanged     =    2	; 1=Graph just changed Split <-> normal
grfSplitOverride =   3	; 1 = ignore graph split flag if set
write_on_graph  =    4	; 1 = TEXT OR = WRITING TO GRAPH SCREEN
g_style_active  =    5	; 1 = GRAPH STYLES ARE ENABLED, USE THEM
cmp_mod_box     =    6	; 1 = DOING MOD BOX PLOT COMPUTATION
textWrite       =    7	; 1 = Small font writes to buffer
							; 0 = Small font writes to display
;

newIndicFlags   =  21
extraIndic      =    0
saIndic         =    1

newFlags2	    =  22
noRestores	    =  5

smartFlags      =  23
;
;----------------------------------------------------------------------
; Note: Fix these Equates if smartFlags are moved                            
;----------------------------------------------------------------------
;
smarter_mask    =  3
smarter_test    =  1
smartGraph      =  0
smartGraph_inv  =  1                                                 

more_Flags	= 26

No_Del_Stat	= 2
;----------------------------------------------------------------------
;           Available for ASM programming
;----------------------------------------------------------------------

asm_Flag1       =  33          ; ASM CODING
asm_Flag2       =  34          ; ASM CODING
asm_Flag3       =  35          ; ASM CODING

;----------------------------------------------------------------------
;
getSendFlg	=  36
comFailed	=  1	      ; 1 = Get/Send Communication Failed
;
appLwrCaseFlag  =  36
lwrCaseActive   =  3
;
apiFlg3		=  42 
;
apiFlg4		=  43
fullScrnDraw	=    2	; DRAW INTO LAST ROW/COL OF SCREEN

groupFlags	=  38
inGroup		=  1		;1 = IN GROUP CONTEXT


xapFlag0        =  46      ; external app flags
xapFlag1        =  47
xapFlag2        =  48
xapFlag3        =  49

fontFlags	=  50
fracDrawLFont	=    2
fracTallLFont	=    3
customFont	=    7  

plotFlag3	=  60  
bufferOnly	=    0
useFastCirc	=    4

DBKeyFlags      =     61              ;dbus keyboard flags
;Modifier         =     0..3          ;Reserved for LSNibble of Modifiers
repeatMost        =     4             ;set if repeat normal keys
haveDBKey         =     5             ;set if have DBus key
keyDefaultsF      =     6             ;set if don't use system default keys.
HWLinkErrF        =     7             ;have a SE Hardware Link error.

;
varTypeMask	=  1FH             
varGraphRef	=    6

;
;======================================================================
;           Character Font equates  
;======================================================================
;
; Large font equates
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
LboxIcon 	=     00Ah
LcrossIcon 	=     00Bh
LdotIcon 	=     00Ch
LsubT   	=     00Dh		;small capital T for parametric mode.
LcubeR  	=     00Eh		;slightly different 3 for cubed root.
LhexF		=	00Fh
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
Lneg		=	01Ah
Lexponent	=	01Bh
Lstore		=	01Ch
Lten		=	01Dh
LupArrow	=	01Eh
LdownArrow	=	01Fh
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
Lasterisk	=	02Ah
LplusSign	=	02Bh
Lcomma		=	02Ch
Ldash		=	02Dh
Lperiod		=	02Eh
Lslash		=	02Fh
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
Lcolon		=	03Ah
Lsemicolon	=	03Bh
LLT		=	03Ch
LEQ		=	03Dh
LGT		=	03Eh
Lquestion	=	03Fh
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
LcapJ	=	04Ah
LcapK	=	04Bh
LcapL	=	04Ch
LcapM	=	04Dh
LcapN	=	04Eh
LcapO	=	04Fh
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
LcapZ	=	05Ah
Ltheta  =     05Bh
Lbackslash	=	05Ch
LrBrack		=	05Dh
Lcaret		=	05Eh
Lunderscore	=	05Fh
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
Lj	=	06Ah
Lk	=	06Bh
Ll	=	06Ch
Lm	=	06Dh
Ln	=	06Eh
Lo	=	06Fh
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
Lz	=	07Ah
LlBrace	=	07Bh
Lbar	=	07Ch
LrBrace	=	07Dh
Ltilde	=	07Eh
LinvEQ	=	07Fh
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
LcapAAcute	=	08Ah
LcapAGrave	=	08Bh
LcapACaret	=	08Ch
LcapADier	=	08Dh
LaAcute		=	08Eh
LaGrave		=	08Fh
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
LcapIAcute	=	09Ah
LcapIGrave	=	09Bh
LcapICaret	=	09Ch
LcapIDier	=	09Dh
LiAcute		=	09Eh
LiGrave		=	09Fh
LiCaret		=	0A0h
LiDier		=	0A1h
LcapOAcute	=	0A2h
LcapOGrave	=	0A3h
LcapOCaret	=	0A4h
LcapODier	=	0A5h
LoAcute		=	0A6h
LoGrave		=	0A7h
LoCaret		=	0A8h
LoDier		=	0A9h
LcapUAcute	=	0AAh
LcapUGrave	=	0ABh
LcapUCaret	=	0ACh
LcapUDier	=	0ADh
LuAcute		=	0AEh
LuGrave		=	0AFh
LuCaret		=	0B0h
LuDier		=	0B1h
LcapCCed	=	0B2h
LcCed		=	0B3h
LcapNTilde	=	0B4h
LnTilde		=	0B5h
Laccent		=	0B6h
Lgrave		=	0B7h
Ldieresis	=	0B8h
LquesDown	=	0B9h
LexclamDown	=	0BAh
Lalpha		=	0BBh
Lbeta		=	0BCh
Lgamma		=	0BDh
LcapDelta	=	0BEh
Ldelta		=	0BFh
Lepsilon	=	0C0h
LlBrack 	=     0C1h
Llambda		=	0C2h
Lmu		=	0C3h
Lpi		=	0C4h
Lrho		=	0C5h
LcapSigma	=	0C6h
Lsigma		=	0C7h
Ltau		=	0C8h
Lphi		=	0C9h
LcapOmega	=	0CAh
LxMean		=	0CBh
LyMean		=	0CCh
LsupX		=	0CDh
Lellipsis	=	0CEh
Lleft		=	0CFh
Lblock		=	0D0h
Lper		=	0D1h
Lhyphen		=	0D2h
Larea		=	0D3h
Ltemp		=	0D4h
Lcube		=	0D5h
Lenter		=	0D6h
LimagI		=	0D7h
Lphat		=	0D8h
Lchi		=	0D9h
LstatF		=     0DAh
Llne		=	0DBh
LlistL		=	0DCh
LfinanN 	=	0DDh
L2_r_paren 	=	0DEh
LblockArrow     =     0DFh
LcurO   	=     0E0h
LcurO2  	=     0E1h
LcurOcapA       =     0E2h
LcurOa  	=     0E3h
LcurI   	=     0E4h
LcurI2  	=     0E5h
LcurIcapA       =     0E6h
LcurIa  	=     0E7h
LGline          =     0E8h    ; = 0
LGthick         =     0E9h    ; = 1
LGabove         =     0EAh    ; = 2
LGbelow         =     0EBh    ; = 3
LGpath          =     0ECh    ; = 4
LGanimate       =     0EDh    ; = 5
LGdot           =     0EEh    ; = 6
LUpBlk          =     0EFh    ;Up arrow and Block in solver
LDnBlk          =     0F0h    ;Down arrow and Block in solver
LcurFull        =     0F1h    ;note: must be last char (PutMap checks)

;Small font equates
;
SrecurN         =     001h
SrecurU		=	002h
SrecurV		=	003h
SrecurW		=	004h
Sconvert	=	005h
SFourSpaces 	= 	006h
SsqDown		=	007h
Sintegral	=	008h
Scross		=	009h
SboxIcon 	=	00Ah
ScrossIcon 	=	00Bh
SdotIcon 	=    	00Ch
SsubT   	=     00Dh
ScubeR  	=     00Eh
ShexF		=	00Fh
Sroot		=	010h
Sinverse	=	011h
Ssquare		=	012h
Sangle		=	013h
Sdegree		=	014h
Sradian		=	015h
Stranspose	=	016h
SLE		=	017h
SNE		=	018h
SGE		=	019h
Sneg		=	01Ah
Sexponent	=	01Bh
Sstore		=	01Ch
Sten		=	01Dh
SupArrow	=	01Eh
SdownArrow	=	01Fh
Sspace		=	020h
Sexclam		=	021h
Squote		=	022h
Spound		=	023h
Sdollar		=	024h
Spercent	=	025h
Sampersand	=	026h
Sapostrophe	=	027h
SlParen		=	028h
SrParen		=	029h
Sasterisk	=	02Ah
SplusSign	=	02Bh
Scomma		=	02Ch
Sdash		=	02Dh
Speriod		=	02Eh
Sslash		=	02Fh
S0		=	030h
S1		=	031h
S2		=	032h
S3		=	033h
S4		=	034h
S5		=	035h
S6		=	036h
S7		=	037h
S8		=	038h
S9		=	039h
Scolon		=	03Ah
Ssemicolon	=	03Bh
SLT		=	03Ch
SEQ		=	03Dh
SGT		=	03Eh
Squestion	=	03Fh
SatSign		=	040h
ScapA		=	041h
ScapB		=	042h
ScapC		=	043h
ScapD		=	044h
ScapE		=	045h
ScapF		=	046h
ScapG		=	047h
ScapH		=	048h
ScapI		=	049h
ScapJ		=	04Ah
ScapK		=	04Bh
ScapL		=	04Ch
ScapM		=	04Dh
ScapN		=	04Eh
ScapO		=	04Fh
ScapP		=	050h
ScapQ		=	051h
ScapR		=	052h
ScapS		=	053h
ScapT		=	054h
ScapU		=	055h
ScapV		=	056h
ScapW		=	057h
ScapX		=	058h
ScapY		=	059h
ScapZ		=	05Ah
Stheta  	=     05Bh
Sbackslash	=	05Ch
SrBrack		=	05Dh
Scaret		=	05Eh
Sunderscore	=	05Fh
Sbackquote	=	060h
SmallA		=	061h
SmallB		=	062h
SmallC		=	063h
SmallD		=	064h
SmallE		=	065h
SmallF		=	066h
SmallG		=	067h
SmallH		=	068h
SmallI		=	069h
SmallJ		=	06Ah
SmallK		=	06Bh
SmallL		=	06Ch
SmallM		=	06Dh
SmallN		=	06Eh
SmallO		=	06Fh
SmallP		=	070h
SmallQ		=	071h
SmallR		=	072h
SmallS		=	073h
SmallT		=	074h
SmallU		=	075h
SmallV		=	076h
SmallW		=	077h
SmallX		=	078h
SmallY		=	079h
SmallZ		=	07Ah
SlBrace		=	07Bh
Sbar		=	07Ch
SrBrace		=	07Dh
Stilde		=	07Eh
SinvEQ		=	07Fh
Ssub0		=	080h
Ssub1		=	081h
Ssub2		=	082h
Ssub3		=	083h
Ssub4		=	084h
Ssub5		=	085h
Ssub6		=	086h
Ssub7		=	087h
Ssub8		=	088h
Ssub9		=	089h
ScapAAcute	=	08Ah
ScapAGrave	=	08Bh
ScapACaret	=	08Ch
ScapADier	=	08Dh
SaAcute		=	08Eh
SaGrave		=	08Fh
SaCaret		=	090h
SaDier		=	091h
ScapEGrave	=	092h
ScapEAcute	=	093h
ScapECaret	=	094h
ScapEDier	=	095h
SeAcute		=	096h
SeGrave		=	097h
SeCaret		=	098h
SeDier		=	099h
ScapIAcute	=	09Ah
ScapIGrave	=	09Bh
ScapICaret	=	09Ch
ScapIDier	=	09Dh
SiAcute		=	09Eh
SiGrave		=	09Fh
SiCaret		=	0A0h
SiDier		=	0A1h
ScapOAcute	=	0A2h
ScapOGrave	=	0A3h
ScapOCaret	=	0A4h
ScapODier	=	0A5h
SoAcute		=	0A6h
SoGrave		=	0A7h
SoCaret		=	0A8h
SoDier		=	0A9h
ScapUAcute	=	0AAh
ScapUGrave	=	0ABh
ScapUCaret	=	0ACh
ScapUDier	=	0ADh
SuAcute		=	0AEh
SuGrave		=	0AFh
SuCaret		=	0B0h
SuDier		=	0B1h
ScapCCed	=	0B2h
ScCed		=	0B3h
ScapNTilde	=	0B4h
SnTilde		=	0B5h
Saccent		=	0B6h
Sgrave		=	0B7h
Sdieresis	=	0B8h
SquesDown	=	0B9h
SexclamDown	=	0BAh
Salpha		=	0BBh
Sbeta		=	0BCh
Sgamma		=	0BDh
ScapDelta	=	0BEh
Sdelta		=	0BFh
Sepsilon	=	0C0h
SlBrack 	=     0C1h
Slambda		=	0C2h
Smu		=	0C3h
Spi		=	0C4h
Srho		=	0C5h
ScapSigma	=	0C6h
Ssigma		=	0C7h
Stau		=	0C8h
Sphi		=	0C9h
ScapOmega	=	0CAh
SxMean		=	0CBh
SyMean		=	0CCh
SsupX		=	0CDh
Sellipsis	=	0CEh
Sleft		=	0CFh
Sblock		=	0D0h
Sper		=	0D1h
Shyphen		=	0D2h
Sarea		=	0D3h
Stemp		=	0D4h
Scube		=	0D5h
Senter		=	0D6h
SimagI		=	0D7h
Sphat		=	0D8h
Schi		=	0D9h
SstatF  	=     0DAh
Slne		=	0DBh
SlistL		=	0DCh
SfinanN 	=	0DDh
S2_r_paren 	=	0DEh
SnarrowCapE     =     0DFh
SListLock 	=	0E0h
Sscatter1 	=	0E1h                    
Sscatter2 	=	0E2h                    
Sxyline1  	=	0E3h                    
Sxyline2  	=	0E4h
Sboxplot1 	=	0E5h
Sboxplot2 	=	0E6h
Shist1    	=	0E7h
Shist2    	=	0E8h
SmodBox1  	=	0E9h
SmodBox2  	=	0EAh
Snormal1  	=	0EBh
Snormal2  	=	0ECh
;
;======================================================================
;           Keypress Equates
;======================================================================
;           Keyboard key names
;
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
kSendID         =       01Bh
kSendSW         =       01Ch
kYes            =       01Dh
kNoWay          =       01Eh
kvSendType      =       01Fh
kOverWAll       =       020h
;
kNo             =       025h
kKReset         =       026h
kApp            =       027h
;
kDoug           =       028h
kListflag       =       029h
menuStart       =       02Bh
;
kAreYouSure     =       02Bh
kAppsMenu       =       02Ch
kPrgm           =       02Dh
kZoom           =       02Eh
kDraw           =       02Fh
kSPlot          =       030h
kStat           =       031h
kMath           =       032h
kTest           =       033h
kChar           =       034h
kVars           =       035h
kMem            =       036h
kMatrix         =       037h
kDist           =       038h
kAngle          =       039h
kList           =       03Ah
kCalc           =       03Bh
kFin            =       03Ch
;
menuEnd         =       kFin
;
kCatalog        =       03Eh
kInputDone      =       03Fh
kOff            =       kInputDone
;
kQuit           =       040h
appStart        =       kQuit

;
kLinkIO         =       041h
kMatrixEd       =       042h
kStatEd         =       043h
kGraph          =       044h
kMode           =       045h
kPrgmEd         =       046h        ; PROGRAM EDIT
kPrgmCr         =       047h        ; PROGRAM CREATE
kWindow         =       048h        ; RANGE EDITOR
kYequ           =       049h        ; EQUATION EDITOR
kTable          =       04Ah        ; TABLE EDITOR
kTblSet         =       04Bh        ; TABLE SET
kChkRAM         =       04Ch        ; CHECK RAM
kDelMem         =       04Dh        ; DELETE MEM
kResetMem       =       04Eh        ; RESET MEM
kResetDef	=	  04Fh        ; RESET DEFAULT
kPrgmInput      =       050h        ; PROGRAM INPUT
kZFactEd        =       051h        ; ZOOM FACTOR EDITOR
kError          =       052h        ; ERROR
kSolveTVM       =       053h        ; TVM SOLVER
kSolveRoot	=	  054h        ; SOLVE EDITOR
kStatP          =       055h        ; stat plot
kInfStat 	=	  056h	      ; Inferential Statistic
kFormat         =       057h        ; FORMAT
kExtApps        =       058h        ; External Applications.     NEW
kNewApps        =       059h        ; New Apps for Cerberus.
;
append          =       kNewApps
;
echoStart1      =       05Ah
;
kTrace          =       05Ah
kZFit           =       05Bh
kZIn            =       05Ch
kZOut           =       05Dh
kZPrev          =       05Eh
kBox            =       05Fh
kDecml          =       060h
kSetZm          =       061h
kSquar          =       062h
kStd            =       063h
kTrig           =       064h
kUsrZm          =       065h
kZSto           =       066h
kZInt           =       067h
kZStat          =       068h
;
echoStart2      =       069h
;
kSelect         =       069h
kCircl          =       06Ah
kClDrw          =       06Bh
kLine           =       06Ch
kPen            =       06Dh
kPtChg          =       06Eh
kPtOff          =       06Fh
kPtOn           =       070h
kVert           =       071h
kHoriz          =       072h
kText           =       073h
kTanLn          =       074h
;
kEval           =       075h
kInters         =       076h
kDYDX           =       077h
kFnIntg         =       078h
kRootG          =       079h
kDYDT           =       07Ah
kDXDT           =       07Bh
kDRDo           =       07Ch
KGFMin          =       07Dh
KGFMax          =       07Eh
;
;
EchoStart       =       07Fh
;
kListName       =       07Fh
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
kStore          =       08Ah
kComma          =       08Bh
kChs            =       08Ch
kDecPnt         =       08Dh
k0              =       08Eh
k1              =       08Fh
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
kClrTbl         =       0C4h
;
kAns            =       0C5h
kColon          =       0C6h
;
kNDeriv         =       0C7h
kFnInt          =       0C8h
kRoot           =       0C9h
;
kQuest          =       0CAh
kQuote          =       0CBh
kTheta          =       0CCh
kIf             =       0CDh
kThen           =       0CEh
kElse           =       0CFh
kFor            =       0D0h
kWhile          =       0D1h
kRepeat         =       0D2h
kEnd            =       0D3h
kPause          =       0D4h
kLbl            =       0D5h
kGoto           =       0D6h
kISG            =       0D7h
kDSL            =       0D8h
kMenu           =       0D9h
kExec           =       0DAh
kReturn         =       0DBh
kStop           =       0DCh
kInput          =       0DDh
kPrompt         =       0DEh
kDisp           =       0DFh
kDispG          =       0E0h
kDispT          =       0E1h
kOutput         =       0E2h
kGetKey         =       0E3h
kClrHome        =       0E4h
kPrtScr         =       0E5h
kSinH           =       0E6h
kCosH           =       0E7h
kTanH           =       0E8h
kASinH          =       0E9h
kACosH          =       0EAh
kATanH          =       0EBh
kLBrace         =       0ECh
kRBrace         =       0EDh
kI              =       0EEh
kCONSTeA        =       0EFh
kPlot3          =       0F0h
kFMin           =       0F1h
kFMax           =       0F2h
kL1A            =       0F3h
kL2A            =       0F4h
kL3A            =       0F5h
kL4A            =       0F6h
kL5A            =       0F7h
kL6A            =       0F8h
kunA            =       0F9h
kvnA            =       0FAh
kwnA            =       0FBh
;
;======================================================================
;       THIS KEY MEANS THAT IT IS A 2 BYTE KEYCODE
;       THERE ARE 2 OF THESE KEYS; BE CAREFUL WITH USAGE
;======================================================================
;
kExtendEcho2    =       0FCh
;
;======================================================================;
;       THIS KEY MEANS THAT THE KEY PRESS IS ONE THAT ECHOS
;       INTO A BUFFER, AND IT IS A 2 BYTE KEY CODE, GO LOOK AT
;       (EXTECHO) FOR THE KEY VALUE 
;======================================================================
;
kExtendEcho     =       0FEh
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

kLR1            =       kE1BT+20

kXRoot          =       kE1BT+21
kCube           =       kE1BT+22
kCbRt           =       kE1BT+23    ; Cube ROOT
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
kDegr           =       kE1BT+38    ; DEGREES CONV
kAPost          =       kE1BT+39
kToDMS          =       kE1BT+40
kRToPo          =       kE1BT+41    ; R
kRToPr          =       kE1BT+42
kPToRx          =       kE1BT+43
kPToRy          =       kE1BT+44

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
kDim            =       kE1BT+60
kFill           =       kE1BT+61
kIdent          =       kE1BT+62
kRandm          =       kE1BT+63
kAug            =       kE1BT+64

kOneVar         =       kE1BT+65
kTwoVar         =       kE1BT+66
kLR             =       kE1BT+67
kLRExp          =       kE1BT+68
kLRLn           =       kE1BT+69
kLRPwr          =       kE1BT+70
kMedMed         =       kE1BT+71
kQuad           =       kE1BT+72
kClrLst         =       kE1BT+73
kHist           =       kE1BT+74
kxyLine         =       kE1BT+75
kScatter        =       kE1BT+76

kmRad           =       kE1BT+77
kmDeg           =       kE1BT+78
kmNormF         =       kE1BT+79
kmSci           =       kE1BT+80
kmEng           =       kE1BT+81
kmFloat         =       kE1BT+82

kFix            =       kE1BT+83
kSplitOn        =       kE1BT+84
kFullScreen     =       kE1BT+85
kStndrd         =       kE1BT+86
kParam          =       kE1BT+87
kPolar          =       kE1BT+88
kSeqG           =       kE1BT+89
kAFillOn        =       kE1BT+90
kAFillOff       =       kE1BT+91
kACalcOn        =       kE1BT+92
kACalcOff       =       kE1BT+93
kFNOn           =       kE1BT+94
kFNOff          =       kE1BT+95

kPlotsOn        =       kE1BT+96
kPlotsOff       =       kE1BT+97

kPixelChg       =       kE1BT+98

kSendMBL        =       kE1BT+99
kRecvMBL        =       kE1BT+100

kBoxPlot        =       kE1BT+101
kBoxIcon        =       kE1BT+102
kCrossIcon      =       kE1BT+103
kDotIcon        =       kE1BT+104

kE2BT           =       kE1BT+105

kSeqential      =       kE2BT
kSimulG         =       kE2BT+1
kPolarG         =       kE2BT+2
kRectG          =       kE2BT+3
kCoordOn        =       kE2BT+4
kCoordOff       =       kE2BT+5
kDrawLine       =       kE2BT+6
kDrawDot        =       kE2BT+7
kAxisOn         =       kE2BT+8
kAxisOff        =       kE2BT+9
kGridOn         =       kE2BT+10
kGridOff        =       kE2BT+11
kLblOn          =       kE2BT+12
kLblOff         =       kE2BT+13

kL1             =       kE2BT+14
kL2             =       kE2BT+15
kL3             =       kE2BT+16
kL4             =       kE2BT+17
kL5             =       kE2BT+18
kL6             =       kE2BT+19

;
;======================================================================
;           These keys are layed on top of existing keys to
;           enable localization in the inferential stats editor
;======================================================================
;
kinfData        =       kL1
kinfStats       =       kL1+1
kinfYes         =       kL1+2
kinfNo          =       kL1+3
kinfCalc        =       kL1+4
kinfDraw        =       kL1+5
kinfAlt1ne      =       kL1+6
kinfAlt1lt      =       kL1+7
kinfAlt1gt      =       kL1+8
kinfAlt2ne      =       kL1+9
kinfAlt2lt      =       kL1+10
kinfAlt2gt      =       kL1+11
kinfAlt3ne      =       kL1+12
kinfAlt3lt      =       kL1+13
kinfAlt3gt      =       kL1+14
kinfAlt4ne      =       kL1+15
kinfAlt4lt      =       kL1+16
kinfAlt4gt      =       kL1+17
kinfAlt5ne      =       kL1+18
kinfAlt5lt      =       kL1+19
kinfAlt5gt      =       kL1+20
kinfAlt6ne      =       kL1+21
kinfAlt6lt      =       kL1+22
kinfAlt6gt      =       kL1+23
;
;
kMatA           =       kE2BT+20
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
kZu0            =       kE2BT+55
kZv0            =       kE2BT+56
kZnMin          =       kE2BT+57
kZnMax          =       kE2BT+58

kDelLast        =       kE2BT+59
kSinReg         =       kE2BT+60
kConstE         =       kE2BT+61

kPic1           =       kE2BT+62
kPic2           =       kE2BT+63
kPic3           =       kE2BT+64

kDelVar         =       kE2BT+65
kGetCalc        =       kE2BT+66
kRealM          =       kE2BT+67
kPolarM         =       kE2BT+68
kRectM          =       kE2BT+69
kuv             =       kE2BT+70    ; U vs V
kvw             =       kE2BT+71    ; V vs W
kuw             =       kE2BT+72    ; U vs W
kFinPMTend      =       kE2BT+73
kFinPMTbeg      =       kE2BT+74
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
kZTest          =       kE2BT+101
kTTest          =       kE2BT+102
k2SampZTest     =       kE2BT+103
k2SampTTest     =       kE2BT+104
k1PropZTest     =       kE2BT+105
k2PropZTest     =       kE2BT+106
kChiTest        =       kE2BT+107
k2SampFTest     =       kE2BT+108
kZIntVal        =       kE2BT+109
kTIntVal        =       kE2BT+110
k2SampTInt      =       kE2BT+111
k2SampZInt      =       kE2BT+112
k1PropZInt      =       kE2BT+113
k2PropZInt      =       kE2BT+114
kDNormal        =       kE2BT+115
kInvNorm        =       kE2BT+116
kDT             =       kE2BT+117
kChi            =       kE2BT+118
kDF             =       kE2BT+119
kBinPDF         =       kE2BT+120
kBinCDF         =       kE2BT+121
kPoiPDF         =       kE2BT+122
kPoiCDF         =       kE2BT+123
kun             =       kE2BT+124
kvn             =       kE2BT+125
kwn             =       kE2BT+126
kRecn           =       kE2BT+127
kPlotStart      =       kE2BT+128
kZPlotStart     =       kE2BT+129   ; recursion n
kXFact          =       kE2BT+130   ; PlotStart
kYFact          =       kE2BT+131   ; ZPlotStart
kANOVA          =       kE2BT+132   ; XFact
kMaxY           =       kE2BT+133   ; YFact
kWebOn          =       kE2BT+134   ; MinY
kWebOff         =       kE2BT+135   ; MaxY
kTblInput       =       kE2BT+136   ; WEB ON
kGeoPDF         =       kE2BT+137   ; WEB OFF
kGeoCDF         =       kE2BT+138   ; WEB OFF
kShadeNorm      =       kE2BT+139
kShadeT         =       kE2BT+140
kShadeChi       =       kE2BT+141
kShadeF         =       kE2BT+142
kPlotStep       =       kE2BT+143
kZPlotStep      =       kE2BT+144
kLinRegtTest    =       kE2BT+145
KMGT		=	  kE2BT+146   ; VERT SPLIT
kSelectA        =       kE2BT+147
kZFitA          =       kE2BT+148
kE2BT_End       =       kZFitA
;
;
;======================================================================
;           More 2 Byte Keys 
;======================================================================

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
kConj		=       kE2BT2+47
kReal		=       kE2BT2+48
kFAngle		=       kE2BT2+49
kLCM		=       kE2BT2+50
kGCD		=       kE2BT2+51
kRandInt	=       kE2BT2+52
kRandNorm	=       kE2BT2+53
kToPolar	=       kE2BT2+54
kToRect		=       kE2BT2+55
kYMean		=       kE2BT2+56
kStdX		=       kE2BT2+57
kStdX1		=       kE2BT2+58
kw0		=       kE2BT2+59
kMatF		=       kE2BT2+60
kMatG		=       kE2BT2+61
kMatRH		=       kE2BT2+62
kMatI		=       kE2BT2+63
kMatJ		=       kE2BT2+64
kYMean1		=       kE2BT2+65
kStdY		=       kE2BT2+66
kStdY1		=       kE2BT2+67
kMatToLst	=       kE2BT2+68
kLstToMat	=       kE2BT2+69
kCumSum		=       kE2BT2+70
kDeltaLst	=       kE2BT2+71
kStdDev		=       kE2BT2+72
kVariance	=       kE2BT2+73
kLength		=       kE2BT2+74
kEquToStrng	=       kE2BT2+75
kStrngToEqu	=       kE2BT2+76
kExpr		=       kE2BT2+77
kSubStrng	=       kE2BT2+78
kInStrng	=       kE2BT2+79
kStr1		=       kE2BT2+80
kStr2		=       kE2BT2+81
kStr3		=       kE2BT2+82
kStr4           =       kE2BT2+83
kStr5           =       kE2BT2+84
kStr6           =       kE2BT2+85
kStr7           =       kE2BT2+86
kStr8           =       kE2BT2+87
kStr9           =       kE2BT2+88
kStr0           =       kE2BT2+89
kFinN           =       kE2BT2+90
kFinI           =       kE2BT2+91
kFinPV          =       kE2BT2+92
kFinPMT         =       kE2BT2+93
kFinFV          =       kE2BT2+94
kFinPY          =       kE2BT2+95
kFinCY          =       kE2BT2+96
kFinFPMT        =       kE2BT2+97
kFinFI          =       kE2BT2+98
kFinFPV         =       kE2BT2+99
kFinFN          =       kE2BT2+100
kFinFFV         =       kE2BT2+101
kFinNPV         =       kE2BT2+102
kFinIRR         =       kE2BT2+103
kFinBAL         =       kE2BT2+104
kFinPRN         =       kE2BT2+105
kFinINT         =       kE2BT2+106
kSumX           =       kE2BT2+107
kSumX2          =       kE2BT2+108
kFinToNom       =       kE2BT2+109
kFinToEff       =       kE2BT2+110
kFinDBD         =       kE2BT2+111
kStatVP         =       kE2BT2+112
kStatZ          =       kE2BT2+113
kStatT          =       kE2BT2+114
kStatChi        =       kE2BT2+115
kStatF          =       kE2BT2+116
kStatDF         =       kE2BT2+117
kStatPhat       =       kE2BT2+118
kStatPhat1      =       kE2BT2+119
kStatPhat2      =       kE2BT2+120
kStatMeanX1     =       kE2BT2+121
kStatMeanX2     =       kE2BT2+122
kStatStdX1      =       kE2BT2+123
kStatStdX2      =       kE2BT2+124
kStatStdXP      =       kE2BT2+125
kStatN1         =       kE2BT2+126
kStatN2         =       kE2BT2+127
kStatLower      =       kE2BT2+128
kStatUpper      =       kE2BT2+129
kuw0            =       kE2BT2+130
kImag           =       kE2BT2+131
kSumY           =       kE2BT2+132
kXres           =       kE2BT2+133
kStat_s         =       kE2BT2+134
kSumY2          =       kE2BT2+135
kSumXY          =       kE2BT2+136
kuXres          =       kE2BT2+137
kModBox		=       kE2BT2+138
kNormProb	=       kE2BT2+139
kNormalPDF      =       kE2BT2+140
kTPDF           =       kE2BT2+141
kChiPDF         =       kE2BT2+142
kFPDF           =       kE2BT2+143
kMinY           =       kE2BT2+144   ; MinY
kRandBin        =       kE2BT2+145
kRef            =       kE2BT2+146
kRRef           =       kE2BT2+147
kLRSqr          =       kE2BT2+148
kBRSqr          =       kE2BT2+149
kDiagOn         =       kE2BT2+150
kDiagOff        =       kE2BT2+151
kun1            =       kE2BT2+152   ; FOR RCL USE WHEN GOTTEN FROM 82
kvn1            =       kE2BT2+153   ; FOR RCL USE WHEN GOTTEN FROM 82
;
k83_00End       =	kvn1            ;end of original keys...
kArchive        =     k83_00End + 1
kUnarchive      =     k83_00End + 2
kAsm            =     k83_00End + 3   ; Asm(
kAsmPrgm        =     k83_00End + 4   ; AsmPrgm
kAsmComp        =     k83_00End + 5   ; AsmComp(
;
kcapAAcute	=       k83_00End + 6
kcapAGrave	=       k83_00End + 7
kcapACaret	=       k83_00End + 8
kcapADier	=       k83_00End + 9
kaAcute		=       k83_00End + 10
kaGrave		=       k83_00End + 11
kaCaret		=       k83_00End + 12
kaDier		=       k83_00End + 13
kcapEAcute	=       k83_00End + 14
kcapEGrave	=       k83_00End + 15
kcapECaret	=       k83_00End + 16
kcapEDier	=       k83_00End + 17
keAcute		=       k83_00End + 18
keGrave		=       k83_00End + 19
keCaret		=       k83_00End + 20
keDier		=       k83_00End + 21
kcapIAcute	=       k83_00End + 22
kcapIGrave	=       k83_00End + 23
kcapICaret	=       k83_00End + 24
kcapIDier	=       k83_00End + 25
kiAcute		=       k83_00End + 26
kiGrave		=       k83_00End + 27
kiCaret		=       k83_00End + 28
kiDier		=       k83_00End + 29
kcapOAcute	=       k83_00End + 30
kcapOGrave	=       k83_00End + 31
kcapOCaret	=       k83_00End + 32
kcapODier	=       k83_00End + 33
koAcute		=       k83_00End + 34
koGrave		=       k83_00End + 35
koCaret		=       k83_00End + 36
koDier		=       k83_00End + 37
kcapUAcute	=       k83_00End + 38
kcapUGrave	=       k83_00End + 39
kcapUCaret	=       k83_00End + 40
kcapUDier	=       k83_00End + 41
kuAcute		=       k83_00End + 42
kuGrave		=       k83_00End + 43
kuCaret		=       k83_00End + 44
kuDier		=       k83_00End + 45
kcapCCed	=       k83_00End + 46
kcCed		=       k83_00End + 47
kcapNTilde	=       k83_00End + 48
knTilde		=       k83_00End + 49
kaccent		=       k83_00End + 50
kgrave		=       k83_00End + 51
kdieresis	=       k83_00End + 52
kquesDown	=       k83_00End + 53
kexclamDown     =       k83_00End + 54
kalpha          =       k83_00End + 55
kbeta           =      k83_00End +  56
kgamma          =      k83_00End +  57
kcapDelta       =      k83_00End +  58
kdelta          =      k83_00End +  59
kepsilon        =      k83_00End +  60
klambda         =      k83_00End +  61
kmu             =      k83_00End +  62
kpi2            =      k83_00End +  63
krho            =      k83_00End +  64
kcapSigma       =      k83_00End +  65
ksigma          =      k83_00End +  66
ktau            =      k83_00End +  67
kphi            =      k83_00End +  68
kcapOmega       =      k83_00End +  69
kphat           =      k83_00End +  70
kchi2           =      k83_00End +  71
kstatF2         =      k83_00End +  72
kLa		=     k83_00End + 73
kLb		=     k83_00End + 74
kLc		=     k83_00End + 75
kLd		=     k83_00End + 76
kLe		=     k83_00End + 77
kLf		=     k83_00End + 78
kLg		=     k83_00End + 79
kLh		=     k83_00End + 80
kLi		=     k83_00End + 81
kLj		=     k83_00End + 82
kLk		=     k83_00End + 83
kLl		=     k83_00End + 84
kLm		=     k83_00End + 85
kLsmalln	=    k83_00End +  86
kLo		=    k83_00End +  87
kLp		=    k83_00End +  88
kLq		=    k83_00End +  89
kLsmallr	=    k83_00End +  90
kLs		=    k83_00End +  91
kLt		=    k83_00End +  92
kLu		=    k83_00End +  93
kLv		=    k83_00End +  94
kLw		=    k83_00End +  95
kLx		=    k83_00End +  96
kLy		=    k83_00End +  97
kLz		=    k83_00End +  98
kGarbageC	=  k83_00End +  99 ; GarbageCollect
;
kE2BT2_End	=     kGarbageC

; the following keys were added in OS version 1.15
KE2BT3          =       0
;
kReserved         =     KE2BT3+01       ;01 - 001d
kAtSign         =     KE2BT3+02       ;02 - 002d
kPound          =     KE2BT3+03       ;03 - 003d
kDollar         =     KE2BT3+04       ;04 - 004d
kAmpersand      =     KE2BT3+05       ;05 - 005d
kBackQuote      =     KE2BT3+06       ;06 - 006d
kSemicolon      =     KE2BT3+07       ;07 - 007d
kBackSlash      =     KE2BT3+08       ;08 - 008d
kVertSlash      =     KE2BT3+09       ;09 - 009d
kUnderscore     =     KE2BT3+10       ;0A - 010d
kTilde          =     KE2BT3+11       ;0B - 011d
kPercent        =     KE2BT3+12       ;0C - 012d
kLastUsedK3     =     kPercent

kTab            =     KE2BT3+13       ;0D - 013d
kShftTaB        =     KE2BT3+14       ;0E - 014d
kShftDel        =     KE2BT3+15       ;0F - 015d
kShftBack       =     KE2BT3+16       ;10 - 016d
kShftPgUp       =     KE2BT3+17       ;11 - 017d
kShftPgDn       =     KE2BT3+18       ;12 - 018d
kShftLeft       =     KE2BT3+19       ;13 - 019d
kShftRight      =     KE2BT3+20       ;14 - 020d
kShftUp         =     KE2BT3+21       ;15 - 021d
kShftDn         =     KE2BT3+22       ;16 - 022d
;
kDiamond        =     KE2BT3+23
;
kDiaAdd         =     kDiamond+00     ;17 - 023d
kDiaSub         =     kDiamond+01     ;18 - 024d
kDiaTilde       =     kDiamond+02     ;19 - 025d
kDiaDiv         =     kDiamond+03     ;1A - 026d
kDiaBkSlash     =     kDiamond+04     ;1B - 027d
kDiaColon       =     kDiamond+05     ;1C - 028d
kDiaQuote       =     kDiamond+06     ;1D - 029d
kDiaLBrack      =     kDiamond+07     ;1E - 030d
kDiaRBrack      =     kDiamond+08     ;1F - 031d
kDiaBkSpace     =     kDiamond+09     ;20 - 032d
kDiaEnter       =     kDiamond+10     ;21 - 033d
kDiaComma       =     kDiamond+11     ;22 - 034d
kDiaDel         =     kDiamond+12     ;23 - 035d
kDiaDecPnt      =     kDiamond+13     ;24 - 036d
kDia0           =     kDiamond+14     ;25 - 037d
kDia1           =     kDiamond+15     ;26 - 038d
kDia2           =     kDiamond+16     ;27 - 039d
kDia3           =     kDiamond+17     ;28 - 040d
kDia4           =     kDiamond+18     ;29 - 041d
kDia5           =     kDiamond+19     ;2A - 042d
kDia6           =     kDiamond+20     ;2B - 043d
kDia7           =     kDiamond+21     ;2C - 044d
kDia8           =     kDiamond+22     ;2D - 045d
kDia9           =     kDiamond+23     ;2E - 046d
kDiaTab         =     kDiamond+24     ;2F - 047d
kDiaSpace       =     kDiamond+25     ;30 - 048d
kDiaA           =     kDiamond+26     ;31 - 049d
kDiaB           =     kDiamond+27     ;32 - 050d
kDiaC           =     kDiamond+28     ;33 - 051d
kDiaD           =     kDiamond+29     ;34 - 052d
kDiaE           =     kDiamond+30     ;35 - 053d
kDiaF           =     kDiamond+31     ;36 - 054d
kDiaG           =     kDiamond+32     ;37 - 055d
kDiaH           =     kDiamond+33     ;38 - 056d
kDiaI           =     kDiamond+34     ;39 - 057d
kDiaJ           =     kDiamond+35     ;3A - 058d
kDiaK           =     kDiamond+36     ;3B - 059d
kDiaL           =     kDiamond+37     ;3C - 060d
kDiaM           =     kDiamond+38     ;3D - 061d
kDiaN           =     kDiamond+39     ;3E - 062d
kDiaO           =     kDiamond+40     ;3F - 063d
kDiaP           =     kDiamond+41     ;40 - 064d
kDiaQ           =     kDiamond+42     ;41 - 065d
kDiaR           =     kDiamond+43     ;42 - 066d
kDiaS           =     kDiamond+44     ;43 - 067d
kDiaT           =     kDiamond+45     ;44 - 068d
kDiaU           =     kDiamond+46     ;45 - 069d
kDiaV           =     kDiamond+47     ;46 - 070d
kDiaW           =     kDiamond+48     ;47 - 071d
kDiaX           =     kDiamond+49     ;48 - 072d
kDiaY           =     kDiamond+50     ;49 - 073d
kDiaZ           =     kDiamond+51     ;4A - 074d
kDiaPgUp        =     kDiamond+52     ;4B - 075d
kDiaPgDn        =     kDiamond+53     ;4C - 076d
kDiaLeft        =     kDiamond+54     ;4D - 077d
kDiaRight       =     kDiamond+55     ;4E - 078d
kDiaUp          =     kDiamond+56     ;4F - 079d
kDiaDn          =     kDiamond+57     ;50 - 080d
;
kdbSquare       =     kDiamond+58
;
kSqrAdd         =     kdbSquare+00    ;51 - 081d
kSqrSub         =     kdbSquare+01    ;52 - 082d
kSqrTilde       =     kdbSquare+02    ;53 - 083d
kSqrDiv         =     kdbSquare+03    ;54 - 084d
kSqrBkSlash     =     kdbSquare+04    ;55 - 085d
kSqrColon       =     kdbSquare+05    ;56 - 086d
kSqrQuote       =     kdbSquare+06    ;57 - 087d
kSqrLBrack      =     kdbSquare+07    ;58 - 088d
kSqrRBrack      =     kdbSquare+08    ;59 - 089d
kSqrBkSpace     =     kdbSquare+09    ;5A - 090d
kSqrEnter       =     kdbSquare+10    ;5B - 091d
kSqrComma       =     kdbSquare+11    ;5C - 092d
kSqrDel         =     kdbSquare+12    ;5D - 093d
kSqrDecPnt      =     kdbSquare+13    ;5E - 094d
kSqr0           =     kdbSquare+14    ;5F - 095d
kSqr1           =     kdbSquare+15    ;60 - 096d
kSqr2           =     kdbSquare+16    ;61 - 097d
kSqr3           =     kdbSquare+17    ;62 - 098d
kSqr4           =     kdbSquare+18    ;63 - 099d
kSqr5           =     kdbSquare+19    ;64 - 100d
kSqr6           =     kdbSquare+20    ;65 - 101d
kSqr7           =     kdbSquare+21    ;66 - 102d
kSqr8           =     kdbSquare+22    ;67 - 103d
kSqr9           =     kdbSquare+23    ;68 - 104d
kSqrTab         =     kdbSquare+24    ;69 - 105d
kSqrSpace       =     kdbSquare+25    ;6A - 106d
kSqrA           =     kdbSquare+26    ;6B - 107d
kSqrB           =     kdbSquare+27    ;6C - 108d
kSqrC           =     kdbSquare+28    ;6D - 109d
kSqrD           =     kdbSquare+29    ;6E - 110d
kSqrE           =     kdbSquare+30    ;6F - 111d
kSqrF           =     kdbSquare+31    ;70 - 112d
kSqrG           =     kdbSquare+32    ;71 - 113d
kSqrH           =     kdbSquare+33    ;72 - 114d
kSqrI           =     kdbSquare+34    ;73 - 115d
kSqrJ           =     kdbSquare+35    ;74 - 116d
kSqrK           =     kdbSquare+36    ;75 - 117d
kSqrL           =     kdbSquare+37    ;76 - 118d
kSqrM           =     kdbSquare+38    ;77 - 119d
kSqrN           =     kdbSquare+39    ;78 - 120d
kSqrO           =     kdbSquare+40    ;79 - 121d
kSqrP           =     kdbSquare+41    ;7A - 122d
kSqrQ           =     kdbSquare+42    ;7B - 123d
kSqrR           =     kdbSquare+43    ;7C - 124d
kSqrS           =     kdbSquare+44    ;7D - 125d
kSquareT        =     kdbSquare+45    ;7E - 126d
kSqrU           =     kdbSquare+46    ;7F - 127d
kSqrV           =     kdbSquare+47    ;80 - 128d
kSqrW           =     kdbSquare+48    ;81 - 129d
kSqrX           =     kdbSquare+49    ;82 - 130d
kSqrY           =     kdbSquare+50    ;83 - 131d
kSqrZ           =     kdbSquare+51    ;84 - 132d
kSqrPgUp        =     kdbSquare+52    ;85 - 133d
kSqrPgDn        =     kdbSquare+53    ;86 - 134d
kSqrLeft        =     kdbSquare+54    ;87 - 135d
kSqrRight       =     kdbSquare+55    ;88 - 136d
kSqrUp          =     kdbSquare+56    ;89 - 137d
kSqrDn          =     kdbSquare+57    ;8A - 138d
;
kUnDef          =     kdbSquare+58    ;8A - 139d
kE2BT3_End      =     kUnDef
;

;======================================================================
;           TI-83 Plus Context Equates
;======================================================================
;
cxCmd           =     kQuit                   ;home screen
cxMatEdit       =     kMatrixEd               ;matrix editor
cxPrgmEdit      =     kPrgmEd                 ;program editor
cxEquEdit       =     kYequ                   ;equation editor
cxGrRange       =     kWindow                 ;graph range editor
cxGrZfact       =     kZFactEd                ;graph zoom factors editor
cxGraph         =     kGraph                  ;graph mode
cxStatEdit      =     kStatEd                 ;statistics list editor
cxPrgmInput     =     kPrgmInput              ;programmed input
cxError         =     kError                  ;error handler
cxLinkIO        =     kLinkIO                 ;LINK I/O interface
cxMem           =     kResetMem               ;reset memory
cxDefMem        =     kResetDef               ;reset default
cxRAMApp        =     kChkRAM                 ;RAM usage screen
cxMode          =     kMode                   ;mode settings screen
cxErase         =     kDelMem                 ;memory erase
cxPrgmCreate    =     kPrgmCr                 ;PROGRAM CREATE
cxTableEditor   =     kTable                  ;TABLE EDITOR
cxTableSet      =     kTblSet                 ;TABLE SET UP
cxStatPlot      =     kStatP                  ;STAT PLOTS
cxInfStat	=	kInfStat		;Inferential Statistic
cxFormat        =     kFormat                 ;FORMAT CONTEXT
cxSolveTVM      =     kSolveTVM               ;Solve TVM
cxSolveRoot     =     kSolveRoot              ;Solve Root
lastOldApp      =     kExtApps                ;external applications
cxextapps       =     kExtApps
cxNewApps       =     kNewApps                ;new cerberus applications
cxGroup         =     cxNewApps+0             ;1st new app.
cxUnGroup       =     cxNewApps+1             ;2nd new app.
lastNewApp      =     cxUnGroup               ;last new app for this ver

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
;
;======================================================================
;           Tokens
;======================================================================
;
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
;
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
;
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
;
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
tRoot           =       IMUN+16     ; 22h 'ROOT'
tSeries         =       IMUN+17     ; 23h 'seq'
tFnInt          =       IMUN+18     ; 24h 'fnInt'
tNDeriv         =       IMUN+19     ; 25h 'fnIr'
tEvalF          =       IMUN+20     ; 26h
tFmin           =       IMUN+21     ; 27h
tFmax           =       IMUN+22     ; 28h
;
tEOSEL          =       IMUN+23
;
tSpace          =       tEOSEL       ; 29h   ' '
tString         =       tEOSEL+1     ; 2Ah  '"'
tComma          =       tEOSEL+2     ; 2Bh  ','
;
;
tii             =       2Ch         ; i
;
;======================================================================
;           Postfix Functions
;======================================================================
;
tPost           =       2Dh
;
tFact           =       tPost       ; 2Dh '!'
;
tCubicR         =       2Eh
tQuartR         =       2Fh

;======================================================================
;           Number Tokens 
;======================================================================
;
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
;
;======================================================================
;            BINARY OP
;======================================================================
;
tOr             =       3Ch         ; 3Ch  '_or_'
tXor            =       3Dh         ; 3Dh
;
tColon          =       3Eh         ; 3Eh ':'
tEnter          =       3Fh         ; 3Fh  Lenter
;
tAnd            =       40h         ; 40h  '_and_'       
;
;======================================================================
;            LETTER TOKENS   
;======================================================================

;
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
tTheta          =       LET+26      ; 5Bh
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
;
;
;======================================================================
;            Mode Setting Commands
;======================================================================

;
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
;
;======================================================================
;            BINARY OP
;======================================================================
;
tAdd            =       70h         ; 70h '+'
tSub            =       71h         ; 71h '-'
tAns            =       72h         ; 72h
;
;======================================================================
;            Mode Setting Commands
;======================================================================
;
MODES           =       73h
;
tFix            =       MODES       ; 73h 'Fix_'
tSplitOn        =       MODES+1     ; 74h
tFullScreen     =       MODES+2     ; 75h
tStndrd         =       MODES+3     ; 76h 'Func'
tParam          =       MODES+4     ; 77h 'Param'
tPolar          =       MODES+5     ; 78h 'Pol'
tSeqG           =       MODES+6     ; 79h
tAFillOn        =       MODES+7     ; 7Ah 'AUTO FILL ON
tAFillOff       =       MODES+8     ; 7Bh
tACalcOn        =       MODES+9     ; 7Ch
tACalcOff       =       MODES+10    ; 7Dh 'AutoFill OFF
;
;       GRAPH FORMAT TOKENS ARE 2 BYTE TOKENS
;
tGFormat        =       MODES+11    ; 7Eh
;
tBoxIcon        =       7Fh
tCrossIcon      =       80h
tDotIcon        =       81h
;
;======================================================================
;            (More) BINARY OP
;======================================================================
;
tMul            =       82h         ; 82h  '*'
tDiv            =       83h         ; 83h  '/'
;
;======================================================================
;            SOME GRAPH COMMANDS
;======================================================================
;
GRCMDS          =       84h
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
tUsrZm          =       GRCMDS+12   ; 90h 'ZRcl'
tPrtScrn        =       GRCMDS+13   ; 91h 'PrtScrn'
tZoomSto        =       GRCMDS+14   ; 92h  ZOOM STORE
tText           =       GRCMDS+15   ; 93h
;
;======================================================================
;           BINARY OP  (Combination & Permutation)
;======================================================================
;
tnPr            =       GRCMDS+16   ; 94h '_nPr_'
tnCr            =       GRCMDS+17   ; 95h '_nCr_'
;
;======================================================================
;           MORE GRAPH COMMANDS
;======================================================================
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
;======================================================================
;           TOKEN A0 CANNOT BE AN EOS FUNCTION
;           SINCE LOW MULT=A0 ALREADY
;======================================================================
;
tPtChg          =       GRCMDS+28   ; A0h 'PtChg'
tPXOn           =       GRCMDS+29   ; A1h
tPXOff          =       GRCMDS+30   ; A2h
tPXChg          =       GRCMDS+31   ; A3h
tShade          =       GRCMDS+32   ; A4h 'Shade'
tCircl          =       GRCMDS+33   ; A5h 'Circl'
tHorz           =       GRCMDS+34   ; A6h 'HORIZONTAL'
tTanLn          =       GRCMDS+35   ; A7h 'TanLn'
tDrInv          =       GRCMDS+36   ; A8h 'DrInv_'
tDrawF          =       GRCMDS+37   ; A9h 'DrawF_'
;
tVarStrng       =       0AAh
;
;======================================================================
;           Functions with No Arguments                                    
;======================================================================
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
tNot            =       UNARY+8     ; B8h
tIPart          =       UNARY+9     ; B9h
tFPart          =       UNARY+10    ; BAh
;
;
;======================================================================
;           NEW 2 BYTE TOKENS
;======================================================================
;
t2ByteTok       =       0BBh         ;
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
tSinH           =       UNARYLR+12  ; C8h
tASinH          =       UNARYLR+13  ; C9h
tCosH           =       UNARYLR+14  ; CAh
tACosH          =       UNARYLR+15  ; CBh
tTanH           =       UNARYLR+16  ; CCh
tATanH          =       UNARYLR+17  ; CDh
; 
;======================================================================
;           SOME PROGRAMMING COMMANDS                                      
;======================================================================
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
tOutput         =       PROGTOK+18  ; E0h    'Outpt'
tClLCD          =       PROGTOK+19  ; E1h    'ClLCD'
tConst          =       PROGTOK+20  ; E2h    'Fill'
tSortA          =       PROGTOK+21  ; E3h    'sortA_'
tSortD          =       PROGTOK+22  ; E4h    'sortD_'
tDispTab        =       PROGTOK+23  ; E5h    'Disp Table
tMenu           =       PROGTOK+24  ; E6h    'Menu'
tSendMBL        =       PROGTOK+25  ; E7h    'SEND'
tGetMBL         =       PROGTOK+26  ; E8h    'GET'
;
;======================================================================
;           STAT PLOT COMMANDS                                             
;======================================================================
;
statPCmd        =       PROGTOK+27
;
tPlotOn         =       statPCmd    ; E9h     ' PLOTSON'
tPlotOff        =       statPCmd+1   ; EAh     ' PLOTSOFF
;
tListName       =       0EBh        ; LIST DESIGNATOR
;
tPlot1          =       0ECh
tPlot2          =       0EDh
tPlot3          =       0EEh
;
tUnused01       =       0EFh        ;        available?
;
tPower          =       0F0h        ;        '^'
tXRoot          =       0F1h        ;        LsupX,Lroot

STATCMD         =       0F2h
;
tOneVar         =       STATCMD     ; F2h    'OneVar_'
tTwoVar         =       STATCMD+1   ; F3h
tLR             =       STATCMD+2   ; F4h    'LinR(A+BX
tLRExp          =       STATCMD+3   ; F5h    'ExpR_'
tLRLn           =       STATCMD+4   ; F6h    'LnR_'
tLRPwr          =       STATCMD+5   ; F7h    'PwrR_'
tMedMed         =       STATCMD+6   ; F8h
tQuad           =       STATCMD+7   ; F9h
tClrLst         =       STATCMD+8   ; FAh    'CLEAR LIST
tClrTbl         =       STATCMD+9   ; FBh    CLEAR TABLE
tHist           =       STATCMD+10  ; FCh    'Hist_'
txyLine         =       STATCMD+11  ; FDh    'xyline_'
tScatter        =       STATCMD+12  ; FEh    'Scatter_'
tLR1            =       STATCMD+13  ; FFh    'LINR(AX+B

;       
;======================================================================
;           2ND HALF OF GRAPH FORMAT TOKENS
;======================================================================
;           Format settings commands
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
tuv             =       GFMT+16     ; U vs V
tvw             =       GFMT+17     ; V vs W
tuw             =       GFMT+18     ; U vs W
;
;======================================================================
;           2ND HALF OF USER MATRIX TOKENS                                 
;======================================================================
;
tMatA           =       00h         ; MAT A
tMatB           =       01h         ; MAT B
tMatC           =       02h         ; MAT C
tMatD           =       03h         ; MAT D
tMatE           =       04h         ; MAT E
tMatF           =       05h         ; MAT F
tMatG           =       06h         ; MAT G
tMatH           =       07h         ; MAT H
tMatI           =       08h         ; MAT I
tMatJ           =       09h         ; MAT J
;      
;======================================================================
;           2ND HALF OF USER LIST TOKENS
;======================================================================
;
tL1             =       00h         ; LIST 1
tL2             =       01h         ; LIST 2
tL3             =       02h         ; LIST 3
tL4             =       03h         ; LIST 4
tL5             =       04h         ; LIST 5
tL6             =       05h         ; LIST 6

;
;======================================================================
;           2ND HALF OF USER EQUATION TOKENS
;======================================================================
;
;       "Y" EQUATIONS HAVE BIT 4 SET
;
tY1             =       10h         ; Y1
tY2             =       11h         ; Y2
tY3             =       12h         ; Y3
tY4             =       13h         ; Y4
tY5             =       14h         ; Y5
tY6             =       15h         ; Y6
tY7             =       16h         ; Y7
tY8             =       17h         ; Y8
tY9             =       18h         ; Y9
tY0             =       19h         ; Y0
;
;       PARAM EQUATIONS HAVE BIT 5 SET
;
tX1T            =       20h         ; X1t
tY1T            =       21h         ; Y1t
tX2T            =       22h         ; X2t
tY2T            =       23h         ; Y2t
tX3T            =       24h         ; X3t
tY3T            =       25h         ; Y3t
tX4T            =       26h         ; X4t
tY4T            =       27h         ; Y4t
tX5T            =       28h         ; X5t
tY5T            =       29h         ; Y5t
tX6T            =       2Ah         ; X6t
tY6T            =       2Bh         ; Y6t
;
;       POLAR EQUATIONS HAVE BIT 6 SET
;
tR1             =       40h         ; R1
tR2             =       41h         ; R2
tR3             =       42h         ; R3
tR4             =       43h         ; R4
tR5             =       44h         ; R5
tR6             =       45h         ; R6
;
;       RECURSION EQUATIONS HAVE BIT 7 SET
;
tun             =       80h         ; Un
tvn             =       81h         ; Vn
twn             =       82h         ; Wn
;
;======================================================================
;           2ND HALF OF USER PICTURE TOKENS                                
;======================================================================
;
tPic1           =       00h         ; PIC1
tPic2           =       01h         ; PIC2
tPic3           =       02h         ; PIC3
tPic4           =       03h         ; PIC4
tPic5           =       04h         ; PIC5
tPic6           =       05h         ; PIC6
tPic7           =       06h         ; PIC7
tPic8           =       07h         ; PIC8
tPic9           =       08h         ; PIC9
tPic0           =       09h         ; PIC0
; 
;======================================================================
;           2ND HALF OF USER GRAPH DATABASE TOKENS                         
;======================================================================
;
tGDB1           =       00h         ; GDB1
tGDB2           =       01h         ; GDB2
tGDB3           =       02h         ; GDB3
tGDB4           =       03h         ; GDB4
tGDB5           =       04h         ; GDB5
tGDB6           =       05h         ; GDB6
tGDB7           =       06h         ; GDB7
tGDB8           =       07h         ; GDB8
tGDB9           =       08h         ; GDB9
tGDB0           =       09h         ; GDB0
;  
;======================================================================
;           2ND HALF OF STRING VARS                                        
;======================================================================
;
tStr1           =       00h
tStr2           =       01h
tStr3           =       02h
tStr4           =       03h
tStr5           =       04h
tStr6           =       05h
tStr7           =       06h
tStr8           =       07h
tStr9           =       08h
tStr0           =       09h

; 
;======================================================================
;           2ND HALF OF SYSTEM OUTPUT ONLY VARIABLES                       
;======================================================================
;
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
tRecurn		=       21h		; RECURSION N
tStatP          =       22h
tStatZ          =       23h
tStatT          =       24h
tStatChi        =       25h
tStatF          =       26h
tStatDF         =       27h
tStatPhat       =       28h
tStatPhat1      =       29h
tStatPhat2      =       2Ah
tStatMeanX1     =       2Bh
tStatStdX1      =       2Ch
tStatN1         =       2Dh
tStatMeanX2     =       2Eh
tStatStdX2      =       2Fh
tStatN2         =       30h
tStatStdXP      =       31h
tStatLower      =       32h
tStatUpper      =       33h
tStat_s         =       34h
tLRSqr          =       35h         ; r^2
tBRSqr          =       36h         ; R^2
;
;======================================================================
;       These next tokens are only used to access the data
;       they are display only and the user cannot access them at all
;======================================================================
;
tF_DF    =     37h       ; ANOFAV FACTOR DF
tF_SS    =     38h       ; ANOFAV FACTOR SS
tF_MS    =     39h       ; ANOFAV FACTOR MS
tE_DF    =     3Ah       ; ANOFAV ERROR DF
tE_SS    =     3Bh       ; ANOFAV ERROR SS
tE_MS    =     3Ch       ; ANOFAV ERROR MS
;
;
;======================================================================
;           2ND HALF OF SYSTEM INPUT/OUTPUT VARIABLES
;======================================================================
;      SYSTEM VARIABLE EQUATES
;
tuXscl       =  0
tuYscl       =  1
tXscl        =  2
tYscl        =  3
tRecuru0     =  4        ; U 1ST INITIAL COND
tRecurv0     =  5        ; V 1ST INITIAL COND
tun1         =  6        ; U(N-1)    ; NOT USED
tvn1         =  7        ; V(N-1)    ; NOT USED
tuRecuru0    =  8        ;
tuRecurv0    =  9        ;
;
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
tTblStep	=  21h
tTStep		=  22h
tThetaStep	=  23h
tuTStep		=  24h
tuThetStep	=  25h
;
tDeltaX		=  26h
tDeltaY		=  27h
;
tXFact		=  28h
tYFact		=  29h
;
tTblInput       =       2Ah
;
tFinN           =       2Bh
tFinI           =       2Ch
tFinPV          =       2Dh
tFinPMT         =       2Eh
tFinFV          =       2Fh
tFinPY          =       30h
tFinCY          =       31h
;
tRecurw0        =       32h         ; w0(1)
tuRecurw0       =       33h
;
tPlotStep       =       34h
tuPlotStep      =       35h
;
tXres           =       36h
tuXres          =       37h
;
tRecuru02       =       38h         ; u0(2)
tuRecuru02      =       39h
tRecurv02       =       3Ch         ; v0(2)
tuRecurv02      =       3Dh
tRecurw02       =       3Eh         ; w0(2)
tuRecurw02      =       3Fh
;
;======================================================================
;           2nd byte of t2ByteTok tokens
;======================================================================
;
tFinNPV         =       00h         ;
tFinIRR         =       01h         ;
tFinBAL         =       02h         ;
tFinPRN         =       03h         ;
tFinINT         =       04h         ;
tFinToNom       =       05h         ;
tFinToEff       =       06h         ;
tFinDBD         =       07h         ;
tLCM            =       08h         ;
tGCD            =       09h         ;
tRandInt        =       0Ah         ;
tRandBin        =       0Bh         ;
tSubStrng       =       0Ch         ;
tStdDev         =       0Dh         ;
tVariance       =       0Eh         ;
tInStrng        =       0Fh         ;
tDNormal        =       10h         ;
tInvNorm        =       11h         ;
tDT             =       12h         ;
tChI            =       13h         ;
tDF             =       14h         ;
tBINPDF         =       15h         ;
tBINCDF         =       16h         ;
tPOIPDF         =       17h         ;
tPOICDF         =       18h         ;
tGEOPDF         =       19h         ;
tGEOCDF         =       1Ah         ;
tNormalPDF      =       1Bh         ;
tTPDF           =       1Ch         ;
tChiPDF         =       1Dh         ;
tFPDF           =       1Eh         ;
tRandNorm       =       1Fh         ;
tFinFPMT        =       20h         ;
tFinFI          =       21h         ;
tFinFPV         =       22h         ;
tFinFN          =       23h         ;
tFinFFV         =       24h         ;
tConj           =       25h         ;
tReal           =       26h         ;
tImag           =       27h         ;
tAngle          =       28h         ;
tCumSum         =       29h         ;
tExpr           =       2Ah         ;
tLength         =       2Bh         ;
tDeltaLst       =       2Ch         ;
tRef            =       2Dh         ;
tRRef           =       2Eh         ;
tToRect         =       2Fh         ;
tToPolar        =       30h         ;
tConste         =       31h         ;
tSinReg         =       32h         ;
tLogistic       =       33h         ;
tLinRegTTest    =       34h         ;
tShadeNorm      =       35h         ;
tShadeT         =       36h         ;
tShadeChi       =       37h         ;
tShadeF         =       38h         ;
tMatToLst       =       39h         ;
tLstToMat       =       3Ah         ;
tZTest          =       3Bh         ;
tTTest          =       3Ch         ;
t2SampZTest     =       3Dh         ;
t1PropZTest     =       3Eh         ;
t2PropZTest     =       3Fh         ;
tChiTest        =       40h         ;
tZIntVal        =       41h         ;
t2SampZInt      =       42h         ;
t1PropZInt      =       43h         ;
t2PropZInt      =       44h         ;
tGraphStyle     =       45h         ;
t2SampTTest     =       46h         ;
t2SampFTest     =       47h         ;
tTIntVal        =       48h         ;
t2SampTInt      =       49h         ;
tSetupLst       =       4Ah         ;
tFinPMTend      =       4Bh         ;
tFinPMTbeg      =       4Ch         ;
tRealM          =       4Dh         ;
tPolarM         =       4Eh         ;
tRectM          =       4Fh         ;
tExprOn         =       50h         ;
tExprOff        =       51h         ;
tClrAllLst      =       52h         ;
tGetCalc        =       53h         ;
tDelVar         =       54h         ;
tEquToStrng     =       55h         ;
tStrngToEqu     =       56h         ;
tDelLast        =       57h         ;
tSelect         =       58h         ;
tANOVA          =       59h         ;
tModBox         =       5Ah         ;
tNormProb       =       5Bh         ;
;
;
tMGT            =       64h         ; VERTICAL SPLIT
tZFit           =       65h         ; ZOOM FIT
tDiag_on        =       66h         ; DIANOSTIC DISPLAY ON
tDiag_off       =       67h         ; DIANOSTIC DISPLAY OFF
tOkEnd2v0       =       67h         ;end of 2byte tokens for version 0.
tArchive        =       68h         ;archive
tUnarchive      =       69h         ;unarchive
tasm		=	  6Ah
tasmComp	=	  6Bh	      ; asm compile
tasmPrgm	=	  6Ch	      ; signifies a program is asm
tasmCmp 	=	  6Dh	      ; asm program is compiled
;
tLcapAAcute     =       6Eh
tLcapAGrave     =       6Fh
tLcapACaret     =       70h
tLcapADier      =       71h
tLaAcute        =       72h
tLaGrave        =       73h
tLaCaret        =       74h
tLaDier         =       75h
tLcapEAcute     =       76h
tLcapEGrave     =       77h
tLcapECaret     =       78h
tLcapEDier      =       79h
tLeAcute        =       7Ah
tLeGrave        =       7Bh
tLeCaret        =       7Ch
tLeDier         =       7Dh
;
tLcapIGrave     =       7Fh
tLcapICaret     =       80h
tLcapIDier      =       81h
tLiAcute        =       82h
tLiGrave        =       83h
tLiCaret        =       84h
tLiDier         =       85h
tLcapOAcute     =       86h
tLcapOGrave     =       87h
tLcapOCaret     =       88h
tLcapODier      =       89h
tLoAcute        =       8Ah
tLoGrave        =       8Bh
tLoCaret        =       8Ch
tLoDier         =       8Dh
tLcapUAcute     =       8Eh
tLcapUGrave     =       8Fh
tLcapUCaret     =       90h
tLcapUDier      =       91h
tLuAcute        =       92h
tLuGrave        =       93h
tLuCaret        =       94h
tLuDier         =       95h
tLcapCCed       =       96h
tLcCed          =       97h
tLcapNTilde     =       98h
tLnTilde        =       99h
tLaccent        =       9Ah
tLgrave         =       9Bh
tLdieresis      =       9Ch
tLquesDown      =       9Dh
tLexclamDown    =       9Eh
tLalpha         =       9Fh
tLbeta          =      0a0h
tLgamma         =      0a1h
tLcapDelta      =      0a2h
tLdelta         =      0a3h
tLepsilon       =      0a4h
tLlambda        =      0a5h
tLmu            =      0a6h
tLpi            =      0a7h
tLrho           =      0a8h
tLcapSigma      =      0a9h
;
;
tLphi           =      0aBh
tLcapOmega      =      0aCh
tLphat          =      0aDh
tLchi           =      0aEh
tLstatF         =      0aFh
;

tLa      =     0b0h
tLb      =     0b1h
tLc      =     0b2h
tLd      =     0b3h
tLsmalle =     0b4h
tLf      =     0b5h
tLsmallg =     0b6h
tLh      =     0b7h
tLi      =     0b8h
tLj      =     0b9h
tLk      =     0bAh
;

tLl      =     0bCh
tLm      =     0bDh
tLn      =     0bEh
tLo      =     0bFh
tLp      =     0c0h
tLq      =     0c1h
tLsmallr =     0c2h
tLs      =     0c3h
tLsmallt =     0c4h
tLu      =     0c5h
tLv      =     0c6h
tLw      =     0c7h
tLx      =     0c8h
tLy      =     0c9h
tLz      =     0cAh
tLsigma         =      0cbh
tLtau           =      0cch
tLcapIAcute     =      0cdh
;
;
tGarbagec =    0Ceh
TokEnd2V1       =      0Ceh         ;tLAST TOKEN IN THIS VERSION...
; the following tokens were added in OS version 1.15
tTilde          	=     0CFh            ; ~
tReserved     	=      0D0h
tAtSign        	 =     0D1h            ; @
tPound          	=     0D2h            ; #
tDollar        	 =     0D3h            ; $
tAmpersand     	 =     0D4h            ; &
tBackQuote      	=     0D5h            ; `
tSemicolon      	=     0D6h            ; ;
tBackSlash      	=     0D7h            ; \
tVertSlash      	=     0D8h            ; |
tUnderscore     	=     0D9h            ; _
tPercent        	=     0DAh            ; %
LastToken      	 =     0DAh            ;tLAST TOKEN IN THIS VERSION...
tInvalid   	  =     0DBh

;
;======================================================================
;           Data Type Equates                                                    
;======================================================================
;
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
UnknownEquObj   =       0Ah
NewEquObj       =       0Bh
CplxObj         =       0Ch
CListObj        =       0Dh
UndefObj        =       0Eh
WindowObj       =       0Fh
ZStoObj         =       10h
TblRngObj       =       11h
LCDObj          =       12h
BackupObj       =       13h
AppObj		=	  14h		;application, only used in menus/link
AppVarObj	=	  15h		;application variable
TempProgObj     =       16h           ;program, home deletes when finished
GroupObj        =       17h           ;group.
; 
;======================================================================
;	    Parser Equates
;======================================================================
OPRand      =              tRand
OPProd      =              tProd
OPSum       =              tSum
OPAdd       =              tAdd
OPSub       =              tSub
OPMult      =              tMul
OPDiv       =              tDiv
OPPower     =              tPower
OPXRoot     =              tXRoot
OPEq        =              tEQ
OPNeq       =              tNE
OPRound2    =              tRound
OPAug       =              tAug
OPMax       =              tMax
OPMin       =              tMin
OPAnd       =              tAnd
OPOr        =              tOr
OPXor       =              tXor
OPNcr       =              tnCr
OPNpr       =              tnPr
OPLt        =              tLT
OPLe        =              tLE
OPGt        =              tGT
OPGe        =              tGE
;
OPLog       =              tLog
OPTenX      =              tALog
OPLn        =              tLn
OPEtoX      =              tExp
OPNot       =              tNot
OPSin       =              tSin
OPASin      =              tASin
OPCos       =              tCos
OPACos      =              tACos
OPTan       =              tTan
OPATan      =              tATan
OPSinH      =              tSinH
OPASinH     =              tASinH
OPCosH      =              tCosH
OPACosH     =              tACosH
OPTanH      =              tTanH
OPATanH     =              tATanH
OPInverse   =              tRecip
OPDet       =              tDet
OPSqroot    =              tSqrt
OPSquare    =              tSqr
OPNegate    =              tChs
OPIpart     =              tIPart
OPFpart     =              tFPart
OPIntgr     =              tInt
OPFact      =              tFact
OPAbs       =              tAbs
OPIdent     =              tIdent
OPTranspose =              tTrnspos
OPSortA     =              tSortA
OPSortD     =              tSortD
OPToDec     =              tToDEC
OPToFrac    =              tToAbc
OPRadd      =              tRowPlus
OPRmlt      =              tmRow
OPRswap     =              tRowSwap
OPMltRadd   =              tmRowPlus
OPRand1     =              tRandM
OPSolve     =              tRoot
OPDeriv81   =              tNDeriv
OPQuad      =              tFnInt
OPSeq       =              tSeries
OPDim       =              tDim
OPRad       =              tFromRad
OPDeg       =              tFromDeg
OPRtopr     =              tRToPr
OPRtopo     =              tRToPo
OPPtorx     =              tPToRx
OPPtory     =              tPToRy
OPCube      =              tCube
OPCbrt      =              tCubRt
OPFmin      =              tFmin
OPFmax      =              tFmax
OPNpv           =       tIf         ; immediate unary
OPIrr           =       tIf+1       ; immediate unary
OPBal           =       tIf+2       ; immediate unary
OPPrn           =       tIf+3       ; immediate unary
OPIntr          =       tIf+4       ; immediate unary
OPToNom         =       tIf+5       ; immediate unary
OPToEff         =       tIf+6       ; immediate unary
OPFinDbd           =       tIf+7       ; immediate unary
OPLcm           =       tIf+8       ; immediate unary
OPGcd           =       tIf+9       ; immediate unary
OPRandInt       =       tIf+10      ; immediate unary
OPRandBin        =      tIf+11      ; immediate unary
OPSubstr        =       tIf+12      ; immediate unary
OPstDev         =       tIf+13      ; immediate unary
OPVariance      =       tIf+14      ; immediate unary
OPInstr         =       tIf+15      ; immediate unary
OPNormal        =       tIf+16      ; immediate unary
OPINormal       =       tIf+17      ; immediate unary
OPDt            =       tIf+18      ; immediate unary
OPChi           =       tIf+19      ; immediate unary
OPDf            =       tIf+20      ; immediate unary
OPBinpdf        =       tIf+21      ; immediate unary
OPBincdf        =       tIf+22      ; immediate unary
OPPoipdf        =       tIf+23      ; immediate unary
OPPoicdf        =       tIf+24      ; immediate unary
OPGeopdf        =       tIf+25      ; immediate unary
OPGeocdf        =       tIf+26      ; immediate unary
OPNormalpdf     =       tIf+27      ; immediate unary
OPTpdf          =       tIf+28      ; immediate unary
OPChipdf        =       tIf+29      ; immediate unary
OPFpdf          =       tIf+30      ; immediate unary
OPRandNrm       =       tIf+31      ; immediate unary
OPConj           =       tZoomIn
OPReal           =       tZoomIn	   +1
OPImag           =       tZoomIn	   +2
OPAngle          =       tZoomIn	   +3
OPCumSum         =       tZoomIn	   +4
OPExpr           =       tZoomIn	   +5
OPLength         =       tZoomIn	   +6
OPDeltaLst       =       tZoomIn	   +7
OPRef            =       tZoomIn	   +8
OPRref           =       tZoomIn	   +9
OPConst          =       tZoomIn	   +10

;======================================================================
;           I/O equates                                                          
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
;	Interrupt equates
;======================================================================
;
iAll	= 1011b
intrptEnPort	= 3 ; interrupt enable (in/out) 
;
;======================================================================
;	Memory paging equates
;======================================================================
;
memPageAPort	= 6 ; memory page A (in/out)
;
;======================================================================
;	LCD Driver equates
;======================================================================
LCDinstPort	= 10h
LCDdataPort = 11h
;
;======================================================================
;	Graph style definitions
;======================================================================
;
GSline          =     0		;Func, Param, and Polar modes default
GSthick         =     1
GSabove         =     2
GSbelow         =     3
GSpath          =     4
GSanimate       =     5
GSdot           =     6		;Recursive mode default

;======================================================================
;           DEVICE CODES                                                       
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
;
;======================================================================
;           System Error Codes                                                  
;======================================================================
;
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
E_LnkErr        =     31+E_EDIT
E_LnkMemErr     =     32+E_EDIT
E_LnkTransErr   =     33+E_EDIT
E_LnkDupErr     =     34+E_EDIT
E_LnkMemFull    =     35+E_EDIT
E_Unknown       =     36+E_EDIT

E_Scale         =     37+E_EDIT
E_IdNotFound    =     38
E_NoMode        =     39+E_EDIT
E_Validation    =     40
E_Length        =     41+E_EDIT
E_Application   =     42+E_EDIT
E_AppErr1       =     43+E_EDIT
E_AppErr2       =     44+E_EDIT
E_ExpiredApp	=	45
E_BadAddr	=	46
E_Archived      =     47+E_EDIT
E_Version       =     48
E_ArchFull      =     49
E_Variable      =     50+E_EDIT
E_Duplicate     =     51+E_EDIT
HigErrNum       =     51


; obsolete error numbers34              ;first LINK error
E_LinkIOChkSum	=	34
E_LinkIOTimeOut	=	35
E_LinkIOBusy	=	36
E_LinkIOVer	=	37

;
;======================================================================
;           EQUATES TO RAM LOCATIONS FOR STAT VARS 
;======================================================================
;
;
FPLEN	=	9		;Length of a floating-point number.
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
PStat   =     MedY3      + 2*FPLEN
ZStat   =     PStat      + FPLEN
TStat   =     ZStat      + FPLEN
ChiStat =     TStat      + FPLEN
FStat   =     ChiStat    + FPLEN
DF      =     FStat      + FPLEN
Phat    =     DF         + FPLEN
Phat1   =     Phat       + FPLEN
Phat2   =     Phat1      + FPLEN
MeanX1  =     Phat2      + FPLEN
StdX1   =     MeanX1     + FPLEN
StatN1  =     StdX1      + FPLEN
MeanX2  =     StatN1     + FPLEN
StdX2   =     MeanX2     + FPLEN
StatN2  =     StdX2      + FPLEN
StdXP2  =     StatN2     + FPLEN
SLower  =     StdXP2     + FPLEN
SUpper  =     SLower     + FPLEN
SStat   =     SUpper     + FPLEN

F_DF	=     anovaf_vars
F_SS	=     F_DF + FPLEN
F_MS	=     F_SS + FPLEN
E_DF	=     F_MS + FPLEN
E_SS	=     E_DF + FPLEN
E_MS	=     E_SS + FPLEN