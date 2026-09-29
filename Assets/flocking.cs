using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class flocking : MonoBehaviour
{
    public float neighbourRadius = 3f;
    public float separationRadius = 1f;

    public float cohesionStrength = 1f;
    public float separationStrength = 2f;
    public float targetStrength = 1f;

    public float speed = 2f;

    public GameObject target;

    void Update()
    {
        Vector2 targetPosition =
            transform.InverseTransformPoint(target.transform.position);


        foreach (Transform boid in transform)
        {
            Vector2 myPosition = boid.localPosition;

            Vector2 cohesion = Vector2.zero;
            Vector2 separation = Vector2.zero;

            int neighbourCount = 0;

            foreach (Transform other in transform)
            {
                if (other == boid)
                    continue;

                Vector2 otherPosition = other.localPosition;
                Vector2 difference = otherPosition - myPosition;
                float distance = difference.magnitude;

                if (distance < neighbourRadius)
                {
                    cohesion += difference;
                    neighbourCount++;

                    if (distance < separationRadius)
                    {
                        separation -= difference.normalized / distance;
                    }
                }
            }

            if (neighbourCount > 0)
                cohesion /= neighbourCount;

            Vector2 toTarget = targetPosition - myPosition;

            Vector2 direction =
                cohesion.normalized * cohesionStrength +
                separation.normalized * separationStrength +
                toTarget.normalized * targetStrength;

            direction = Vector2.ClampMagnitude(direction, 1f);

            boid.localPosition += (Vector3)(direction * speed * Time.deltaTime);
        }
    }
}
