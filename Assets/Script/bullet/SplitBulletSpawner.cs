using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBulletSpawner : BaseBulletSpawner
{
    public int id;
    protected override Bullet CreateBullet()
    {
        GameObject obj = Instantiate(bulletPrefab);
        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.pool = bulletPool;
        PackageLocalItem packageLocalItem = GameManager.Instance.GetPackageLocalItemByUid(uid);
        BulletItem bulletItem = GameManager.Instance.GetPackageLocalItemById(id);
        bullet.damage = bulletItem.damage * Mathf.Pow(1.1f, packageLocalItem.lv - 1);
        bullet.bulletSpeed = bulletItem.speed;
        bullet.lifeTime = bulletItem.lifeTime;
        bullet.uid = uid;
        spawnedBullets.Add(bullet);
        return bullet;
    }
}
