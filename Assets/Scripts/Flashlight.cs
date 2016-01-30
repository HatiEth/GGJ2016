using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

    public float intensity= 2.5f;
    Light l;

	// Use this for initialization
	void Start () {
        l = GetComponent<Light>();
        l.intensity = 0f;
        l.color = new Color(192f / 255f, 1f, 49f / 255f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("f"))
        {
            l.intensity = intensity;
            Sun.sun.fogreduction = 0.2f;
            RenderSettings.fogMode = FogMode.Exponential;
        }
        if (Input.GetKeyDown("g"))
        {
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            Sun.sun.fogreduction = 0.0f;
            l.intensity = 0f;
        }
    }
}
