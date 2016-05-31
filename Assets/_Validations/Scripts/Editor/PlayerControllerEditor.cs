using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// Use this Editor to build an friendly inspector for the GD's
/// Avoid using Debug.Log, is quite a pain in the eye try to follow those messages at it speed!
/// </summary>

[CustomEditor(typeof(PlayerHackSlashController))]
public class PlayerControllerEditor : Editor {
    // variables
    private bool showGDOptions = false;       // collapse the GD information
    private bool showPlayerBehaviour = false; // show player current behaviour
    private string playerBehaviour = "None";  // the current behaviour regarding the delegate events
    private int playerMoveSpeed = 0;          // current sqrtMagnitude velocity of the player in Unity's unit!

    private PlayerHackSlashController playerController;

    /// <summary>
    /// Raises the inspector GUI event.
    /// </summary>
    public override void OnInspectorGUI () {
        GetPlayerController();
        base.OnInspectorGUI ();

        showGDOptions = EditorGUILayout.Foldout(showGDOptions, "Game Designer Options");
        if (showGDOptions) {
            #region Player behaviour show to avoid Debug.Log
            EditorGUILayout.BeginHorizontal();
            showPlayerBehaviour = GUILayout.Toggle(showPlayerBehaviour, "Show behaviour");
            GUI.enabled = false; // make sure it's only "read-only"
            GUILayout.TextField((showPlayerBehaviour) ? playerBehaviour : "    ", 30);
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            if (showPlayerBehaviour)
                EditorGUILayout.LabelField("Move speed", playerMoveSpeed.ToString() + " units");
            #endregion
        }
    }

    /// <summary>
    /// Gets the player controller.
    /// </summary>
    private void GetPlayerController() {
        if (playerController == null) {
            playerController = ((PlayerHackSlashController) target);
            // assign the event triggers for the ocntroller
            playerController.OnPlayerMovementEvent += OnPlayerMove;
            playerController.OnPlayerStopEvent += OnPlayerStop;
            playerController.OnPlayerJumpEvent += OnPlayerJump;
            playerController.OnPlayerRunEvent += OnPlayerRun;
        }
    }

    #region events for the player controller
    private void OnPlayerMove(float moveSpeed) {
        playerBehaviour = "Player Moving";
        //TODO - I really don't know if lerp is need here, but....
        playerMoveSpeed = (int) Mathf.Abs(Mathf.Lerp(playerMoveSpeed, moveSpeed, 0.5f));
    }
    private void OnPlayerStop() {
        playerBehaviour = "Player Stoped";
        playerMoveSpeed = 0;
    }
    private void OnPlayerJump() {
        playerBehaviour = "Player Jump";
    }
    private void OnPlayerRun() {
        playerBehaviour = "Player Runing";
    }
    #endregion
}
