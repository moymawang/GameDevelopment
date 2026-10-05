using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Transform startPosition;

    public Slider healthSlider;

    public float healthSegmentWidth = 50f;

    private int startingMaxHealth;

    void Start()
    {
        startingMaxHealth = maxHealth;
        currentHealth = maxHealth;

        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }


        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = startPosition.position;

        maxHealth = startingMaxHealth;

        currentHealth = maxHealth;

        UpdateHealthBar();

        FloorBonus[] bonuses =
            FindObjectsByType<FloorBonus>(FindObjectsSortMode.None);

        foreach (FloorBonus bonus in bonuses)
        {
            bonus.ResetBonus();
        }

    }

    public void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;

            RectTransform sliderRect =
                healthSlider.GetComponent<RectTransform>();

            sliderRect.sizeDelta = new Vector2(
                maxHealth * healthSegmentWidth,
                sliderRect.sizeDelta.y
            );


        }
        else
        {

        }
    }
}