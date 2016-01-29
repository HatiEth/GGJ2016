using UnityEngine;
using System.Collections;

public class DummyBlockSpawner : MonoBehaviour {

    public KeyCode SpawnKey = KeyCode.Alpha1;
    public GameObject BlockGO;

    public Bounds bound;

    private Vector3 lastPosition;

    // Use this for initialization
    void Start()
    {
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(SpawnKey))
        {
            var go = GameObject.Instantiate<GameObject>(BlockGO);
            go.transform.position = PointInsideBounds();
            lastPosition = go.transform.position;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(transform.position, Vector3.Scale(transform.lossyScale, bound.size));
    }


    Vector3 PointInsideBounds()
    {
        Vector3 p;

        Vector3 pMin = transform.position - (Vector3.Scale(transform.lossyScale, bound.extents));
        Vector3 pMax = transform.position + (Vector3.Scale(transform.lossyScale, bound.extents));

        p.x = Random.Range(pMin.x, pMax.x);
        p.y = Random.Range(pMin.y, pMax.y);
        p.z = Random.Range(pMin.z, pMax.z);

        return (p);
    }
}
