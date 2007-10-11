; Emerson.inc
; Benjamin Ryves 2005 [benryves@benryves.com]
; AT keyboard equates

; There are a large number of different codes used by the keyboard routines.
;
; Firstly are the codes that are assigned to particular keys. There are three different
; representations:
;	RAW SCANCODES:
;		These are defined in the form at_sc[e]_*
;		These are the raw codes sent by the keyboard. They are sometimes
;		paired up as an "enhanced" keycode (these are at_sce_* codes).
;	"EMERSON" CODES:
;		These are defined in the form em_sc[e]_*
;		They are meant to correspond directly to the key of the same name in
;		the at_sc[e]_* format, but are only ever one byte long. This makes
;		handling considerably simpler and this is the standard internal format.
;	ASCII CODES:
;		These codes are by and large the same as the standard ASCII set (some
;		are changed to match the TIOS ASCII set, and the characters 128->255
;		are special nonprintable characters (eg Left Shift, Page Up).
;		These are translated by a routine that uses a table installed on the user's
;		calculator to easily localise the keyboard layout.
;
; There are some other codes used by the routines:
;
; at_stat_*: Status flags for LEDs and a few extra ones used by the internal routines.
; at_cmd_*: Command byte definitions for calculator->keyboard comms.
; at_resp_*: Keyboard response codes (keyboard->calculator).

; SCANCODES: (taken from a UK layout keyboard)

; Top row: 


at_sc_esc	.equ	$76
at_sc_f1	.equ	$05
at_sc_f2	.equ	$06
at_sc_f3	.equ	$04
at_sc_f4	.equ	$0C

at_sc_f5	.equ	$03
at_sc_f6	.equ	$0B
at_sc_f7	.equ	$83
at_sc_f8	.equ	$0A

at_sc_f9	.equ	$01
at_sc_f10	.equ	$09
at_sc_f11	.equ	$78
at_sc_f12	.equ	$07

at_sc_sysrq	.equ	$84	; Alt + Print Screen
at_sc_scroll	.equ	$7E	; Scroll lock

;  Second row:


at_sc_btick	.equ	$0E	; `¬

at_sc_1	.equ	$16
at_sc_2	.equ	$1E
at_sc_3	.equ	$26
at_sc_4	.equ	$25
at_sc_5	.equ	$2E
at_sc_6	.equ	$36
at_sc_7	.equ	$3D
at_sc_8	.equ	$3E
at_sc_9	.equ	$46
at_sc_0	.equ	$45

at_sc_minus	.equ	$4E	; -_
at_sc_equals	.equ	$55	; =+

at_sc_backsp	.equ	$66	; Backspace

; Third row:

at_sc_tab	.equ	$0D

at_sc_q	.equ	$15
at_sc_w	.equ	$1D
at_sc_e	.equ	$24
at_sc_r	.equ	$2D
at_sc_t	.equ	$2C
at_sc_y	.equ	$35
at_sc_u	.equ	$3C
at_sc_i	.equ	$43
at_sc_o	.equ	$44
at_sc_p	.equ	$4D

at_sc_lbrack	.equ	$54	; [{
at_sc_rbrack	.equ	$5B	; ]}

at_sc_enter	.equ	$5A

; Fourth row

at_sc_caps	.equ	$58	; Caps lock

at_sc_a	.equ	$1C
at_sc_s	.equ	$1B
at_sc_d	.equ	$23
at_sc_f	.equ	$2B
at_sc_g	.equ	$34
at_sc_h	.equ	$33
at_sc_j	.equ	$3B
at_sc_k	.equ	$42
at_sc_l	.equ	$4B

at_sc_colon	.equ	$4C	; ;:
at_sc_apos	.equ	$52	; '@
at_sc_hash	.equ	$5D	; #~

; Fifth row:

at_sc_lshift	.equ	$12	; Left shift
at_sc_bslash	.equ	$61	; Backslash

at_sc_z	.equ	$1A
at_sc_x	.equ	$22
at_sc_c	.equ	$21
at_sc_v	.equ	$2A
at_sc_b	.equ	$32
at_sc_n	.equ	$31
at_sc_m	.equ	$3A

at_sc_comma	.equ	$41	; ,<
at_sc_fullstop .equ	$49	; .>
at_sc_fslash	.equ	$4A	; Forwards slash
at_sc_rshift	.equ	$59	; Right shift

; Bottom row:

at_sc_lcrtl	.equ	$14	; Left control
at_sc_alt	.equ	$11	; Alt
at_sc_space	.equ	$29	; Space bar


; Number pad:

at_sc_n0	.equ	$70
at_sc_n1	.equ	$69
at_sc_n2	.equ	$72
at_sc_n3	.equ	$7A
at_sc_n4	.equ	$6B
at_sc_n5	.equ	$73
at_sc_n6	.equ	$74
at_sc_n7	.equ	$6C
at_sc_n8	.equ	$75
at_sc_n9	.equ	$7D

at_sc_nlock	.equ	$77	; Number lock

at_sc_nmul	.equ	$7C	; *
at_sc_nsub	.equ	$7B	; -
at_sc_nadd	.equ	$79	; +

at_sc_npoint	.equ	$71	; Decimal point

; Enhanced keys:
; These keys are preceded by the scancode $E0

at_sce_lwin	.equ	$1F	; Left Windows
at_sce_altgr	.equ	$11	; Right Alt (AltGr)
at_sce_rwin	.equ	$27	; Right Windows
at_sce_app	.equ	$2F	; Application
at_sce_rctrl	.equ	$14	; Right control

at_sce_sleep	.equ	$3F	; Sleep
at_sce_wake	.equ	$5E	; Wake
at_sce_power	.equ	$37	; Power

at_sce_printsc .equ	$7C	; Print Screen (also sends enhanced $12)

at_sce_insert	.equ	$70
at_sce_delete	.equ	$71
at_sce_home	.equ	$6C
at_sce_end	.equ	$69
at_sce_pageup	.equ	$7D
at_sce_pagedown .equ	$7A

at_sce_up	.equ	$75	; Cursor up
at_sce_left	.equ	$6B	; Cursor left
at_sce_down	.equ	$72	; Cursor down
at_sce_right	.equ	$74	; Cursor right

at_sce_ndiv	.equ	$4A	; Number pad /
at_sce_nenter	.equ	$5A	; Number pad Enter

; SPECIAL SCANCODES

at_scs_keyup	.equ	$F0
at_scs_enhance .equ	$E0

; KEYBOARD COMMANDS:

at_cmd_disable .equ	$F5	; Resets keyboard, returns ACK and stops scanning. LEDs not affected.
at_cmd_echo	.equ	$EE	; Responds with $EE
at_cmd_enable	.equ	$F4	; Clears output buffer, enables, returns ACK
at_cmd_id	.equ	$F2	; Responds ACK + ID bytes [$AB,$83]
at_cmd_resend	.equ	$FE	; Resends last scancode
at_cmd_reset	.equ	$FF	; Resets keyboard, performs POST.
at_cmd_scanset .equ	$F0	; Responds ACK, expects scan code set $01-$03. If $00 sent, returns current.

at_cmd_a_ty	.equ	$F7	; Sets all keys typematic. [Responds ACK]
at_cmd_a_mkbk	.equ	$F8	; Sets all keys make/break. [Responds ACK]
at_cmd_a_mk	.equ	$F9	; Sets all keys make. [Responds ACK]
at_cmd_a_tymkbk .equ	$FA	; Sets all keys typematic/make/break. [Responds ACK]
at_cmd_k_ty	.equ	$FB	; Sets key typematic. [Responds ACK, expects scancode, responds ACK]
at_cmd_k_mkbk	.equ	$FC	; Sets key make/break. [Responds ACK, expects scancode, responds ACK]
at_cmd_k_mk	.equ	$FD	; Sets key make. [Responds ACK, expects scancode, responds ACK]
at_cmd_default .equ	$F6	; Sets the default mode; resets keyboard, responds ACK.

at_cmd_status	.equ	$ED	; Set status LEDs [Responds ACK, expects status byte, responds ACK]
at_cmd_rate	.equ	$F3	; Set keyboard autorepeat rate
				; [Responds ACK, send %.DDRRRRR (DD=delay: [(DD+1)/4] seconds,
				;  RRRRR=rate (11111=30x/sec, 00000=2x/sec) - responds ACK].

; KEYBOARD RESPONSE CODES:

at_resp_ack	.equ	$FA	; Acknowledge, ACK (sent in success).
at_resp_post	.equ	$AA	; Poweron self-test passed (sent on startup).
at_resp_echo	.equ	$EE	; Response to echo command.
at_resp_error_a .equ	$00	; Key error/buffer overflow
at_resp_error_b .equ	$FF	; Key error/buffer overflow
at_resp_resend .equ	$FE	; Sent when keyboard requests a resend of last byte.

; STATUS LED BITMASKS:

at_stat_num	.equ	%00000010
at_stat_caps	.equ	%00000100
at_stat_scroll .equ	%00000001

; INTERNAL STATUS FLAG BITS

at_stat_shift	.equ	%00001000
at_stat_alt	.equ	%00010000
at_stat_ctrl	.equ	%00100000


;----

; EMERSON CODES
; These are single-byte, Emerson-specific key codes.
; They are ALL single bytes.

em_sc_esc	.equ	$01
em_sc_f1	.equ	$02
em_sc_f2	.equ	$03
em_sc_f3	.equ	$04
em_sc_f4	.equ	$05
em_sc_f5	.equ	$06
em_sc_f6	.equ	$07
em_sc_f7	.equ	$08
em_sc_f8	.equ	$09
em_sc_f9	.equ	$0A
em_sc_f10	.equ	$0B
em_sc_f11	.equ	$0C
em_sc_f12	.equ	$0D
em_sc_sysrq	.equ	$0E
em_sc_scroll	.equ	$0F
em_sc_btick	.equ	$10
em_sc_1	.equ	$11
em_sc_2	.equ	$12
em_sc_3	.equ	$13
em_sc_4	.equ	$14
em_sc_5	.equ	$15
em_sc_6	.equ	$16
em_sc_7	.equ	$17
em_sc_8	.equ	$18
em_sc_9	.equ	$19
em_sc_0	.equ	$1A
em_sc_minus	.equ	$1B
em_sc_equals	.equ	$1C
em_sc_backsp	.equ	$1D
em_sc_tab	.equ	$1E
em_sc_q	.equ	$1F
em_sc_w	.equ	$20
em_sc_e	.equ	$21
em_sc_r	.equ	$22
em_sc_t	.equ	$23
em_sc_y	.equ	$24
em_sc_u	.equ	$25
em_sc_i	.equ	$26
em_sc_o	.equ	$27
em_sc_p	.equ	$28
em_sc_lbrack	.equ	$29
em_sc_rbrack	.equ	$2A
em_sc_enter	.equ	$2B
em_sc_caps	.equ	$2C
em_sc_a	.equ	$2D
em_sc_s	.equ	$2E
em_sc_d	.equ	$2F
em_sc_f	.equ	$30
em_sc_g	.equ	$31
em_sc_h	.equ	$32
em_sc_j	.equ	$33
em_sc_k	.equ	$34
em_sc_l	.equ	$35
em_sc_colon	.equ	$36
em_sc_apos	.equ	$37
em_sc_hash	.equ	$38
em_sc_lshift	.equ	$39
em_sc_bslash	.equ	$3A
em_sc_z	.equ	$3B
em_sc_x	.equ	$3C
em_sc_c	.equ	$3D
em_sc_v	.equ	$3E
em_sc_b	.equ	$3F
em_sc_n	.equ	$40
em_sc_m	.equ	$41
em_sc_comma	.equ	$42
em_sc_fullstop .equ	$43
em_sc_fslash	.equ	$44
em_sc_rshift	.equ	$45
em_sc_lctrl	.equ	$46
em_sc_alt	.equ	$47
em_sc_space	.equ	$48
em_sc_n0	.equ	$49
em_sc_n1	.equ	$4A
em_sc_n2	.equ	$4B
em_sc_n3	.equ	$4C
em_sc_n4	.equ	$4D
em_sc_n5	.equ	$4E
em_sc_n6	.equ	$4F
em_sc_n7	.equ	$50
em_sc_n8	.equ	$51
em_sc_n9	.equ	$52
em_sc_nlock	.equ	$53
em_sc_nmul	.equ	$54
em_sc_nsub	.equ	$55
em_sc_nadd	.equ	$56
em_sc_npoint	.equ	$57
em_sce_lwin	.equ	$58
em_sce_altgr	.equ	$59
em_sce_rwin	.equ	$5A
em_sce_app	.equ	$5B
em_sce_rctrl	.equ	$5C
em_sce_sleep	.equ	$5D
em_sce_wake	.equ	$5E
em_sce_power	.equ	$5F
em_sce_printsc .equ	$60
em_sce_insert	.equ	$61
em_sce_delete	.equ	$62
em_sce_home	.equ	$63
em_sce_end	.equ	$64
em_sce_pageup	.equ	$65
em_sce_pagedown .equ	$66
em_sce_up	.equ	$67
em_sce_left	.equ	$68
em_sce_down	.equ	$69
em_sce_right	.equ	$6A
em_sce_ndiv	.equ	$6B
em_sce_nenter	.equ	$6C