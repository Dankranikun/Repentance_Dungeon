using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerController1 : MonoBehaviour
{
    // Entrada del jugador (Player Input)
    public float horizontalMove;
    public float verticalMove;
    public Vector3 playerInput;

    // Control del jugador (Player Control)
    public CharacterController player;
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

    // Animaciones del personaje



    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        // "Des-suavizar" el movimiento del jugador
        if (playerInput.magnitude > 1) playerInput.Normalize();

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();

        player.Move(movePlayer * Time.deltaTime);

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
        if (player.isGrounded)
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
}
