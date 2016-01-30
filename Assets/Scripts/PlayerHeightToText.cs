using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHeightToText : MonoBehaviour {

    Text text;
    float maxHeight = float.NegativeInfinity;

    public Transform Player;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        maxHeight = Mathf.Max(Player.position.y, maxHeight);
        text.text = maxHeight + "m";
	
	}
}
