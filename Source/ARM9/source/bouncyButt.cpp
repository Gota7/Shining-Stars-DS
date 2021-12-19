#include "SM64DS_2.h"

//Make it so Wario can but bounce as metal.


//Do something with code.
void nsub_020d5368()
{
    asm
    (
        "cmp    r0, #0                    \n\t"
        "ldrne    r0, =IMMUNE_TO_DAMAGE    \n\t"
        "movne    r14, #1                    \n\t"
        "strneb    r14, [r0]                \n\t"
        "b        0x020d5370                \n\t"
    );
}