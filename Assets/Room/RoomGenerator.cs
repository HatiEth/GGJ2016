using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class RoomGenerator : MonoBehaviour {

    public int size;
    public int RoomSize;
    public GameObject Room;
    public List<RoomDescription> Biomes;
    List<int> RoomDescs;

    int[,] Rooms;
	// Use this for initialization
    int Get(int X, int Y)
    {
        if (X < 0 || X >= Rooms.GetLength(0)) return 0;
        if (Y < 0 || Y >= Rooms.GetLength(1)) return 0;
        return Rooms[X, Y];
    }
    void Set(int X, int Y, int val)
    {
        if (X < 0 || X >= Rooms.GetLength(0)) return;
        if (Y < 0 || Y >= Rooms.GetLength(1)) return;
        Rooms[X, Y]  = val;
    }

    void Start () {
        GenerateRoomArray();
        SpawnRooms();
    }
	
    void SpawnRooms()
    {

        for (int x = 0; x < Rooms.GetLength(0); x++)
            for (int y = 0; y < Rooms.GetLength(0); y++)
            {
                GameObject o = GameObject.Instantiate(Room,new Vector3(x*40,0,y*40),new Quaternion()) as GameObject;
                RoomScript scr = o.GetComponent<RoomScript>();
                scr.DoorEast = WallConfig.None;
                scr.DoorSouth = WallConfig.None;

                WallConfig north = (Get(x, y + 1) != Get(x, y)) ? WallConfig.Door : WallConfig.None;
                WallConfig west = (Get(x + 1, y) != Get(x, y)) ? WallConfig.Door : WallConfig.None;
                if (north == WallConfig.Door && Random.value < 0.5f) north = WallConfig.Wall;
                if (west == WallConfig.Door && Random.value < 0.5f) west = WallConfig.Wall;

                scr.DoorNorth = north;
                scr.DoorWest = west;
                scr.SetRoomDesc(Biomes[RoomDescs[Rooms[x,y]]]);
                
            }
    }

    void GenerateRoomArray()
    {
        Rooms = new int[size, size];

        List<IntPoint> Seeds = new List<IntPoint>();
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
            {
                Seeds.Add(new IntPoint(x, y));
            }
        Shuffle(ref Seeds);

        int Counter = 1;

        for (int i = 0; i < Seeds.Count; i++)
        {
            int x = Seeds[i].X;
            int y = Seeds[i].Y;
            if (Get(x, y) == 0)
            {
                Set(x, y, Counter);

                for (int j = 0; j < RoomSize; j++)
                {
                    int movedir = (int)(Random.value * 4);
                    int diffx = 0;
                    int diffy = 0;
                    if (movedir == 0) diffx += 1;
                    if (movedir == 1) diffy += 1;
                    if (movedir == 2) diffx -= 1;
                    if (movedir == 3) diffy -= 1;

                    if (Get(x + diffx, y + diffy) == 0)
                    {
                        x += diffx;
                        y += diffy;
                        Set(x, y, Counter);
                    }
                }
                Counter++;
            }
        }

        RoomDescs = new List<int>();
        for(int i = 0;i < RoomDescs.Count;i++)
        {
            int nr = (int)(Random.value * Biomes.Count);
            RoomDescs.Add(nr);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}

    private System.Random rng;

    void Shuffle(ref List<IntPoint> list)
    {
        rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            IntPoint value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    struct IntPoint
    {
        public int X;
        public int Y;
        public IntPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}

