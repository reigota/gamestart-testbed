using UnityEngine;
using System.Collections;

namespace Gameplay.Camera
{
    public class OrbitalCameraController : MonoBehaviour 
	{
		[SerializeField] private BaseCamera baseCamera;
		[SerializeField] private Transform target;

        private Transform _myTransform;
		private float _mouseX;
		private float _mouseY;
		private bool _mousePressed = false;

        private void Awake()
		{
			baseCamera = GetComponent<BaseCamera> ();
            _myTransform = this.transform;
        }

		private void Start()
		{
            if (target == null)
                Debug.Log("Miss target:");

            InitializeCameraValues();

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
                float wantedHeight = target.position.y + baseCamera.CameraHeight;

                float currentRotationAngle = transform.eulerAngles.y;
                float currentHeight = transform.position.y;

                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, baseCamera.RotationDamping * Time.deltaTime);

                currentHeight = Mathf.Lerp(currentHeight, wantedHeight, baseCamera.HeightDamping * Time.deltaTime);

                var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

                _myTransform.position = target.position;
                _myTransform.position -= currentRotation * Vector3.forward * baseCamera.CameraDistance;

                _myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);

                _myTransform.LookAt(target);
            }
		}

		private void MouseOrbit()
		{
			_mouseX += Input.GetAxis ("Mouse X") * baseCamera.MouseSpeedX * 0.02f;
			_mouseY -= Input.GetAxis ("Mouse Y") * baseCamera.MouseSpeedY * 0.02f;

			Quaternion mouseRotation = Quaternion.Euler (_mouseY, _mouseX, 0);
			Vector3 mousePosition = mouseRotation * new Vector3 (0f, 0f, -baseCamera.CameraDistance) + target.position;

			_myTransform.rotation = mouseRotation;
			_myTransform.position = mousePosition;
		}

        private void InitializeCameraValues()
        {
            baseCamera.CameraTarget = target;
            baseCamera.CameraHeight = 4f;
            baseCamera.CameraDistance = 6f;
            baseCamera.MouseSpeedX = 250f;
            baseCamera.MouseSpeedY = 120f;
            baseCamera.HeightDamping = 2.0f;
            baseCamera.RotationDamping = 3.0f;
        }

    }
}

