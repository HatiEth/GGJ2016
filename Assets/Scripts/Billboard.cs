using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    [SerializeField]
    bool UseOrigin = false;

    Quaternion origin;


    void Start()
    {
        origin = transform.rotation;

    }

	void Update () {

        //transform.rotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
        transform.LookAt(Camera.main.transform);
        if (UseOrigin)
        {
            transform.localRotation = origin * transform.localRotation;
        }
	}
}
