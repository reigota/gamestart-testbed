using System;

namespace Game.Core.Events.PlayerEvents
{
    public enum AnimatiorEvents
    {
        OnStartWalk,
        OnStartRun,
        OnStartLeftStraf,
        OnStartRightStraf
    }

    [Serializable]
    public class AnimatiorEvent : BaseEvent<AnimatiorEvents>
    {
        public override string GetEventName()
        {
            return GetType().Name;
        }
    }
}
