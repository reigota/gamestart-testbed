using UnityEngine;
using System.Collections;
using GamePlay.Interfaces;
using System;

namespace GamePlay.Inputs
{

    public class UserInput : MonoBehaviour, IInput
    {

        private Vector3 _inputDirection;
        private ControlBehaviour _controlBehaviour;

        public void Update()
        {
            CheckInput();
        }

        private void CheckInput()
        {

            float verticalInput = Input.GetAxisRaw("Vertical");
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            _inputDirection = new Vector3(horizontalInput, 0, verticalInput);
        }

        public ControlBehaviour GetDesiredBehaviour()
        {

            ControlBehaviour ret = _inputDirection.x != 0 || _inputDirection.z != 0  ?
                            (Input.GetButton("Run") ? ControlBehaviour.Run : ControlBehaviour.Walk)
                            : ControlBehaviour.Idle;

            if(Input.GetButton("Jump"))
                ret |= ControlBehaviour.Jump;

            if(Input.GetButton("Attack"))
                ret |= ControlBehaviour.Attack;

            //Debug.Log(ret);
            return ret;
        }

        public Vector3 GetDesiredDirection()
        {
            return _inputDirection;
        }
    }
}