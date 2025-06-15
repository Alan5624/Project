using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Landmine : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTime;
    public float timer;
    public float ExplosionRadius;
    public float ExplosionDamage;
    public float LandmineDistance;
    public float delayTime;
    public ObjectPool<Landmine> pool;
    private bool isReturned;
    [HideInInspector] public new Rigidbody2D rigidbody2D;
    public GameObject explosionVFX;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    protected virtual void OnEnable()
    {
        timer = 0f;
        isReturned = false;
    }
    protected virtual void Disable()
    {
        rigidbody2D.velocity = Vector2.zero;
    }
    protected virtual void ReturnToPool()
    {
        if (!isReturned)
        {
            isReturned = true;
            pool.Release(this);
        }
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            ReturnToPool();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isReturned && other.CompareTag("Enemy"))
        {
            Explode();
        }
    }
    protected virtual void Explode()
    {
        isReturned = true;
        if (explosionVFX != null)
        {
            // 爆炸特效
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }
        // 爆炸特效
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, ExplosionRadius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Charactor enemy = hit.GetComponent<Charactor>();
                    if (enemy != null)
                    {
                        enemy.Hurt(ExplosionDamage);
                    }
                }
            }
            ReturnToPool();
        }
    }
}