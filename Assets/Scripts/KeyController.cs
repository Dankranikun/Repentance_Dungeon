using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyController : MonoBehaviour
{
    void Start() { }

    void Update() { }

    private void OnTriggerEnter(Collider collider)
    {
        PlayerController1.collectedKeys++;
        Destroy(gameObject);
    }
}
