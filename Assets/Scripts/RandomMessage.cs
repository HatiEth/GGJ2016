using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomMessage : MonoBehaviour {

    public List<string> Possible;
    public GameObject Msgob;

	// Use this for initialization
	void Start () {
        Font f = Msgob.GetComponent<Font>();
        f.Target = "";
        f.Text = Possible[(int)(Possible.Count * Random.value)];
        Instantiate(Msgob,transform.position, new Quaternion());
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(this);
	}
}
