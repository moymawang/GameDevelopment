using StarterAssets;
using UnityEngine;

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

    private ThirdPersonController affectedPlayerMovement;
    private PlayerHealth affectedPlayerHealth;

    private void OnTriggerEnter(Collider other)
    {

        if (used)
            return;

        affectedPlayerMovement =
            other.GetComponentInParent<ThirdPersonController>();

        affectedPlayerHealth =
            other.GetComponentInParent<PlayerHealth>();

        if (bonusType == BonusType.Speed)
        {
            if(affectedPlayerMovement != null)
{

                affectedPlayerMovement.MoveSpeed += speedBonus;

                used = true;
            }
        }
        else if (bonusType == BonusType.Health)
        {
            if (affectedPlayerHealth != null)
            {

                if (affectedPlayerHealth.currentHealth < 3)
                {
                    affectedPlayerHealth.currentHealth += healthBonus;

                    if (affectedPlayerHealth.currentHealth > 3)
                    {
                        affectedPlayerHealth.currentHealth = 3;
                    }
                }

                else if (affectedPlayerHealth.currentHealth == 3 &&
                         affectedPlayerHealth.maxHealth == 3)
                {
                    affectedPlayerHealth.maxHealth = 4;
                    affectedPlayerHealth.currentHealth = 4;
                }

                affectedPlayerHealth.UpdateHealthBar();

                used = true;
            }
        }
    }

    public void ResetBonus()
    {
        if (bonusType == BonusType.Speed)
        {
            if (affectedPlayerMovement != null)
            {
                affectedPlayerMovement.MoveSpeed -= speedBonus;
            }

            affectedPlayerMovement = null;
        }

        else if (bonusType == BonusType.Health)
        {
            affectedPlayerHealth = null;
        }

        used = false;
    }
}