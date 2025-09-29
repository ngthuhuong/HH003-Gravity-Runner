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
