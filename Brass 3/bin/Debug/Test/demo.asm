;;;; <Latenite generated code>

;#include "target.tgt"
#define TI83PI
#include "headers.inc"

;;;; </Latenite generated code>
;;;; <API code>

#define api_defines
#include "api.inc"

;;;; </API code>
;;;; <Latenite generated code>

; ==========================================
; Description (ignored if not Ion/MirageOS)
; ==========================================
#ifndef TI83P
.db "API Demonstration",0
#endif

; ==========================================
; Program entry point
; ==========================================
init_all:

;;;; </Latenite generated code>
;;;; <API code>

	graph.clear()
	graph.println(input)
	kbd.readln.graph(name,15)
	
	graph.clear()
	graph.print(welcome)
	graph.println(excl)
	graph.newline()

	graph.println(demo)
	graph.newline()
	graph.println(presskey)
	kbd.waitkey()

	graph.newline()
	graph.println(scrolldemo)
	graph.newline()
	graph.println(presskey)
	kbd.waitkey()

	home.clear()
	home.println(homedemo)

again_loop:
	kbd.readln.home(savesscreen,100)
	string.compare(savesscreen,cowstr)
	jp	nz,again
	home.println(okay)
	kbd.waitkey()
	
	graph.clear()
	graph.println(goodthing)
	graph.newline()
	graph.println(exit)
	kbd.waitkey()
	ret

again:
	home.println(notokay)
	jp again_loop

input:
	.db "Please turn on Alpha lock and type your name (max 15 chars)",0
welcome:
	.db "Welcome "
name:
	.fill 16,0
excl:
	.db "!",0
demo:
	.db "This is a demonstration of the Ti API. ",
	.db "You have just seen text in- and output in action in the graphscreen, ",
	.db "all taken care of by a few simple to use macro",$27,"s.",0
presskey:
	.db "Please press a key...",0
scrolldemo:
	.db "As you can see, when the outputted text goes of the screen the api scrolls",
	.db " the screen back up again like you would expect in the homescreen.",0
homedemo:
	.db "The API can alsobe used in the  homescreen.     Please type the word "
cowstr:
	.db "COW",0
notokay:
	.db "That",$27,"s not what I asked you to  type! Please tryagain :D",0
okay:
	.db "Very good, you  can type the    word cow :). As you can see, theAPI can compare two strings for you.",0
goodthing:
	.db "And the best thing is: this API should start growing and growing from today on, making your life easier with every addition!",0
exit:
	.db "Thank you for your attention. You can press any key to quit the demo.",0

end_of_data:

#define api_routines
#include "api.inc"

end_of_routines:

;;;; </API code>

;;;; <TASM instructions>

.echo "Instructions: "
.echo (input - init_all)
.echo " bytes\nData: "
.echo (end_of_data - input)
.echo " bytes\nRoutines: "
.echo (end_of_routines - end_of_data)
.echo " bytes\n"

.END
END

;;;; </TASM instructions>