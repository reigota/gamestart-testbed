using UnityEngine;
using System.Collections;
using Game.Core;

namespace Game.Entity.Unit
{
    public class PlayerAnimationController : BaseBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private PlayerMovementController playerMovementController;

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
        }

        private void Update()
        {

        }
    }
}
