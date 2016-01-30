using UnityEngine;
using System.Collections;

public class BottomConstraint : SpawnConstraint
{
    public bool Run(ref Vector3 position, ref Quaternion rotation, GameObject asset, ref SpawnConstraintOptions Options)
    {
        Collider c = asset.GetComponent<Collider>();
        Vector3 halfext = asset.GetComponent<Collider>().bounds.extents / 2;
        RaycastHit[] hits = Physics.BoxCastAll(position,halfext , new Vector3(0, -1, 0));
        float mindist = float.PositiveInfinity;
        for (int i = 0;i < hits.Length;i++)
        {
            if (mindist > hits[i].distance) mindist = hits[i].distance;
        }
        if (float.IsInfinity(mindist)) return false;
        if (Physics.CheckBox(position, halfext)) return false;
        position = position + mindist * new Vector3(0, -1, 0) - new Vector3(0, -1, 0);
        return true;
    }
}

