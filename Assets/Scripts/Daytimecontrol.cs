using UnityEngine;
using System.Collections;

public class Daytimecontrol : MonoBehaviour {
    public float gametime
    {
        get;
        private set;
    }
    float speed = 12f; //1h = 5min
    float startoffset = 60f * 60f * 16f; //16Uhr 

	// Use this for initialization
	void Start () {
        gametime = startoffset;
	    
	}
	
	// Update is called once per frame
	void Update () {
        gametime = startoffset + Time.timeSinceLevelLoad * speed * 10f; //FIXME: *10 for debug speed
	}
}
