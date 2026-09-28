using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    [SerializeField]
    float movementSpeed = 2f;

    [SerializeField]
    float movementRange = 3f;

    float startingHeight;

    void Start()
    {
        startingHeight = transform.position.y;
    }

    void Update()
    {
        float movement = Mathf.Sin(Time.time * movementSpeed) * movementRange;

        transform.position = new Vector3(
            transform.position.x,
            startingHeight + movement,
            transform.position.z
        );
    }
}