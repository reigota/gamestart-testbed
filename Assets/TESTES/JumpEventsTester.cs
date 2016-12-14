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
        //BaseBehaviour bb = ((BaseBehaviour)GetComponent<IMovement>());
        //bb.Subscribe<JumpEvent>("JumpEvent", JumpEventHander);
        EventAggregator.AddListener<PlayerJumpEvent>(JumpEventHandler);
    }

    private void JumpEventHandler(IGameEvent gameEvent)
    {
        PlayerJumpEvent jumpEvent = (PlayerJumpEvent)gameEvent;

        Color c = Color.white;

        Debug.Log(jumpEvent.JumpEvent);

        switch(jumpEvent.JumpEvent)
        {
            case JumpEvent.OnStartJump:
                c = OnStartJumpColor;
                break;
            case JumpEvent.OnStartGoingDown:
                c = OnStartGoingDownColor;
                break;
            case JumpEvent.OnStartFalling:
                c = OnStartFallingColor;
                break;
            case JumpEvent.OnEndJump:
                c = OnEndJumpColor;
                break;
            case JumpEvent.OnReachTheGround:
                c = OnReachTheGroundColor;
                break;
        }

        _renderer.material.color = c;

    }


    public void SecondJmpEvent(JumpEvent jumpEvent)
    {
        if(jumpEvent == JumpEvent.OnReachTheGround)
            transform.localScale += Vector3.one * 0.5f;
    }

}
