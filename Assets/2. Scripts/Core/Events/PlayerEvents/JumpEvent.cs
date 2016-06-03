﻿using System;


namespace Game.Core.Events.PlayerEvents
{
    public enum JumpEvents
    {
        OnStartJump,
        OnStartGoingDown,
        OnStartFalling,
        OnEndJump,
        OnReachTheGround
    }

    [Serializable]
    public class JumpEvent : BaseEvent<JumpEvents>
    {
        public override string GetEventName() { return GetType().Name; }
    }
}