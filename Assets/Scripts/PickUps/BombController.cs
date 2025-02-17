using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombController : MonoBehaviour
{
    void Start() { }

    void Update() { }

    private void OnTriggerEnter(Collider collider)
    {
        PlayerController1.collectedBombs++;
        Destroy(gameObject);
    }
}
