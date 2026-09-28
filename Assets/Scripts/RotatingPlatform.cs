using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 45f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}