using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public float attackRange = 2.0f; // Distancia a la que el enemigo ataca

    private Transform player; // Referencia al jugador
    public PlayerHealth playerHealth; //Reference to the player health in another script

    public bool isWalking = false;
    public bool isAttacking = false;

    public int damageDeal = 1;

    void Start()
    {
        // Obtener referencia del Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró un Animator en " + gameObject.name);
        }

        // Buscar al jugador
        if (PlayerAggro.Instance != null)
        {
            player = PlayerAggro.Instance.transform;
        }
        else
        {
            Debug.LogWarning("PlayerAggro no encontrado, buscando manualmente al jugador...");
            GameObject playerObject = GameObject.FindWithTag("Player"); // Buscar por tag
            if (playerObject != null)
            {
                player = playerObject.transform;
                Debug.Log("Jugador encontrado manualmente.");
            }
            else
            {
                Debug.LogError("No se encontró ningún objeto con el tag 'Player'.");
            }
        }

        // Asegurar que el NavMeshAgent tiene un destino válido
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    void Update()
    {
        if (player == null) return; // Ahora player siempre debería existir

        // Asegurar que el NavMeshAgent se mueva correctamente
        navMeshAgent.SetDestination(player.position);

        // Verificar si el enemigo se está moviendo y actualizar la variable de clase
        isWalking = navMeshAgent.velocity.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);

        // Verificar si el enemigo está cerca para atacar y actualizar la variable de clase
        isAttacking = Vector3.Distance(transform.position, player.position) <= attackRange;
        animator.SetBool("isAttacking", isAttacking);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerDamage")
        {
            playerHealth.TakeDamage(damageDeal);
        }
    }
}
