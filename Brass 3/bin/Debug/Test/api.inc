; [about]
; <font face="Arial"><center><B>Application Programming Interface-like thingy :)</b>
; <i>Version 1.0 -- By Timendus</i></center>
;
; Please see <a href="#docDetails">the defines</a> or demo.asm for a short description on how 
; to use this API in your program.
;
; This is a demonstration version, please don't bitch me too
; hard if I left in some ugly bugs or unoptimized source :)
;
; <b>Variable types</b>
;
; &rarr; = 16-bits pointer to data
; # = 8-bits value (normal byte)
; % = 16-bits value (double byte)
;
; <b>Ideas yet to implement</b>
;
; <font size=1>- Add word wrapping (it wraps already, also in the graphscreen, but not at words)
; - More String routines (append,copy,etc)
; - Better linking (get it working :)/faster/running in background/more than 2 calcs) 
; - file.read(filename) / file.write(filename,data) for programs
; - var.read(varletter) / var.write(varletter,value) for A-Z vars
; - str.read(strnumber) / str.write(strnumber,data) for str0-str9
; - menu.choice(str1,str2) to give a choice between two things (return in z and a)
; - menu.alert(str) to give an alert that goes away after a keypress
; - menu.question(str) to ask "str? Yes/No" (return in z and a)
; - menu.options(menu) to give a proper menu (.db 1,"Option one" \ .db 2,"Option two" \ ... \ .db $FF) (return in a)</font></font>
; [/about]



; *******************************************************************
; <b><font size=4>Defines</font></b>
;



; ***
; [var]api_defines[/var]
; 
; Use
;  #define api_defines
;  #include api.inc
; To include the macro's at the beginning of your source
;

#IFDEF api_defines
#IFNDEF api_routines

	#DEFINE		home.println(str)			  ld	hl,str
	#DEFCONT						\ call	api_println
	#DEFCONT						\ #DEFINE api_use_println

	#DEFINE		home.print(str)				  ld	hl,str
	#DEFCONT						\ bcall(_puts)

	#DEFINE		home.clear()				  call	api_clear
	#DEFCONT						\ #DEFINE api_use_clear

	#DEFINE		home.newline()				  bcall(_newline)

	#DEFINE		graph.println(str)			  ld	hl,str
	#DEFCONT						\ call	api_vputsln
	#DEFCONT						\ #DEFINE api_use_vputsln
	
	#DEFINE		graph.print(str)			  ld	hl,str
	#DEFCONT						\ call	api_vputs
	#DEFCONT						\ #DEFINE api_use_vputs
	
	#DEFINE		graph.clear()				  call	api_vclear
	#DEFCONT						\ #DEFINE api_use_vclear

	#DEFINE		graph.newline()				  call	api_vnewline
	#DEFCONT						\ #DEFINE api_use_vnewline

	#DEFINE		kbd.readln.home(buffer,maxsize)		  ld	hl,buffer
	#DEFCONT						\ ld	b,maxsize
	#DEFCONT						\ call	api_readln
	#DEFCONT						\ #DEFINE api_use_readln

	#DEFINE		kbd.readln.graph(buffer,maxsize)	  ld	hl,buffer
	#DEFCONT						\ ld	b,maxsize
	#DEFCONT						\ call	api_vreadln
	#DEFCONT						\ #DEFINE api_use_vreadln

	#DEFINE		kbd.waitkey()				  bcall(_getkey)

	#DEFINE		string.compare(str1,str2)		  ld	hl,str1
	#DEFCONT						\ ld	de,str2
	#DEFCONT						\ call	api_stringcompare
	#DEFCONT						\ #DEFINE api_use_stringcompare

#ENDIF
#ENDIF







; ***
; [var]api_routines[/var]
; 
; Use
;  #define api_routines
;  #include api.inc
; To include the routines at the end of your program code
; (but before your data on Ti-83+ because of the boundary)
;

#IFDEF api_routines



; *******************************************************************
; <b><font size=4>Routines for the Homescreen</font></b>
;

; ***
; [routine]home.println(&rarr;str)[/routine]
;
; Print a zero terminated string to the homescreen,
; followed by a newline
;
; Example:
;  home.println(helloworld)
;  ...
;  helloworld: .db "Hello world",0
;

	#IFDEF api_use_println

api_println:
	bcall(_puts)
	bcall(_newline)
	ret

	#ENDIF

; ***
; [routine]home.print(&rarr;str)[/routine]
;
; Print a zero terminated string to the homescreen,
; without a newline
;
; Example:
;  home.print(helloworld)
;  ...
;  helloworld: .db "Hello world",0
;

; ***
; [routine]home.clear()[/routine]
;
; Clears the homescreen and returns the cursor to the
; upperleftmost corner
;

	#IFDEF api_use_clear

api_clear:
	bcall(_clrLCDFull)
	bcall(_homeup)
	ret

	#ENDIF

; ***
; [routine]home.newline()[/routine]
;
; Terminates the current line with a newline
;






; *******************************************************************
; <b><font size=4>Routines for the Graphscreen</font></b>
;

; ***
; [routine]graph.println(&rarr;str)[/routine]
;
; Print a zero terminated string to the graphscreen,
; terminating with a newline
;
; Example:
;  graph.println(helloworld)
;  ...
;  helloworld: .db "Hello world",0
;

	#IFDEF api_use_vputsln

	#DEFINE api_use_vnewline
	#DEFINE api_use_vputs
api_vputsln:
	call	api_vputs
	call	api_vnewline
	ret

	#ENDIF

; ***
; [routine]graph.print(&rarr;str)[/routine]
;
; Print a zero terminated string to the graphscreen,
; without a newline
;
; Example:
;  graph.print(helloworld)
;  ...
;  helloworld: .db "Hello world",0
;
	
	#IFDEF api_use_vputs

	#DEFINE api_use_vnewline
api_vputs:
	set	textwrite,(iy+SGRFLAGS)
api_vputs_loop:
	ld	a,(hl)
	inc	hl
	or	a
	jp	z,ionFastCopy
	bcall(_vputmap)
	jr	nc,api_vputs_loop
	push	hl
	call	api_vnewline
	pop	hl
	dec	hl
	jr	api_vputs_loop

	#ENDIF
	
; ***
; [routine]graph.clear()[/routine]
;
; Clears the graphscreen and returns the cursor to the
; upperleftmost corner
;

	#IFDEF api_use_vclear

api_vclear:
	res textInverse,(iy+textflags)
	bcall(_clrLCDFull)
	ld	hl,plotsscreen
	ld	de,plotsscreen+1
	ld	bc,767
	xor	a
	ld	(hl),a
	ldir
	ld	hl,0
	ld	(PENCOL),hl
	ret

	#ENDIF

; ***
; [routine]graph.newline()[/routine]
;
; Terminates the current line with a newline
;

	#IFDEF api_use_vnewline

api_vnewline:
	ld	hl,(PENCOL)
	ld	l,0
	ld	a,h
	add	a,6
	cp	60
	jr	nc,api_vnewline_scrollup
	ld	h,a
	ld	(PENCOL),hl
	ret
api_vnewline_scrollup:
	ld	(PENCOL),hl
	ld	de,plotsscreen
	ld	hl,plotsscreen+(6*12)
	ld	bc,768-(6*12)
	ldir
	ld	hl,plotsscreen+768-(6*12)
	ld	de,plotsscreen+768-(6*12)+1
	ld	bc,(6*12)-1
	xor	a
	ld	(hl),a
	ldir
	ret

	#ENDIF





; *******************************************************************
; <b><font size=4>Routines for getting user input</font></b>
;

; ***
; [routine]kbd.readln.home(&rarr;buffer,#maxlength)[/routine]
;
; Get a zero terminated string from the user.
; String will be returned in <i>buffer</i>,
; input will have length <i>maxlength</i> or less.
;
; Example:
;  kbd.readln.home(saferam1,768)
;  home.println(youtyped)
;  home.println(saferam1)
;  ...
;  youtyped: .db "You typed:",0
;
	
	#IFDEF api_use_readln

api_readln:
	push	bc
	push	hl
	bcall(_getkey)
	pop	hl
	pop	bc
	cp	5
	jr	z,api_readln_done
	cp	$99
	call	z,api_readln_space
	sub	89		 ; Hoofdletters (gedrukt met alpha)
	ld	(hl),a
	bcall(_putc)
	inc	hl
	djnz	api_readln
	dec	hl
api_readln_done:
	ld	(hl),0
	bcall(_newline)
	ret
api_readln_space:
	ld	a,' '+89
	ret

	#ENDIF


; ***
; [routine]kbd.readln.graph(&rarr;buffer,#maxlength)[/routine]
;
; Get a zero terminated string from the user.
; String will be returned in <i>buffer</i>,
; input will have length <i>maxlength</i> or less.
;
; Example:
;  kbd.readln.graph(saferam1,768)
;  graph.println(youtyped)
;  graph.println(saferam1)
;  ...
;  youtyped: .db "You typed:",0
;

	#IFDEF api_use_vreadln

	#DEFINE api_use_vnewline
api_vreadln:
	set	textwrite,(iy+SGRFLAGS)
	push	bc
	push	hl
	call	ionFastCopy
	bcall(_getkey)
	pop	hl
	pop	bc
	cp	5
	jr	z,api_vreadln_done
	cp	$99
	call	z,api_vreadln_space
	sub	89		 ; Hoofdletters (gedrukt met alpha)
	ld	(hl),a
	ld	c,a
	push	bc
	bcall(_vputmap)
	pop	bc
	call	c,api_vreadln_newline
	inc	hl
	djnz	api_vreadln
api_vreadln_done:
	ld	(hl),0
	jr	api_vnewline
api_vreadln_space:
	ld	a,' '+89
	ret
api_vreadln_newline:
	push	hl
	call	api_vnewline
	pop	hl
	ld	a,c
	bcall(_vputmap)
	ret

	#ENDIF

; ***
; [routine]kbd.waitkey()[/routine]
;
; Waits for a keypress and returns
;





; *******************************************************************
; <b><font size=4>Routines for string manipulation</font></b>
;

; ***
; [routine]string.compare(&rarr;str1,&rarr;str2)[/routine]
;
; Compares two zero terminated strings for equality, returns z flag:
;  z:  <i>str1</i> == <i>str2</i>
;  nz: <i>str1</i> != <i>str2</i>
;

	#IFDEF api_use_stringcompare

	; Return z = equal, nz = not equal
api_stringcompare:
	ld	a,(hl)
	ld	b,a
	or	a				; is byte in (hl) 0?
	jr	z,api_stringcompare_endstring

	ld	a,(de)
	cp	b					; if (de) != (hl) && not end of string
	ret	nz					; return not equal
	inc	hl					; else proceed to next byte
	inc	de
	jr	api_stringcompare
api_stringcompare_endstring:
	ld	a,(de)
	or	a				; then see if (de) = 0 too
	ret					; if so; z, else; nz

	#ENDIF

#ENDIF

