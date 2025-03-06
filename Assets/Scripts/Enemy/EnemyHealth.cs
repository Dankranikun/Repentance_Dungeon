using UnityEngine;
using UnityEngine.AI; // Importar NavMeshAgent

public class EnemyHealth : MonoBehaviour
{
	public int maxHealth = 5;
	public int currentHealth;
	private bool isDead = false;
	private Animator animator;
	public AIEnemy enemyAI;
	private NavMeshAgent navMeshAgent;
	private Rigidbody rb;

	void Start()
	{
		currentHealth = maxHealth;
		animator = GetComponentInChildren<Animator>(); // Buscar Animator en hijos
		enemyAI = GetComponent<AIEnemy>();
		navMeshAgent = GetComponent<NavMeshAgent>(); // Buscar NavMeshAgent si existe
		rb = GetComponent<Rigidbody>(); // Buscar Rigidbody si existe
	}

	public void TakeDamage(int amount)
	{
		if (isDead) return; // No seguir dañando si ya está muerto

		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		if (isDead) return;
		isDead = true;

		Debug.Log("Enemigo ha muerto, activando animación...");

		// **Desactivar IA y detener el movimiento**
		if (enemyAI != null)
		{
			enemyAI.enabled = false;
			enemyAI.StopAllCoroutines();
		}

		// **Detener el NavMeshAgent si lo tiene**
		if (navMeshAgent != null)
		{
			navMeshAgent.isStopped = true; // Detiene el pathfinding
			navMeshAgent.enabled = false; // Desactiva el componente
		}

		// **Desactivar la física del Rigidbody**
		if (rb != null)
		{
			rb.linearVelocity = Vector3.zero; // Evita que siga deslizándose
			rb.angularVelocity = Vector3.zero; // Detiene la rotación
			rb.isKinematic = true; // Evita que interactúe con la física
		}

		// **Desactivar TODAS las colisiones**
		Collider[] colliders = GetComponentsInChildren<Collider>();
		foreach (Collider col in colliders)
		{
			col.enabled = false;
		}

		// **Reproducir animación de muerte**
		if (animator != null)
		{
			animator.SetBool("isDead", true);
		}
		else
		{
			Debug.LogError("No se encontró Animator en el enemigo.");
		}

		// **Destruir el enemigo después de la animación**
		Destroy(gameObject, 2.0f);
	}
}
