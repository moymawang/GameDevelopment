using UnityEngine;

public class CameraPOV : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Vector3 offset = new Vector3(0, 6, -8);

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}