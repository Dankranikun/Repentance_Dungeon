using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    Animator animator;

    public GameObject playerModel;
    public PlayerController1 playerController1;

    void Start()
    {
        health = 3;
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            //playerModel.SetActive(false);
            animator.SetBool("isDead", true);
            playerController1.enabled = false;
            //Destroy(gameObject);
        }
    }
}
