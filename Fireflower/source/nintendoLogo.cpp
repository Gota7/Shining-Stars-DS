#include "SM64DS_2.h"

// Nyehehe The Nintendo logo is blue now. That's my hack.
over(0x020914E0) const int newNintendoPalette[16] = {
  13741,
  900535428,
  900543917,
  900543917,
  900543917,
  900543917,
  900543917,
  874526125,
  874526125,
  740307008,
  606087200,
  471868448,
  335550464,
  136317952,
  2048,
  0
};

//Hooks every frame.
hook(0x0200da0c)
void hook_0200da0c()
{
	Crash();
}
