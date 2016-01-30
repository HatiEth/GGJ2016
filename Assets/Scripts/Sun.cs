using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Sun : MonoBehaviour {

    public static Sun sun;

    Vector3 start = new Vector3(270f, 90f, 0f);
    public Color dayColor = new Color(255f / 255f, 244f / 255f, 214f / 255f);
    public Color dawnColor = new Color(238f/255f, 123f / 255f, 104f / 255f);
    float dawntime = 60.0f * 60.0f * 17.0f;
    float dawnend = 60.0f * 60.0f * 18.0f;
    float nighttime = 60.0f * 60.0f * 19.0f;

    float ambienIday = 0.9f;
    float ambienInight = 0.2f;

    float reflectday = 0.5f;
    float reflectnight = 0.0f;

    float fogday = 0.00f;
    float fognight = 0.5f;
    public float fogreduction=0f;


    Light l;
    // Use this for initialization
    void Start () {
        l = GetComponent<Light>();
        l.color = dayColor;
        sun = this;
        l.shadowStrength = 0.6f;
        Camera.main.GetComponent<SunShafts>().sunColor = l.color;
        if (Camera.main.GetComponent<SunShafts>()==null)
            Camera.main.gameObject.AddComponent<SunShafts>();
        if (!Camera.main.GetComponent<SunShafts>().isActiveAndEnabled)
            Camera.main.GetComponent<SunShafts>().enabled=true;
        Camera.main.GetComponent<SunShafts>().sunTransform = l.transform;

        RenderSettings.fog = true;
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogMode = FogMode.ExponentialSquared;

        RenderSettings.reflectionIntensity = reflectday;
    }
	
	// Update is called once per frame
	void Update () {
        Daytimecontrol time = transform.parent.GetComponent<Daytimecontrol>();
        if (time.gametime > 60 * 60 * 24)
            return; //there is no HOPE
        transform.rotation=Quaternion.Euler(start+time.gametime*new Vector3(-360f/(60f*60f*24f),0,0));

        LightColorControl(time);
        AmbientControl(time);
    }


    void LightColorControl(Daytimecontrol time)
    {
        if (time.gametime > dawntime && time.gametime < dawnend)
        {
            l.color = Color.Lerp(dayColor, dawnColor, (time.gametime - dawntime) / (dawnend - dawntime));
            RenderSettings.reflectionIntensity = Mathf.Lerp(reflectday, reflectnight, (time.gametime - dawntime) / (dawnend - dawntime));
            Camera.main.GetComponent<SunShafts>().sunColor = l.color;
        }
    }

    void AmbientControl(Daytimecontrol time)
    {
        if (time.gametime > dawntime && time.gametime < dawnend)
            RenderSettings.ambientLight= Color.Lerp(dayColor, dawnColor, (time.gametime - dawntime) / (dawnend - dawntime));

        if (time.gametime > dawntime && time.gametime < nighttime)
        {
            RenderSettings.ambientIntensity = Mathf.Lerp(ambienIday, ambienInight, (time.gametime - dawntime) / (nighttime - dawntime));
        }

        if (time.gametime > dawnend && time.gametime < nighttime)
        {
            RenderSettings.fogDensity = Mathf.Lerp(fogday, fognight, (time.gametime - dawnend) / (nighttime - dawnend));
            RenderSettings.fogDensity -= RenderSettings.fogDensity * fogreduction;
        }


    }
}
