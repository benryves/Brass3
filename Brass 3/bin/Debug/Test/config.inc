; ==================================================================================================
; CONFIG.INC - Game settings, really.
; ==================================================================================================

;What to compile for? There are three options:
;TI83I - Compiles for the TI-83 (Ion).
;TI83P - Compiles for the TI-83 Plus (Ion).
;TI83M - Compiles for the TI-83 Plus (MirageOS). 


#define TI83I

;#define BETA

#define BG_Scroll_Delay 	3	;How many game loops before I should scroll?
#define NUM_BULLETS		4	;How many bullets?
#define Max_Level_Size		4000	;How many bytes should be set aside for the level?
#define Num_Enemies		15	;The number of enemies
#define Max_Explosions		(EO_EB-Explosion_Buffer)/3	;Max number of explosions!
#define Max_Frames_Explosion	6*16	;Number of frames of explosion animation.
#define Explosion_Frame_Rate	8	;Delay between explosion animation frames.
#define ZombieTime		150	;For how long should the game run after you die?
#define Flash_Delay_Time	10	;How many frames between each flash of "Press 2nd"
#define DitherBuffer SavesScreen
#define ExtraShipDuration	100	;How long should the extra ships "pause" for?
#define Pause_Length		50	;How long are the pauses?
#define NumLevels 		(EO_Level_Table-Level_Table)/2
#define Mine_Speed		12	;How many loops before the mine is shifted.


;Debugging options (=cheats!)
;#define Hide_Back			;Enable to not draw the background
;#define Infinite_Lives			;If enabled, you have infinite lives.
;#define Indestructable			;If enabled, you are indestructable
;#define SkipLevelKey			;If enabled, hitting [/] skips a level.
;#define DRAW_SPRITE_OVER		;When enabled draws the sprite you are "over" on the background.
