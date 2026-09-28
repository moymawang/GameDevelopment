using UnityEngine;
public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField]
    Transform startPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController playerController = other.GetComponent<CharacterController>();

            if (playerController != null)
            {
                playerController.enabled = false;
                other.transform.position = startPosition.position;
                playerController.enabled = true;
            }
        }
    }
}