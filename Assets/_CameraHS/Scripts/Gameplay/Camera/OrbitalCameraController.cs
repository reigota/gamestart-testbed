using UnityEngine;
using System.Collections;

namespace Gameplay.Camera
{
    public class OrbitalCameraController : MonoBehaviour 
	{
		[SerializeField] private Transform target;

        private Transform _myTransform;
		private float _mouseX;
		private float _mouseY;
		private bool _mousePressed = false;
        private float _cameraHeight = 4f;
        private float _cameraDistance = 6f;
        private float _mouseSpeedX = 250f;
        private float _mouseSpeedY = 120f;
        private float _heightDamping = 2.0f;
        private float _rotationDamping = 3.0f;

        private void Awake()
		{
            _myTransform = this.transform;
        }

		private void Start()
		{
            if (target == null)
                Debug.Log("Miss target:");
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
            if (target == null)
                return;

            if (_mousePressed) 
				MouseOrbit ();

            else
            {
                _mouseY = 0;
                _mouseX = 0;

                float wantedRotationAngle = target.eulerAngles.y;
                float wantedHeight = target.position.y + _cameraHeight;

                float currentRotationAngle = transform.eulerAngles.y;
                float currentHeight = transform.position.y;

                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);

                currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

                var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

                //_myTransform.position = target.position;
                _myTransform.position -= currentRotation * Vector3.forward * _cameraDistance;

                _myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);

                _myTransform.LookAt(target);
            }
		}

		private void MouseOrbit()
		{
			_mouseX += Input.GetAxis ("Mouse X") *_mouseSpeedX * 0.02f;
			_mouseY -= Input.GetAxis ("Mouse Y") * _mouseSpeedY * 0.02f;

			Quaternion mouseRotation = Quaternion.Euler (_mouseY, _mouseX, 0);
			Vector3 mousePosition = mouseRotation * new Vector3 (0f, 0f, -_cameraDistance) + target.position;

			_myTransform.rotation = mouseRotation;
			_myTransform.position = mousePosition;
		}

    }
}

