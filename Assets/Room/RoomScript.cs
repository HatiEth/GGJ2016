using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WallConfig
{
    Door, None, Wall
}

public class RoomScript : MonoBehaviour {

    public WallConfig DoorSouth;
    public WallConfig DoorNorth;
    public WallConfig DoorWest;
    public WallConfig DoorEast;

    const int WallHeigth = 10;
    const int WallWidth = 40;
    const int WallDistance = 20;
    const float DoorWidth = 5.0f;
    GameObject Ground;
    ConstraintSpawner spwn;
    Material M;

    bool playerInside;

	// Use this for initialization
	void Start () {
        GenerateStructure();
        StartCoroutine(spwn.GenerateContent());
    }

    void GenerateStructure()
    {
        Ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Ground.transform.position = new Vector3(0, -0.5f, 0) + transform.position;
        Ground.transform.rotation = new Quaternion();
        Ground.transform.localScale = new Vector3(WallWidth, 1, WallWidth);
        Ground.name = "Ground";
        Ground.transform.parent = transform;
        Ground.GetComponent<Renderer>().material = M;

        float WallDoorPos = ((WallWidth / 2) + DoorWidth) / 2;


        Quaternion souteuler = Quaternion.Euler(0, 0, 90);
        if (DoorSouth == WallConfig.Wall)
        {
            AddWall("South", new Vector3(20, 5, 0), souteuler, new Vector3(WallHeigth, 1, WallWidth));
        }
        else if (DoorSouth == WallConfig.Door)
        {
            AddWall("South2", new Vector3(WallDistance, 5, -WallDoorPos), souteuler,
                new Vector3(WallHeigth, 1, (WallWidth / 2) - DoorWidth));
            AddWall("South2", new Vector3(WallDistance, 5, WallDoorPos), souteuler,
                new Vector3(WallHeigth, 1, (WallWidth / 2) - DoorWidth));
        }

        Quaternion northeuler = Quaternion.Euler(0, 0, -90);
        if (DoorNorth == WallConfig.Wall)
        {
            AddWall("North", new Vector3(-WallDistance, 5, 0), northeuler, new Vector3(WallHeigth, 1, WallWidth));
        }
        else if (DoorNorth == WallConfig.Door)
        {
            AddWall("North1", new Vector3(-WallDistance, 5, -WallDoorPos), northeuler, new Vector3(WallHeigth, 1, (WallWidth / 2) - DoorWidth));
            AddWall("North2", new Vector3(-WallDistance, 5, WallDoorPos), northeuler, new Vector3(WallHeigth, 1, (WallWidth / 2) - DoorWidth));

        }

        Quaternion westeuler = Quaternion.Euler(0, 90, 90);
        if (DoorWest == WallConfig.Wall)
        {
            AddWall("West", new Vector3(0, 5, -WallDistance), westeuler, new Vector3(WallHeigth, 1, WallWidth));
        }
        else if (DoorWest == WallConfig.Door)
        {
            AddWall("West1", new Vector3(-WallDoorPos, 5, -WallDistance), westeuler, new Vector3(WallHeigth, 1, (WallWidth / 2) - DoorWidth));
            AddWall("West2", new Vector3(WallDoorPos, 5, -WallDistance), westeuler, new Vector3(WallHeigth, 1, (WallWidth / 2) - DoorWidth));
        }

        Quaternion easteuler = Quaternion.Euler(0, 90, 90);
        if (DoorEast == WallConfig.Wall)
        {
            AddWall("East", new Vector3(0, 5, -WallDistance), easteuler, new Vector3(WallHeigth, 1, WallWidth));
        }
        else if (DoorEast == WallConfig.Door)
        {
            AddWall("East1", new Vector3(-WallDoorPos, 5, WallDistance), easteuler, new Vector3(WallHeigth, 1, (WallWidth / 2) - DoorWidth));
            AddWall("East2", new Vector3(WallDoorPos, 5, WallDistance), easteuler, new Vector3(WallHeigth, 1, (WallWidth / 2) - DoorWidth));
        }
    }

    public void SetRoomDesc(Biom v, GameObject BluePrint, Material M)
    {
        GameObject go = (GameObject)Instantiate(BluePrint);
        go.transform.parent = transform;
        this.M = M;
        this.spwn = go.GetComponent<ConstraintSpawner>();            
        this.spwn.Desc = v;
        this.spwn.transform.position = this.transform.position;
        this.spwn.transform.localScale = this.transform.localScale;
        go.transform.localScale = new Vector3(go.transform.localScale.x*39, go.transform.localScale.y * 10, go.transform.localScale.z*39);

        go.transform.position += new Vector3(0,5,0);
    }

    void AddWall(string Name, Vector3 Pos, Quaternion rot, Vector3 Scale)
    {
        GameObject obj = GameObject.Instantiate(Ground);        
        obj.transform.position = Pos + transform.position;
        obj.transform.rotation = rot;
        obj.transform.localScale = Scale;
        obj.name = Name;
        obj.transform.parent = transform;
        obj.GetComponent<Renderer>().material = M;
    }

    // Update is called once per frame
    void Update () {
        Vector3 size = spwn.transform.localScale;
        Vector3 roompos = spwn.transform.position;
        Bounds b = new Bounds(roompos, size);
        if (b.Contains(Camera.main.transform.position) && playerInside == false)
        {
            playerInside = true;
            onPlayerEnter();
        }
    }

    void onPlayerEnter()
    {
        Debug.Log("Player enter Room :" + spwn.transform.position.x / 40 + " " + spwn.transform.position.z / 40);
    }
}
