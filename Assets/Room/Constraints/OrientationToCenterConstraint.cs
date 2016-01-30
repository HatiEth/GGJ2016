using UnityEngine;
using System.Collections;

public class OrientationToCenterConstraint : SpawnConstraint
{
    public bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options)
    {
        rotation = Quaternion.LookRotation(Options.SpawnArea.center- position, new Vector3(0,1,0));
        rotation = rotation * Quaternion.Euler(0, -90, 0);
        Vector3 halfext = asset.GetComponent<Collider>().bounds.extents / 2;
        if (Physics.CheckBox(position, halfext, rotation)) return false;
        return true;
    }
}

