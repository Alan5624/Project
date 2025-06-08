using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    private Transform UIOK;
    private Transform UIScrollView;
    public GameObject packageCellPrefab;
    private void Awake()
    {
        InistantUI();
    }

    private void Start() {
        RefreshUI();
    }

    private void RefreshUI()
    {
        RectTransform scrollContent = UIScrollView.GetComponent<ScrollRect>().content;
        for (int i = 0; i < scrollContent.childCount; i++)
        {
            Destroy(scrollContent.GetChild(i).gameObject);
        }

        foreach (PackageLocalItem item in GameManager.Instance.GetPackageLocalData())
        {
            Transform packageUIItem = Instantiate(packageCellPrefab, scrollContent).transform;
            PackageCell packageCell = packageUIItem.GetComponent<PackageCell>();
            packageCell.Refresh(item, this);
        }
    }

    public override void OpenPanel(string name)
    {
        base.OpenPanel(name);
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.inputSystem.Game.Disable();
        }
        Time.timeScale = 0f; // 暫停遊戲
    }

    public override void ClosePanel(string name)
    {
        base.ClosePanel(name);
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.inputSystem.Game.Enable();
        }
        Time.timeScale = 1f; // 恢復遊戲
    }

    private void InistantUI()
    {
        InstantName();
        InstanceClick();
    }

    private void InstantName()
    {
        UIOK = transform.Find("PackagePanel/RightBottom/OK");
        UIScrollView = transform.Find("PackagePanel/LeftBottom/Scroll View");
    }

    private void InstanceClick()
    {
        UIOK.GetComponent<Button>().onClick.AddListener(OnClickOK);
    }

    private void OnClickOK()
    {
        Debug.Log("OK button clicked.");
        ClosePanel(name);
    }
}
