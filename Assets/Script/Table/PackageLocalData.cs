using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData
{
    private static PackageLocalData instance;
    public static PackageLocalData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PackageLocalData();
            }
            return instance;
        }
    }

    public List<PackageLocalItem> items;

    public void SavePackage()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("PackageLocalData", json);
        PlayerPrefs.Save();
    }

    public List<PackageLocalItem> LoadPackage()
    {
        if (items != null)
        {
            return items;
        }

        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            string json = PlayerPrefs.GetString("PackageLocalData");
            PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(json);
            items = packageLocalData.items;
            return items;
        }
        else
        {
            items = new List<PackageLocalItem>();
            return items;
        }
    }
}

[System.Serializable]
public class PackageLocalItem
{
    public string uid;
    public int id;

    public int lv;
    public float damage;

    public override string ToString()
    {
        return $"PackageLocalItem: UID = {uid}, ID = {id}";
    }
}