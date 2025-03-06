using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public int health;
	public int maxHealth = 10;
	Animator animator;

	public GameObject playerModel;
	public PlayerController1 playerController1;

	void Start()
	{
		health = 5;
		animator = GetComponentInChildren<Animator>();
		//animator = GetComponent<Animator>();
	}
	public void TakeDamage(int amount)
	{
		health -= amount;

		if (health <= 0)
		{
			Debug.Log("⚰ Jugador muerto. Activando animación..."); // 🔹 Verifica si se ejecuta
			Debug.Log("Animator encontrado? " + (animator != null)); // 🔹 Asegura que no es null

			animator.SetBool("isDead", true); // 🔹 Activa la animación de muerte
			playerController1.enabled = false; // 🔹 Desactiva controles

			CharacterController characterController = GetComponent<CharacterController>();
			if (characterController != null)
			{
				characterController.enabled = false; // 🔹 Desactiva el CharacterController
			}

			Rigidbody rb = GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.linearVelocity = Vector3.zero; // 🔹 Detiene cualquier movimiento
				rb.isKinematic = true; // 🔹 Evita que siga recibiendo fuerzas físicas
			}
		}
	}



}
