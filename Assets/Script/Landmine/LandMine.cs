using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LandMine : ExplosionBullet
{
    // Start is called before the first frame update
    private SpriteRenderer sp;
    private Collider2D col;
    public Transform spawnPoint;
    public float FadeInTime;
    protected override void Awake()
    {
        base.Awake();
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(FadeIn(FadeInTime));
    }

    // Update is called once per frame
    protected override void Fire(Vector2 direction)
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        transform.position = mouseWorldPos;
    }
    private IEnumerator FadeIn(float duration)
    {
        float FadeTimer = 0f;
        Color color = sp.color;
        color.a = 0f;
        sp.color = color;
        while (FadeTimer < duration)
        {
            FadeTimer += Time.deltaTime;
            float alpha = Mathf.Clamp01(FadeTimer / duration);//Mathf.Clamp01(x)0~1
            color.a = alpha;
            sp.color = color;
            yield return null;
        }
        color.a = 1f;
        sp.color = color;
        
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
    }
}
