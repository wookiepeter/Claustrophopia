using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laberinth : MonoBehaviour {

     int SpawnSeite;
    public GameObject LevelSpawner;
    GameObject LevelKachel;
	// Use this for initialization
	void Start () {
		
        for (int i = 0; i < 10; i++)
        {
            SpawnSeite = Random.Range(0, 3);

            if (SpawnSeite == 0)
            {
                LevelSpawner.transform.position = LevelSpawner.transform.position + new Vector3(3.67f, 0, 0);
            }
            if (SpawnSeite == 1)
            {
                LevelSpawner.transform.position = LevelSpawner.transform.position + new Vector3(0, 0,-3.67f);
            }
            if (SpawnSeite == 2)
            {
                LevelSpawner.transform.position = LevelSpawner.transform.position + new Vector3(-3.67f, 0, 0);
            }
            if (SpawnSeite == 3)
            {
                LevelSpawner.transform.position = LevelSpawner.transform.position + new Vector3(0, 0, 3.67f);
            }
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
