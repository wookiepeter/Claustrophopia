using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMetalDoorsScript : MonoBehaviour {
	private bool zoneDoor = false;
	private bool doorOpened = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Door - Hebel
		if(!doorOpened && zoneDoor && Input.GetKeyDown(KeyCode.G))
		{
			GetComponent<Animator>().SetTrigger("LargeDoorOpen");
			doorOpened = true;
		}
	}
	private void OnTriggerEnter(Collider Player)
    {
		zoneDoor = true;
		GameObject.Find("[Key] - Press G to open the door").GetComponent<UnityEngine.UI.Text>().enabled = true;
	}
	private void OnTriggerExit(Collider Player)
    {
		zoneDoor = false;
		GameObject.Find("[Key] - Press G to open the door").GetComponent<UnityEngine.UI.Text>().enabled = false;
	}
}
