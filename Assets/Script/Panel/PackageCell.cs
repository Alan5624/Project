using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform UIIcon;

    private PackageLocalItem packageLocalItem;
    private BulletItem bulletItem;
    private PackagePanel uiPanel;

    private void Awake()
    {
        InistantUI();
    }

    private void InistantUI()
    {
        InstantName();
    }

    private void InstantName()
    {
        UIIcon = transform.Find("Item");
    }

    public void Refresh(PackageLocalItem packageLocalItem, PackagePanel uiPanel)
    {
        this.packageLocalItem = packageLocalItem;
        bulletItem = GameManager.Instance.GetPackageLocalItemById(packageLocalItem.id);
        this.uiPanel = uiPanel;

        Texture2D texture2D = (Texture2D)Resources.Load<Texture2D>(bulletItem.iconPath);
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = sprite;
    }
}
