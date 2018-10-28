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
    private float speedFlashLight = 0.1f;
    public bool zone1 = false;
    public bool zone2 = false;
    public bool zone3 = false;
    public bool zone4 = false;
	private float timerStress = 0;
	private int stressFaktor = 0;
	private int maxStressFaktor = 1000;
    // Use this for initialization
    void Start () {
        BlurOptimized =         GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>();
        MotionBlur =            GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
        FishEye =               GetComponent<UnityStandardAssets.ImageEffects.Fisheye>();
        NoiseAndScratches =     GetComponent<UnityStandardAssets.ImageEffects.NoiseAndScratches>();
        flashLight =            GameObject.Find("[FlashLight]");
        flashLightObject =      GameObject.Find("Taschenlampe");
        breathe1AudioSource =   GameObject.Find("[Audio]/Breathe_Zone_1").GetComponent<AudioSource>();
        breathe2AudioSource =   GameObject.Find("[Audio]/Breathe_Zone_2").GetComponent<AudioSource>();
        breathe3AudioSource =   GameObject.Find("[Audio]/Breathe_Zone_3").GetComponent<AudioSource>();
        heart1AudioSource =     GameObject.Find("[Audio]/Heart_beat_1").GetComponent<AudioSource>();
        heart2AudioSource =     GameObject.Find("[Audio]/Heart_beat_2").GetComponent<AudioSource>();
		// All effects on
		AllEffectsOn();
    }
    // Update is called once per frame
    void Update () {

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
			// Stressfaktor timer
			timerStress += Time.deltaTime;
			if(timerStress > 1.0f)
			{
				if(stressFaktor >= 0) stressFaktor -= 3;
				timerStress -= 1.0f;
			}
        }
        // Zone 2
        if(zone2)
        {
			// Stressfaktor timer
			timerStress += Time.deltaTime;
			if(timerStress > 1.0f)
			{
				stressFaktor += 1;
				timerStress -= 1.0f;
			}
        }
        // Zone 2
        if(zone3)
        {
			// Stressfaktor timer
			timerStress += Time.deltaTime;
			if(timerStress > 1.0f)
			{
				stressFaktor += 5;
				timerStress -= 1.0f;
			}
        }
        // Zone 3
        if(zone4)
        {
			// Stressfaktor timer
			timerStress += Time.deltaTime;
			if(stressFaktor < 200) stressFaktor = 200;
			if(timerStress > 1.0f)
			{
				stressFaktor += 10;
				timerStress -= 1.0f;
			}
        }

		// Stressfaktoreinfluss
		var blurOptimizedValue = stressFaktor * 0.001f;
		var motionBlurValue = stressFaktor * 0.00092f;
		var fishEyeValue = stressFaktor * 0.0003f;
		var noiseValue = stressFaktor * 0.001f;
		//
		BlurOptimized.blurSize = Mathf.Lerp(0, 1f, blurOptimizedValue);
        MotionBlur.blurAmount = Mathf.Lerp(0, 0.92f, motionBlurValue);
        FishEye.strengthX = Mathf.Lerp(0, 0.3f, fishEyeValue);
        FishEye.strengthY = Mathf.Lerp(0, 0.3f, fishEyeValue);
		NoiseAndScratches.grainIntensityMin = Mathf.Lerp(0, 0.5f, noiseValue);
		NoiseAndScratches.grainIntensityMax = Mathf.Lerp(0, 0.5f, noiseValue);
		//
		if(stressFaktor < 100)
		{
			// Audio
			breathe1AudioSource.enabled = true;
            breathe2AudioSource.enabled = false;
            breathe3AudioSource.enabled = false;
            heart1AudioSource.enabled = true;
            heart2AudioSource.pitch = 0;
            heart2AudioSource.enabled = false;
			// Flash light
            flashLight.GetComponent<Light>().intensity = 1;
 			// Velocity settings
            GetComponent<ExtendFlycam>().climbSpeed = 2;
            GetComponent<ExtendFlycam>().climbSpeed = 5;
            GetComponent<ExtendFlycam>().cameraSensitivity = 90;
		} else if(stressFaktor >= 100 && stressFaktor < 200)
		{
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
		} else
		{
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
	private void AllEffectsOn()
	{
		BlurOptimized.enabled = true;
        MotionBlur.enabled = true;
        FishEye.enabled = true;
        NoiseAndScratches.enabled = true;
	}
	private void AllEffectsOff()
	{
		BlurOptimized.enabled = false;
        MotionBlur.enabled = false;
        FishEye.enabled = false;
        NoiseAndScratches.enabled = false;
	}
}

