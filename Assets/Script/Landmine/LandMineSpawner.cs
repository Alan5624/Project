using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Not use
public class LandMineSpawner : BaseBulletSpawner
{
    protected override void OnGetFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
        BulletCount++;
    }
    protected override void OnReleaseToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        BulletCount--;
    }
}
