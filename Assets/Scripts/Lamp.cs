using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour {
    [SerializeField]
    bool hit = false;
    [SerializeField]
    float swingtime;
    float timer;
    [SerializeField]
    float swingspeed;
    float maxswing = 60;
    float swingdirection = 1;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("b"))
        {
            Onhit();
        }
        if (hit == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                hit = false;
            }
            
            if (Vector3.Angle(Vector3.down,-transform.up) >= (timer/swingtime)*maxswing && Mathf.Sign(Vector3.SignedAngle(Vector3.down, -transform.up,Vector3.right)) == Mathf.Sign(swingdirection))
            {
                swingdirection = -swingdirection;
                print("richtungswechsel");
            }
            transform.Rotate(Vector3.right, swingspeed * Time.deltaTime * swingdirection);
        }
    }
    void Onhit()
    {
        hit = true;
        timer = swingtime;
        print("start swinging");
    }
} 
