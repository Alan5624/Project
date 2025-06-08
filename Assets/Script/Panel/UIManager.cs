using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance;
    private Transform ui_root;
    private Dictionary<string, string> pathDictionary;
    private Dictionary<string, GameObject> prefabDictionary;
    public Dictionary<string, BasePanel> panelDictionary;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    public Transform UIRoot
    {
        get
        {
            if (ui_root == null)
            {
                if (GameObject.Find("Canvas") != null)
                {
                    ui_root = GameObject.Find("Canvas").transform;
                }
                else
                {
                    ui_root = new GameObject("Canvas").transform;
                }
            }
            return ui_root;
        }
    }

    private UIManager()
    {
        InstanceDictionary();
    }

    public BasePanel OpenPanel(string name)
    {
        BasePanel panel = null;
        if (panelDictionary.TryGetValue(name, out panel))
        {
            Debug.LogWarning($"UIManager: Panel {name} is already open.");
            return null;
        }

        string path = "";
        if (!pathDictionary.TryGetValue(name, out path))
        {
            Debug.LogError($"UIManager: Path for {name} not found.");
            return null;
        }

        GameObject panelPrefab = null;
        if (!prefabDictionary.TryGetValue(name, out panelPrefab))
        {
            string realPath = "Prefab/Panel/" + path;
            panelPrefab = Resources.Load<GameObject>(realPath);
            prefabDictionary.Add(name, panelPrefab);

            if (panelPrefab == null)
            {
                Debug.LogError($"❌ UIManager: Resources.Load 失敗，realPath = {realPath}");
            }
        }

        GameObject panelObject = GameObject.Instantiate(panelPrefab, UIRoot, false);
        panel = panelObject.GetComponent<BasePanel>();
        panelDictionary.Add(name, panel);
        panel.OpenPanel(name);
        return panel;
    }

    public bool ClosePanel(string name)
    {
        BasePanel panel = null;
        if (!panelDictionary.TryGetValue(name, out panel))
        {
            Debug.LogError($"UIManager: Panel {name} not found.");
            return false;
        }

        panel.ClosePanel(name);
        return true;
    }

    private void InstanceDictionary()
    {
        prefabDictionary = new Dictionary<string, GameObject>();
        panelDictionary = new Dictionary<string, BasePanel>();
        pathDictionary = new Dictionary<string, string>
        {
            { UIConst.PackagePanel, "PackagePanel" }
        };
    }
}

public class PackageItemComparrer : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem a, PackageLocalItem b)
    {
        return b.uid.CompareTo(a.uid);
    }
}