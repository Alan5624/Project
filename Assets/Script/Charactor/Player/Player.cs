using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : Charactor
{
    public BaseBulletSpawner bulletSpawner;
    public bool isCoolDown;
    public float attackCooldownDuration;
    [HideInInspector]public InputSystem inputSystem;
    [HideInInspector] public new Rigidbody2D rigidbody2D;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    public Color flashColor = Color.red;
    public float flashDuration;
    public string nowUsingUid;
    public int money;

    private Color originalColor;

    private void Start()
    {
        originalColor = spriteRenderer.color;
        money = 0;
    }

    private void Awake()
    {
        inputSystem = new InputSystem();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UIManager.Instance.panelDictionary.Clear();
        nowUsingUid = "0";
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //inputSystem.Game.Fire.performed += OnFire;
        inputSystem.Game.Enable();
    }

    private void Update() {
        if (inputSystem.Game.OpenPackage.triggered)
        {
            UIManager.Instance.OpenPanel(UIConst.PackagePanel);
        }
    }

    private void FixedUpdate()
    {
        if (inputSystem.Game.Fire.ReadValue<float>() > 0 && !isCoolDown)
        {
            Attack();
        }
        if (inputSystem.Game.PutLandmine.ReadValue<float>() > 0 && !isCoolDown)
        {
            LandMineAttack();
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
        bulletSpawner.Fire(0, false, nowUsingUid);
        StartCoroutine(AttackCooldownCoroutine());
    }
    public void LandMineAttack()
    {
        bulletSpawner.Fire(0, false, nowUsingUid);
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

    private void OnGUI()
    {
        GUI.skin.label.fontSize = 30;
        GUI.Label(new Rect(20, 20, 500, 500), "Money : " + money);
        GUI.Label(new Rect(20, 60, 500, 500), "HP : " + currentHealth + "/" + maxHealth);
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
