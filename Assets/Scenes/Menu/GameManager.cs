using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Camera mainCamera;

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
        FindCamera();
    }

    private void FindCamera()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (mainCamera != null)
        {
            // Asegurar la posición correcta de la cámara
            mainCamera.transform.position = new Vector3(0, 50, -11);
            mainCamera.transform.rotation = Quaternion.Euler(78, 0, 0);
        }
        else
        {
            Debug.LogWarning("No se encontró la cámara en la escena.");
        }
    }
}
