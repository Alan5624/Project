using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : Charactor
{
    public float speed;
    public Vector2 MovementInput;
    public Transform playerTransform;
    public int money;

    public float damage;
    [Header("擊退")]
    public bool isKnockback;
    public float knockbackForce;
    public float knockbackDuration;
    public float knockbackTimer;
    [HideInInspector] public SpriteRenderer spriteRenderer;
    [HideInInspector] public new Collider2D collider2D;
    [HideInInspector] public new Rigidbody2D rigidbody2D;

    public Color flashColor = Color.red;
    public float flashDuration;

    private Color originalColor;

    private EnemySpawner spawner;

    protected virtual void Start()
    {
        originalColor = spriteRenderer.color;
    }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void SetSpawner(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }
    }


    protected virtual void FixedUpdate()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            MovementInput = direction * speed;
            rigidbody2D.velocity = MovementInput;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

        if (knockbackTimer > 0)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            Vector2 knockbackDirection = (transform.position - playerTransform.position).normalized;
            rigidbody2D.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Charactor player = other.gameObject.GetComponent<Charactor>();
            if (player != null)
            {
                player.Hurt(damage);
                ReturnToPool();
            }
        }
    }

    public virtual void Hurt()
    {
        if (isKnockback)
        {
            knockbackTimer = knockbackDuration;
        }
        FlashRed();
    }

    public virtual new void Die()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.money += money;
        }
        ReturnToPool();
    }

    public virtual void ReturnToPool()
    {
        spriteRenderer.color = originalColor;
        if (spawner != null)
        {
            spawner.ReleaseEnemy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void FlashRed()
    {
        StartCoroutine(FlashRoutine());
    }

    protected virtual IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}
