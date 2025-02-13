using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si hay inputs de movimiento
        if (Input.GetKey("W") || Input.GetKey("A") || Input.GetKey("S") || Input.GetKey("D"))
        {
            animator.SetBool("isWalking", true);
        }
    }
}
