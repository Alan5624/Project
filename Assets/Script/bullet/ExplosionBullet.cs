using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public float ExplosionRadius;
    public GameObject explosionVFX;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Charactor enemy = other.gameObject.GetComponent<Charactor>();
            if (enemy != null)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Charactor enemy = collider.GetComponent<Charactor>();
                if (enemy != null)
                {
                    enemy.Hurt(damage);
                }
            }
        }
        ReturnToPool();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}
