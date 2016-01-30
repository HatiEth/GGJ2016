using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SchalterToggle : MonoBehaviour, Interactable {

    bool IsActive = false;
    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
    public void Interact(Picker picker)
    {
        IsActive = !IsActive;
        anim.SetBool("IsActive", IsActive);
    }
}
