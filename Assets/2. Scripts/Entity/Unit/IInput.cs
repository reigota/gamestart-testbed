using UnityEngine;
using System;

namespace Game.Entity.Unit {

    [Flags]
    public enum ControlBehaviour
    {
        Idle = 1,
        Walk = 2,
        Attack = 4,
        Jump = 8,
        Run = 16
    }

    public interface IInput {

        Vector3 GetDesiredDirection();
        
        ControlBehaviour GetDesiredBehaviour();

    }
}