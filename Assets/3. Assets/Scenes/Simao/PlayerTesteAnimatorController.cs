using UnityEngine;
using System.Collections;
using System;


public class PlayerTesteAnimatorController : MonoBehaviour
{
    Animator anim;
    bool isWalking = false;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Walking();

        Turn();
        
        Move();
    }

    private void Walking()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isWalking = !isWalking;
            anim.SetBool("Walk", isWalking);
        }
    }

    private void Turn()
    {
        anim.SetFloat("Turn", Input.GetAxisRaw("Horizontal"));
    }

    private void Move()
    {
        if (anim.GetBool("Walk"))
        {
            if (Input.GetAxisRaw("MoveZ") < 0)
                anim.SetFloat("MoveZ", -.25f);
            else if (Input.GetAxisRaw("MoveZ") > 0)
                anim.SetFloat("MoveZ", .25f);
            else
                anim.SetFloat("MoveZ", 0);

            if (Input.GetAxisRaw("MoveX") < 0)
                anim.SetFloat("MoveX", -.25f);
            else if (Input.GetAxisRaw("MoveX") > 0)
                anim.SetFloat("MoveX", .25f);
            else
                anim.SetFloat("MoveX", 0);


        }
        else
        {
            anim.SetFloat("MoveZ", Input.GetAxisRaw("MoveZ"));
            anim.SetFloat("MoveX", Input.GetAxisRaw("MoveX"));
        }

    }
}
