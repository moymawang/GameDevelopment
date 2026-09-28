using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 2f;

    [SerializeField]
    float movementRange = 3f;

    float startingPosition;

    void Start()
    {
        startingPosition = transform.position.x;
    }

    void Update()
    {
        float movement = Mathf.Sin(Time.time * movementSpeed) * movementRange;

        transform.position = new Vector3(
            startingPosition + movement,
            transform.position.y,
            transform.position.z
        );
    }
}