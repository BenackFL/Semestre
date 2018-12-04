using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour {

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetMouseButtonDown(0)){
            animator.SetBool("IsRunning", true);
        } else {
            animator.SetBool("IsRunning", false);

        }

        if (Input.GetKeyDown(KeyCode.Space)){
            animator.SetTrigger("Jump");

        }
		
	}

    public void UpDateState(string state = null)
    {
        if (state != null)
        {

            animator.Play(state);

        }
    }
}
