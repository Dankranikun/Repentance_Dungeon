using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public int health;
	public int maxHealth = 10;
	private bool isDead = false; // Para evitar múltiples llamadas a la muerte

	private Animator animator;
	private GameOverManager gameOverManager;

	public GameObject playerModel;
	public PlayerController1 playerController1;

	void Start()
	{
		health = 5;
		animator = GetComponentInChildren<Animator>();

		// Buscar automáticamente el GameOverManager en la escena
		gameOverManager = FindFirstObjectByType<GameOverManager>();
	}

	public void TakeDamage(int amount)
	{
		if (isDead) return; // Si ya está muerto, no sigue recibiendo daño

		health -= amount;

		if (health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		isDead = true; // Evita múltiples ejecuciones

		Debug.Log("⚰ Jugador muerto. Activando animación...");
		Debug.Log("Animator encontrado? " + (animator != null));

		if (animator != null)
		{
			animator.SetBool("isDead", true); // 🔹 Activa la animación de muerte
		}

		// 🔹 Desactivar controles del jugador
		if (playerController1 != null)
		{
			playerController1.enabled = false;
		}

		// 🔹 Desactivar CharacterController si existe
		CharacterController characterController = GetComponent<CharacterController>();
		if (characterController != null)
		{
			characterController.enabled = false;
		}

		// 🔹 Desactivar Rigidbody si existe
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.linearVelocity = Vector3.zero; // 🔹 Detiene cualquier movimiento
			rb.isKinematic = true; // 🔹 Evita que siga recibiendo fuerzas físicas
		}

		// 🔹 Verificar y asignar GameOverManager si es necesario
		if (gameOverManager == null)
		{
			gameOverManager = FindFirstObjectByType<GameOverManager>();
		}

		// 🔹 Mostrar pantalla de Game Over
		if (gameOverManager != null)
		{
			Debug.Log("🛑 Mostrando pantalla de Game Over...");
			gameOverManager.ShowGameOver();
		}
		else
		{
			Debug.LogError("❌ GameOverManager no encontrado en la escena.");
		}
	}

	public void ResetHealthAndPosition()
	{
		Debug.Log("🔄 Reiniciando jugador: Vida y posición...");

		health = 5; // Restaurar la vida a 5
		isDead = false; // Resetear estado de muerte
		playerController1.enabled = true; // Reactivar controles

		// Reactivar el CharacterController si existe
		CharacterController characterController = GetComponent<CharacterController>();
		if (characterController != null)
		{
			characterController.enabled = true;
		}

		// Reactivar Rigidbody si existe
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.isKinematic = false; // Permitir movimiento
			rb.linearVelocity = Vector3.zero; // Asegurar que no se mueve de forma inesperada
		}

		// Reiniciar animaciones
		if (animator != null)
		{
			animator.SetBool("isDead", false);
		}

		// 🔹 Mover al jugador a la posición de inicio
		transform.position = new Vector3(0, 1.2f, 0);

		Debug.Log("✅ Jugador reseteado correctamente.");
	}

}
