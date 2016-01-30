using UnityEngine;
using System.Collections;

public class StickyObject : MonoBehaviour {

    public bool IsPartOfGroup { get { return group != null; } }
    StickyGroup group = null;

    void Start()
    {
        if (GetComponent<StickyGroup>() != null) {
            group = GetComponent<StickyGroup>();
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (GetComponent<Pickup>() != null && GetComponent<Pickup>().IsPickedUp)
            return;
        if (GetComponent<Pickup>() != null && !GetComponent<Pickup>().OnceWasPickedUp)
            return;

        if (collision.gameObject.GetComponent<StickyGroup>() != null)
        {
            group = collision.gameObject.GetComponent<StickyGroup>();
            AddToOwnGroup(this);
        }

        if (collision.gameObject.GetComponent<StickyObject>() != null)
        {
            if (!IsPartOfGroup && !collision.gameObject.GetComponent<StickyObject>().IsPartOfGroup)
            {
                // Disabled for gameplay reasons
                //CreateStickyGroup(); 
            }
            else if (!IsPartOfGroup)
            {
                collision.gameObject.GetComponent<StickyObject>().AddToOwnGroup(this);
            }
            else if (!collision.gameObject.GetComponent<StickyObject>().IsPartOfGroup)
            {
                AddToOwnGroup(collision.gameObject.GetComponent<StickyObject>());
            }
            else
            {
            }
        }
    }

    void MergeGroups(StickyObject other)
    {
        Debug.Log("Merge groups");
        other.transform.parent = null;
        if (other.group.transform.childCount == 0)
        {
            Destroy(other.group.gameObject);
        }
        other.group = null;
        AddToOwnGroup(other);
    }

    void AddToOwnGroup(StickyObject obj)
    {
        StartCoroutine(DelaySticky(obj));
    }

    IEnumerator DelaySticky(StickyObject obj)
    {
        Destroy(obj.GetComponent<Rigidbody>());
        obj.GetComponent<StickyObject>().group = group;
        obj.transform.parent = group.transform;

        yield return null;
    }

    void CreateStickyGroup()
    {
        Debug.Log("New sticky group from " + gameObject);
        var go = new GameObject("Sticky Group");
        group = go.AddComponent<StickyGroup>();
        go.AddComponent<Rigidbody>();

        go.transform.position = transform.position;
        transform.parent = go.transform;

        Destroy(transform.GetComponent<Rigidbody>());
    }
}
