using UnityEngine;
using System.Collections;

namespace Gameplay.Camera
{
    public class OrbitalCameraController : MonoBehaviour 
	{
		[SerializeField] private BaseCamera baseCamera;
		[SerializeField] private Transform target;
		[SerializeField] private float mouseSpeedX = 250f;
	    [SerializeField] private float mouseSpeedY = 120f;
        [SerializeField] private float heightDamping = 2.0f;
        [SerializeField] private float rotationDamping = 3.0f;

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
            if (target == null)
                return;

            if (_mousePressed) 
				MouseOrbit ();

            else
            {
                _mouseY = 0;
                _mouseX = 0;

                // Calculate the current rotation angles
                float wantedRotationAngle = target.eulerAngles.y;
                float wantedHeight = target.position.y + baseCamera.CameraHeight;

                float currentRotationAngle = transform.eulerAngles.y;
                float currentHeight = transform.position.y;

                // Damp the rotation around the y-axis
                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

                // Damp the height
                currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

                // Convert the angle into a rotation
                var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

                // Set the position of the camera on the x-z plane to:
                // distance meters behind the target
                _myTransform.position = target.position;
                _myTransform.position -= currentRotation * Vector3.forward * baseCamera.CameraDistance;

                // Set the height of the camera
                _myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);

                // Always look at the target
                _myTransform.LookAt(target);
            }
		}

		private void MouseOrbit()
		{
			_mouseX += Input.GetAxis ("Mouse X") * mouseSpeedX * 0.02f;
			_mouseY -= Input.GetAxis ("Mouse Y") * mouseSpeedY * 0.02f;

			Quaternion mouseRotation = Quaternion.Euler (_mouseY, _mouseX, 0);
			Vector3 mousePosition = mouseRotation * new Vector3 (0f, 0f, -baseCamera.CameraDistance) + target.position;

			_myTransform.rotation = mouseRotation;
			_myTransform.position = mousePosition;
		}
	}
}

