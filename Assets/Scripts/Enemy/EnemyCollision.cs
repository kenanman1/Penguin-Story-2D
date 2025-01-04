using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("cup"))
        {
            AudioManager.instance.PlayCollectSound();
            Destroy(collision.gameObject);
        }
    }
}
