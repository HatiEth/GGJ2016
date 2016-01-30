using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Spawnable
{
    public GameObject Asset;
    public List<string> SpawnRules;
}

[System.Serializable]
public struct RoomDescription {
    public List<Spawnable> Spawnables;

}
