using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private Transform UIStart;

    private void Awake()
    {
        InistantUI();
    }

    private void InistantUI()
    {
        InistantName();
        InistantClick();
    }

    private void InistantName()
    {
        UIStart = transform.Find("Center/Start");
    }
    private void InistantClick()
    {
        UIStart.GetComponent<Button>().onClick.AddListener(OnClickStart);
    }

    private void OnClickStart()
    {
        SceneManager.Instance.SwitchScene("SampleScene");
    }
}
