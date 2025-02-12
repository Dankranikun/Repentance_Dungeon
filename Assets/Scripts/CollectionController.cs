using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionController : MonoBehaviour
{
    void Start() { }

    void Update() { }

    private void OnTriggerEnter(Collider collider)
    {
        PlayerController1.collectedCoins++;
        Destroy(gameObject);
    }
}
