using UnityEngine;
using System.Collections;

public class MidConstraint : SpawnConstraint
{
    public bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options)
    {
        position = Options.SpawnArea.center;
        return true;
    }
}

