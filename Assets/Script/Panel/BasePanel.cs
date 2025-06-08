using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected bool isRemove = false;
    protected new string name;

    public virtual void OpenPanel(string name)
    {
        this.name = name;
        gameObject.SetActive(true);
    }

    public virtual void ClosePanel(string name)
    {
        isRemove = true;
        gameObject.SetActive(false);
        if (UIManager.Instance.panelDictionary.ContainsKey(name))
        {
            UIManager.Instance.panelDictionary.Remove(name);
        }
        else
        {
            UIManager.Instance.panelDictionary.Remove(name);
            Debug.LogError($"BasePanel: Panel {name} not found in dictionary.");
        }
        Destroy(gameObject);
    }
}
