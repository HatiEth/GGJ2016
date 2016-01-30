using UnityEngine;
using System.Collections;

public class Daytimecontrol : MonoBehaviour {
    public static Daytimecontrol timeControl;
    public float gametime
    {
        get;
        private set;
    }
    float speed = 12f; //1h = 5min
    float startoffset = 60f * 60f * 16f; //16Uhr 
    public static float dawn = 60f * 60f * 18f;
    public static float night = 60f * 60f * 19f;

    // Use this for initialization
    void Start () {
        timeControl = this;
        gametime = startoffset;
	    
	}
	
	// Update is called once per frame
	void Update () {
        gametime = startoffset + Time.timeSinceLevelLoad * speed * 10f; //FIXME: *10 for debug speed
	}
}
