using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    public float timer;
    public float damage;
    public float bulletSpeed;
    public float delayTime;
    public string uid;
    public IObjectPool<Bullet> pool;
    [HideInInspector] public new Rigidbody2D rigidbody2D;

    private bool isReturned;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        timer = 0f;
        isReturned = false;
    }

    protected virtual void OnDisable()
    {
        rigidbody2D.velocity = Vector2.zero;
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            ReturnToPool();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Charactor enemy = other.gameObject.GetComponent<Charactor>();
            if (enemy != null)
            {
                enemy.Hurt(damage);
            }
            ReturnToPool();
        }
    }

    protected virtual void ReturnToPool()
    {
        if (!isReturned)
        {
            isReturned = true;
            pool.Release(this);
        }
    }

    public void Fire(Vector2 direction)
    {
        rigidbody2D.velocity = direction * bulletSpeed;
    }

    public IEnumerator DelayEnableCollider(Collider2D collider2D)
    {
        collider2D.enabled = false;
        yield return new WaitForSeconds(delayTime);
        collider2D.enabled = true;
    }
}
