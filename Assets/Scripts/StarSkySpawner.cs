using UnityEngine;
using System.Collections;

public class StarSkySpawner : MonoBehaviour {

    public GameObject StarPrefab;
    public int StarCount = 10;

    public float Radius = 10f;

	// Use this for initialization
	void Start () {
        for (int i=0;i<StarCount;++i) {
            Vector3 r = Random.onUnitSphere;
            r.y = Mathf.Abs(r.y);

            var go = GameObject.Instantiate<GameObject>(StarPrefab);
            go.transform.parent = this.transform;
            go.transform.position = transform.position + r * Radius;
        }
	
	}

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
