using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineSpawner : BaseBulletSpawner
{
    // Start is called before the first frame update
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

    // Update is called once per frame
}
