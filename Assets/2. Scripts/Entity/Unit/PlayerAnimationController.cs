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
            anim.SetFloat("DirectionX", _input.GetDesiredDirection().x, .2f, Time.deltaTime);
            anim.SetFloat("DirectionZ", _input.GetDesiredDirection().z, .2f, Time.deltaTime);
        }
    }
}
