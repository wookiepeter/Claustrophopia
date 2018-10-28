using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject[] querReihen;

    [SerializeField]
    Vector3 UnitTile = new Vector3(3.67f, 0, -3.67f);

    [SerializeField]
    Vector3 StartingPoint = new Vector3(0, 0, 0);

    [SerializeField]
    int squareSize = 12;

    [SerializeField]
    GameObject[,] map;

    string[] asciiMap;

    [SerializeField]
    PetFollower GhostPrefab;

    [SerializeField]
    GameObject GummyBear;

    [SerializeField]
    GameObject examSolutions;

    [SerializeField]
    GameObject darlek;

    // Use this for initialization
    void Start()
    {
        // StartingPoint = transform.position;        
        BuildMapArray();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("ComputeMap")]
    void BuildMapArray()
    {
        map = new GameObject[squareSize, squareSize];
        for (int x = 0; x < querReihen.Length; x++)
        {
            Vector3 oldPosition = Vector3.zero;
            for (int z = 0; z < 12; z++)
            {
                GameObject go = querReihen[x].transform.GetChild(z).gameObject;
                map[x, z] = go;
                if (z != 0 && (Vector3.Distance(oldPosition, go.transform.position) > UnitTile.x + 0.5f || Vector3.Distance(oldPosition, go.transform.position) < UnitTile.x - 0.5f))
                {
                    Debug.LogWarning("Warning in LevelManager: Segmenterror at: " + go.name + " in Row " + querReihen[x].name);
                }
                oldPosition = go.transform.position;
            }
        }
        BuildAsciiMap(1);
        CheckMapArray();
    }

    void CheckMapArray()
    {
        print("checking Map array");
        print(map[0, 0].gameObject.name);
        print(map[11, 11].gameObject.name);
    }

    void BuildAsciiMap(int level)
    {
        // x --> 
        //   ^
        // z |

        // pet p
        // darlek d
        // gummibär g
        // examslösungen e
        // ausgang a
        asciiMap = new string[12];
        switch (level)
        {
            case 1:
                asciiMap = new string[] {
                    "1p1111111111",
                    "100000111111",
                    "101e1000000g",
                    "001110111101",
                    "001110111101",
                    "000000110001",
                    "00111011000a",
                    "01111000000#",
                    "011110000011",
                    "000000#11011",
                    "11110111101g",
                    "g111000000p1" };
                break;
            case 2:

                break;
            case 3:

                break;
        }

        GenerateGameTargets();

        string arrayString = "";
        foreach (string s in asciiMap)
        {
            arrayString += s + "\n";
        }


        Debug.Log(arrayString);
    }

    void GenerateGameTargets()
    {
        for (int x = 0; x < 12; x++)
        {
            for(int z = 0; z < 12; z++)
            {
                char c = GetType(x, z);
                switch (c)
                {
                    case 'p':
                        PetFollower p = Instantiate(GhostPrefab, GetTileCenter(new Vector2Int(x, z)), Quaternion.identity, transform);
                        p.Player = player;
                        break;
                    case 'a':

                        break;
                    case 'd':

                        break;
                    case 'e':

                        break;
                }
            }
        }
    }

    Vector3 GetTileCenter(Vector2Int pos)
    {
        print("Position for pos: " + pos + " is " + StartingPoint + " + " + new Vector3(UnitTile.x * pos.x, 0, UnitTile.z * pos.y) + " = " + (StartingPoint + new Vector3(UnitTile.x * pos.x, 0, UnitTile.z * pos.y)));
        return transform.position + StartingPoint + new Vector3(UnitTile.x * pos.x, 0, UnitTile.z * pos.y);
    }

    char GetType(int x, int z)
    {
        if (x < 0 || x > 11 || z < 0 || z > 11)
            return '!';
        // print("found " + asciiMap[11 - x][11 - z] + " at position " + x + ", " + z);
        return asciiMap[11 - z][11 - x];
    }
    char GetType(Vector2Int pos)
    {
        return GetType(pos.x, pos.y);
    }

    void SetType(int x, int z, char c)
    {
        print("setting char " + c + " at position " + x + ", " + z);
        asciiMap[11-z] = asciiMap[11 - z].Insert(11 - x, c.ToString());
        asciiMap[11-z] = asciiMap[11 - z].Remove(11 - x + 1, 1);
    }

    void SetType(Vector2Int pos, char c)
    {
        SetType(pos.x, pos.y, c);
    }

}
