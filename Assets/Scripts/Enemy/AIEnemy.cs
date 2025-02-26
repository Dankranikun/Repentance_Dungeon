using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public float attackRange = 2.0f; // Distancia a la que el enemigo ataca

    private Transform player; // Referencia al jugador

    public bool isWalking = false;
    public bool isAttacking = false;

    void Start()
    {
        // Obtener referencia del Animator
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró un Animator en " + gameObject.name);
        }

        // Buscar al jugador si existe
        if (PlayerAggro.Instance != null)
        {
            player = PlayerAggro.Instance.transform;
            navMeshAgent.destination = player.position;
        }
        else
        {
            Debug.LogWarning("PlayerAggro no encontrado.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Moverse hacia el jugador
        navMeshAgent.destination = player.position;

        // Verificar si el enemigo se está moviendo y actualizar la variable de clase
        isWalking = navMeshAgent.velocity.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);

        // Verificar si el enemigo está cerca para atacar y actualizar la variable de clase
        isAttacking = Vector3.Distance(transform.position, player.position) <= attackRange;
        animator.SetBool("isAttacking", isAttacking);
    }

}
