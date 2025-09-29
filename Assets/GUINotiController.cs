using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUINotiController : GUIBase
{
    [SerializeField] TextMeshProUGUI txtNoti;
    // Start is called before the first frame update
     private void Awake()
        {
            GUIManager.Instance.RegisterGUIComponent("noti", this);
            Hide();
        }

    void Start()
    {
        txtNoti = transform.Find("txtNoti").GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        txtNoti = transform.Find("txtNoti").GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlertNoMap()
    {
        txtNoti.text="Bạn đã hoàn thành tất cả các bản đồ!";
        Show();
    }
}
