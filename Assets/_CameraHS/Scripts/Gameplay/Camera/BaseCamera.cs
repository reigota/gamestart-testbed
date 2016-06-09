using UnityEngine;
using System.Collections;
using System;

namespace Gameplay.Camera
{
	public class BaseCamera : MonoBehaviour, ICamera 
	{
		private Transform _cameraTarget;
		private float _cameraHeight;
		private float _cameraDistance;
        private float _mouseSpeedX;
        private float _mouseSpeedY;
        private float _heightDamping;
        private float _rotationDamping;



        public Transform CameraTarget 
		{
			get 
			{
				return _cameraTarget;
			}
			set 
			{
				_cameraTarget = value;
			}
		}

		public float CameraHeight 
		{
			get 
			{
				return _cameraHeight;
			}
			set 
			{
				_cameraHeight = value;
			}
		}

		public float CameraDistance 
		{
			get 
			{
				return _cameraDistance;
			}
			set
			{
				_cameraDistance = value;
			}
		}

        public float MouseSpeedX
        {
            get
            {
                return _mouseSpeedX;
            }
            set
            {
                _mouseSpeedX = value;
            }
        }

        public float MouseSpeedY
        {
            get
            {
                return _mouseSpeedY;
            }
            set
            {
                _mouseSpeedY = value;
            }
        }

        public float HeightDamping
        {
            get
            {
                return _heightDamping;
            }
            set
            {
                _heightDamping = value;
            }
        }

        public float RotationDamping
        {
            get
            {
                return _rotationDamping;
            }
            set
            {
                _rotationDamping = value;
            }
        }

        public Vector3 StartCameraPostition(Transform target, float height,float distance)
		{
			Vector3 tempPosition = new Vector3 (target.position.x, target.position.y + height, target.position.z - distance);
			return tempPosition;
		}
	}

}

