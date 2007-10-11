; ==================================================================================================
; POWERUPS.INC - Powerup graphics and loading code.
; Start Powerup just sets the correct number of uses and graphic, using a LUT.
; ==================================================================================================

PowerupCache:
.db %11111111
.db %11001111
.db %10110111
.db %10110110
.db %11110110
.db %11111001
.db %11111111

Times:
.db %11111111
.db %11111111
.db %10011111
.db %11111111
.db %10011111
.db %11111111
.db %11111111

pEMP:
.db %11111111
.db %11001111
.db %10110111
.db %10110110
.db %11110110
.db %11111001
.db %11111111

pLaser:
.db %11111111
.db %11101010
.db %11110001
.db %10000100
.db %11110001
.db %11101010
.db %11111111

pShield:
.db %11111111
.db %11101011
.db %11011101
.db %11110111
.db %11011101
.db %11101011
.db %11111111

pTime:
.db %11111111
.db %11100011
.db %11010101
.db %11010001
.db %11011101
.db %11100011
.db %11111111

pKamikaze:
.db %11111111
.db %11100011
.db %11010101
.db %11001001
.db %11100011
.db %11010101
.db %11111111

pSpeed:
.db %11111111
.db %11011101
.db %10110110
.db %10100010
.db %10110110
.db %11011101
.db %11111111

;First 2b = address of powerup sprite, last byte = number of uses
Powerup_Table:
.dw pEMP \ .db 1
.dw pShield \ .db 2
.dw pLaser \ .db 3
.dw pTime \ .db 2
.dw pKamikaze \ .db 1
.dw pSpeed \ .db 2
EO_Powerup_Table:

Start_Powerup:
    ld (Powerup_Type),a
    ld l,a
    ld h,0
    push hl
    pop de
    add hl,hl
    add hl,de
    ld de,Powerup_Table
    add hl,de
    push hl
    pop ix

    ld a,(ix+2)
    ld (Num_Powerups),a

    ld h,(ix+1)
    ld l,(ix+0)
    ld de,PowerupCache
    ld bc,7
    ldir

    ret


