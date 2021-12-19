@ Yoshi gets special moves.
nsub_020DDEAC_ov_02:

	@ check if character is Yoshi
	ldr	r0, [r4, #0x8]
	cmp	r0, #0x3
	bne	0x020DDEDC

	@ approach used by the game to get "INPUT_PERSISTENT"
	ldr	r1, =0x020A0E40
	mov	r0, #0x18
	ldrb	r2, [r1]
	ldr	r1, =0x0209F49C
	mul	r0, r2, r0
	ldrh	r0, [r1, r0]

	@ choose attack depending on whether "Y" is pressed
	ands	r0, r0, #0x800
	beq	0x020DDEB8
	b	0x020DDEDC
	
@ Yoshi can sweep kick brick blocks.
ovr_020B3718_ov_02:
	cmp r1, #0x4
	
@ Yoshi can ground pound brick blocks.
ovr_020B3838_ov_02:
	cmp r2, #0x4