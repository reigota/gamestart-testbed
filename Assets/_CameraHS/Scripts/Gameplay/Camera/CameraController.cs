using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    //Mouse variables
    private float _angleV;
    private float _angleH;
    [SerializeField]
    private float _mouseSensibility = 10f;

    //Control variables
    private bool _mousePressed;

    //Damps variables for smooth movement
    private float _heigthDamp;
    private float _rotationDamp;

    private float _cameraDistance;

    //Inspector variables
    [SerializeField]
    private Transform cameraTarget;
    [SerializeField]
    private Transform[] cameraPositions;
    [SerializeField]
    private float cameraSpeed;

    //Phiscs variables
    private int _positionIndex = 0;
    private RaycastHit _hit;

    private void Awake()
    {

        _cameraDistance = 4f;
    }

    private void Start()
    {
        if(cameraTarget == null)
            Debug.Log("Miss target:");
    }

    private void Update()
    {
        ChangeCameraInput();
        CheckMouseOrbit();
    }

    private void FixedUpdate()
    {
        //CameraHitDetection();
        
    }

    public void LateUpdate()
    {
        if(_mousePressed)
            MouseOrbit();
        // todo: smooth
        transform.LookAt(cameraTarget);
    }

    private void CheckMouseOrbit()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(_mousePressed == false)
                _angleH = _angleV = 0f;
            _mousePressed = true;
        }

        if(Input.GetMouseButtonUp(1))
            _mousePressed = false;
    }

    private void ChangeCameraInput()
    {
        if(Input.GetKeyDown("e") && _positionIndex < (cameraPositions.Length - 1))
            _positionIndex++;

        else if(Input.GetKeyDown("e") && _positionIndex >= (cameraPositions.Length - 1))
            _positionIndex = 0;
    }

    private void MouseOrbit()
    {

        _angleV += Input.GetAxis("Mouse X") * _mouseSensibility * Time.deltaTime;
        _angleH += Input.GetAxis("Mouse Y") * _mouseSensibility * Time.deltaTime;

        Quaternion rot = Quaternion.Euler(-_angleH, _angleV, 0);

        transform.position = cameraTarget.position +
                            rot * cameraPositions[_positionIndex].localPosition;

        transform.LookAt(cameraTarget);

    }

    private void CameraHitDetection()
    {
        if(!Physics.Linecast(cameraTarget.position, cameraPositions[_positionIndex].position))
        {
            transform.position = Vector3.Lerp(transform.position, cameraPositions[_positionIndex].position, cameraSpeed * Time.deltaTime);
            Debug.DrawLine(cameraTarget.position, cameraPositions[_positionIndex].position);
        }
        else if(Physics.Linecast(cameraTarget.position, cameraPositions[_positionIndex].position, out _hit))
        {
            transform.position = Vector3.Lerp(transform.position, _hit.point, (cameraSpeed * 2.0f) * Time.deltaTime);
            Debug.DrawLine(cameraTarget.position, _hit.point);
        }
    }








}
