using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

public class FailPanelController : MonoBehaviour
{
    private Button closeButton;

    private void OnEnable()
    {
        // Tìm button trong children thay vì trên chính Panel
        closeButton = GetComponentInChildren<Button>();

        if (closeButton == null)
        {
            Debug.LogError("Close button not found!");
        }
        else
        {
            // Gán sự kiện click
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(DeactivePanel);
        }
    }

    public void DeactivePanel()
    {
        gameObject.SetActive(false);
        MMEventManager.TriggerEvent(new ResumeGameEvent());
        Time.timeScale = 1;
    }
}