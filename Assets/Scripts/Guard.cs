
using System.Collections;
using UnityEngine;

public class Guard : MonoBehaviour
{
    [Header("Path")]
    public Transform pathHolder;
    public float moveSpeed = 2f;
    public float reachDistance = 0.1f;
    public float waitTime = 1f;

    private Transform[] waypoints;
    private int currentWaypoint = 0;
    private float waitTimer = 0f;

    public float turnSpeed = 5f; 

    void Start()
    {
        if (pathHolder == null || pathHolder.childCount == 0)
        {
            Debug.LogError("Guard needs a Path with waypoints!", this);
            enabled = false;
            return;
        }

        waypoints = new Transform[pathHolder.childCount];

        for (int i = 0; i < pathHolder.childCount; i++)
        {
            waypoints[i] = pathHolder.GetChild(i);
        }

        currentWaypoint = 0;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        Transform target = waypoints[currentWaypoint];

        Vector3 targetPosition = target.position;
        targetPosition.y = transform.position.y;

        Vector3 direction = targetPosition - transform.position;

        if (direction.magnitude <= reachDistance)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                waitTimer = 0f;
            }

            return;
        }

        waitTimer = 0f;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );

        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    IEnumerator RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        while (Quaternion.Angle(transform.rotation, lookRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                lookRotation,
                Time.deltaTime * turnSpeed
            );
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        if (pathHolder == null)
            return;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
        }

        // Draw lines between waypoints.
        for (int i = 0; i < pathHolder.childCount; i++)
        {
            Transform current = pathHolder.GetChild(i);
            Transform next = pathHolder.GetChild(
                (i + 1) % pathHolder.childCount
            );

            Gizmos.DrawLine(current.position, next.position);
        }
    }
}
