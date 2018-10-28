using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour {

    [SerializeField]
    GameObject camera;

    [SerializeField]
    LeverController activeLever;

    bool inLeverRange;
    bool lookingAtLever;
    bool displayingLeverMessage;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (displayingLeverMessage == false && inLeverRange == true && Vector3.Angle(camera.transform.forward, activeLever.transform.position - camera.transform.position) < 60)
        {
            displayingLeverMessage = true;
            activeLever.ToggleLeverText(true);
        }
        else if(displayingLeverMessage == true && inLeverRange == true)
        {
            if(Vector3.Angle(camera.transform.forward, activeLever.transform.position - camera.transform.position) < 60)
            {
                displayingLeverMessage = true;
                activeLever.ToggleLeverText(true);
            }
            else if(Vector3.Angle(camera.transform.forward, activeLever.transform.position - camera.transform.position) > 80)
            {
                displayingLeverMessage = false;
                activeLever.ToggleLeverText(false);
            }
        }
        else if(displayingLeverMessage == true && inLeverRange == false)
        {
            displayingLeverMessage = false;
            activeLever.ToggleLeverText(false);
            activeLever = null;
        }
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lever") || other.tag == "Player")
        {
            inLeverRange = true;
            activeLever = other.GetComponent<LeverController>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lever") || other.tag == "Player")
        {
            inLeverRange = false;
        }
    }
}
