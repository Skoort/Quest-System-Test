using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScript : MonoBehaviour
{
	[SerializeField] private CharacterController _controller;

	private Vector3 _moveDir;
	public float Speed { get; set; } = 5F;
	private float _gravity = 20F;

	[SerializeField] private float _jumpForce = 10F;
	private float _vertVelocity;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
	}

	private void Update()
	{
		MoveThePlayer();
	}

	private void MoveThePlayer()
	{
		_moveDir = new Vector3(
			Input.GetAxis("Horizontal"),
			0,
			Input.GetAxis("Vertical"));

		if (GameInfo.Instance.IsInMenu)
		{
			_moveDir = Vector3.zero;
		}


		_moveDir = transform.TransformDirection(_moveDir);
		_moveDir *= Speed * Time.deltaTime;

		ApplyGravity();

		_controller.Move(_moveDir);
	}

	private void ApplyGravity()
	{
		if (_controller.isGrounded)
		{
			_vertVelocity -= _gravity * Time.deltaTime;//= -0.2F;  // A small constant downward velocity to smooth out any bumps.

			if (!GameInfo.Instance.IsInMenu)
			{ 
				PlayerJump();
			}
		}
		else
		{
			_vertVelocity -= _gravity * Time.deltaTime;
		}

		_moveDir.y = _vertVelocity * Time.deltaTime;
	}

	private void PlayerJump()
	{
		if (_controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
		{
			_vertVelocity = _jumpForce;
		}
	}
}
