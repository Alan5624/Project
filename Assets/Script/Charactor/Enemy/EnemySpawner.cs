using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;

    public float spawnRadiusMin;
    public float spawnRadiusMax;
    public float spawnInterval;

    private ObjectPool<GameObject> enemyPool;

    private void Awake()
    {
        InitializePool();
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void InitializePool()
    {
        enemyPool = new ObjectPool<GameObject>(
            CreateEnemy,
            OnGetEnemy,
            OnReleaseEnemy,
            OnDestroyEnemy,
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 50
        );
    }

    GameObject CreateEnemy() => Instantiate(enemyPrefab);

    void OnGetEnemy(GameObject enemy)
    {
        enemy.SetActive(true);
        SetupEnemy(enemy);
    }

    void OnReleaseEnemy(GameObject enemy) => enemy.SetActive(false);

    void OnDestroyEnemy(GameObject enemy) => Destroy(enemy);

    void SetupEnemy(GameObject enemy)
    {
        Enemy enemyComp = enemy.GetComponent<Enemy>();
        if (enemyComp != null)
        {
            enemyComp.playerTransform = playerTransform;
            enemyComp.SetSpawner(this);
        }
    }

    public void SpawnEnemy()
    {
        Vector2 spawnPos = GetRandomPositionOutsideRadius();
        GameObject enemy = enemyPool.Get();
        SetEnemyPosition(enemy, spawnPos);
    }

    Vector2 GetRandomPositionOutsideRadius()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(spawnRadiusMin, spawnRadiusMax);
        return (Vector2)playerTransform.position + randomDirection * randomDistance;
    }

    void SetEnemyPosition(GameObject enemy, Vector2 position)
    {
        enemy.transform.position = position;
        enemy.transform.rotation = Quaternion.identity;
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        enemyPool.Release(enemy);
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
