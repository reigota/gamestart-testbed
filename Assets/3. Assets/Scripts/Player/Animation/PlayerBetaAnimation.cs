using UnityEngine;
using System.Collections;
using System;

/*
    Class para teste e validações das animações e script de movimentação. 
    Para a animação esta sendo utilizado um blend tree | 2D Freeform Directional
    Parametros float para as animações: DirectionZ, DirectionX

*/

[RequireComponent(typeof(Animator))]
public class PlayerBetaAnimation : MonoBehaviour
{
    //Private variables
    private Animator anim;
    private PlayerHackSlashController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerHackSlashController>();
    }

    private void OnEnabled()
    {
        playerController.OnPlayerMovementEvent += OnPlayerMovement;
        playerController.OnPlayerStopEvent += OnPlayerStop;   
    }

    private void OnDisabled()
    {
        playerController.OnPlayerMovementEvent -= OnPlayerMovement;
        playerController.OnPlayerStopEvent -= OnPlayerStop;
    }

    private void OnPlayerStop()
    {
        Debug.Log("parado");
    }

    //TODO = escuta ações de movimento do player e chama as animações de locomotion
    private void OnPlayerMovement(float moveSpeed)
    {
        anim.SetFloat("DirectionX", playerController.PlayerInput.x);
        anim.SetFloat("DirectionZ", playerController.PlayerInput.z);
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        OnPlayerMovement(0f);
    }
}
