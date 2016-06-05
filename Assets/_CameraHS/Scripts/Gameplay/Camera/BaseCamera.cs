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

		public Vector3 StartCameraPostition(Transform target, float height,float distance)
		{
			Vector3 tempPosition = new Vector3 (target.position.x, target.position.y + height, target.position.z - distance);
			return tempPosition;
		}
	}

}

