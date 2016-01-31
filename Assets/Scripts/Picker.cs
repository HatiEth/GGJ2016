using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Picker : MonoBehaviour {

    public float RaycastLength = 10f;
    public Transform PickupAnchor;
    public GameObject MainMenu;
    RaycastHit[] pickupHits = new RaycastHit[20];
    int currentInteractHits;

    [SerializeField]
    private float MaxPickUpDistance = 5f;
    [SerializeField]
    private float MinPickUpDistance = 2.5f;

    bool HasMainMenu = false;
    GameObject MainMenuInst;

    public bool HasPickup { get { return PickupAnchor.transform.childCount > 0; } }


    void Start()
    {
        PickupAnchor.transform.localPosition = PickupAnchor.transform.localPosition.normalized * MinPickUpDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (HasMainMenu)
            if ((MainMenuInst.transform.position - transform.position).magnitude > 6)
        {
            { Destroy(MainMenuInst); HasMainMenu = false; }
        }

        Ray testRay = new Ray(transform.position, transform.forward);
        currentInteractHits = Physics.RaycastNonAlloc(testRay, pickupHits, RaycastLength);

        for (int i = 0; i < currentInteractHits; i++)
        {
            var hitTransform = pickupHits[i].transform;
            var po = hitTransform.GetComponent<Outlineable>();
            if (po != null)
            {
                po.onMouseover();
            }
        }

        if (Input.GetKeyDown("escape"))
        {
            if (HasMainMenu)
            {
                Destroy(MainMenuInst);
                HasMainMenu = false;
            }
            
            Vector3 v = transform.rotation * new Vector3(0, 0, 0);
            MainMenuInst = (GameObject)Instantiate(MainMenu, transform.position + v, transform.rotation);
            HasMainMenu = true;
        }

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

        if (HasPickup && Input.mouseScrollDelta.y != 0f)
        {
            float currentDistance = PickupAnchor.transform.localPosition.magnitude;
            currentDistance += Input.mouseScrollDelta.y / 10f;
            float dist = Mathf.Clamp(currentDistance, MinPickUpDistance, MaxPickUpDistance);

            PickupAnchor.transform.localPosition = PickupAnchor.transform.localPosition.normalized * dist;
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


                var p2 = hitTransform.GetComponent<Klickable>();
                if (p2 != null)
                {
                    ResolveScript(p2.Target);
                    break;
                }

                var po = hitTransform.GetComponent<Interactable>();
                
                if (po != null)
                {
                    po.Interact(this);
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

            Children[i].GetComponent<Pickup>().IsPickedUp = false;
        }
    }

    IEnumerator RotatePickup()
    {
        Vector3 mp = Input.mousePosition;
        while (!Input.GetButtonUp("Fire2")&&HasPickup)
        {
            Vector3 mdelta = Input.mousePosition - mp;
            float yRot = Input.GetAxis("Mouse X");
            float xRot = Input.GetAxis("Mouse Y");

            PickupAnchor.GetChild(0).localRotation *= Quaternion.Euler (0f, 0f, yRot);
            PickupAnchor.localRotation *= Quaternion.Euler (-xRot, 0f, 0f);
            yield return null;
            
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * RaycastLength);
    }

    public void ResolveScript(string S)
    {
        if (S == "Exit")
        {
            Application.Quit();
        }
        if (S == "Restart")
        {
            Destroy(MainMenuInst);
            HasMainMenu = false;
            if (HasPickup) DropObject();
            transform.parent.transform.position = new Vector3(15, 0.6f, 15);
        }
    }
}
