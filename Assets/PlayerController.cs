using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool gravityFlipped = false;
    public float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // Set initial gravity scale to 1
    }

    void Update()
    {
        // Move continuously to the right
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        
        if (IsTapped())
        {
            FlipGravity();
        }
    }

    bool IsTapped()
    {
        // Check for touch input
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            return true;

        // Check for mouse click
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
            return true;

        return false;
    }

    void FlipGravity()
    {
        gravityFlipped = !gravityFlipped;
        rb.gravityScale = gravityFlipped ? -1f : 1f;

        // Flip the player's scale to visually represent gravity change
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }
}