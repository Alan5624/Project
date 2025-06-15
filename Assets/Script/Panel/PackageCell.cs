using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PackageCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    private Transform UIIcon;
    private Transform UILV;
    private Transform UIChose;

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
        UILV = transform.Find("LV");
        UIChose = transform.Find("Chose");
    }

    public void Refresh(PackageLocalItem packageLocalItem, PackagePanel uiPanel)
    {
        this.packageLocalItem = packageLocalItem;
        bulletItem = GameManager.Instance.GetPackageLocalItemById(packageLocalItem.id);
        this.uiPanel = uiPanel;

        Texture2D texture2D = (Texture2D)Resources.Load<Texture2D>(bulletItem.iconPath);
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0));
        UIIcon.GetComponent<Image>().sprite = sprite;

        UILV.GetComponent<Text>().text = "LV. " + packageLocalItem.lv.ToString();
        UIChose.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        uiPanel.ChoseUid = packageLocalItem.uid;
        uiPanel.RefreshDetail(packageLocalItem);
        Debug.Log("ChoseUid: " + uiPanel.ChoseUid);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIChose.gameObject.SetActive(true);
        Debug.Log("OnPointerEnter: " + packageLocalItem.uid);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIChose.gameObject.SetActive(false);
        Debug.Log("OnPointerExit: " + packageLocalItem.uid);
    }
}
