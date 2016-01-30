using UnityEngine;
using System.Collections;

public class CircularConstraint : SpawnConstraint
{
    public bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options)
    {
        float Rotation = Random.value * 360.0f;
        Vector3 Center = Options.SpawnArea.center;
        Vector3 halfext = asset.GetComponent<Collider>().bounds.extents / 2;

        position = Center + Quaternion.Euler(0, Rotation, 0) * new Vector3(Options.SpawnArea.extents.x, 0, 0);
        if (Physics.CheckBox(position, halfext, rotation)) return false;
        return true;
    }
}

