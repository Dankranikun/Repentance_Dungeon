using UnityEngine;

public class Shoot : MonoBehaviour
{
	public Transform spawnPoint; // Punto donde se generan las flechas
	public GameObject projectile; // Prefab de la flecha

	public float shootForce = 10f; // Fuerza del disparo
	public float shootRate = 0.5f; // Tiempo entre disparos (cooldown)
	private float nextShootTime = 0f; // Controla cuándo se puede disparar de nuevo
	private Vector3 shootDirection = Vector3.zero; // Dirección del disparo

	void Update()
	{
		shootDirection = Vector3.zero; // Resetear dirección

		// Flechas del teclado controlan el disparo
		if (Input.GetKey(KeyCode.UpArrow))
		{
			shootDirection = Vector3.forward;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			shootDirection = Vector3.back;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			shootDirection = Vector3.left;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			shootDirection = Vector3.right;
		}

		// Solo dispara si hay una dirección y ha pasado el cooldown
		if (shootDirection != Vector3.zero && Time.time >= nextShootTime)
		{
			Fire();
			nextShootTime = Time.time + shootRate;
		}
	}

	private void Fire()
	{
		// Instancia la flecha en la posición del spawnPoint
		GameObject newBullet = Instantiate(projectile, spawnPoint.position, Quaternion.identity);

		// Aplica velocidad a la flecha asegurando que no haya componente en Y
		Rigidbody rb = newBullet.GetComponent<Rigidbody>();
		if (rb != null)
		{
			shootDirection.y = 0f; // Aseguramos que la flecha solo se mueva en XZ
			rb.linearVelocity = shootDirection.normalized * shootForce; // Se usa linearVelocity en Unity 2023+
			rb.useGravity = false; // Desactiva la gravedad en el Rigidbody
		}

		// Ajusta la rotación de la flecha para que apunte correctamente en la dirección del disparo
		newBullet.transform.rotation = Quaternion.LookRotation(shootDirection, Vector3.up);

		// Debug para verificar que se está disparando correctamente
		Debug.Log("Disparo en dirección: " + shootDirection);
	}
}