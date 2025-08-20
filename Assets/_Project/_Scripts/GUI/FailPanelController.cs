using System;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FailPanelController : GUIBase
{
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        Hide();
    }

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
    
   public override void Show()
    {
        base.Show();
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
        //bổ sung them ự kiện lose game
        Debug.Log("Player has no hearts left!");
    }

}