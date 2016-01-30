using UnityEngine;
using System.Collections;

public class MidHeigthConstraint : SpawnConstraint
{
    public bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options)
    {
        position = new Vector3(position.x, 5, position.z);
        Vector3 halfext = asset.GetComponent<Collider>().bounds.extents / 2;
        if (Physics.CheckBox(position, halfext, rotation)) return false;
        return true;
    }
}

