using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AssignCameraToPlayer();
        RespawnPlayerAtSavedPosition(); // Nueva función para posicionar al jugador
    }

    private void AssignCameraToPlayer()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            PlayerController1 playerController = FindFirstObjectByType<PlayerController1>();
            if (playerController != null)
            {
                playerController.SetCamera(mainCamera);
            }
            else
            {
                Debug.LogWarning("No se encontró PlayerController1 en la escena.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró la cámara en la escena.");
        }
    }

    private void RespawnPlayerAtSavedPosition()
    {
        // Recuperar la posición guardada del jugador
        float x = PlayerPrefs.GetFloat("SpawnX", transform.position.x);
        float y = PlayerPrefs.GetFloat("SpawnY", transform.position.y);
        float z = PlayerPrefs.GetFloat("SpawnZ", transform.position.z);

        transform.position = new Vector3(x, y, z);
    }
}
