using UnityEngine;
using System.Collections;

public class KnifeScan : MonoBehaviour {

    public Transform Player;

    public int StartChance = 0;
    public int IncrementalStartChance = 1;
    public bool IsFiring = false;
    public bool IsAlive = true;

    private bool wasFiringOnce = false;

    public bool StickyOnCollision = true;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(SearchForTarget());

    }

    IEnumerator SearchForTarget()
    {
        RaycastHit hitInfo;
        while (IsAlive)
        {
            var old = gameObject.layer;
            gameObject.layer = 2;
            bool hasHit = Physics.Raycast(transform.position, Player.position - transform.position, out hitInfo, Mathf.Infinity, ~Physics.IgnoreRaycastLayer);
            if (hasHit && hitInfo.collider.CompareTag("Player"))
            {
                if (Random.Range(0, 100) < StartChance)
                {
                    StartCoroutine(FireAtTarget(hitInfo.collider.transform));
                    yield return new WaitUntil(() => !IsFiring);
                }
                else
                {
                    StartChance += IncrementalStartChance;
                }
            }
            else
            {
                Debug.DrawRay (transform.position, (Player.position - transform.position) *1000, Color.red);
            }
            yield return null;
            gameObject.layer = old;

        }
    }

    IEnumerator FireAtTarget(Transform target)
    {
        IsFiring = true;
        var rigidbody = this.GetComponent<Rigidbody>();
        if (rigidbody == null) yield break;
        rigidbody.isKinematic = true;
        

        while (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target.position - transform.position)) > 3f)
        {
            transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((target.position - transform.position).normalized, Vector3.up), Time.deltaTime);
            yield return null;
        }

        Vector3 targetPosAfterAim = target.position;
        Vector3 dir = (targetPosAfterAim - transform.position).normalized;

        while ((targetPosAfterAim - transform.position).magnitude > 10f)
        {
            rigidbody.MovePosition(transform.position + (targetPosAfterAim - transform.position).normalized * 50f * Time.fixedDeltaTime);
            yield return null;
        }

        rigidbody.isKinematic = false;
        rigidbody.AddForce(dir, ForceMode.Impulse);

        yield return new WaitForSeconds(Random.Range(5, 10));
        StartChance = -StartChance;
        IsFiring = false;
        wasFiringOnce = true;
        yield break;
    }

    public void OnDrawGizmosSelected()
    {
        if (Player == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Player.position - transform.position) * 2f);

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!wasFiringOnce) return;
        if (StickyOnCollision)
        {
            if (collision.gameObject.CompareTag("Player") || (collision.transform.GetComponent<StickyObject>() == null && collision.transform.GetComponent<StickyGroup>() == null))
            {
                transform.parent = collision.transform;
                Destroy(GetComponent<Rigidbody>());
                Destroy(GetComponent<StickyObject>());
                Destroy(GetComponent<Pickup>());
                IsAlive = false;
            }

            if (collision.transform.GetComponent<StickyObject>() != null || collision.transform.GetComponent<StickyGroup>() != null)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                IsAlive = false;
            }
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            IsAlive = false;
        }
    }
}
