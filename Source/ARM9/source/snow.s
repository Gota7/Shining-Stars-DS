@ Change the 5 slots for level snow. The last byte in each word is the level.
/*overwrite
ovr_0200E0E4:
	.word 0xe353000A  @ Icefire Caverns.
	.word 0x1353000B  @ Icefire Caverns 2nd Area.
	.word 0x13530013  @ Wintry Wasteland.
	.word 0x1353002E  @ Dust Drain.
	.word 0x13530001  @ Gameover.
	
	.word 0x059F521C  @ Loads 0x112 (the snow particle system definition ID) into r5.
	.word 0x0A000010  @ Conditionally branches to the part of the function where the particle system gets spawned.
overwrite*/