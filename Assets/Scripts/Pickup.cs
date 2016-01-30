using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour, Interactable {

    public bool IsPickedUp;




    public void Interact(Picker picker)
    {
        if (!IsPickedUp)
        {
            transform.parent = picker.PickupAnchor.transform;
            transform.localPosition = Vector3.zero;

            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            transform.gameObject.layer = LayerMask.NameToLayer("PickedUp");

            this.IsPickedUp = true;
        }
    }
}
