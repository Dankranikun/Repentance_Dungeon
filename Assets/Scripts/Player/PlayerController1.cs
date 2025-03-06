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
	public float playerSpeed;
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

		// Si hay datos guardados en PlayerPrefs, usa esa posición
		if (PlayerPrefs.HasKey("SpawnX"))
		{
			float x = PlayerPrefs.GetFloat("SpawnX");
			float y = PlayerPrefs.GetFloat("SpawnY");
			float z = PlayerPrefs.GetFloat("SpawnZ");
			Player.GetComponent<Rigidbody>().MovePosition(new Vector3(x, y, z));

		}
		else
		{
			// Si no hay datos guardados, inicia en el centro de la sala
			transform.position = new Vector3(0, 1.171f, 0);
		}

		UpdateInventoryUI();
		if (GameManager.Instance != null)
		{
			GameManager.Instance.RegisterPlayer(gameObject);
		}
	}

	void Update()
	{
		// Movimiento solo con WASD y Stick Izquierdo
		float moveX = Input.GetAxis("Horizontal"); // WASD / Stick Izquierdo
		float moveZ = Input.GetAxis("Vertical");   // WASD / Stick Izquierdo

		playerInput = new Vector3(moveX, 0, moveZ);
		playerInput = Vector3.ClampMagnitude(playerInput, 1);

		if (playerInput.magnitude > 1) playerInput.Normalize();

		camDirection();

		movePlayer = playerInput.x * camRight + playerInput.z * camForward;
		movePlayer *= playerSpeed;

		if (movePlayer.magnitude > 0.1f)
		{
			Player.transform.LookAt(Player.transform.position + movePlayer);
		}

		SetGravity();
		Player.Move(movePlayer * Time.deltaTime);
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