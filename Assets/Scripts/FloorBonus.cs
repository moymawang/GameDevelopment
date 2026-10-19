using UnityEngine;
using StarterAssets;

public class FloorBonus : MonoBehaviour
{
    public enum BonusType
    {
        Speed,
        Health
    }

    public BonusType bonusType;

    public float speedBonus = 1f;
    public int healthBonus = 1;

    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (used)
            return;

        if (bonusType == BonusType.Speed)
        {
            FirstPersonController playerMovement =
                other.GetComponent<FirstPersonController>();

            if (playerMovement != null)
            {
                playerMovement.MoveSpeed += speedBonus;

                Debug.Log("Speed increased! New speed: " +
                    playerMovement.MoveSpeed);
            }
        }

        else if (bonusType == BonusType.Health)
        {
            PlayerHealth playerHealth =
                other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.maxHealth += healthBonus;
                playerHealth.currentHealth += healthBonus;

                Debug.Log("Health increased! New max health: " +
                    playerHealth.maxHealth);
            }
        }

        used = true;
    }
}