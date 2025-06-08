using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GNcmd
{
    [MenuItem("CMcmd/讀取BulletTable")]
    public static void ReadBulletTable()
    {
        BulletTableData bulletTableData = Resources.Load<BulletTableData>("TabIe/BulletTable");
        foreach (BulletItem item in bulletTableData.bulletItemsList)
        {
            Debug.Log($"ID: {item.id}, Name: {item.name}, Skill Description: {item.skillDescription}, Speed: {item.speed}, Damage: {item.damage}, Life Time: {item.lifeTime}");
        }
    }

    [MenuItem("CMcmd/寫入PackageLocalData")]
    public static void WritePackageLocalData()
    {
        PackageLocalData.Instance.items = new List<PackageLocalItem>();
        for (int i = 0; i <= 2; i++)
        {
            PackageLocalItem packageLocalItem = new PackageLocalItem
            {
                uid = i.ToString(),
                id = i
            };
            PackageLocalData.Instance.items.Add(packageLocalItem);
        }
        PackageLocalData.Instance.SavePackage();
    }

    [MenuItem("CMcmd/讀取PackageLocalData")]
    public static void ReadPackageLocalData()
    {
        List<PackageLocalItem> items = PackageLocalData.Instance.LoadPackage();
        foreach (PackageLocalItem item in items)
        {
            Debug.Log(item.ToString());
        }
    }

    [MenuItem("CMcmd/清除PackageLocalData")]
    public static void ClearPackageLocalData()
    {
        PackageLocalData.Instance.LoadPackage();
        PackageLocalData.Instance.items.Clear();
        PackageLocalData.Instance.SavePackage();
        Debug.Log("PackageLocalData has been cleared.");
    }

    [MenuItem("CMcmd/打開PackagePanel")]
    public static void OpenPackagePanel()
    {
        Debug.Log("打開PackagePanel");
        UIManager.Instance.OpenPanel(UIConst.PackagePanel);
    }

    [MenuItem("CMcmd/關閉PackagePanel")]
    public static void ClosePackagePanel()
    {
        Debug.Log("關閉PackagePanel");
        UIManager.Instance.ClosePanel(UIConst.PackagePanel);
        //UIManager.Instance.panelDictionary.Remove("PackagePanel");
    }

    [MenuItem("CMcmd/Test")]
    public static void Test()
    {
        var go = Resources.Load("Prefab/Panel/PackagePanel");
        Debug.Log(go == null ? "❌ 沒載到" : $"✅ 載入成功: {go.name}");
    }

    [MenuItem("CMcmd/清空panelDictionary")]
    public static void ClearPanelDictionary()
    {
        if (UIManager.Instance.panelDictionary != null)
        {
            UIManager.Instance.panelDictionary.Clear();
            Debug.Log("已清空 panelDictionary");
        }
        else
        {
            Debug.LogError("panelDictionary 為 null，無法清空");
        }
    }
}

public class GetResourcePath
{
    [MenuItem("Assets/複製 Resources.Load 路徑")]
    public static void CopyResourcesLoadPath()
    {
        Object obj = Selection.activeObject;
        string path = AssetDatabase.GetAssetPath(obj);

        int index = path.IndexOf("Resources/");
        if (index >= 0)
        {
            string resourcePath = path.Substring(index + "Resources/".Length);
            resourcePath = resourcePath.Replace(".prefab", "").Replace(".png", "").Replace(".jpg", ""); // 去副檔名
            EditorGUIUtility.systemCopyBuffer = resourcePath;
            Debug.Log($"已複製 Resources.Load 路徑：{resourcePath}");
        }
        else
        {
            Debug.LogError("該資源不在 Resources 資料夾中");
        }
    }
}