using UnityEngine;
using System;
using System.Collections;

using Game.Core;
using Game.Core.Events.PlayerEvents;

namespace Game.Entity.Unit
{
    /// <summary>
    /// Ver grafo da maquina de estados do pulo em https://drive.google.com/file/d/0BzUTb9rpce1-T2dWb0dDYTdFOWc/view?usp=sharing
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : BaseBehaviour, IMovement
    {
        #region Auxiliar/internal structs, enums and classes

        [Serializable]
        public struct PlayerAttributes
        {
            public float jumpForce;
            public float speedRunForward;
            public float speedWalkForward;
        }

        // Usado para evitar ficar instanciando eventos. Quando precisar publicar um evento 
        // basta reusar o evento que já esta instanciado e publica-lo novamente.
        // Como se fosse um "pool de eventos".
        private struct PlayerEvents
        {
            public PlayerJumpEvent jumpEvent;
            public PlayerAnimationEvent animatiorEvent;
        }

        // Possiveis estados do pulo
        public enum JumpState
        {
            OnTheGround,
            JumpBegin,   // Apenas no frame onde o desejo de pular é atendido. No frame seguinte já estará no GoingUp
            GoingUp,     // Enquanto rigidbody.velocity.y > 0, após JumpBegin
            GoingDown,   // Enquanto rigidbody.velocity.y < 0, após GoingUp
            JumpEnd,     // Apenas no frame onde atinge o chão. No frame seguinte já estará no OnTheGround
            FallingDown  // Quando apenas cai de uma plataforma, sem ter pulado. Enquanto rigidbody.velocity.y < 0.
        }

        #endregion

        #region Private fields

        [SerializeField]
        private PlayerAttributes _attributes;

        private PlayerEvents _events;

        private JumpState _jumpState;
        private Rigidbody _rigidbody;
        private IInput _input;
        private ControlBehaviour _previousDesiredBehaviour;
        private ControlBehaviour _currentDesiredBehaviour;
        #endregion

        #region Public properties

        public float JumpForce { get { return _attributes.jumpForce; } }

        public float SpeedRunForward { get { return _attributes.speedRunForward; } }

        public float SpeedWalkForward { get { return _attributes.speedWalkForward; } }

        #endregion

        #region Public methods

        public bool IsJumping()
        {
            return _jumpState != JumpState.OnTheGround;
        }

        public bool IsWalking()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private/Protected methods

        private IEnumerator ProcessJump()
        {
            _jumpState = JumpState.JumpBegin;
            _rigidbody.AddForce(Vector3.up * _attributes.jumpForce, ForceMode.Impulse);
            _events.jumpEvent.Publish(JumpEvent.OnStartJump);
            yield return 0;

            _jumpState = JumpState.GoingUp;
            yield return new WaitUntil(() => { return _rigidbody.velocity.y < 0; });

            _jumpState = JumpState.GoingDown;
            _events.jumpEvent.Publish(JumpEvent.OnStartGoingDown);
            yield return new WaitUntil(() => { return _rigidbody.velocity.y == 0; });

            _jumpState = JumpState.JumpEnd;
            _events.jumpEvent.Publish(JumpEvent.OnEndJump);
            yield return 0;

            _jumpState = JumpState.OnTheGround;
            _events.jumpEvent.Publish(JumpEvent.OnReachTheGround);
        }

        private IEnumerator ProcessFall()
        {
            _jumpState = JumpState.FallingDown;
            _events.jumpEvent.Publish(JumpEvent.OnStartFalling);
            yield return new WaitUntil(() => { return _rigidbody.velocity.y == 0; });

            _jumpState = JumpState.OnTheGround;
            _events.jumpEvent.Publish(JumpEvent.OnReachTheGround);
        }

        private void UpdateDesiredBehaviour()
        {
            // todo: verificar se tem controle no ar..
            if(IsJumping()) return;

            Vector3 curVel;

            bool walk = _currentDesiredBehaviour.IsSet(ControlBehaviour.Walk);
            bool run = _currentDesiredBehaviour.IsSet(ControlBehaviour.Run);
            bool jump = _currentDesiredBehaviour.IsSet(ControlBehaviour.Jump);

            if (jump)
            {
                StartCoroutine(ProcessJump());
                return;
            }

            if(walk || run)
            {
                // se houver condicoes de andar...
                // if (bla bla bla)
                {
                    curVel = _input.GetDesiredDirection() * (walk ? _attributes.speedWalkForward : _attributes.speedRunForward);
                    
                    Quaternion rotacao = Quaternion.LookRotation(transform.forward, transform.up);

                    curVel = rotacao * curVel;

                    _rigidbody.velocity = new Vector3(curVel.x, _rigidbody.velocity.y, curVel.z);

                     

                    if(_previousDesiredBehaviour.IsSet(ControlBehaviour.Idle))
                    {

                        // inicio do movimento (walk)
                        // publicacao do evento de inicio de movimento
                    }
                }
            }
        }

        private void GetComponentReferences()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _input = GetComponent<IInput>();
            _previousDesiredBehaviour = _input.GetDesiredBehaviour();
        }

        private void CreatePlayerEvents()
        {
            if(_events.jumpEvent == null)
            {
                _events.jumpEvent = new PlayerJumpEvent();
            }
            
        }


        #endregion

        #region Unity Events

        public void Awake()
        {
            GetComponentReferences();
            CreatePlayerEvents();
        }

        public void FixedUpdate()
        {
            if (!IsJumping())
            {
                if (_rigidbody.velocity.y<0)
                {
                    StartCoroutine(ProcessFall());
                }
            }
        }

        public void Update()
        {
            _currentDesiredBehaviour = _input.GetDesiredBehaviour();
            
            UpdateDesiredBehaviour();

            _previousDesiredBehaviour = _currentDesiredBehaviour;
        }

        #endregion

    }
}