using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float totalTimer = 120;
    [SerializeField] private float minTimeToSpawn = 2;
    [SerializeField] private float maxTimeToSpawn = 10;
    [SerializeField] private float timeToDecrease = 0.5f;
    private float timeToSpawn = 0;
    private bool isFinished = false;
    private Collectable collectable;

    private void Start()
    {
        collectable = FindFirstObjectByType<Collectable>();
        ScheduleNextSpawn();
    }

    private void Update()
    {
        if (isFinished)
            return;
        if (collectable != null && collectable.isStarted && totalTimer >= 0)
        {
            totalTimer -= Time.deltaTime;
            timeToSpawn -= Time.deltaTime;
            UIManager.Instance.UpdateTimerText(totalTimer);
            if (timeToSpawn <= 0)
                GenerateEnemy();
        }
        else if (totalTimer <= 0)
        {
            isFinished = true;
            Invoke(nameof(End), 5f);
        }
    }

    private void End()
    {
        GameManager.instance.DestroyObject();
    }

    private void GenerateEnemy()
    {
        ScheduleNextSpawn();
        float randomX = 0;
        GameObject enemy = ObjectPoolManager.Instance.GetObject();
        if (Random.Range(1, 3) == 1)
            randomX = transform.position.x + 20;
        else
            randomX = transform.position.x - 20;
        enemy.transform.position = new Vector3(randomX, transform.position.y, transform.position.z);
    }

    private void ScheduleNextSpawn()
    {
        if (maxTimeToSpawn > minTimeToSpawn)
            maxTimeToSpawn -= timeToDecrease;
        timeToSpawn = Random.Range(minTimeToSpawn, maxTimeToSpawn);
    }
}
