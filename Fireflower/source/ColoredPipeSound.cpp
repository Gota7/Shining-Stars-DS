#include "SM64DS_2.h"
#include "MOM_Objects.h"

extern "C" Actor* FindCustomPipe(const Actor& exit)
{
    Actor* customPipe = nullptr;
    while ((customPipe = Actor::FindWithActorID(CustomObject::COLORED_PIPE, customPipe)) && customPipe->pos.Dist(exit.pos) >= 300._f);
    return customPipe;
}








	/*Actor* customPipe = FindWithActorID(537, 0);

	while (customPipe != nullptr)
	{
		if (Vec3_Dist(customPipe->pos, exit.pos) < 300._f) return customPipe;

		customPipe = FindWithActorID(537, customPipe);
	}
	
	return customPipe;*/