using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    public List<SpawnItem> ToSpawn;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < ToSpawn.Count; i++)
            ToSpawn[i].Spawn(new Bounds(transform.position - transform.lossyScale, transform.lossyScale * 2));
	}
    // Update is called once per frame
    void Update () {
	
	}
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position - transform.lossyScale, transform.lossyScale * 2);
    }
}
