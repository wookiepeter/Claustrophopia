using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMetalDoorsScript : MonoBehaviour {
	private bool zoneDoor = false;
	private bool doorOpened = false;
	private bool warten = false;
	private AudioSource metallDoorAudioSource;
    private AudioClip metallDoorOpenAudioClip;
	private AudioClip metallDoorCloseAudioClip;

	// Use this for initialization
	void Start () {
        metallDoorOpenAudioClip = Resources.Load<AudioClip>("Audio/metal_door_open_1");
		metallDoorCloseAudioClip = Resources.Load<AudioClip>("Audio/metal_door_close_1");
        metallDoorAudioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
	}
	
	// Update is called once per frame
	void Update () {
		// Door - Hebel
		if(!warten && !doorOpened && zoneDoor && Input.GetKeyDown(KeyCode.G))
		{
				GetComponent<Animator>().SetTrigger("LargeDoorOpen");
				metallDoorAudioSource.clip = metallDoorOpenAudioClip;
				metallDoorAudioSource.Play();
				StartCoroutine(WaitDoor());
		}
		if(!warten && doorOpened && zoneDoor && Input.GetKeyDown(KeyCode.G))
		{
				GetComponent<Animator>().SetTrigger("LargeDoorClose");
				metallDoorAudioSource.clip = metallDoorCloseAudioClip;
				StartCoroutine(MetallDoorClose());
				StartCoroutine(WaitDoor());
		}
	}
	private void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Player")
		{
			zoneDoor = true;
			GameObject.Find("[Key] - Press G to open the door").GetComponent<UnityEngine.UI.Text>().enabled = true;
		}
	}
	private void OnTriggerExit(Collider other)
    {
		if(other.tag == "Player")
		{
			zoneDoor = false;
			GameObject.Find("[Key] - Press G to open the door").GetComponent<UnityEngine.UI.Text>().enabled = false;
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
	private IEnumerator MetallDoorClose()
	{
		yield return new WaitForSeconds(0.75f);
		metallDoorAudioSource.Play();
	}
}
