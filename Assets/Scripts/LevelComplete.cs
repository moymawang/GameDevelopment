using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField]
    GameObject levelCompleteText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelCompleteText.SetActive(true);

            MovementController playerMovement = other.GetComponent<MovementController>();

            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
        }
    }
}