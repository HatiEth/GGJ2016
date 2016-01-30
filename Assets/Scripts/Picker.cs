using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Picker : MonoBehaviour {

    public float RaycastLength = 10f;
    public Transform PickupAnchor;
    RaycastHit[] pickupHits = new RaycastHit[20];

    public bool HasPickup { get { return PickupAnchor.transform.childCount > 0; } }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (HasPickup)
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
        int results = Physics.RaycastNonAlloc(testRay, pickupHits, RaycastLength);
        if (results > 0)
        {
            PickUpObject pickupable = null;
            for (int i = 0; i < results; ++i)
            {
                var hitTransform = pickupHits[i].transform;
                var po = hitTransform.GetComponent<PickUpObject>();
                if (po != null)
                {
                    hitTransform.parent = PickupAnchor.transform;
                    hitTransform.localPosition = Vector3.zero;

                    hitTransform.GetComponent<Rigidbody>().useGravity = false;
                    hitTransform.GetComponent<Rigidbody>().isKinematic = true;
                    hitTransform.gameObject.layer = LayerMask.NameToLayer("PickedUp");

                    po.isPickedUp = true;

                    if (hitTransform.GetComponent<FixedJoint>() != null)
                    {
                        //GameObject.Destroy(hitTransform.GetComponent<FixedJoint>());
                        // Detach all fixed joints and detach from all fixed joints attached on
                        po.DisconnectAllStickies();
                    }

                    break;
                }

            }
        }
    }

    void DropObject()
    {
        List<Transform> Children = new List<Transform>(PickupAnchor.childCount);
        for (int i = 0; i < PickupAnchor.childCount; ++i)
        {
            Children.Add(PickupAnchor.GetChild(i));
        }

        PickupAnchor.transform.DetachChildren();
        for (int i = 0; i < Children.Count; ++i)
        {
            var rigidbody = Children[i].GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
                rigidbody.gameObject.layer = 0;
            }

            Children[i].GetComponent<PickUpObject>().isPickedUp = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * RaycastLength);
    }
}
