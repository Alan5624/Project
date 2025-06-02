using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Charactor : MonoBehaviour
{
    [Header("屬性")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [HideInInspector]public float CurrentHealth => currentHealth;
    [Header("無敵")]
    [SerializeField] protected bool isVulnerable;
    [SerializeField] protected float invulnerableDuration;

    public UnityEvent OnHurt;
    public UnityEvent OnDie;

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
        isVulnerable = false;
    }

    public virtual void Hurt(float damage) {
        if (!isVulnerable) {
            StartCoroutine(InVulnerableCoroutine());
            currentHealth -= damage;
            OnHurt?.Invoke();
            if (currentHealth <= 0) {
                Die();
            }
        }
    }

    public virtual void Die() {
        currentHealth = 0;
        OnDie?.Invoke();
    }

    protected virtual IEnumerator InVulnerableCoroutine() {
        isVulnerable = true;
        yield return new WaitForSeconds(invulnerableDuration);
        isVulnerable = false;
    }
}
