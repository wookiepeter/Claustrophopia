using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

    [SerializeField]
    bool open;

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("Open", open);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleGate()
    {
        open = !open;
        animator.SetBool("Open", open);
    }
}
