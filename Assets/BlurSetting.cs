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
	public float speed = 1f;
	public float speedFishEye = 0.75f;
	public bool zone1 = false;
	public bool zone2 = false;
	public bool zone3 = false;
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
}
