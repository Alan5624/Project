using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public float rotateSpeed;
    public float chaseDistance;
    public float minDistance;
    [SerializeField]private Charactor currentTarget;
    private float lockOnTimer;
    public float lockOnDuration;
    public LayerMask enemyLayer;

    protected override void OnEnable() {
        base.OnEnable();
        currentTarget = null;
        lockOnTimer = 0f;
    }

    protected override void Update()
    {
        base.Update();

        if (lockOnTimer > 0f)
        {
            lockOnTimer -= Time.deltaTime;
        }

        Collider2D[] chaseColliders = Physics2D.OverlapCircleAll(transform.position, chaseDistance, enemyLayer);

        // 如果搜尋不到任何敵人，解除目標
        if (chaseColliders.Length == 0)
        {
            currentTarget = null;
            rigidbody2D.angularVelocity = 0f;
        }

        // 如果當前目標已經死亡，解除目標
        if (currentTarget != null && currentTarget.CurrentHealth <= 0)
        {
            currentTarget = null;
            rigidbody2D.angularVelocity = 0f;
        }

        // 沒有目標或鎖定時間到，就重新尋找最近的敵人
        if (currentTarget == null || lockOnTimer <= 0f)
        {
            minDistance = float.MaxValue;
            Charactor nearestTarget = null;

            foreach (Collider2D chaseCollider in chaseColliders)
            {
                Charactor targetCharactor = chaseCollider.GetComponent<Charactor>();
                if (targetCharactor == null) continue;

                float distance = Vector2.Distance(transform.position, targetCharactor.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTarget = targetCharactor;
                }
            }

            // 如果找到最近的目標，則設定為當前目標:))))))))))
            if (nearestTarget != null)
            {
                currentTarget = nearestTarget;
                lockOnTimer = lockOnDuration;
            }
        }
        
        // 追蹤目前的目標
        if (currentTarget != null)
        {
            Vector2 direction = (currentTarget.transform.position - transform.position).normalized;
            float rotateAmount = Vector3.Cross(transform.up, direction).z;

            rigidbody2D.angularVelocity = rotateAmount * rotateSpeed;
            rigidbody2D.velocity = transform.up * bulletSpeed;
            //rigidbody2D.velocity = direction * bulletSpeed;
            //transform.rotation = Quaternion.LookRotation(Vector3.forward, rigidbody2D.velocity.normalized);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
