using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public Camera mainCamera;
    public GameObject player; // 🔹 Referencia al Player

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
        FindPlayer();
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
            Debug.LogWarning("❌ No se encontró la cámara en la escena.");
        }
    }

    private void FindPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");

            if (player == null)
            {
                Debug.LogWarning("❌ No se encontró el Player en la escena.");
            }
            else
            {
                Debug.Log("✅ Player encontrado y asignado en GameManager.");
            }
        }
    }
    public void RegisterPlayer(GameObject playerObject)
    {
        player = playerObject;
        Debug.Log("✅ Player registrado en GameManager.");
    }


}
