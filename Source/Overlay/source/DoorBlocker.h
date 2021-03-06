#ifndef DOOR_BLOCKER_INCLUDED
#define DOOR_BLOCKER_INCLUDED

#include "include/SM64DS_2.h"

struct DoorBlocker : public Platform
{	
	//ShadowVolume shadow;
	//Matrix4x3 shadowMat;
	Vector3 center;
	uint8_t eventID;
	bool gone;
	bool camSet;
	
	void UpdateModelTransform();

	static DoorBlocker* Spawn();
	virtual int InitResources() override;
	virtual int CleanupResources() override;
	virtual int Behavior() override;
	virtual int Render() override;
	virtual ~DoorBlocker();

	static SharedFilePtr modelFile;
	static SharedFilePtr clsnFile;

	static SpawnInfo<DoorBlocker> spawnData;
};

#endif