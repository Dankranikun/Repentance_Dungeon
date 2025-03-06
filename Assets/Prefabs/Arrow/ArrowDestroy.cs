using UnityEngine;

public class Arrow : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
			// Quitar vida enemigos
			other.gameObject.GetComponent<EnemyHealth>().TakeDamage(2);

			Debug.Log("Colisión con: " + other.gameObject.name + " | Tag: " + other.gameObject.tag);
		}

		// Destruir la flecha al chocar con cualquier objeto
		Destroy(gameObject, 0.25f);
	}
}