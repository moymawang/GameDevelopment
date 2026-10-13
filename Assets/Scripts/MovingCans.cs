
using UnityEngine;

public class MovingCans : MonoBehaviour
{
    [Header("Side-to-Side Movement")]
    public float moveDistance = 0.6f;
    public float moveSpeed = 1f;

    [Header("Fall Detection")]
    public float tableTopHeight = 0.5f;

    private Vector3 startPosition;
    private bool stopped = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (stopped)
            return;

        // Stop moving once the can falls below the tabletop.
        if (transform.position.y < tableTopHeight - 0.1f)
        {
            stopped = true;
            return;
        }

        // All cans use the same timing, so they move together.
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        Vector3 newPosition = transform.position;
        newPosition.x = startPosition.x + offset;

        transform.position = newPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Stop this can's movement when the ball hits it.
        if (collision.gameObject.GetComponent<DragAndThrow>() != null)
        {
            stopped = true;
        }
    }
}
