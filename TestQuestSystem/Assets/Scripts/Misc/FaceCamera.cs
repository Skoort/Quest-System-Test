using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
	private static Camera _mainCamera;

	private void Awake()
	{
		_mainCamera = Camera.main;
	}

	private void Update()
    {
		transform.LookAt(_mainCamera.transform.position);
    }
}
