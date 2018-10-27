using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollower : MonoBehaviour {
    public GameObject Player;
    public float TargetDistance;
    public float AllowedDistance;
    public GameObject Ghost;
    public float FollowSpeed;

	void Update () {
        transform.LookAt(Player.transform);


        TargetDistance = Vector3.Distance(Player.transform.position, Ghost.transform.position);
            if (TargetDistance >= AllowedDistance)
            {
                FollowSpeed = 0.1f;
                Ghost.GetComponent<Animation>().Play("ghost_move");
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, FollowSpeed);
                print("move");
            }
            else
            {
                FollowSpeed = 0;
                Ghost.GetComponent<Animation>().Play("ghost_idle");
                print("idle");
            }
        
	}
}
