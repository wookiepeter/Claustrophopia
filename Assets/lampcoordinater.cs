using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lampcoordinater : MonoBehaviour {
    [SerializeField]
    bool hit = false;
    [SerializeField]
    float swingtime;
    float timer;
    [SerializeField]
    float swingspeed;
    [SerializeField]
    float maxswing;
    float swingdirection = 1;
    Vector3 rotationaxis;
    private float randomtimer= 5;
    [SerializeField]
    List<Lamp> lamplist;
    private int n = 5;
    private int randomobject;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        randomtimer -= Time.deltaTime;
        if (randomtimer < 0)
        {
            randomobject = Random.Range(0, lamplist.Count);
            randomhit(Random.onUnitSphere, lamplist[randomobject]);
            randomtimer = n;
            n = Random.Range(5, 10);
        }
    }
    void randomhit(Vector3 pushdirection,Lamp gameobject)
    {
        gameobject.Onhit(pushdirection, swingspeed, swingtime, maxswing);

    }
    public void turningscript()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            hit = false;
            transform.rotation = Quaternion.identity;
        }

        if (Vector3.Angle(Vector3.down, -transform.up) >= (timer / swingtime) * maxswing && Mathf.Sign(Vector3.SignedAngle(Vector3.down, -transform.up, rotationaxis)) == Mathf.Sign(swingdirection))
        {
            swingdirection = -swingdirection;
        }
        transform.Rotate(rotationaxis, swingspeed * Time.deltaTime * timer * swingdirection);
    }
}
