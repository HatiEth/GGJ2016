using UnityEngine;
using System.Collections;

public class StickyGroup : MonoBehaviour {

    void Start()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

}
