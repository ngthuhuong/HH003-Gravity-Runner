using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool gravityFlipped = false;
    public float moveSpeed = 5f;
    private bool isGrounded; // Check if the player is grounded

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f; // Set initial gravity
    }

    void Update()
    {
        // Move continuously to the right
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        if (IsTapped() && isGrounded) // Allow gravity flip only when grounded
        {
            FlipGravity();
        }
        CheckOutOfScreen(); // Check if the player is out of bounds

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player is no longer colliding with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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

        // Flip the player's scale to reflect gravity change
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }
    void CheckOutOfScreen()
    {
        if (Mathf.Abs(transform.position.y) > 5f) // Check if the player is beyond 5f vertically
        {
            Debug.Log("Out screen");
            Time.timeScale = 0f; // Stop the game
        }
    }
}