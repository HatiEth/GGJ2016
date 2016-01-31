using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DaytimeToText : MonoBehaviour {
	Daytimecontrol day;
	public Text text;

	// Use this for initialization
	void Start () {
		day = GetComponent<Daytimecontrol> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = ""+day.gametime;
	}
}
