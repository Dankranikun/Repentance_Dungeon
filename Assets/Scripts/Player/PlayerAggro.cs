using UnityEngine;

public class PlayerAggro : MonoBehaviour
{
    public static PlayerAggro Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Previene múltiples instancias
        }
    }
}

