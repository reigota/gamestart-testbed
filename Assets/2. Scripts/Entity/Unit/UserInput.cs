using UnityEngine;

using Game.Core;
using System;

namespace Game.Entity.Unit
{

    public class UserInput : BaseBehaviour, IInput
    {
        #region Private fields

        private Vector3 _inputDirection;
        private ControlBehaviour _controlBehaviour;

        #endregion

        #region Unity Events

        public void Update()
        {
            UpdateDirection();
            UpdateDesiredAction();
        }

        #endregion

        #region Private methods

        private void UpdateDirection()
        {
            float verticalInput = Input.GetAxisRaw("Vertical");
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            _inputDirection = new Vector3(horizontalInput, 0, verticalInput);
        }

        private void UpdateDesiredAction()
        {

            ControlBehaviour ret = _inputDirection.x != 0 || _inputDirection.z != 0  ?
                            (Input.GetButton("Run") ? ControlBehaviour.Run : ControlBehaviour.Walk)
                            : ControlBehaviour.Idle;

            if(Input.GetButton("Jump"))
                ret |= ControlBehaviour.Jump;

            if(Input.GetButton("Attack"))
                ret |= ControlBehaviour.Attack;

            _controlBehaviour = ret;
        }

        #endregion

        #region Public methods

        public Vector3 GetDesiredDirection()
        {
            return _inputDirection;
        }

        public ControlBehaviour GetDesiredBehaviour()
        {
            return _controlBehaviour;
        }

        public Ray getMouseRay() {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        #endregion
    }
}