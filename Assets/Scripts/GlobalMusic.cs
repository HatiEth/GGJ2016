using UnityEngine;
using System.Collections;

public class GlobalMusic : MonoBehaviour {

	public Daytimecontrol DaytimeControl;

	public AudioSource CalmMusic;
	public AudioSource DawnMusic;
	public AudioSource NightMusic;

	public float CalmMusicFadeOut;
	public float DawnMusicFadeOut;

	public AnimationCurve FadeOutCurve;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (DaytimeControl.gametime < CalmMusicFadeOut) {
			float calmMusicVolume = FadeOutCurve.Evaluate (DaytimeControl.gametime - (Daytimecontrol.dawn -800) );
			CalmMusic.volume = calmMusicVolume;
			DawnMusic.volume = 1.0f - calmMusicVolume;
			NightMusic.volume = 0f;
		} else {
			float dawnMusicVolume = FadeOutCurve.Evaluate (DaytimeControl.gametime - Daytimecontrol.night);
			DawnMusic.volume = dawnMusicVolume;
			CalmMusic.volume = 0f;
			NightMusic.volume = 1.0f - dawnMusicVolume;
		}
	}
}
