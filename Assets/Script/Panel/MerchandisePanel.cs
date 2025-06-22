using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchandisePanel : BasePanel
{
    private Transform Descripion;
    private Transform UIIcon;
    private Transform UISkillDescripion;
    private Transform UIPrice;
    private Transform UIBUY;

    private PackageLocalItem packageLocalItem;
    private BulletItem bulletItem;
    private PackagePanel uiPanel;
    public string UID;

    private void Awake()
    {
        InistantUI();
    }

    private void Start()
    {

    }

    private void InistantUI()
    {
        InstantName();
        InstanceClick();
    }

    private void InstantName()
    {
        Descripion = transform.Find("Left/Descripion");
        UIIcon = transform.Find("Left/Icon");
        UISkillDescripion = transform.Find("Center/SkillDescripion");
        UIPrice = transform.Find("Right/Price");
        UIBUY = transform.Find("Right/Buy");
    }

    private void InstanceClick()
    {
        UIBUY.GetComponent<Button>().onClick.AddListener(OnClickBUY);
    }

    private void OnClickBUY()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            if (player.money >= bulletItem.price)
            {
                player.money -= bulletItem.price;
                PackageLocalItem packageLocalItem = new PackageLocalItem
                {
                    uid = UID.ToString(),
                    id = bulletItem.id,
                    lv = 1,
                    damage = bulletItem.damage
                };
                PackageLocalData.Instance.items.Add(packageLocalItem);
                PackageLocalData.Instance.SavePackage();
                uiPanel.RefreshUI();
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    public void Refresh(PackageLocalItem packageLocalItem, PackagePanel uiPanel)
    {
        this.packageLocalItem = packageLocalItem;
        bulletItem = GameManager.Instance.GetPackageLocalItemById(packageLocalItem.id);
        this.uiPanel = uiPanel;

        Descripion.GetComponent<Text>().text = bulletItem.name;
        UISkillDescripion.GetComponent<Text>().text = bulletItem.skillDescription;
        UIPrice.GetComponent<Text>().text = bulletItem.price.ToString();
        Texture2D texture2D = (Texture2D)Resources.Load<Texture2D>(bulletItem.iconPath);
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = sprite;
    }
}
