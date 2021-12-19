// Disable all of the level-specific minimap changes eg. the ship in 
// JRB or the fortress in WF.
void repl_020F99B4_ov_02() 
{
	asm
	(
		"mov r0, #0x00		\t\n"
	);
}