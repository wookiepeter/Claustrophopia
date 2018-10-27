using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movingtext : MonoBehaviour {
    [SerializeField]
    public float speed;
    [SerializeField]
    public Vector3 direction;
    private float fadeTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float translation = speed * Time.deltaTime;

        transform.Translate(direction * translation);
    }
}
