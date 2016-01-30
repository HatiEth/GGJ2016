using UnityEngine;
using System.Collections;

public class UncommenConstraint : SpawnConstraint
{
    public bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options)
    {
        if (Random.value > 0.2f)
        {
            return false;
        }
        return true;
    }
}

