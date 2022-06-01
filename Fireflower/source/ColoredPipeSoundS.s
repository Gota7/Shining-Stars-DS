.hook(020B0CC4, 2)
nsub_020b0cc4_ov_02:
    bne  0x020b0cc8
    mov  r0, r6
    bl   FindCustomPipe
    movs r7, r0
    beq  0x020b0cd4
    b    0x020b0cc8