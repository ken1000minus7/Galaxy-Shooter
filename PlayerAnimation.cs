using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
        	animator.SetBool("Turn_Left",true);
        	animator.SetBool("Turn_Right",false);
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
        	animator.SetBool("Turn_Left",false);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
        	animator.SetBool("Turn_Right",true);
        	animator.SetBool("Turn_Left",false);
        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
        	animator.SetBool("Turn_Right",false);
        }
    }
}
