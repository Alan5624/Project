using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBullet : Bullet
{
    public SplitBulletSpawner bulletSpawner;
    public int splitCount = 3;
    public float splitRotationOffset;
    public int id = 0;
    protected override void ReturnToPool()
    {
        float startAngle = -splitRotationOffset * (splitCount - 1) / 2f;

        for (int i = 0; i < splitCount; i++)
        {
            float angle = startAngle + i * splitRotationOffset;
            bulletSpawner.id = id;
            bulletSpawner.Fire(angle, true, uid);
        }
        base.ReturnToPool();
    }

    private void OnDestroy() {
        bulletSpawner.Clear();
    }
}
