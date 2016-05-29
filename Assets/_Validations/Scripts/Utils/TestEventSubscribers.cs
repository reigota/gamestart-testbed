using UnityEngine;
using System.Collections;
using System;

//TODO validate other events ( run and jump );
public class TestEventSubscribers : MonoBehaviour 
{
    [SerializeField]
    private PlayerHackSlashController playerController;

    private void OnEnable()
    {
        playerController.OnPlayerMovementEvent += OnPlayerMovement;
        playerController.OnPlayerStopEvent += OnPlayerStop;
    }

    private void OnDislabe()
    {
        playerController.OnPlayerMovementEvent -= OnPlayerMovement;
        playerController.OnPlayerStopEvent -= OnPlayerStop;
    }

    private void OnPlayerMovement()
    {
        Debug.Log("Player move");
    }

    private void OnPlayerStop()
    {
        Debug.Log("Player stop");
    }

}
