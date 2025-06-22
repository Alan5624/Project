using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailPanel : BasePanel
{
    private Transform Descripion;
    private Transform UIIcon;
    private Transform UISkillDescripion;
    private Transform UIPrice;
    private Transform UIUpgrade;
    private Transform UILV;
    private Transform UIDamge;

    private PackageLocalItem packageLocalItem;
    private BulletItem bulletItem;
    private PackagePanel uiPanel;
    public string UID;

    private void Awake()
    {
        InistantUI();
        gameObject.SetActive(false);
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
        UIDamge = transform.Find("Center/Damage");
        UIPrice = transform.Find("Right/Price");
        UIUpgrade = transform.Find("Right/Upgrade");
        UILV = transform.Find("Left/LV");
    }

    private void InstanceClick()
    {
        UIUpgrade.GetComponent<Button>().onClick.AddListener(OnClickUpgrade);
    }

    private void OnClickUpgrade()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            if (player.money >= bulletItem.price * packageLocalItem.lv)
            {
                player.money -= bulletItem.price * packageLocalItem.lv;
                packageLocalItem.lv++;
                packageLocalItem.damage = bulletItem.damage * Mathf.Pow(1.1f, packageLocalItem.lv - 1);
                Refresh(packageLocalItem, uiPanel);
                uiPanel.RefreshUI();
                player.bulletSpawner.Clear();
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    public void Refresh(PackageLocalItem packageLocalItem, PackagePanel uiPanel)
    {
        gameObject.SetActive(true);
        this.packageLocalItem = packageLocalItem;
        bulletItem = GameManager.Instance.GetPackageLocalItemById(packageLocalItem.id);
        this.uiPanel = uiPanel;

        Descripion.GetComponent<Text>().text = bulletItem.name;
        UISkillDescripion.GetComponent<Text>().text = bulletItem.skillDescription;
        UIPrice.GetComponent<Text>().text = (bulletItem.price * packageLocalItem.lv).ToString();
        Texture2D texture2D = (Texture2D)Resources.Load<Texture2D>(bulletItem.iconPath);
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = sprite;
        UILV.GetComponent<Text>().text = "LV. " + packageLocalItem.lv.ToString();
        UIDamge.GetComponent<Text>().text = "Damage: " + ((int)packageLocalItem.damage).ToString();
    }
}