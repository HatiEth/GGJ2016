using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeSceneOnAnyKey : MonoBehaviour {

    public string SceneName;
    bool hasChanged = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.anyKeyDown && !hasChanged)
        {
            hasChanged = true;
            SceneManager.LoadScene(SceneName);
        }
	}
}
