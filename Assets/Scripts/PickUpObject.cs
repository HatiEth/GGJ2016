using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PickUpObject : MonoBehaviour {

    public bool isPickedUp = false;

    public List<FixedJoint> connectees = new List<FixedJoint>();

    [HideInInspector]
    public new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Decrease joint count...

    void OnCollisionStay(Collision collision)
    {
        if (!isPickedUp && collision.gameObject.GetComponent<PickUpObject>() && !collision.gameObject.GetComponent<PickUpObject>().isPickedUp && collision.gameObject.GetComponent<StickyObject>() != null)
        {
            FixedJoint[] fjoints = gameObject.GetComponents<FixedJoint>();
            if(fjoints.Length > 0 && fjoints.All(f => f.connectedBody != collision.rigidbody)) // Don't connect to same
            {
                var fjoint = gameObject.AddComponent<FixedJoint>();
                fjoint.connectedBody = collision.rigidbody;
                connectees.Add(fjoint);
            }
            else if (fjoints.Length == 0) // Connect if first
            {
                var fjoint = gameObject.AddComponent<FixedJoint>();
                fjoint.connectedBody = collision.rigidbody;
                connectees.Add(fjoint);
                LockRigidbody();
            }
        }
    }

    public void DisconnectAllStickies()
    {
        while (connectees.Count > 0)
        {
            if (connectees[0].connectedBody != null)
            {
                connectees[0].connectedBody.GetComponent<PickUpObject>().DetachSticky(rigidbody);
            }
            Destroy(connectees[0]);
            connectees.RemoveAt(0);
        }
        UnlockRigidbody();
    }

    public void DetachSticky(Rigidbody body)
    {
        int found = -1;
        for (int i = 0; i < connectees.Count; ++i)
        {
            if (connectees[i].connectedBody == body)
            {
                found = i;
                break;
            }
        }

        if (found != -1)
        {
            Destroy(connectees[found]);
            connectees.RemoveAt(found);
            if (connectees.Count == 0)
            {
                UnlockRigidbody();
            }
        }
    }

    void LockRigidbody()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
    }

    void UnlockRigidbody()
    {
        rigidbody.constraints = RigidbodyConstraints.None;
    }
}
