using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum Side
{
    North, West, South, East
}

[System.Serializable]
public struct SpawnItem {
    public GameObject PreFab;
    public float Density;
    Bounds currentbound;

    public void Spawn(Bounds B)
    {
        currentbound = B;
        for (int i = 0; i < 50; i++)
        {
            Vector3 Position = new Vector3(B.min.x + (B.max.x - B.min.x) * Random.value, B.min.y + (B.max.y - B.min.y) * Random.value, B.min.z + (B.max.z - B.min.z) * Random.value);
            Quaternion Rotation = new Quaternion();
            List<string> constraints = PreFab.GetComponent<PlacementConstraint>().Constraints;
            bool Next = false;
            for (int j = 0; j < constraints.Count;j++)
            {
                if (!SolveConstraint(constraints[j], ref Position, ref Rotation)) Next = true;
            }
            if (Next) continue;
            
            GameObject.Instantiate(PreFab, Position, new Quaternion());
        }
    }

    public bool SolveConstraint(string Constraint, ref Vector3 Position, ref Quaternion Rotation)
    {
        if (Constraint == "Bottom") return Bottom(ref Position, ref Rotation);
        if (Constraint == "North") return BorderSpawn(ref Position, ref Rotation, Side.North);
        throw new System.Exception("Unkown Constraint: " + Constraint);
    }

    private bool BorderSpawn(ref Vector3 Position, ref Quaternion Rotation, Side Direction)
    {
        //Vector3 DirA =
        //currentbound.
        return true;
    }

    private bool Bottom(ref Vector3 Position, ref Quaternion Rotation)
    {
        //RaycastHit[] rch = Physics.RaycastAll(new Ray(Position, new Vector3(0, -1, 0)));
        Vector3 halfext = PreFab.transform.GetComponent<Renderer>().bounds.extents / 2;
        RaycastHit[] rch = Physics.BoxCastAll(Position, halfext, new Vector3(0, -1, 0));
        float minlen = float.PositiveInfinity;
        for (int i = 0; i < rch.Length; i++)
            if (rch[i].distance < minlen)
                minlen = rch[i].distance;
        if (float.IsInfinity(minlen)) return false;
        Position = Position + new Vector3(0, -minlen + PreFab.transform.localScale.y, 0);
        if (Physics.CheckBox(Position,halfext))
            return false;
        return true;
    }
}
