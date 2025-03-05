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
	public Joystick joystick;

	// Estadísticas del jugador
	public float playerMovSpeed = 8f;
	//public float playerAttSpeed = 5f;
	//public float playerAttRange = 5f;

	void Start()
	{
		Player = GetComponent<CharacterController>();

		UpdateInventoryUI();
		if (GameManager.Instance != null)
		{
			GameManager.Instance.RegisterPlayer(gameObject);
		}
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

	void Update()
	{

		if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
		{
			horizontalMove = Input.GetAxisRaw("Horizontal"); // WASD / Stick Izquierdo
			verticalMove = Input.GetAxisRaw("Vertical");   // WASD / Stick Izquierdo
		}


		playerInput = new Vector3(horizontalMove, 0, verticalMove);
		playerInput = Vector3.ClampMagnitude(playerInput, 1);

		// "Des-suavizar" el movimiento del jugador
		if (playerInput.magnitude > 1) playerInput.Normalize();

		camDirection();

		movePlayer = playerInput.x * camRight + playerInput.z * camForward;

		movePlayer = movePlayer * playerSpeed;

		Player.transform.LookAt(Player.transform.position + movePlayer);

		SetGravity();

		Player.Move(movePlayer * Time.deltaTime);

		if (collectedPickUps != null)
		{
			collectedPickUps.text = "Coins: " + collectedCoins + "\nKeys: " + collectedKeys + "\nBombs: " + collectedBombs;
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
}