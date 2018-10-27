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
	private float timerZone1 = 0;
	private float timerZone2 = 0;
	private float timerZone3 = 0;
	private float timeToDead = 30f;
	// Use this for initialization
	void Start () {
		BlurOptimized = 		GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>();
		MotionBlur = 			GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
		FishEye = 				GetComponent<UnityStandardAssets.ImageEffects.Fisheye>();
		NoiseAndScratches = 	GetComponent<UnityStandardAssets.ImageEffects.NoiseAndScratches>();
		flashLight = 			GameObject.Find("[FlashLight]");
		flashLightObject = 		GameObject.Find("Taschenlampe");
		breathe1 = 				GameObject.Find("Breathe_Zone_1").GetComponent<AudioSource>();
		breathe2 = 				GameObject.Find("Breathe_Zone_2").GetComponent<AudioSource>();
		breathe3 = 				GameObject.Find("Breathe_Zone_3").GetComponent<AudioSource>();
		heart1 = 				GameObject.Find("Heart_beat_1").GetComponent<AudioSource>();
		heart2 = 				GameObject.Find("Heart_beat_2").GetComponent<AudioSource>();
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
			// Effects
			timerZone1 += Time.deltaTime;
			timerZone2 = 0;
			timerZone3 = 0;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speedBlur, 3.33f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speedBlur, 0.33f);
			// Audio
			breathe1.enabled = true;
			breathe2.enabled = false;
			breathe3.enabled = false;
			heart1.enabled = true;
			heart2.enabled = false;
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
			timerZone1 = 0;
			timerZone2 += Time.deltaTime;
			timerZone3 = 0;
			speedFishEye = 0.1f;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speedBlur, 6.66f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speedBlur, 0.66f);
			FishEye.strengthX = Mathf.PingPong(Time.time * speedFishEye, 0.1f);
			FishEye.strengthY = Mathf.PingPong(Time.time * speedFishEye, 0.1f);
			// Audio
			breathe1.enabled = false;
			breathe2.enabled = true;
			breathe3.enabled = false;
			heart1.enabled = false;
			heart2.pitch = 1;
			heart2.enabled = true;
			// Flash light
			flashLight.GetComponent<Light>().intensity = Random.RandomRange(0.5f, 1.0f);
			// Velocity settings
			GetComponent<ExtendFlycam>().climbSpeed = 1.5f;
			GetComponent<ExtendFlycam>().climbSpeed = 3.5f;
			GetComponent<ExtendFlycam>().cameraSensitivity = 75;
		}
		// Zone 3
		if(zone3)
		{
			// Effects
			timerZone1 = 0;
			timerZone2 = 0;
			timerZone3 += Time.deltaTime;
			speedFishEye = 0.5f;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speedBlur, 10f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speedBlur, 0.92f);
			FishEye.strengthX = Mathf.PingPong(Time.time * speedFishEye, 0.3f);
			FishEye.strengthY = Mathf.PingPong(Time.time * speedFishEye, 0.3f);
			// Audio
			breathe1.enabled = false;
			breathe2.enabled = false;
			breathe3.enabled = true;
			heart1.enabled = false;
			heart2.pitch = 2;
			heart2.enabled = true;
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
    }
	private void OnTriggerExit(Collider other)
    {
	}
	private void ClownLaughing()
	{
	}
}
