using UnityEngine;

public class flocking2 : MonoBehaviour
{
    public GameObject target;

    public float neighbourRadius = 3f;
    public float separationRadius = 1f;

    public float cohesionStrength = 1f;
    public float separationStrength = 2f;
    public float targetStrength = 1f;

    public float speed = 2f;
    public float turnSpeed = 3f;

    void FixedUpdate()
    {
        Vector2 targetPosition = target.transform.position;

        foreach (Transform boid in transform)
        {
            Rigidbody2D rb = boid.GetComponent<Rigidbody2D>();

            Vector2 myPosition = rb.position;

            Vector2 cohesion = Vector2.zero;
            Vector2 separation = Vector2.zero;

            int neighbourCount = 0;

            foreach (Transform other in transform)
            {
                if (other == boid)
                    continue;

                Vector2 otherPosition = other.position;

                Vector2 difference = otherPosition - myPosition;
                float distance = difference.magnitude;

                if (distance < neighbourRadius)
                {
                    cohesion += difference;
                    neighbourCount++;

                    if (distance < separationRadius && distance > 0.001f)
                        separation -= difference.normalized / distance;
                }
            }

            if (neighbourCount > 0)
                cohesion /= neighbourCount;

            Vector2 toTarget = targetPosition - myPosition;

            Vector2 desiredDirection =
                cohesion.normalized * cohesionStrength +
                separation.normalized * separationStrength +
                toTarget.normalized * targetStrength;

            desiredDirection =
                Vector2.ClampMagnitude(desiredDirection, 1f);

            Vector2 desiredVelocity =
                desiredDirection * speed;

            rb.linearVelocity = Vector2.Lerp(
                rb.linearVelocity,
                desiredVelocity,
                turnSpeed * Time.fixedDeltaTime
            );
        }
    }
}