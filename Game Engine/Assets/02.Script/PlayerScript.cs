using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{   
    
    enum State
    {
        A_idle, A_attact,A_gethit,A_run

    }State state = State.A_idle;

    Animator animator;
    GameObject mycamera;

    private void Awake()
    {
        mycamera = GameObject.FindGameObjectWithTag("MainCamera");
        animator=GetComponent<Animator>();
        change_state(State.A_idle);
    }

    private void Update()
    {
        if (state != State.A_attact)
        {

            if (Input.GetKey(KeyCode.W))
            {

                change_state(State.A_run);
                transform.Translate(Vector3.forward * Time.deltaTime * 5f);
            }
        }
           
        if (Input.GetKeyUp(KeyCode.W))
           {
               change_state(State.A_idle);
           }
        
        if (Input.GetMouseButtonUp(0))
        {
            change_state(State.A_attact);

        }


        con_ani();
     
        transform.rotation = Quaternion.Euler(new Vector3(0, mycamera.transform.eulerAngles.y, 0));
    
        

    }
    void setani()
    {
        switch(state)
        {
            case State.A_idle:
                animator.SetBool("a_idle", true);
                animator.SetBool("a_run", false);
                
                break;
            case State.A_run:
                animator.SetBool("a_idle", false);
                animator.SetBool("a_run", true);
                
                break;
            case State.A_attact:
                animator.SetBool("a_idle", false);
                animator.SetBool("a_run", false);
                animator.SetTrigger("a_attact");
                break;
        }
    }

    void change_state(State value)
    {
        state = value;
        setani();
    }

    void con_ani()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("a_attact"))
            return;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            return;
        change_state(State.A_idle);
    }
}
