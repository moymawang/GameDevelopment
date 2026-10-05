using UnityEngine;

public class LaserMover : MonoBehaviour
{
    public float speed = 6f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z <= 7f)
        {
            transform.position = startPosition;
        }
    }
}