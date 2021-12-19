@ Have player go to gameover level on death.
@ EQ flag is set if gameover.
repl_02029B44:
	pusheq {r0-r1}           @ Save registers used.
	ldreq r0, =0x0209211C    @ Address of the return level to go to.
	moveq r1, #0x1           @ Set level to be castle grounds (the gameover level).
	streq r1, [r0]           @ Save changes.
	ldreq r0, =0x0209F200    @ Address of the return entrance to go to.
	moveq r1, #0xD           @ Set entrance to 13.
	streq r1, [r0]           @ Save changes.
	popeq {r0-r1}            @ Restore registers used.
	bx lr                    @ Return.
	
	
@ Disable coded return things.
ovr_02029C38:
	nop
	nop
	nop
	nop
	nop
	nop
	nop
	nop
	nop
	
	
@ Have player go to gameover level on fall.
@ NE flag is set if gameover.
repl_02029BA0:
	pushne {r0-r1}           @ Save registers used.
	ldrne r0, =0x0209211C    @ Address of the return level to go to.
	movne r1, #0x1           @ Set level to be castle grounds (the gameover level).
	strne r1, [r0]           @ Save changes.
	ldrne r0, =0x0209F200    @ Address of the return entrance to go to.
	movne r1, #0xD           @ Set entrance to 13.
	strne r1, [r0]           @ Save changes.
	popne {r0-r1}            @ Restore registers used.
	bx lr                    @ Return.