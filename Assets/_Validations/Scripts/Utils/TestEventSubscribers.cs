using UnityEngine;
using System.Collections;

public class TestEventSubscribers : MonoBehaviour 
{
    [SerializeField]
    private PlayerHackSlashController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerHackSlashController>();
    }

    private void OnEnable()
    {
        Debug.Log("Subsc");
        playerController.OnPlayerMovementEvent += OnPlayerMovement;
    }
    private void OnDisable()
    {
        Debug.Log("Unsubs");
        playerController.OnPlayerMovementEvent -= OnPlayerMovement;
    }

    private void OnPlayerMovement()
    {
        Debug.Log("Player move");
    }

    private void OnPlayerStop()
    {
        Debug.Log("Player idle");
    }

}
