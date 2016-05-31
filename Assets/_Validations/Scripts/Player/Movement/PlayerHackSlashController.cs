using UnityEngine;
using System.Collections;
using System;

//Inicio da validação de movimentação hack and slash Tony.

[RequireComponent(typeof(Rigidbody))]
public class PlayerHackSlashController : MonoBehaviour 
{
    //Private variables
    private Rigidbody rigidBody;
    private Vector3 playerInput = Vector3.zero;
    private bool playerJumped = false;

    //Plubic delegates
    public Action<float> OnPlayerMovementEvent;
    public Action OnPlayerStopEvent;
    public Action OnPlayerJumpEvent;
    public Action OnPlayerGroundedEvent; // FIXME: we need a better name - since there is jump, we need to dispatch an event when we hit the ground!
    public Action OnPlayerRunEvent;
    public Vector3 PlayerInput { get { return playerInput; }} // Para ser acessado por classes com interesse no vector3 dos inputs

    //Inspector variables
    [Header("Variables for player controller")]
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

        // We must see if the magnitude velocity of the rigidbody is greater than 0 to say that
        // the character is really moving, otherwise, it has stop moving!
        Vector3 rigidbodyVelocityNoY = new Vector3(
            rigidBody.velocity.x,
            0, // the Y will be for jump
            rigidBody.velocity.z);
        
        if (rigidbodyVelocityNoY.sqrMagnitude > 0) {
            DispatchPlayerMovementEvent();
        } else
            DispatchPlayerStopEvent();
    }

    //TODO = gravity fix, and infinity jump
    private void CheckJump()
    {
        //FIXME: we need a way to uncheck this when he hit the ground. this way we make sure that the player
        //       can only jump when is grounded, not in the air!
        if (!playerJumped) {
            if(Input.GetButtonDown("Jump"))
            {
                playerJumped = true;
                rigidBody.AddForce(new Vector3(0, jumpForce, 0));
                DispatchPlayerJumpEvent();
            }
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
            OnPlayerMovementEvent(rigidBody.velocity.sqrMagnitude);
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
