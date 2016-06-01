using UnityEngine;
using System.Collections;
using GamePlay.Interfaces;
using System;

namespace GamePlay.Movement
{
    [Serializable]
    public struct PlayerAttributes
    {
        public float jumpForce;
        public float speedRunForward;
        public float speedWalkForward;
    }

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour, IMovement
    {

        [SerializeField]
        private PlayerAttributes _attributes;

        public float JumpForce { get { return _attributes.jumpForce; } }

        public float SpeedRunForward { get { return _attributes.speedRunForward; } }

        public float SpeedWalkForward { get { return _attributes.speedWalkForward; } }

        private Rigidbody _rigidbody;
        private IInput _input;
        private ControlBehaviour _previousDesiredBehaviour;

        public bool IsJumping()
        {
            return _rigidbody.velocity.y != 0;
        }

        public bool IsWalking()
        {
            throw new NotImplementedException();
        }

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<IInput>();
            _previousDesiredBehaviour = _input.GetDesiredBehaviour();


        }

        public void FixedUpdate()
        {
            Vector3 curVel;

            ControlBehaviour currentDesiredBehaviour = _input.GetDesiredBehaviour();

            bool walk = currentDesiredBehaviour.IsSet(ControlBehaviour.Walk);
            bool run = currentDesiredBehaviour.IsSet(ControlBehaviour.Run);
            bool jump = currentDesiredBehaviour.IsSet(ControlBehaviour.Jump);

            if( walk || run)
            {
                // se houver condicoes de andar...
                // if (bla bla bla)
                {
                    curVel = _input.GetDesiredDirection() * (walk ? _attributes.speedWalkForward : _attributes.speedRunForward);

                    _rigidbody.velocity = new Vector3(curVel.x, _rigidbody.velocity.y, curVel.z);
                    
                    if(_previousDesiredBehaviour.IsSet(ControlBehaviour.Idle))
                    {

                        // inicio do movimento (walk)
                        // publicacao do evento de inicio de movimento
                        curVel = _rigidbody.velocity;
                        curVel.y = _attributes.jumpForce;
                        _rigidbody.velocity = curVel;
                    }
                }
            }


            _previousDesiredBehaviour = currentDesiredBehaviour;
        }
    }
}