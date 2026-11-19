
using UnityEngine;

public class AuraRingRotation : MonoBehaviour
{
    public float rotationSpeed = 80f;

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
