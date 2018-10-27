using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharakterSetting : MonoBehaviour {
	public GameObject mainCamera;
	public bool allEffectsOff = false;
	private GameObject flashLight;
	private GameObject flashLightObject;
	public AudioSource breathe1;
	public AudioSource breathe2;
	public AudioSource breathe3;
	public AudioSource heart1;
	public AudioSource heart2;
	public AudioSource clown1;
	private UnityStandardAssets.ImageEffects.BlurOptimized BlurOptimized;
	private UnityStandardAssets.ImageEffects.MotionBlur MotionBlur;
	private UnityStandardAssets.ImageEffects.Fisheye FishEye;
	private UnityStandardAssets.ImageEffects.NoiseAndScratches NoiseAndScratches;
	private float speed = 1f;
	private float speedFishEye = 0.75f;
	private float speedFlashLight = 0.1f;
	public bool zone1 = false;
	public bool zone2 = false;
	public bool zone3 = false;
	private bool zoneKey = false;
	private bool zoneDoor = false;
	private bool zoneLever = false;
	private bool keyInHand = false;
	private GameObject key;
	private bool doorOpened = false;
	private bool leverPulled = false;
	private bool leverRaised = false;
	private float timerZone1 = 0;
	private float timerZone2 = 0;
	private float timerZone3 = 0;
	private float timeToDead = 30f;
	// Use this for initialization
	void Start () {
		BlurOptimized = mainCamera.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>();
		MotionBlur = mainCamera.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
		FishEye = mainCamera.GetComponent<UnityStandardAssets.ImageEffects.Fisheye>();
		NoiseAndScratches = mainCamera.GetComponent<UnityStandardAssets.ImageEffects.NoiseAndScratches>();
		flashLight = GameObject.Find("[FlashLight]");
		flashLightObject = GameObject.Find("Taschenlampe");
	}
	
	// Update is called once per frame
	void Update () {
		// Noise
		timeToDead = Time.time / 100;
		NoiseAndScratches.grainIntensityMin = Mathf.Lerp(0, 0.5f, timeToDead);
		NoiseAndScratches.grainIntensityMax = Mathf.Lerp(0, 0.5f, timeToDead);
		// Flashlight 
		flashLight.transform.localRotation = new Quaternion (flashLight.transform.localRotation.x,
			Random.RandomRange(0.01f, 0.02f),
			flashLight.transform.localRotation.z,
			flashLight.transform.localRotation.w);
		flashLight.transform.localPosition = new Vector3 (Mathf.PingPong(Time.time * speedFlashLight, 0.1f),
			flashLight.transform.localPosition.y,
			flashLight.transform.localPosition.z);
		flashLightObject.transform.localPosition = new Vector3 (Mathf.PingPong(Time.time * speedFlashLight, 0.1f),
			flashLightObject.transform.localPosition.y,
			flashLightObject.transform.localPosition.z);
		// Velocity Off
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
			flashLight.GetComponent<Light>().intensity = 1;
			// Velocity settings
			GetComponent<ExtendFlycam>().climbSpeed = 2;
			GetComponent<ExtendFlycam>().climbSpeed = 5;
			GetComponent<ExtendFlycam>().cameraSensitivity = 90;
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
			flashLight.GetComponent<Light>().intensity = Random.RandomRange(0.5f, 1.0f);
			// Velocity settings
			GetComponent<ExtendFlycam>().climbSpeed = 1.5f;
			GetComponent<ExtendFlycam>().climbSpeed = 3.5f;
			GetComponent<ExtendFlycam>().cameraSensitivity = 75;
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
			flashLight.GetComponent<Light>().intensity = Random.RandomRange(0.0f, 1.0f);
			// Velocity settings
			GetComponent<ExtendFlycam>().climbSpeed = 1;
			GetComponent<ExtendFlycam>().climbSpeed = 2.5f;
			GetComponent<ExtendFlycam>().cameraSensitivity = 60;
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
			GameObject.Find("[Door] - Animator").GetComponent<Animator>().SetTrigger("OpenDoor");
			keyInHand = false;
			doorOpened = true;
			clown1.enabled = true;
			GameObject.Find("Clown").GetComponent<Animator>().SetTrigger("ClownMove");
		}
		if(doorOpened && zoneDoor && Input.GetKeyDown(KeyCode.G))
		{
			if(GameObject.Find("[Door] - Animator").transform.localPosition == new Vector3(-2.69f,
				GameObject.Find("[Door] - Animator").transform.localPosition.y,
				GameObject.Find("[Door] - Animator").transform.localPosition.z))
			{
				GameObject.Find("[Door] - Animator").GetComponent<Animator>().SetTrigger("CloseDoor");
				doorOpened = false;
			}
		}
		if(!leverPulled && zoneLever && Input.GetKeyDown(KeyCode.G))
		{
			GameObject.Find("[Hebel]").GetComponent<Animator>().SetTrigger("LeverPull");
			leverPulled = true;
			leverRaised = false;
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
		if(other.tag == "Lever")
		{
			zoneLever = true;
			//GameObject.Find("[Key] - Press G to put the key").GetComponent<UnityEngine.UI.Text>().enabled = true;
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
		if(other.tag == "Lever")
		{
			zoneLever = false;
			//GameObject.Find("[Key] - Press G to put the key").GetComponent<UnityEngine.UI.Text>().enabled = false;
		}
	}
	private void ClownLaughing()
	{

	}
}
