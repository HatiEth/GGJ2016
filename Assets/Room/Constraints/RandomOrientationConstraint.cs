using UnityEngine;
using System.Collections;

public class RandomOrientationConstraint : SpawnConstraint
{
    public RandomOrientationConstraint(string Orientation)
    {

    }

    public bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options)
    {
        Options.IsFixed = true;
        return true;
    }
}

