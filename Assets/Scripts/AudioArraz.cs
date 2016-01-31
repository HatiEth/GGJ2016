using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class AudioArraz : MonoBehaviour {

	public AudioClip[] SoundFiles;
	public float DelayAfterPlay;

	private AudioSource aSource;
	// Use this for initialization
	void Start () {
	}

	void OnEnable() {
		aSource = GetComponent<AudioSource> ();
		StartCoroutine (PlaySound ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator PlaySound ()
	{
		while (enabled) {
			var clip = SoundFiles[Random.Range(0, SoundFiles.Length-1)];
			if (clip != null) {
				aSource.PlayOneShot(clip);
				yield return new WaitForSeconds (clip.length + DelayAfterPlay);
			}
			yield return null;
		}
	}
}
