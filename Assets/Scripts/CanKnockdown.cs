
using UnityEngine;

public class CanKnockdown : MonoBehaviour
{
    [Header("Table Settings")]
    public float tableTopHeight = 0.5f;

    [Header("Detection Settings")]
    public float detectionMargin = 0.1f;

    private GameManager gameManager;
    private bool hasFallen = false;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("No GameManager found in the scene!");
        }
    }

    void Update()
    {
        if (hasFallen || gameManager == null)
            return;

        if (transform.position.y <
            tableTopHeight - detectionMargin)
        {
            hasFallen = true;

            gameManager.RegisterCanKnockedDown(gameObject);
        }
    }
}
