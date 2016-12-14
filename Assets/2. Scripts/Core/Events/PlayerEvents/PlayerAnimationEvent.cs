using System;
using UnityEngine;

namespace Game.Core.Events.PlayerEvents
{
    public enum AnimationEvent
    {
        OnStartWalk,
        OnStartRun,
        OnStartLeftStraf,
        OnStartRightStraf
    }

    [Serializable]
    public class PlayerAnimationEvent : BaseGameEvent
    {

        private AnimationEvent _animEvent;

        public AnimationEvent AnimationEvent { get { return _animEvent; } }

        public PlayerAnimationEvent(AnimationEvent je)
        {
            _animEvent = je;
        }

        public PlayerAnimationEvent(GameObject sender, AnimationEvent je): base(sender)
        {
            _animEvent = je;
        }

    }
}
