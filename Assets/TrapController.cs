using UnityEngine;

public class TrapController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object colliding with the trap has the "Player" tag
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the trap!");
        }
    }
}