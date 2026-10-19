
using UnityEngine;
using TMPro;
using System.Collections;

public class GuardDetection : MonoBehaviour
{
    [Header("Guard Vision")]
    public Transform player;
    public Transform playerCameraRoot;
    public Transform eyePoint;
    public Light spotLight;

    [Header("Detection")]
    public LayerMask playerLayer;
    public LayerMask obstacleLayers;

    [Header("Respawn")]
    public Transform startPosition;

    [Header("Caught Message")]
    public TextMeshProUGUI caughtText;
    public float messageDuration = 2f;

    private bool playerCaught = false;

    void Start()
    {
        if (caughtText != null)
        {
            caughtText.gameObject.SetActive(false);
        }

        if (player == null)
            Debug.LogError(name + ": Player is not assigned!", this);

        if (playerCameraRoot == null)
            Debug.LogError(name + ": PlayerCameraRoot is not assigned!", this);

        if (eyePoint == null)
            Debug.LogError(name + ": EyePoint is not assigned!", this);

        if (spotLight == null)
            Debug.LogError(name + ": Spotlight is not assigned!", this);

        if (startPosition == null)
            Debug.LogError(name + ": StartPosition is not assigned!", this);
    }

    void Update()
    {
        if (playerCaught)
            return;

        if (player == null ||
            playerCameraRoot == null ||
            eyePoint == null ||
            spotLight == null)
        {
            return;
        }

        float detectionRange = spotLight.range;
        float detectionAngle = spotLight.spotAngle;

        bool playerNearby = Physics.CheckSphere(
            eyePoint.position,
            detectionRange,
            playerLayer,
            QueryTriggerInteraction.Ignore
        );

        if (!playerNearby)
            return;

        Vector3 direction =
            playerCameraRoot.position - eyePoint.position;

        float distance = direction.magnitude;

        if (distance <= 0.01f || distance > detectionRange)
            return;

        float angle = Vector3.Angle(
            eyePoint.forward,
            direction
        );

        if (angle > detectionAngle / 2f)
            return;

        int detectionMask = playerLayer.value | obstacleLayers.value;

        RaycastHit[] hits = Physics.RaycastAll(
            eyePoint.position,
            direction.normalized,
            distance + 0.2f,
            detectionMask,
            QueryTriggerInteraction.Ignore
        );

        RaycastHit closestHit = new RaycastHit();
        bool foundHit = false;
        float closestDistance = float.MaxValue;

        foreach (RaycastHit hit in hits)
        {
            Transform hitTransform = hit.collider.transform;

            if (hitTransform == transform ||
                hitTransform.IsChildOf(transform))
            {
                continue;
            }

            if (hit.distance < closestDistance)
            {
                closestDistance = hit.distance;
                closestHit = hit;
                foundHit = true;
            }
        }

        if (!foundHit)
            return;

        Transform firstHit = closestHit.collider.transform;

        if (firstHit == player || firstHit.IsChildOf(player))
        {
            Debug.Log(name + " detected the player.", this);
            CatchPlayer();
        }

    }

    void CatchPlayer()
    {
        if (playerCaught)
            return;

        playerCaught = true;

        if (caughtText != null)
        {
            caughtText.text = "You have been caught!";
            caughtText.gameObject.SetActive(true);
        }

        if (startPosition == null)
        {
            Debug.LogError(
                name + ": Cannot respawn because StartPosition is missing!",
                this
            );
        }
        else
        {
            CharacterController controller =
                player.GetComponent<CharacterController>();

            if (controller == null)
            {
                controller =
                    player.GetComponentInChildren<CharacterController>();
            }

            if (controller != null)
                controller.enabled = false;

            player.SetPositionAndRotation(
                startPosition.position,
                startPosition.rotation
            );

            if (controller != null)
                controller.enabled = true;

            Debug.Log(
                name + " respawned the player at " +
                startPosition.position,
                this
            );
        }

        StartCoroutine(HideCaughtMessage());
    }

    IEnumerator HideCaughtMessage()
    {
        yield return new WaitForSeconds(messageDuration);

        if (caughtText != null)
            caughtText.gameObject.SetActive(false);

        playerCaught = false;
    }

    void OnDrawGizmosSelected()
    {
        if (eyePoint == null || spotLight == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(
            eyePoint.position,
            spotLight.range
        );

        Gizmos.color = Color.red;
        Gizmos.DrawRay(
            eyePoint.position,
            eyePoint.forward * spotLight.range
        );
    }
}
