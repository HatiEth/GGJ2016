using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour, Interactable, Outlineable {

    public bool IsPickedUp;
    private bool mouseover;
    

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

    public void onMouseover()
    {
        if (GetComponent<Renderer>())
        {
            Material[] ms = GetComponent<Renderer>().materials;
            foreach (Material m in ms)
            {
                if (m.HasProperty("_drawOutline"))
                {
                    m.SetFloat("_drawOutline", 1f);
                    mouseover = true;
                }
            }
        }
        else
        {
            Renderer[] rs = GetComponentsInChildren<Renderer>();

            foreach (Renderer r in rs)
            {
                Material[] ms = r.materials;
                foreach (Material m in ms)
                {
                    if (m.HasProperty("_drawOutline"))
                    {
                        m.SetFloat("_drawOutline", 1f);
                        mouseover = true;
                    }
                }
            }
        }
    }

    public void Update()
    {
        if (GetComponent<Renderer>())
        {
            if (!mouseover)
            {
                Material[] ms = GetComponent<Renderer>().materials;
                foreach (Material m in ms)
                {
                        if (m.HasProperty("_drawOutline"))
                            m.SetFloat("_drawOutline", 0f);
                }
            }
            else
                mouseover = false;
        }
        else
        {
            Renderer[] rs = GetComponentsInChildren< Renderer > ();
            foreach(Renderer r in rs)
            {
                if (!mouseover)
                {
                    Material[] ms = r.materials;
                    foreach (Material m in ms)
                    {
                        if (m.HasProperty("_drawOutline"))
                            m.SetFloat("_drawOutline", 0f);
                    }
                }
                else
                    mouseover = false;
            }

        }



    }
}
