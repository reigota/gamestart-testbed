using UnityEngine;
using System.Collections;

namespace Gameplay.Camera
{
	public class CameraTestScript : MonoBehaviour 
	{
		[SerializeField] private BaseCamera _baseCamera;
		[SerializeField] private Transform _target;
		[SerializeField] private float _height;
		[SerializeField] private float _distance;


		private void Awake()
		{
			_baseCamera = GetComponent<BaseCamera> ();
		}

		private void  Start()
		{
			_baseCamera.CameraTarget = _target;
			_baseCamera.CameraHeight = _height;
			_baseCamera.CameraDistance = _distance;
		}
				
	}

}
