﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.rotation = Quaternion.identity;
	}

    private void OnTriggerEnter(Collider other)
    {
        other.CompareTag("")
        print("Ouch!");
    }
}
