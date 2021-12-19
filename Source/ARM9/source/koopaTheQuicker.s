@Make KTQ faster.

@For normal speed.
/*overwrite
ovr_0211A408_ov_3E:
	mov r5, #0x6
overwrite*/

@For tiny-huge-island speed.
/*overwrite
ovr_0211A454_ov_3E:
	moveq r5, #0x6
overwrite*/

@For catch-up speed.
/*overwrite
ovr_0211A440_ov_3E:
	movgt r5, #0x6
overwrite*/