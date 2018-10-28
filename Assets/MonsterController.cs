using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
	public bool start;
	public Animator monster;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
			start = true;
			if(start) monster.SetBool("Start", true);
		}
	}
	private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
			start = false;
			if(!start) monster.SetBool("Start", false);
		}
	}
}
