using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public struct Biom
{
    public string Name;
    public List<Spawnable> Spawnables;
    public Material Clr;
}
