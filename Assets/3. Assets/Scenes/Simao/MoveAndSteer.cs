using UnityEngine;
using System.Collections;

public class MoveAndSteer : MonoBehaviour
{

    //This is the speed with which we want to rotate our character
    public float rotationSpeed = 90f;

    //We will store the desired character speed (used in the Animator Controller) in this variable
    float desiredSpeed = 0f;

    //This variables are used to store Horizontal and Vertical input axes values.
    float hor = 0f;
    float ver = 0f;

    //We store the reference to the Animator component in this variable.
    Animator anim;

    void Start()
    {

        //We assign the Animator component to our anim variable when the game starts
        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        //We rotate our character in the global Y axis, depending on our Horizontal axis input
        //transform.Rotate(Vector3.up * hor * rotationSpeed * Time.deltaTime);

        //If player holds left shift or right shift we set the desiredSpeed to 2, to make our character run.
        //If player doesn't hold any of the shift keys, we set it to 1, to make the character walk.

        //if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        //{
        //    desiredSpeed = 3f;
        //}
        //else
       // {
        //    desiredSpeed = 1f;
       // }

        //We set the DesiredSpeed parameter based on the Vertical axis input.
        //If player doesn't hold the up arrow, the value of the ver variable will be 0,
        //so we will set the DesiredSpeed parameter to 0 and make the character play Idle animation


        //anim.SetFloat("DesiredSpeed", desiredSpeed * ver, 0.2f, Time.deltaTime);
        //anim.SetFloat("WalkBack", desiredSpeed * ver, 0.2f, Time.deltaTime);



        anim.SetFloat("VertSpeed", ver, 0.2f, Time.deltaTime);
        anim.SetFloat("HorSpeed", hor, 0.2f, Time.deltaTime);


        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetLayerWeight(1, 1);
            if ((Input.GetAxisRaw("Vertical") == 0f) && (Input.GetAxisRaw("Horizontal") == 0f))
            {
                anim.SetBool("rightTurn", true);
            }
        } else
        {
            anim.SetBool("rightTurn", false);
        }


        if (Input.GetKey(KeyCode.E))
        {
            anim.SetLayerWeight(1, 1);
            if ((Input.GetAxisRaw("Vertical") == 0f) && (Input.GetAxisRaw("Horizontal") == 0f))
            {
                anim.SetBool("leftTurn", true);
            }
        }
        else
        {
            anim.SetBool("leftTurn", false);
            anim.SetLayerWeight(0, 1);
        }


    }
}
