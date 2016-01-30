using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Picker : MonoBehaviour {

    public float RaycastLength = 10f;
    public Transform PickupAnchor;
    RaycastHit[] pickupHits = new RaycastHit[20];
    int currentInteractHits;

    public bool HasPickup { get { return PickupAnchor.transform.childCount > 0; } }

    // Update is called once per frame
    void Update()
    {
        Ray testRay = new Ray(transform.position, transform.forward);
        currentInteractHits = Physics.RaycastNonAlloc(testRay, pickupHits, RaycastLength);
        if (Input.GetButtonDown("Fire1"))
        {
            if (HasPickup)
            {
                DropObject();
            }
            else
            {
                InteractWithObjectInFront();
            }
        }
        if (Input.GetButtonDown("Fire2") && HasPickup)
        {
            StartCoroutine(RotatePickup());
        }
    }

    void InteractWithObjectInFront()
    {
        if (currentInteractHits > 0)
        {
            for (int i = 0; i < currentInteractHits; ++i)
            {
                var hitTransform = pickupHits[i].transform;
                var po = hitTransform.GetComponent<Interactable>();
                if (po != null)
                {
                    po.Interact(this);
                }
                break;
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

            Children[i].GetComponent<Pickup>().IsPickedUp = false;
        }
    }

    IEnumerator RotatePickup()
    {
        Vector3 mp = Input.mousePosition;
        while (!Input.GetButtonUp("Fire2"))
        {
            yield return null;
            Vector3 mdelta = Input.mousePosition - mp;
            float yRot = Input.GetAxis("Mouse X");
            float xRot = Input.GetAxis("Mouse Y");

            PickupAnchor.GetChild(0).localRotation *= Quaternion.Euler (0f, 0f, yRot);
            PickupAnchor.localRotation *= Quaternion.Euler (-xRot, 0f, 0f);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * RaycastLength);
    }
}
