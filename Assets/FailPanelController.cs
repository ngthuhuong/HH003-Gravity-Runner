using System;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailPanelController : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI text;
    
    private void OnEnable()
    {
        
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

   public  void Hide()
    {
        gameObject.SetActive(false);
    }
   public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void DeactivePanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        MMEventManager.TriggerEvent(new ResumeGameEvent());
        
    }

    public void SetEndLifeScreen()
    {
        text.text = "You lose!";
        Show();
        Debug.Log("Player has no hearts left!");
    }

}