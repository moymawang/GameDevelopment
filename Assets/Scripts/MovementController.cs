using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    float jumpHeight = 2f;

    [SerializeField]
    float gravity = -9.81f;

    Vector2 moveInput;
    float verticalVelocity;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is standing on the ground
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        // Horizontal movement
        Vector3 movement = new Vector3(
            moveInput.x,
            0,
            moveInput.y
        );

        controller.Move(moveSpeed * Time.deltaTime * movement);

        // Gravity
        verticalVelocity += gravity * Time.deltaTime;

        // Vertical movement
        controller.Move(
            Vector3.up * verticalVelocity * Time.deltaTime
        );
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(
                jumpHeight * -2f * gravity
            );
        }
    }
}