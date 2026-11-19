
using UnityEngine;

public class MagicRingRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f;

    void Update()
    {
        transform.Rotate(
            transform.forward,
            rotationSpeed * Time.deltaTime,
            Space.World
        );
    }
}
