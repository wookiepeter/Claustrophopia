using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharakterSetting : MonoBehaviour {
	public GameObject mainCamera;
	private GameObject flashLight;
	private GameObject flashLightObject;
	private AudioSource breathe1AudioSource;
	private AudioSource breathe2AudioSource;
	private AudioSource breathe3AudioSource;
	private AudioSource heart1AudioSource;
	private AudioSource heart2AudioSource;
	private UnityStandardAssets.ImageEffects.BlurOptimized BlurOptimized;
	private UnityStandardAssets.ImageEffects.MotionBlur MotionBlur;
	private UnityStandardAssets.ImageEffects.Fisheye FishEye;
	private UnityStandardAssets.ImageEffects.NoiseAndScratches NoiseAndScratches;
	private float speedBlur = 1f;
	private float speedFishEye = 0.75f;
	private float speedFlashLight = 0.1f;
	public bool zone1 = false;
	public bool zone2 = false;
	public bool zone3 = false;
	public bool zone4 = false;
	private float timerZone1 = 0;
	private float timerZone2 = 0;
	private float timerZone3 = 0;
	private float timerZone4 = 0;
	private float timeToDead = 30f;
	// Use this for initialization
	void Start () {
		BlurOptimized = 		GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>();
		MotionBlur = 			GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
		FishEye = 				GetComponent<UnityStandardAssets.ImageEffects.Fisheye>();
		NoiseAndScratches = 	GetComponent<UnityStandardAssets.ImageEffects.NoiseAndScratches>();
		flashLight = 			GameObject.Find("[FlashLight]");
		flashLightObject = 		GameObject.Find("Taschenlampe");
		breathe1AudioSource = 	GameObject.Find("[Audio]/Breathe_Zone_1").GetComponent<AudioSource>();
		breathe2AudioSource = 	GameObject.Find("[Audio]/Breathe_Zone_2").GetComponent<AudioSource>();
		breathe3AudioSource = 	GameObject.Find("[Audio]/Breathe_Zone_3").GetComponent<AudioSource>();
		heart1AudioSource = 	GameObject.Find("[Audio]/Heart_beat_1").GetComponent<AudioSource>();
		heart2AudioSource = 	GameObject.Find("[Audio]/Heart_beat_2").GetComponent<AudioSource>();
	}
	// Update is called once per frame
	void Update () {

		// Noise (time to dead)
		timeToDead = Time.time / 1000;
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
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		// Zone 1
		if(zone1)
		{
			BlurOptimized.enabled = false;
			MotionBlur.enabled = false;
			FishEye.enabled = false;
			// Audio
			breathe1AudioSource.enabled = true;
			breathe2AudioSource.enabled = false;
			breathe3AudioSource.enabled = false;
			heart1AudioSource.enabled = true;
			heart2AudioSource.enabled = false;
			// Flash light
			flashLight.GetComponent<Light>().intensity = 1;
			// Velocity settings
			GetComponent<ExtendFlycam>().climbSpeed = 2;
			GetComponent<ExtendFlycam>().climbSpeed = 5;
			GetComponent<ExtendFlycam>().cameraSensitivity = 90;
		}
		// Zone 2
		if(zone2)
		{
			// Effects
			timerZone1 += Time.deltaTime;
			timerZone2 = 0;
			timerZone3 = 0;
			timerZone4 = 0;
			BlurOptimized.enabled = true;
			MotionBlur.enabled = true;
			FishEye.enabled = false;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speedBlur, 3.33f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speedBlur, 0.33f);
			// Audio
			breathe1AudioSource.enabled = true;
			breathe2AudioSource.enabled = false;
			breathe3AudioSource.enabled = false;
			heart1AudioSource.enabled = true;
			heart2AudioSource.enabled = false;
			// Flash light
			flashLight.GetComponent<Light>().intensity = 1;
			// Velocity settings
			GetComponent<ExtendFlycam>().climbSpeed = 2;
			GetComponent<ExtendFlycam>().climbSpeed = 5;
			GetComponent<ExtendFlycam>().cameraSensitivity = 90;
		}
		// Zone 2
		if(zone3)
		{
			// Effects
			timerZone1 = 0;
			timerZone2 += Time.deltaTime;
			timerZone3 = 0;
			speedFishEye = 0.1f;
			BlurOptimized.enabled = true;
			MotionBlur.enabled = true;
			FishEye.enabled = true;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speedBlur, 6.66f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speedBlur, 0.66f);
			FishEye.strengthX = Mathf.PingPong(Time.time * speedFishEye, 0.1f);
			FishEye.strengthY = Mathf.PingPong(Time.time * speedFishEye, 0.1f);
			// Audio
			breathe1AudioSource.enabled = false;
			breathe2AudioSource.enabled = true;
			breathe3AudioSource.enabled = false;
			heart1AudioSource.enabled = false;
			heart2AudioSource.pitch = 1;
			heart2AudioSource.enabled = true;
			// Flash light
			flashLight.GetComponent<Light>().intensity = Random.RandomRange(0.5f, 1.0f);
			// Velocity settings
			GetComponent<ExtendFlycam>().climbSpeed = 1.5f;
			GetComponent<ExtendFlycam>().climbSpeed = 3.5f;
			GetComponent<ExtendFlycam>().cameraSensitivity = 75;
		}
		// Zone 3
		if(zone4)
		{
			// Effects
			timerZone1 = 0;
			timerZone2 = 0;
			timerZone3 += Time.deltaTime;
			speedFishEye = 0.5f;
			BlurOptimized.enabled = true;
			MotionBlur.enabled = true;
			FishEye.enabled = true;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speedBlur, 10f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speedBlur, 0.92f);
			FishEye.strengthX = Mathf.PingPong(Time.time * speedFishEye, 0.3f);
			FishEye.strengthY = Mathf.PingPong(Time.time * speedFishEye, 0.3f);
			// Audio
			breathe1AudioSource.enabled = false;
			breathe2AudioSource.enabled = false;
			breathe3AudioSource.enabled = true;
			heart1AudioSource.enabled = false;
			heart2AudioSource.pitch = 2;
			heart2AudioSource.enabled = true;
			// Flash light
			flashLight.GetComponent<Light>().intensity = Random.RandomRange(0.0f, 1.0f);
			// Velocity settings
			GetComponent<ExtendFlycam>().climbSpeed = 1;
			GetComponent<ExtendFlycam>().climbSpeed = 2.5f;
			GetComponent<ExtendFlycam>().cameraSensitivity = 60;
		}
	}
	private void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Zone_1")
		{
			zone1 = true;
			zone2 = false;
			zone3 = false;
			zone4 = false;
		}
		if(other.tag == "Zone_2")
		{
			zone1 = false;
			zone2 = true;
			zone3 = false;
			zone4 = false;
		}
		if(other.tag == "Zone_3")
		{
			zone1 = false;
			zone2 = false;
			zone3 = true;
			zone4 = false;
		}
		if(other.tag == "Zone_4")
		{
			zone1 = false;
			zone2 = false;
			zone3 = false;
			zone4 = true;
		}
    }
	private void OnTriggerExit(Collider other)
    {
		if(other.tag == "Zone_1")
		{
			zone1 = false;
		}
		if(other.tag == "Zone_2")
		{
			zone2 = false;
		}
		if(other.tag == "Zone_3")
		{
			zone3 = false;
		}
		if(other.tag == "Zone_4")
		{
			zone4 = false;
		}
	}
}
