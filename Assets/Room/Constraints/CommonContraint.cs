using UnityEngine;
using System.Collections;

public class CommonConstraint : SpawnConstraint
{
    public bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options)
    {
        if (Random.value > 0.6f)
        {
            return false;
        }
        return true;
    }
}

