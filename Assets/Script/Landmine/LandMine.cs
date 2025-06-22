using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LandMine : ExplosionBullet
{
    private SpriteRenderer sp;
    public Transform spawnPoint;
    public float FadeInTime;
    protected override void Awake()
    {
        base.Awake();
        sp = GetComponent<SpriteRenderer>();

    }
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(FadeIn(FadeInTime));
    }

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
}
