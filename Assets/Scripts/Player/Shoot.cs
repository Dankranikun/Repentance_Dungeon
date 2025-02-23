using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject projectile;

    public float shootForce = 10000f;
    public float shootRate = 0.5f;
    public Vector3 shootDirection = Vector3.zero;

    private float shootRateTime = 0;


    void Update()
    {
        shootDirection = Vector3.zero;
        // Aligns the arrow in the desired direction
        if (Input.GetKeyDown(KeyCode.UpArrow))
            shootDirection = Vector3.forward; // (0, 0, 1)
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            shootDirection = Vector3.back; // (0, 0, -1)
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            shootDirection = Vector3.left; // (-1, 0, 0)
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            shootDirection = Vector3.right; // (1, 0, 0)

        if (shootDirection != Vector3.zero)
        {
            // Asegurar que la dirección se mantenga en el plano XZ
            shootDirection.y = 0.1f;
            shootDirection.Normalize();

            // Forzar que las flechas siempre se disparen desde y = 1.18
            Vector3 spawnPosition = new Vector3(spawnPoint.position.x, 1.18f, spawnPoint.position.z);

            // Instanciar la flecha con la rotación en Z = 90°
            GameObject newBullet = Instantiate(projectile, spawnPosition, Quaternion.Euler(0, 0, 90));

            // Obtener el Rigidbody de la flecha y aplicar velocidad
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = shootDirection * (shootForce * Time.deltaTime);
            }

            // Ajustar la rotación de la flecha para que apunte correctamente en la dirección del disparo
            newBullet.transform.rotation = Quaternion.LookRotation(shootDirection, Vector3.up) * Quaternion.Euler(0, 0, 90);
        }

    }

}
