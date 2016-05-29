using UnityEngine;
using System.Collections;
using System;

//Inicio da validação de movimentação hack and slash Tony.

public class PlayerHackSlashController : MonoBehaviour 
{
    //Private variables
    private Rigidbody rigidBody;
    private Vector3 playerInput = Vector3.zero;

    //Plubic delegates
    public Action OnPlayerMovementEvent;
    public Action OnPlayerStopEvent;
    public Action OnPlayerJumpEvent;
    public Action OnPlayerRunEvent;

    //Inspector variables
    [SerializeField] private float movementSpeed;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckInputs();
        MovimentPlayer();
    }
    
    private void CheckInputs()
    {
       float verticalInput = Input.GetAxis("Vertical");
       float horizontalInput = Input.GetAxis("Horizontal");

       playerInput = new Vector3(horizontalInput, 0, verticalInput);
    }

    private void MovimentPlayer()
    {
        rigidBody.velocity = playerInput * movementSpeed;

        if (playerInput != Vector3.zero)
            DispatchPlayerMovementEvent();      
    }

    private void CheckJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            DispatchPlayerJumpEvent();
        }
    }

    private void CheckRun()
    {
        if (Input.GetButtonDown("Run"))
        {
            DispatchPlayerRunEvent();
        }
    }

    //Dispatchers

    private void DispatchPlayerMovementEvent()
    {
        if (OnPlayerMovementEvent != null)
            OnPlayerMovementEvent();
    }

    private void DispatchPlayerStopEvent()
    {
        if (OnPlayerMovementEvent != null)
            OnPlayerMovementEvent();
    }

    private void DispatchPlayerJumpEvent()
    {
        if (OnPlayerMovementEvent != null)
            OnPlayerMovementEvent();
    }

    private void DispatchPlayerRunEvent()
    {
        if (OnPlayerMovementEvent != null)
            OnPlayerMovementEvent();
    }


}
