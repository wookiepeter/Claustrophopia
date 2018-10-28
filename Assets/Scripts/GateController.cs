using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

    [SerializeField]
    bool open;
    Animator animator;

    // Alexey - Add audio
    private AudioSource gridAudioSource;
    private AudioClip gridAudioClip;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("Open", open);

        // Alexey - Add audio
        gridAudioClip = Resources.Load<AudioClip>("Audio/grid_1");
        gridAudioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        gridAudioSource.clip = gridAudioClip;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void ToggleGate()
    {
        open = !open;
        animator.SetBool("Open", open);

        // Alexey - Add audio
        gridAudioSource.Play();
    }
}
