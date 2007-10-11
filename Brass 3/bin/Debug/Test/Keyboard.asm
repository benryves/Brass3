#include "AT_Prtcl.asm"
#include "Emerson.inc"

.module Keyboard

; CONFIGURATION:

_buffer = TI.savesScreen	; Set this to a safe RAM location
_buffer_size = 16	; How many bytes do you want to buffer?

keyb_update:

	; Will we need to update the status LEDs?

	ld hl,_ignore_status

	ld a,(keyb_status)
	and %00000111
	ld b,a
	ld a,(_keyb_old_status)
	and %00000111
	cp b
	jr z,_no_status_change

	ld hl,_set_status

_no_status_change:
	ld (_status_changer+1),hl

	; Clear out the buffer:

	ld hl,_buffer		; Point HL to the buffer
	xor a			; A = 0
	ld (hl),a		; Set first byte of buffer to A (0)
	ld de,_buffer+1	; DE = 1 byte after the buffer

	ld bc,_buffer_size-1
	ldir			; Fill buffers with 0

	ld hl,_buffer

	ld b,_buffer_size


_get_key_loop:

	push bc		; Save BC
	call at_get_byte	; Get a byte in
	pop bc			; Recall BC

_status_changer:
	jp nz,_set_status	; No byte in [Self modifying address]

	ld (hl),a		; Store byte in buffer
	inc hl			; Advance to next buffer slot
	djnz _get_key_loop

_set_status:
	ld a,at_cmd_status
	call at_send_byte
	call at_get_byte
	cp $FE
	jr z,_set_status

	ld a,(keyb_status)
	and %00000111
	call at_send_byte
	call at_get_byte
	cp $FE
	jr z,_set_status

_ignore_status:

	; Inhibit all further communication
	ld a,1
	out (TI.bport),a

	; Store current status as old status
	ld a,(keyb_status)
	ld (_keyb_old_status),a

	; Now we need to run through all the keyboard events!

	ld ix,_buffer	; Start at the beginning of the buffer.

_handle_next_scancode:

	; Let's assume it's a normal key:

	ld hl,_scancode_lut
	ld bc,_scancode_lut_end-_scancode_lut
	ld de,_key_down

	ld a,(ix+0)

	or a
	jr z,_handled_all_keys

	cp at_scs_enhance		; Is it an enhanced key?
	jr nz,_not_enhanced

		inc ix	; We know it's enhanced, so move along.
		ld a,(ix+0)
		or a
		jr z,_handled_all_keys
		ld hl,_scancode_e_lut
		ld bc,_scancode_e_lut_end-_scancode_e_lut
_not_enhanced:

	; Now HL points to our LUT, BC is the correct length
	; and IX points to the next byte.

	cp at_scs_keyup ; Is it a key UP event?
	jr nz,_not_keyup

		inc ix
		ld a,(ix+0)
		or a
		jr z,_handled_all_keys
		ld de,_key_up

_not_keyup:

	; Move to next chunk before we do anything
	inc ix
	; At this point, A=scancode, HL->translation table, DE->handler, BC=table size, IX->next scancode.

	; We now need to run the translation.

	cpir	; Simple as that!

	jr nz,_handle_next_scancode	; Not found

	; So HL->scancode+1
	; Here is where the magic happens:


	push de
	ld de,0-_scancode_lut
	add hl,de
	ld a,l
	pop de

	; Now, A = an Emerson scancode.

	; We need to 'call' DE.
	; We can spoof this easily:

	ld hl,_handle_next_scancode
	push hl	; _h_n_s is on TOP of the stack.
	push de ; our event handler is on top of the stack;
	ret	; POP back off stack and jump to it.
		; When the handler RETs it'll pop off _h_n_s and carry on scanning!


_handled_all_keys:

_null_handler:
	ret


_key_up:
	; Internal key up handler


	push af

	cp em_sc_rshift
	jr z,_is_shift_u
	cp em_sc_lshift
	jr nz,_not_shift_u
_is_shift_u:
		ld a,(keyb_status)
		and $FF-at_stat_shift
		ld (keyb_status),a
		jr _done_key_up
_not_shift_u:

	cp em_sce_rctrl
	jr z,_is_ctrl_u
	cp em_sc_lctrl
	jr nz,_not_ctrl_u
_is_ctrl_u:
		ld a,(keyb_status)
		and $FF-at_stat_ctrl
		ld (keyb_status),a
		jr _done_key_up
_not_ctrl_u:

	cp em_sce_altgr
	jr z,_is_alt_u
	cp em_sc_alt
	jr nz,_not_alt_u
_is_alt_u:
		ld a,(keyb_status)
		and $FF-at_stat_alt
		ld (keyb_status),a
		jr _done_key_up
_not_alt_u:

	; ?



_done_key_up:
	pop af
_key_up_handler:
	jp _null_handler

_key_down:
	; Internal key down handler

	push af
	; Which key was it?
	cp em_sc_nlock
	jr nz,_not_nlock
		; It is NUM LOCK
		ld a,(keyb_status)
		xor at_stat_num
		ld (keyb_status),a
		jr _done_key_down
_not_nlock:
	cp em_sc_caps
	jr nz,_not_caps
		; It is CAPS LOCK
		ld a,(keyb_status)
		xor at_stat_caps
		ld (keyb_status),a
		jr _done_key_down
_not_caps:
	cp em_sc_scroll
	jr nz,_not_scroll
		; It is SCROLL LOCK
		ld a,(keyb_status)
		xor at_stat_scroll
		ld (keyb_status),a
		jr _done_key_down

_not_scroll:


	cp em_sc_rshift
	jr z,_is_shift_d
	cp em_sc_lshift
	jr nz,_not_shift_d
_is_shift_d:
		ld a,(keyb_status)
		or at_stat_shift
		ld (keyb_status),a
		jr _done_key_down
_not_shift_d:



	cp em_sce_rctrl
	jr z,_is_ctrl_d
	cp em_sc_lctrl
	jr nz,_not_ctrl_d
_is_ctrl_d:
		ld a,(keyb_status)
		or at_stat_ctrl
		ld (keyb_status),a
		jr _done_key_down
_not_ctrl_d:


	cp em_sce_altgr
	jr z,_is_alt_d
	cp em_sc_alt
	jr nz,_not_alt_d
_is_alt_d:
		ld a,(keyb_status)
		or at_stat_alt
		ld (keyb_status),a
		jr _done_key_down
_not_alt_d:


_done_key_down:

	pop af
_key_down_handler:
	jp _null_handler


; HELPER ROUTINES TO SET KEY HANDLERS

keyb_reset_keydown:
	ld hl,_null_handler
keyb_set_keydown:
	ld (_key_down_handler+1),hl
	ret

keyb_reset_keyup:
	ld hl,_null_handler
keyb_set_keyup:
	ld (_key_up_handler+1),hl
	ret


keyb_status:
	.db 0	; Status byte
_keyb_old_status:
	.db 0	; Last status byte

; AT scancode to internal representation:
; Note that they are in the exact order that the em_* versions are numbered.
; This is so that we can perform the conversion.
_scancode_lut:
.db at_sc_esc
.db at_sc_f1
.db at_sc_f2
.db at_sc_f3
.db at_sc_f4
.db at_sc_f5
.db at_sc_f6
.db at_sc_f7
.db at_sc_f8
.db at_sc_f9
.db at_sc_f10
.db at_sc_f11
.db at_sc_f12
.db at_sc_sysrq
.db at_sc_scroll
.db at_sc_btick
.db at_sc_1
.db at_sc_2
.db at_sc_3
.db at_sc_4
.db at_sc_5
.db at_sc_6
.db at_sc_7
.db at_sc_8
.db at_sc_9
.db at_sc_0
.db at_sc_minus
.db at_sc_equals
.db at_sc_backsp
.db at_sc_tab
.db at_sc_q
.db at_sc_w
.db at_sc_e
.db at_sc_r
.db at_sc_t
.db at_sc_y
.db at_sc_u
.db at_sc_i
.db at_sc_o
.db at_sc_p
.db at_sc_lbrack
.db at_sc_rbrack
.db at_sc_enter
.db at_sc_caps
.db at_sc_a
.db at_sc_s
.db at_sc_d
.db at_sc_f
.db at_sc_g
.db at_sc_h
.db at_sc_j
.db at_sc_k
.db at_sc_l
.db at_sc_colon
.db at_sc_apos
.db at_sc_hash
.db at_sc_lshift
.db at_sc_bslash
.db at_sc_z
.db at_sc_x
.db at_sc_c
.db at_sc_v
.db at_sc_b
.db at_sc_n
.db at_sc_m
.db at_sc_comma
.db at_sc_fullstop
.db at_sc_fslash
.db at_sc_rshift
.db at_sc_lcrtl
.db at_sc_alt
.db at_sc_space
.db at_sc_n0
.db at_sc_n1
.db at_sc_n2
.db at_sc_n3
.db at_sc_n4
.db at_sc_n5
.db at_sc_n6
.db at_sc_n7
.db at_sc_n8
.db at_sc_n9
.db at_sc_nlock
.db at_sc_nmul
.db at_sc_nsub
.db at_sc_nadd
.db at_sc_npoint
_scancode_lut_end:

; Enhanced keys; all these start with a $E0 code

_scancode_e_lut:
.db at_sce_lwin
.db at_sce_altgr
.db at_sce_rwin
.db at_sce_app
.db at_sce_rctrl
.db at_sce_sleep
.db at_sce_wake
.db at_sce_power
.db at_sce_printsc
.db at_sce_insert
.db at_sce_delete
.db at_sce_home
.db at_sce_end
.db at_sce_pageup
.db at_sce_pagedown
.db at_sce_up
.db at_sce_left
.db at_sce_down
.db at_sce_right
.db at_sce_ndiv
.db at_sce_nenter
_scancode_e_lut_end: