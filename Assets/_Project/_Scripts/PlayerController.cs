using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, MMEventListener<HitEvent>, MMEventListener<ResumeGameEvent>,
    MMEventListener<LoseAHeartEvent>
{
    // --- Fields ---
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private Health playerHealth;

    private bool gravityFlipped = false;
    private bool isGrounded = false; // Track if the player is grounded
    private bool isStopped = false; // Track if the player is stopped

    private float distance;
    private Vector3 startPoint;
    private bool savedGravity;
    public float moveSpeed = 5f; // Player movement speed

    // --- Unity Methods ---
    private void Start()
    {
        InitializeComponents();
        distance = 0;
        SaveTheRestartPoint();
    }

    private void SaveTheRestartPoint()
    {
        startPoint = transform.position;
        savedGravity = gravityFlipped;
    }

    private void Update()
    {
        if (!isStopped)
        {
            MovePlayer();
            HandleInput();
            CheckOutOfScreen();
            distance = transform.position.x - startPoint.x; // Calculate distance
        }
    }

    #region private methods
 private void OnEnable()
    {
        this.MMEventStartListening<HitEvent>();
        this.MMEventStartListening<ResumeGameEvent>();
        this.MMEventStartListening<LoseAHeartEvent>();
    }

    private void OnDisable()
    {
        this.MMEventStopListening<HitEvent>();
        this.MMEventStopListening<ResumeGameEvent>();
        this.MMEventStopListening<LoseAHeartEvent>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollisionEnter(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        HandleCollisionExit(collision);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleTriggerEnter(other);
    }

    // --- Private Methods ---
    private void InitializeComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerHealth = GetComponent<Health>();
        rb.gravityScale = 1f; // Set initial gravity
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    private void HandleInput()
    {
        if (IsTapped() && isGrounded)
        {
            FlipGravity();
        }
    }

    private void HandleCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            playerAnimator.SetBool("isJump", false);
            playerAnimator.SetBool("isRun", true);
        }
        else if (collision.gameObject.CompareTag("Trap"))
        {
            MMEventManager.TriggerEvent(new HitEvent());
        }
    }

    private void HandleCollisionExit(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            playerAnimator.SetBool("isJump", true);
            playerAnimator.SetBool("isRun", false);
        }
    }

    private void HandleTriggerEnter(Collider2D other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
        if (other.CompareTag("Coin"))
        {
          
            MMEventManager.TriggerEvent(new EarnCoinEvent(1)); // Increase coin count
            Destroy(other.gameObject); // Destroy the coin
        }else if (other.CompareTag("MBox"))
        {
            MMEventManager.TriggerEvent(new GetBoxEvent());
            Destroy(other.gameObject); // Destroy the coin
        }else if (other.CompareTag("End"))
        {
            Debug.Log("End");
            MMEventManager.TriggerEvent(new LevelCompleteEvent());
            Time.timeScale = 0f; // Stop the game
        }
    }

    private bool IsTapped()
    {
        return Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began ||
               Input.GetMouseButtonDown(0);
    }

    private void FlipGravity()
    {
        gravityFlipped = !gravityFlipped;
        rb.gravityScale = gravityFlipped ? -1f : 1f;

        // Flip the player's scale to reflect gravity change
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }

    private void CheckOutOfScreen()
    {
        if (Mathf.Abs(transform.position.y) > 20f) // Check vertical bounds
        {
            Debug.Log("Out of screen");
            // StopPlayer();
            MMEventManager.TriggerEvent(new LoseAHeartEvent());
            Time.timeScale = 0f; // Stop the game
        }
    }

    

    #endregion
   
    // --- Event Handlers ---
    public void OnMMEvent(HitEvent eventType)
    {
        playerHealth.TakeDamage(100);
        playerAnimator.Play("Hit");
        StartCoroutine(WaitForAnimationToEnd());
    }

    private IEnumerator WaitForAnimationToEnd()
    {
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        while (stateInfo.IsName("Hit") && stateInfo.normalizedTime < 1f)
        {
            yield return null; // Wait for one frame
        }

        Time.timeScale = 0f; // Pause the game
    }

    public void OnMMEvent(LoseAHeartEvent eventType)
    {
        Debug.Log("Player lost a heart!");
        StopPlayer(); // Stop the player
    }

    public void OnMMEvent(ResumeGameEvent eventType)
    {
        Debug.Log("Game resumed!");
        ContinuePlayer(); // Resume the player
    }

    // --- Public Methods ---
    public void StopPlayer()
    {
        isStopped = true;
        rb.velocity = Vector2.zero; // Stop player movement
        playerAnimator.SetBool("isRun", false); // Stop running animation
    }

    public void ContinuePlayer() //comeback to the start point
    {
        transform.position = startPoint;
        isStopped = true;

        // Ensure gravity matches the saved state
        if (gravityFlipped != savedGravity)
        {
            FlipGravity(); // Use the existing method to flip gravity
        }

        playerAnimator.Play("Idle");
        StartCoroutine(WaitForCoolDown());
    }
    public void ContinuePlayerNoReset() 
    {
        isStopped = true;
        if (gravityFlipped != savedGravity)
        {
            FlipGravity(); // Use the existing method to flip gravity
        }
        isGrounded = true;
        playerAnimator.Play("Idle");
        StartCoroutine(WaitForCoolDown());
    }
    private IEnumerator WaitForCoolDown()
    {
             yield return new WaitForSeconds(1f); 
             isStopped = false; 
             playerAnimator.SetBool("isRun",true);
    }
    public void RunInstantly()
    {
        isStopped = false;
        if (gravityFlipped != savedGravity)
        {
            FlipGravity(); // Use the existing method to flip gravity
        }
        isGrounded = true;
        playerAnimator.SetBool("isRun", true);
    }
}
 
