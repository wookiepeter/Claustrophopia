using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField]
    GameObject[] querReihen;

    [SerializeField]
    Vector3 UnitTile = new Vector3(3.67f,0, 3.67f);

    [SerializeField]
    int squareSize = 12;

    [SerializeField]
    GameObject[,] map;

	// Use this for initialization
	void Start () {
        BuildMapArray();
	}
	
	// Update is called once per frame
	void Update () {
		
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

        CheckMapArray();
    }

    void CheckMapArray()
    {
        print("checking Map array");
        print(map[0, 0].gameObject.name);
        print(map[11, 11].gameObject.name);
    }
}
