using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel : BasePanel
{
    private Transform UIOK;
    private Transform UIUSE;
    private Transform UIScrollView;
    public GameObject packageCellPrefab;
    private BulletItem bulletItem;
    private PackageLocalItem packageLocalItem;
    private string choseUid;

    public string ChoseUid
    {
        get
        {
            return choseUid;
        }
        set
        {
            choseUid = value;
            // Refresh
        }
    }

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
        UIUSE = transform.Find("PackagePanel/RightBottom/USE");
        UIScrollView = transform.Find("PackagePanel/LeftBottom/Scroll View");
    }

    private void InstanceClick()
    {
        UIOK.GetComponent<Button>().onClick.AddListener(OnClickOK);
        UIUSE.GetComponent<Button>().onClick.AddListener(OnClickUse);
    }

    private void OnClickUse()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            if (player.nowUsingUid == choseUid)
            {
                Debug.Log("Already using this item.");
                return;
            }
            if (choseUid == null)
            {
                Debug.Log("No item selected.");
                return;
            }
            player.nowUsingUid = choseUid;
            packageLocalItem = GameManager.Instance.GetPackageLocalItemByUid(choseUid);
            bulletItem = GameManager.Instance.GetPackageLocalItemById(packageLocalItem.id);
            player.bulletSpawner.SetBullet(bulletItem.prefab);
        }
    }
    private void OnClickOK()
    {
        Debug.Log("OK button clicked.");
        ClosePanel(name);
    }
}
