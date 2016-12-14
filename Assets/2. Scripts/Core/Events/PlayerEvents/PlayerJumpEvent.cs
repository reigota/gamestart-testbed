using System;
using UnityEngine;

namespace Game.Core.Events.PlayerEvents
{
    public enum JumpEvent
    {
        OnStartJump,
        OnStartGoingDown,
        OnStartFalling,
        OnEndJump,
        OnReachTheGround
    }

    [Serializable]
    public class PlayerJumpEvent : BaseGameEvent
    {

        public JumpEvent JumpEvent { get; set; }

        public PlayerJumpEvent(JumpEvent je): base()
        {
            JumpEvent = je;
        }

        public PlayerJumpEvent(GameObject sender, JumpEvent je): base(sender)
        {
            JumpEvent = je;
        }

        public PlayerJumpEvent() : base()
        {
        }

        public void Publish(JumpEvent je)
        {
            JumpEvent = je;
            EventAggregator.Publish<PlayerJumpEvent>(this);
        }

    }
}