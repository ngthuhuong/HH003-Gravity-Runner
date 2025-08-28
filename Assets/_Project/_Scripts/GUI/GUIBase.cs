using UnityEngine;
using UnityEngine.EventSystems;

public class GUIBase : MonoBehaviour
{
    // Show the GUI element
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    // Hide the GUI element
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    public virtual void ShowAndPause()
    {
        Show();
        Time.timeScale = 0; // Pause the game
    }
    public virtual void HideAndResume()
    {
        Hide();
        Time.timeScale = 1; 
    }
}