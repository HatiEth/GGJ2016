using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeSceneOnAnyKey : MonoBehaviour {

    public string SceneName;

    bool isChanging = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown && !isChanging)
        {
            isChanging = true;
            SceneManager.LoadScene(SceneName);
        }
	}
}
