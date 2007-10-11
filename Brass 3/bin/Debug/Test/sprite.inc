; ==================================================================================================
; SPRITE.INC - handles drawing sprites - Not written by me!
; ==================================================================================================

;-----------------------------------------------------------;
;                                                           ;
; Sprite Drawing Routines                                   ;
; Version 1.2                                               ;
;                                                           ;
; Copyright (c)2001 TI-Calculator Programming Alliance      ;
; Written and Programmed by James Vernon <james@calc.org>   ;
; ICQ#: 71589304                                            ;
; http://tcpa.calc.org                                      ;
;                                                           ;
;-----------------------------------------------------------;

;----->
; GENERAL USAGE:
;  #include "sprite.asm"
;<-----

; Ensure gbuf is defined
#ifndef gbuf
#ifdef SPRITE_86
#define gbuf _plotsscreen
#else
#define gbuf plotsscreen
#endif
#endif

#ifdef PUT_ALIGNED_SPRITE
#ifdef PUT_MASKED_ALIGNED_SPRITE
#define GET_ALIGNED_SPRITE_POS
#endif
#endif

#ifndef BOTTOM_CLIP
#define BOTTOM_CLIP 64
#endif

#ifndef RIGHT_CLIP
#ifdef SPRITE_86
#define RIGHT_CLIP 128
#else
#define RIGHT_CLIP 96
#endif
#endif

;------------------------------------------------
; putAlignedSprite - Draw an 8x8 aligned sprite
; Usage:    #define PUT_ALIGNED_SPRITE
;------------------------------------------------
#ifdef PUT_ALIGNED_SPRITE
putAlignedSprite:

        ld      h,0
        add     hl,hl                           ; x2
        add     hl,hl                           ; x4
        add     hl,hl                           ; x8
        add     hl,hl                           ; x16
        add     hl,hl                           ; x32
#ifdef SPRITE_86
        add     hl,hl                           ; x64
        add     hl,hl                           ; x128
#else
        ld      b,h
        ld      c,l
        add     hl,hl                           ; x64
        add     hl,bc                           ; x96

        ld      c,a
        ld      b,0
        add     hl,bc
AlignedSpriteBigBuf:
        ld      bc,gbuf
        add     hl,bc                           ; HL => Where to put sprite
        ld      b,8
#endif
putASLoop:
        ld      a,(de)
        ld      (hl),a
        inc     de
        push    de
#ifdef SPRITE_86
        ld      de,16
#else
        ld      de,12
#endif
        add     hl,de
        pop     de
        djnz    putASLoop
        ret
#endif

;------------------------------------------------
; putMaskedAlignedSprite - Draw an 8x8 masked aligned sprite
; Usage:    #define PUT_MASKED_ALIGNED_SPRITE
;------------------------------------------------
#ifdef PUT_MASKED_ALIGNED_SPRITE
putMaskedAlignedSprite:
#ifdef GET_ALIGNED_SPRITE_POS
        call    getAlignedSpritePos
#else
        ld      h,0
        add     hl,hl                           ; x2
        add     hl,hl                           ; x4
        add     hl,hl                           ; x8
        add     hl,hl                           ; x16
        add     hl,hl                           ; x32
#ifdef SPRITE_86
        add     hl,hl                           ; x64
        add     hl,hl                           ; x128
#else
        ld      b,h
        ld      c,l
        add     hl,hl                           ; x64
        add     hl,bc                           ; x96
#endif
        ld      c,a
        ld      b,0
        add     hl,bc
        ld      bc,gbuf
        add     hl,bc                           ; HL => Where to put sprite
        ld      b,8
#endif
        push    de
        pop     ix                              ; IX => Sprite
#ifdef SPRITE_86
        ld      de,16
#else
        ld      de,12
#endif
putMASLoop:
        ld      a,(ix+8)
        cpl
        and     (hl)
        ld      (hl),a
        ld      a,(ix)
        and     (ix+8)
        or      (hl)
        ld      (hl),a
        add     hl,de
        inc     ix
        djnz    putMASLoop
        ret
#endif

;------------------------------------------------
; putSprite - Draw an 8x8 sprite to gbuf (using OR method), no clipping
; Usage:    #define PUT_SPRITE
;------------------------------------------------
#ifdef PUT_SPRITE
putSprite:
        ld      h,0
        ld      b,h
        add     hl,hl                           ; x2
        add     hl,hl                           ; x4
#ifdef SPRITE_86
        add     hl,hl                           ; x8
        add     hl,hl                           ; x16
#else
        ld      c,l
        add     hl,hl                           ; x8
        add     hl,bc                           ; x12
#endif
        ld      c,a
        srl     c
        srl     c
        srl     c
        add     hl,bc
        ld      bc,gbuf
        add     hl,bc                           ; HL => Where to display sprite
        and     $07
        ld      c,a
        push    de
        pop     ix                              ; IX => Sprite
        ld      b,8
putSLoop:
        ld      d,(ix)
        ld      e,0
        ld      a,c
        or      a
        jr      z,putSSkip
putSLoop2:
        srl     d
        rr      e
        dec     a
        jr      nz,putSLoop2
putSSkip:
        ld      a,(hl)
        or      d
        ld      (hl),a
        inc     hl
        ld      a,(hl)
        or      e
        ld      (hl),a
#ifdef SPRITE_86
        ld      de,15
#else
        ld      de,11
#endif
        add     hl,de
        inc     ix
        djnz    putSLoop
        ret
#endif

;------------------------------------------------
; putClippedSprite - Draw an 8x8 unmasked sprite to gbuf with clipping
; Usage:    #define PUT_CLIPPED_SPRITE
;------------------------------------------------
#ifdef PUT_CLIPPED_SPRITE
#ifndef FIND_PIXEL
#define FIND_PIXEL
#endif
putClippedSprite:
        ld      a,c
        cp      200
        jr      nc,putCSNoBottomClip
        cp      BOTTOM_CLIP
        ret     nc
putCSNoBottomClip:
        ld      a,$FF
        ld      (_putCSClipMask),a
        bit     7,b
        jr      z,putCSCheckRightClip
        ld      a,b
        cp      249
        ret     c
        neg
        push    bc
        ld      b,a
        ld      a,$FF
putCSLeftClip:
        srl     a
        djnz    putCSLeftClip
        ld      (_putCSClipMask),a
        pop     bc
        ld      a,b
#ifdef SPRITE_86
        add     a,128
#else
        add     a,96
#endif
        ld      b,a
        dec     c
        jr      putCSCheckBottomClip
putCSCheckRightClip:
        ld      a,b
        sub     RIGHT_CLIP-7
        jr      c,putCSCheckBottomClip
        push    bc
        ld      b,a
        inc     b
        ld      a,$FF
putCSRightClip:
        add     a,a
        djnz    putCSRightClip
        ld      (_putCSClipMask),a
        pop     bc
putCSCheckBottomClip:
        ld      a,8
        ld      (_putCSRows2Put),a
        bit     7,c
        jr      nz,putCSCheckTopClip
        ld      a,BOTTOM_CLIP-1
        sub     c
        ret     c
        inc     a
        cp      8
        jr      nc,putCSClippingDone
        ld      (_putCSRows2Put),a
        jr      putCSClippingDone
putCSCheckTopClip:
        ld      a,c
        cp      249
        ret     c
        push    bc
        neg
        ld      b,a
        sub     8
        neg
        ld      (_putCSRows2Put),a
putCSTopClip:
        inc     hl
        djnz    putCSTopClip
        pop     bc
        ld      c,0
putCSClippingDone:
        push    hl
        pop     ix                              ; IX => Sprite
        ld      a,$00
_putCSRows2Put = $-1
        push    af
        call    findPixel
        ld      (_putCSBitMask),a
        pop     bc
putCSPutRow:
        push    bc
        push    hl
        ld      a,$00
_putCSClipMask = $-1
        ld      e,a
        ld      b,8
        ld      c,(ix)
        inc     ix
putCSPutCol:
        push    bc
        ld      a,$00
_putCSBitMask  = $-1
        and     (hl)
        rlc     e
        jr      nc,putCSNextBit
        ld      a,(_putCSBitMask)
        rlc     c
        jr      c,putCSBitOn
        jr      putCSNextBit
putCSBitOn:
        or      (hl)
        ld      (hl),a
putCSNextBit:
        ld      a,(_putCSBitMask)
        rrca
        ld      (_putCSBitMask),a
        jr      nc,putCSNoIncHL
        inc     hl
putCSNoIncHL:
        pop     bc
        rlc     c
        djnz    putCSPutCol
        pop     hl
#ifdef SPRITE_86
        ld      de,16
#else
        ld      de,12
#endif
        add     hl,de
        pop     bc
        djnz    putCSPutRow
        ret
#endif

;------------------------------------------------
; putClippedMaskedSprite - Draw an 8x8 masked sprite to gbuf with clipping
; Usage:    #define PUT_CLIPPED_MASKED_SPRITE
;------------------------------------------------
#ifdef PUT_CLIPPED_MASKED_SPRITE
#ifndef FIND_PIXEL
#define FIND_PIXEL
#endif
putClippedMaskedSprite:
        ld      a,c
        cp      200
        jr      nc,putCMSNoBottomClip
        cp      BOTTOM_CLIP
        ret     nc
putCMSNoBottomClip:
        ld      a,$FF
        ld      (_putCMSClipMask),a
        bit     7,b
        jr      z,putCMSCheckRightClip
        ld      a,b
        cp      249
        ret     c
        neg
        push    bc
        ld      b,a
        ld      a,$FF
putCMSLeftClip:
        srl     a
        djnz    putCMSLeftClip
        ld      (_putCMSClipMask),a
        pop     bc
        ld      a,b
#ifdef SPRITE_86
        add     a,128
#else
        add     a,96
#endif
        ld      b,a
        dec     c
        jr      putCMSCheckBottomClip
putCMSCheckRightClip:
        ld      a,b
        sub     RIGHT_CLIP-7
        jr      c,putCMSCheckBottomClip
        push    bc
        ld      b,a
        inc     b
        ld      a,$FF
putCMSRightClip:
        add     a,a
        djnz    putCMSRightClip
        ld      (_putCMSClipMask),a
        pop     bc
putCMSCheckBottomClip:
        ld      a,8
        ld      (_putCMSRows2Put),a
        bit     7,c
        jr      nz,putCMSCheckTopClip
        ld      a,BOTTOM_CLIP-1
        sub     c
        ret     c
        inc     a
        cp      8
        jr      nc,putCMSClippingDone
        ld      (_putCMSRows2Put),a
        jr      putCMSClippingDone
putCMSCheckTopClip:
        ld      a,c
        cp      249
        ret     c
        push    bc
        neg
        ld      b,a
        sub     8
        neg
        ld      (_putCMSRows2Put),a
putCMSTopClip:
        inc     hl
        djnz    putCMSTopClip
        pop     bc
        ld      c,0
putCMSClippingDone:
        push    hl
        pop     ix                              ; IX => Sprite, Mask
        ld      a,$00
_putCMSRows2Put = $-1
        push    af
        call    findPixel
        ld      (_putCMSBitMask),a
        pop     bc
putCMSPutRow:
        push    bc
        push    hl
        ld      a,$00
_putCMSClipMask = $-1
        ld      e,a
        ld      a,(ix+8)
        and     e
        ld      e,a
        ld      b,8
        ld      c,(ix)
        inc     ix
putCMSPutCol:
        push    bc
        sla     d
        ld      a,$00
_putCMSBitMask  = $-1
        and     (hl)
        rlc     e
        jr      nc,putCMSNextBit
        ld      a,(_putCMSBitMask)
        rlc     c
        jr      c,putCMSBitOn
        cpl
        and     (hl)
        ld      (hl),a
        jr      putCMSNextBit
putCMSBitOn:
        or      (hl)
        ld      (hl),a
putCMSNextBit:
        ld      a,(_putCMSBitMask)
        rrca
        ld      (_putCMSBitMask),a
        jr      nc,putCMSNoIncHL
        inc     hl
putCMSNoIncHL:
        pop     bc
        rlc     c
        djnz    putCMSPutCol
        pop     hl
#ifdef SPRITE_86
        ld      de,16
#else
        ld      de,12
#endif
        add     hl,de
        pop     bc
        djnz    putCMSPutRow
        ret
#endif

;------------------------------------------------
; getAlignedSpritePos - Get position on gbuf to display an aligned sprite
;
; Input:    A = X
;           L = Y
; Output:   HL => Where to display sprite
;           B = 8 rows to copy
;------------------------------------------------
#ifdef GET_ALIGNED_SPRITE_POS
getAlignedSpritePos:
        ld      h,0
        add     hl,hl                           ; x2
        add     hl,hl                           ; x4
        add     hl,hl                           ; x8
        add     hl,hl                           ; x16
        add     hl,hl                           ; x32
#ifdef SPRITE_86
        add     hl,hl                           ; x64
        add     hl,hl                           ; x128
#else
        ld      b,h
        ld      c,l
        add     hl,hl                           ; x64
        add     hl,bc                           ; x96
#endif
        ld      c,a
        ld      b,0
        add     hl,bc
        ld      bc,gbuf
        add     hl,bc
        ld      b,8
        ret
#endif

;------------------------------------------------
; findPixel - Get byte and bit offset to a pixel on gbuf
;
; Input:    B = X
;           C = Y
; Output:   HL => Byte in gbuf
;           A = Bitmask
;------------------------------------------------
#ifdef FIND_PIXEL
findPixel:
        ld      a,b
        ld      b,0
        ld      h,b
        ld      l,c                             ; HL = Y x 1
#ifdef SPRITE_86
        add     hl,hl                           ; x2
        add     hl,hl                           ; x4
        add     hl,hl                           ; x8
        add     hl,hl                           ; x16
#else
        add     hl,bc                           ; x2
        add     hl,bc                           ; x3
        add     hl,hl                           ; x6
        add     hl,hl                           ; x12
#endif
        ld      bc,gbuf
        add     hl,bc
        ld      b,0
        ld      c,a
        and     $07
        srl     c
        srl     c
        srl     c
        add     hl,bc
        ld      b,a
        inc     b
        ld      a,$01
findPixelLoop:
        rrca
        djnz    findPixelLoop
        ret
#endif

.end
