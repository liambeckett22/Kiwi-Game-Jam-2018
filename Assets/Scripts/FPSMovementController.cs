using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSMovementController : MonoBehaviour
{
	public static bool useMouseLook = true;

	[SerializeField] private float horizontalRotateSensitivity = 2f;
	[SerializeField] private float verticalRotateSensitivity = 2f;

	[SerializeField] private float jumpSpeed = 1f;
	[SerializeField] private float moveSpeed = 5f;

	[SerializeField] [Range(0, 1.0f)] private float groundSpeedDampener = 0.24f;
	[SerializeField] [Range(0, 1.0f)] private float airSpeedDampener = 0.12f;

	[SerializeField] [Range(0, 1.0f)] private float gravityDampener = 0.5f;
    private float oldGravityDamper;

    [SerializeField] private Transform characterModel;
	[SerializeField] private Transform characterCamera;

	private CharacterController characterController;
	private Vector3 verticalMomentum = Vector3.zero;

	private bool isGrounded;
	private CollisionFlags flags;

	private static HashSet<KeyCode> DirectionKeys = new HashSet<KeyCode>()
	{
		KeyCode.W,
		KeyCode.S,
		KeyCode.A,
		KeyCode.D,
	};

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
        oldGravityDamper = gravityDampener;

    }

	private void Start()
	{
		Cursor.visible = false;
		StartCoroutine(RotateByMouseMovement(horizontalRotateSensitivity, verticalRotateSensitivity));
	}

	private void Update()
	{
		UpdateJumping();

		var moveDirection = GetMoveDirection();
		if (moveDirection.sqrMagnitude > 0)
		{
			moveDirection.Normalize();
			float dampener = isGrounded ? groundSpeedDampener : airSpeedDampener;
			flags = characterController.Move(moveDirection * dampener * moveSpeed * Time.deltaTime);
		}
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Contains("FallingFloorSquare"))
        {
            gravityDampener = 1;
            moveSpeed = moveSpeed /2;

        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name.Contains("FallingFloorSquare"))
        {
            gravityDampener = oldGravityDamper;
            moveSpeed = moveSpeed * 2;

        }
    }

    private void UpdateJumping()
	{
		if (isGrounded)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				verticalMomentum = Vector3.up * jumpSpeed;
				isGrounded = false;
				flags = characterController.Move(verticalMomentum * Time.deltaTime);
			}

            if (!characterController.isGrounded)
            {
                verticalMomentum += Physics.gravity * gravityDampener * Time.deltaTime;
                flags = characterController.Move(verticalMomentum * Time.deltaTime);
                if ((flags & CollisionFlags.Below) != 0)
                {
                    verticalMomentum = Vector3.zero;
                    isGrounded = true;
                }
            }
		}
		else
		{
			verticalMomentum += Physics.gravity * gravityDampener * Time.deltaTime;
			flags = characterController.Move(verticalMomentum * Time.deltaTime);
			if (( flags & CollisionFlags.Below ) != 0)
			{
				verticalMomentum = Vector3.zero;
				isGrounded = true;
			}
		}
	}

	private Vector3 GetMoveDirection()
	{
		var moveDirection = Vector3.zero;
		foreach (KeyCode key in DirectionKeys)
		{
			moveDirection += GetDirectionFromInput(key);
		}

		return moveDirection;
	}

	private Vector3 GetDirectionFromInput(KeyCode key)
	{
		Vector3 direction = Vector3.zero;
		if (Input.GetKey(key))
		{
			switch (key)
			{
				case KeyCode.W:
					direction = transform.forward;
					break;
				case KeyCode.S:
					direction = -transform.forward;
					break;
				case KeyCode.A:
					direction = -transform.right;
					break;
				case KeyCode.D:
					direction = transform.right;
					break;
				default:
					break;
			}
		}

		return direction;
	}

	private IEnumerator RotateByMouseMovement(float horizontalSensitivity, float verticalSensitivity)
	{
		while (useMouseLook)
		{
			float yRot = Input.GetAxis("Mouse X") * horizontalSensitivity;
			transform.localRotation *= Quaternion.Euler(0f, yRot, 0f);

			float xRot = Input.GetAxis("Mouse Y") * verticalSensitivity;
			characterCamera.localRotation = ClampVerticalRotation(characterCamera.localRotation * Quaternion.Euler(-xRot, 0f, 0f), -60, 60);

			yield return null;
		}

		yield break;
	}

	private Quaternion ClampVerticalRotation(Quaternion q, float minimum, float maximum)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

		angleX = Mathf.Clamp(angleX, minimum, maximum);

		q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}
}