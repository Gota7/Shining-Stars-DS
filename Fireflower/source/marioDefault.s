@ Make Mario the starting character.
.rlnk(0x02013DDC)
repl_02013ddc:
    mov r0, #0
    bx  lr
	
@ Skip opening cutscene.
.rlnk(0x02013DF4)
repl_02013df4:
    orr r1, r1, #0x88
    bx  lr

/*

@ Make Mario's Door Yoshi's.
.rlnk(0x02144588, 100)
repl_02144588_ov_64:

    @ If player enters their own door, go to same player as door logic.
    moveq r1, #0x0
    beq samePlayerAsDoor
    
    @ If player does not enter their own door, but enter's Yoshi's door, return Yoshi.
    cmp r2, #0x0
    beq returnYoshi
    bne skip
    
    @ Player entered their own door. If it is Yoshi's door (Was Mario's), return Yoshi.
    samePlayerAsDoor:
    cmp r2, #0x0
    bne skip
    mov r1, #0x3
    b skip
    
    @ Return Yoshi, but if Yoshi enters his own door, return Mario.
    returnYoshi:
    cmp r0, #0x3
    moveq r1, #0x0
    movne r1, #0x3
    
    @ Continue on with whatever gay code the game does.
    skip:
    bx lr

*/