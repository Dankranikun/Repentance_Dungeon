using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerController1 : MonoBehaviour
{
	// Entrada del jugador (Player Input)
	public float horizontalMove = 0f;
	public float verticalMove = 0f;
	public Vector3 playerInput;

	// Control del jugador (Player Control)
	public CharacterController Player;
	public float playerSpeed = 7.5f;
	private Vector3 movePlayer;
	public float gravity = 9.8f;
	public float fallVelocity;

	// Control de cámara (Camera Control)
	public Camera mainCamera;
	private Vector3 camForward;
	private Vector3 camRight;

	// Interfaz de usuario
	public TextMeshProUGUI collectedPickUps;
	public static int collectedCoins = 0;
	public static int collectedKeys = 0;
	public static int collectedBombs = 0;

	// Joystick virtual

	// Estadísticas del jugador
	public float playerMovSpeed = 8f;

	void Start()
	{
		Player = GetComponent<CharacterController>();
		playerSpeed = playerMovSpeed;

		// Buscar la cámara si no está asignada
		if (mainCamera == null)
		{
			mainCamera = Camera.main;
			Debug.Log("✅ Cámara encontrada y asignada");
		}
		else
		{
			Debug.Log("⚠️ Cámara ya estaba asignada");
		}

		UpdateInventoryUI();
		if (GameManager.Instance != null)
		{
			GameManager.Instance.RegisterPlayer(gameObject);
		}
	}

	void Update()
	{
		float moveX = 0f;
		float moveZ = 0f;

		// Teclado (WASD)
		if (Input.GetKey(KeyCode.W)) moveZ += 1f;
		if (Input.GetKey(KeyCode.S)) moveZ -= 1f;
		if (Input.GetKey(KeyCode.D)) moveX += 1f;
		if (Input.GetKey(KeyCode.A)) moveX -= 1f;

		// Mando / Joystick (sin smoothing)
		// moveX += Input.GetAxisRaw("Horizontal");
		// moveZ += Input.GetAxisRaw("Vertical");

		// Limitar a magnitud máxima de 1
		Vector3 moveDirection = new Vector3(moveX, 0, moveZ);
		if (moveDirection.magnitude > 1f)
		{
			moveDirection = moveDirection.normalized;
		}

		if (moveDirection.magnitude > 0.1f)
		{
			// Mover
			Vector3 movement = moveDirection * playerSpeed * Time.deltaTime;
			Player.Move(movement);

			// Rotar hacia la dirección
			Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
		}

		// AÑADIR ESTO: Detener velocidad del Rigidbody
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.linearVelocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
	}

	void camDirection()
	{
		camForward = mainCamera.transform.forward;
		camRight = mainCamera.transform.right;

		camForward.y = 0;
		camRight.y = 0;

		camForward = camForward.normalized;
		camRight = camRight.normalized;
	}

	void SetGravity()
	{
		if (Player.isGrounded)
		{
			fallVelocity = -gravity * Time.deltaTime;
			movePlayer.y = fallVelocity;
		}
		else
		{
			fallVelocity -= gravity * Time.deltaTime;
			movePlayer.y = fallVelocity;
		}
	}

	public void SetCamera(Camera cam)
	{
		mainCamera = cam;
		Debug.Log("Cámara asignada al jugador");
	}

	public static void AddCoin()
	{
		collectedCoins++;
		UpdateInventoryUI();
	}

	public static void AddKey()
	{
		collectedKeys++;
		UpdateInventoryUI();
	}

	public static void AddBomb()
	{
		collectedBombs++;
		UpdateInventoryUI();
	}

	private static void UpdateInventoryUI()
	{
		if (UIManager.instance != null)
		{
			UIManager.instance.UpdateUI(collectedCoins, collectedKeys, collectedBombs);
		}
	}
}