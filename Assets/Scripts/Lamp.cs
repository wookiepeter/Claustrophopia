using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    bool hit = false;
    [SerializeField]
    float swingtime;
    [HideInInspector]
    public float currentswingtime;
    float timer;
    [SerializeField]
    float swingspeed;
    [HideInInspector]
    public float currentswingspeed;
    [SerializeField]
    float maxswing;
    [HideInInspector]
    public float currentmaxswing;
    float swingdirection = 1;
    Vector3 rotationaxis;
    public GameObject Player;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*randomtimer = Random.Range(1, 30);
        if (randomtimer>10 && randomtimer<20)
        {
            randomhit(Random.onUnitSphere);
        }*/
        if (hit == true)
        {
            turningscript();
        }
    }
    public void Onhit(Vector3 pushdirection, float _currentswingspeed, float _currentswingtime, float _currentmaxswing)
    {
        if (hit == false)
        {
            hit = true;
            rotationaxis = Vector3.Cross(Vector3.up, pushdirection);
            currentswingspeed = _currentswingspeed;
            currentswingtime = _currentswingtime;
            currentmaxswing = _currentmaxswing;

            timer = currentswingtime;
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Onhit(gameObject.transform.position - Player.transform.position, swingspeed, swingtime, maxswing);
        }
    }
    public void turningscript()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            hit = false;
            transform.rotation = Quaternion.identity;
        }

        if (Vector3.Angle(Vector3.down, -transform.up) >= (timer / currentswingtime) * currentmaxswing && Mathf.Sign(Vector3.SignedAngle(Vector3.down, -transform.up, rotationaxis)) == Mathf.Sign(swingdirection))
        {
            swingdirection = -swingdirection;
        }
        transform.Rotate(rotationaxis, currentswingspeed * Time.deltaTime * timer * swingdirection);
    }
}
