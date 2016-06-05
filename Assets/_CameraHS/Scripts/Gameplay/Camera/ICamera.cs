using UnityEngine;
using System.Collections;

namespace Gameplay.Camera
{
	public interface ICamera
	{
		Transform CameraTarget { get; set; }
		float CameraHeight { get; set; }
		float CameraDistance { get; set; }

		Vector3 StartCameraPostition (Transform target, float height, float distance);
	}

}

