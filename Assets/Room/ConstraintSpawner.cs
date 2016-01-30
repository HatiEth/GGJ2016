using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConstraintSpawner : MonoBehaviour {

    public bool SpawnAtStart;
    public Biom Desc;

	// Use this for initialization
	void Start () {
        if (SpawnAtStart)
            GenerateContent();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 siz = transform.localScale;
        Gizmos.DrawWireCube(transform.position, siz);
    }

    static Dictionary<GameObject, GameObject> Cache;
    static GameObject GetCache(GameObject G)
    {
        if (Cache == null) Cache = new Dictionary<GameObject, GameObject>();
        if (!Cache.ContainsKey(G))
            Cache[G] = (GameObject)Instantiate(G, new Vector3(0, -100, 0), new Quaternion());
        return Cache[G];
    }

    public void GenerateContent()
    {
        for (int i = 0; i < Desc.Spawnables.Count; i++)
        {
            for (int j = 0; j < Desc.Spawnables[i].AverageAmount; j++)
            {
                int nr = (int)(Desc.Spawnables[i].Asset.Count * Random.value);
                GameObject Asset = GetCache(Desc.Spawnables[i].Asset[nr]);

                Bounds Area = new Bounds(transform.position, transform.localScale);
                Vector3 Position = Area.min + new Vector3((Area.max.x - Area.min.x) * UnityEngine.Random.value, (Area.max.y - Area.min.y) * UnityEngine.Random.value, (Area.max.z - Area.min.z) * UnityEngine.Random.value);
                Quaternion Rotation = new Quaternion();
                bool no = false;
                SpawnConstraintOptions Options = new SpawnConstraintOptions();
                Options.SpawnArea = Area;
                for (int x = 0; x < Desc.Spawnables[i].SpawnRules.Count; x++)
                {
                    string s = Desc.Spawnables[i].SpawnRules[x];
                    no = no || !Spawnable.Factory(s).Run(ref Position, ref Rotation, Asset, ref Options);
                }
                if (!no)
                {
                    GameObject o = (GameObject)Instantiate(Asset, Position, Rotation);
                    if (Options.IsFixed)
                        o.GetComponent<Rigidbody>().isKinematic = true;
                }



            }
        }
    }
}
