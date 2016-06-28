using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
    //Mouse variables
    private float _mouseX;
    private float _mouseY;
    private float _mouseSpeedX;
    private float _mouseSpeedY;

    //Control variables
    private bool _mousePressed;

    //Damps variables for smooth movement
    private float _heigthDamp;
    private float _rotationDamp;

    private float _cameraDistance;

    //Inspector variables
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform[] cameraPositions;
    [SerializeField] private float cameraSpeed;

    //Phiscs variables
    private int _positionIndex = 0;
    private RaycastHit _hit;

    private void Awake()
    {
        _mouseSpeedX = 250f;
        _mouseSpeedY = 120f;
        _cameraDistance = 4f;
}

    private void Start()
    {
        if (cameraTarget == null)
            Debug.Log("Miss target:");
    }

    private void Update()
    {
        ChangeCameraInput();
        CheckMouseOrbit();
    }

    private void FixedUpdate()
    {
        CameraHitDetection();
        transform.LookAt(cameraTarget);
        
        if (_mousePressed)
            MouseOrbit();

            
    }

    private void CheckMouseOrbit()
    {
        if (Input.GetMouseButtonDown(1))
            _mousePressed = true;

        if (Input.GetMouseButtonUp(1))
            _mousePressed = false;
    }

    private void ChangeCameraInput()
    {
        if (Input.GetKeyDown("e") && _positionIndex < (cameraPositions.Length - 1))
            _positionIndex++;

        else if (Input.GetKeyDown("e") && _positionIndex >= (cameraPositions.Length - 1))
            _positionIndex = 0;
    }

    private void MouseOrbit()
    {
        
        _mouseX += Input.GetAxis("Mouse X") * _mouseSpeedX * 0.02f;
        _mouseY -= Input.GetAxis("Mouse Y") * _mouseSpeedY * 0.02f;

        Quaternion mouseRotation = Quaternion.Euler(_mouseY, _mouseX, 0);
        Vector3 mousePosition =  cameraPositions[_positionIndex].position;

        transform.LookAt(cameraTarget);
        transform.rotation = mouseRotation;
        transform.position = mousePosition;
    }

    private void CameraHitDetection()
    {
        if (!Physics.Linecast(cameraTarget.position, cameraPositions[_positionIndex].position))
        {
            transform.position = Vector3.Lerp(transform.position, cameraPositions[_positionIndex].position, cameraSpeed * Time.deltaTime);
            Debug.DrawLine(cameraTarget.position, cameraPositions[_positionIndex].position);
        }
        else if (Physics.Linecast(cameraTarget.position, cameraPositions[_positionIndex].position, out _hit))
        {
            transform.position = Vector3.Lerp(transform.position, _hit.point, (cameraSpeed * 2.0f) * Time.deltaTime);
            Debug.DrawLine(cameraTarget.position, _hit.point);
        }
    }








}
