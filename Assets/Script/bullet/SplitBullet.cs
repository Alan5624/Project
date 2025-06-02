using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBullet : Bullet
{
    public BulletSpawner bulletSpawner;
    public int splitCount = 3;
    public float splitRotationOffset;
    protected override void ReturnToPool()
    {
        float startAngle = -splitRotationOffset * (splitCount - 1) / 2f;

        for (int i = 0; i < splitCount; i++)
        {
            float angle = startAngle + i * splitRotationOffset;
            bulletSpawner.Fire(angle, true);
        }
        base.ReturnToPool();
    }
}
