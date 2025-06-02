using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : Charactor
{
    public BulletSpawner bulletSpawner;

    public bool isCoolDown;
    public float attackCooldownDuration;
    private InputSystem inputSystem;
    [HideInInspector] public new Rigidbody2D rigidbody2D;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    public Color flashColor = Color.red;
    public float flashDuration;

    private Color originalColor;

    private void Start() {
        originalColor = spriteRenderer.color;
    }

    private void Awake()
    {
        inputSystem = new InputSystem();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //inputSystem.Game.Fire.performed += OnFire;
        inputSystem.Game.Enable();
    }

    private void FixedUpdate()
    {
        if (inputSystem.Game.Fire.ReadValue<float>() > 0 && !isCoolDown)
        {
            Attack();
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    // private void OnFire(InputAction.CallbackContext context)
    // {
    //     if (!isCoolDown)
    //     {
    //         Attack();
    //     }
    // }

    public void Attack()
    {
        bulletSpawner.Fire(0, false);
        StartCoroutine(AttackCooldownCoroutine());
    }

    public new void Die()
    {
        inputSystem.Game.Disable();
        StopAllCoroutines();
        spriteRenderer.color = flashColor;
        Debug.Log("Player has died.");
    }

    public void FlashRed()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        isCoolDown = true;
        yield return new WaitForSeconds(attackCooldownDuration);
        isCoolDown = false;
    }
}
