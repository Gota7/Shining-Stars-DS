@ Make conditional check for Mario never despawn feather.
ovr_020B2ED4_ov_02:
	nop

@ Pretend Mario is getting the wings.
ovr_020E03E4_ov_02:
	mov r0, #0x0
	
@ Same thing as above, but for ? boxes.
ovr_02149AAC_ov_66:
	mov r4, #0x0