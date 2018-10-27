﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollower : MonoBehaviour {
    public GameObject Player;
    public float TargetDistance;
    public float AllowedDistance;
    public GameObject Ghost;
    public float FollowSpeed;
    float timer = 0;
    bool danger = false;
    float dangerspeed = 1;
    float dangerspeedveritcal = 1;

    void Update () {
        transform.LookAt(Player.transform);
        timer += Time.deltaTime;
        if (danger == true)
        {
            dangerspeed = 2;
            dangerspeedveritcal = 5;
        }

        TargetDistance = Vector3.Distance(Player.transform.position, Ghost.transform.position);
            if (TargetDistance >= AllowedDistance)
            {
                FollowSpeed = 0.085f*dangerspeed;
                Ghost.GetComponent<Animation>().Play("ghost_move");
                Vector3 newposition = Vector3.MoveTowards(transform.position, Player.transform.position, FollowSpeed);
                newposition.y = (Player.transform.position.y + Mathf.Sin(timer* dangerspeedveritcal))*0.5f;
                transform.position = Vector3.MoveTowards(transform.position, newposition, FollowSpeed);//Player.transform.position, FollowSpeed);  
        }
            else
            {
                FollowSpeed = 0;
                Ghost.GetComponent<Animation>().Play("ghost_idle");
                transform.position = new Vector3(transform.position.x, (Player.transform.position.y + Mathf.Sin(timer* dangerspeedveritcal)) * 0.5f, transform.position.z);
            }
        
	}
}
