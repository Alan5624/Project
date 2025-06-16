using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private BulletTableData bulletTableData;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    public BulletTableData GetBulletTableData()
    {
        if (bulletTableData == null)
        {
            bulletTableData = Resources.Load<BulletTableData>("TabIe/BulletTable");
        }
        return bulletTableData;
    }

    public List<PackageLocalItem> GetPackageLocalData()
    {
        return PackageLocalData.Instance.LoadPackage();
    }

    public BulletItem GetMerchandise()
    {
        List<BulletItem> items = GetBulletTableData().bulletItemsList;
        int id = Random.Range(0, 5);
        return GetPackageLocalItemById(id);
    }

    public PackageLocalItem GetPackageLocalItemByUid(string uid)
    {
        List<PackageLocalItem> items = GetPackageLocalData();
        return items.Find(item => item.uid == uid);
    }

    public BulletItem GetPackageLocalItemById(int id)
    {
        List<BulletItem> items = GetBulletTableData().bulletItemsList;
        return items.Find(item => item.id == id);
    }
}
