using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletTable", menuName = "ScriptableObjects/BulletTable", order = 1)]
public class BulletTableData : ScriptableObject
{
    public List<BulletItem> bulletItemsList = new List<BulletItem>();
}

[System.Serializable]
public class BulletItem
{
    public int id;
    public string name;
    public string skillDescription;
    public float speed;
    public float damage;
    public float lifeTime;
    public GameObject prefab;
    public string iconPath;

    public int price;
}