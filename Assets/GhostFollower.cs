using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollower : MonoBehaviour {
    public GameObject Player;
    public float TargetDistance;
    public float AllowedDistance;
    public GameObject Ghost;
    public float FollowSpeed;
    public RaycastHit Shot;

	void Update () {
        transform.LookAt(Player.transform);
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out Shot))
        {
            TargetDistance = Shot.distance;
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
}
