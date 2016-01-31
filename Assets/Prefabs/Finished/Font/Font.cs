using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Font : MonoBehaviour {

    public List<GameObject> ObjectFont;
    public string Text;
    public string Target;
    public const float LetterDistance = 0.8f;

    public List<GameObject> GetObjs(string s)
    {
        s = s.ToLower();
        List<GameObject> Result = new List<GameObject>();
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '?') Result.Add(ObjectFont[26]);
            else
            if (s[i] == '!') Result.Add(ObjectFont[27]);
            else
            if (s[i] == ' ')
                Result.Add(new GameObject());
            else
            {
                int nr = s[i] - 'a';
                Result.Add(ObjectFont[nr]);
            }
        }
        return Result;
    }

    public void Spawn(string s, Vector3 tPosition)
    {
        List<GameObject> BluePrints = GetObjs(s);
        for (int i = 0; i < BluePrints.Count; i++)
        { 
            Vector3 Pos = new Vector3(-s.Length * LetterDistance / 2 + LetterDistance * i, 0, 0);
            Pos =transform.rotation * Pos;
            Pos = Quaternion.Euler(0, 0, 0) * Pos;
            GameObject letter = (GameObject)Instantiate(BluePrints[i],tPosition + Pos, new Quaternion());
            letter.transform.rotation = Quaternion.Euler(-90,0,0);
            Klickable t = letter.AddComponent<Klickable>();
            t.Target = Target;

            letter.transform.parent = transform;
        }
    }    

	// Use this for initialization
	void Start () {
        Spawn(Text,transform.position);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
