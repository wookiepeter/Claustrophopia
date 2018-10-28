using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct MyRay
{
    public Vector2 origin;
    public Vector2 dir;
    public float length;

    public MyRay(Vector2 _origin, Vector2 _dir, float _length)
    {
        origin = _origin;
        dir = _dir;
        length = _length;
    }
}

/// <summary>
/// Intersection of a Line / Ray with an axis-aligned Rectangle (represented by a min- and maxpoint)
/// Source/Explanation can be found here: http://infocenter.arm.com/help/index.jsp?topic=/com.arm.doc.100140_0100_00_en/nic1407844297127.html
/// </summary>
public class LineRectangleIntersection : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
        ComputeStuff();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ComputeStuff()
    {
        Vector2 minPoint = new Vector2(-7 , -3.5f);
        Vector2 maxPoint = new Vector2(7, 3.5f);

        MyRay MyRay = new MyRay(new Vector2(0, 0), new Vector2(818,2032), 100f);

        Vector2 TminPoint = (minPoint - MyRay.origin) / MyRay.dir;
        Vector2 TmaxPoint = (maxPoint - MyRay.origin) / MyRay.dir;
        float Tmax;
        float Tmin;
        if (MyRay.dir.y / MyRay.dir.x > 0)
        {
            Tmin = (TminPoint.x > TminPoint.y) ? TminPoint.x : TminPoint.y;
            Tmax = (TmaxPoint.x < TmaxPoint.y) ? TmaxPoint.x : TmaxPoint.y;
        }
        else
        {
            Tmin = (TminPoint.x > TmaxPoint.y) ? TminPoint.x : TmaxPoint.y;
            Tmax = (TmaxPoint.x < TminPoint.y) ? TmaxPoint.x : TminPoint.y;
        }
        print("Intersecting at: " + (MyRay.origin + MyRay.dir * Tmin));
        print("and at: " + (MyRay.origin + MyRay.dir * Tmax));
    }
}
