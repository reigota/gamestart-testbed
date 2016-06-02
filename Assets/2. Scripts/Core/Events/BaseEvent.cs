using System;
using UnityEngine.Events;

namespace Game.Core.Events
{
    public abstract class BaseEvent : UnityEvent, IEvent
    {
        public abstract string GetEventName();

        public virtual void Dispatch()
        {
            Invoke();
        }

    }

    public abstract class BaseEvent<T> : UnityEvent<T>, IEvent
    {
        public abstract string GetEventName();

        public virtual void Dispatch()
        {
            throw new ArgumentNullException("arg", "Generic argument can not be omitted. Use BaseEvent instead of BaseEvent<T> as base class.");
        }

        public virtual void Dispatch(T arg)
        {
            Invoke(arg);
        }

    }
}