using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Quitar vida enemigos
        }

        // Destruir la flecha al chocar con cualquier objeto
        Destroy(gameObject, 0.25f);
    }
}