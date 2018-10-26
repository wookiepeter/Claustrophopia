using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurSetting : MonoBehaviour {
	public GameObject mainCamera;
	public AudioSource breathe1;
	public AudioSource breathe2;
	public AudioSource breathe3;
	private UnityStandardAssets.ImageEffects.BlurOptimized BlurOptimized;
	private UnityStandardAssets.ImageEffects.MotionBlur MotionBlur;
	private UnityStandardAssets.ImageEffects.Fisheye FishEye;
	public float speed = 1f;
	public float speedFishEye = 0.75f;
	public bool zone1 = false;
	public bool zone2 = false;
	public bool zone3 = false;
	// Use this for initialization
	void Start () {
		BlurOptimized = mainCamera.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>();
		MotionBlur = mainCamera.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
		FishEye = mainCamera.GetComponent<UnityStandardAssets.ImageEffects.Fisheye>();
	}
	
	// Update is called once per frame
	void Update () {
		// Zone 1
		if(zone1)
		{
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speed, 3.33f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speed, 0.33f);
			breathe1.enabled = true;
			breathe2.enabled = false;
			breathe3.enabled = false;
		}
		// Zone 2
		if(zone2)
		{
			speedFishEye = 0.1f;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speed, 6.66f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speed, 0.66f);
			FishEye.strengthX = Mathf.PingPong(Time.time * speedFishEye, 0.1f);
			FishEye.strengthY = Mathf.PingPong(Time.time * speedFishEye, 0.1f);
			breathe1.enabled = false;
			breathe2.enabled = true;
			breathe3.enabled = false;
		}
		// Zone 3
		if(zone3)
		{
			speedFishEye = 0.5f;
			BlurOptimized.blurSize = Mathf.PingPong(Time.time * speed, 10f);
			MotionBlur.blurAmount = Mathf.PingPong(Time.time * speed, 0.92f);
			FishEye.strengthX = Mathf.PingPong(Time.time * speedFishEye, 0.3f);
			FishEye.strengthY = Mathf.PingPong(Time.time * speedFishEye, 0.3f);
			breathe1.enabled = false;
			breathe2.enabled = false;
			breathe3.enabled = true;
		}
	}
}
