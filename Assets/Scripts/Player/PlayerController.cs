using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
	public Camera playerCamera;

	[Header("Player Settings")]
	public float radius = 0.2f;
	public float height = 2.0f;
	public float crouchHeight = 1.0f;

	[Header("Player Speeds")]
	public float crouchSpeed = 4.5f;
	public float walkingSpeed = 7.5f;
	public float runningSpeedMultiplier = 1.2f;
	public float lookSpeed = 4.0f;

	[Header("Player Limits")]
	public float jumpHeight = 1f;
	public float lookYLimit = 85.0f;

	[Header("Player Inversions")]
	public bool invertY = false;
	public bool invertX = false;

	[Header("Menu's")]
	public GameObject SettingsMenu;

	[Header("Inputs")]
	public bool hasJump;
	public bool isRunning;
	public bool isCrouched;
	public bool isPrimaryInteracting;
	public bool isDropping;
	public bool isSecondaryInteractingToggled;
	public bool isSettingsPressed;

	private bool canMove = true;
	private CharacterController cc;
	
	private Vector3 velocity = Vector3.zero;
	private float rotationX = 0;

	private Vector2 input;
	private Vector2 lookInput;

    private Vector3 Forward => transform.TransformDirection(Vector3.forward);
	private Vector3 Right => transform.TransformDirection(Vector3.right);

	public UnityEvent primaryInteract = new UnityEvent();
	public UnityEvent drop = new UnityEvent();
	public UnityEvent<bool> secondaryInteract = new UnityEvent<bool>();

    private void Awake()
	{
		cc = GetComponent<CharacterController>();
	}

	private void Start()
	{
		cc.radius = radius;
		cc.height = height;
		cc.skinWidth = radius * .1f;

		SetCanMove(true);
	}

	private void Update()
	{
		ProcessPlayerRotation();
		ProcessPlayerMovement();
	}

	public void SetCanMove(bool enabled)
	{
		canMove = enabled;

        if (enabled)
        {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
        }
        else
        {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	private void ApplyGravityAndJump()
	{
		if (hasJump && cc.isGrounded)
		{
			//print("Jump");
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
			return;
		}
		else if (!cc.isGrounded || !hasJump && velocity.y > 0)
		{
			//print(velocity);
			velocity += Physics.gravity * Time.deltaTime;
			return;
		}
		velocity.y = -.5f;
	}

	public void Teleport(Vector3 position, Quaternion rotation)
    {
		cc.enabled = false;
		transform.SetPositionAndRotation(position, rotation);
		cc.enabled = true;
    }

	private void ProcessPlayerRotation()
	{
		if (canMove)
		{
			rotationX += lookInput.y * lookSpeed;
			rotationX = Mathf.Clamp(rotationX, -lookYLimit, lookYLimit);
			playerCamera.transform.localRotation = Quaternion.Euler(invertY ? rotationX : -rotationX, 0, 0);
			playerCamera.transform.rotation = Quaternion.LookRotation(playerCamera.transform.forward, Vector3.up);

			var rotationY = lookInput.x * lookSpeed;
			transform.rotation *= Quaternion.Euler(0, invertX ? -rotationY : rotationY, 0);
		}
	}

	private void ProcessPlayerMovement()
	{
		cc.height = isCrouched ? crouchHeight : height;
		cc.center = new Vector3(0, cc.height / 2, 0);
		playerCamera.transform.localPosition = new Vector3(0, cc.height - .5f, 0);

		var baseSpeed = isCrouched ? crouchSpeed : walkingSpeed;

		var playerSpeed = baseSpeed * (isRunning ? runningSpeedMultiplier : 1);
		var inputVelocity = (Forward * input.y + Right * input.x).normalized * playerSpeed;
		velocity = new Vector3(inputVelocity.x, velocity.y, inputVelocity.z);
		ApplyGravityAndJump();

		if (canMove) cc.Move(velocity * Time.deltaTime);
	}

	public void OnMovement(InputValue value)
    {
		input = canMove ? value.Get<Vector2>() : Vector2.zero;
    }

	public void OnLook(InputValue value)
    {
		lookInput = value.Get<Vector2>();
    }

	public void OnJump(InputValue value)
    {
		hasJump = canMove && value.Get<float>() == 1;
		print(value);
    }

	public void OnSprint(InputValue value)
    {
		isRunning = canMove && value.Get<float>() == 1;
    }

	public void OnCrouch(InputValue value)
    {
		isCrouched = canMove && value.Get<float>() == 1;
	}

	public void OnInteract(InputValue value)
	{
		isPrimaryInteracting = value.Get<float>() == 1;
		if (isPrimaryInteracting) primaryInteract?.Invoke();
	}

	public void OnDrop(InputValue value)
	{
		isDropping = value.Get<float>() == 1;
		if (isDropping) drop?.Invoke();
	}

	public void OnFlashlight(InputValue value)
	{
		if (value.isPressed)
		{
			isSecondaryInteractingToggled = !isSecondaryInteractingToggled;
			secondaryInteract?.Invoke(isSecondaryInteractingToggled);
		}
	}

	public void OnSettings(InputValue value)
	{
		if (value.isPressed)
        {
			var puzzles = FindObjectsOfType<Puzzle2D>().Where(p => p.IsOpen).ToArray();
			for (int i = 0; i < puzzles.Length; i++) puzzles[i].Toggle();
			InteractablesManager.Instance?.noteScreen?.Active(false);
			SettingsMenu.SetActive(!SettingsMenu.activeSelf);
			SetCanMove(!SettingsMenu.activeSelf);
        }
	}
}
