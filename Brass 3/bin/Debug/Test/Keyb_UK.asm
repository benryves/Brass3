; UK key layout

#include "Emerson.inc" ; Equates

.db "EM1"

normal_layout:
.db $1B	; em_sc_esc
.db $FF	; em_sc_f1
.db $FF	; em_sc_f2
.db $FF	; em_sc_f3
.db $FF	; em_sc_f4
.db $FF	; em_sc_f5
.db $FF	; em_sc_f6
.db $FF	; em_sc_f7
.db $FF	; em_sc_f8
.db $FF	; em_sc_f9
.db $FF	; em_sc_f10
.db $FF	; em_sc_f11
.db $FF	; em_sc_f12
.db $FF	; em_sc_sysrq
.db $FF	; em_sc_scroll
.db '`'	; em_sc_btick
.db '1'	; em_sc_1
.db '2'	; em_sc_2
.db '3'	; em_sc_3
.db '4'	; em_sc_4
.db '5'	; em_sc_5
.db '6'	; em_sc_6
.db '7'	; em_sc_7
.db '8'	; em_sc_8
.db '9'	; em_sc_9
.db '0'	; em_sc_0
.db '-'	; em_sc_minus
.db '='	; em_sc_equal
.db $08	; em_sc_backsp
.db $09	; em_sc_tab
.db 'q'	; em_sc_q
.db 'w'	; em_sc_w
.db 'e'	; em_sc_e
.db 'r'	; em_sc_r
.db 't'	; em_sc_t
.db 'y'	; em_sc_y
.db 'u'	; em_sc_u
.db 'i'	; em_sc_i
.db 'o'	; em_sc_o
.db 'p'	; em_sc_p
.db $C1	; em_sc_lbrack
.db ']'	; em_sc_rbrack
.db $0A	; em_sc_enter
.db $FF	; em_sc_caps
.db 'a'	; em_sc_a
.db 's'	; em_sc_s
.db 'd'	; em_sc_d
.db 'f'	; em_sc_f
.db 'g'	; em_sc_g
.db 'h'	; em_sc_h
.db 'j'	; em_sc_j
.db 'k'	; em_sc_k
.db 'l'	; em_sc_l
.db ';'	; em_sc_colon
.db "'"	; em_sc_apos
.db '#'	; em_sc_hash
.db $FF	; em_sc_lshift
.db 92	; em_sc_bslash
.db 'z' ; em_sc_z
.db 'x' ; em_sc_x
.db 'c' ; em_sc_c
.db 'v' ; em_sc_v
.db 'b' ; em_sc_b
.db 'n' ; em_sc_n
.db 'm' ; em_sc_m
.db ',' ; em_sc_comma
.db '.' ; em_sc_fullstop
.db '/' ; em_sc_fslash
.db $FF	; em_sc_rshift
.db $FF	; em_sc_lcrtl
.db $FF	; em_sc_alt
.db ' '	; em_sc_space
.db $FF	; em_sc_n0
.db $FF	; em_sc_n1
.db $FF	; em_sc_n2
.db $FF	; em_sc_n3
.db $FF	; em_sc_n4
.db $FF	; em_sc_n5
.db $FF	; em_sc_n6
.db $FF	; em_sc_n7
.db $FF	; em_sc_n8
.db $FF	; em_sc_n9
.db $FF	; em_sc_nlock
.db '*'	; em_sc_nmul
.db '-'	; em_sc_nsub
.db '+'	; em_sc_nadd
.db $FF	; em_sc_npoint
.db $FF	; em_sce_lwin
.db $FF	; em_sce_altgr
.db $FF	; em_sce_rwin
.db $FF	; em_sce_app
.db $FF	; em_sce_rctrl
.db $FF	; em_sce_sleep
.db $FF	; em_sce_wake
.db $FF	; em_sce_power
.db $FF	; em_sce_printsc
.db $FF	; em_sce_insert
.db $7F	; em_sce_delete
.db $FF	; em_sce_home
.db $FF	; em_sce_end
.db $FF	; em_sce_pageup
.db $FF	; em_sce_pagedown
.db $FF	; em_sce_up
.db $FF	; em_sce_left
.db $FF	; em_sce_down
.db $FF	; em_sce_right
.db '/'	; em_sce_ndiv
.db 10	; em_sce_nenter
normal_layout_end:

shifted_layout:
.db $1B	; em_sc_esc
.db $FF	; em_sc_f1
.db $FF	; em_sc_f2
.db $FF	; em_sc_f3
.db $FF	; em_sc_f4
.db $FF	; em_sc_f5
.db $FF	; em_sc_f6
.db $FF	; em_sc_f7
.db $FF	; em_sc_f8
.db $FF	; em_sc_f9
.db $FF	; em_sc_f10
.db $FF	; em_sc_f11
.db $FF	; em_sc_f12
.db $FF	; em_sc_sysrq
.db $FF	; em_sc_scroll
.db '-'	; em_sc_btick
.db '!'	; em_sc_1
.db $22	; em_sc_2
.db $5B	; em_sc_3
.db '$'	; em_sc_4
.db '%'	; em_sc_5
.db '^'	; em_sc_6
.db '&'	; em_sc_7
.db '*'	; em_sc_8
.db '('	; em_sc_9
.db ')'	; em_sc_0
.db '_'	; em_sc_minus
.db '+'	; em_sc_equals
.db 8	; em_sc_backsp
.db 9	; em_sc_tab
.db 'Q'	; em_sc_q
.db 'W'	; em_sc_w
.db 'E'	; em_sc_e
.db 'R'	; em_sc_r
.db 'T'	; em_sc_t
.db 'Y'	; em_sc_y
.db 'U'	; em_sc_u
.db 'I'	; em_sc_i
.db 'O'	; em_sc_o
.db 'P'	; em_sc_p
.db '{'	; em_sc_lbrack
.db '}'	; em_sc_rbrack
.db 10	; em_sc_enter
.db $FF	; em_sc_caps
.db 'A'	; em_sc_a
.db 'S'	; em_sc_s
.db 'D'	; em_sc_d
.db 'F'	; em_sc_f
.db 'G'	; em_sc_g
.db 'H'	; em_sc_h
.db 'J'	; em_sc_j
.db 'K'	; em_sc_k
.db 'L'	; em_sc_l
.db ':'	; em_sc_colon
.db "@"	; em_sc_apos
.db '~'	; em_sc_hash
.db $FF	; em_sc_lshift
.db '|'	; em_sc_bslash
.db 'Z' ; em_sc_z
.db 'X' ; em_sc_x
.db 'C' ; em_sc_c
.db 'V' ; em_sc_v
.db 'B' ; em_sc_b
.db 'N' ; em_sc_n
.db 'M' ; em_sc_m
.db "<" ; em_sc_comma
.db ">" ; em_sc_fullstop
.db '?' ; em_sc_fslash
.db $FF	; em_sc_rshift
.db $FF	; em_sc_lcrtl
.db $FF	; em_sc_alt
.db ' '	; em_sc_space
.db $FF	; em_sc_n0
.db $FF	; em_sc_n1
.db $FF	; em_sc_n2
.db $FF	; em_sc_n3
.db $FF	; em_sc_n4
.db $FF	; em_sc_n5
.db $FF	; em_sc_n6
.db $FF	; em_sc_n7
.db $FF	; em_sc_n8
.db $FF	; em_sc_n9
.db $FF	; em_sc_nlock
.db '*'	; em_sc_nmul
.db '-'	; em_sc_nsub
.db '+'	; em_sc_nadd
.db $7F	; em_sc_npoint
.db $FF	; em_sce_lwin
.db $FF	; em_sce_altgr
.db $FF	; em_sce_rwin
.db $FF	; em_sce_app
.db $FF	; em_sce_rctrl
.db $FF	; em_sce_sleep
.db $FF	; em_sce_wake
.db $FF	; em_sce_power
.db $FF	; em_sce_printsc
.db $FF	; em_sce_insert
.db $75	; em_sce_delete
.db $FF	; em_sce_home
.db $FF	; em_sce_end
.db $FF	; em_sce_pageup
.db $FF	; em_sce_pagedown
.db $FF	; em_sce_up
.db $FF	; em_sce_left
.db $FF	; em_sce_down
.db $FF	; em_sce_right
.db '/'	; em_sce_ndiv
.db 10	; em_sce_nenter
shifted_layout_end:

; Now we need a list of keys that have Shift inverted by CapsLock:
.db 26	; 26 of them
.db em_sc_q
.db em_sc_w
.db em_sc_e
.db em_sc_r
.db em_sc_t
.db em_sc_y
.db em_sc_u
.db em_sc_i
.db em_sc_o
.db em_sc_p
.db em_sc_a
.db em_sc_s
.db em_sc_d
.db em_sc_f
.db em_sc_g
.db em_sc_h
.db em_sc_j
.db em_sc_k
.db em_sc_l
.db em_sc_z
.db em_sc_x
.db em_sc_c
.db em_sc_v
.db em_sc_b
.db em_sc_n
.db em_sc_m

; Finally, a table of alternative codes for when NumLock is pressed.
.db 11
.db em_sc_n0
.db em_sc_n1
.db em_sc_n2
.db em_sc_n3
.db em_sc_n4
.db em_sc_n5
.db em_sc_n6
.db em_sc_n7
.db em_sc_n8
.db em_sc_n9
.db em_sc_npoint
.db "0123456789."

.END