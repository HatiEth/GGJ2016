using UnityEngine;
using System.Collections;
using System;

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
    RoomDescription Desc;

	// Use this for initialization
	void Start () {
        GenerateStructure();
        
        
    }
    
    void GenerateContent()
    {

    }

    void GenerateStructure()
    {
        Ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Ground.transform.position = new Vector3(0, 0, 0) + transform.position;
        Ground.transform.rotation = new Quaternion();
        Ground.transform.localScale = new Vector3(WallWidth, 1, WallWidth);
        Ground.name = "Ground";
        Ground.transform.parent = transform;

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

    public void SetRoomDesc(RoomDescription v)
    {
        Desc = v;
        GenerateContent();
    }

    void AddWall(string Name, Vector3 Pos, Quaternion rot, Vector3 Scale)
    {
        GameObject obj = GameObject.Instantiate(Ground);
        obj.transform.position = Pos + transform.position;
        obj.transform.rotation = rot;
        obj.transform.localScale = Scale;
        obj.name = Name;
        obj.transform.parent = transform;
    }

    // Update is called once per frame
    void Update () {
	
	}

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector3 siz = transform.localScale * 2 * 20;
    //    siz = new Vector3(siz.x,siz.y - 600.0f,siz.z);
    //    Gizmos.DrawWireCube(transform.position, siz);
    //}
}
