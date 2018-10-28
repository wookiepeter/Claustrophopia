using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMetalDoorsScript : MonoBehaviour {
	private bool zoneDoor = false;
	private bool doorOpened = false;
	private bool warten = false;
	public bool doorLock = false;
	private AudioSource metallDoorAudioSource;
    private AudioClip metallDoorOpenAudioClip;
	private AudioClip metallDoorCloseAudioClip;

	// Use this for initialization
	void Start () {
		gameObject.transform.Find("TextMeshPro").GetComponent<MeshRenderer>().enabled = false;
        metallDoorOpenAudioClip = Resources.Load<AudioClip>("Audio/metal_door_open_1");
		metallDoorCloseAudioClip = Resources.Load<AudioClip>("Audio/metal_door_close_1");
        metallDoorAudioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
	}
	
	// Update is called once per frame
	void Update () {
		// Door - Hebel
		if(!doorLock && !warten && !doorOpened && zoneDoor && Input.GetKeyDown(KeyCode.G))
		{
				GetComponent<Animator>().SetTrigger("LargeDoorOpen");
				metallDoorAudioSource.clip = metallDoorOpenAudioClip;
				metallDoorAudioSource.Play();
				StartCoroutine(WaitDoor());
		}
		if(!doorLock && !warten && doorOpened && zoneDoor && Input.GetKeyDown(KeyCode.G))
		{
				GetComponent<Animator>().SetTrigger("LargeDoorClose");
				metallDoorAudioSource.clip = metallDoorCloseAudioClip;
				metallDoorAudioSource.Play();
				gameObject.transform.Find("TextMeshPro").GetComponent<MeshRenderer>().enabled = false;
				StartCoroutine(WaitDoor());
		}
	}
	private void OnTriggerEnter(Collider other)
    {
		if(!doorLock && other.tag == "Player")
		{
			zoneDoor = true;
			if(!doorOpened) gameObject.transform.Find("TextMeshPro").GetComponent<MeshRenderer>().enabled = true;
		}
	}
	private void OnTriggerExit(Collider other)
    {
		if(!doorLock && other.tag == "Player")
		{
			zoneDoor = false;
			gameObject.transform.Find("TextMeshPro").GetComponent<MeshRenderer>().enabled = false;
		}
	}
	private IEnumerator WaitDoor()
	{
		warten = true;
		yield return new WaitForSeconds(1f);
		warten = false;
		if(!doorOpened) 
		{
			doorOpened = true;
		} else
		{
			doorOpened = false;
		}
	}
}
