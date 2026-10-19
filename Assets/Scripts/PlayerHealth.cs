using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Transform startPosition;

    private CharacterController controller;

    void Start()
    {
        currentHealth = maxHealth;

        controller = GetComponent<CharacterController>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            Debug.Log("Player has no health left!");

            Respawn();
        }
    }

    void Respawn()
    {
        if (controller != null)
        {
            controller.enabled = false;
        }

        transform.position = startPosition.position;

        if (controller != null)
        {
            controller.enabled = true;
        }

        currentHealth = maxHealth;

        Debug.Log("Player respawned!");
        Debug.Log("Player Health: " + currentHealth);
    }
}