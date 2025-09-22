using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private float blinkInterval = 0.2f; // Time between blinks
    private Renderer shieldRenderer;
    private bool isBlinking = false;
    private Color originalColor;

    void Start()
    {
        shieldRenderer = GetComponent<Renderer>();
        if (shieldRenderer == null)
        {
            Debug.LogError("Renderer not found on the shield object!");
            return;
        }

        originalColor = shieldRenderer.material.color; // Store the original color
        StartBlinking();
    }

    public void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            StartCoroutine(BlinkEffect());
        }
    }

    public void StopBlinking()
    {
        isBlinking = false;
        if (shieldRenderer != null)
        {
            // Restore the original color when blinking stops
            shieldRenderer.material.color = originalColor;
        }
    }

    private IEnumerator BlinkEffect()
    {
        while (isBlinking)
        {
            if (shieldRenderer != null)
            {
                // Toggle between full transparency and the original color
                Color color = shieldRenderer.material.color;
                color.a = Mathf.Approximately(color.a, 1f) ? 0.2f : 1f; // Adjust alpha
                shieldRenderer.material.color = color;
            }
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}