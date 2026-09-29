using UnityEngine;

public class dirSet : MonoBehaviour
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.linearVelocity.sqrMagnitude > 0.001f)
        {
            float angle =
                Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x)
                * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
