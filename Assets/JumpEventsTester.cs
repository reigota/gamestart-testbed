using UnityEngine;
using System.Collections;
using Game.Entity;
using Game.Core;
using Game.Core.Events.PlayerEvents;
using System;

public class JumpEventsTester : BaseBehaviour
{
    private Renderer _renderer;

    public Color OnStartJumpColor;
    public Color OnStartGoingDownColor;
    public Color OnStartFallingColor;
    public Color OnEndJumpColor;
    public Color OnReachTheGroundColor;


    public void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
    }

    public void Start()
    {
        BaseBehaviour bb = ((BaseBehaviour)GetComponent<IMovement>());

        bb.Subscribe<JumpEvents>("JumpEvent", JumpEventHander);
    }

    private void JumpEventHander(JumpEvents jumpEvent)
    {
        Color c = Color.white;

        Debug.Log(jumpEvent);

        switch(jumpEvent)
        {
            case JumpEvents.OnStartJump:
                c = OnStartJumpColor;
                break;
            case JumpEvents.OnStartGoingDown:
                c = OnStartGoingDownColor;
                break;
            case JumpEvents.OnStartFalling:
                c = OnStartFallingColor;
                break;
            case JumpEvents.OnEndJump:
                c = OnEndJumpColor;
                break;
            case JumpEvents.OnReachTheGround:
                c = OnReachTheGroundColor;
                break;
        }

        _renderer.material.color = c;

    }


    public void SecondJmpEvent(JumpEvents jumpEvent)
    {
        if(jumpEvent == JumpEvents.OnReachTheGround)
            transform.localScale += Vector3.one * 0.5f;
    }

}
