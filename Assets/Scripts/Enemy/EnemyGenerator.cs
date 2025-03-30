using TMPro;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float totalTimer = 120;
    [SerializeField] private float minTimeToSpawn = 2;
    [SerializeField] private float maxTimeToSpawn = 10;
    [SerializeField] private float timeToDecrease = 0.5f;
    GameObject targetPosition;
    float enemySpeed = 0;
    private float timeToSpawn = 0;
    private bool isFinished = false;
    private Collectable collectable;

    private void Start()
    {
        collectable = FindFirstObjectByType<Collectable>();
        targetPosition = GameObject.FindGameObjectWithTag("target");
        ScheduleNextSpawn();
    }

    private void Update()
    {
        if (isFinished || collectable == null || !collectable.isStarted)
            return;
        if (totalTimer >= 0)
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
        if(enemySpeed == 0)
            enemySpeed = enemy.GetComponent<EnemyMovement>().speed;
        ResetEnemy(enemy);
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

    private void ResetEnemy(GameObject enemy)
    {
        LeanTween.cancelAll(enemy);
        enemy.GetComponent<EnemyMovement>().speed = enemySpeed;
        enemy.GetComponent<EnemyMovement>().targetPosition = targetPosition;
        SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
    }
}
