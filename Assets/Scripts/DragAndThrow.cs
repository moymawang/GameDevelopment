
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndThrow : MonoBehaviour
{
    [Header("Throw Settings")]
    public float throwForce = 12f;
    public float upwardForce = 2f;
    public float aimSensitivity = 0.005f;

    [Header("Ball Reset")]
    public float resetDelay = 3f;

    [Header("Game Manager")]
    public GameManager gameManager;

    private Rigidbody rb;
    private Camera mainCamera;

    private bool isDragging;
    private bool isShot;

    private Vector2 dragStartPosition;
    private Vector2 dragCurrentPosition;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;

        startPosition = transform.position;
        startRotation = transform.rotation;

        if (rb == null)
            Debug.LogError("Ball needs a Rigidbody!");

        if (mainCamera == null)
            Debug.LogError("Camera needs the MainCamera tag!");

        if (gameManager == null)
            Debug.LogError("DragAndThrow needs a GameManager reference!");
    }

    void Update()
    {
        if (rb == null || mainCamera == null || gameManager == null)
            return;

        if (isShot || gameManager.IsGameOver)
            return;

        if (gameManager.chancesLeft <= 0)
            return;

        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(
                Mouse.current.position.ReadValue()
            );

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;

                    dragStartPosition =
                        Mouse.current.position.ReadValue();

                    dragCurrentPosition = dragStartPosition;

                }
            }
        }

        if (isDragging && Mouse.current.leftButton.isPressed)
        {
            dragCurrentPosition =
                Mouse.current.position.ReadValue();
        }

        if (isDragging &&
            Mouse.current.leftButton.wasReleasedThisFrame)
        {
            dragCurrentPosition =
                Mouse.current.position.ReadValue();

            ThrowBall();
        }
    }

    void ThrowBall()
    {
        isDragging = false;

        Vector2 drag = dragCurrentPosition - dragStartPosition;

        if (drag.magnitude < 10f)
        {
            return;
        }

        float horizontalAim =
            drag.x * aimSensitivity * 0.35f;

        float verticalAim =
            drag.y * aimSensitivity * 0.05f;

        Vector3 direction =
            mainCamera.transform.forward * 3f +
            mainCamera.transform.right * horizontalAim +
            Vector3.up * verticalAim;

        direction.Normalize();

        rb.isKinematic = false;

        rb.AddForce(
            direction * throwForce + Vector3.up * 0.5f,
            ForceMode.Impulse
        );

        isShot = true;

        gameManager.UseChance();

        StartCoroutine(ResetBallAfterThrow());

    }

    IEnumerator ResetBallAfterThrow()
    {
        yield return new WaitForSeconds(resetDelay);

        gameManager.CheckEndGame();

        if (gameManager.IsGameOver)
            yield break;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = true;

        transform.position = startPosition;
        transform.rotation = startRotation;

        yield return new WaitForSeconds(0.2f);

        rb.isKinematic = false;
        isShot = false;

    }
}
