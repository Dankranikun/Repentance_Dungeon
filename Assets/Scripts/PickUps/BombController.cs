using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController1.AddBomb();
            Destroy(gameObject);
        }
    }
}
