using UnityEngine;
using UnityEngine.AI;
using System.Collections; // Necesario para usar corrutinas

public class AIEnemy : MonoBehaviour
{
	public NavMeshAgent navMeshAgent;
	public Animator animator;
	public float attackRange = 2.0f;

	private Transform player;
	public PlayerHealth playerHealth; // Referencia al PlayerHealth

	public bool isWalking = false;
	public bool isAttacking = false;
	public int damageDeal = 1;

	void Start()
	{
		animator = GetComponent<Animator>();

		// Iniciar la corrutina para esperar al jugador
		StartCoroutine(WaitForPlayer());
	}

	IEnumerator WaitForPlayer()
	{
		// Esperar hasta que GameManager tenga al Player
		while (GameManager.Instance == null || GameManager.Instance.player == null)
		{
			Debug.LogWarning("⏳ Esperando a que el jugador se genere...");
			yield return null; // Esperar un frame
		}

		// Una vez que el jugador está disponible, asignarlo
		player = GameManager.Instance.player.transform;
		playerHealth = GameManager.Instance.player.GetComponent<PlayerHealth>();

		if (playerHealth == null)
		{
			Debug.LogError("❌ El jugador no tiene un componente PlayerHealth.");
		}
		else
		{
			Debug.Log("✅ Jugador asignado correctamente.");
		}

		// Asegurar que el NavMeshAgent tenga un destino válido
		if (player != null)
		{
			navMeshAgent.SetDestination(player.position);
		}
	}

	void Update()
	{
		if (player == null) return;

		navMeshAgent.SetDestination(player.position);

		isWalking = navMeshAgent.velocity.magnitude > 0.1f;
		animator.SetBool("isWalking", isWalking);

		isAttacking = Vector3.Distance(transform.position, player.position) <= attackRange;
		animator.SetBool("isAttacking", isAttacking);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")) // Asegurar que estamos detectando al jugador
		{
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>(); // Buscar el PlayerHealth en el jugador

			if (playerHealth != null)
			{
				playerHealth.TakeDamage(damageDeal);
				Debug.Log("⚔ Jugador dañado: -" + damageDeal);
			}
			else
			{
				Debug.LogWarning("⚠ No se encontró PlayerHealth en " + other.name);
			}
		}
	}


}
