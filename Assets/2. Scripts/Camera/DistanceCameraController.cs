using UnityEngine;
using System.Collections;

public class DistanceCameraController : MonoBehaviour 
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform[] cameraPositions;
    [SerializeField] private float cameraSpeed;

    private int _positionIndex = 0;
    private RaycastHit _hit;

    private void Update()
    {
        ChangeCameraInput();
    }
    
    private void FixedUpdate()
    {
        transform.LookAt(cameraTarget);
        CameraHitDetection();
    }

    private void ChangeCameraInput()
    {
        if (Input.GetKeyDown("e") && _positionIndex < (cameraPositions.Length - 1))
            _positionIndex++;
        
        else if (Input.GetKeyDown("e") && _positionIndex >= (cameraPositions.Length - 1))
            _positionIndex = 0;
    }

    private void CameraHitDetection()
    {
        if(!Physics.Linecast(cameraTarget.position,cameraPositions[_positionIndex].position))
        {
            transform.position = Vector3.Lerp(transform.position, cameraPositions[_positionIndex].position, cameraSpeed * Time.deltaTime);
            Debug.DrawLine(cameraTarget.position, cameraPositions[_positionIndex].position);
        }
        else if(Physics.Linecast(cameraTarget.position, cameraPositions[_positionIndex].position,out _hit))
        {
            transform.position = Vector3.Lerp(transform.position,_hit.point, (cameraSpeed * 2.0f) * Time.deltaTime);
            Debug.DrawLine(cameraTarget.position, _hit.point);
        }
    }

}
