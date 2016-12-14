using UnityEngine;
using System.Collections;
using Game.Core;

namespace Game.Entity.Unit
{
    public class PlayerAnimationController : BaseBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private PlayerMovementController playerMovementController;


        private IInput _input;
        //Estados da Animacao
        public enum AnimationSate
        {
            isWalk,
            isRun,
            isJump
        }

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            playerMovementController = GetComponent<PlayerMovementController>();
            _input = GetComponent<IInput>();
        }

        private void Update()
        {
            //Read player input
            //float Vert = Input.GetAxisRaw("Vertical");
            //float Horz = Input.GetAxisRaw("Horizontal");
            ////Set animator floating point values
            //anim.SetFloat("Turn", Horz, 0.2f, Time.deltaTime);
            //anim.SetFloat("Forward", Vert*.5f, 0.2f, Time.deltaTime);

            ControlBehaviour ctrl = _input.GetDesiredBehaviour();

            float forward_speed = ctrl.IsSet(ControlBehaviour.Run) ? 1f : (ctrl.IsSet(ControlBehaviour.Walk) ? 0.5f : 0f);

            anim.SetFloat("Turn", _input.GetDesiredDirection().x, .2f, Time.deltaTime);
            anim.SetFloat("Forward", _input.GetDesiredDirection().z * forward_speed, .2f, Time.deltaTime);
        }
    }
}
