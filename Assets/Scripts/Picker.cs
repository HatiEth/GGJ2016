using UnityEngine;
using System.Collections;

public class Picker : MonoBehaviour {

    public Transform PickupAnchor;
    RaycastHit[] pickupHits = new RaycastHit[20];

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            PickupObjectInFront();
        }
	}

    void PickupObjectInFront()
    {
        Ray testRay = new Ray(transform.position, transform.forward);
        Physics.RaycastNonAlloc(testRay, pickupHits, 10f);

    }
}
