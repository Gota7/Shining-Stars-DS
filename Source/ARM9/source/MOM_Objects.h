#ifndef MOM_OBJECTS_INCLUDED
#define MOM_OBJECTS_INCLUDED

#include "SM64DS_2.h"

constexpr int BASE_OBJECT_ID = 512;

//enum containing the decimal object id's of all custom objects
enum CustomObject
{
	GALAXY_SHRINKING_PLATFORM_ID = 					BASE_OBJECT_ID,
	SILVER_COIN_ID = 								BASE_OBJECT_ID + 1,
	INVISIBLE_WALL_ID = 							BASE_OBJECT_ID + 2,
	BERRY_ID = 										BASE_OBJECT_ID + 3,
	YOSHI_RIDE_ID = 								BASE_OBJECT_ID + 4,
	OBJECT_LIGHTING_MODIFIER_ID = 					BASE_OBJECT_ID + 5,
	TWO_DEE_LEVEL_ID = 								BASE_OBJECT_ID + 6,
	TWO_DEE_CAMERA_LIMITER = 						BASE_OBJECT_ID + 7,
	COLORED_GOOMBA_SMALL = 							BASE_OBJECT_ID + 8,
	COLORED_GOOMBA =	 							BASE_OBJECT_ID + 9,
	COLORED_GOOMBA_LARGE = 							BASE_OBJECT_ID + 10,
	TOXIC_LEVEL = 									BASE_OBJECT_ID + 11,
	NOTEBLOCK = 									BASE_OBJECT_ID + 12,
	SHY_GUY = 										BASE_OBJECT_ID + 13,
	LAUNCH_STAR = 									BASE_OBJECT_ID + 14,
	KAMEK_SHOT = 									BASE_OBJECT_ID + 15,
	KAMEK = 										BASE_OBJECT_ID + 16,
	KAMELLA = 										BASE_OBJECT_ID + 17,
	SKYBOX_ROTATOR = 								BASE_OBJECT_ID + 18,
	FALLING_ICICLE = 								BASE_OBJECT_ID + 19,
	GRAVITY_MODIFIER = 								BASE_OBJECT_ID + 20,
	COLORED_GOOMBA_2_SMALL = 						BASE_OBJECT_ID + 21,
	COLORED_GOOMBA_2 =	 							BASE_OBJECT_ID + 22,
	COLORED_GOOMBA_2_LARGE = 						BASE_OBJECT_ID + 23,
	YOSHI_NPC = 									BASE_OBJECT_ID + 24,
	COLORED_PIPE = 									BASE_OBJECT_ID + 25
};

#endif