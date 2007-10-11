; ==================================================================================================
; VARS.INC - Holds game-wide variables.
; ==================================================================================================

;Ship coordinates (DON'T SWAP ORDER!):
Ship_Y:		.db 0 
Ship_X:		.db 0

;Bullet coordinates: (Format: X,Y,Enabled)
Bullet_Pos:	.db 44,00,01
		.db 44,25,01
		.db 44,50,01
		.db 44,72,01
		.db 0,0,128

Bullet_Reset:	.db 44,00,00	;Initial bullet positions.
		.db 44,18,00
		.db 44,36,00
		.db 44,54,00

;If anything on the ground has been hit, this flag gets set.
Blown_Up_Ground:
		.db 0

;Enemy Coordinates: (Format: X,Y,Enabled,How far through path)
Enemies:	.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,0,0
		.db 0,0,128,0
		.db 128,128,128,128

Path_Progress:	.db 0
Path_Pointer:	.dw 0

BlownUpTileLoc:	.dw 0

Explosion_Delay: .db 0

Reg_16_1:	.dw 0
Reg_16_2:	.dw 0

Score:		.db 0,0,0,0,0,0	;Score in units, 10s, 100s, 1000s &c

Attack_Type:	.db 0

Score_Pos:	.db 0

Lives:		.db 4	;How many lives ("ships") there are left.

Extra_Active:	.db 1,1,1,128
Extra_Ships:	.db 0,0,0

Extra_Orig:	.db 34,24,14

Death_Timer:	.db 0

Delay_Between_Enemies:	.db 0

True_Contrast:	.db 0

Eye_1:		.dw 0	;Positions in page file of eyeballs for game ending.
Eye_2:		.dw 0

Blocker:	.db 0

Fired:		.db 0

Game_Type:	.db 0		;Classic or Extended

Game_Pos:	.db 19,32	;Position LUT for selection rectangle.

Level_Num:	.db 0

Extra_Ship_Delay:	.db 0	;How long are those extra ships on screen for?

E_PointsAvailable:	.db 0	;How many points do I get for killing an enemy?

PauseCounter:		.db 0

Mine_Y:			.db 0	;\_ (x,y) position of mine.
Mine_X:			.db 0	;/
Mine_Loop:		.db 0	;Mine delay counter.

EnterInit:		.db 0,0,0	;3 bytes for entering initials.

#ifdef TI83I
.db 0		;Extra byte for compatibilty issues.
#endif



PowerUpDelay:		.db 0
Num_Powerups:		.db 0
Powerup_Type:		.db 0
EMP_Pos:		.db 0
Shield_Time:		.db 0
Laser_Running:		.db 0
Slow_Running:		.db 0
Kamikaze_Active:	.db 0
Speed_Active:		.db 0


GS_Terrain:		.db 0	;Is the terrain inverted for clarity?
Multiply_Key:		.db 0	;Is MULTIPLY held down?

Powerup_Y:		.db 0
Powerup_X:		.db 0
Mode_Down:		.db 0

Use_Face:		.db 0		;Does the level use the end face or not?

Last_Scroll_Pos:	.db 0		;How far were we scrolled last frame?
Delay_Enemies:		.db 0		;Delay between enemy attack patterns.