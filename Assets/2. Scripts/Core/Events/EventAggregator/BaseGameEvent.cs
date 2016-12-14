using UnityEngine;

namespace Game.Core
{
    public class BaseGameEvent : IGameEvent
    {
        public BaseGameEvent()
        {
        }

        public BaseGameEvent(GameObject sender)
        {
            mSender = sender;
        }

        protected GameObject mSender = null;
        public GameObject Sender
        {
            get { return mSender; }
        }

    }
}
