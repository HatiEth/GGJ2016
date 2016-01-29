using UnityEngine;
using System.Collections;

public class Picker : MonoBehaviour {

    public Transform PickupAnchor;
    RaycastHit[] pickupHits = new RaycastHit[20];

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (PickupAnchor.transform.childCount > 0)
            {
                DropObject();
            }
            else
            {
                PickupObjectInFront();
            }
        }
    }

    void PickupObjectInFront()
    {
        Ray testRay = new Ray(transform.position, transform.forward);
        int results = Physics.RaycastNonAlloc(testRay, pickupHits, 10f);
        if (results > 0)
        {
            PickUpObject pickupable = null;
            for (int i = 0; i < results; ++i)
            {
                var hitTransform = pickupHits[i].transform;
                if (hitTransform.GetComponent<PickUpObject>() != null)
                {
                    hitTransform.parent = PickupAnchor.transform;
                    hitTransform.localPosition = Vector3.zero;

                    hitTransform.GetComponent<Rigidbody>().useGravity = false;
                    hitTransform.GetComponent<Rigidbody>().isKinematic = true;
                    break;
                }

            }
        }
    }

    void DropObject()
    {
        for (int i = 0; i < PickupAnchor.childCount; ++i)
        {
            var rigidbody = PickupAnchor.GetChild(i).GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
            }
        }

        PickupAnchor.transform.DetachChildren();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10f);
    }
}
