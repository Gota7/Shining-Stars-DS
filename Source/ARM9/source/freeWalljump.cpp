#include "SM64DS_2.h"

//See if walljumpiing is allowed or not.
extern "C" bool wallSlideOrNot()
{
    static short angle;

    if ((unsigned)(PLAYER_ARR[0]->currState) != Player::ST_WALL_JUMP || PLAYER_ARR[0]->ang.y != angle)
    {
        angle = PLAYER_ARR[0]->ang.y;
        return true;
    }

    PLAYER_ARR[0]->ang.y += 0x8000;
    return false;
}

//Go to the appropriate function whether wall jumping is enabled or not.
void nsub_020c1dbc()
{
    asm
    (
        "push  {r1-r12, r14}        \n\t"
        "bl    wallSlideOrNot      \n\t"
        "pop    {r1-r12, r14}        \n\t"
        "cmp    r0, #0                  \n\t"
        "beq    0x020c1e28        \n\t"
        "bne    0x020c1dc8          \n\t"
    );
}

//Prevent yoshi and luigi fluttering.
void hook_020C1D64() {

	PLAYER_ARR[0]->canFlutterJump = 0;
}