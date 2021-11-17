using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookScript : MonoBehaviour
{
	[SerializeField] private Transform _playerRoot = null;
	[SerializeField] private Transform _lookRoot = null;

	[SerializeField] private bool _invertPitch = false;
	[SerializeField] private bool _canUnlock = true;
	[SerializeField] private float _sensitivity = 5F;
	[SerializeField] private int _smoothSteps = 10;
	[SerializeField] private float _smoothWeight = 0.4F;
	[SerializeField] private float _rollAngle = 3F;
	[SerializeField] private float _rollSpeed = 10F;
	[SerializeField]
	private Vector2 _defaultLookLimits =
		new Vector2(-70F, 80F);

	private Vector2 _lookAngles;
	private Vector2 _currMouseLook;
	private Vector2 _smoothMove;
	private float _currentRollAngle;
	private int _lastLookFrame;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		if (GameInfo.Instance.IsInMenu)
		{
			return;
		}

		LockAndUnlockCursor();

		if (Cursor.lockState == CursorLockMode.Locked)
		{
			LookAround();
		}
	}

	private void LockAndUnlockCursor()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				Cursor.lockState = CursorLockMode.None;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}
	}

	private void LookAround()
	{
		_currMouseLook = new Vector2(
			Input.GetAxis("Mouse Y"),
			Input.GetAxis("Mouse X"));
		_lookAngles.x += _currMouseLook.x * _sensitivity * (_invertPitch ? +1F : -1F);
		_lookAngles.y += _currMouseLook.y * _sensitivity;

		_lookAngles.x = Mathf.Clamp(
			_lookAngles.x,
			_defaultLookLimits.x,
			_defaultLookLimits.y);

		_currentRollAngle =
			Mathf.Lerp(_currentRollAngle,
				Input.GetAxisRaw("Mouse X") * _rollAngle,
				Time.deltaTime * _rollSpeed);

		_playerRoot.localRotation = Quaternion.Euler(0, _lookAngles.y, 0);
		_lookRoot.localRotation = Quaternion.Euler(_lookAngles.x, 0, _currentRollAngle);
	}
}
