using UnityEngine;
using System.Collections;

public class SideConstraint : SpawnConstraint
{
    public bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options)
    {
        Vector3 Direction = new Vector3();
        Quaternion Orientation = new Quaternion();
        int rnd = (int)(Random.value * 4);
        if (rnd == 0) { Direction = new Vector3(-1, 0, 0); Orientation = Quaternion.Euler(0,90,0); }
        if (rnd == 1) { Direction = new Vector3(1, 0, 0); Orientation = Quaternion.Euler(0, -90, 0); }
        if (rnd == 2) { Direction = new Vector3(0, 0, 1); Orientation = Quaternion.Euler(0, 180, 0); }
        if (rnd == 3) { Direction = new Vector3(0, 0, -1); Orientation = Quaternion.Euler(0, 0, 0); }

        Collider c = asset.GetComponent<Collider>();
        Vector3 halfext = asset.GetComponent<Collider>().bounds.extents / 2;
        RaycastHit[] hits = Physics.BoxCastAll(position,halfext , Direction);
        float mindist = float.PositiveInfinity;
        for (int i = 0;i < hits.Length;i++)
        {
            if (mindist > hits[i].distance) mindist = hits[i].distance;
        }
        if (float.IsInfinity(mindist)) return false;
        if (Physics.CheckBox(position, halfext,Orientation)) return false;
        position = position + mindist * Direction;
        rotation *= Orientation;
        return true;
    }
}

