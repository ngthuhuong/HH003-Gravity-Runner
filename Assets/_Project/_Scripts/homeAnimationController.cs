using System.Collections;
using UnityEngine;

public class homeAnimationController : MonoBehaviour
{
    private Animator animator;
    private float idleTime = 2f; // Time spent in idle animation

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
            return;
        }

        StartCoroutine(AnimationLoop());
    }

    private IEnumerator AnimationLoop()
    {
        while (true)
        {
            // Idle animation
            animator.Play("Idle");
            yield return new WaitForSeconds(idleTime);

           
                animator.SetBool("isRun", true);
                yield return new WaitForSeconds(1f); // Run for 3 seconds
                animator.SetBool("isRun", false);
                animator.Play("Idle");
                yield return new WaitForSeconds(idleTime);
            // Jump animation
            animator.SetBool("isDbJump", true);
            yield return new WaitForSeconds(1f); // Jump duration
            animator.SetBool("isDbJump", false);
        }
    }
}