using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurSetting : MonoBehaviour {
	public GameObject mainCamera;
	public AudioSource breathe1;
	public AudioSource breathe2;
	public AudioSource breathe3;
	public AudioSource heart1;
	public AudioSource heart2;
	private UnityStandardAssets.ImageEffects.BlurOptimized BlurOptimized;
	private UnityStandardAssets.ImageEffects.MotionBlur MotionBlur;
	private UnityStandardAssets.ImageEffects.Fisheye FishEye;
	private float speed = 1f;
	private float speedFishEye = 0.75f;
	public bool zone1 = false;
	public bool zone2 = false;
	public bool zone3 = false;
	private bool zoneKey = false;
	private bool zoneDoor = false;
	private bool keyInHand = false;
	private GameObject key;
	private float timerZone1 = 0;
	private float timerZone2 = 0;
	private float timerZone3 = 0;
	// Use this for initialization
	void Start () {
		BlurOptimized = mainCamera.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>();
		MotionBlur = mainCamera.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
		FishEye = mainCamera.GetComponent<UnityStandardAssets.ImageEffects.Fisheye>();
	}
	
	// Update is called once per frame
	void Update () {
		mainCamera.GetComponent<Rigidbody>().velocity = Vector3.zero;
		// Zone 1
		if(zone1)
		{
			timerZone1 += Time.deltaTime;
			timerZone2 = 0;
			timerZone3 = 0;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speed, 3.33f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speed, 0.33f);
			breathe1.enabled = true;
			breathe2.enabled = false;
			breathe3.enabled = false;
			heart1.enabled = true;
			heart2.enabled = false;
		}
		// Zone 2
		if(zone2)
		{
			timerZone1 = 0;
			timerZone2 += Time.deltaTime;
			timerZone3 = 0;
			speedFishEye = 0.1f;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speed, 6.66f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speed, 0.66f);
			FishEye.strengthX = Mathf.PingPong(Time.time * speedFishEye, 0.1f);
			FishEye.strengthY = Mathf.PingPong(Time.time * speedFishEye, 0.1f);
			breathe1.enabled = false;
			breathe2.enabled = true;
			breathe3.enabled = false;
			heart1.enabled = false;
			heart2.pitch = 1;
			heart2.enabled = true;
		}
		// Zone 3
		if(zone3)
		{
			timerZone1 = 0;
			timerZone2 = 0;
			timerZone3 += Time.deltaTime;
			speedFishEye = 0.5f;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speed, 10f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speed, 0.92f);
			FishEye.strengthX = Mathf.PingPong(Time.time * speedFishEye, 0.3f);
			FishEye.strengthY = Mathf.PingPong(Time.time * speedFishEye, 0.3f);
			breathe1.enabled = false;
			breathe2.enabled = false;
			breathe3.enabled = true;
			heart1.enabled = false;
			heart2.pitch = 2;
			heart2.enabled = true;
		}
		// Game over
		if(timerZone3 > 10) Debug.Log("Game over");
		// Key
		if(!keyInHand && zoneKey && Input.GetKeyDown(KeyCode.G)) 
		{
			GameObject.Find("[Key] - Press G to put the key").GetComponent<UnityEngine.UI.Text>().enabled = false;
			zoneKey = false;
			keyInHand = true;
			key.SetActive(false);
			key = null;
		}
		if(keyInHand && zoneDoor && Input.GetKeyDown(KeyCode.G)) 
		{
			GameObject.Find("[Key] - Press G to open the door").GetComponent<UnityEngine.UI.Text>().enabled = false;
			GameObject.Find("[Door] - Animator").GetComponent<Animator>().SetTrigger("CloseDoor");
			zoneDoor = false;
			keyInHand = false;
		}
	}
	private void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Zone_1")
		{
			zone1 = true;
			zone2 = false;
			zone3 = false;
		}
		if(other.tag == "Zone_2")
		{
			zone1 = false;
			zone2 = true;
			zone3 = false;
		}
		if(other.tag == "Zone_3")
		{
			zone1 = false;
			zone2 = false;
			zone3 = true;
		}
		if(other.tag == "Key")
		{
			zoneKey = true;
			GameObject.Find("[Key] - Press G to put the key").GetComponent<UnityEngine.UI.Text>().enabled = true;
			key = other.gameObject;
		}
		if(other.tag == "Door")
		{
			zoneDoor = true;
			GameObject.Find("[Key] - Press G to open the door").GetComponent<UnityEngine.UI.Text>().enabled = true;
		}
    }
	private void OnTriggerExit(Collider other)
    {
		if(other.tag == "Key")
		{
			zoneKey = false;
			GameObject.Find("[Key] - Press G to put the key").GetComponent<UnityEngine.UI.Text>().enabled = false;
			key = null;
		}
		if(other.tag == "Door")
		{
			zoneDoor = false;
			GameObject.Find("[Key] - Press G to open the door").GetComponent<UnityEngine.UI.Text>().enabled = false;
		}
	}
}
