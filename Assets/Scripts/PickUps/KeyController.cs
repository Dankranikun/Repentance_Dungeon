using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController1.AddKey();
            Destroy(gameObject);
        }
    }
}
