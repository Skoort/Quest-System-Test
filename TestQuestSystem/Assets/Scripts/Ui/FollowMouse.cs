using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
	private Camera _mainCamera;

	private void Awake()
	{
		_mainCamera = Camera.main;
	}

	private void Update()
    {
		transform.position = Input.mousePosition;
    }
}
