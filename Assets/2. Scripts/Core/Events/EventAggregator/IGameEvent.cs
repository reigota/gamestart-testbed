using UnityEngine;

namespace Game.Core
{
    public interface IGameEvent
    {
        GameObject Sender { get; }
    }
}
