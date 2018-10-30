using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour {

    public LeverInteraction myLeverInteraction;

    [SerializeField]
    GameObject renderer;

    Animator animator;

    [SerializeField]
    bool leverisUp = true;
    [SerializeField]
    bool leverIsActive = false;
    bool inReichweite; 
    [SerializeField]
    List<GateController> connectedGates;
    [SerializeField]
    Color gizmosColor = Color.green;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        renderer.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        
   
        if (leverIsActive == false && Input.GetKeyDown("e")&& myLeverInteraction.inLeverRange)
        {
            print("activating lever");
            ToggleLever();
        } else if (leverIsActive == true && animator.GetCurrentAnimatorStateInfo(0).IsTag("Stop"))
        {
            leverIsActive = false;
        }
	}

    public void ToggleLever()
    {
        leverIsActive = true;
        if(leverisUp == true)
        {
            animator.SetTrigger("LeverPull");
        }
        else
        {
            animator.SetTrigger("LeverRaise");
        }
        ToggleAllGates();
        leverisUp = !leverisUp;
    }

    public void ToggleAllGates()
    {
        foreach (GateController gate in connectedGates)
        {
            gate.ToggleGate();
        }
    }

    public void ToggleLeverText(bool isActive)
    {
        renderer.SetActive(isActive);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        foreach (GateController gate in connectedGates)
        {
            Gizmos.DrawLine(transform.position, gate.transform.position);
        }
    }
    
}
