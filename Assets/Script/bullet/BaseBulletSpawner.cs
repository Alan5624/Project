using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BaseBulletSpawner : MonoBehaviour
{
    public AudioClip SFX;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public int defaultCapacity;
    public int maxSize;
    public string uid;
    public int BulletCount;
    public int BulletCountLimit;

    protected List<Bullet> spawnedBullets;

    protected IObjectPool<Bullet> bulletPool;

    protected virtual void Awake()
    {
        spawnedBullets = new List<Bullet>();
        bulletPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyBullet,
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public virtual void Clear()
    {
        BulletCount = 0;
        bulletPool.Clear();
        foreach (Bullet bullet in spawnedBullets)
        {
            if (bullet != null && !bullet.IsDestroyed())
            {
                Destroy(bullet.gameObject);
            }
        }
        spawnedBullets.Clear();
    }

    protected virtual Bullet CreateBullet()
    {
        GameObject obj = Instantiate(bulletPrefab);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.pool = bulletPool;
        PackageLocalItem packageLocalItem = GameManager.Instance.GetPackageLocalItemByUid(uid);
        BulletItem bulletItem = GameManager.Instance.GetPackageLocalItemById(packageLocalItem.id);
        bullet.damage = packageLocalItem.damage;
        bullet.bulletSpeed = bulletItem.speed;
        bullet.lifeTime = bulletItem.lifeTime;
        bullet.uid = uid;
        spawnedBullets.Add(bullet);
        return bullet;
    }

    protected virtual void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        BulletCount++;
    }

    protected virtual void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        BulletCount--;
    }

    protected virtual void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public virtual void Fire(float offsetAngleInDegrees, Boolean isDelay, string uid)
    {
        if (BulletCount >= BulletCountLimit && GameManager.Instance.GetPackageLocalItemByUid(uid).id == 4)
        {
            return;
        }
        this.uid = uid;
        Bullet bullet = bulletPool.Get();
        bullet.transform.position = spawnPoint.position;
        Quaternion rotation = Quaternion.Euler(0, 0, offsetAngleInDegrees) * spawnPoint.rotation;
        Vector2 direction = rotation * Vector2.up;

        bullet.transform.position = spawnPoint.position;
        bullet.transform.rotation = rotation;
        if (isDelay)
        {
            bullet.StartCoroutine(bullet.DelayEnableCollider(bullet.GetComponent<Collider2D>()));
        }
        bullet.FireBullet(direction);
        AudioSource.PlayClipAtPoint(SFX, transform.position);
    }

    public virtual void SetBullet(GameObject bulletPrefab)
    {
        Clear();
        this.bulletPrefab = bulletPrefab;
    }
}
