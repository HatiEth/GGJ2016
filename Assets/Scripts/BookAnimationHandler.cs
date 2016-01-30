using UnityEngine;
using System.Collections;

public class BookAnimationHandler : MonoBehaviour {

    KnifeScan scanner;
    Animator anim;

	// Use this for initialization
	void Start () {
        scanner = GetComponent<KnifeScan>();
        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

        anim.SetBool("IsAttacking", (scanner.IsAlive && (scanner.IsFiring || GetComponent<Rigidbody>().isKinematic)));
	}
}
