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
    public Vector3 PlayerInput { get { return playerInput; }} // Para ser acessado por classes com interesse no vector3 dos inputs

    //Inspector variables
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckInputs();
        
    }

    private void FixedUpdate()
    {
        MovementPlayer();
        //CheckJump();
       //CheckRun();
    }
    
    private void CheckInputs()
    {
       float verticalInput = Input.GetAxis("Vertical");
       float horizontalInput = Input.GetAxis("Horizontal");

       playerInput = new Vector3(horizontalInput, 0, verticalInput);
    }

    private void MovementPlayer()
    {
        rigidBody.velocity = playerInput * movementSpeed;

        //TODO = fix button transition dispatch stop event
        if(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            DispatchPlayerMovementEvent();


        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
            DispatchPlayerStopEvent();
    }

    //TODO = gravity fix, and infinity jump
    private void CheckJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            rigidBody.AddForce(new Vector3(0, jumpForce, 0));
            DispatchPlayerJumpEvent();
        }
    }

    //TODO = infinity run
    private void CheckRun()
    {
        float bkpMovementSpeed = movementSpeed;
        if (Input.GetButtonDown("Run"))
        {
            movementSpeed = 2 * movementSpeed;
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
        if (OnPlayerStopEvent != null)
            OnPlayerStopEvent();
    }

    private void DispatchPlayerJumpEvent()
    {
        if (OnPlayerJumpEvent != null)
            OnPlayerJumpEvent();
    }

    private void DispatchPlayerRunEvent()
    {
        if (OnPlayerRunEvent != null)
            OnPlayerRunEvent();
    }


}
