using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public int defaultCapacity;
    public int maxSize;

    private List<Bullet> spawnedBullets;

    private IObjectPool<Bullet> bulletPool;

    private void Awake()
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

    private void Clear()
    {
        bulletPool.Clear();
        foreach (Bullet bullet in spawnedBullets)
        {
            Destroy(bullet.gameObject);
        }
        spawnedBullets.Clear();
    }

    private Bullet CreateBullet()
    {
        GameObject obj = Instantiate(bulletPrefab);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.pool = bulletPool;
        spawnedBullets.Add(bullet);
        return bullet;
    }

    private void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void Fire(float offsetAngleInDegrees, Boolean isDelay)
    {
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
        bullet.Fire(direction);
    }


    public void SetBullet(GameObject bulletPrefab)
    {
        Clear();
        this.bulletPrefab = bulletPrefab;
    }
}
