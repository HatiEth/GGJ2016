using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class RoomGenerator : MonoBehaviour {

    public int size;
    public int RoomSize;
    public GameObject Room;
    public List<Biom> Biomes;
    List<int> RoomDescs;
    public GameObject ConstraintSpawnerBluePrint;

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
        int Midx = 0;
        int Midy = 0;

        for (int x = 0; x < Rooms.GetLength(0); x++)
            for (int y = 0; y < Rooms.GetLength(0); y++)
            {
                GameObject o = GameObject.Instantiate(Room,new Vector3( (0.5f + x - Rooms.GetLength(0)/2.0f)*40.0f, 0, 40 * (0.5f + y-Rooms.GetLength(0) / 2.0f)), new Quaternion()) as GameObject;
                RoomScript scr = o.GetComponent<RoomScript>();
                scr.DoorEast = WallConfig.None;
                scr.DoorSouth = WallConfig.None;

                WallConfig north = (Get(x, y + 1) != Get(x, y)) ? WallConfig.Door : WallConfig.None;
                WallConfig west = (Get(x + 1, y) != Get(x, y)) ? WallConfig.Door : WallConfig.None;

                if (Rooms.GetLength(0) != 5) throw new System.Exception("Achtung. Hardgecodete stelle! Faulheit");
                int s = Rooms.GetLength(0) / 2;
                if (!(x==s && y ==s))
                {
                    if (!(x-1==s && y==s)) 
                    if (north == WallConfig.Door && Random.value < 0.5f) north = WallConfig.Wall;
                    if (!(x == s && y-1 == s))
                    if (west == WallConfig.Door && Random.value < 0.5f) west = WallConfig.Wall;
                }

                scr.DoorNorth = north;
                scr.DoorWest = west;
                if ((Rooms.GetLength(0) / 2) == x && (Rooms.GetLength(0) / 2) == y)
                {
                    scr.SetRoomDesc(Biomes[0], ConstraintSpawnerBluePrint, Biomes[0].Clr);
                }
                else
                {
                    scr.SetRoomDesc(Biomes[RoomDescs[Rooms[x, y]]], ConstraintSpawnerBluePrint,Biomes[RoomDescs[Rooms[x, y]]].Clr);

                }
                
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
        for(int i = 0;i < Counter; i++)
        {
            int nr = (int)(Random.value * (Biomes.Count-1)) +1;
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

