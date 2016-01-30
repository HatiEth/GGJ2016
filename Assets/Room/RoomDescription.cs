using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public struct Spawnable
{
    public List<GameObject> Asset;
    public List<string> SpawnRules;
    public int AverageAmount;

    public static SpawnConstraint Factory(string Rule)
    {
        if (Rule == "Bottom") return new BottomConstraint();
        if (Rule == "Side") return new SideConstraint();
        if (Rule == "Fix") return new FixConstraint();
        if (Rule == "MidHeigth") return new MidHeigthConstraint();
        if (Rule == "RandomOrientationZ") return new RandomOrientationConstraint("Z");
        if (Rule == "Mid") return new MidConstraint();
        if (Rule == "OrientationToCenter") return new OrientationToCenterConstraint();
        if (Rule == "Circular") return new CircularConstraint();
        Debug.Log("Constraint unkown: " + Rule);
        return null;
    }
}

[System.Serializable]
public struct RoomDescription {
    public List<Spawnable> Spawnables;

}


public struct SpawnConstraintOptions
{
    public bool IsFixed;
}

public interface SpawnConstraint
{
    bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options);
}


