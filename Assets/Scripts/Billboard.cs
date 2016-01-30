using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    Quaternion origin;

    void Start()
    {
        origin = transform.rotation;

    }

	void Update () {

        //transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
        transform.LookAt(Camera.main.transform);
	}
}
