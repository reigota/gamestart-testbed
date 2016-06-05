using UnityEngine;
using System.Collections;

namespace Gameplay.Camera
{
	[RequireComponent(typeof(Transform))]
	public class OrbitalCameraController : MonoBehaviour 
	{
		[SerializeField] private BaseCamera baseCamera;
		[SerializeField] private Transform target;
		[SerializeField] private float mouseSpeedX = 250f;
	    [SerializeField] private float mouseSpeedY = 120f;

		private Transform _myTransform;
		private float _mouseX;
		private float _mouseY;
		private bool _mousePressed = false;

		private void Awake()
		{
			baseCamera = GetComponent<BaseCamera> ();
		}

		private void Start()
		{
			_myTransform = this.transform;
			baseCamera.CameraTarget = target;
			baseCamera.CameraHeight = 4f;
			baseCamera.CameraDistance = 6f;
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown (1))
				_mousePressed = true;

			if (Input.GetMouseButtonUp (1))
				_mousePressed = false;

		}

		private void LateUpdate()
		{
			_myTransform.position = baseCamera.StartCameraPostition (baseCamera.CameraTarget,baseCamera.CameraHeight,baseCamera.CameraDistance);
			_myTransform.LookAt (target);

			if (_mousePressed) 
			{
				MouseOrbit ();
			}
		}

		private void MouseOrbit()
		{
			_mouseX += Input.GetAxis ("Mouse X") * mouseSpeedX * 0.02f;
			_mouseY -= Input.GetAxis ("Mouse Y") * mouseSpeedY * 0.02f;

			Quaternion mouseRotation = Quaternion.Euler (_mouseX, _mouseY, 0);
			Vector3 mousePosition = mouseRotation * new Vector3 (0f, 0f, -baseCamera.CameraDistance) + target.position;

			_myTransform.rotation = mouseRotation;
			_myTransform.position = mousePosition;
		}
	}
}

