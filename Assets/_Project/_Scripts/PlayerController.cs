using System;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine;

public class PlayerController : MonoBehaviour, MMEventListener<HitEvent>,MMEventListener<ResumeGameEvent>
{
    private Rigidbody2D rb;
    private bool gravityFlipped = false;
    public float moveSpeed = 5f;
    private bool isGrounded; // Check if the player is grounded
    private Animator playerAnimator;
    private Health playerHealth;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        rb.gravityScale = 1f; // Set initial gravity
        playerHealth = GetComponent<Health>();
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

    #region PrivateMethods

      private void OnEnable()
        {
            this.MMEventStartListening<HitEvent>();
            this.MMEventStartListening<ResumeGameEvent>();
        }
      private void OnDisable()
        {
            this.MMEventStopListening<HitEvent>();
            this.MMEventStopListening<ResumeGameEvent>();
        }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            playerAnimator.SetBool("isJump", false);
            playerAnimator.SetBool("isRun", true);
        }else if (collision.gameObject.CompareTag("Trap"))
        { 
          MMEventManager.TriggerEvent(new HitEvent());  
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player is no longer colliding with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            playerAnimator.SetBool("isJump", true);
            playerAnimator.SetBool("isRun", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Debug.Log("coin collected!");
            MMEventManager.TriggerEvent(new EarnCoinEvent(1)); // tăng coin
            Destroy(other.gameObject); // xoá coin
        }
    }

    #endregion
    
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
        if (Mathf.Abs(transform.position.y) > 20f) // Check if the player is beyond 5f vertically
        {
            Debug.Log("Out screen");
            Time.timeScale = 0f; // Stop the game
        }
    }

    #region EventHandlers

     public void OnMMEvent(HitEvent eventType)
        {
            Debug.Log("Player hit a trap!");
            playerHealth.TakeDamage(100);
            playerAnimator.Play("Hit");
            StartCoroutine(WaitForAnimationToEnd());
        }
    
        private IEnumerator WaitForAnimationToEnd()
        {
            yield return new WaitForSeconds(0.5f); // đợi animation hit kết thúc (thời gian clip)
            Time.timeScale = 0f; // Dừng game sau khi animation kết thúc
            
        }

    #endregion

    public void OnMMEvent(ResumeGameEvent eventType)
    {
        playerAnimator.SetBool("isRun", true);
    }
}