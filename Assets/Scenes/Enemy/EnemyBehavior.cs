using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
	public NavMeshAgent agent;
	public Transform player;
	public LayerMask whatIsGround, whatIsPlayer;

	// Patroling
	public Vector3 walkPoint;
	bool walkPointSet;
	public float walkPointRange;

	// Attack
	public float timeBetweenAttacks;
	bool alreadyAttacked;
	bool attackAnimation;

	// States
	public float sightRange, attackRange;
	public bool playerInSightRange, playerInAttackRange;

	void Awake()
	{
		player = GameObject.Find("PlayerObj").transform;
		agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		// Check for sight and range attack
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

		if (!playerInSightRange && !playerInAttackRange) Patroling();
		if (playerInSightRange && !playerInAttackRange) ChasePlayer();
		if (playerInSightRange && playerInAttackRange) AttackPlayer();
	}

	private void Patroling()
	{
		if (!walkPointSet) SearchWalkPoint();

		if (walkPointSet) agent.SetDestination(walkPoint);

		Vector3 distanceToWalkPoint = transform.position - walkPoint;

		// Walkpoint reached
		if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
	}

	private void SearchWalkPoint()
	{
		float randomZ = Random.Range(-walkPointRange, walkPointRange);
		float randomX = Random.Range(-walkPointRange, walkPointRange);

		walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

		if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
		{
			walkPointSet = true;
		}
	}


	private void ChasePlayer()
	{
		agent.SetDestination(player.position);
	}
	private void AttackPlayer()
	{
		// Make sure the enemy doesn't move
		// agent.SetDestination(transform.position);

		transform.LookAt(player);

		if (!alreadyAttacked)
		{
			// Código para que ataque
			attackAnimation = true;



			alreadyAttacked = true;
			Invoke(nameof(ResetAttack), timeBetweenAttacks);
		}
	}

	public void ResetAttack()
	{
		alreadyAttacked = false;
		attackAnimation = false;
	}
}
