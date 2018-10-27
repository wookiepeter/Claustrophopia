using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thread : MonoBehaviour {

    [SerializeField]
    LineRenderer line;

    [SerializeField]
    float segmentLength;

    [SerializeField]
    Vector3 threadPositionOffset;

    [SerializeField]
    List<Vector3> threadPositions;

    float deltaDistance;

    Vector3 lastPosition;   

	// Use this for initialization
	void Start () {
        lastPosition = transform.position;
        threadPositions = new List<Vector3>();
        RaycastHit hit = new RaycastHit();
        bool stuff = Physics.Raycast(transform.position, Vector3.down, out hit, 10f);
        threadPositions.Add(hit.point);
        threadPositions.Add(transform.position);
        line.SetPositions(threadPositions.ToArray());
	}
	
	// Update is called once per frame
	void Update () {
        deltaDistance += Vector3.Distance(transform.position, lastPosition);
        if (deltaDistance > segmentLength)
        {
            UpdateThreadSegments();
        }
        else
        {
            UpdateLastThreadSegment();
        }
        lastPosition = transform.position;
	}

    void UpdateThreadSegments()
    {
        RaycastHit hit = new RaycastHit();
        bool stuff = Physics.Raycast(transform.position, Vector3.down, out hit, 10f);
        threadPositions[threadPositions.Count - 1] = hit.point;
        threadPositions.Add(transform.position);
        line.positionCount = threadPositions.Count;
        line.SetPositions(threadPositions.ToArray());
        deltaDistance = 0;
    }

    void UpdateLastThreadSegment()
    {
        line.SetPosition(threadPositions.Count - 1, transform.position);
    }
}
