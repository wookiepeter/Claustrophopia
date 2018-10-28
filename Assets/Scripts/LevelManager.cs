using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] querReihen;

    [SerializeField]
    Vector3 UnitTile = new Vector3(3.67f, 0, 3.67f);

    [SerializeField]
    int squareSize = 12;

    [SerializeField]
    GameObject[,] map;

    string[] asciiMap;

    // Use this for initialization
    void Start()
    {
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
        BuildAsciiMap();
        CheckMapArray();
    }

    void CheckMapArray()
    {
        print("checking Map array");
        print(map[0, 0].gameObject.name);
        print(map[11, 11].gameObject.name);
    }

    void BuildAsciiMap()
    {
        // x --> 
        //   ^
        // z |
        asciiMap = new string[] {
            "############",
            "#00000######",
            "#0###000000#",
            "#0###0####0#",
            "#0###0####0#",
            "#00000##000#",
            "00###0##0000",
            "0####000000#",
            "0####0###0##",
            "000000###0##",
            "####0####0##",
            "####000000##" };

        GenerateGameTarget();

        string arrayString = "";
        foreach (string s in asciiMap)
        {
            arrayString += s + "\n";
        }


        Debug.Log(arrayString);
    }

    void GenerateGameTarget()
    {
        Vector2Int targetPos;
        while (true)
        {
            Vector2Int pos = new Vector2Int(Random.Range(0, 12), Random.Range(0, 12));
            if (GetType(pos) == '0')
            {
                Vector2Int neighbor = new Vector2Int(Random.Range(0, 2), Random.Range(0, 2));
                if (GetType(pos + neighbor) == '#')
                {
                    targetPos = pos + neighbor;
                    break;
                }
            }
        }
        SetType(targetPos, 't');
        print("target is now " + GetType(targetPos));
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
