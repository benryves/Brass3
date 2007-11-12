;+----------------------------------------+
;|      Doors CS v6.0 Include File        |
;|         TI-83+/TI-84+ Version          |
;|         Revised March 17, 2007         |
;|           By Kerm Martian              |
;|                                        |
;| Please see http://dcs.cemetech.net for |
;| information and updates including a    |
;| full Developers' SDK.                  |
;+----------------------------------------+

;=================================;
;====== DoorsCS Subroutines ======;
;=================================;

#macro PushGUIStack(xx,yyyy,zzzz)
	ld a,xx
	ld hl,yyyy
	ld de,zzzz
	call PushGUIStack
#endmacro
#macro PopGUIStack()
	call PopGUIStack
#endmacro
#macro PopGUIStacks(xx)
	ld b,xx
	call PopGUIStackS
#endmacro
#macro OpenGUIStack()
	call OpenGUIStack
#endmacro
#macro CloseGUIStack()
	call CloseGUIStack
#endmacro
#macro GUIMouse(xxxx)
	ld hl,xxxx
	call GUIMouse
#endmacro
#macro GUIRender()
	call RenderGUI
#endmacro
#macro RenderGUI()
	call RenderGUI
#endmacro
#macro FileSaveAs(location,filetypepointer,size)
	ld hl,location
	ld de,filetypepointer
	ld bc,size
	call FileSaveAs
#endmacro
#macro FileSave(location,filetypepointer,size)
	ld hl,location
	ld de,filetypepointer
	ld bc,size
	call FileSave
#endmacro
#macro FileOpen()
	call FileOpen
#endmacro
#macro MouseRecover()
	call ResetAppPage
#endmacro
#macro Pause()
	call Pause
#endmacro
#macro GUIFindFirst()
	call GUIFindFirst
#endmacro
;#macro GUIFindNext(xxxx,yyyy)
;	ld hl,xxxx
;	ld de,yyyy
;	call GUIFindNext
;#endmacro
;#macro GUIFindNext(hl,de)
;	call GUIFindNext
;#endmacro
#macro GUIFindNext()
	call GUIFindNext
#endmacro

;=================================;
;======== Ion Subroutines ========;
;=================================;

iversion              = $4083
irandom               = $4086
isprite               = $4089
ilsprite              = $408C
igetpix               = $408F
ifastcopy             = $4092
idetect               = $4095
idecomp               = $4098

ionversion            = $4083
ionrandom             = $4086
ionputsprite          = $4089
ionlargesprite        = $408C
iongetpixel           = $408F
ionfastcopy           = $4092
iondetect             = $4095
iondecompress         = $4098

iputsprite            = $4089
ilargesprite          = $408C
igetpixel             = $408F
idecompress           = $4098

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
Small_Window 	      = $41D6
ClrDialogFull 	      = $41D9
LargeWindow 	      = $41DC
ClrWinFull            = $41DF
PlaySound 	          = $41E2
VDispHL 	          = $41E5
Pause 	              = $41E8
hDetect 	          = $41EB
OpenGUIStack 	      = $41EE
CloseGUIStack	      = $41F1
PushGUIStack 	      = $41F4
PopGUIStack 	      = $41F7
RenderGUI 	          = $41FA
PopGUIStacks	      = $41FD
GUIMouse 	          = $4200
GUIFindFirst 	      = $4203
GUIFindNext 	      = $4206
ResetAppPage 	      = $4000
Cn2_Setup	          = $4209
Cn2_Clear_SendBuf     = $420C
Cn2_Clear_RecBuf      = $420F
Cn2_Setdown	          = $4212
FileOpen 	          = $4215
FileSave 	          = $4218
FileSaveAs 	          = $421B
DispLongInt	          = $421E

null					=$00
LargeWin				=$01
SmallWin				=$02
FullScreenImg			=$03
Text					=$04
WinButtons				=$05
WrappedText				=$06
ButtonText				=$07
ButtonImg				=$08
TextLineIn				=$09
Radio					=$0A
Checkbox				=$0B
ByteInt					=$0C
WordInt					=$0D
Hotspot					=$0E
TextMultiline			=$0F
Sprite					=$10
LargeSprite				=$11
PassIn					=$12
ScrollVert				=$13
ScrollHoriz				=$14
Border					=$15
Rect					=$16
Custom					=$17
MouseCursor				=$18