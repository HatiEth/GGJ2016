using UnityEngine;
using System.Collections;

public class RandomOrientationConstraint : SpawnConstraint
{
    string orient;
    public RandomOrientationConstraint(string Orientation)
    {
        orient = Orientation;
    }

    public bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options)
    {
        if (orient == "Z")
        {
            rotation = Quaternion.Euler(0, Random.value * 360.0f, 0);
        }
        Vector3 halfext = asset.GetComponent<Collider>().bounds.extents / 2;
        if (Physics.CheckBox(position, halfext, rotation)) return false;
        return true;
    }
}

